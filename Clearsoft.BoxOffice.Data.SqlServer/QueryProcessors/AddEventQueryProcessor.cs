using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.QueryProcessors;
using Clearsoft.BoxOffice.Web.Common.Security;
using NHibernate;
using System;

namespace Clearsoft.BoxOffice.Data.SqlServer.QueryProcessors
{
    public class AddEventQueryProcessor : IAddEventQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddEventQueryProcessor(IDateTime dateTime, ISession session, IUserSession userSession)
        {
            _dateTime = dateTime;
            _session = session;
            _userSession = userSession;
        }

        public void AddEvent(Event @event)
        {
            @event.CreatedDate = _dateTime.UtcNow;
            @event.Status = _session.QueryOver<Status>().Where(x => x.Name == "Not Published").SingleOrDefault();
            @event.CreatedBy = _session.Get<User>(1L);//Temp Hack to not error
            //@event.CreatedBy = _session.QueryOver<User>().Where(x => x.Username == _userSession.UserName).SingleOrDefault();

            _session.SaveOrUpdate(@event);
        }
    }
}
