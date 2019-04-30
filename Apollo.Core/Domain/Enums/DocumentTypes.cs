// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/04/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Apollo.Core.Domain.Enums
{
    public enum DocumentTypes
    {
        [Description("Not Set"), Category("NotSet")]
        NotSet = 0,
        [Description("Welcome Letter (GL)"), Category("WelcomeLetter")]
        WelcomeLetterGl = 1,
        [Description("Welcome Letter (WC)"), Category("WelcomeLetter")]
        WelcomeLetterWc = 2,
        [Description("Welcome Letter (SA)"), Category("WelcomeLetter")]
        WelcomeLetterSa = 3,
        [Description("Welcome Letter Batch"), Category("Batch")]
        WelcomeLetterBatch = 4,
        [Description("Verification Sales"), Category("Verification")]
        VerificationSales = 5,
        [Description("Certificates of Insurance"), Category("COI")]
        CertificateOfInsurance = 6,
        [Description("Client Import"), Category("Import")]
        ClientImport = 7
    }
}