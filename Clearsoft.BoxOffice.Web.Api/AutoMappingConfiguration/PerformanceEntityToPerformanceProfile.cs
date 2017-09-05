using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class PerformanceEntityToPerformanceProfile : Profile
    {
        public PerformanceEntityToPerformanceProfile()
        {
            CreateMap<Data.Entities.Performance, Models.Performance>()
                .ForMember(opt => opt.Links, x => x.Ignore());
        }
    }
}