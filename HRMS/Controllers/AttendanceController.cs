﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.IO;

namespace HRMS.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {

        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;

        UtilityController u = new UtilityController();

        public DateTimeFormatInfo UsCinfo { get => usCinfo; set => usCinfo = value; }

        [HttpGet]
        public IActionResult PunchINPunchOut(PunchInPunchOutViewModel model)
        {
            return View(model);
        }
        public JsonResult EmployeeInAttendanceMark(PunchInPunchOutViewModel model)
        {

            employee_attendance ea = new employee_attendance();
            ea.employee_id = User.Identity.Name;
            ea.date = u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/");
            ea.day = u.currentDateTime().ToString("dddd");
            ea.in_time = u.currentDateTime().ToString("HH:mm:ss");
            ea.in_location_type = model.in_location_type;
            ea.punchin_latitude = model.latitude;
            ea.punchin_longitude = model.longitude;
            ea = ea.SaveInAttendance(ea);
            return Json(ea.msg);
        }
        public JsonResult EmployeeOutAttendanceMark(PunchInPunchOutViewModel model)
        {
            string msg = "";
            try
            {
                employee_attendance ea = new employee_attendance();
                ea.employee_id = User.Identity.Name;
                ea.date = u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/");
                //ea.date = u.currentDateTime().ToString("MM/dd/yyyy").Replace("-", "/");
                ea.day = u.currentDateTime().ToString("dddd");
                ea.out_time = u.currentDateTime().ToString("HH:mm:ss");
                ea.out_location_type = model.out_location_type;
                ea.punchout_latitude = model.latitude;
                ea.punchout_longitude = model.longitude;
                ea = ea.SaveOutAttendance(ea);
                msg = ea.msg;
            }
            catch (Exception ex)
            {
                msg = Convert.ToString(ex);
            }


            return Json(msg);
        }

        public JsonResult getEmployeeAttendenceList(PunchInPunchOutViewModel model)
        {
            string empid = "";
            employee_attendance ea = new employee_attendance();
            List<employee_attendance> ealst = new List<employee_attendance>();
            if (model.employee_id == null)
            {
                empid = User.Identity.Name;
            }
            else
            {
                empid = model.employee_id;
            }
            ealst = ea.getemployeeAttendacelistByemployeeId(empid);
            string tableemenent = "";
            string atttype = "";
            string tagColor = "";
            if (ealst.Count > 0)
            {
                if (model.pg == "Add")
                {
                    tableemenent = "<thead><tr><th>Date</th><th>Day</th><th>In Time</th><th>Out Time</th><th>Half/Full Day</th><th>Punch Valid/Invalid</th></tr></thead><tbody>";
                }
                else
                {
                    tableemenent = "<thead><tr><th>Date</th><th>Day</th><th>In Time</th><th>Out Time</th><th>Half/Full Day</th><th>Punch Valid/Invalid</th><th>Action</th></tr></thead><tbody>";
                }
                foreach (var a in ealst)
                {

                    if (a.punch_type == "Valid")
                    {
                        if (a.duration == Convert.ToDecimal(1))
                        {
                            tagColor = "class=\"table-primary\"";
                        }
                        else if (a.duration == Convert.ToDecimal(0.5))
                        {

                            tagColor = " class=\"table-warning\"";

                        }
                        else if (a.duration < Convert.ToDecimal(0.5))
                        {
                            tagColor = " class=\"table-danger\"";
                        }
                    }
                    else
                    {
                        tagColor = " class=\"table-danger\"";

                    }
                    if (a.duration == Convert.ToDecimal(1))
                    {
                        atttype = "Full Day";
                        if (model.pg != "Add")
                        {
                            tableemenent = tableemenent + "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>" + atttype + "</td><td>" + a.punch_type + "</td><td></td></tr>";
                        }
                        else
                        {
                            tableemenent = tableemenent + "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>" + atttype + "</td><td>" + a.punch_type + "</td></tr>";
                        }
                    }
                    else if (a.duration == Convert.ToDecimal(0.5))
                    {
                        atttype = "Half Day";
                        tableemenent = tableemenent + "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>" + atttype + "</td><td>" + a.punch_type + "</td>";
                        if (a.is_approved == 2)
                        {
                            if (model.employee_id == null)
                            {
                                if (model.pg != "Add")
                                {
                                    tableemenent = tableemenent + "<td><button type =\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#EditInvalid\" onclick=btnEditOnclick('" + a.in_time + "','" + a.out_time + "','" + a.date + "','" + a.id + "','" + a.punch_type + "','" + a.duration + "')> <i class=\"fa-solid fa-pen\"></i></button ></td></tr>";
                                }
                                
                            }
                            else
                            {
                                tableemenent += "<td></td></tr>";
                            }
                        }
                        else
                        {
                            tableemenent += "<td></td></tr>";
                        }

                    }
                    else if (a.duration < Convert.ToDecimal(0.5))
                    {
                        atttype = "Less Than Half Day";
                        tableemenent = tableemenent + "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>" + atttype + "</td><td>" + a.punch_type + "</td>";
                        if (a.is_approved == 2)
                        {
                            if (model.employee_id == null)
                            {

                                if (model.pg != "Add")
                                {
                                    tableemenent = tableemenent + "<td><button type =\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#EditInvalid\" onclick=btnEditOnclick('" + a.in_time + "','" + a.out_time + "','" + a.date + "','" + a.id + "','" + a.punch_type + "','" + a.duration + "')> <i class=\"fa-solid fa-pen\"></i></button ></td></tr>";
                                }
                            }
                            else
                            {
                                tableemenent += "<td></td></tr>";
                            }

                        }
                        else
                        {
                            tableemenent += "<td></td></tr>";
                        }
                    }
                }
            }
            tableemenent += " </tbody>";
            return Json(tableemenent);
        }

        public JsonResult getfulldayHalfdayAndAbsent(PunchInPunchOutViewModel model)
        {
            employee_attendance ea = new employee_attendance();
            List<employee_attendance> ealst = new List<employee_attendance>();
            model.tot_days_in_month = Convert.ToString(DateTime.DaysInMonth(u.currentDateTime().Year, u.currentDateTime().Month));

            string month = u.currentDateTime().Month.ToString().Length == 1 ? "0" + u.currentDateTime().Month.ToString() : u.currentDateTime().Month.ToString();
            string fdate = "01/" + month + "/" + u.currentDateTime().Year;
            string tdate = model.tot_days_in_month + "/" + month + "/" + u.currentDateTime().Year;
            ealst = ea.getdataBydate(User.Identity.Name, fdate, tdate);

            foreach (var a in ealst)
            {

                if (a.duration == Convert.ToDecimal(1))
                {
                    model.fday++;

                }
                else if (a.duration == Convert.ToDecimal(0.5))
                {
                    model.hday++;



                }
                else if (a.duration < Convert.ToDecimal(0.5))
                {
                    model.absent++;


                }
            }

            model.sundays = Convert.ToString(u.CountDayOfWeekInMonth(u.currentDateTime().Year, u.currentDateTime().Month, DayOfWeek.Sunday));
            model.holidays = Convert.ToString(u.HolidaysInMonth(fdate, tdate));
            return Json(model);
        }
        public JsonResult editIntimeOutTime(PunchInPunchOutViewModel model)
        {
            Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
            string msg = amr.saveInOutModifyDetails(model, User.Identity.Name);
            return Json(msg);
        }
        [HttpGet]

        public IActionResult EmployeeAttendanceModifyList(PunchInPunchOutViewModel model)
        {
            return View(model);
        }
        public JsonResult getemployeeattendanceModifyList(PunchInPunchOutViewModel model)
        {
            Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
            List<Employee_Attendance_mod_Request> amrlist = new List<Employee_Attendance_mod_Request>();
            amrlist = amr.getEmployeeAttModifyApplylistUndermanagers(User.Identity.Name);
            model.attlist = "<tr><th>Name</th><th>Emp Id</th><th>Application Date</th><th>Attendance Date</th><th>Actual In Time</th><th>Requested In Time</th>" +
               "<th>Actual Out Time</th><th>Requested Out Time</th><th>Actual Punch Type</th><th>Requested Punch Type</th><th>Actual Day Count</th><th>Requested Day Count</th><th>Reason</th><th>Status</th></tr>";
            if (amrlist.Count > 0)
            {

               
                foreach (var a in amrlist)
                {
                    model.attlist = model.attlist + "<tr><td>" + a.name + "</td><td>" + a.employee_id + "</td><td>" + a.application_date + "</td><td>" + a.attendance_date + "</td><td>" + a.actual_In_time + "</td><td>" + a.requested_in_time + "</td>";
                    model.attlist = model.attlist + "<td>" + a.actual_out_time + "</td><td>" + a.requested_out_time + "</td><td>" + a.actual_punch_type + "</td><td>" + a.requested_punch_type + "</td><td>" + a.actual_day_count + "</td><td>" + a.requested_day_count + "</td><td>" + a.reason + "</td>";
                    model.attlist = model.attlist + "<td><button type =\"button\" class=\"btn btn-success\" data-bs-toggle=\"modal\" data-bs-target=\"#Approved\" onclick=btnApprovrdOnclick('" + a.attendance_Id + "','" + a.employee_id + "')>Approve</i></button ></td>";
                    model.attlist = model.attlist + "<td><button type =\"button\" class=\"btn btn-danger\" data-bs-toggle=\"modal\" data-bs-target=\"#Rejected\" onclick=btnRejectOnclick('" + a.attendance_Id + "','" + a.employee_id + "')>Rejected</i></button ></td></tr>";
                }
            }
            leave_details ed = new leave_details();
            List<leave_details> eedlist = new List<leave_details>();
            UtilityController uc = new UtilityController();

            string status = "";
            string status_color = "";
            eedlist = ed.getleaveDetailsbyemployee_id(User.Identity.Name);
            model.leavelist = "<tr><th>Emp Id</th><th>Name</th><th>Type</th><th>Duration</th><th>From Date</th><th>To Date</th><th>Reason</th>" +
               "<th>Status</th><th colspan=\"2\">Action</th></tr>";
            foreach (var a in eedlist)
            {
                if (a.is_approved == 2)
                {
                    status = "Pending";
                    status_color = "bga bgaa-warning";
                }
                else if (a.is_approved == 1)
                {
                    status = "Approved";
                    status_color = "bga bgaa-success ";
                }
                else
                {
                    status = "Rejected";
                    status_color = "bga bgaa-danger";
                }
                string leave = uc.getleaveType(a.leave_type);
                model.leavelist += "<tr>";
                model.leavelist += "<td>" + a.employee_id + "</td>";
                model.leavelist += "<td>" + a.name + "</td>";
                model.leavelist += "<td>" + leave + "</td>";
                model.leavelist += "<td>" + a.leave_duration + " days</td>";
                model.leavelist += "<td>" + a.leave_from_date + "</td>";
                model.leavelist += "<td>" + a.leave_to_date + "</td>";
                model.leavelist += "<td>" + a.leave_reason + "</td>";
                model.leavelist += "<td><span class=\"" + status_color + "\">" + status + "</span></td>";
                model.leavelist += "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#ApprovedLeave\" " +
                    "onclick=\"btnAcceptLeave('" + a.employee_id + "','" + a.id + "')\">" +
                    "<i class=\"fa-solid fa-check\"></i></button></td>";
                model.leavelist += "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#RejectedLeave\" " +
                       "onclick=\"btnRejectLeave('" + a.employee_id + "','" + a.id + "')\">" +
                       "<i class=\"fa-solid fa-xmark\"></i></button></td>";
                model.leavelist += "</tr>";
            }
            return Json(model);
        }


        public JsonResult ApprovedAttandence(string employee_id, string attendance_Id, string appreason)
        {
            Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
            string msg = amr.UpdateApprovrdAttandencebyEmployeeId(employee_id, attendance_Id, appreason, User.Identity.Name);
            return Json(msg);
        }
        public JsonResult RejectedAttandence(string employee_id, string attendance_Id, string rejreason)
        {
            Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
            string msg = amr.UpdateRejectedAttandencebyEmployeeId(employee_id, attendance_Id, rejreason, User.Identity.Name);
            return Json(msg);
        }

        [HttpGet]

        public IActionResult LeaveApply(PunchInPunchOutViewModel model)
        {
            return View(model);
        }

        public JsonResult SaveLeaveApply(LeaveApplyViewModel model)
        {
            leave_details ld = new leave_details();
            string msg = ld.SaveLeaveApplydetails(model, User.Identity.Name);
            return Json(msg);
        }
        public JsonResult getApplyleaveList(LeaveApplyViewModel model)
        {
            UtilityController uc = new UtilityController();
            leave_details ld = new leave_details();
            List<leave_details> ldlist = new List<leave_details>();
            ldlist = ld.getemployeeLeaveListByEmployee_id(User.Identity.Name);
            string tableElement = "";
            string status = "";
            string status_color = "";
            if (ldlist.Count > 0)
            {

                tableElement = "<tr><th>Leave Type</th><th>Leave Duration</th><th>From Date</th><th>To Date</th><th>Reason</th>" +
                "<th>Status</th><th>Manager Remarks</th><th>Action</th></tr>";
                foreach (var a in ldlist)
                {
                    if (a.is_approved == 2)
                    {
                        status = "Pending";
                        status_color = "bd-badge bg-warning";
                    }
                    else if (a.is_approved == 1)
                    {
                        status = "Approved";
                        status_color = "bd-badge bg-success";
                    }
                    else
                    {
                        status = "Rejected";
                        status_color = "bd-badge bg-danger";
                    }
                    string leave = uc.getleaveType(a.leave_type);
                    tableElement += "<tr>";
                    tableElement += "<td>" + leave + "</td>";
                    tableElement += "<td>" + a.leave_duration + " days</td>";
                    tableElement += "<td>" + a.leave_from_date + "</td>";
                    tableElement += "<td>" + a.leave_to_date + "</td>";
                    tableElement += "<td>" + a.leave_reason + "</td>";
                    tableElement += "<td><span class=\"" + status_color + "\">" + status + "</span></td>";
                    tableElement += "<td>" + a.manager_remarks + "</td>";
                    if (a.is_approved == 2)
                        tableElement += "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#Removeleave\" " +
                            "onclick=\"btnRemoveLeaveOnclick('" + a.id + "')\">" +
                            "<i class=\"fa-solid fa-pen\"></i></button></td>";

                    tableElement += "</tr>";
                }
            }
            return Json(tableElement);
        }
        public JsonResult RemoveLeaves(LeaveApplyViewModel model)
        {
            leave_details ld = new leave_details();
            string msg = ld.UpdateLeaveApplydetails(model, User.Identity.Name);
            return Json(msg);
        }
        public JsonResult ApprovedLeave(string employee_id, string id, string accleavereason)
        {
            leave_details ld = new leave_details();
            string msg = ld.UpdateApprovrdLeavebyEmployeeId(employee_id, id, accleavereason, User.Identity.Name);
            return Json(msg);
        }
        public JsonResult RejectedLeave(string employee_id, string id, string rejleavereason)
        {
            leave_details ld = new leave_details();
            string msg = ld.UpdateRejectedLeavebyEmployeeId(employee_id, id, rejleavereason, User.Identity.Name);
            return Json(msg);
        }
        public JsonResult RefreshLeave()
        {
            Leave_ledger ll = new Leave_ledger();
            string date = "01/" + u.currentDateTime().ToString("MM") + "/" + u.currentDateTime().Year.ToString();
            string msg = "";
            if (Convert.ToDateTime(date, UsCinfo) <= u.currentDateTime())
            {
                msg = ll.refreshleaveBalance(User.Identity.Name, date);
            }
            else
            {
                msg = "Chosen Month Cannot Be Greater Than Present Month";
            }
            return Json(msg);
        }
        public JsonResult getNumberOfleaves()
        {
            Leave_ledger ll = new Leave_ledger();
            var data = ll.getleaveBalance(User.Identity.Name);
            return Json(data);
        }


        public JsonResult getleaveledgerList(PunchInPunchOutViewModel model)
        {
            Leave_ledger ll = new Leave_ledger();
            List<Leave_ledger> lllst = new List<Leave_ledger>();
            lllst = ll.getleaveLedger(User.Identity.Name);
            string tableemenent = "";
            //string atttype = "";
            string tagColor = "";
            if (lllst.Count > 0)
            {
                tableemenent = "<tr><th>Date</th><th>Leave Type</th><th>Amount</th><th>Balance</th><th>Remarks</th></tr>";
                foreach (var a in lllst)
                {
                    if (a.dr_cr == "C")
                    {
                        tagColor = "class=\"table-primary\"";
                        tableemenent += "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.lv_hd + "</td><td>" + a.lv_amount + "</td><td>" + a.lv_balance + "</td><td>" + a.remarks + "</td></tr>";
                    }
                    else
                    {
                        tagColor = " class=\"table-danger\"";
                        tableemenent += "<tr " + tagColor + " ><td>" + a.date + "</td><td>" + a.lv_hd + "</td><td>" + a.lv_amount + "</td><td>" + a.lv_balance + "</td><td>" + a.remarks + "</td></tr>";
                    }
                }
            }
            return Json(tableemenent);
        }



        [HttpGet]

        public IActionResult AddCompOff(LeaveApplyViewModel model)
        {
            model.empiddesc = u.getempidDescBymanagerid(User.Identity.Name);
            return View(model);
        }

        public JsonResult saveCompOff(LeaveApplyViewModel model)
        {
            leave_details ld = new leave_details();
            string msg = ld.SaveCompOffByEmployeeID(model, User.Identity.Name);
            return Json(msg);
        }


        public string monthstartdate()
        {
            string startdt = "01/" + u.currentDateTime().Month + "/" + u.currentDateTime().Year;
            return startdt;
        }
        public string monthlastdate()
        {

            int lastDayOfMonth = DateTime.DaysInMonth(u.currentDateTime().Year, u.currentDateTime().Month);
            string enddt = lastDayOfMonth + "/" + u.currentDateTime().Month + "/" + u.currentDateTime().Year;

            return enddt;
        }

        public JsonResult getsundayList()
        {
            List<employee_attendance> ealst = new List<employee_attendance>();
            DateTime startdt = Convert.ToDateTime(monthstartdate(), usCinfo);
            var tbl = "";
            tbl = "<tr><th>Date</th><th>Day</th></tr>";
            int lastDayOfMonth = DateTime.DaysInMonth(u.currentDateTime().Year, u.currentDateTime().Month);

            for (int i = 1; i <= lastDayOfMonth; i++)
            {
                employee_attendance ea = new employee_attendance();

                if (startdt.DayOfWeek == DayOfWeek.Sunday)
                {
                    ea.date = Convert.ToString(startdt);
                    tbl += "<tr>";
                    tbl += "<td>" + startdt.ToString("dd/MM/yyyy") + "</td>";
                    tbl += "<td>Sunday</td><tr>";

                }
                startdt = startdt.AddDays(1);
            }

            return Json(tbl);
        }
        public JsonResult getholidaysList()
        {
            List<Holiday_List> hlist = new List<Holiday_List>();
            Holiday_List hl = new Holiday_List();
            DateTime startdt = new DateTime(u.currentDateTime().Year, u.currentDateTime().Month, 1);
            var tbl = "";
            tbl = "<tr><th>Date</th><th>Holiday</th><th>Alternative</th></tr>";
            int lastDayOfMonth = DateTime.DaysInMonth(u.currentDateTime().Year, u.currentDateTime().Month);
            DateTime enddt = new DateTime(u.currentDateTime().Year, u.currentDateTime().Month, lastDayOfMonth);
            hlist = hl.getholidaylist(monthstartdate(), monthlastdate());
            foreach (var a in hlist)
            {
                tbl += "<tr>";
                tbl += "<td>" + a.date + "</td>";
                tbl += "<td>" + a.description + "</td>";
                if (a.alternative == "Yes")
                {
                    tbl += "<td>Yes</td></tr>";
                }
                else
                {
                    tbl += "</tr>";

                }

            }

            return Json(tbl);
        }

        public JsonResult getFullDaylist(string day)
        {
            var tbl = "";
            tbl = "<tr><th>Date</th><th>Day</th><th>In Time</th><th>Out Time</th><th>Half/Full Day</th><th>Punch Valid/Invalid</th></tr>";


            employee_attendance ea = new employee_attendance();
            List<employee_attendance> ealst = new List<employee_attendance>();
            ealst = ea.getdataBydate(User.Identity.Name, monthstartdate(), monthlastdate());

            foreach (var a in ealst)
            {
                if (day == "full")
                {
                    if (a.duration == Convert.ToDecimal(1))
                    {

                        tbl += "<tr class=\"table-primary\"><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>Full Day</td><td>" + a.punch_type + "</td>" +
                             "<td></td></tr>";
                    }
                }
                else if (day == "half")
                {
                    if (a.duration == Convert.ToDecimal(0.5))
                    {

                        tbl += "<tr class=\"table-warning\"><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>Half Day</td><td>" + a.punch_type + "</td>" +
                            "<td></td></tr>";
                    }
                }
                else
                {
                    if (a.duration < Convert.ToDecimal(0.5))
                    {

                        tbl += "<tr class=\"table-danger\"><td>" + a.date + "</td><td>" + a.day + "</td><td>" + a.in_time + "</td><td>" + a.out_time + "</td><td>Less Then Half Day</td><td>" + a.punch_type + "</td>" +
                           "<td></td></tr>";
                    }
                }


            }

            return Json(tbl);
        }

        [HttpGet]
        public IActionResult Holidays(holidaysViewModel model)
        {

            return View(model);
        }
        public JsonResult SaveHolidays(holidaysViewModel model)
        {

            Holiday_List hl = new Holiday_List();
            string msg = hl.SaveUpdateHolidays(model, User.Identity.Name);
            return Json(msg);
        }

        public JsonResult getholidayList()
        {
            List<Holiday_List> hlist = new List<Holiday_List>();
            Holiday_List hl = new Holiday_List();

            var tbl = "";
            tbl = "<tr><th>Date</th><th>Holiday</th><th>Alternative</th></tr>";
            string fdate = "01/01/" + u.currentDateTime().Year;
            string tdate = "31/12/" + u.currentDateTime().Year;
            hlist = hl.getholidaylist(fdate, tdate);
            foreach (var a in hlist)
            {

                if (a.alt_id != 0)
                {
                    tbl += "<tr class=\"table-primary\">";
                    tbl += "<td>" + a.date + "</td>";
                    tbl += "<td>" + a.description + "</td>";
                    tbl += "<td>" + a.alternative + "</td></tr>";
                }
                else
                {
                    tbl += "<tr>";
                    tbl += "<td>" + a.date + "</td>";
                    tbl += "<td>" + a.description + "</td>";
                    tbl += "<td></td></tr>";

                }

            }
            return Json(tbl);
        }
        public IActionResult downloadHolidayPdf(string year, string month)
        {
            var document = GetDocument();
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-length", stream.Length.ToString());
            byte[] bytes = stream.ToArray();
            stream.Close();
            return File(bytes, "application/pdf", "HolidayList.pdf");
        }
        public PdfDocument GetDocument()
        {
            var document = new Document();
            BuildDocument(document);
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            //return pdfRenderer.PdfDocument;
            return pdfRenderer.PdfDocument;
        }

        public void BuildDocument(Document document)
        {
            List<Holiday_List> hlist = new List<Holiday_List>();
            Holiday_List hl = new Holiday_List();
            string fdate = "01/01/" + u.currentDateTime().Year;
            string tdate = "31/12/" + u.currentDateTime().Year;
            hlist = hl.getholidaylist(fdate, tdate);
            Section section = document.AddSection();
            //Image image = section.Headers.Primary.AddImage("D:\\CLW ALL Projects & Documentation\\clw final - Design\\Fin-Pro-v1\\wwwroot\\Images\\clw_logo1.jpeg");
            Image image = section.Headers.Primary.AddImage("wwwroot\\img\\logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.WrapFormat.Style = WrapStyle.Through;
            //Create Header
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 11;
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("Rishi Technovision PVT.LTD.");
            paragraph.AddLineBreak();
            paragraph.AddText("BF-88 SALTLAKE SECTOR-I, BIDHANNAGAR, WEST BENGAL      Phone: 0341 - 2526440");
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("Holiday List - " + u.currentDateTime().Year);
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.Format.SpaceAfter = 20;
            var table = section.AddTable();
            table.Style = "Table";
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.Alignment = RowAlignment.Center;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;
            table.Borders.Width = 0.5;
            Column column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("6cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            Row row = table.AddRow();
            row.Format.Font.Name = "Times New Roman";
            //row.Format.Font.Bold = true;
            row.Height = Unit.FromCentimeter(.6);
            //row.Format.Font.Size = 10;
            row.Cells[0].AddParagraph("Date");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Font.Name = "Arial";
            row.Cells[1].AddParagraph("Holiday");
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[1].Format.Font.Name = "Arial";
            row.Cells[2].AddParagraph("Alternative");
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Name = "Arial";
            foreach (var a in hlist)
            {
                Row row1 = table.AddRow();
                row1.Height = Unit.FromCentimeter(.6);
                row1.Format.Font.Name = "Times New Roman";
                if (a.alt_id != 0)
                {
                    row1.Cells[0].AddParagraph(a.date);
                    row1.Cells[1].AddParagraph(a.description);
                    row1.Cells[2].AddParagraph(a.alternative);
                    row1.Shading.Color = Colors.LightSkyBlue;
                }
                else
                {
                    row1.Cells[0].AddParagraph(a.date);
                    row1.Cells[1].AddParagraph(a.description);
                }
            }
        }




        [HttpGet]
        public IActionResult AddAttendance(PunchInPunchOutViewModel model)
        {
            model.empiddesc = u.getempidDescBymanagerid(User.Identity.Name);
            return View(model);
        }
        public JsonResult saveAttendenceByadmin(PunchInPunchOutViewModel model)
        {

            employee_attendance ea = new employee_attendance();
            string msg = ea.SaveAttendanceByAdmin(model, User.Identity.Name);
            return Json(msg);
        }

    }
}
