using AutoMapper;
using Clearsoft.BoxOffice.Web.Api.Models;
using Event = Clearsoft.BoxOffice.Data.Entities.Event;
namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class NewEventToEventEntityProfile : Profile
    {
        public NewEventToEventEntityProfile()
        {
            CreateMap<NewEvent, Event>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.CreatedBy, x => x.Ignore())
                .ForMember(opt => opt.EventId, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.Performances, x => x.Ignore());
        }
    }
}
