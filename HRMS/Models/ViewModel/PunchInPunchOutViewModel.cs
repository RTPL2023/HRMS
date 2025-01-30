using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class PunchInPunchOutViewModel
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string in_location_type { get; set; }
        public string out_location_type { get; set; }
        public string requested_in_time { get; set; }
        public string requested_out_time { get; set; }
        public string attendance_date { get; set; }
        public string attendance_Id { get; set; }
        public string actual_In_time { get; set; }
        public string actual_out_time { get; set; }
        public string actual_punch_type { get; set; }
        public string actual_day_count { get; set; }
        public string reason { get; set; }
        public string employee_id { get; set; }
        public string application_date { get; set; }
        public string requested_punch_type { get; set; }
        public string msg { get; set; }
        public decimal requested_day_count { get; set; }
        public int is_approved { get; set; }
        public string attlist { get; set; }
        public string leavelist { get; set; }

    }
}
