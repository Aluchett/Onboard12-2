using Onboard12.Server.ViewModels;




namespace Onboard12.Server.Services;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetProducts();

    Task<ProductViewModel> GetProduct(int id);

    Task<ProductViewModel> CreateProduct(CreateProductRequest request);

    Task<ProductViewModel> UpdateProduct(int id, CreateProductRequest request);

    Task DeleteProduct(int id);
}

