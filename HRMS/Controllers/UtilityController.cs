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
using System.Data;

namespace HRMS.Controllers
{

    public class UtilityController : Controller
    {
        SQLConfig config = new SQLConfig();

        //****************For Branch Drop Down

        public JsonResult getBranchMastDetails()
        {
            MasterBranch mb = new MasterBranch();

            var mblst = mb.getBranchMast();
            return Json(mblst);
        }
        //****************For Country Drop Down
        public JsonResult getCountryMastDetails()
        {
            
            MasterCountry cm = new MasterCountry();
            var country = cm.getCountryMast();
            
            return  Json(country);
        }
        //****************For State Drop Down
        public JsonResult getStateMastDetails()
        {

            MasterState ms = new MasterState();
            var State = ms.getStateMast();

            return Json(State);
        }

        //****************For District Drop Down
        public JsonResult getDistrictMastDetails()
        {

            MasterDistrict md = new MasterDistrict();

            var District = md.getDistrictMast();

            return Json(District);
        }

        //****************For City Drop Down
        public JsonResult getCityMastDetails()
        {

            MasterCity mc = new MasterCity();


            var City = mc.getCityMast();

            return Json(City);
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
        //****************For Designation Drop Down

        public JsonResult getdesignation()
        {
            Designation_Master ltm = new Designation_Master();
            var dlst = ltm.getDesignation_Masterlists();
            return Json(dlst);
        }
        //****************For Designation Drop Down

        public JsonResult getdepartmentmaster()
        {
            Department_Master dm = new Department_Master();
            var dmlst = dm.getDepartment_Masterlists();
            return Json(dmlst);
        }
        //****************For ReportingManager Drop Down

        public JsonResult getReportingManager()
        {
            Employee_Master em = new Employee_Master();
            var emplst = em.getmanageremployeelists();
            return Json(emplst);
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
        //cloud
        //public DateTime currentDateTime()
        //{
        //    DateTime curdt = DateTime.Now.AddHours(13);
        //    curdt = curdt.AddMinutes(30);
        //    return curdt;
        //}
        //local
        public DateTime currentDateTime()
        {
            DateTime curdt = DateTime.Now;

            return curdt;
        }
        public IEnumerable<SelectListItem> getempidDesc()
        {
            DetailReportViewModel m = new DetailReportViewModel();
            Employee_Master em = new Employee_Master();

            m.empiddesc = em.getempidMast().ToList().Select(x => new SelectListItem
            {
                Value = x.employee_id.ToString(),
                Text = x.name.ToString()
            });

            return m.empiddesc;
        }
        //day count
        public int CountDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek)
        {
            DateTime startDate = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            int weekDayCount = 0;
            for (int day = 0; day < days; ++day)
            {
                weekDayCount += startDate.AddDays(day).DayOfWeek == dayOfWeek ? 1 : 0;
            }
            return weekDayCount;
        }

        //Optional Holiday count
        public int OpHolidaysInMonthTakenOrNot(string empid, string from_dt, string to_dt)
        {

            string date = "";
            int count = 0;

            string sql = "Select * from Holiday_List where convert(date,date,103)>=convert(date,'" + from_dt + "',103) and convert(date,date,103)<=convert(date,'" + to_dt + "',103) and alternative='Yes'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    date = Convert.ToString(dr["date"]);
                    sql = "Select * from employee_attendance where employee_id=empid and date='" + date + "' and punch_type='Valid'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count == 0)
                    {
                        count = count + 1;
                    }

                }

            }
            return count;
        }

        //Holiday count
        public int HolidaysInMonth(string from_dt, string to_dt)
        {
            int count = 0;
            string sql = "Select Count(*) as cnt from Holiday_List where convert(date,date,103)>=convert(date,'" + from_dt + "',103) and convert(date,date,103)<=convert(date,'" + to_dt + "',103) and alternative='No'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                count = Convert.ToInt32(dr["cnt"]);
            }
            return count;
        }
    }
}