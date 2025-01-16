using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models.ViewModel
{
    public class CountryViewModel
    {
        public String CountryId { get; set; }
        public String CountrySubId { get; set; }
        public String CountryName { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public string Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int ccode { get; set; }
        public string msg { get; set; }
    }
}