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
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }

       
        public List<Holiday_List> getholidaylist(string fdate, string tdate)
        {
            List<Holiday_List> ldlist = new List<Holiday_List>();
            string sql = "Select * from Holiday_List Where  convert(date,date,103)>=convert(date,'" + fdate + "',103)and convert(date,date,103)<=convert(date,'" + tdate + "',103) order by convert(date,date,103)";
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
                    ld.alt_id = !Convert.IsDBNull(dr["alt_id"]) ? Convert.ToInt32(dr["alt_id"]) : Convert.ToInt32(0);
                    if (ld.alt_id != 0)
                    {
                        sql = "Select * from Holiday_List Where  id=" + ld.alt_id + "";
                        config.singleResult(sql);
                        DataRow dr1 = (DataRow)config.dt.Rows[0];
                        ld.alternative = ld.alternative + " ( " + Convert.ToString(dr1["date"]) + "-" + Convert.ToString(dr1["description"]) + ") ";
                    }
                    ldlist.Add(ld);
                }
            }
            return (ldlist);
        }

        public string SaveUpdateHolidays(holidaysViewModel model,string user)
        {
            model.date = Convert.ToDateTime(model.date, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/");
            string msg = "";
            string sql = "Select * from Holiday_List where Convert(varchar,date,103) = Convert(varchar,'" + model.date + "',103)"; ;
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {
                config.Insert("Holiday_List", new Dictionary<string, object>()
                    {
                        { "date",model.date },
                        { "description",model.description },
                        { "alternative",model.alternative },
                        { "alt_id",model.altid},
                        { "created_by",user },
                        { "created_on",user },
                    });
                msg = "Holiday Added Successfully";
            }
            else
            {
                sql = "update Holiday_List set ";
                sql = sql + "description='" + model.description + "',";
                sql = sql + "alternative='" + model.alternative+"',";
                sql = sql + "alt_id='" + model.altid + "',";
                sql = sql + "modified_by='" +user+"',";
                sql = sql + "modified_on='" + u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/") + "'";
                sql = sql + "where ";
                sql = sql + " date='" + model.date + "'";
                config.Execute_Query(sql);
                msg = "Holiday Updated Successfully";
            }
            return (msg);
        }
    }
}
