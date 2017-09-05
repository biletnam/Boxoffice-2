using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Common.Logging;
using Clearsoft.BoxOffice.Web.Api.Models;
using Clearsoft.BoxOffice.Web.Common.Security;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.Security
{
    public class EventDataSecurityMessageHandler: DelegatingHandler
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public EventDataSecurityMessageHandler(ILogManager logManager, IUserSession userSession)
        {
            _log = logManager.GetLog(typeof(EventDataSecurityMessageHandler));
            _userSession = userSession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (CanHandleResponse(response))
            {
                ApplySecurityToResponseData((ObjectContent)response.Content);
            }
            return response;
        }

        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;
            var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof(Event);
            return canHandleResponse;
        }

        public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var removeSensitiveData = !_userSession.IsInRole(Constants.RoleNames.SeniorWorker);

            if (removeSensitiveData)
            {
                _log.DebugFormat("Applyingsecurity data masking for user {0}", _userSession.UserName);
            }

            ((Event)responseObjectContent.Value).SetShouldSerializePerformances(!removeSensitiveData);
        }
    }
}