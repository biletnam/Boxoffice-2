using AutoMapper;
using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class PerformanceToPerformanceEntityProfile : Profile
    {
        public PerformanceToPerformanceEntityProfile() 
        {
            CreateMap<Performance, Data.Entities.Performance>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.CreatedBy, x => x.Ignore())
                .ForMember(opt => opt.PerformanceId, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore());
        }
    }
}