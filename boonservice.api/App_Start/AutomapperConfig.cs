using System.Web;
using AutoMapper;
using boonservice.api;
using boonservice.api.Models;

[assembly: PreApplicationStartMethod(typeof(AutoMapperConfig), "Configure")]

namespace boonservice.api
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new SuperheroAutoMapperProfile()));
            Mapper.Initialize(cfg => cfg.AddProfile(new UserAutoMapperProfile()));
        }
    }
}