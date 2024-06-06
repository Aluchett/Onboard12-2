using AutoMapper;
using Onboard12.Server.ViewModels;
using Onboard12.Server.Models;
using StoreReact.ViewModels;




namespace Onboard12.Server.Mapping;




    public class MappingProfile : Profile
{ 
    public MappingProfile() 
{ 

    CreateMap<Customer,CustomerViewModel>(); 
    CreateMap<CustomerViewModel, Customer>();
    CreateMap<CreateCustomerRequest, Customer>();
    CreateMap<Customer, CreateCustomerRequest>();

    CreateMap<Product, ProductViewModel>();
    CreateMap<ProductViewModel, Product>();
    CreateMap<CreateProductRequest, Product>();
    CreateMap<Product,CreateProductRequest>();

    CreateMap<Store, StoreViewModel>();
    CreateMap<StoreViewModel, Store>();
    CreateMap<CreateStoreRequest, Store>();
    CreateMap<Store, CreateStoreRequest>();

    CreateMap<Sales, SalesViewModel>();
    CreateMap<SalesViewModel, Sales>();
    CreateMap<CreateSalesRequest, Sales>();
    CreateMap<Sales,CreateSalesRequest>();
    
    }
}
