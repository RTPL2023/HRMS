using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using HRMS.Includes;

namespace HRMS.Models.Database
{
    public class users
    {
        SQLConfig config = new SQLConfig();
        public string user_id { get; set; }
        public string employee_id { get; set; }
        public string user_role { get; set; }
        public string password { get; set; }
        public string allocated_branchid { get; set; }
        public string blocked { get; set; }
        public int valid { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string device_name { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }
        public string m_device_name { get; set; }
        public users getLoggin(string _employee_id, string _pwd)
        {
            string sql = "SELECT * FROM users WHERE employee_id = '" + _employee_id + "'  and Password = '" + _pwd + "' ";

            users us = new users();
            try
            {
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr = (DataRow)config.dt.Rows[0];
                    us.user_role = Convert.ToString(dr["user_role"]);
                    us.valid = 1;
                }
                else
                {
                    us.user_role = "";
                    us.valid = 2;
                }
            }
            catch (Exception ex)
            {
                us.valid = -1;

            }

            return us;
        }
        public string updatePassword(string _employee_id, string oldpass,string newpass)
        {
            string msg = "";
            string sql = "SELECT * FROM users WHERE employee_id = '" + _employee_id + "'  and Password = '" + oldpass + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "update users set Password='" + newpass + "' WHERE employee_id = '" + _employee_id + "'  and Password = '" + oldpass + "' ";
                config.Execute_Query(sql);
                msg = "Password Updated";
            }
            else
            {
                msg = "Old Password Miss Match!!";

            }
            return msg;
        }
    }
}
