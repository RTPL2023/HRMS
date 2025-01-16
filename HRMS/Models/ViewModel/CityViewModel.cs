using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HRMS.Models.ViewModel
{
    public class CityViewModel
    {
        public String CityId { get; set; }
        public String Citysubcode { get; set; }
        public String CityName { get; set; }
        public String DistrictId { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public String Create_Time { get; set; }
        public String DistrictName { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int codenum { get; set; }
        public string msg { get; set; }
        public int acode { get; set; }
        public IEnumerable<SelectListItem> DistrictDesc { get; set; }
    }
}