using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Web.Api.Models;
using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Common;
using System.Net.Http;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class AddEventMaintenanceProcessor : IAddEventMaintenanceProcessor
    {
        private readonly IMapper _autoMapper;
        private readonly IAddEventQueryProcessor _queryProcessor;

        public AddEventMaintenanceProcessor(IMapper autoMapper, IAddEventQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
        }

        public Models.Event AddEvent(NewEvent newEvent)
        {
            var eventEntity =_autoMapper.Map<Data.Entities.Event>(newEvent);
            _queryProcessor.AddEvent(eventEntity);

            var @event = _autoMapper.Map<Models.Event>(eventEntity);

            //TODO: Implement link service
            @event.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost:123/api/v1/events/" + @event.EventId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return @event;
        }
    }
}