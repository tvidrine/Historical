using System;
using Apollo.Core.Contracts.Domain;

namespace Apollo.Core.Domain.Core
{
    public abstract class ModelBase : IHaveId, IHaveAuditData
    {
        protected ModelBase()
        {
            CreatedOn   = DateTimeOffset.Now;
            CreatedById = 1;
            LastModifiedOn = DateTimeOffset.Now;
            LastModifiedById = 1;
        }
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int LastModifiedById { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }

    }
}