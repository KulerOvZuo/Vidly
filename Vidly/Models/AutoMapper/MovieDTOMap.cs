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
        public void InitMovieDTO()
        {
            this.CreateMap<Movie, MovieDTO>();
            this.CreateMap<MovieDTO, Movie>();
        }
    }
}