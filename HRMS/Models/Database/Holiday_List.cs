using HRMS.Models.ViewModel;
using HRMS.Models.DataBase;
using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Controllers;
using System.Globalization;

namespace HRMS.Models.Database
{
    public class Holiday_List
    {
        UtilityController u = new UtilityController();
        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string alternative { get; set; }
        public int alt_id { get; set; }
        public List<Holiday_List> getemployeeLeaveListByEmployee_id(string fdate,string tdate)
        {
            List<Holiday_List> ldlist = new List<Holiday_List>();
            string sql = "Select * from Holiday_List Where  convert(date,date,103)>=convert(date,'" + fdate + "',103)and convert(date,date,103)<=convert(date,'" + tdate + "',103) order by id desc";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Holiday_List ld = new Holiday_List();
                    ld.id = Convert.ToInt32(dr["id"]);
                    ld.description = Convert.ToString(dr["description"]);
                    ld.date = Convert.ToString(dr["date"]);
                    ld.alternative = Convert.ToString(dr["alternative"]);
                    ld.alt_id = !Convert.IsDBNull(dr["alt_id"])?Convert.ToInt32(dr["alt_id"]): Convert.ToInt32(0);
                   
                    ldlist.Add(ld);
                }
            }
            return (ldlist);
        }
    }
}
