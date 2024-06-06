using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Onboard12.Server.Models;

public class Product
{

    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public ICollection<Sales>? Sales { get; set; }
}
