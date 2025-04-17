using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using HRMS.Includes;

namespace HRMS.Models.DataBase
{
    public class MasterCountry
    {
        SQLConfig config = new SQLConfig();
        public String countryid { get; set; }
        public String countrysubid { get; set; }
        public String countryname { get; set; }
        public String created_by { get; set; }
        public String create_date { get; set; }
        public string create_time { get; set; }
        public String modified_by { get; set; }
        public String modified_date { get; set; }
        public String modified_time { get; set; }
        public String device_name { get; set; }
        public String m_device_name { get; set; }
        public int ccode { get; set; }
        public bool checkdata { get; set; }
        public string msg { get; set; }
        public List<MasterCountry> getAllCountryList()
        {
            string sql = "Select * from  Master_Country  where is_deleted<>1 order by CountryId";
            config.singleResult(sql);
            List<MasterCountry> mcl = new List<MasterCountry>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCountry mc = new MasterCountry();
                    mc.countryid = Convert.ToString(dr["CountryId"]);
                    mc.countryname = Convert.ToString(dr["CountryName"]);
                    mcl.Add(mc);
                }
            }
            return mcl;
        }
        public MasterCountry CheckCountryName(string country)
        {
            string sql = "Select * from  Master_Country where Is_Deleted = 0 order by CountryId";
            config.singleResult(sql);

            MasterCountry mc = new MasterCountry();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryname = Convert.ToString(dr["CountryName"]);
                    if (mc.countryname == country)
                    {
                        mc.checkdata = true;
                        return mc;
                    }
                    else
                    {
                        mc.checkdata = false;
                    }
                }

            }

            return mc;
        }
        public MasterCountry getcountryid(MasterCountry mc)
        {
            string sql = "Select * from  Master_Country order by CountryId";
            config.singleResult(sql);

            ccode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryid = Convert.ToString(dr["CountryId"]);
                    string subcode = (Convert.ToString(dr["CountryId"])).Substring(0, 1);
                    if (mc.countrysubid == subcode)
                    {
                        //ccode = Convert.ToInt32(mc.CountryId.Substring(5)) + 1;
                        ccode = ccode + 1;
                    }
                }

            }

            return mc;
        }
        public string saveCountry(MasterCountry mc)
        {
            config.Insert("Master_Country", new Dictionary<string, object>()
            {
                { "CountryId",   mc.countryid},
                { "CountryName", mc.countryname},
                { "Created_by",  mc.created_by},
                { "Create_date", mc.create_date},
                { "Create_Time", mc.create_time},
                { "device_name", mc.device_name},
               
            });
            mc.msg = "Saved Successfully";
            return mc.msg;
        }
        public string DeleteCountry(MasterCountry mc)
        {
            try
            {
                config.Update("Master_Country", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   mc.modified_by},
                    { "Modified_Date", mc.modified_date},
                    { "Modified_Time", mc.modified_time},
                    { "M_Device_Name", mc.m_device_name},

                }, new Dictionary<string, object>()
                  {
                  { "CountryId", mc.countryid }
                });
                mc.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                mc.msg = "Deletion Not Completed Succesfuly"+ex;
            }
            return mc.msg;
        }

        public string updateCountry(MasterCountry mc)
        {
            DataTable dt = new DataTable();
            Hashtable hs = new Hashtable();
            string sql = "select * from master_country where CountryName='" + mc.countryname + "'";
            config.singleResult(sql);
            int check = 0;
            if (config.dt.Rows.Count > 0)
            {
                check = 1;
                mc.msg = "Same Country Name Already Exist";
            }
            if (check == 0)
            {
                try
                {
                    config.Update("Master_Country", new Dictionary<String, object>()
                    {
                    { "countryname",   mc.countryname},
                    { "Modified_By",   mc.modified_by},
                    { "Modified_Date", mc.modified_date},
                    { "Modified_Time", mc.modified_time},
                    { "M_Device_Name", mc.m_device_name},

                    }, new Dictionary<string, object>()
                    {
                        { "CountryId", mc.countryid }
                    });
                    mc.msg = "Updated Successfuly";
                }
                catch (Exception ex)
                {
                    mc.msg = "Updation Not Completed Succesfuly"+ex;
                }
            }
            return mc.msg;
        }
        public List<MasterCountry> getCountryMast()
        {
            string sql = "Select * from  Master_Country where is_deleted<>1 order by countryname";
            config.singleResult(sql);

            List<MasterCountry> mcl = new List<MasterCountry>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCountry mc = new MasterCountry();
                    mc.countryid = Convert.ToString(dr["countryId"]);
                    mc.countryname = Convert.ToString(dr["countryname"]);
                    mcl.Add(mc);
                }

            }

            return mcl;
        }
        public MasterCountry GetCountryNameByCountryId(string countryid)
        {
            string sql = "Select * from  Master_Country where countryid='" + countryid + "' order by CountryId";
            config.singleResult(sql);

            MasterCountry mc = new MasterCountry();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryname = Convert.ToString(dr["CountryName"]);

                }

            }

            return mc;
        }
    }
}