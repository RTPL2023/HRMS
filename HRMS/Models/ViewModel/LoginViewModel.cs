using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class LoginViewModel
    {
        public string emp_id { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string msg { get; set; }
        public string empployee_id { get; set; }
        public string oldpass { get; set; }
        public string newpass { get; set; }


    }
}
