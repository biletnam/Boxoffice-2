using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Web.Api.Models;
using FluentNHibernate.Automapping;
using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Data.Exceptions;
using Event = Clearsoft.BoxOffice.Web.Api.Models.Event;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class PublishEventWorkflowProcessor : IPublishEventWorkflowProcessor
    {
        private readonly IMapper _mapper;
        private readonly IEventByIdQueryProcessor _eventByIdQueryProcessor;

        private readonly IUpdateEventStatusQueryProcessor _updateEventStatusQueryProcessor;

        public PublishEventWorkflowProcessor(IMapper mapper, IEventByIdQueryProcessor eventByIdQueryProcessor, IUpdateEventStatusQueryProcessor updateEventStatusQueryProcessor)
        {
            _mapper = mapper;
            _eventByIdQueryProcessor = eventByIdQueryProcessor;
            _updateEventStatusQueryProcessor = updateEventStatusQueryProcessor;
        }

        public Event PublishEvent(long eventId)
        {
            var eventEntity = _eventByIdQueryProcessor.GetEvent(eventId);

            if(eventEntity == null)
            {
                throw new RootObjectNotFoundException("Event not found");
            }

            //business logic goes here

            if(eventEntity.Status.Name != "Not Published")
            {
                throw new BusinessRuleViolationException("Incorrect event status. Expected status of 'Not Published'.");
            }

            _updateEventStatusQueryProcessor.UpdateEventStatus(eventEntity, "Published");
            var @event = _mapper.Map<Event>(eventEntity);

            return @event;
        }
    }
}