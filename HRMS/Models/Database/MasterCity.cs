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
        public String cityid { get; set; }
        public String CitySubId { get; set; }
        public String cityname { get; set; }
        public String districtid { get; set; }
        public String districtname { get; set; }
        public String stateid { get; set; }
        public String statename { get; set; }
        public String countryid { get; set; }
        public String countryname { get; set; }
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
        //    string sql = "Select * from  Master_City where districtid='" + distid + "' order by cityid";
        //    config.singleResult(sql);

        //    List<MasterCity> mcl = new List<MasterCity>();
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            MasterCity mc = new MasterCity();
        //            mc.Value = Convert.ToString(dr["cityid"]);
        //            mc.Text = Convert.ToString(dr["cityname"]);
        //            mcl.Add(mc);
        //        }
        //    }
        //    return mcl;
        //}
        public List<MasterCity> GetCityByDistrictId(string distid)
        {
        
            string sql = "Select * from  Master_City where districtid='" + distid + "' order by cityid";
            config.singleResult(sql);
            
            List<MasterCity> mcl = new List<MasterCity>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity mc = new MasterCity();
                    mc.Value = Convert.ToString(dr["cityid"]);
                    mc.Text = Convert.ToString(dr["cityname"]);
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
        public MasterCity getCityDetailsBycityid(string cityid)
        {
            MasterCity mc = new MasterCity();
            string sql = "select * from master_city a,master_district b where a.DistrictId=b.DistrictId and a.cityid='" + cityid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.cityid = Convert.ToString(dr["cityid"]);
                    mc.cityname = Convert.ToString(dr["cityname"]);
                    mc.districtid = Convert.ToString(dr["DistrictId"]);
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
                    mc.cityname = Convert.ToString(dr["CityName"]);
                    if (mc.cityname == City)
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
        public List<MasterCity> getAllCityList()
        {
            string sql = "select * from Master_City mc,master_district md where mc.is_deleted<>1 and mc.DistrictId=md.DistrictId order by cityname";
            config.singleResult(sql);

            List<MasterCity> mcl = new List<MasterCity>();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity ms = new MasterCity();
                    ms.cityid = Convert.ToString(dr["cityid"]);
                    ms.cityname = Convert.ToString(dr["cityname"]);
                    ms.districtid = Convert.ToString(dr["DistrictId"]);
                    ms.districtname = Convert.ToString(dr["DistrictName"]);
                    mcl.Add(ms);
                }
            }
            return mcl;
        }

        public string UpdateCity(MasterCity mc)
        {
            string sql = "select * from master_city where cityname ='" + mc.cityname + "' and DistrictId='" + mc.districtid + "'";
            config.singleResult(sql);
            int check = 0;
            if (config.dt.Rows.Count > 0)
            {
                check = 1;
                mc.msg = "Same City Already Exist Under Same District";
            }
            if (check == 0)
            {
                try
                {
                    config.Update("Master_City", new Dictionary<String, object>()
                        {
                        { "cityname",      mc.cityname},
                        { "DistrictId",    mc.districtid},
                        { "Modified_By",   mc.Modified_by},
                        { "Modified_Date", mc.Modified_Date},
                        { "Modified_Time", mc.Modified_Time},
                        { "M_Device_Name", mc.M_Device_name},
                        }, new Dictionary<string, object>()
                        {
                        { "cityid", mc.cityid },
                        });
                    mc.msg = "Updated Successfuly";
                }
                catch (Exception ex)
                {
                    mc.msg = "Updation Not Completed Succesfuly";
                }
            }
            return mc.msg;
        }

       

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
                  { "cityid", mc.cityid }
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
            string sql = "Select * from Master_City order by cityid";

            config.singleResult(sql);

            codenum = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {


                    mc.cityid = Convert.ToString(dr["cityid"]);
                    string subcode = (Convert.ToString(dr["cityid"])).Substring(0, 1);
                    if (mc.CitySubId == subcode)
                    {
                        //codenum = Convert.ToInt32(mc.cityid.Substring(5)) + 1;
                        codenum = codenum + 1;
                    }

                }

            }
            return mc;

        }

        public MasterCity Checkcityname(string City, string districtid)
        {
            string sql = "Select * from  Master_City where districtid='" + districtid + "' order by districtid";
            config.singleResult(sql);

            MasterCity mc = new MasterCity();

            mc.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.cityname = Convert.ToString(dr["cityname"]);
                    if (mc.cityname == City)
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

        public string saveCity(MasterCity mc)
        {
            config.Insert("Master_City", new Dictionary<string, object>()
                {
                { "cityid", mc.cityid},
                { "cityname", mc.cityname},
                { "DistrictId", mc.districtid},
                { "Created_by", mc.Created_by},
                { "Create_date", mc.Create_date},
                { "Create_Time", mc.Create_Time},
                { "Modified_by", mc.Modified_by},
                { "Modified_Date", mc.Modified_Date},
                { "Modified_Time", mc.Modified_Time},
                { "Device_name", mc.Device_name},
                { "M_Device_name", mc.M_Device_name}
            });
            mc.msg = "Saved Successfully";
            return mc.msg;
        }

       

        public List<MasterCity> getCityMast()
        {
            string sql = "Select * from  Master_City where is_deleted<>1 order by cityname";
            config.singleResult(sql);

            List<MasterCity> mcl = new List<MasterCity>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterCity mc = new MasterCity();
                    mc.cityid = Convert.ToString(dr["cityid"]);
                    mc.cityname = Convert.ToString(dr["cityname"]);
                    mcl.Add(mc);
                }

            }

            return mcl;
        }
        public MasterCity getDistrictStateCountryBycityid(string cityid)
        {
            MasterCity mc = new MasterCity();
            string sql = "Select a.cityid,a.cityname,a.districtid,b.districtid,b.districtname,b.stateid,c.stateid,c.statename,c.countryid,d.countryid,d.countryname from  Master_City a,master_district b,master_state c, master_country d where a.districtid=b.districtid and b.stateid=c.stateid and c.countryid=d.countryid and a.cityid = '" + cityid + "'";
            config.singleResult(sql);

           
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.districtname = Convert.ToString(dr["districtName"]);
                    mc.statename = Convert.ToString(dr["StateName"]);
                    mc.countryname = Convert.ToString(dr["CountryName"]);
                }
            }
            return mc;
        }

        public MasterCity GetcitynameBycityid(string cityid)
        {
            string sql = "Select * from  Master_City where cityid='" + cityid + "' order by cityid";
            config.singleResult(sql);

            MasterCity mc = new MasterCity();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mc.cityname = Convert.ToString(dr["cityname"]);

                }

            }

            return mc;
        }
    }
}