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
    public class UpdateEventStatusQueryProcessor : IUpdateEventStatusQueryProcessor
    {
        private readonly ISession _session;

        public UpdateEventStatusQueryProcessor(ISession session)
        {
            _session = session;
        }
        public void UpdateEventStatus(Event eventToUpdate, string statusName)
        {
            var status = _session.QueryOver<Status>().Where(x => x.Name == statusName).SingleOrDefault();

            eventToUpdate.Status = status;
            _session.SaveOrUpdate(eventToUpdate);
        }
    }
}
