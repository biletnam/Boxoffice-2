using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Web.Api.Models;
using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.Exceptions;
using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public class UnpublishEventWorkflowProcessor : IUnpublishEventWorkflowProcessor
    {
        private readonly IMapper _mapper;
        private readonly IEventByIdQueryProcessor _eventByIdQueryProcessor;
        private readonly IUpdateEventStatusQueryProcessor _updateEventStatusQueryProcessor;

        public UnpublishEventWorkflowProcessor(IMapper mapper, IEventByIdQueryProcessor eventByIdQueryProcessor, IUpdateEventStatusQueryProcessor updateEventStatusQueryProcessor)
        {
            _mapper = mapper;
            _eventByIdQueryProcessor = eventByIdQueryProcessor;
            _updateEventStatusQueryProcessor = updateEventStatusQueryProcessor;
        }

        public Models.Event UnpublishEvent(long eventId)
        {
            var eventEntity = _eventByIdQueryProcessor.GetEvent(eventId);
            
            if(eventEntity == null)
            {
                throw new RootObjectNotFoundException("Event not found");
            }

            //business logic goes here

            if(eventEntity.Status.Name != "Published")
            {
                throw new BusinessRuleViolationException("Incorrect task status. Expected status of 'Published'.");
            }

            _updateEventStatusQueryProcessor.UpdateEventStatus(eventEntity, "Not Published");

            var @event = _mapper.Map<Models.Event>(eventEntity);

            return @event;
        }
    }
}