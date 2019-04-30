using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Common
{
    public class Contact : ModelBase, IContact
    {
        public Contact()
        {
            
        }
        public Contact(int entityId)
        {
            EntityId = entityId;
        }
        public int EntityId { get; }
        //public IAddress Address { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Email { get; set; }
        public string FaxNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}