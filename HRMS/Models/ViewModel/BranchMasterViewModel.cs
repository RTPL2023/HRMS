using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Models.ViewModel
{
    public class BranchMasterViewModel
    {
        public string BranchId { get; set; }
        public string Date { get; set; }
        public string BranchName { get; set; }
        public string InvCode { get; set; }
        public string BranchAddress { get; set;}
        public string Gst_In { get; set; }
        public string CityId { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> CityDesc { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public IEnumerable<SelectListItem> DistDesc { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public IEnumerable<SelectListItem> StateDesc { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public IEnumerable<SelectListItem> CountryDesc { get; set; }
        public string Pin { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string createdBy { get; set; }
        public string createDate { get; set; }
        public string createTime { get; set; }
        public string DeviceName { get; set; }
        public string Modified_by { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedTime { get; set; }
        public string MdeviceName { get; set; }
        public string msg { get; set; }
    }
}