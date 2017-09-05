using Clearsoft.BoxOffice.Common.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;

namespace Clearsoft.BoxOffice.Web.Common.Security
{
    public class UserAuditAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public UserAuditAttribute(ILogManager logManager, IUserSession userSession)
        {
            _log = logManager.GetLog(typeof(UserAuditAttribute));
            _userSession = userSession;
        }

        public UserAuditAttribute() : this(WebContainerManager.Get<ILogManager>(),WebContainerManager.Get<IUserSession>())
        {

        }

        public override bool AllowMultiple => false;

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _log.Debug("Starting execution...");
            var username = _userSession.UserName;
            return Task.Run(() => AuditCurrentUser(username), cancellationToken);
        }

        public void AuditCurrentUser(string username)
        {
            //simulate audit process
            _log.InfoFormat("Action being executed by user={0}", username);
            Thread.Sleep(3000);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _log.InfoFormat("Action executed by user={0}", _userSession.UserName);
        }
    }
}
