using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
namespace HRMS.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult PunchINPunchOut(PunchInPunchOutViewModel model)
        {
            return View(model);
        }
        public JsonResult EmployeeInAttendanceMark(PunchInPunchOutViewModel model)
        {

            employee_attendance ea = new employee_attendance();
            ea.employee_id = User.Identity.Name;
            ea.date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-","/");
            ea.day = DateTime.Now.ToString("dddd");
            ea.in_time = DateTime.Now.ToString("HH:mm:ss");
            ea.in_location_type = model.in_location_type;
            ea.punchin_latitude = model.latitude;
            ea.punchin_longitude = model.longitude;
            ea=ea.SaveInAttendance(ea);
            return Json(ea.msg);
        }
        public JsonResult EmployeeOutAttendanceMark(PunchInPunchOutViewModel model)
        {

            employee_attendance ea = new employee_attendance();
            ea.employee_id = User.Identity.Name;
            ea.date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            ea.day = DateTime.Now.ToString("dddd");
            ea.out_time = DateTime.Now.ToString("HH:mm:ss");
            ea.out_location_type = model.out_location_type;
            ea.punchout_latitude = model.latitude;
            ea.punchout_longitude = model.longitude;
           ea = ea.SaveOutAttendance(ea);
            return Json(ea.msg);
        }
    }
}
