// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/25/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IWorkProductDelieveryApplicationService
    {
        Task SendWorkProductDeliveryDocumentAsync();
    }
}