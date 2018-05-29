using AutoMapper;

namespace boonservice.api.Models
{
    public class SuperheroAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PostSuperheroModel, Superhero>();
            Mapper.CreateMap<PutSuperheroModel, Superhero>();
        }
    }
}