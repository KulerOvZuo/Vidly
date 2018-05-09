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
            this.CreateMap<Customer, CustomerDTO>();
            this.CreateMap<CustomerDTO, Customer>();
        }
    }
}