// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/21/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{
    public class Claim
    {
        public string ClaimStatus { get; set; }
        public string ClaimantName { get; set; }
        public string DOL { get; set; }
        public string InjuryDesc { get; set; }
        public string ClassCode { get; set; }
    }
}