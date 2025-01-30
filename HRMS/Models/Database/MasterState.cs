using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using HRMS.Includes;

namespace HRMS.Models.DataBase
{
    public class MasterState
    {
        SQLConfig config = new SQLConfig();
        public String stateid { get; set; }
        public String StateSubId { get; set; }
        public String statename { get; set; }
        public String CountryId { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public string Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; }
        public bool checkdata { get; set; }
        public int scode { get; set; }
        public string msg { get; set; }
        public string CountryName { get; set; }


        public MasterState CheckAllStateDeletedOrNot(string id)
        {
            string sql = "Select * from  Master_State where countryid='" + id + "' and is_deleted=0";
            config.singleResult(sql);

            MasterState ms = new MasterState();
            ms.checkdata = false;


            if (config.dt.Rows.Count > 0)
            {
                ms.checkdata = true;
            }
            return ms;
        }

        public MasterState getStateDetailsBystateid(string stateid)
        {
            MasterState ms = new MasterState();
            string sql = "select * from master_State a,master_country b where a.CountryId=b.CountryId and a.stateid='" + stateid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.stateid = Convert.ToString(dr["stateid"]);
                    ms.statename = Convert.ToString(dr["statename"]);
                    ms.CountryId = Convert.ToString(dr["CountryId"]);
                }
            }
            return ms;
        }

        //public string UpdateState(MasterState ms)
        //{
        //    string sql = "select * from master_state where statename='" + ms.statename + "' and countryid='" + ms.CountryId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        ms.msg = "Same State Already Exist Under Same Country";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            config.Update("Master_State", new Dictionary<String, object>()
        //                {
        //                { "statename",     ms.statename},
        //                { "CountryId",     ms.CountryId},
        //                { "Modified_By",   ms.Modified_by},
        //                { "Modified_Date", ms.Modified_Date},
        //                { "Modified_Time", ms.Modified_Time},
        //                { "M_Device_Name", ms.M_Device_name},

        //                }, new Dictionary<string, object>()
        //                {
        //                { "stateid", ms.stateid },
        //                });
        //            ms.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            ms.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }

        //    return ms.msg;
        //}
        //public DataTable UpdateState(MasterState ms)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    string sql = "select * from master_state where statename='" + ms.statename + "' and countryid='" + ms.CountryId + "'";
        //    config.singleResult(sql);
        //    int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        check = 1;
        //        ms.msg = "Same State Already Exist Under Same Country";
        //    }
        //    if (check == 0)
        //    {
        //        try
        //        {
        //            {
        //                hs.Add("statename", ms.statename);
        //                hs.Add("CountryId", ms.CountryId);
        //                hs.Add("Modified_By", ms.Modified_by);
        //                hs.Add("Modified_Date", ms.Modified_Date);
        //                hs.Add("Modified_Time", ms.Modified_Time);
        //                hs.Add("M_Device_Name", ms.M_Device_name);
        //            }
        //            {
        //                hs.Add("stateid", ms.stateid);
        //            }
        //            dt = config.ReturnScalar("spUpdateState", hs);
        //            ms.msg = "Updated Successfuly";
        //        }
        //        catch (Exception ex)
        //        {
        //            ms.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    return dt;
        //}
        public string DeleteState(MasterState ms)
        {
            try
            {
                config.Update("Master_State", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   ms.Modified_by},
                    { "Modified_Date", ms.Modified_Date},
                    { "Modified_Time", ms.Modified_Time},
                    { "M_Device_Name", ms.M_Device_name},

                }, new Dictionary<string, object>()
                  {
                  { "stateid", ms.stateid }
                });
                ms.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                ms.msg = "Deletion Not Completed Succesfuly";
            }
            return ms.msg;
        }
        public List<MasterState> getAllStateList()
        {
            string sql = "select * from master_State ms,master_country mc where ms.is_deleted<>1 and ms.countryid=mc.countryid order by statename";
            config.singleResult(sql);
            List<MasterState> msl = new List<MasterState>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterState ms = new MasterState();
                    ms.stateid = Convert.ToString(dr["stateid"]);
                    ms.statename = Convert.ToString(dr["statename"]);
                    ms.CountryName = Convert.ToString(dr["CountryName"]);
                    msl.Add(ms);
                }
            }
            return msl;
        }
        public MasterState getstateid(MasterState sid)
        {
            string sql = "Select * from  Master_State order by stateid";
            config.singleResult(sql);

            scode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    sid.stateid = Convert.ToString(dr["stateid"]);
                    string subcode = Convert.ToString(dr["stateid"]).Substring(0, 1);
                    if (sid.StateSubId == subcode)
                    {
                        //scode = Convert.ToInt32(sid.stateid.Substring(5)) + 1;
                        scode = scode + 1;
                    }
                }

            }

            return sid;
        }

        public MasterState Checkstatename(string state,string countryid)
        {
            string sql = "Select * from  Master_state where countryid='"+ countryid + "' order by stateid";
            config.singleResult(sql);

            MasterState ms = new MasterState();

            ms.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.statename = Convert.ToString(dr["statename"]);
                    if (ms.statename == state)
                    {
                        ms.checkdata = true;
                        return ms;
                    }
                    else
                    {
                        ms.checkdata = false;
                    }
                }

            }

            return ms;
        }

        public List<MasterState> getStateMast()
        {
            string sql = "Select * from  Master_State a where is_deleted<>1 order by statename";
            config.singleResult(sql);

            List<MasterState> msl = new List<MasterState>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterState ms = new MasterState();
                    ms.stateid = Convert.ToString(dr["stateid"]);
                    ms.statename = Convert.ToString(dr["statename"]);
                    msl.Add(ms);
                }

            }

            return msl;
        }

        //public string savestate(MasterState sid)
        //{
        //    config.Insert("Master_State", new Dictionary<string, object>()
        //    {
        //        { "stateid", sid.stateid},
        //        { "statename", sid.statename},
        //        { "CountryId",sid. CountryId},
        //        { "Created_by",sid.Created_by},
        //        { "Create_date", sid.Create_date},
        //        { "Create_Time", sid.Create_Time},
        //        { "Modified_by", sid.Modified_by},
        //        { "Modified_Date", sid.Modified_Date},
        //        { "Modified_Time", sid.Modified_Time},
        //        { "Device_name", sid.Device_name},
        //        { "M_Device_name", sid.M_Device_name},
        //               });
        //    sid.msg = "Saved Successfully";
        //    return sid.msg;


        //}

        //public DataTable savestate(MasterState ms)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("stateid", ms.stateid);
        //    hs.Add("statename", ms.statename);
        //    hs.Add("CountryId", ms.CountryId);
        //    hs.Add("Created_by", ms.Created_by);
        //    hs.Add("Create_date", ms.Create_date);
        //    hs.Add("Create_Time", ms.Create_Time);
        //    hs.Add("Modified_by", ms.Modified_by);
        //    hs.Add("Modified_Date", ms.Modified_Date);
        //    hs.Add("Modified_Time", ms.Modified_Time);
        //    hs.Add("Device_name", ms.Device_name);
        //    hs.Add("M_Device_name", ms.M_Device_name);
        //    dt = config.ReturnScalar("spSavestate", hs);
        //    return dt;
        //}
        public  MasterState GetstateidBystatename(string state)
        {
            MasterState ms = new MasterState();
            string sql = "Select * from  Master_State where statename='"+ state + "' order by stateid";
            config.singleResult(sql);

            scode = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.stateid = Convert.ToString(dr["stateid"]);
                    
                }

            }
            return ms;
        }

        public MasterState GetstatenameBystateid(string stateid)
        {
            string sql = "Select * from  Master_State where stateid='" + stateid + "' order by stateid";
            config.singleResult(sql);

            MasterState ms = new MasterState();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.statename = Convert.ToString(dr["statename"]);

                }

            }

            return ms;
        }
    }
}
