using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class SalaryMasterViewModel
    {
        public string basic { get; set; }
        public string hra { get; set; }
        public string other_allw { get; set; }
        public string tot_earn { get; set; }
        public string emp_ptax { get; set; }
        public string emp_pf { get; set; }
        public string emp_esic { get; set; }
        public string emp_tds { get; set; }
        public string comp_pf { get; set; }
        public string comp_esic { get; set; }
        public string tot_emp_ded { get; set; }
        public string tot_comp_con { get; set; }
        public string emp_net { get; set; }
        public string tot_ctc { get; set; }
        public string tds_type { get; set; }
        public IEnumerable<SelectListItem> empiddesc { get; set; }
        public IEnumerable<SelectListItem> financal_yeardesc { get; set; }
        public string employee_id { get; set; }
        public string effect_date { get; set; }
        public string designation { get; set; }
        public string financal_year { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        
    }
}
