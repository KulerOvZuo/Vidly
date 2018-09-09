using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.API;

namespace Vidly.Models.AutoMapper
{
    public partial class MappingProfile : Profile
    {
        public void InitCustomerDTO()
        {
            this.CreateMap<Customer, CustomerDTO>()
                .ForMember(d => d.MoviesCount, s => s.MapFrom(src => src.Movies2Customers.Count));
         
            this.CreateMap<CustomerDTO, Customer>();
        }
    }
}