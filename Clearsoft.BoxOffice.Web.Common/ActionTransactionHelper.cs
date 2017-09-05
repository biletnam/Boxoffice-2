using Clearsoft.BoxOffice.Common.Logging;
using log4net;
using NHibernate;
using NHibernate.Context;
using System.Web.Http.Filters;

namespace Clearsoft.BoxOffice.Web.Common
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ILog _log;
        public ActionTransactionHelper(ISessionFactory sessionFactory, ILogManager logManager)
        {
            _sessionFactory = sessionFactory;
            _log = logManager.GetLog(typeof(ActionTransactionHelper));
        }

        public bool TransactionHandled { get; private set; }
        public bool SessionClosed { get; private set; }

        public void BeginTransaction()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) return;
            var session = _sessionFactory.GetCurrentSession();
            if(session != null)
            {
                session.BeginTransaction();
            }
        }

        public void EndTransaction(HttpActionExecutedContext filterContext)
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) { _log.Error("The session was already closed"); return; }
            var session = _sessionFactory.GetCurrentSession();
            if (session == null) return;

            if (!session.Transaction.IsActive) return;

            if(filterContext.Exception == null)
            {
                session.Flush();
                session.Transaction.Commit();
            }
            else
            {
                session.Transaction.Rollback();
            }
            TransactionHandled = true;
        }

        public void CloseSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) return;
            var session = _sessionFactory.GetCurrentSession();
            session.Close();
            session.Dispose();

            CurrentSessionContext.Unbind(_sessionFactory);
            SessionClosed = true;
        }
    }
}
