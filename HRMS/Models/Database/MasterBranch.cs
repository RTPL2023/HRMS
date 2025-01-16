using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using HRMS.Includes;

namespace HRMS.Models.DataBase
{
    public class MasterBranch
    {

        SQLConfig config = new SQLConfig();
        public String branchId { get; set; }
        public String BranchSubId { get; set; }
        public String branchName { get; set; }
        public String Address { get; set; }
        public String ContactNo { get; set; }
        public String EmailId { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public String Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; } 
        public String Country_Id { get; set; }
        public String Br_Country_Name { get; set; }
        public String State_Id { get; set; }
        public String Br_State_Name { get; set; }
        public String District_Id { get; set; }
        public String Br_District_Name { get; set; }
        public String City_Id { get; set; }
        public String Br_City_Name { get; set; }
        public String Gst_In { get; set; }
        public String Pin_code { get; set; }
        public int codenum { get; set; }
        public string msg { get; set; }
        public int acode { get; set; }
        public bool checkdata { get; set; }

        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }

        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Inv_Code { get; set; }


        public List<MasterBranch> Getbranchlists()
        {
            string sql = "Select * from  Master_Branch order by branchId";
            config.singleResult(sql);
            List<MasterBranch> dml = new List<MasterBranch>();
            if (config.dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 0)
                    {
                        MasterBranch dm1 = new MasterBranch();
                        dm1.Value = Convert.ToString(0);
                        dm1.Text = Convert.ToString("Select Branch");
                        dml.Add(dm1);
                    }
                    MasterBranch dm = new MasterBranch();
                    dm.Value = Convert.ToString(dr["branchId"]);
                    dm.Text = Convert.ToString(dr["branchName"]);
                    dml.Add(dm);
                    i = i + 1;
                }
            }
            else
            {
                MasterBranch dm = new MasterBranch();
                dm.Value = Convert.ToString(0);
                dm.Text = Convert.ToString("Select Branch");
                dml.Add(dm);
            }
            return dml;
        }
        public string GetFrombranchNameById(string branchId)
        {
            string Fr_Branch_Name = string.Empty;
            string sql = "SELECT * from Master_Branch WHERE branchId = '" + branchId + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Fr_Branch_Name = Convert.ToString(dr["branchName"]);
                }
            }
            return Fr_Branch_Name;
        }
        public string GetTobranchNameById(string branchId)
        {
            string To_Branch_Name = string.Empty;
            string sql = "SELECT * from Master_Branch WHERE branchId = '" + branchId + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    To_Branch_Name = Convert.ToString(dr["branchName"]);
                }
            }
            return To_Branch_Name;
        }

        public MasterBranch CheckAllBranchesDeletedOrNot(string id)
        {
            string sql = "Select * from  Master_Branch where city_id='" + id + "' and is_deleted=0";
            config.singleResult(sql);            
            MasterBranch mb = new MasterBranch();
            mb.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                mb.checkdata = true;
            }
            return mb;
        }
        public string DeleteBranch(MasterBranch mb)
        {
            try
            {
                config.Update("Master_Branch", new Dictionary<String, object>()
                {
                    { "Is_deleted",   1},
                    { "Modified_By",   mb.Modified_by},
                    { "Modified_Date", mb.Modified_Date},
                    { "Modified_Time", mb.Modified_Time},
                    { "M_Device_Name", mb.M_Device_name},

                }, new Dictionary<string, object>()
                  {
                  { "branchId", mb.branchId }
                });
                mb.msg = "Deleted Successfuly";
            }
            catch (Exception ex)
            {
                mb.msg = "Deletion Not Completed Succesfuly";
            }
            return mb.msg;
        }

        //public string savebranch(MasterBranch mb)
        //{
        //    config.Insert("Master_Branch", new Dictionary<string, object>()
        //        {
        //        { "branchId", mb.branchId},
        //        { "branchName", mb.branchName},
        //        { "Inv_Code",mb.Inv_Code },
        //        { "Address", mb.Address},
        //        { "ContactNo", mb.ContactNo},
        //        { "EmailId", mb.EmailId},
        //        { "Created_by", mb.Created_by},
        //        { "Create_date", mb.Create_date},
        //        { "Create_Time", mb.Create_Time},
        //        { "Device_name", mb.Device_name},
        //        { "Modified_by", mb.Modified_by},
        //        { "Modified_Date", mb.Modified_Date},
        //        { "Gst_In", mb.Gst_In},
        //        { "Modified_Time", mb.Modified_Time},
        //        { "M_Device_name", mb.M_Device_name},
        //        { "Country_Id", mb.Country_Id},
        //        { "State_Id", mb.State_Id},
        //        { "District_Id", mb.District_Id},
        //        { "City_Id", mb.City_Id},
        //        { "Pin_code", mb.Pin_code}
        //    });
        //    mb.msg = "Saved Successfully";
        //    return mb.msg;
        //}

        //public DataTable savebranch(MasterBranch mb)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("branchId", mb.branchId);
        //    hs.Add("branchName", mb.branchName);
        //    hs.Add("Inv_Code", mb.Inv_Code);
        //    hs.Add("Address", mb.Address);
        //    hs.Add("ContactNo", mb.ContactNo);
        //    hs.Add("EmailId", mb.EmailId);
        //    hs.Add("Created_by", mb.Created_by);
        //    hs.Add("Create_date", mb.Create_date);
        //    hs.Add("Create_Time", mb.Create_Time);
        //    hs.Add("Device_name", mb.Device_name);
        //    hs.Add("Modified_by", mb.Modified_by);
        //    hs.Add("Modified_Date", mb.Modified_Date);
        //    hs.Add("Gst_In", mb.Gst_In);
        //    hs.Add("Modified_Time", mb.Modified_Time);
        //    hs.Add("M_Device_name", mb.M_Device_name);
        //    hs.Add("Country_Id", mb.Country_Id);
        //    hs.Add("State_Id", mb.State_Id);
        //    hs.Add("District_Id", mb.District_Id);
        //    hs.Add("City_Id", mb.City_Id);
        //    hs.Add("Pin_code", mb.Pin_code);
        //    dt = config.ReturnScalar("spSavebranch", hs);
        //    return dt;
        //}

        public MasterBranch CheckbranchName(string Branch, string Cityid)
        {
            string sql = "Select * from  Master_Branch where city_id='" + Cityid + "' order by city_id";
            config.singleResult(sql);

            MasterBranch mb = new MasterBranch();

            mb.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mb.branchName = Convert.ToString(dr["branchName"]);
                    if (mb.branchName == Branch)
                    {
                        mb.checkdata = true;
                        return mb;
                    }
                    else
                    {
                        mb.checkdata = false;
                    }
                }

            }

            return mb;
        }

        public MasterBranch getbranchId(MasterBranch mb)
        {
            string sql = "Select * from Master_Branch order by branchId";
            config.singleResult(sql);
            codenum = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mb.branchId = Convert.ToString(dr["branchId"]);
                    string subcode = (Convert.ToString(dr["branchId"])).Substring(0, 1);
                    if (mb.BranchSubId == subcode)
                    {
                        //codenum = Convert.ToInt32(mb.branchId.Substring(5)) + 1;
                        codenum = codenum + 1;
                    }
                }
            }
            return mb;
        }

        public List<MasterBranch> getBranchMast()
        {
            string sql = "Select * from  Master_Branch order by branchId";
            config.singleResult(sql);
            List<MasterBranch> mbl = new List<MasterBranch>();          
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in config.dt.Rows)
                        {
                            MasterBranch mb = new MasterBranch();
                            mb.branchId = Convert.ToString(dr["branchId"]);
                            mb.branchName = Convert.ToString(dr["branchName"]);
                            mbl.Add(mb);
                        }
                    }
            return mbl;
        }
        public String getBranch(String branchId,string dbname)
        {
            string BrName = string.Empty;
            string sql = "SELECT * from Master_Branch WHERE branchId = '" + branchId + "' ";      
            //config.singleResultFirstTime(sql, dbname.Replace(" ","_"));
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    BrName = Convert.ToString(dr["branchName"]);
                }
            }
            return BrName;  
        }

        public String getbranchIdBybranchName(String branchName)
        {
            string BrName = string.Empty;
            string sql = "SELECT * from Master_Branch WHERE branchName = '" + branchName + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    branchId = Convert.ToString(dr["branchId"]);
                }
            }
            return branchId;
        }
        public MasterBranch GetBranchDetails(string branchId,string db)
        {
            string sql = "Select  a.branchId,a.branchName,a.inv_code,a.Address,a.gst_in,a.contactno,a.emailid,a.pin_code,a.country_id,a.state_id,a.district_id,a.city_id,";
            sql = sql + "b.cityid,b.cityname,c.districtid,c.districtname,d.stateid,d.statename,e.countryid,e.countryname from  Master_Branch a,master_city b,";
            sql = sql + "master_district c,master_state d,master_country e where a.city_id=b.cityid and a.district_id=c.districtid and a.state_id=d.stateid and a.country_id=";
            sql=sql+"e.countryid and a.branchId='" + branchId + "'";
            config.singleResult(sql);
            MasterBranch mb = new MasterBranch();        
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mb.branchName = Convert.ToString(dr["branchName"]);
                    mb.Inv_Code = Convert.ToString(dr["Inv_Code"]);
                    mb.Gst_In = Convert.ToString(dr["Gst_In"]);
                    mb.Address = Convert.ToString(dr["Address"]);
                    mb.CityName = Convert.ToString(dr["CityName"]);
                    mb.DistrictName = Convert.ToString(dr["DistrictName"]);
                    mb.StateName = Convert.ToString(dr["StateName"]);
                    mb.CountryName = Convert.ToString(dr["CountryName"]);
                    mb.EmailId = Convert.ToString(dr["EmailId"]);
                    mb.ContactNo = Convert.ToString(dr["ContactNo"]);
                    mb.Pin_code = Convert.ToString(dr["Pin_code"]);                   
                }
            }
            return mb;
        }

        public MasterBranch CheckInvoiceCode(string Inv_Code)
        {
            string sql = "Select * from  Master_Branch where Inv_Code='" + Inv_Code + "'";
            config.singleResult(sql);
            MasterBranch mb = new MasterBranch();
            mb.checkdata = false;
            if (config.dt.Rows.Count > 0)
            {          
                        mb.checkdata = true;                 
            }
            else
            {
                        mb.checkdata = false;                 
            }        
            return mb;
        }

        public string getbranchNameByInvoiceCode(String invcode)
        {
            string BrName = string.Empty;
            string sql = "SELECT * from Master_Branch WHERE Inv_Code = '" + invcode + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    BrName = Convert.ToString(dr["branchName"]);
                }
            }
            return BrName;
        }


        //public MasterBranch getBranchDetailsBybranchId(string Brid)
        //{
        //    MasterBranch mb = new MasterBranch();
        //    string sql = "select * from master_Branch a,master_country b,master_State c,Master_district d,Master_City e where a.Country_Id=b.Countryid and a.State_Id=c.StateId and a.District_Id=d.DistrictId and a.City_Id=e.CityId and a.branchId='" + Brid + "'";
        //    config.singleResult(sql);
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            mb.branchId = Convert.ToString(dr["branchId"]);
        //            mb.branchName = Convert.ToString(dr["branchName"]);
        //            mb.Br_Country_Name = Convert.ToString(dr["CountryName"]);
        //            mb.Br_State_Name = Convert.ToString(dr["StateName"]);
        //            mb.Br_District_Name = Convert.ToString(dr["DistrictName"]);
        //            mb.Br_City_Name = Convert.ToString(dr["CityName"]);
        //            mb.City_Id = Convert.ToString(dr["City_Id"]);
        //            mb.District_Id = Convert.ToString(dr["District_Id"]);
        //            mb.State_Id = Convert.ToString(dr["State_Id"]);
        //            mb.Country_Id = Convert.ToString(dr["Country_Id"]);
        //            mb.Address = Convert.ToString(dr["Address"]);
        //            mb.Gst_In = Convert.ToString(dr["Gst_In"]);
        //            mb.ContactNo = Convert.ToString(dr["ContactNo"]);
        //            mb.EmailId = Convert.ToString(dr["EmailId"]);
        //            mb.Pin_code = Convert.ToString(dr["Pin_code"]);
        //            mb.Inv_Code = Convert.ToString(dr["Inv_Code"]);


        //        }
        //    }
        //    return mb;
        //}

        //public MasterBranch getBranchDetailsBybranchId(string Brid)
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable hs = new Hashtable();
        //    hs.Add("Brid", Brid);
        //    dt = config.ReturnScalar("spgetBranchDetailsBybranchId", hs);
        //    MasterBranch mb = new MasterBranch();
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            mb.branchId = Convert.ToString(dr["branchId"]);
        //            mb.branchName = Convert.ToString(dr["branchName"]);
        //            mb.Br_Country_Name = Convert.ToString(dr["CountryName"]);
        //            mb.Br_State_Name = Convert.ToString(dr["StateName"]);
        //            mb.Br_District_Name = Convert.ToString(dr["DistrictName"]);
        //            mb.Br_City_Name = Convert.ToString(dr["CityName"]);
        //            mb.City_Id = Convert.ToString(dr["City_Id"]);
        //            mb.District_Id = Convert.ToString(dr["District_Id"]);
        //            mb.State_Id = Convert.ToString(dr["State_Id"]);
        //            mb.Country_Id = Convert.ToString(dr["Country_Id"]);
        //            mb.Address = Convert.ToString(dr["Address"]);
        //            mb.Gst_In = Convert.ToString(dr["Gst_In"]);
        //            mb.ContactNo = Convert.ToString(dr["ContactNo"]);
        //            mb.EmailId = Convert.ToString(dr["EmailId"]);
        //            mb.Pin_code = Convert.ToString(dr["Pin_code"]);
        //            mb.Inv_Code = Convert.ToString(dr["Inv_Code"]);
        //        }
        //    }
        //    return mb;
        //}

        //public MasterBranch getBranchDetailsBybranchId(string Brid,string db)
        //{
        //    MasterBranch mb = new MasterBranch();
        //    string sql = "select * from master_Branch a,master_country b,master_State c,Master_district d,Master_City e where a.Country_Id=b.Countryid and a.State_Id=c.StateId and a.District_Id=d.DistrictId and a.City_Id=e.CityId and a.branchId='" + Brid + "'";
        //    config.singleResultByDbname(sql,db);
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            mb.branchId = Convert.ToString(dr["branchId"]);
        //            mb.branchName= Convert.ToString(dr["branchName"]);
        //            mb.Br_Country_Name = Convert.ToString(dr["CountryName"]);
        //            mb.Br_State_Name = Convert.ToString(dr["StateName"]);
        //            mb.Br_District_Name = Convert.ToString(dr["DistrictName"]);
        //            mb.Br_City_Name = Convert.ToString(dr["CityName"]);
        //            mb.City_Id = Convert.ToString(dr["City_Id"]);
        //            mb.District_Id = Convert.ToString(dr["District_Id"]);
        //            mb.State_Id = Convert.ToString(dr["State_Id"]);
        //            mb.Country_Id = Convert.ToString(dr["Country_Id"]);
        //            mb.Address = Convert.ToString(dr["Address"]);
        //            mb.Gst_In = Convert.ToString(dr["Gst_In"]);
        //            mb.ContactNo = Convert.ToString(dr["ContactNo"]);
        //            mb.EmailId = Convert.ToString(dr["EmailId"]);
        //            mb.Pin_code = Convert.ToString(dr["Pin_code"]);
        //            mb.Inv_Code = Convert.ToString(dr["Inv_Code"]);


        //        }
        //    }
        //    return mb;
        //}

        public List<MasterBranch> getAllBranchList()
        {
            string sql = "select * from master_Branch a,master_country b,master_State c,Master_district d,Master_City e where a.Country_Id=b.Countryid and a.State_Id=c.StateId and a.District_Id=d.DistrictId and a.City_Id=e.CityId and a.is_deleted<>1 order by branchName";
            config.singleResult(sql);

            List<MasterBranch> mbl = new List<MasterBranch>();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MasterBranch mb = new MasterBranch();
                    mb.branchId = Convert.ToString(dr["branchId"]);
                    mb.branchName = Convert.ToString(dr["branchName"]);
                    mb.Address = Convert.ToString(dr["Address"]);
                    mb.City_Id = Convert.ToString(dr["City_Id"]);
                    mb.District_Id = Convert.ToString(dr["District_Id"]);
                    mb.State_Id = Convert.ToString(dr["State_Id"]);
                    mb.Country_Id = Convert.ToString(dr["Country_Id"]);
                    mb.Br_Country_Name = Convert.ToString(dr["CountryName"]);
                    mb.Br_State_Name = Convert.ToString(dr["StateName"]);
                    mb.Br_District_Name = Convert.ToString(dr["DistrictName"]);
                    mb.Br_City_Name = Convert.ToString(dr["CityName"]);
                    mb.Gst_In = Convert.ToString(dr["Gst_In"]);
                    mb.ContactNo = Convert.ToString(dr["ContactNo"]);
                    mb.EmailId = Convert.ToString(dr["EmailId"]);
                    mb.Pin_code = Convert.ToString(dr["Pin_code"]);
                    mb.Inv_Code = Convert.ToString(dr["Inv_Code"]);
                    mbl.Add(mb);
                }

            }


            return mbl;
        }

        //public string Updatebranch(MasterBranch mb)
        //{
        //    string sql = "select * from Master_Branch where branchName ='" + mb.branchName + "' and City_Id='" + mb.City_Id + "'";
        //    config.singleResult(sql);
        //    //int check = 0;
        //    if (config.dt.Rows.Count > 0)
        //    {
                
        //        try
        //        {
        //            config.Update("Master_Branch", new Dictionary<String, object>()
        //            {
        //            { "Address", mb.Address },
        //            { "EmailId", mb.EmailId },
        //            { "ContactNo", mb.ContactNo },
        //            {"Inv_Code", mb.Inv_Code },
        //            { "City_Id", mb.City_Id },
        //            { "District_Id", mb.District_Id},
        //            { "State_Id", mb.State_Id},
        //            { "Gst_In", mb.Gst_In},
        //            { "Country_Id", mb.Country_Id},
        //            { "Pin_code", mb.Pin_code},
        //            { "Modified_By", mb.Modified_by},
        //            { "Modified_Date", mb.Modified_Date},
        //            { "Modified_Time", mb.Modified_Time},
        //            { "M_Device_Name", mb.M_Device_name},
        //            }, new Dictionary<string, object>()
        //            {
        //            { "branchId", mb.branchId },
        //            });
        //            mb.msg = "Updated Successfully";
        //            //mb.msg = "Same Branch Already Exist Under Same City";
        //        }
        //        catch (Exception ex)
        //        {
        //            mb.msg = "Updation Not Completed Succesfuly";
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            config.Update("Master_Branch", new Dictionary<String, object>()
        //            {
        //            { "branchName", mb.branchName},
        //            { "Address", mb.Address },
        //            { "EmailId", mb.EmailId },
        //            { "ContactNo", mb.ContactNo },
        //            {"Inv_Code", mb.Inv_Code },
        //            { "City_Id", mb.City_Id },
        //            { "District_Id", mb.District_Id},
        //            { "State_Id", mb.State_Id},
        //            { "Gst_In", mb.Gst_In},
        //            { "Country_Id", mb.Country_Id},
        //            { "Pin_code", mb.Pin_code},
        //            { "Modified_By", mb.Modified_by},
        //            { "Modified_Date", mb.Modified_Date},
        //            { "Modified_Time", mb.Modified_Time},
        //            { "M_Device_Name", mb.M_Device_name},
        //            }, new Dictionary<string, object>()
        //            {
        //            { "branchId", mb.branchId },
        //            });
        //            mb.msg = "Updated Successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            mb.msg = "Updation Not Completed Succesfuly";
        //        }

               
        //    }

        //    return mb.msg;
        //}

        public DataTable Updatebranch(MasterBranch mb)
        {
            DataTable dt = new DataTable();
            Hashtable hs = new Hashtable();
            string sql = "select * from Master_Branch where branchName ='" + mb.branchName + "' and City_Id='" + mb.City_Id + "'";
            config.singleResult(sql);
            //int check = 0;
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("Master_Branch", new Dictionary<String, object>()
                    {
                    { "Address", mb.Address },
                    { "EmailId", mb.EmailId },
                    { "ContactNo", mb.ContactNo },
                    {"Inv_Code", mb.Inv_Code },
                    { "City_Id", mb.City_Id },
                    { "District_Id", mb.District_Id},
                    { "State_Id", mb.State_Id},
                    { "Gst_In", mb.Gst_In},
                    { "Country_Id", mb.Country_Id},
                    { "Pin_code", mb.Pin_code},
                    { "Modified_By", mb.Modified_by},
                    { "Modified_Date", mb.Modified_Date},
                    { "Modified_Time", mb.Modified_Time},
                    { "M_Device_Name", mb.M_Device_name},
                    }, new Dictionary<string, object>()
                    {
                    { "branchId", mb.branchId },
                    });
                    mb.msg = "Updated Successfully";
                    //mb.msg = "Same Branch Already Exist Under Same City";
                }
                catch (Exception ex)
                {
                    mb.msg = "Updation Not Completed Succesfuly";
                }
            }
            else
            {
                try
                {
                    config.Update("Master_Branch", new Dictionary<String, object>()
                    {
                    { "branchName", mb.branchName},
                    { "Address", mb.Address },
                    { "EmailId", mb.EmailId },
                    { "ContactNo", mb.ContactNo },
                    {"Inv_Code", mb.Inv_Code },
                    { "City_Id", mb.City_Id },
                    { "District_Id", mb.District_Id},
                    { "State_Id", mb.State_Id},
                    { "Gst_In", mb.Gst_In},
                    { "Country_Id", mb.Country_Id},
                    { "Pin_code", mb.Pin_code},
                    { "Modified_By", mb.Modified_by},
                    { "Modified_Date", mb.Modified_Date},
                    { "Modified_Time", mb.Modified_Time},
                    { "M_Device_Name", mb.M_Device_name},
                    }, new Dictionary<string, object>()
                    {
                    { "branchId", mb.branchId },
                    });
                    mb.msg = "Updated Successfully";
                }
                catch (Exception ex)
                {
                    mb.msg = "Updation Not Completed Succesfuly";
                }
            }
            return dt;
        }
    }
}