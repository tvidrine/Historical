using Apollo.Core.Domain.Common;

namespace Apollo.Core.Contracts.Domain
{
    public interface IContact : IHaveAuditData, IHaveId
    {
        int EntityId { get; }
        //IAddress Address { get; set; }
        ContactTypeEnum ContactType { get; set; }
        string Email { get; set; }
        string FaxNumber { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }
    }
}