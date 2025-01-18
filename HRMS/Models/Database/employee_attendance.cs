using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Includes;

namespace HRMS.Models.Database
{
    public class employee_attendance
    {
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
        public  int rownum { get; set; }

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
                sql = "insert into employee_attendance (employee_id,date,day,in_time,in_location_type,punchin_latitude,punchin_longitude) values ('"+ ea.employee_id + "', ";
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
                DateTime emp_intime = Convert.ToDateTime(ea.date + " " + ea.in_time);
                DateTime emp_outtime = Convert.ToDateTime(ea.date + " " + ea.out_time);
                TimeSpan diff = emp_outtime - emp_intime;
                if (ea.in_location_type=="Valid"&&ea.out_location_type=="Valid")
                {
                    if (diff.TotalHours <9)
                    {
                        if(diff.TotalHours > 4.5)
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





    }
}
