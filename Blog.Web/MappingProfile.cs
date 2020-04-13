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
            CreateMap<Data.Models.Post, ViewModels.Post>().ForMember(
                    dest => dest.PostId, 
                    opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<ViewModels.EditablePost, Data.Models.Post>().ForMember(
                    dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PostId)).ReverseMap();
        }

    }
}
