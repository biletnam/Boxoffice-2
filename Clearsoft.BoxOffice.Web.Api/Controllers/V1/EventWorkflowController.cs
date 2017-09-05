using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing;
using Clearsoft.BoxOffice.Web.Api.Models;
using Clearsoft.BoxOffice.Web.Common;
using Clearsoft.BoxOffice.Web.Common.Routing;
using Clearsoft.BoxOffice.Web.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class EventWorkflowController : ApiController
    {
        private readonly IPublishEventWorkflowProcessor _publishEventWorkflowProcessor;
        private readonly IUnpublishEventWorkflowProcessor _unpublishEventWorkflowProcessor;

        public EventWorkflowController(IPublishEventWorkflowProcessor publishEventWorkflowProcessor, IUnpublishEventWorkflowProcessor unpublishEventWorkflowProcessor)
        {
            _publishEventWorkflowProcessor = publishEventWorkflowProcessor;
            _unpublishEventWorkflowProcessor = unpublishEventWorkflowProcessor;
        }

        [HttpPost]
        [Route("events/{eventId:long}/publish", Name ="PublishEventRoute")]
        public Event PublishEvent(long eventId)
        {
            var @event = _publishEventWorkflowProcessor.PublishEvent(eventId);

            return @event;
        }

        [HttpPost]
        [UserAudit]
        [Route("events/{eventId:long}/unpublish", Name ="UnpublishEventRoute")]
        public Event UnpublishEvent(long eventId)
        {
            var @event = _unpublishEventWorkflowProcessor.UnpublishEvent(eventId);

            return @event;
        }
    }
}
