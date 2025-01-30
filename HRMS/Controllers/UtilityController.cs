using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.Models.ViewModel;

using HRMS.Models.Database;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;
using HRMS.Includes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRMS.Models.DataBase;

namespace HRMS.Controllers
{

    public class UtilityController : Controller
    {

        //****************For Branch Drop Down

        public JsonResult getBranchMastDetails()
        {
            MasterBranch mb = new MasterBranch();

            var mblst = mb.getBranchMast();
            return Json(mblst);
        }
        //****************For Country Drop Down
        public IEnumerable<SelectListItem> getCountryMastDetails()
        {
            StateViewModel svm = new StateViewModel();
            MasterCountry cm = new MasterCountry();
            svm.CountryDesc = cm.getCountryMast().ToList().Select(x => new SelectListItem
            {
                Value = x.CountryId.ToString(),
                Text = x.CountryName.ToString()
            }); ;
            return svm.CountryDesc;
        }
        //****************For State Drop Down
        public IEnumerable<SelectListItem> getStateMastDetails()
        {
            DistrictViewModel dvm = new DistrictViewModel();
            MasterState ms = new MasterState();
            dvm.StateDesc = ms.getStateMast().ToList().Select(x => new SelectListItem
            {
                Value = x.StateId.ToString(),
                Text = x.StateName.ToString()
            }); ;

            return dvm.StateDesc;

        }
        //****************For District Drop Down
        public IEnumerable<SelectListItem> getDistrictMastDetails()
        {
            CityViewModel cvm = new CityViewModel();
            MasterDistrict md = new MasterDistrict();
            cvm.DistrictDesc = md.getDistrictMast().ToList().Select(x => new SelectListItem
            {
                Value = x.DistrictId.ToString(),
                Text = x.DistrictName.ToString()
            }); ;

            return cvm.DistrictDesc;

        }
        //****************For City Drop Down
        public IEnumerable<SelectListItem> getCityMastDetails()
        {
            BranchMasterViewModel bvm = new BranchMasterViewModel();
            MasterCity mc = new MasterCity();
            bvm.CityDesc = mc.getCityMast().ToList().Select(x => new SelectListItem
            {
                Value = x.CityId.ToString(),
                Text = x.CityName.ToString()
            }); ;

            return bvm.CityDesc;
        }

        //****************District Wise City Drop Down
        public ActionResult FillCity(string District_Id)
        {
            MasterCity mc = new MasterCity();
            var cities = mc.GetCityByDistrictId(District_Id);
            return Json(cities);
        }

        //****************For Leave Drop Down

        public ActionResult GetleaveList()
        {
            leave_type_master ltm = new leave_type_master();
            var leavetype = ltm.GetleaveType();
            return Json(leavetype);
        }

        public string getleaveType(string lid)
        {
            string leaves = "";
            if (lid == "CL")
                leaves = "Casual Leave";
            if (lid == "EL")
                leaves = "Earned Leave";
            if (lid == "SL")
                leaves = "Sick Leave";
        
            if (lid == "CO")
                leaves = "Comp-Off";
          

            return leaves;

        }

        public DateTime currentDateTime()
        {
            DateTime curdt = DateTime.Now.AddHours(13);
            curdt = curdt.AddMinutes(30);
            return curdt;
        }

    }
}