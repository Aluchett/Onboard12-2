using Onboard12.Server.ViewModels;


namespace Onboard12.Server.Services;

public interface IStoreService
{
    Task<IEnumerable<StoreViewModel>> GetStores();

    Task<StoreViewModel> GetStore(int id); 


    Task<StoreViewModel> CreateStore(CreateStoreRequest request);
    
    Task<StoreViewModel> UpdateStore(int id, CreateStoreRequest request);

    Task DeleteStore (int id); 


}
