using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Employee_bankid_details getemployeeBankdetails(string employee_id)
        {
            Employee_bankid_details ebd = new Employee_bankid_details();

            string sql = "Select * from Employee_bankid_details where employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ebd.ac_name = Convert.ToString(dr["ac_name"]);
                    ebd.ac_no = Convert.ToString(dr["ac_no"]);
                    ebd.bank_name = Convert.ToString(dr["bank_name"]);
                    ebd.branch_name = Convert.ToString(dr["branch_name"]);
                    ebd.ifsc_code = Convert.ToString(dr["ifsc_code"]);
                    ebd.branch_code = Convert.ToString(dr["branch_code"]);
                    ebd.aadhar_no = Convert.ToString(dr["aadhar_no"]);
                    ebd.pan_no = Convert.ToString(dr["pan_no"]);
                    ebd.voter_id_no = Convert.ToString(dr["voter_id_no"]);
                    ebd.pf_no = Convert.ToString(dr["pf_no"]);
                    ebd.esic_no = Convert.ToString(dr["esic_no"]);
                    ebd.passport_no = Convert.ToString(dr["passport_no"]);

                }

            }

            return (ebd);
        }
    }
}
