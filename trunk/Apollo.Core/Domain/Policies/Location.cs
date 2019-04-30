// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class Location : ModelBase, ILocation
    {
        public Guid PrincipalId { get; set; }
        public IList<Claim> Claims { get; set; }
        public RateSplit RateSplit { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }
        public IAddress MainAddress { get; set; }
        public string Phone { get; set; }
        public bool IncludeAllEmployeesForExposure { get; set; }
        public IList<IClassCode> ClassCodes { get; set; }
        public IList<IPayroll> Payroll { get; set; }
    }
}