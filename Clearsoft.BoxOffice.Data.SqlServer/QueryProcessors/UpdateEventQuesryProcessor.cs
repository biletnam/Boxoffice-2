using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.Exceptions;
using Clearsoft.BoxOffice.Data.QueryProcessors;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace Clearsoft.BoxOffice.Data.SqlServer.QueryProcessors
{
    public class UpdateEventQuesryProcessor : IUpdateEventQueryProcessor
    {
        private readonly ISession _session;

        public UpdateEventQuesryProcessor(ISession session)
        {
            _session = session;
        }

        public Event AddEventPerformance(long eventId, long performanceId)
        {
            var @event = GetValidEvent(eventId);

            UpdateEventPerformances(@event, new[] { performanceId }, true);

            _session.SaveOrUpdate(@event);

            return @event;
        }

        public Event DeleteEventPerformance(long eventId, long perfomrnaceId)
        {
            var @event = GetValidEvent(eventId);
            var performance = @event.Performances.FirstOrDefault(x => x.PerformanceId == perfomrnaceId);

            if(performance != null)
            {
                @event.Performances.Remove(performance);
                _session.SaveOrUpdate(@event);
            }

            return @event;
        }

        public Event DeleteEventPerformances(long eventId)
        {
            var @event = GetValidEvent(eventId);

            UpdateEventPerformances(@event, null, false);

            _session.SaveOrUpdate(@event);

            return @event;
        }

        public Event ReplaceEventPerformances(long eventId, IEnumerable<long> performanceIds)
        {
            var @event = GetValidEvent(eventId);

            UpdateEventPerformances(@event, performanceIds, false);

            _session.SaveOrUpdate(@event);

            return @event;
        }

        public virtual Event GetValidEvent(long eventId)
        {
            var @event = _session.Get<Event>(eventId);

            if(@event == null)
            {
                throw new RootObjectNotFoundException("Event not found");
            }

            return @event;
        }

        public virtual Performance GetValidPerformance(long performanceId)
        {
            var performance = _session.Get<Performance>(performanceId);

            if(performance == null)
            {
                throw new ChildObjectNotFoundException("Performance not found");
            }

            return performance;
        }

        public virtual void UpdateEventPerformances(Event @event, IEnumerable<long> performanceIds, bool appendToExisting)
        {
            if (!appendToExisting)
            {
                @event.Performances.Clear();
            }

            if(performanceIds != null)
            {
                foreach(var performance in performanceIds.Select(GetValidPerformance))
                {
                    if (!@event.Performances.Contains(performance))
                    {
                        @event.Performances.Add(performance);
                    }
                }
            }
        }

        public Event GetUpdatedEvent(long eventId, PropertyValueMapType updatedPropertyValueMap)
        {
            var @event = GetValidEvent(eventId);

            var propertyInfos = typeof(Event).GetProperties();
            foreach(var propertyValuePair in updatedPropertyValueMap)
            {
                propertyInfos.Single(x => x.Name == propertyValuePair.Key).SetValue(@event, propertyValuePair.Value);
            }

            _session.SaveOrUpdate(@event);

            return @event;
        }
    }
}
