using AutoMapper;

namespace boonservice.api.Models
{
    public class UserAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<B3G_USERS, UserAuthor>();
        }
    }
}