using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class AddAllMasterOfAddressViewModel
    {
        public string countryname { get; set; }
        public string statename { get; set; }
        public string editcountryname { get; set; }
        public string editstatename { get; set; }
        public string editstateid{ get; set; }
        public string editcountryid { get; set; }
        public string countryid { get; set; }
        public string stateid { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string districtid { get; set; }
        public string districtname { get; set; }
        public string editdistrictname { get; set; }
        public string editdistrictid { get; set; }
        public string cityid { get; set; }
        public string cityname { get; set; }
        public string editcityname { get; set; }
        public string editcityid { get; set; }
        public IEnumerable<SelectListItem> countrydesc { get; set; }

        public string msg { get; set; }
    }
}
