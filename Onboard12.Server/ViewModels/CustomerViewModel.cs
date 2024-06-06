using System.ComponentModel.DataAnnotations;

namespace Onboard12.Server.ViewModels; 

public class CustomerViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

}
