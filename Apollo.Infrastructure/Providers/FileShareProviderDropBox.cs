using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Messages.Responses;
using Dropbox.Api;
using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;

namespace Apollo.Infrastructure.Providers
{
    public class FileShareProviderDropBox
    {
        private ILogManager _logManager;
        private IAuditConfiguration _auditConfiguration;

        public FileShareProviderDropBox(ILogManager logManager, IAuditConfiguration auditConfiguration)
        {
            _logManager = logManager;
            _auditConfiguration = auditConfiguration;
        }
        public GetResponse<string> CreateShareFolderLink(string folderName, string description)
        {
            return CreateShareFolderLinkAsync(folderName, description).Result;
        }

        public async Task<GetResponse<string>> CreateShareFolderLinkAsync(string folderName, string description)
        {
            var response = new GetResponse<string>();

            try
            {
                // Create Client
                using (var client = new DropboxClient(_auditConfiguration.ShareFileConfiguration.Token))
                {
                    var account = await client.Users.GetCurrentAccountAsync();

                    var clientWitRoot = client.WithPathRoot(new PathRoot.Root(account.RootInfo.RootNamespaceId));

                    var fullpath = $@"/Audit Files/{folderName}";
                   
                    // Create Folder
                    var folder = await clientWitRoot.Files.CreateFolderV2Async(fullpath, true);

                    // Create link
                    var link = await clientWitRoot.Sharing.CreateSharedLinkWithSettingsAsync(fullpath);

                    response.Content = link.Url;
                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "FileShareProviderDropbox.CreateShareLinkAsync");
                response.AddError(e);
            }

            return response;
        }
    }
}
