using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.Models
{
    public class Event : ILinkContaininng
    {
        private List<Link> _links;
        private bool _shouldSerializePerformances;
        [Key]
        public long? EventId { get; set; }
        [Editable(true)]
        public string Name { get; set; }
        [Editable(false)]
        public DateTime? CreatedDate { get; set; }
        [Editable(false)]
        public Status Status { get; set; }
        [Editable(false)]
        public List<Performance> Performances { get; set; }
        [Editable(false)]
        public List<Link> Links
        {
            get
            {
                return _links ?? (_links = new List<Link>());
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

        public void SetShouldSerializePerformances(bool shouldSerialize)
        {
            _shouldSerializePerformances = shouldSerialize;
        }

        public bool ShouldSerializePerformances()
        {
            return _shouldSerializePerformances;
        }
    }
}
