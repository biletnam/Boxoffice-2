using System.Collections.Generic;
using AutoMapper;
using Event = Clearsoft.BoxOffice.Web.Api.Models.Event;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class EventPerformancesMaintenanceProcessor : IEventPerformancesMaintenanceProcessor
    {
        private readonly IMapper _mapper;
        private readonly IUpdateEventQueryProcessor _updateEventQueryProcessor;

        public EventPerformancesMaintenanceProcessor(IMapper mapper, IUpdateEventQueryProcessor updateEventQueryProcessor)
        {
            _mapper = mapper;
            _updateEventQueryProcessor = updateEventQueryProcessor;
        }

        public Event AddPerformanceToEvent(long eventId, long performanceId)
        {
            var eventEntity = _updateEventQueryProcessor.AddEventPerformance(eventId, performanceId);
            return CreateEventResponse(eventEntity);
        }

        public Event DeleteEventPerformance(long eventId, long performanceId)
        {
            var eventEntity = _updateEventQueryProcessor.DeleteEventPerformance(eventId, performanceId);
            return CreateEventResponse(eventEntity);
        }

        public Event DeleteEventPerformances(long eventId)
        {
            var eventEntity = _updateEventQueryProcessor.DeleteEventPerformances(eventId);
            return CreateEventResponse(eventEntity);
        }

        public Event ReplaceEventPerformances(long eventId, IEnumerable<long> performanceIds)
        {
            var eventEntity = _updateEventQueryProcessor.ReplaceEventPerformances(eventId, performanceIds);

            return CreateEventResponse(eventEntity);
        }

        public virtual Event CreateEventResponse(Data.Entities.Event eventEntity)
        {
            var @entity = _mapper.Map<Event>(eventEntity);
            return @entity;
        }
    }
}