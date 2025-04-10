using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;

namespace HRMS.Controllers
{
    public class ReportController : Controller
    {
        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;

        UtilityController u = new UtilityController();
        public IActionResult DetailReport(DetailReportViewModel model)
        {
            UtilityController u = new UtilityController();
           
            int month = 0;
            int year = 0;
            string mn = "";
            if(u.currentDateTime().AddMonths(-1).ToString("MM")=="12")
            {
                year = Convert.ToInt32(u.currentDateTime().ToString("yyyy")) - 1;
                month = Convert.ToInt32(u.currentDateTime().AddMonths(-1).ToString("MM"));
                mn = u.currentDateTime().AddMonths(-1).ToString("MM");
            }
            else
            {
                year = Convert.ToInt32(u.currentDateTime().ToString("yyyy"));
                month = Convert.ToInt32(u.currentDateTime().AddMonths(-1).ToString("MM"));
                mn = u.currentDateTime().AddMonths(-1).ToString("MM");
            }
            model.from_date = "01/" + mn + "/" + year.ToString();
            model.to_date= Convert.ToString(DateTime.DaysInMonth(year, month)+"/"+ mn + "/" + year.ToString());
            model.empiddesc = u.getempidDesc();
            model.tot_days_in_month = Convert.ToString(DateTime.DaysInMonth(year, month));
            model.sundays = Convert.ToString(u.CountDayOfWeekInMonth(year, month, DayOfWeek.Sunday));
            model.holidays = Convert.ToString(u.HolidaysInMonth(model.from_date, model.to_date));
          

            return View(model);
        }
        public JsonResult GetDetailAttendanceReport(DetailReportViewModel model)
        {
           int year = Convert.ToInt32(Convert.ToDateTime(model.from_date,usCinfo).ToString("yyyy"));
           int month = Convert.ToInt32(Convert.ToDateTime(model.from_date, usCinfo).ToString("MM"));
            model.tot_days_in_month = Convert.ToString(DateTime.DaysInMonth(year, month));
            model.sundays = Convert.ToString(u.CountDayOfWeekInMonth(year, month, DayOfWeek.Sunday));
            model.holidays = Convert.ToString(u.HolidaysInMonth(model.from_date, model.to_date));
            model.op_holidays = Convert.ToString(u.OpHolidaysInMonthTakenOrNot(model.employee_id,model.from_date, model.to_date));
            salary_master sm = new salary_master();
            sm = sm.GetSalaryDetailByEmployeeid(model.employee_id);
            leave_details ld = new leave_details();
            model.leaves_taken = ld.GetLeaveCountByEmpid(model).ToString("0.0");
            decimal work_day = 0;
            int j = 1;
            List<leave_details> ldl = new List<leave_details>();
            ldl = ld.GetLeaves(model.employee_id, model.from_date, model.to_date);
            if (ldl.Count > 0)
            {

                //fd = "<thead><tr><th> Srno</th><th> Item Name </th><th>Quantity</th></tr></thead>";
                foreach (var a in ldl)
                {
                    if (a.leave_type == "SL")
                    {
                        a.leave_type="Sick Leave";
                    }if (a.leave_type == "EL")
                    {
                        a.leave_type="Earned Leave";
                    }if (a.leave_type == "CL")
                    {
                        a.leave_type="Casual Leave";
                    }if (a.leave_type == "CO")
                    {
                        a.leave_type="Comp-Off";
                    }
                   
                    if (j == 1)
                    {
                        model.tableelement1 = "<thead><tr><th> Date</th><th>Type</th></thead>";
                        model.tableelement1 = model.tableelement1 + "<tr><td>" + a.apply_date + "</td><td>" + a.leave_type + "</td></tr>";

                    }
                    else
                    {
                        model.tableelement1 = model.tableelement1 + "<tr><td>" + a.apply_date + "</td><td>" + a.leave_type + "</td></tr>";
                    }
                    j = j + 1;

                    
                }
            }
            employee_attendance ea = new employee_attendance();
           
            List<employee_attendance> eal = new List<employee_attendance>();
            string TableElement = string.Empty;
            int i = 1;
            string daytype = "";
            string mod = "";
            
            eal = ea.GetDetailAttendanceReportByEmpId(model);
            if(eal.Count>0)
            {
               
                //fd = "<thead><tr><th> Srno</th><th> Item Name </th><th>Quantity</th></tr></thead>";
                foreach (var a in eal)
                {
                    if(a.duration==Convert.ToDecimal(0.5))
                    {
                        daytype = "Half Day";
                    }
                    if(a.duration==Convert.ToDecimal(1))
                    {
                        daytype = "Full Day";
                    }
                    if(a.duration==Convert.ToDecimal(0))
                    {
                        daytype = "Invalid";
                    }
                    if(a.is_approved==1)
                    {
                        mod = "Yes";
                    }
                    else
                    {
                        mod = "No";
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<thead><tr><th> Date</th><th>Day</th><th>In Time</th><th>Out Time</th><th>Duration</th><th>Punch Type</th><th>Modified</th></thead>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.date + "</td><td>" + a.day + "</td><td>" +a.in_time + "</td><td>" + a.out_time + "</td><td>" + daytype + "</td><td>" + a.punch_type + "</td><td>" +mod+ "</td></tr>";

                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>" + daytype + "</td><td>" + a.punch_type + "</td><td>" + mod+ "</td></tr>";
                    }
                    i = i + 1;

                    if (a.punch_type == "Valid")
                    {
                        work_day = work_day + a.duration;
                    }
                }
            }
            model.total_working_days = Convert.ToString(work_day);
            model.lop = Convert.ToString(Convert.ToDecimal(model.tot_days_in_month) - (Convert.ToDecimal(model.sundays) + Convert.ToDecimal(model.holidays) + Convert.ToDecimal(model.op_holidays)+ Convert.ToDecimal(model.leaves_taken)+ Convert.ToDecimal(model.total_working_days)));
            model.act_salary = sm.emp_net.ToString("0.00");
            model.cal_salary = (sm.emp_net - Convert.ToDecimal(Math.Round((sm.emp_net / 30) * Convert.ToDecimal(model.lop)))).ToString("0.00");

            Salary_Ledger sl = new Salary_Ledger();
            string monthname = Convert.ToDateTime(model.from_date, usCinfo).ToString("MMMM");
            sl.employee_id = model.employee_id;
            sl.month = monthname;
            sl.days_in_month = model.tot_days_in_month;
            sl.year = Convert.ToString(year);
            sl.actual_net_pay = Convert.ToDecimal(model.act_salary);
            sl.calculated_net_pay = Convert.ToDecimal(model.cal_salary);
            sl.lop = model.lop;
            sl.SaveSalaryLedger(sl);

            return Json(model);
        }

        public IActionResult getxlfileForDetailAttendance(string employee_id, string from_date, string to_date)
        {
            DetailReportViewModel model = new DetailReportViewModel();
            model.employee_id = employee_id;
            model.from_date = from_date;
            model.to_date = to_date;
            int year = Convert.ToInt32(Convert.ToDateTime(model.from_date, usCinfo).ToString("yyyy"));
            int month = Convert.ToInt32(Convert.ToDateTime(model.from_date, usCinfo).ToString("MM"));
            model.tot_days_in_month = Convert.ToString(DateTime.DaysInMonth(year, month));
            model.sundays = Convert.ToString(u.CountDayOfWeekInMonth(year, month, DayOfWeek.Sunday));
            model.holidays = Convert.ToString(u.HolidaysInMonth(model.from_date, model.to_date));
            model.op_holidays = Convert.ToString(u.OpHolidaysInMonthTakenOrNot(model.employee_id, model.from_date, model.to_date));
            leave_details ld = new leave_details();
            model.leaves_taken = ld.GetLeaveCountByEmpid(model).ToString("0.0");
            decimal work_day = 0;



            
            List<leave_details> ldl = new List<leave_details>();
            ldl = ld.GetLeaves(employee_id, from_date, to_date);
          
            employee_attendance ea = new employee_attendance();
            List<employee_attendance> eal = new List<employee_attendance>();

            Employee_Master em = new Employee_Master();
            em = em.getEmployeename(employee_id);
          
            string daytype = "";
            string mod = "";

            eal = ea.GetDetailAttendanceReportByEmpId(model);


            using (var workbook = new XLWorkbook())
            {
                
                var worksheet = workbook.Worksheets.Add("Attendance_Report_" + employee_id);
                worksheet.Style.Font.FontName = "Arial Narrow";
                worksheet.Style.Font.FontSize = 12;

                worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Style.NumberFormat.Format = "@";
                var currentRow = 1;
                worksheet.Cell(currentRow, 6).Style.Font.SetUnderline();
                worksheet.Cell(currentRow, 6).Style.Font.SetBold();
                worksheet.Cell(currentRow, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 6).Value = "Attendance Report Of " + em.name + " For " + from_date + " - " + to_date;
                currentRow = 2;
                worksheet.Cell(currentRow, 1).Value = "Date";
                worksheet.Cell(currentRow, 2).Value = "Day";
                worksheet.Cell(currentRow, 3).Value = "In Time";
                worksheet.Cell(currentRow, 4).Value = "Out Time";
                worksheet.Cell(currentRow, 5).Value = "Duration";
                worksheet.Cell(currentRow, 6).Value = "Punch Type";
                worksheet.Cell(currentRow, 7).Value = "Modified"; 
                worksheet.Cell(currentRow, 11).Value = "Leave Date";
                worksheet.Cell(currentRow, 12).Value = "Leave Type";




                currentRow = currentRow + 1;
                foreach (var a in eal)
                {
                    if (a.duration == Convert.ToDecimal(0.5))
                    {
                        daytype = "Half Day";
                    }
                    if (a.duration == Convert.ToDecimal(1))
                    {
                        daytype = "Full Day";
                    }
                    if (a.duration == Convert.ToDecimal(0))
                    {
                        daytype = "Invalid";
                    }
                    if (a.is_approved == 1)
                    {
                        mod = "Yes";
                    }
                    else
                    {
                        mod = "No";
                    }
                    worksheet.Cell(currentRow, 1).Value = a.date;
                    worksheet.Cell(currentRow, 2).Value = a.day;
                    worksheet.Cell(currentRow, 3).Value = a.in_time;
                    worksheet.Cell(currentRow, 4).Value = a.out_time;
                    worksheet.Cell(currentRow, 5).Value = daytype;
                    worksheet.Cell(currentRow, 6).Value = a.punch_type;
                    worksheet.Cell(currentRow, 7).Value = mod;

                    if (a.punch_type == "Valid")
                    {
                        work_day = work_day + a.duration;
                    }

                    currentRow++;
                }
                currentRow = 3;
                foreach (var a in ldl)
                {
                    if (a.leave_type == "SL")
                    {
                        a.leave_type = "Sick Leave";
                    }
                    if (a.leave_type == "EL")
                    {
                        a.leave_type = "Earned Leave";
                    }
                    if (a.leave_type == "CL")
                    {
                        a.leave_type = "Casual Leave";
                    }
                    if (a.leave_type == "CO")
                    {
                        a.leave_type = "Comp-Off";
                    }
                    worksheet.Cell(currentRow, 11).Value = a.apply_date;
                    worksheet.Cell(currentRow, 12).Value = a.leave_type;
                   


                    currentRow++;
                }
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Total Days";
                worksheet.Cell(currentRow, 2).Value = "Sundays";
                worksheet.Cell(currentRow, 3).Value = "Holidays";
                worksheet.Cell(currentRow, 4).Value = "Op-Holidays";
                worksheet.Cell(currentRow, 5).Value = "Leaves";
                worksheet.Cell(currentRow, 6).Value = "LOP";
                worksheet.Cell(currentRow, 7).Value = "Total Working Day";
                worksheet.Cell(currentRow, 8).Value = "Actual Net Pay";
                worksheet.Cell(currentRow, 9).Value = "Calculated Net Pay";
                salary_master sm = new salary_master();
                sm = sm.GetSalaryDetailByEmployeeid(model.employee_id);
                model.total_working_days = Convert.ToString(work_day);
                model.lop = Convert.ToString(Convert.ToDecimal(model.tot_days_in_month) - (Convert.ToDecimal(model.sundays) + Convert.ToDecimal(model.holidays) + Convert.ToDecimal(model.op_holidays) + Convert.ToDecimal(model.leaves_taken) + Convert.ToDecimal(model.total_working_days)));
                model.act_salary = sm.emp_net.ToString("0.00");
                model.cal_salary = (sm.emp_net - Convert.ToDecimal(Math.Round((sm.emp_net / 30) * Convert.ToDecimal(model.lop)))).ToString("0.00");

                currentRow++;
                worksheet.Cell(currentRow, 1).Value = model.tot_days_in_month;
                worksheet.Cell(currentRow, 2).Value = model.sundays;
                worksheet.Cell(currentRow, 3).Value = model.holidays;
                worksheet.Cell(currentRow, 4).Value = model.op_holidays;
                worksheet.Cell(currentRow, 5).Value = model.leaves_taken;
                worksheet.Cell(currentRow, 6).Value = model.lop;
                worksheet.Cell(currentRow, 7).Value = model.total_working_days;
                worksheet.Cell(currentRow, 8).Value = model.act_salary;
                worksheet.Cell(currentRow, 9).Value = model.cal_salary;

                Salary_Ledger sl = new Salary_Ledger();
                string monthname = Convert.ToDateTime(model.from_date, usCinfo).ToString("MMMM");
                sl.employee_id = model.employee_id;
                sl.month = monthname;
                sl.days_in_month = model.tot_days_in_month;
                sl.year = Convert.ToString(year);
                sl.actual_net_pay = Convert.ToDecimal(model.act_salary);
                sl.calculated_net_pay = Convert.ToDecimal(model.cal_salary);
                sl.lop = model.lop;
                sl.SaveSalaryLedger(sl);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "" + employee_id + "_Attendance_Rpt_" + from_date + "_To_" + to_date.Replace("/", "_") + ".xlsx");
                }

            }
            
        } 
        
        
        public IActionResult getxlfileForSummaryAttendance(string from_date, string to_date)
        {
            List<Employee_Master> empl = new List<Employee_Master>();
            Employee_Master em = new Employee_Master();
            empl = em.getempidMast();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Summary_Attendance_Report");
                worksheet.Style.Font.FontName = "Arial Narrow";
                worksheet.Style.Font.FontSize = 12;

                worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Style.NumberFormat.Format = "@";
                var currentRow = 1;
                worksheet.Cell(currentRow, 6).Style.Font.SetUnderline();
                worksheet.Cell(currentRow, 6).Style.Font.SetBold();
                worksheet.Cell(currentRow, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 6).Value = "Attendance Report For " + from_date + " - " + to_date;
                currentRow = 2;
                worksheet.Cell(currentRow, 1).Value = "Employee Id";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Total Days";
                worksheet.Cell(currentRow, 4).Value = "Sundays";
                worksheet.Cell(currentRow, 5).Value = "Holidays";
                worksheet.Cell(currentRow, 6).Value = "Op-Holidays";
                worksheet.Cell(currentRow, 7).Value = "Leaves";
                worksheet.Cell(currentRow, 8).Value = "LOP";
                worksheet.Cell(currentRow, 9).Value = "Total Working Day";
                worksheet.Cell(currentRow, 10).Value = "Actual Net Pay";
                worksheet.Cell(currentRow, 11).Value = "Calculated Net Pay";
                foreach (var ab in empl)
                {
                    //if (ab.employee_id == "RTPL0014")
                    //{

                    //}
                    DetailReportViewModel model = new DetailReportViewModel();
                    model.employee_id = ab.employee_id;
                    model.from_date = from_date;
                    model.to_date = to_date;
                    int year = Convert.ToInt32(Convert.ToDateTime(model.from_date, usCinfo).ToString("yyyy"));
                    int month = Convert.ToInt32(Convert.ToDateTime(model.from_date, usCinfo).ToString("MM"));
                    model.tot_days_in_month = Convert.ToString(DateTime.DaysInMonth(year, month));
                    model.sundays = Convert.ToString(u.CountDayOfWeekInMonth(year, month, DayOfWeek.Sunday));
                    model.holidays = Convert.ToString(u.HolidaysInMonth(model.from_date, model.to_date));
                    model.op_holidays = Convert.ToString(u.OpHolidaysInMonthTakenOrNot(model.employee_id, model.from_date, model.to_date));
                    leave_details ld = new leave_details();
                    model.leaves_taken = ld.GetLeaveCountByEmpid(model).ToString("0.0");
                    salary_master sm = new salary_master();
                    sm = sm.GetSalaryDetailByEmployeeid(model.employee_id);
                    decimal work_day = 0;

                    employee_attendance ea = new employee_attendance();
                    List<employee_attendance> eal = new List<employee_attendance>();
                    em = em.getEmployeename(ab.employee_id);

                    eal = ea.GetDetailAttendanceReportByEmpId(model);

                    foreach (var a in eal)
                    {

                        if (a.punch_type == "Valid")
                        {
                            work_day = work_day + a.duration;
                        }

                    }
                    
                    model.total_working_days = Convert.ToString(work_day);
                    model.lop = Convert.ToString(Convert.ToDecimal(model.tot_days_in_month) - (Convert.ToDecimal(model.sundays) + Convert.ToDecimal(model.holidays) + Convert.ToDecimal(model.op_holidays) + Convert.ToDecimal(model.leaves_taken) + Convert.ToDecimal(model.total_working_days)));
                    model.act_salary = sm.emp_net.ToString("0.00");
                    model.cal_salary = (sm.emp_net - Convert.ToDecimal(Math.Round((sm.emp_net / 30) * Convert.ToDecimal(model.lop)))).ToString("0.00");


                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = ab.employee_id;
                    worksheet.Cell(currentRow, 2).Value = em.name;
                    worksheet.Cell(currentRow, 3).Value = model.tot_days_in_month;
                    worksheet.Cell(currentRow, 4).Value = model.sundays;
                    worksheet.Cell(currentRow, 5).Value = model.holidays;
                    worksheet.Cell(currentRow, 6).Value = model.op_holidays;
                    worksheet.Cell(currentRow, 7).Value = model.leaves_taken;
                    worksheet.Cell(currentRow, 8).Value = model.lop;
                    worksheet.Cell(currentRow, 9).Value = model.total_working_days;
                    worksheet.Cell(currentRow, 10).Value = model.act_salary;
                    worksheet.Cell(currentRow, 11).Value = model.cal_salary;

                    Salary_Ledger sl = new Salary_Ledger();
                    string monthname = Convert.ToDateTime(model.from_date, usCinfo).ToString("MMMM");
                    sl.employee_id = model.employee_id;
                    sl.month = monthname;
                    sl.days_in_month = model.tot_days_in_month;
                    sl.year = Convert.ToString(year);
                    sl.actual_net_pay = Convert.ToDecimal(model.act_salary);
                    sl.calculated_net_pay = Convert.ToDecimal(model.cal_salary);
                    sl.lop = model.lop;
                    sl.SaveSalaryLedger(sl);

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Attendance_Rpt_Summary_" + from_date + "_To_" + to_date.Replace("/", "_") + ".xlsx");
                }

            }
            
        }
    }
}
