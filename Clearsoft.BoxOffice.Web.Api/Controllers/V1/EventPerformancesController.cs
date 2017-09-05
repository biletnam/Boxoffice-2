using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing;
using Clearsoft.BoxOffice.Web.Api.Models;
using Clearsoft.BoxOffice.Web.Common;
using Clearsoft.BoxOffice.Web.Common.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("events")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class EventPerformancesController : ApiController
    {
        private readonly IEventPerformancesMaintenanceProcessor _eventPerformancesMaintenanceProcessor;

        public EventPerformancesController(IEventPerformancesMaintenanceProcessor eventPerformancesMaintenanceProcessor)
        {
            _eventPerformancesMaintenanceProcessor = eventPerformancesMaintenanceProcessor;
        }

        [Route("{eventId:long}/performances", Name = "ReplaceEventPerformancesRoute")]
        [HttpPut]
        public Event ReplaceEventPerformances(long eventId, [FromBody] IEnumerable<long> performanceIds)
        {
            var @event = _eventPerformancesMaintenanceProcessor.ReplaceEventPerformances(eventId, performanceIds);
            return @event;
        }

        [Route("{eventId:long}/performances", Name = "DeleteEventPerformancesRoute")]
        [HttpDelete]
        public Event DeleteEventPerformances(long eventId)
        {
            var @event = _eventPerformancesMaintenanceProcessor.DeleteEventPerformances(eventId);
            return @event;
        }

        [Route("{eventId:long}/performances/{performanceId:long}", Name = "AddEventPerformanceRoute")]
        [HttpPut]
        public Event AddEventPerformance(long eventId, long performanceId)
        {
            var @event = _eventPerformancesMaintenanceProcessor.AddPerformanceToEvent(eventId, performanceId);
            return @event;
        }

        [Route("{eventId:long}/performances/{performanceId:long}", Name = "DeleteEventPerformanceRoute")]
        [HttpDelete]
        public Event ReplaceEventPerformances(long eventId, long performanceId)
        {
            var @event = _eventPerformancesMaintenanceProcessor.DeleteEventPerformance(eventId, performanceId);
            return @event;
        }
    }
}