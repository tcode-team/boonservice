using AutoMapper;

namespace boonservice.api.Models
{
    public class RepairAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<RepairHeader, afs_repair_header>();
            Mapper.CreateMap<RepairItems, afs_repair_items>();
        }
    }
}