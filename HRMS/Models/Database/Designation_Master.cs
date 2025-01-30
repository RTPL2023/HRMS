using HRMS.Controllers;
using HRMS.Includes;
using HRMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Designation_Master
    {
        SQLConfig config = new SQLConfig();

        public string id { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }
       
        UtilityController u = new UtilityController();
        

        public string saveDesignationMaster(employee_masterViewModel model)
        {
            string msg = "";
            string sql = "Select * from Designation_Master where Designation = '" + model.designation + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {

                config.Insert("Designation_Master", new Dictionary<string, object>()
                {
                    { "designation",model.designation },
                    { "department",model.department },
                    { "created_by","users"},
                    { "created_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/") },

                });
                msg = "Saved Successfully";
            }
            else
            {
                msg = "Designation Alrady Exisd";
            }
            return (msg);
        }
        public Designation_Master getdesignationByid(int id)
        {
            Designation_Master dm = new Designation_Master();

            string sql = "Select * from Designation_Master where id=" + id + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    dm.id = Convert.ToString(dr["id"]);
                    dm.department = Convert.ToString(dr["department"]);
                    dm.designation = Convert.ToString(dr["designation"]);

                }
            }
            return (dm);
        }  
        public string modifydesignationByid(employee_masterViewModel model)
        {

            string msg = "";
            string sql = "Select * from Designation_Master where id=" + model.editid + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("Designation_Master", new Dictionary<String, object>()
                {
                    { "department", model.editdepartment},
                    { "designation", model.editdesignation},
                    { "modified_by",   "user"},
                    { "modified_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                    

                }, new Dictionary<string, object>()
                  {
                  { "id",Convert.ToInt32( model.editid) }
                });
                msg = "Designation Update Successfull";
            }
            return (msg);
        }
        public List<Designation_Master> getDesignation_Masterlists()
        {
            List<Designation_Master> dmlst = new List<Designation_Master>();
            string sql = "Select * from Designation_Master order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Designation_Master dm = new Designation_Master();
                    dm.id = Convert.ToString(dr["id"]);
                    dm.department = Convert.ToString(dr["department"]);
                    dm.designation = Convert.ToString(dr["designation"]);
                    dmlst.Add(dm);
                }
            }
            return (dmlst);
        }
    }
}
