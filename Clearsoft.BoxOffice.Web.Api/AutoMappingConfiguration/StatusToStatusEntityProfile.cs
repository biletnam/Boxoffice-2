using AutoMapper;
using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class StatusToStatusEntityProfile : Profile
    {
        public StatusToStatusEntityProfile()
        {
            CreateMap<Status, Data.Entities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}