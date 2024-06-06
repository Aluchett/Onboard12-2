namespace Onboard12.Server.Models; 

public class Sales
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public required DateTime DateSold { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Product? Product { get; set; }
    public Customer? Customer { get; set; }
    public Store? Store { get; set; }
}
