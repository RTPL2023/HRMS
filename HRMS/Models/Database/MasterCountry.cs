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
        public String countryId { get; set; }
        public String CountrySubId { get; set; }
        public String countryname { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public string Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int ccode { get; set; }
        public bool checkdata { get; set; }
        public string msg { get; set; }

        //public string UpdateCountry(MasterCountry mc)
        //{
        //    string sql = "select * from master_country where countryname='" + mc.countryname + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        mc.msg = "Same Country Name Already Exist";
        //    }
        //    if(check == 0)
        //    {
        //        try
        //        {
        //            config.Update("Master_Country",new Dictionary<String, object>()
        //            {
        //            { "countryname",   mc.countryname},
        //            { "Modified_By",   mc.Modified_by},
        //            { "Modified_Date", mc.Modified_Date},
        //            { "Modified_Time", mc.Modified_Time},
        //            { "M_Device_Name", mc.M_Device_name},

        //            }, new Dictionary<string, object>()
        //            {
        //            { "CountryId", mc.CountryId },
        //            });
        //            mc.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            mc.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
            
        //    return mc.msg;
        //}

        //public DataTable UpdateCountry(MasterCountry mc)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    string sql = "select * from master_country where countryname='" + mc.countryname + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        mc.msg = "Same Country Name Already Exist";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            {
        //                hs.Add("countryname", mc.countryname);
        //                hs.Add("Modified_By", mc.Modified_by);
        //                hs.Add("Modified_Date", mc.Modified_Date);
        //                hs.Add("Modified_Time", mc.Modified_Time);
        //                hs.Add("M_Device_Name", mc.M_Device_name);
        //            }
        //            {
        //                hs.Add("CountryId", mc.CountryId);
        //            }
        //            dt = config.ReturnScalar("spUpdateCountry", hs);
        //            //if (dt.Rows.Count > 0) mc.msg = "Updated Successfuly";
        //            mc.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            mc.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return dt;
        //}
        public MasterCountry getCountryDetailsByCountryId(string Country_Id,string db)
        {
            MasterCountry mc = new MasterCountry();
            string sql = "Select * from  Master_Country where countryid='" + Country_Id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryId = Convert.ToString(dr["countryId"]);
                    mc.countryname = Convert.ToString(dr["countryname"]);
                }
            }
            return mc;
        }
        public List<MasterCountry> getAllCountryList(string db)
        {
            string sql = "Select * from  Master_Country  where is_deleted<>1 order by countryId";
            config.singleResult(sql);
            List<MasterCountry> mcl = new List<MasterCountry>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCountry mc = new MasterCountry();
                    mc.countryId = Convert.ToString(dr["countryId"]);
                    mc.countryname = Convert.ToString(dr["countryname"]);
                    mcl.Add(mc);
                }
            }
            return mcl;
        }
        public string DeleteCountry(MasterCountry mc)
        {
            try
            {
                config.Update("Master_Country", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   mc.Modified_by},
                    { "Modified_Date", mc.Modified_Date},
                    { "Modified_Time", mc.Modified_Time},
                    { "M_Device_Name", mc.M_Device_name},

                }, new Dictionary<string, object>()
                  {
                  { "countryId", mc.countryId }
                });
                mc.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                mc.msg = "Deletion Not Completed Succesfuly";
            }
            return mc.msg;
        }
        public MasterCountry getcountryid(MasterCountry mc)
        {
            string sql = "Select * from  Master_Country order by countryId";
            config.singleResult(sql);

            ccode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryId = Convert.ToString(dr["countryId"]);
                    string subcode = (Convert.ToString(dr["countryId"])).Substring(0, 1);
                    if (mc.CountrySubId == subcode)
                    {
                        //ccode = Convert.ToInt32(mc.countryId.Substring(5)) + 1;
                        ccode = ccode + 1;
                    }
                }

            }

            return mc;
        }
        public MasterCountry Checkcountryname(string country)
        {
            string sql = "Select * from  Master_Country where Is_Deleted = 0 order by countryId";
            config.singleResult(sql);

            MasterCountry mc = new MasterCountry();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryname = Convert.ToString(dr["countryname"]);
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

        public MasterCountry GetcountrynameBycountryId(string countryid)
        {
            string sql = "Select * from  Master_Country where countryid='"+ countryid + "' order by countryId";
            config.singleResult(sql);

            MasterCountry mc = new MasterCountry();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryname = Convert.ToString(dr["countryname"]);
                   
                }

            }

            return mc;
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
                    mc.countryId = Convert.ToString(dr["countryId"]);
                    mc.countryname = Convert.ToString(dr["countryname"]);
                    mcl.Add(mc);
                }

            }

            return mcl;
        }
        //public string saveCountry(MasterCountry mc)
        //{
        //    config.Insert("Master_Country", new Dictionary<string, object>()
        //    {
        //        { "countryId",   mc.countryId},
        //        { "countryname", mc.countryname},
        //        { "Created_by",  mc.Created_by},
        //        { "Create_date", mc.Create_date},
        //        { "Create_Time", mc.Create_Time},
        //        { "Modified_by", mc.Modified_by},
        //        { "Modified_Date", mc.Modified_Date},
        //        { "Modified_Time", mc.Modified_Time},
        //        { "Device_name", mc.Device_name},
        //        { "M_Device_name", mc.M_Device_name},
        //    });
        //        mc.msg = "Saved Successfully";
        //    return mc.msg;
        //}

        //public DataTable saveCountry(MasterCountry mc)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("countryId", mc.countryId);
        //    hs.Add("countryname", mc.countryname);
        //    hs.Add("Create_date", mc.Create_date);
        //    hs.Add("Create_Time", mc.Create_Time);
        //    hs.Add("Created_by", mc.Created_by);
        //    hs.Add("Device_name", mc.Device_name);
        //    hs.Add("Modified_Date", mc.Modified_Date);
        //    hs.Add("Modified_Time", mc.Modified_Time);
        //    hs.Add("Modified_by", mc.Modified_by);
        //    hs.Add("M_Device_name", mc.M_Device_name);
        //    dt = config.ReturnScalar("spSaveCountry", hs);
        //    return dt;
        //}
        public MasterCountry GetcountryIdBycountryname(string country)
        {
            MasterCountry mc = new MasterCountry();
            string sql = "Select * from  Master_Country where countryname='" + country + "' order by countryId";
            config.singleResult(sql);

            ccode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.countryId = Convert.ToString(dr["countryId"]);

                }

            }
            return mc;

        }
    }
}