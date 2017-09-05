using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.Models
{
    public interface ILinkContaininng
    {
        List<Link> Links { get; set; }

        void AddLink(Link link);
    }
}
