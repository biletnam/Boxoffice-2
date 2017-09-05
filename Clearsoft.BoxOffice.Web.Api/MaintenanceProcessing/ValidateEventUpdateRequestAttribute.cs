using Clearsoft.BoxOffice.Common.Logging;
using Clearsoft.BoxOffice.Web.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using Clearsoft.BoxOffice.Web.Api.Models;
using Newtonsoft.Json;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class ValidateEventUpdateRequestAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;

        public ValidateEventUpdateRequestAttribute(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(ValidateEventUpdateRequestAttribute));
        }

        public ValidateEventUpdateRequestAttribute() : this(WebContainerManager.Get<ILogManager>())
        {

        }

        public override bool AllowMultiple => false;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var eventId = (long)actionContext.ActionArguments[ActionParameterNames.EventId];
            var eventFragment = (JObject)actionContext.ActionArguments[ActionParameterNames.EventFragment];

            _log.DebugFormat("{0} = {1}", ActionParameterNames.EventFragment, eventFragment);

            if(eventFragment == null)
            {
                const string errorMessage = "Malformed or null request.";

                _log.Debug(errorMessage);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                return;
            }

            try
            {
                var @event = eventFragment.ToObject<Event>();
                if(@event.EventId.HasValue && @event.EventId != eventId)
                {
                    const string errorMessage = "Event ids do not match.";
                    _log.Debug(errorMessage);
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                    return;
                }
            }
            catch(JsonException ex)
            {
                _log.Debug(ex.Message);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        public static class ActionParameterNames
        {
            public const string EventFragment = "updatedEvent";
            public const string EventId = "id";
        }
    }
}