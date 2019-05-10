// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/01/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Messages.Responses;
using ShareFile.Api.Client;
using ShareFile.Api.Client.Logging;
using ShareFile.Api.Client.Models;
using ShareFile.Api.Client.Security.Authentication.OAuth2;

namespace Apollo.Infrastructure.Providers
{
    public class FileShareProvider //: IFileShareProvider
    {
        private readonly ILogManager _logManager;
        private readonly IAuditConfiguration _auditConfiguration;

        public FileShareProvider(ILogManager logManager, IAuditConfiguration auditConfiguration)
        {
            _logManager = logManager;
            _auditConfiguration = auditConfiguration;
        }

        public GetResponse<string> CreateShareFolderLink(string folderName, string description = "")
        {
            var response = new GetResponse<string>();

            try
            {
                var client = PasswordAuthentication();
                var principal = StartSession(client);

                var defaultUserFolder = LoadFolder(client);
                var createdFolder = CreateFolder(client, defaultUserFolder, folderName, description);
                SetUploadNotification(client, createdFolder, principal);
                var share = CreateShare(createdFolder);

                var createdShare = client.Shares.Create(share).Execute();
                response.Content = createdShare.Uri.AbsoluteUri;
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "FileShareProvider.CreateShareLinkAsync");
                response.AddError(e);
            }

            return response;
        }
        public async Task<GetResponse<string>> CreateShareFolderLinkAsync(string folderName, string description = "")
        {
            var response = new GetResponse<string>();

            try
            {
                var client = await PasswordAuthenticationAsync();
                var principal = await StartSessionAsync(client);

                var defaultUserFolder = await LoadFolderAsync(client);
                var createdFolder = await CreateFolderAsync(client, defaultUserFolder, folderName, description);

                SetUploadNotification(client,createdFolder, principal);

                var share = CreateShare(createdFolder);

                var createdShare = await client.Shares.Create(share).ExecuteAsync();
                response.Content = createdShare.Uri.AbsoluteUri;

            }
            catch (Exception e)
            {
                _logManager.LogError(e, "FileShareProvider.CreateShareLinkAsync");
                response.AddError(e);
            }

            return response;
        }
        private Folder CreateFolder(ShareFileClient client, Folder parentFolder, string newFolderName, string description)
        {
            var newFolder = CreateFolder(newFolderName, description);

            return client.Items.CreateFolder(parentFolder.url, newFolder, overwrite: true, passthrough:true).Execute();
        }
        private async Task<Folder> CreateFolderAsync(ShareFileClient client, Folder parentFolder, string newFolderName, string description)
        {
            var newFolder = CreateFolder(newFolderName, description);
            
            return await client.Items.CreateFolder(parentFolder.url, newFolder, overwrite: true).ExecuteAsync();
        }
        private Folder LoadFolder(ShareFileClient client)
        {
            var folder = (Folder)client.Items.Get().Expand("Children").Execute();

            return folder;
        }
        private async Task<Folder> LoadFolderAsync(ShareFileClient client)
        {
            var folder = (Folder) await client.Items.Get().Expand("Children").ExecuteAsync();

            return folder;
        }
        private ShareFileClient PasswordAuthentication()
        {
            var shareFileConfiguration = _auditConfiguration.ShareFileConfiguration;
            
            var configuration = Configuration.Default();
            configuration.Logger = new DefaultLoggingProvider();

            var client = new ShareFileClient(shareFileConfiguration.Url, configuration);
            var oauthService = new OAuthService(client, shareFileConfiguration.ClientId, shareFileConfiguration.ClientSecret);

            // Perform a password grant request.
            var oauthToken = oauthService.PasswordGrantAsync(
                shareFileConfiguration.UserName, 
                shareFileConfiguration.Password, 
                shareFileConfiguration.SubDomain, 
                shareFileConfiguration.ControlPlane).Result;

            // Add credentials and update client with new BaseUri
            client.AddOAuthCredentials(oauthToken);
            client.BaseUri = oauthToken.GetUri();

            return client;
        }
        private async Task<ShareFileClient> PasswordAuthenticationAsync()
        {
            var shareFileConfiguration = _auditConfiguration.ShareFileConfiguration;
            var configuration = Configuration.Default();
            configuration.Logger = new DefaultLoggingProvider();

            var client = new ShareFileClient(@"https://secure.sf-api.com/sf/v3/", configuration);
            var oauthService = new OAuthService(client, shareFileConfiguration.ClientId, shareFileConfiguration.ClientSecret);

            // Perform a password grant request.
            var oauthToken = await oauthService.PasswordGrantAsync(shareFileConfiguration.UserName, shareFileConfiguration.Password, shareFileConfiguration.SubDomain, shareFileConfiguration.ControlPlane);

            // Add credentials and update client with new BaseUri
            client.AddOAuthCredentials(oauthToken);
            client.BaseUri = oauthToken.GetUri();

            return client;
        }
        private Principal StartSession(ShareFileClient client)
        {
            var session = client.Sessions.Login().Expand("Principal").Execute();
            return session.Principal;
        }
        private async Task<Principal> StartSessionAsync(ShareFileClient client)
        {
            var session = await client.Sessions.Login().Expand("Principal").ExecuteAsync();
            return session.Principal;
        }
        private Folder CreateFolder(string newFolderName, string description)
        {
            var folder = new Folder
            {
                Name = newFolderName,
                Description = description
            };

            return folder;
        }
        private Share CreateShare(Folder createdFolder)
        {
            return new Share
            {
                Parent = new Item { Id = createdFolder.Id},
                ShareType = ShareType.Request
            };
        }
        private void SetUploadNotification(ShareFileClient client, Folder createdFolder, Principal principal)
        {
            var acl = new AccessControl
            {
                NotifyOnUpload = true,
                NotifyOnDownload = true,
                Principal = principal,
                IsOwner = true,
                CanDownload = true,
                CanUpload = true
            };
            client.AccessControls.CreateByItem(createdFolder.url, acl,sendDefaultNotification:true);

            var foo = client.AccessControls.GetByItem(createdFolder.url).Top(1).Execute();
        }
    }

}