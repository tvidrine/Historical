// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/01/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;

namespace Apollo.Infrastructure.Models
{
    public abstract class DtoBase<TModel, TInfo> : IDto, IHaveAuditData
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int LastModifiedById { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }

        public virtual IDto FromModel(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual TModel ToModel()
        {
            throw new NotImplementedException();
        }

        public virtual TInfo ToInfo()
        {
            throw new NotImplementedException();
        }
    }
}