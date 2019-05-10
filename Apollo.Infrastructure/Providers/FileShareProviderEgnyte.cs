using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.FileShare;

namespace Apollo.Infrastructure.Providers
{
    public class FileShareProviderEgnyte : IFileShareProvider
    {
        private readonly ILogManager _logManager;
        private readonly IAuditConfiguration _auditConfiguration;
        private readonly HttpClient _client;

        public FileShareProviderEgnyte(ILogManager logManager, IAuditConfiguration auditConfiguration)
        {
            _logManager = logManager;
            _auditConfiguration = auditConfiguration;
            _client = RestApiClientFactory.GetClient(auditConfiguration.ShareFileConfiguration.BaseUrl);
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
                var auditFileFolder = $@"{_auditConfiguration.ShareFileConfiguration.BaseShareFolder}/{folderName}";

                // Setup headers
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _auditConfiguration.ShareFileConfiguration.Token);
                _client.DefaultRequestHeaders.Accept.Clear();
                
                // Create the folder
                var folderId = await CreateFolderAsync(auditFileFolder);

                // Return the link
                return await CreateLinkAsync(auditFileFolder);
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "FileShareProviderEgnyte.CreateShareLinkAsync");
                response.AddError(e);
            }

            return response;
        }

        private async Task<bool> CreateFolderAsync(string folderName)
        {
           

            var response = await _client.PostAsJsonAsync(
                $@"/pubapi/v1/fs/shared/{folderName}",
                new
                {
                    action = "add_folder"
                });

            
            return response.IsSuccessStatusCode;
        }
        
        private async Task<GetResponse<string>> CreateLinkAsync(string folderName)
        {
            var response = new GetResponse<string>();
           
            var postResponse = await _client.PostAsJsonAsync(
                $@"/pubapi/v1/links",
                new
                {
                    path = $@"/Shared/{folderName}",
                    type = "upload",
                    notify = "true"
                });

            if (postResponse.IsSuccessStatusCode)
            {
                var results = await postResponse.Content.ReadAsAsync<EgnyteCreateLinkResponse>();
                if(results.Links.Any())
                    response.Content = results.Links.First().Url;
            }


            return response;
        }
    }
}