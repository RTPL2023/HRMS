using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Models.ViewModel
{
    public class DetailReportViewModel
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string tot_days_in_month { get; set; }
        public string sundays { get; set; }
        public string holidays { get; set; }
        public string op_holidays { get; set; }
        public string leaves_taken { get; set; }
        public string lop { get; set; }
        public string total_working_days { get; set; }
        public string tableelement { get; set; }
        public string tableelement1 { get; set; }
        public IEnumerable<SelectListItem> empiddesc { get; set; }

    }
}
