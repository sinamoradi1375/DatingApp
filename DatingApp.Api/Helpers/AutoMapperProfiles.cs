using System.Linq;
using AutoMapper;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;

namespace DatingApp.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(dest => dest.Age,opt => {
                    opt.ResolveUsing(x => x.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(dest => dest.Age,opt => {
                    opt.ResolveUsing(x => x.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotosForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => {
                    opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(m => m.RecipientPhotoUrl, opt => {
                    opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url);
                });
        }
    }
}