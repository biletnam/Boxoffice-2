using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clearsoft.BoxOffice.Web.Api.Models;
using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.Exceptions;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.InquiryProcessing
{
    public class EventByIdInquiryProcessor : IEventByIdInquiryProcessor
    {
        private readonly IMapper _mapper;
        private readonly IEventByIdQueryProcessor _eventByIdQueryProcessor;

        public EventByIdInquiryProcessor(IMapper mapper, IEventByIdQueryProcessor eventByIdQueryProcessor)
        {
            _mapper = mapper;
            _eventByIdQueryProcessor = eventByIdQueryProcessor;
        }

        public Models.Event GetEvent(long eventId)
        {
            var eventEntity = _eventByIdQueryProcessor.GetEvent(eventId);

            if (eventEntity == null)
            {
                throw new RootObjectNotFoundException("Event not found");
            }

            var @event = _mapper.Map<Models.Event>(eventEntity);

            return @event;
        }
    }
}