using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.Entities
{
    public class Event : IVersionedEntity
    {
        private readonly IList<Performance> _performances = new List<Performance>();

        public virtual long EventId { get; set; }
        public virtual string Name { get; set; }

        public virtual Status Status { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual IList<Performance> Performances
        {
            get
            {
                return _performances;
            }
        }

        public virtual byte[] Version { get; set; }
    }
}
