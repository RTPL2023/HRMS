using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Includes;
using System.Globalization;
using HRMS.Models.ViewModel;

namespace HRMS.Models.Database
{
    public class employee_attendance
    {
        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }

        public string date { get; set; }

        public string day { get; set; }

        public string in_time { get; set; }

        public string out_time { get; set; }

        public string punch_type { get; set; }

        public int is_approved { get; set; }

        public string approved_by { get; set; }

        public string approved_on { get; set; }

        public string remarks { get; set; }

        public string in_location_type { get; set; }

        public string out_location_type { get; set; }

        public string punchin_latitude { get; set; }

        public string punchin_longitude { get; set; }

        public string punchout_latitude { get; set; }

        public string punchout_longitude { get; set; }
        public decimal duration { get; set; }
        public string msg { get; set; }
        public int rownum { get; set; }

        public employee_attendance SaveInAttendance(employee_attendance ea)
        {
            string sql = "Select * from employee_attendance where employee_id='" + ea.employee_id + "' and date='" + ea.date + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                ea.msg = "Record Already Exist. Unable To Punch Again";
            }
            else
            {
                sql = "insert into employee_attendance (employee_id,date,day,in_time,in_location_type,punchin_latitude,punchin_longitude) values ('" + ea.employee_id + "', ";
                sql = sql + "'" + ea.date + "','" + ea.day + "','" + ea.in_time + "','" + ea.in_location_type + "','" + ea.punchin_latitude + "','" + ea.punchin_longitude + "')";
                ea.rownum = config.Execute_Query_WithRetValue(sql);

                if (ea.rownum > 0)
                {
                    ea.msg = "You Have Punched In Successfully";
                }
                else
                {
                    ea.msg = "Unable To Punch In";
                }
            }

            return ea;
        }
        public employee_attendance SaveOutAttendance(employee_attendance ea)
        {
            string sql = "Select * from employee_attendance where employee_id='" + ea.employee_id + "' and date='" + ea.date + "' and out_time is null";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ea.in_time = Convert.ToString(dr["in_time"]);
                ea.in_location_type = Convert.ToString(dr["in_location_type"]);
                DateTime emp_intime = Convert.ToDateTime(ea.date + " " + ea.in_time, usCinfo);
                DateTime emp_outtime = Convert.ToDateTime(ea.date + " " + ea.out_time, usCinfo);
                TimeSpan diff = emp_outtime - emp_intime;
                if (ea.in_location_type == "Valid" && ea.out_location_type == "Valid")
                {
                    if (diff.TotalHours < 9)
                    {
                        if (diff.TotalHours > 4.5)
                        {
                            ea.punch_type = "Valid";
                            ea.duration = Convert.ToDecimal(0.5);
                        }
                        else
                        {
                            ea.punch_type = "Invalid";
                            ea.duration = Convert.ToDecimal(0);
                        }

                    }
                    else
                    {
                        ea.punch_type = "Valid";
                        ea.duration = Convert.ToDecimal(1);
                    }
                }
                else
                {
                    ea.punch_type = "Invalid";
                    ea.duration = Convert.ToDecimal(0);
                }

                //ea.date = Convert.ToDateTime(ea.date).ToString("dd/MM/yyyy").Replace("-", "/");
                sql = "update employee_attendance set ";
                sql = sql + "out_time='" + ea.out_time + "',";
                sql = sql + "out_location_type='" + ea.out_location_type + "',";
                sql = sql + "punchout_latitude='" + ea.punchout_latitude + "',";
                sql = sql + "punchout_longitude='" + ea.punchout_longitude + "', ";
                sql = sql + "punch_type='" + ea.punch_type + "', ";
                sql = sql + "duration=" + ea.duration + " ";

                sql = sql + "where ";
                sql = sql + "employee_id='" + ea.employee_id + "'and ";
                sql = sql + "day='" + ea.day + "'and ";
                sql = sql + "date='" + ea.date + "'";
                ea.rownum = config.Update_Execute_Query(sql);
                if (ea.rownum > 0)
                {
                    ea.msg = "You Have Punched Out Successfully";
                }
                else
                {
                    ea.msg = "Unable To Punch Out";
                }


            }
            else
            {
                ea.msg = "No Punch In Record Found Or You Have Already Punched Out";

            }

            return ea;
        }



        public List<employee_attendance> getemployeeAttendacelistByemployeeId(string empl_id)
        {
            List<employee_attendance> ealst = new List<employee_attendance>();
            string sql = "Select top (45) * from employee_attendance Where employee_Id='" + empl_id + "' order by convert(date,date,103) desc";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    employee_attendance ea = new employee_attendance();
                    ea.id = Convert.ToInt32(dr["id"]);
                    ea.duration = !Convert.IsDBNull(dr["duration"]) ? Convert.ToDecimal(dr["duration"]) : Convert.ToDecimal(0);
                    ea.date = Convert.ToString(dr["date"]);
                    ea.day = Convert.ToString(dr["day"]);
                    ea.in_time = Convert.ToString(dr["in_time"]);
                    ea.out_time = Convert.ToString(dr["out_time"]);
                    //ea.duration = Convert.ToDecimal(dr["duration"]);
                    ea.punch_type = Convert.ToString(dr["punch_type"]);
                    ea.is_approved = !Convert.IsDBNull(dr["is_approved"]) ? Convert.ToInt32(dr["is_approved"]) : Convert.ToInt32(2);
                    ealst.Add(ea);
                }
            }
            return (ealst);
        }
        public List<employee_attendance> getdataBydate(string empl_id, string f_date, string t_date)
        {
            List<employee_attendance> ealst = new List<employee_attendance>();
            string sql = "Select * from employee_attendance Where employee_Id='" + empl_id + "' and convert(date,date,103)>=convert(date,'" + f_date + "',103)and convert(date,date,103)<=convert(date,'" + t_date + "',103) order by id desc";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    employee_attendance ea = new employee_attendance();
                    ea.id = Convert.ToInt32(dr["id"]);
                    ea.duration = !Convert.IsDBNull(dr["duration"]) ? Convert.ToDecimal(dr["duration"]) : Convert.ToDecimal(0);
                    ea.date = Convert.ToString(dr["date"]);
                    ea.day = Convert.ToString(dr["day"]);
                    ea.in_time = Convert.ToString(dr["in_time"]);
                    ea.out_time = Convert.ToString(dr["out_time"]);
                    //ea.duration = Convert.ToDecimal(dr["duration"]);
                    ea.punch_type = Convert.ToString(dr["punch_type"]);
                    ea.is_approved = !Convert.IsDBNull(dr["is_approved"]) ? Convert.ToInt32(dr["is_approved"]) : Convert.ToInt32(2);
                    ealst.Add(ea);
                }
            }
            return (ealst);
        }
        public List<employee_attendance> GetDetailAttendanceReportByEmpId(DetailReportViewModel model)
        {
            List<employee_attendance> ealst = new List<employee_attendance>();
            string sql = "Select  * from employee_attendance Where employee_Id='" + model.employee_id + "' and Convert(date,date,103)>=Convert(date,'" + model.from_date + "',103) and Convert(date,date,103)<=Convert(date,'" + model.to_date + "',103) order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    employee_attendance ea = new employee_attendance();
                    ea.employee_id = Convert.ToString(dr["employee_id"]);
                    ea.date = Convert.ToString(dr["date"]);
                    ea.day = Convert.ToString(dr["day"]);
                    ea.in_time = Convert.ToString(dr["in_time"]);
                    ea.out_time = Convert.ToString(dr["out_time"]);
                    ea.punch_type = Convert.ToString(dr["punch_type"]);
                    ea.duration = !Convert.IsDBNull(dr["duration"]) ? Convert.ToDecimal(dr["duration"]) : Convert.ToDecimal(0);
                    ea.is_approved = !Convert.IsDBNull(dr["is_approved"]) ? Convert.ToInt32(dr["is_approved"]) : Convert.ToInt32(2);
                    ealst.Add(ea);
                }
            }
            return (ealst);
        }



        public string SaveAttendanceByAdmin(PunchInPunchOutViewModel model, string user)
        {
            model.attendance_date = Convert.ToDateTime(model.attendance_date, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/");
            int lv_count = 0;
            int holiday_count = 0;
            int sunday = 0;
           
            string msg = "";
            string sql = "Select * from Employee_attendance where employee_id='" + model.employee_id + "' and Convert(varchar,date,103)=Convert(varchar,'" + model.attendance_date + "',103) ";
            config.singleResult(sql);

            if (config.dt.Rows.Count > 0)
            {
                msg = "Attendance Already Added";
            }
            else
            {
                leave_details ld = new leave_details();
                //check leaves on date
                sql = "Select * from Leave_count where employee_id='" + model.employee_id + "' and Convert(varchar,date,103)=Convert(varchar,'" + model.attendance_date + "',103)";
                config.singleResult(sql);

                if (config.dt.Rows.Count > 0)
                {
                    lv_count++;
                }
                //check Hiliday
                sql = "Select * from Holiday_List where  Convert(varchar,date,103)=Convert(varchar,'" + model.attendance_date + "',103)";
                config.singleResult(sql);

                if (config.dt.Rows.Count > 0)
                {
                    holiday_count++;
                }
                if (Convert.ToDateTime(model.attendance_date, usCinfo).DayOfWeek == DayOfWeek.Sunday)
                {
                    sunday++;
                }
                if (lv_count != 1 && holiday_count != 1 && sunday != 1)
                {

                    config.Insert("Employee_attendance", new Dictionary<string, object>()
                    { 
                          {"employee_Id" ,model.employee_id},
                          {"date",model.attendance_date},
                          {"day",Convert.ToDateTime( model.attendance_date,usCinfo).ToString("dddd")},
                          {"in_time",model.actual_In_time},
                          {"Out_time",model.actual_out_time},
                          {"In_Location_type","Valid"},
                          {"Out_Location_Type","Valid"},
                          {"Punchin_latitude","22.5973024"},
                          {"Punchin_longitude","88.4180823"},
                          {"Punchout_latitude","22.5973024"},
                          {"Punchout_longitude","88.4180823"},
                          {"punch_type","Valid"},
                          {"Duration",Convert.ToInt32(model.day_count)},
                          {"Remarks",model.reason+" (Done By-"+user+")"} });
                    msg = "Saved";
                }
                else
                {
                    msg = "Given Date Is Not Applicable For Attendance For This Employee";
                }
            }
            return msg;


        }

    }
}
