using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Common
{
    public class DateTimeAdapter : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
