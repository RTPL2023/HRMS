using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Employee_Emergency_Details
    {
        SQLConfig config = new SQLConfig();
        public string employee_id { get; set; }
        public string emg_contact_name { get; set; }
        public string emg_contact_relation { get; set; }
        public string emg_contact_no { get; set; }
    }
}
