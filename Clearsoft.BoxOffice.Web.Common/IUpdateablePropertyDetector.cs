﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Common
{
    public interface IUpdateablePropertyDetector
    {
        IEnumerable<string> GetNamesOfPropertiesToUpdate<TTargetType>(object objectContainingUpdatedData);
    }
}
