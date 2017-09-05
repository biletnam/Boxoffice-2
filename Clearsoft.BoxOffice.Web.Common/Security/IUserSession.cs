using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Common.Security
{
    public interface IUserSession
    {
        string FirstName { get; }
        string LastName { get; }
        string UserName { get; }

        bool IsInRole(string roleName);
    }
}
