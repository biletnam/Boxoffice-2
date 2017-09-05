using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.InquiryProcessing
{
    public interface IEventByIdInquiryProcessor
    {
        Event GetEvent(long eventId);
    }
}
