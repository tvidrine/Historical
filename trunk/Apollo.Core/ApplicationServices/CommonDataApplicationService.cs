// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Domain.Common;

namespace Apollo.Core.ApplicationServices
{
    public class CommonDataApplicationService : ICommonDataApplicationService
    {
        public IList<StateInfo> GetStates(bool includeAll = false)
        { 
            var list = new List<StateInfo>
            {
                new StateInfo{ Id = 1, Name = "Alabama", Abbreviation="AL"},
                new StateInfo{ Id = 2, Name = "Alaska", Abbreviation="AK"},
                new StateInfo{ Id = 3, Name = "Arizona", Abbreviation="AZ"},
                new StateInfo{ Id = 4, Name = "Arkansas", Abbreviation="AR"},
                new StateInfo{ Id = 5, Name = "California", Abbreviation="CA"},
                new StateInfo{ Id = 6, Name = "Colorado", Abbreviation="CO"},
                new StateInfo{ Id = 7, Name = "Connecticut", Abbreviation="CT"},
                new StateInfo{ Id = 8, Name = "Delaware", Abbreviation="DE"},
                new StateInfo{ Id = 9, Name = "Delaware", Abbreviation="DE"},
                new StateInfo{ Id = 10, Name = "Florida", Abbreviation="FL"},
                new StateInfo{ Id = 11, Name = "Georgia", Abbreviation="GA"},
                new StateInfo{ Id = 12, Name = "Hawaii", Abbreviation="HI"},
                new StateInfo{ Id = 13, Name = "Idaho", Abbreviation="ID"},
                new StateInfo{ Id = 14, Name = "Illinois", Abbreviation="IL"},
                new StateInfo{ Id = 15, Name = "Indiana", Abbreviation="IN"},
                new StateInfo{ Id = 16, Name = "Iowa", Abbreviation="IA"},
                new StateInfo{ Id = 17, Name = "Kansas", Abbreviation="KS"},
                new StateInfo{ Id = 18, Name = "Kentucky", Abbreviation="KY"},
                new StateInfo{ Id = 19, Name = "Louisiana", Abbreviation="LA"},
                new StateInfo{ Id = 20, Name = "Maine", Abbreviation="ME"},
                new StateInfo{ Id = 21, Name = "Maryland", Abbreviation="MD"},
                new StateInfo{ Id = 22, Name = "Massachusetts", Abbreviation="MA"},
                new StateInfo{ Id = 23, Name = "Michigan", Abbreviation="MI"},
                new StateInfo{ Id = 24, Name = "Minnesota", Abbreviation="MN"},
                new StateInfo{ Id = 25, Name = "Mississippi", Abbreviation="MS"},
                new StateInfo{ Id = 26, Name = "Missouri", Abbreviation="MO"},
                new StateInfo{ Id = 27, Name = "Montana", Abbreviation="MT"},
                new StateInfo{ Id = 28, Name = "Nebraska", Abbreviation="NE"},
                new StateInfo{ Id = 29, Name = "Nevada", Abbreviation="NV"},
                new StateInfo{ Id = 30, Name = "NewHampshire", Abbreviation="NH"},
                new StateInfo{ Id = 31, Name = "NewJersey", Abbreviation="NJ"},
                new StateInfo{ Id = 32, Name = "NewMexico", Abbreviation="NM"},
                new StateInfo{ Id = 33, Name = "NewYork", Abbreviation="NY"},
                new StateInfo{ Id = 34, Name = "NorthCarolina", Abbreviation="NC"},
                new StateInfo{ Id = 35, Name = "NorthDakota", Abbreviation="ND"},
                new StateInfo{ Id = 36, Name = "Ohio", Abbreviation="OH"},
                new StateInfo{ Id = 37, Name = "Oklahoma", Abbreviation="OK"},
                new StateInfo{ Id = 38, Name = "Oregon", Abbreviation="OR"},
                new StateInfo{ Id = 39, Name = "Pennsylvania", Abbreviation="PA"},
                new StateInfo{ Id = 40, Name = "RhodeIsland", Abbreviation="RI"},
                new StateInfo{ Id = 41, Name = "SouthCarolina", Abbreviation="SC"},
                new StateInfo{ Id = 42, Name = "SouthDakota", Abbreviation="SD"},
                new StateInfo{ Id = 43, Name = "Tennessee", Abbreviation="TN"},
                new StateInfo{ Id = 44, Name = "Texas", Abbreviation="TX"},
                new StateInfo{ Id = 45, Name = "Utah", Abbreviation="UT"},
                new StateInfo{ Id = 46, Name = "Vermont", Abbreviation="VT"},
                new StateInfo{ Id = 47, Name = "Virginia", Abbreviation="VA"},
                new StateInfo{ Id = 48, Name = "Washington", Abbreviation="WA"},
                new StateInfo{ Id = 49, Name = "WestVirginia", Abbreviation="WV"},
                new StateInfo{ Id = 50, Name = "Wisconsin", Abbreviation="WI"},
                new StateInfo{ Id = 51, Name = "Wyoming", Abbreviation="WY"}
            };

            if(includeAll)
                list.Add(new StateInfo { Id = 0, Name = "All", Abbreviation = string.Empty });

            return list;
        }
    }
}