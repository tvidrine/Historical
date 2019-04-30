using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class PolicyAgent : ModelBase
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string PolicyNumber { get; set; }
        public int UserId { get; set; }
    }
}