using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Request
{
    public class FeesRequest
    {
        [Required(ErrorMessage ="From Date required")]
        public DateTime FromDate { get; set; } = DateTime.Now.AddMonths(-1);
        [Required(ErrorMessage = "To Date required")]
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
