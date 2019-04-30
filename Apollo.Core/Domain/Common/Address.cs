using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Common
{
   public class Address : ModelBase, IAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}