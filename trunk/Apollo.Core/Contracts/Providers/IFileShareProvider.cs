// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/01/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Providers
{
    public interface IFileShareProvider
    {
        GetResponse<string> CreateShareFolderLink(string folderName, string description);
        Task<GetResponse<string>> CreateShareFolderLinkAsync(string folderName, string description);
    }
}