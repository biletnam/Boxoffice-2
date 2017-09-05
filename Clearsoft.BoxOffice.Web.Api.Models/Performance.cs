using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.Models
{
    public class Performance : ILinkContaininng
    {
        private List<Link> _links;

        public long PerformanceId { get; set; }
        public DateTime PerformanceDate { get; set; }
        public DateTime CreatedDate { get; set; }


        public List<Link> Links
        {
            get
            {
                return _links ?? new List<Link>();
            }
            set
            {
                _links = value;
            }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

    }
}
