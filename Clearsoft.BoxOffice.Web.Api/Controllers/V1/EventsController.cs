using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Web.Api.InquiryProcessing;
using Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing;
using Clearsoft.BoxOffice.Web.Api.Models;
using Clearsoft.BoxOffice.Web.Common;
using Clearsoft.BoxOffice.Web.Common.Routing;
using Clearsoft.BoxOffice.Web.Common.Validation;
using System.Net.Http;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("events")]
    [Authorize(Roles = Constants.RoleNames.JuniorWorker)]
    public class EventsController : ApiController
    {

        private readonly IAddEventMaintenanceProcessor _addEventMaintenanceProcessor;
        private readonly IEventByIdInquiryProcessor _eventByIdInquiryProcessor;
        private readonly IUpdateEventMaintenanceProcessor _updateEventMaintenanceProcessor;
        public EventsController(IAddEventMaintenanceProcessor addEventMaintenanceProcessor, IEventByIdInquiryProcessor eventByIdInquiryProcessor, IUpdateEventMaintenanceProcessor updateEventMaintenanceProcessor)
        {
            _addEventMaintenanceProcessor = addEventMaintenanceProcessor;
            _eventByIdInquiryProcessor = eventByIdInquiryProcessor;
            _updateEventMaintenanceProcessor = updateEventMaintenanceProcessor;
        }

        [Route("",Name ="AddEventRoute")]
        [UnitOfWorkActionFilter]
        [Authorize(Roles =Constants.RoleNames.Manager)]
        [ValidateModel]
        [HttpPost]
        public IHttpActionResult AddEvent(HttpRequestMessage requestMessage, NewEvent newEvent)
        {
            var @event = _addEventMaintenanceProcessor.AddEvent(newEvent);

            var result = new EventCreatedActionResult(requestMessage, @event);
            return result;
        }

        [Route("{id:long}", Name ="GetEventRoute")]
        public Event GetEvent(long id)
        {
            var @event = _eventByIdInquiryProcessor.GetEvent(id);

            return @event;
        }

        [Route("{id:long}", Name = "UpdateEventRoute")]
        [HttpPut]
        [HttpPatch]
        [ValidateEventUpdateRequest]
        [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
        public Event UpdateEvent(long id, [FromBody] object updatedEvent)
        {
            var @event = _updateEventMaintenanceProcessor.UpdateEvent(id, updatedEvent);

            return @event;
        }
    }
}
