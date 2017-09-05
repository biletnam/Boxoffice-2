using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.Entities
{
    public class Performance: IVersionedEntity
    {
        public virtual long PerformanceId { get; set; }
        public virtual DateTime PerformanceDate { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual Status Status { get; set; }
        public virtual byte[] Version { get; set; }

    }
}
