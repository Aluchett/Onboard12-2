using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreReact.Models;
using StoreReact.ViewModels;



namespace StoreReact.Services;

public class CustomerService : ICustomerService 

{
     private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
    {
        var customers = await _context.Customers.ToListAsync();

        return _mapper.Map<IEnumerable<CustomerViewModel>>(customers);
    }

    public async Task<CustomerViewModel> GetCustomer(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);

        if (customer == null)
        {
            throw new Exception("Customer not found!");
        }

        return _mapper.Map<CustomerViewModel>(customer);
    }

    public async Task<CustomerViewModel> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = _mapper.Map<Customer>(request);

       // customer.Updated = DateTime.Now;
        //customer.Created = DateTime.Now;//

        //context.Customers.Add(customer);//
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerViewModel>(customer);
    }

    public async Task<CustomerViewModel> UpdateCustomer(int id, CreateCustomerRequest request)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);

        if (customer == null)
        {
            throw new Exception("Customer not found!");
        }

        customer = _mapper.Map(request, customer);
        //customer.Updated = DateTime.Now;//

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerViewModel>(customer);
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);

        if (customer == null)
        {
            throw new Exception("Customer not found!");
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

    }

}
