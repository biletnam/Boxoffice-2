using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Clearsoft.BoxOffice.Web.Common.ErrorHandling
{
    public class SimpleErrorResult : IHttpActionResult
    {
        private readonly string _errorMessage;
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;

        public SimpleErrorResult(HttpRequestMessage requestMessage, HttpStatusCode statusCode, string errorMessage)
        {
            _errorMessage = errorMessage;
            _requestMessage = requestMessage;
            _statusCode = statusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_requestMessage.CreateErrorResponse(_statusCode, _errorMessage));
        }
    }
}
