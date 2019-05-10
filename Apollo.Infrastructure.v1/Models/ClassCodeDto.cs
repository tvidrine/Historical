// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 03/13/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Domain.ClassCode;

namespace Apollo.Infrastructure.v1.Models
{
    public class ClassCodeDto
    {
        public int LocationID { get; set; }
        public string ClassCodeState { get; set; }
        public string ClassCode { get; set; }
        public string ClassCodeDesc { get; set; }
        public int ClassCodeId { get; set; }

        public IClassCode ToModel()
        {
            return new ClassCode
            {
                Id = ClassCodeId,
                State = ClassCodeState,
                Description = ClassCodeDesc,
                Code = ClassCode
            };
        }
    }
}