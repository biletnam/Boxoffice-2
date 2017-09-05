using Clearsoft.BoxOffice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace Clearsoft.BoxOffice.Data.QueryProcessors
{
    public interface IUpdateEventQueryProcessor
    {
        Event ReplaceEventPerformances(long eventId, IEnumerable<long> performanceIds);
        Event DeleteEventPerformances(long eventId);
        Event AddEventPerformance(long eventId, long performanceId);
        Event DeleteEventPerformance(long eventId, long perfomrnaceId);
        Event GetUpdatedEvent(long eventId, PropertyValueMapType updatedPropertyValueMap);

    }
}
