using Clearsoft.BoxOffice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.SqlServer.Mapping
{
    public class PerformanceMap : VersionedClassMap<Performance>
    {
        public PerformanceMap()
        {
            Id(x => x.PerformanceId);

            Map(x => x.PerformanceDate).Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
            References(x => x.CreatedBy, "CreatedUserId");
            References(x => x.Status, "StatusId");
        }
    }
}
