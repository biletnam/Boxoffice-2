using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public interface IEventPerformancesMaintenanceProcessor
    {
        Event ReplaceEventPerformances(long eventId, IEnumerable<long> performanceIds);
        Event DeleteEventPerformances(long eventId);
        Event AddPerformanceToEvent(long eventId, long performanceId);
        Event DeleteEventPerformance(long eventId, long performanceId);
    }
}