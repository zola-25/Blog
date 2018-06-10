using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.Post, ViewModels.Post>();
            CreateMap<ViewModels.NewPost, Data.Models.Post>();
        }

    }
}
