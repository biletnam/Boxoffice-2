using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clearsoft.BoxOffice.Web.Api.Security
{
    public interface IBasicSecurityService
    {
        bool SetPrincipal(string username, string password);
    }
}