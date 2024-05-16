namespace StoreReact.Models;

public class Sales
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public DateTime DateSold { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public required Product Product { get; set; }
    public required Customer Customer { get; set; }
    public required Store Store { get; set; }
}

