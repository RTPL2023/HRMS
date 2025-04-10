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
        public String countryid { get; set; }
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

        public string savestate(MasterState sid)
        {
            config.Insert("Master_State", new Dictionary<string, object>()
            {
                { "StateId", sid.stateid},
                { "StateName", sid.statename},
                { "countryid",sid.countryid},
                { "Created_by",sid.Created_by},
                { "Create_date", sid.Create_date},
                { "Create_Time", sid.Create_Time},
                { "Modified_by", sid.Modified_by},
                { "Modified_Date", sid.Modified_Date},
                { "Modified_Time", sid.Modified_Time},
                { "Device_name", sid.Device_name},
                { "M_Device_name", sid.M_Device_name},
                       });
            sid.msg = "Saved Successfully";
            return sid.msg;


        }
        public string UpdateState(MasterState ms)
        {
            DataTable dt = new DataTable();
            Hashtable hs = new Hashtable();
            string sql = "select * from master_state where StateName='" + ms.statename + "' and countryid='" + ms.countryid + "'";
            config.singleResult(sql);
            int check = 0;
            if (config.dt.Rows.Count > 0)
            {
                check = 1;
                ms.msg = "Same State Already Exist Under Same Country";
            }
            if (check == 0)
            {
                try
                {
                    config.Update("master_state", new Dictionary<String, object>()
                    {
                    { "StateName",   ms.statename},
                    { "countryid",   ms.countryid},
                    { "Modified_By",   ms.Modified_by},
                    { "Modified_Date", ms.Modified_Date},
                    { "Modified_Time", ms.Modified_Time},
                    { "M_Device_Name", ms.M_Device_name},

                    }, new Dictionary<string, object>()
                    {
                        { "StateId", ms.stateid }
                    });
          
                    ms.msg = "Updated Successfuly";
                }
                catch (Exception ex)
                {
                    ms.msg = "Updation Not Completed Succesfuly"+ex;
                }
            }
            return ms.msg;
        }
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

        public MasterState CheckStateName(string state, string countryid)
        {
            string sql = "Select * from  Master_state where countryid='" + countryid + "' order by StateId";
            config.singleResult(sql);

            MasterState ms = new MasterState();

            ms.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.statename = Convert.ToString(dr["StateName"]);
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
                    ms.countryid = Convert.ToString(dr["countryid"]);
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
                  { "StateId", ms.stateid }
                });
                ms.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                ms.msg = "Deletion Not Completed Succesfuly";
            }
            return ms.msg;
        }

        public MasterState GetStateNameByStateId(string stateid)
        {
            string sql = "Select * from  Master_State where stateid='" + stateid + "' order by StateId";
            config.singleResult(sql);

            MasterState ms = new MasterState();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ms.statename = Convert.ToString(dr["StateName"]);

                }

            }

            return ms;
        }
    }
}
