using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.Entities
{
    public class User: IVersionedEntity
    {
        public virtual long UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
        public virtual byte[] Version { get; set; }
    }
}
