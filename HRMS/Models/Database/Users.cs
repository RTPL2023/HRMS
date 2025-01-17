using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Users
    {

        public string user_id { get; set; }
        public string employee_id { get; set; }
        public string user_role { get; set; }
        public string password { get; set; }
        public string allocated_branchid { get; set; }
        public string blocked { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string device_name { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }
        public string m_device_name { get; set; }
    }
}
