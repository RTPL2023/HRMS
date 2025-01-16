using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using HRMS.Includes;

namespace HRMS.Models.DataBase
{
    public class MasterCity
    {
       
        SQLConfig config = new SQLConfig();
        public String CityId { get; set; }
        public String CitySubId { get; set; }
        public String CityName { get; set; }
        public String DistrictId { get; set; }
        public String DistrictName { get; set; }
        public String StateId { get; set; }
        public String StateName { get; set; }
        public String CountryId { get; set; }
        public String CountryName { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public  String Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int codenum { get; set; }
        public string msg { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public int acode { get; set; }
        public bool checkdata { get; set; }

        //public List<MasterCity> GetCityByDistrictId(string distid,string db)
        //{
        //    string sql = "Select * from  Master_City where districtid='" + distid + "' order by CityId";
        //    config.singleResult(sql);

        //    List<MasterCity> mcl = new List<MasterCity>();
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            MasterCity mc = new MasterCity();
        //            mc.Value = Convert.ToString(dr["CityId"]);
        //            mc.Text = Convert.ToString(dr["CityName"]);
        //            mcl.Add(mc);
        //        }
        //    }
        //    return mcl;
        //}
        public List<MasterCity> GetCityByDistrictId(string distid)
        {
        
            string sql = "Select * from  Master_City where districtid='" + distid + "' order by CityId";
            config.singleResult(sql);
            
            List<MasterCity> mcl = new List<MasterCity>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity mc = new MasterCity();
                    mc.Value = Convert.ToString(dr["CityId"]);
                    mc.Text = Convert.ToString(dr["CityName"]);
                    mcl.Add(mc);
                }
            }
            return mcl;
        }


        public MasterCity CheckAllCityDeletedOrNot(string id)
        {
            string sql = "Select * from  Master_City where Districtid='" + id + "' and is_deleted=0";
            config.singleResult(sql);
            MasterCity mc = new MasterCity();
            mc.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                mc.checkdata = true;
            }
            return mc;
        }
        public MasterCity getCityDetailsByCityId(string cityid)
        {
            MasterCity mc = new MasterCity();
            string sql = "select * from master_city a,master_district b where a.DistrictId=b.DistrictId and a.CityId='" + cityid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.CityId = Convert.ToString(dr["CityId"]);
                    mc.CityName = Convert.ToString(dr["CityName"]);
                    mc.DistrictId = Convert.ToString(dr["DistrictId"]);
                }
            }
            return mc;
        }


        public List<MasterCity> getAllCityList()
        {
            string sql = "select * from Master_City mc,master_district md where mc.is_deleted<>1 and mc.DistrictId=md.DistrictId order by CityName";
            config.singleResult(sql);

            List<MasterCity> mcl = new List<MasterCity>();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity ms = new MasterCity();
                    ms.CityId = Convert.ToString(dr["CityId"]);
                    ms.CityName = Convert.ToString(dr["CityName"]);
                    ms.DistrictId = Convert.ToString(dr["DistrictId"]);
                    ms.DistrictName = Convert.ToString(dr["DistrictName"]);
                    mcl.Add(ms);
                }
            }
            return mcl;
        }

        //public string UpdateCity(MasterCity mc)
        //{
        //    string sql = "select * from master_city where CityName ='" + mc.CityName + "' and DistrictId='" + mc.DistrictId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        mc.msg = "Same City Already Exist Under Same District";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            config.Update("Master_City", new Dictionary<String, object>()
        //                {
        //                { "CityName",      mc.CityName},
        //                { "DistrictId",    mc.DistrictId},
        //                { "Modified_By",   mc.Modified_by},
        //                { "Modified_Date", mc.Modified_Date},
        //                { "Modified_Time", mc.Modified_Time},
        //                { "M_Device_Name", mc.M_Device_name},
        //                }, new Dictionary<string, object>()
        //                {
        //                { "CityId", mc.CityId },
        //                });
        //            mc.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            mc.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return mc.msg;
        //}

        //public DataTable UpdateCity(MasterCity mc)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    string sql = "select * from master_city where CityName ='" + mc.CityName + "' and DistrictId='" + mc.DistrictId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        mc.msg = "Same City Already Exist Under Same District";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            {
        //                hs.Add("CityName", mc.CityName);
        //                hs.Add("DistrictId", mc.DistrictId);
        //                hs.Add("Modified_By", mc.Modified_by);
        //                hs.Add("Modified_Date", mc.Modified_Date);
        //                hs.Add("Modified_Time", mc.Modified_Time);
        //                hs.Add("M_Device_Name", mc.M_Device_name);
        //            }
        //            {
        //                hs.Add("CityId", mc.CityId);
        //            }
        //            dt = config.singleResult("spUpdateCity", hs);
        //            mc.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            mc.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return dt;
        //}

        public string DeleteCity(MasterCity mc)
        {
            try
            {
                config.Update("Master_City", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   mc.Modified_by},
                    { "Modified_Date", mc.Modified_Date},
                    { "Modified_Time", mc.Modified_Time},
                    { "M_Device_Name", mc.M_Device_name},

                }, new Dictionary<string, object>()
                  {
                  { "CityId", mc.CityId }
                });
                mc.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                mc.msg = "Deletion Not Completed Succesfuly";
            }
            return mc.msg;
        }
        public MasterCity getcityId(MasterCity mc)
        {
            string sql = "Select * from Master_City order by CityId";

            config.singleResult(sql);

            codenum = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {


                    mc.CityId = Convert.ToString(dr["CityId"]);
                    string subcode = (Convert.ToString(dr["CityId"])).Substring(0, 1);
                    if (mc.CitySubId == subcode)
                    {
                        //codenum = Convert.ToInt32(mc.CityId.Substring(5)) + 1;
                        codenum = codenum + 1;
                    }

                }

            }
            return mc;

        }

        public MasterCity CheckCityName(string City, string districtid)
        {
            string sql = "Select * from  Master_City where districtid='" + districtid + "' order by districtid";
            config.singleResult(sql);

            MasterCity mc = new MasterCity();

            mc.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.CityName = Convert.ToString(dr["CityName"]);
                    if (mc.CityName == City)
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

        //public string saveCity(MasterCity mc)
        //{
        //    config.Insert("Master_City", new Dictionary<string, object>()
        //        {
        //        { "CityId", mc.CityId},
        //        { "CityName", mc.CityName},
        //       { "DistrictId", mc.DistrictId},
        //        { "Created_by", mc.Created_by},
        //        { "Create_date", mc.Create_date},
        //        { "Create_Time", mc.Create_Time},
        //        { "Modified_by", mc.Modified_by},
        //        { "Modified_Date", mc.Modified_Date},
        //        { "Modified_Time", mc.Modified_Time},
        //        { "Device_name", mc.Device_name},
        //        { "M_Device_name", mc.M_Device_name}
        //    });
        //    mc.msg = "Saved Successfully";
        //    return mc.msg;
        //}

        //public DataTable saveCity(MasterCity mc)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("CityId", mc.CityId);
        //    hs.Add("CityName", mc.CityName);
        //    hs.Add("DistrictId", mc.DistrictId);
        //    hs.Add("Created_by", mc.Created_by);
        //    hs.Add("Create_date", mc.Create_date);
        //    hs.Add("Create_Time", mc.Create_Time);
        //    hs.Add("Modified_by", mc.Modified_by);
        //    hs.Add("Modified_Date", mc.Modified_Date);
        //    hs.Add("Modified_Time", mc.Modified_Time);
        //    hs.Add("Device_name", mc.Device_name);
        //    hs.Add("M_Device_name", mc.M_Device_name);
        //    dt = config.singleResult("spSaveCity", hs);
        //    return dt;
        //}

        public MasterCity GetCityIdByCityName(string city)
        {
            MasterCity mc = new MasterCity();
            string sql = "Select * from  Master_City where CityName='" + city + "' order by CityId";
            config.singleResult(sql);

            acode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.CityId = Convert.ToString(dr["CityId"]);

                }

            }
            return mc;

        }

        public List<MasterCity> getCityMast()
        {
            string sql = "Select * from  Master_City where is_deleted<>1 order by CityName";
            config.singleResult(sql);

            List<MasterCity> mcl = new List<MasterCity>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity mc = new MasterCity();
                    mc.CityId = Convert.ToString(dr["CityId"]);
                    mc.CityName = Convert.ToString(dr["CityName"]);
                    mcl.Add(mc);
                }

            }

            return mcl;
        }
        public MasterCity getDistrictStateCountryByCityId(string cityid)
        {
            MasterCity mc = new MasterCity();
            string sql = "Select a.cityid,a.cityname,a.districtid,b.districtid,b.districtname,b.stateid,c.stateid,c.statename,c.countryid,d.countryid,d.countryname from  Master_City a,master_district b,master_state c, master_country d where a.districtid=b.districtid and b.stateid=c.stateid and c.countryid=d.countryid and a.cityid = '" + cityid + "'";
            config.singleResult(sql);

           
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.DistrictName = Convert.ToString(dr["districtName"]);
                    mc.StateName = Convert.ToString(dr["StateName"]);
                    mc.CountryName = Convert.ToString(dr["CountryName"]);
                }
            }
            return mc;
        }

        public MasterCity GetCityNameByCityId(string CityId)
        {
            string sql = "Select * from  Master_City where cityid='" + CityId + "' order by cityid";
            config.singleResult(sql);

            MasterCity mc = new MasterCity();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.CityName = Convert.ToString(dr["CityName"]);

                }

            }

            return mc;
        }
    }
}