namespace Apollo.Core.Contracts.Domain
{
    public interface IAddress : IHaveId, IHaveAuditData
    {
        string City { get; set; }
        string Line1 { get; set; }
        string Line2 { get; set; }
        string State { get; set; }
        string Zipcode { get; set; }
        
    }
}