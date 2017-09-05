using AutoMapper;
using Clearsoft.BoxOffice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.AutoMappingConfiguration
{
    public class StatusEntityToStatusProfile : Profile
    {
        public StatusEntityToStatusProfile()
        {
            CreateMap<Status, Models.Status>();
        }
    }
}