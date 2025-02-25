using HRMS.Models.ViewModel;
using HRMS.Models.DataBase;
using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Controllers;

namespace HRMS.Models.Database
{
    public class Employee_Attendance_mod_Request
    {
        UtilityController u = new UtilityController();
        SQLConfig config = new SQLConfig();
        public int attendance_Id { get; set; }
        public string employee_id { get; set; }
        public string name { get; set; }
        public string application_date { get; set; }
        public string attendance_date { get; set; }
        public string actual_In_time { get; set; }
        public string requested_in_time { get; set; }
        public string actual_out_time { get; set; }
        public string requested_out_time { get; set; }
        public string actual_punch_type { get; set; }
        public string requested_punch_type { get; set; }
        public decimal actual_day_count { get; set; }
        public decimal requested_day_count { get; set; }
        public int is_approved { get; set; }
        public string reason { get; set; }
        public string manager_remark { get; set; }
        public string saveInOutModifyDetails(PunchInPunchOutViewModel model, string employee_id)
        {
            string msg = "";
            string sql = "Select * from Employee_Attendance_mod_Request where attendance_Id = '" + model.attendance_Id + "' and employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {

                model.requested_punch_type = "Valid";
               
                config.Insert("Employee_Attendance_mod_Request", new Dictionary<string, object>()
                    {
                        { "attendance_Id",model.attendance_Id },
                        { "employee_id",employee_id },
                        { "attendance_date",model.attendance_date },
                        { "Application_date",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                        { "actual_In_time",model.actual_In_time},
                        { "Requested_In_time",model.requested_in_time},
                        { "actual_out_time",model.actual_out_time},
                        { "Requested_out_time",model.requested_out_time},
                        { "actual_punch_type",model.actual_punch_type},
                        { "Requested_punch_type",model.requested_punch_type},
                        { "actual_day_count",model.actual_day_count},
                        { "Requested_day_count",model.requested_day_count},
                        { "Reason",model.reason},

                        //{ "created_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/") },

                    });
                msg = "Application Submitted Successfully";

            }
            else
            {
                model.requested_punch_type = "Valid";

                sql = "update Employee_Attendance_mod_Request set ";
                sql = sql + "attendance_date='" + model.attendance_date + "',";
                sql = sql + "Application_date='" + u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/") + "',";
                sql = sql + "actual_In_time='" + model.actual_In_time + "',";
                sql = sql + "Requested_In_time='" + model.requested_in_time + "',";
                sql = sql + "actual_out_time='" + model.actual_out_time + "',";
                sql = sql + "Requested_out_time='" + model.requested_out_time + "',";
                sql = sql + "actual_punch_type='" + model.actual_punch_type + "',";
                sql = sql + "Requested_punch_type='" + model.requested_punch_type + "',";
                sql = sql + "actual_day_count='" + model.actual_day_count + "',";
                sql = sql + "Requested_day_count='" + model.requested_day_count + "',";
                sql = sql + "Reason='" + model.reason + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and attendance_Id='" + model.attendance_Id + "'";
                config.Execute_Query(sql);
                msg = "Application Modified Successfully";
            }

            return (msg);
        }
        public List<Employee_Attendance_mod_Request> getEmployeeAttModifyApplylistUndermanagers(string empl_id)
        {
            List<Employee_Attendance_mod_Request> amrlist = new List<Employee_Attendance_mod_Request>();

            string sql = "Select * from Employee_Master Where Reporting_mg_id='" + empl_id + "' Order By id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in config.dt.Rows)
                {
                    string employee_id = Convert.ToString(dr1["employee_id"]);
                    sql = "Select * from Employee_Attendance_mod_Request Where employee_Id='" + employee_id + "' and is_approved is null ";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in config.dt.Rows)
                        {
                            Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
                            amr.attendance_Id = Convert.ToInt32(dr["attendance_Id"]);
                            amr.name = Convert.ToString(dr1["name"]);
                            amr.employee_id = Convert.ToString(dr["employee_id"]);
                            amr.attendance_date = Convert.ToString(dr["attendance_date"]);
                            amr.application_date = Convert.ToString(dr["Application_date"]);
                            amr.actual_In_time = Convert.ToString(dr["actual_In_time"]);
                            amr.requested_in_time = Convert.ToString(dr["Requested_In_time"]);
                            amr.actual_out_time = Convert.ToString(dr["actual_out_time"]);
                            amr.requested_out_time = Convert.ToString(dr["requested_out_time"]);
                            amr.actual_punch_type = Convert.ToString(dr["actual_punch_type"]);
                            amr.requested_punch_type = Convert.ToString(dr["Requested_punch_type"]);
                            amr.actual_day_count = Convert.ToDecimal(dr["actual_day_count"]);
                            amr.requested_day_count = Convert.ToDecimal(dr["Requested_day_count"]);
                            amr.reason = Convert.ToString(dr["Reason"]);
                            amrlist.Add(amr);
                        }
                    }
                }
            }

            return (amrlist);
        }

        public string UpdateApprovrdAttandencebyEmployeeId(string employee_id, string attendance_Id, string appreason, string user)
        {
            string msg = "";
            string sql = "Select * from Employee_Attendance_mod_Request Where employee_Id='" + employee_id + "' and attendance_Id='" + attendance_Id + "'  ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
                foreach (DataRow dr in config.dt.Rows)
                {

                    amr.attendance_Id = Convert.ToInt32(dr["attendance_Id"]);
                    amr.employee_id = Convert.ToString(dr["employee_id"]);
                    amr.attendance_date = Convert.ToString(dr["attendance_date"]);
                    amr.application_date = Convert.ToString(dr["Application_date"]);
                    amr.actual_In_time = Convert.ToString(dr["actual_In_time"]);
                    amr.requested_in_time = Convert.ToString(dr["Requested_In_time"]);
                    amr.actual_out_time = Convert.ToString(dr["actual_out_time"]);
                    amr.requested_out_time = Convert.ToString(dr["requested_out_time"]);
                    amr.actual_punch_type = Convert.ToString(dr["actual_punch_type"]);
                    amr.requested_punch_type = Convert.ToString(dr["Requested_punch_type"]);
                    amr.actual_day_count = Convert.ToDecimal(dr["actual_day_count"]);
                    amr.requested_day_count = Convert.ToDecimal(dr["Requested_day_count"]);
                    amr.reason = Convert.ToString(dr["Reason"]);

                }
                //---- Employee_Attendance_mod_Request update------

                sql = "update Employee_Attendance_mod_Request set ";
                sql = sql + "Is_approved=1,";
                sql = sql + "Manager_remarks='" + appreason + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and attendance_Id='" + attendance_Id + "'";
                config.Execute_Query(sql);

                //------Employee_attendance update----------
                sql = "update Employee_attendance set ";
                sql = sql + "in_time='" + amr.requested_in_time + "',";
                sql = sql + "out_time='" + amr.requested_out_time + "',";
                sql = sql + "punch_type='" + amr.requested_punch_type + "',";
                sql = sql + "Duration='" + amr.requested_day_count + "',";
                sql = sql + "is_approved=1,";
                sql = sql + "approved_by='" + user + "',";
                sql = sql + "approved_on='" + u.currentDateTime().ToString("dd/MM/yyyy") + "',";
                sql = sql + "Remarks='" + appreason + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and id='" + attendance_Id + "'";
                config.Execute_Query(sql);
            }
            msg = "Approved Attendance";
            return (msg);
        }
        public string UpdateRejectedAttandencebyEmployeeId(string employee_id, string attendance_Id, string rejreason, string user)
        {
            string msg = "";
            string sql = "Select * from Employee_Attendance_mod_Request Where employee_Id='" + employee_id + "' and attendance_Id='" + attendance_Id + "'  ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                Employee_Attendance_mod_Request amr = new Employee_Attendance_mod_Request();
                foreach (DataRow dr in config.dt.Rows)
                {

                    amr.attendance_Id = Convert.ToInt32(dr["attendance_Id"]);
                    amr.employee_id = Convert.ToString(dr["employee_id"]);
                    amr.attendance_date = Convert.ToString(dr["attendance_date"]);
                    amr.application_date = Convert.ToString(dr["Application_date"]);
                    amr.actual_In_time = Convert.ToString(dr["actual_In_time"]);
                    amr.requested_in_time = Convert.ToString(dr["Requested_In_time"]);
                    amr.actual_out_time = Convert.ToString(dr["actual_out_time"]);
                    amr.requested_out_time = Convert.ToString(dr["requested_out_time"]);
                    amr.actual_punch_type = Convert.ToString(dr["actual_punch_type"]);
                    amr.requested_punch_type = Convert.ToString(dr["Requested_punch_type"]);
                    amr.actual_day_count = Convert.ToDecimal(dr["actual_day_count"]);
                    amr.requested_day_count = Convert.ToDecimal(dr["Requested_day_count"]);
                    amr.reason = Convert.ToString(dr["Reason"]);

                }
                //---- Employee_Attendance_mod_Request update------

                sql = "update Employee_Attendance_mod_Request set ";
                sql = sql + "Is_approved=0,";
                sql = sql + "Manager_remarks='" + rejreason + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and attendance_Id='" + attendance_Id + "'";
                config.Execute_Query(sql);

                //------Employee_attendance update----------
                sql = "update Employee_attendance set ";
                sql = sql + "is_approved=0,";
                sql = sql + "approved_by='" + user + "',";
                sql = sql + "approved_on='" + u.currentDateTime().ToString("dd/MM/yyyy") + "',";
                sql = sql + "Remarks='" + rejreason + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and id='" + attendance_Id + "'";
                config.Execute_Query(sql);
            }
            msg = "Reject Attendance";
            return (msg);
        }
    }
}
