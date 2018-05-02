using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.API;

namespace Vidly.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Customer, CustomerDTO>();
            this.CreateMap<CustomerDTO, Customer>();
        }
    }
}