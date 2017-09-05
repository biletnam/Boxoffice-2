using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public interface IUpdateEventMaintenanceProcessor
    {
        Event UpdateEvent(long eventId, object eventFragment);
    }
}
