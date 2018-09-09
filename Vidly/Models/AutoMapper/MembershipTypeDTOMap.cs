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
        public void InitMembershipTypeDTOMap()
        {
            this.CreateMap<MembershipType, MembershipTypeDTO>();
        }
    }
}