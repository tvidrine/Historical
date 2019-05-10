using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Domain.Communication;
using Apollo.Core.Messages.Results;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;

namespace Apollo.Infrastructure.Providers
{
    public class EmailProvider : ICommunicationProvider
    {
        private readonly EmailConfiguration _configuration;
        private readonly ILogManager _logManager;

        public EmailProvider(IAuditConfiguration configuration, ILogManager logManager)
        {
            _configuration = configuration.EmailConfiguration;
            _logManager = logManager;
        }

        public bool Clear(ClientConfiguration clientConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearAsync(ClientConfiguration clientConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task<GetResult<IReadOnlyList<Packet>>> GetPacketsAsync(ClientConfiguration clientConfiguration = null)
        {
            var getResult = new GetResult<IReadOnlyList<Packet>>();
            var userName = clientConfiguration == null ? _configuration.PopUsername : clientConfiguration.UserName;
            var password = clientConfiguration == null ? _configuration.PopPassword : clientConfiguration.Password;

            try
            {
                using (var emailClient = new Pop3Client())
                {
                    emailClient.Connect(_configuration.PopServer, _configuration.PopPort, true);
                    await emailClient.AuthenticateAsync(userName, password);

                    var list = new List<Packet>();
                    for (var i = 0; i < emailClient.Count; i++)
                    {
                        var message = emailClient.GetMessage(i);
                        var packet = new Packet()
                        {
                            Message = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                            Topic = message.Subject
                        };

                        // Check for attachments
                        if (message.Attachments.Any())
                        {
                            foreach (var attachment in message.Attachments)
                            {
                                var part = (MimePart)attachment;

                                using (var stream = new MemoryStream())
                                {
                                    part.Content.DecodeTo(stream);
                                    packet.Data = stream.ToArray();
                                }
                            }
                        }

                        list.Add(packet);
                        emailClient.DeleteMessage(i);
                    }

                    getResult.Content = list;
                    getResult.Succeeded = true;
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, $@"EmailProvider.GetPacketAsync");
                getResult.Succeeded = false;
                getResult.Errors.Add(new CommunicationError
                {
                    Code = e.HResult.ToString(),
                    Description = e.Message
                });
            }
            return getResult;
        }

        public async Task<SendResult> SendPacketAsync(Packet packet, ClientConfiguration clientConfiguration = null)
        {
            return await Task.Run(() =>
            {
                var sendResult = new SendResult();

                try
                {
                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(_configuration.InfoAddress));
                    mimeMessage.To.Add(new MailboxAddress(packet.Recipient));
                    mimeMessage.Subject = packet.Topic;

                    var builder = new BodyBuilder {TextBody = packet.Message};
                    if(!string.IsNullOrEmpty(packet.Filename))
                        builder.Attachments.Add(packet.Filename, packet.Data);

                    mimeMessage.Body = builder.ToMessageBody();

                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect(
                            _configuration.SmtpServer, 
                            _configuration.SmtpPort, 
                            true);
                        emailClient.Authenticate(
                            _configuration.SmtpUsername, 
                            _configuration.SmtpPassword);
                        emailClient.Send(mimeMessage);
                        emailClient.Disconnect(true);
                    }

                    sendResult.Succeeded = true;

                }
                catch (Exception e)
                {
                    _logManager.LogError(e, $@"EmailProvider.SendPacketAsync: {packet.ToString()}");
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