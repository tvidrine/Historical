// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/03/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Domain.Common
{
    [Flags]
    public enum DocumentTypesToChange
    {
        NotSet = 0,
        CertificateOfInsurance = 0x1,
        ProfitLossStatement = 0x2,
        Verification = 0x4,
        Other = 0xFF
    }

    public static class DocumentTypesExt
    {
        public static DocumentTypesToChange ToDocumentTypes(this string value)
        {
            var documentTypes = (DocumentTypesToChange)Enum.Parse(typeof(DocumentTypesToChange), value);
            return documentTypes;
        }
    }
}