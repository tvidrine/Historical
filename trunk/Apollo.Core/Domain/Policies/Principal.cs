// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class Principal : ModelBase      
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}