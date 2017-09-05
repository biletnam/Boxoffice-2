using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Data.Entities;
namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class EventEntityToEventProfile : Profile
    {
        public EventEntityToEventProfile()
        {
            CreateMap<Event, Models.Event>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Performances, x => x.ResolveUsing<EventPerformancesResolver>());
        }
    }
}