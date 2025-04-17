using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using HRMS.Includes;

namespace HRMS.Models.DataBase
{
    public class MasterDistrict
    {
        SQLConfig config = new SQLConfig();
        public String districtid { get; set; }
        public String DistrictSubId { get; set; }
        public String districtname { get; set; }
        public String stateid { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public string Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public int dcode { get; set; }
        public string msg { get; set; }
        public bool checkdata { get; set; }
        public string statename { get; set; }

        public MasterDistrict CheckAllDistrictDeletedOrNot(string id)
        {
            string sql = "Select * from  Master_District where Stateid='" + id + "' and is_deleted=0";
            config.singleResult(sql);
            MasterDistrict md = new MasterDistrict();
            md.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                md.checkdata = true;
            }
            return md;
        }
        
      
        public List<MasterDistrict> getAllDistrictList()
        {
            string sql = "select * from master_district md,master_state ms where md.is_deleted<>1 and md.StateId=ms.stateid order by districtname";
            config.singleResult(sql);

            List<MasterDistrict> mdl = new List<MasterDistrict>();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterDistrict md = new MasterDistrict();
                    md.districtid = Convert.ToString(dr["districtid"]);
                    md.districtname = Convert.ToString(dr["districtname"]);
                    md.stateid = Convert.ToString(dr["StateId"]);
                    md.statename = Convert.ToString(dr["StateName"]);
                    mdl.Add(md);
                }
            }
            return mdl;
        }
        public MasterDistrict getdistrictid(MasterDistrict did)
        {
            string sql = "Select * from  Master_District order by districtid";
            config.singleResult(sql);

            dcode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    did.districtid = Convert.ToString(dr["districtid"]);
                    string subcode = Convert.ToString(dr["districtid"]).Substring(0,1);
                    if (did.DistrictSubId == subcode)
                    {
                        //dcode = Convert.ToInt32(did.districtid.Substring(5)) + 1;
                        dcode = dcode + 1;
                    }
                }
            }
            return did;
        }
     
        public List<MasterDistrict> getDistrictMast()
        {
            string sql = "Select * from  Master_District where is_Deleted<>1 order by districtname";
            config.singleResult(sql);

            List<MasterDistrict> mdl = new List<MasterDistrict>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterDistrict md = new MasterDistrict();
                    md.districtid = Convert.ToString(dr["districtid"]);
                    md.districtname = Convert.ToString(dr["districtname"]);
                    mdl.Add(md);
                }

            }

            return mdl;
        }
        public MasterDistrict CheckDistrictName(string district, string stateid)
        {
            string sql = "Select * from  Master_District where stateid='" + stateid + "' order by StateId";
            config.singleResult(sql);

            MasterDistrict md = new MasterDistrict();

            md.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtname = Convert.ToString(dr["DistrictName"]);
                    if (md.districtname == district)
                    {
                        md.checkdata = true;
                        return md;
                    }
                    else
                    {
                        md.checkdata = false;
                    }
                }

            }

            return md;
        }
        public string saveDistrict(MasterDistrict did)
        {
            config.Insert("Master_District", new Dictionary<string, object>()
            {
                { "DistrictId", did.districtid},
                { "DistrictName", did.districtname},
                { "StateId", did.stateid},
                { "Created_by",did.Created_by},
                { "Create_date", did.Create_date},
                { "Create_Time", did.Create_Time},
                { "Device_name", did.Device_name},
                
            });
            did.msg = "Saved Successfully";
            return did.msg;


        }
        public string UpdateDistrict(MasterDistrict md)
        {
            string sql = "select * from master_district where DistrictName ='" + md.districtname + "' and stateid='" + md.stateid + "'";
            config.singleResult(sql);
            int check = 0;
            if (config.dt.Rows.Count > 0)
            {
                check = 1;
                md.msg = "Same District Already Exist Under Same State";
            }
            if (check == 0)
            {
                try
                {
                    config.Update("Master_District", new Dictionary<String, object>()
                        {
                        { "DistrictName",     md.districtname},
                        { "StateId",       md.stateid},
                        { "Modified_By",   md.Modified_by},
                        { "Modified_Date", md.Modified_Date},
                        { "Modified_Time", md.Modified_Time},
                        { "M_Device_Name", md.M_Device_name},
                        }, new Dictionary<string, object>()
                        {
                        { "DistrictId", md.districtid },
                        });
                    md.msg = "Updated Successfuly";
                }
                catch (Exception ex)
                {
                    md.msg = "Updation Not Completed Succesfuly";
                }
            }
            return md.msg;
        }
        public string DeleteDistrict(MasterDistrict md)
        {
            try
            {
                config.Update("Master_District", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   md.Modified_by},
                    { "Modified_Date", md.Modified_Date},
                    { "Modified_Time", md.Modified_Time},
                    { "M_Device_Name", md.M_Device_name},

                }, new Dictionary<string, object>()
                  {
                  { "DistrictId", md.districtid }
                });
                md.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                md.msg = "Deletion Not Completed Succesfuly";
            }
            return md.msg;
        }
        public MasterDistrict GetDistrictNameByDistrictId(string Districtid)
        {
            string sql = "Select * from  Master_District where districtid='" + Districtid + "' order by districtid";
            config.singleResult(sql);

            MasterDistrict md = new MasterDistrict();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtname = Convert.ToString(dr["DistrictName"]);

                }

            }

            return md;
        }
    }
}