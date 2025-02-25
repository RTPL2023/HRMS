using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class LeaveApplyViewModel
    {
       public int id { get; set; }
        public string employee_id { get; set; }
        public string leave_from_date { get; set; }
        public string leave_duration { get; set; }
        public string leave_type { get; set; }
        public string leave_to_date { get; set; }
        public string leave_reason { get; set; }
        public string apply_date { get; set; }
        public string is_approved { get; set; }
        public string manager_remarks { get; set; }
        public string approved_by { get; set; }
        public string approved_on { get; set; }
        public IEnumerable<SelectListItem> empiddesc { get; set; }

    }
}
