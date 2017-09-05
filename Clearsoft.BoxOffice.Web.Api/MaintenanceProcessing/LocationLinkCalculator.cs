using Clearsoft.BoxOffice.Common;
using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public static class LocationLinkCalculator
    {
        public static Uri GetLocationLink(ILinkContaininng linkContaining)
        {
            var locationLink = linkContaining.Links.FirstOrDefault(x => x.Rel == Constants.CommonLinkRelValues.Self);
            return locationLink == null ? null : new Uri(locationLink.Href);
        }
    }
}