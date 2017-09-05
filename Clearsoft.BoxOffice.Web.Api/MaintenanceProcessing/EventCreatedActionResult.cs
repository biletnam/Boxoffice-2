using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class EventCreatedActionResult : IHttpActionResult
    {
        private readonly Event _createdEvent;
        private readonly HttpRequestMessage _requestMessage;

        public EventCreatedActionResult(HttpRequestMessage requestMessage, Event createdEvent) 
        {
            _createdEvent = createdEvent;
            _requestMessage = requestMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = _requestMessage.CreateResponse(System.Net.HttpStatusCode.Created,
                _createdEvent);

            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_createdEvent);

            return responseMessage;
        }
    }
}