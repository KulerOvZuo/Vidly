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
        public MappingProfile()
        {
            Type thisType = this.GetType();
            var methods = thisType.GetMethods().Where(m => m.Name.Contains("Init"));

            foreach(var method in methods)
                method.Invoke(this, null);
        }
    }
}