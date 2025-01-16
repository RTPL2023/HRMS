using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HRMS.Models.ViewModel
{
    public class DistrictViewModel
    {
        public String DistrictId { get; set; }
        public String DistrictSubId { get; set; }
        public String DistrictName { get; set; }
        public String StateId { get; set; }
        public String StateName { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public string Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int dcode { get; set; }
        public string msg { get; set; }
        public string State { get; set; }
       
        public IEnumerable<SelectListItem> StateDesc { get; set; }
    }
}