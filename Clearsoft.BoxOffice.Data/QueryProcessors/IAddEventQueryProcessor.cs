using Clearsoft.BoxOffice.Data.Entities;

namespace Clearsoft.BoxOffice.Data.QueryProcessors
{
    public interface IAddEventQueryProcessor
    {
        void AddEvent(Event @event);
    }
}
