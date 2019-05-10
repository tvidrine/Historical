// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{
    public class PrincipalSummary
    {
        public string PrincipalTitle { get; set; }
        public string PrincipalName { get; set; }
        public int PrincipalGross { get; set; }
        public int PrincipalNet { get; set; }
        public int PrincipalDays { get; set; }
        public string PrincipalState { get; set; }
        public string PrincipalInclExcl { get; set; }
        public string PrincipalDuties { get; set; }
        public string PrincipalClass { get; set; }
        public string PrincipalClassDescp { get; set; }
    }
}