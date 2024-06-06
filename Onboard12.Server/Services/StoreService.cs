using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Onboard12.Server.ViewModels;
using Onboard12.Server.Models;


namespace Onboard12.Server.Services;

public class StoreService : IStoreService
{
    private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public StoreService(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StoreViewModel>> GetStores()
    {
        var stores = await _context.Stores.ToListAsync();

        return _mapper.Map<IEnumerable<StoreViewModel>>(stores);
    }

    public async Task<StoreViewModel> GetStore(int id)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(store => store.Id == id);

        if (store == null)
        {
            throw new Exception("Store not found!");
        }

        return _mapper.Map<StoreViewModel>(store);
    }

    public async Task<StoreViewModel> CreateStore(CreateStoreRequest request)
    {
        var store = _mapper.Map<Store>(request);
        store.Updated = DateTime.Now;
        store.Created = DateTime.Now;

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        return _mapper.Map<StoreViewModel>(store);
    }

    public async Task<StoreViewModel> UpdateStore(int id, CreateStoreRequest request)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(store => store.Id == id);

        if (store == null)
        {
            throw new Exception("Store not found!");
        }

        store = _mapper.Map(request, store);
        store.Updated = DateTime.Now;

        await _context.SaveChangesAsync();

        return _mapper.Map<StoreViewModel>(store);
    }

    public async Task DeleteStore(int id)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(store => store.Id == id);

        if (store == null)
        {
            throw new KeyNotFoundException("Store not found!");
        }

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
    }
}