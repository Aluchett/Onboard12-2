using Onboard12.Server.ViewModels;
using StoreReact.ViewModels;


namespace Onboard12.Server.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerViewModel>> GetCustomers();
    Task<CustomerViewModel> GetCustomer(int id);
    Task<CustomerViewModel> CreateCustomer(CreateCustomerRequest request);
    Task<CustomerViewModel> UpdateCustomer(int id, CreateCustomerRequest request);
    Task DeleteCustomer(int id);
}
