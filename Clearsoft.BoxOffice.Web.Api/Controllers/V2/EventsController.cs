using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Api.Controllers.V2
{
    [RoutePrefix("api/{apiVersion:apiVersionConstraint(v2)}/events")]
    public class EventsController : ApiController
    {
        [Route("",Name ="AddEventRouteV2")]
        [HttpPost]
        public Event AddEvent(HttpRequestMessage requestMessage, Event newEvent)
        {
            return new Event { Name = "in v2, newEvent.Name = " + newEvent.Name };
        }
    }
}
