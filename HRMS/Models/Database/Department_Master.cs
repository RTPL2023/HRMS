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
    public class Department_Master
    {
        SQLConfig config = new SQLConfig();

        public string id { get; set; }
        public string department { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }

        UtilityController u = new UtilityController();


        public string saveDepartment_Master(employee_masterViewModel model)
        {
            string msg = "";
            string sql = "Select * from Department_Master where department = '" + model.department + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {

                config.Insert("Department_Master", new Dictionary<string, object>()
                {
                    { "department",model.department },
                    { "created_by","users"},
                    { "created_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/") },

                });
                msg = "Saved Successfully";
            }
            else
            {
                msg = "department Alrady Exisd";
            }
            return (msg);
        }
        public Department_Master getdepartmentByid(int id)
        {
            Department_Master dm = new Department_Master();

            string sql = "Select * from Department_Master where id=" + id + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    dm.id = Convert.ToString(dr["id"]);

                    dm.department = Convert.ToString(dr["department"]);

                }
            }
            return (dm);
        }
        public string modifydepartmentByid(employee_masterViewModel model)
        {

            string msg = "";
            string sql = "Select * from Department_Master where id=" + model.editid + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("Department_Master", new Dictionary<String, object>()
                {

                    { "department", model.editdepartment},
                    { "modified_by",   "user"},
                    { "modified_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},


                }, new Dictionary<string, object>()
                  {
                  { "id",Convert.ToInt32( model.editid) }
                });
                msg = "Department Updated Successfull";
            }
            return (msg);
        }
        public string deletedepartmentByid(employee_masterViewModel model)
        {
            string msg = "";
            string sql = "delete from Department_Master where id=" + model.editid + "";
            config.Execute_Query(sql);

            msg = "Department Deleted Successfull";

            return (msg);
        }
        public List<Department_Master> getDepartment_Masterlists()
        {
            List<Department_Master> dmlst = new List<Department_Master>();
            string sql = "Select * from Department_Master order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Department_Master dm = new Department_Master();
                    dm.id = Convert.ToString(dr["id"]);

                    dm.department = Convert.ToString(dr["department"]);
                    dmlst.Add(dm);
                }
            }
            return (dmlst);
        }
        public Department_Master getdepartmentlist()
        {
            Department_Master dm = new Department_Master();

            string sql = "Select * from Department_Master ORDER BY department";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    dm.id = Convert.ToString(dr["id"]);

                    dm.department = Convert.ToString(dr["department"]);

                }
            }
            return (dm);
        }
    }
}
