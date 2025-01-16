using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Employee_Document
    {
        public string employee_id { get; set; }
        public Byte[] bank_detail { get; set; }
        public Byte[] aadhar { get; set; }
        public Byte[] pan { get; set; }
        public Byte[] voter_id { get; set; }
        public Byte[] joining_letter { get; set; }
        public Byte[] police_verification { get; set; }
        public Byte[] permanent_letter { get; set; }
        public Byte[] employee_img { get; set; }
    }
}
