using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Web.Api.Models;
using Clearsoft.BoxOffice.Data.QueryProcessors;
using AutoMapper;
using Clearsoft.BoxOffice.Web.Common;
using Newtonsoft.Json.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class UpdateEventMaintenanceProcessor : IUpdateEventMaintenanceProcessor
    {
        private readonly IMapper _mapper;
        private readonly IUpdateEventQueryProcessor _updateEventQueryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateEventMaintenanceProcessor(IMapper mapper, IUpdateEventQueryProcessor updateEventQueryProcessor, IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _mapper = mapper;
            _updateEventQueryProcessor = updateEventQueryProcessor;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        public Event UpdateEvent(long eventId, object eventFragment)
        {
            var eventFragmentAsJObject = (JObject)eventFragment;
            var eventContainingUpdateData = eventFragmentAsJObject.ToObject<Event>();
            var updatedPropertyValueMap = GetPropertyValueMap(eventFragmentAsJObject, eventContainingUpdateData);
            var updatedEventEntity = _updateEventQueryProcessor.GetUpdatedEvent(eventId, updatedPropertyValueMap);
            var @event = _mapper.Map<Event>(updatedEventEntity);

            return @event;
        }

        public virtual PropertyValueMapType GetPropertyValueMap(JObject eventFragment, Event eventContainingUpdateData)
        {
            var namesOfModifiedProperties = _updateablePropertyDetector.GetNamesOfPropertiesToUpdate<Event>(eventFragment).ToList();
            var propertyInfos = typeof(Event).GetProperties();
            var updatedPropertyValeMap = new PropertyValueMapType();

            foreach(var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(eventContainingUpdateData);

                updatedPropertyValeMap.Add(propertyName, propertyValue);
            }

            return updatedPropertyValeMap;
        }
    }
}