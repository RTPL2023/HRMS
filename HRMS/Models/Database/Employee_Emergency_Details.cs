using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Data;
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
        public string emg_address { get; set; }



        public Employee_Emergency_Details getemployeeEmergency_Details(string employee_id)
        {
            Employee_Emergency_Details eec= new Employee_Emergency_Details();

            string sql = "Select * from Employee_Emergency_Details where employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    eec.emg_contact_name = Convert.ToString(dr["emg_contact_name"]);
                    eec.emg_contact_relation = Convert.ToString(dr["emg_contact_relation"]);
                    eec.emg_contact_no = Convert.ToString(dr["emg_contact_no"]);
                    eec.emg_address = Convert.ToString(dr["emg_address"]);
                }
            }
            return (eec);
        }
    }
}
