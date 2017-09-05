using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Web.Api.Models
{
    public class NewEvent
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
