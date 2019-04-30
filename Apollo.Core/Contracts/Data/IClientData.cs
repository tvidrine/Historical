// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace Apollo.Core.Contracts.Data
{
    public interface IClientData
    {
        bool IsSuccessful { get; set; }
        Stream Data { get; set; }
    }
}