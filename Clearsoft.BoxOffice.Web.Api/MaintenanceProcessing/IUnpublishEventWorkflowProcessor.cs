﻿using Clearsoft.BoxOffice.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing
{
    public interface IUnpublishEventWorkflowProcessor
    {
        Event UnpublishEvent(long eventId);
    }
}