
using Onboard12.Server.ViewModels;



namespace Onboard12.Server.Services;


public interface ISalesService

{  
    Task<IEnumerable<SalesViewModel>> GetSales();
    Task<SalesViewModel> GetSale(int id);
    Task<SalesViewModel> CreateSale(CreateSalesRequest request);
    Task<SalesViewModel> UpdateSale(CreateSalesRequest request);
    Task DeleteSale(int id); 
}
