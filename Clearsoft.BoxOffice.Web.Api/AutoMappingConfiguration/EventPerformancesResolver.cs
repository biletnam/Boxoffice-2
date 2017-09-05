using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Performance = Clearsoft.BoxOffice.Web.Api.Models.Performance;
namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class EventPerformancesResolver : IValueResolver<Event,object,List<Performance>>
    {
        public List<Performance> Resolve(Event source, object destination, List<Performance> destMember, ResolutionContext context)
        {
            return source.Performances.Select(x => context.Mapper.Map<Performance>(x)).ToList();
        }
    }
}