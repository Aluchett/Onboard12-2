using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Onboard12.Server.ViewModels;
using Onboard12.Server.Models;
using Onboard12.Server.Services;



namespace Store_Onboard12.Server.Services;



public class ProductService : IProductService 
{
    private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        return _mapper.Map<IEnumerable<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel> GetProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            throw new Exception("Product not found!");
        }

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<ProductViewModel> CreateProduct(CreateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        product.Updated = DateTime.Now;
        product.Created = DateTime.Now;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();


        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<ProductViewModel> UpdateProduct(int id, CreateProductRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            throw new Exception("Product not found!");
        }

        product = _mapper.Map(request, product);
        product.Updated = DateTime.Now;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            throw new KeyNotFoundException("Product not found!");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}