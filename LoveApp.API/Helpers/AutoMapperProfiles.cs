using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToListDto>()
            .ForMember(dest => dest.PhotoUrl, opt =>{
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
             .ForMember(dest => dest.Age, opt =>{
                opt.ResolveUsing(d => d.DateofBirth.CalculateAge());
            });
             CreateMap<User, UserToDetailsDto>()         
            .ForMember(dest => dest.PhotoUrl, opt =>{
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })          
             .ForMember(dest => dest.Age, opt =>{
                opt.ResolveUsing(d => d.DateofBirth.CalculateAge());
            });
             CreateMap<Photo, PhotosToDetailsDto>();
        }
    }
}