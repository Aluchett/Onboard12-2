using AutoMapper;
using StoreReact.Models;
using StoreReact.ViewModels;
using System.Runtime.InteropServices;


namespace StoreReact.Mapping;




    public class MappingProfile : Profile
{ 
    public MappingProfile() 
{ 

    CreateMap<Customer,CustomerViewModel>(); 
    CreateMap<CustomerViewModel, Customer>();
    CreateMap<CreateCustomerRequest, Customer>();
    CreateMap<Customer,CreateCustomerRequest>();
    }
}
