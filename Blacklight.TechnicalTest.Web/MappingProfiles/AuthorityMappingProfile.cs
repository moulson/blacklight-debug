using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FHR.Domain.Models;
using FHR.Web.Models;

namespace FHR.Web.MappingProfiles
{
    public class AuthorityMappingProfile : Profile
    {
        public AuthorityMappingProfile()
        {
            CreateMap<LocalAuthority, LocalAuthorityViewModel>();
            CreateMap<IEnumerable<LocalAuthority>, AuthorityRatingsViewModel>()
                .ForMember(dest => dest.Authorities, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.AuthoritiesLoadedSuccessfuly, opt =>
                {
                    opt.Condition(src => src != null && src.Count() > 1);
                    opt.UseValue(true);
                });
        }
    }
}