// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentFTP;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Domain.Communication;
using Apollo.Core.Messages.Results;

namespace Apollo.Infrastructure.Providers
{
    public class FtpProvider : ICommunicationProvider
    {
        private readonly FtpConfiguration _configuration;
        private readonly ILogManager _logManager;

        public FtpProvider(IAuditConfiguration configuration, ILogManager logManager)
        {
            _logManager = logManager;
            _configuration = configuration.FtpConfiguration;
        }

        public bool Clear(ClientConfiguration clientConfiguration)
        {
            try
            {
                // 1. Get a connect
                var ftpClient = new FtpClient
                {
                    Host = _configuration.Host,
                    Port = _configuration.Port,
                    Credentials = new NetworkCredential(_configuration.Username, _configuration.Password)
                };

                ftpClient.Connect();

                // 2. Get list of files
                var clientFolder = $@"{_configuration.ClientFolder}/{clientConfiguration.ClientName}";
                foreach (var item in ftpClient.GetListing(clientFolder))
                    if (item.Type == FtpFileSystemObjectType.File)
                        ftpClient.DeleteFile(item.FullName);


                ftpClient.Disconnect();

                return true;
            }
            catch (Exception e)
            {
                _logManager.LogError(e, $@"FtpProvider.Clear");

                return false;
            }
        }

        public async Task<bool> ClearAsync(ClientConfiguration clientConfiguration)
        {
            try
            {
                // 1. Get a connect
                var ftpClient = new FtpClient
                {
                    Host = _configuration.Host,
                    Port = _configuration.Port,
                    Credentials = new NetworkCredential(_configuration.Username, _configuration.Password)
                };

                await ftpClient.ConnectAsync();

                // 2. Get list of files
                var clientFolder = $@"{_configuration.ClientFolder}/{clientConfiguration.ClientName}";
                foreach (var item in ftpClient.GetListing(clientFolder))
                    if(item.Type == FtpFileSystemObjectType.File)
                        ftpClient.DeleteFile(item.FullName);

                ftpClient.Disconnect();

                return true;
            }
            catch (Exception e)
            {
                _logManager.LogError(e, $@"FtpProvider.ClearAsync");

                return false;
            }
        }

        public async Task<GetResult<IReadOnlyList<Packet>>> GetPacketsAsync(ClientConfiguration clientConfiguration)
        {
            var getResult = new GetResult<IReadOnlyList<Packet>>();
            var userName = clientConfiguration.UserName;
            var password = clientConfiguration.Password;

            try
            {
                // 1. Get a connect
                var ftpClient = new FtpClient
                {
                    Host = _configuration.Host,
                    Port = _configuration.Port,
                    Credentials = new NetworkCredential(_configuration.Username, _configuration.Password)
                };

                await ftpClient.ConnectAsync();

                // 2. pull any available files
                var list = new List<Packet>();
                var clientFolder = $@"{_configuration.ClientFolder}/{clientConfiguration.ClientName}";

                foreach (var item in ftpClient.GetListing(clientFolder))
                {
                    if (item.Type == FtpFileSystemObjectType.File)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await ftpClient.DownloadAsync(ms, item.FullName);
                            var packet = new Packet
                            {
                                Filename = item.Name,
                                Data = ms.ToArray()
                            };

                            list.Add(packet);

                            // Move filed to processed folder
                            var processedFolder = $@"{clientFolder}/processed";
                            
                            if (!ftpClient.DirectoryExists(processedFolder))
                                ftpClient.CreateDirectory(processedFolder);

                            ftpClient.MoveFile(item.FullName, $@"{processedFolder}/{item.Name}", FtpExists.Overwrite);
                        }
                    }
                }
                    

                ftpClient.Disconnect();
                getResult.Content = list;
                getResult.Succeeded = true;
            }
            catch (Exception e)
            {
                _logManager.LogError(e, $@"FtpProvider.GetPacketAsync");
                getResult.Succeeded = false;
                getResult.Errors.Add(new CommunicationError
                {
                    Code = e.HResult.ToString(),
                    Description = e.Message
                });
            }

            return getResult;
        }

        public async Task<SendResult> SendPacketAsync(Packet packet, ClientConfiguration clientConfiguration)
        {
            return await Task.Run(() =>
            {
                var sendResult = new SendResult();

                try
                {
                    var ftpClient = new FtpClient
                    {
                        Host = _configuration.Host,
                        Port = _configuration.Port,
                        SslProtocols = SslProtocols.Tls,
                        Credentials = new NetworkCredential(
                            _configuration.Username,
                            _configuration.Password)
                    };

                    ftpClient.Connect();

                    var ftpFile = $@"{_configuration.ClientFolder}/{clientConfiguration.ClientName}/{packet.Filename}";
                    using (var ms = new MemoryStream(packet.Data))
                    {
                        ftpClient.Upload(ms, ftpFile, FtpExists.Overwrite);
                    }
                    
                    ftpClient.Disconnect();
                    sendResult.Succeeded = true;
                }
                catch (Exception e)
                {
                    _logManager.LogError(e, $@"EmailProvider.SendPacketAsync: {packet}");
                    sendResult.Succeeded = false;
                    sendResult.Errors.Add(new CommunicationError
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    });
                }

                return sendResult;
            });
        }
    }
}