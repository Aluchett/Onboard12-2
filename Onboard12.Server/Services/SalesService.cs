using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Onboard12.Server.ViewModels;
using Onboard12.Server.Models;

namespace Onboard12.Server.Services; 

public class SalesService : ISalesService
{
    private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public SalesService(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SalesViewModel>> GetSales()
    {
        var sales = await _context.Sales
            .Include(sale => sale.Customer)
            .Include(sale => sale.Product)
            .Include(sale => sale.Store)
            .ToListAsync();


        return _mapper.Map<IEnumerable<SalesViewModel>>(sales);
    }

    public async Task<SalesViewModel> GetSale(int id)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(sale => sale.Id == id);

        if (sale == null)
        {
            throw new Exception("Sale not found!");
        }

        return _mapper.Map<SalesViewModel>(sale);
    }

    public async Task<SalesViewModel> CreateSale(CreateSalesRequest request)
    {
        var sale = _mapper.Map<Sales>(request);
        sale.Created = DateTime.Now;
        sale.Updated = DateTime.Now;

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return _mapper.Map<SalesViewModel>(sale);
    }

    public async Task<SalesViewModel> UpdateSale(CreateSalesRequest request)
    {
        var saleToUpdate = await _context.Sales.FirstOrDefaultAsync(sale => sale.Id == request.Id);

        if (saleToUpdate == null)
        {
            throw new Exception("Sale not found!");
        }

        saleToUpdate = _mapper.Map(request, saleToUpdate);
        saleToUpdate.Updated = DateTime.Now;

        _context.Sales.Update(saleToUpdate);
        await _context.SaveChangesAsync();

        return _mapper.Map<SalesViewModel>(saleToUpdate);
    }

    public async Task DeleteSale(int id)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(sale => sale.Id == id);

        if (sale == null)
        {
            throw new KeyNotFoundException("Sale not found!");
        }

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }


}
