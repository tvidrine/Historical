// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;

namespace Apollo.Core.DomainServices.Validators
{
    public class ClientSettingValidator : ValidatorBase<IClientSetting>, IClientSettingValidator
    {
        public async Task<bool> IsValidAsync(IClientSetting instance)
        {
            return await Task.FromResult(true);
        }
    }
}