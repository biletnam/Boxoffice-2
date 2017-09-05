using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.Exceptions
{
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message)
        {

        }
    }
}
