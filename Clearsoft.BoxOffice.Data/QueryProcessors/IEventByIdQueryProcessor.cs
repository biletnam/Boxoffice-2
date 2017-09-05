﻿using Clearsoft.BoxOffice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.QueryProcessors
{
    public interface IEventByIdQueryProcessor
    {
        Event GetEvent(long eventId);
    }
}
