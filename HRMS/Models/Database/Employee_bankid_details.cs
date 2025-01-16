using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Employee_bankid_details
    {
        SQLConfig config = new SQLConfig();
        public string employee_id { get; set; }
        public string ac_name { get; set; }
        public string ac_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string branch_code { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string voter_id_no { get; set; }
        public string pf_no { get; set; }
        public string esic_no { get; set; }
        public string passport_no { get; set; }
    }
}
