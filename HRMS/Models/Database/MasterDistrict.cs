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
        public String StateId { get; set; }
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
        public string StateName { get; set; }

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
        //public string UpdateDistrict(MasterDistrict md)
        //{
        //    string sql = "select * from master_district where districtname ='" + md.districtname + "' and stateid='" + md.StateId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        md.msg = "Same District Already Exist Under Same State";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            config.Update("Master_District", new Dictionary<String, object>()
        //                {
        //                { "districtname",     md.districtname},
        //                { "StateId",       md.StateId},
        //                { "Modified_By",   md.Modified_by},
        //                { "Modified_Date", md.Modified_Date},
        //                { "Modified_Time", md.Modified_Time},
        //                { "M_Device_Name", md.M_Device_name},
        //                }, new Dictionary<string, object>()
        //                {
        //                { "districtid", md.districtid },
        //                });
        //            md.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            md.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return md.msg;
        //}
        //public DataTable UpdateDistrict(MasterDistrict md)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    string sql = "select * from master_district where districtname ='" + md.districtname + "' and stateid='" + md.StateId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        md.msg = "Same District Already Exist Under Same State";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            {
        //                hs.Add("districtname", md.districtname);
        //                hs.Add("StateId", md.StateId);
        //                hs.Add("Modified_By", md.Modified_by);
        //                hs.Add("Modified_Date", md.Modified_Date);
        //                hs.Add("Modified_Time", md.Modified_Time);
        //                hs.Add("M_Device_Name", md.M_Device_name);
        //            }
        //            {
        //                hs.Add("districtid", md.districtid);
        //            }
        //            dt = config.ReturnScalar("spUpdateDistrict", hs);
        //            md.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            md.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return dt;
        //}
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
                  { "districtid", md.districtid }
                });
                md.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                md.msg = "Deletion Not Completed Succesfuly";
            }
            return md.msg;
        }

        public MasterDistrict getDistrictDetailsBydistrictid(string Districtid)
        {
            MasterDistrict md = new MasterDistrict();
            string sql = "select * from master_district a,master_State b where a.StateId=b.StateId and a.districtid='" + Districtid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtid = Convert.ToString(dr["districtid"]);
                    md.districtname = Convert.ToString(dr["districtname"]);
                    md.StateId = Convert.ToString(dr["StateId"]);
                }
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
                    md.StateId = Convert.ToString(dr["StateId"]);
                    md.StateName = Convert.ToString(dr["StateName"]);
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
        //public string saveDistrict(MasterDistrict did)
        //{
        //    config.Insert("Master_District", new Dictionary<string, object>()
        //    {
        //        { "districtid", did.districtid},
        //        { "districtname", did.districtname},
        //        { "StateId", did.StateId},
        //        { "Created_by",did.Created_by},
        //        { "Create_date", did.Create_date},
        //        { "Create_Time", did.Create_Time},
        //        { "Modified_by", did.Modified_by},
        //        { "Modified_Date", did.Modified_Date},
        //        { "Modified_Time", did.Modified_Time},
        //        { "Device_name", did.Device_name},
        //        { "M_Device_name", did.M_Device_name},
        //    });
        //    did.msg = "Saved Successfully";
        //    return did.msg;


        //}

        //public DataTable saveDistrict(MasterDistrict md)
        //{

        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("districtid", md.districtid);
        //    hs.Add("districtname", md.districtname);
        //    hs.Add("StateId", md.StateId);
        //    hs.Add("Created_by", md.Created_by);
        //    hs.Add("Create_date", md.Create_date);
        //    hs.Add("Create_Time", md.Create_Time);
        //    hs.Add("Device_name", md.Device_name);
        //    hs.Add("Modified_by", md.Modified_by);
        //    hs.Add("Modified_Date", md.Modified_Date);
        //    hs.Add("Modified_Time", md.Modified_Time);
        //    hs.Add("M_Device_name", md.M_Device_name);
        //    dt = config.ReturnScalar("spSaveDistrict", hs);
        //    return dt;



        //}
        public MasterDistrict GetdistrictidBydistrictname(string district)
        {
            MasterDistrict md = new MasterDistrict();
            string sql = "Select * from  Master_District where districtname='" + district+ "' order by districtid";
            config.singleResult(sql);

            dcode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtid = Convert.ToString(dr["districtid"]);

                }

            }
            return md;

        }

        

        public MasterDistrict Checkdistrictname(string district, string stateid)
        {
            string sql = "Select * from  Master_District where stateid='" + stateid + "' order by StateId";
            config.singleResult(sql);

            MasterDistrict md = new MasterDistrict();

            md.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtname = Convert.ToString(dr["districtname"]);
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

        public MasterDistrict GetdistrictnameBydistrictid(string Districtid)
        {
            string sql = "Select * from  Master_District where districtid='" + Districtid + "' order by districtid";
            config.singleResult(sql);

            MasterDistrict md = new MasterDistrict();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    md.districtname = Convert.ToString(dr["districtname"]);

                }

            }

            return md;
        }
    }
}