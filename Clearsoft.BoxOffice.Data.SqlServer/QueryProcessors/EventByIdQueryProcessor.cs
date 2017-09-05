using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.QueryProcessors;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.SqlServer.QueryProcessors
{
    public class EventByIdQueryProcessor : IEventByIdQueryProcessor
    {
        private readonly ISession _session;

        public EventByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Event GetEvent(long eventId)
        {
            var @event = _session.Get<Event>(eventId);
            return @event;
        }
    }
}
