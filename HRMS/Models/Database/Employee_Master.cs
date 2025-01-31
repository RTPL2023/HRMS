
using HRMS.Includes;
using HRMS.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HRMS.Models.Database
{
    public class Employee_Master
    {
        UtilityController u = new UtilityController();
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string contact_number_1 { get; set; }
        public string contact_number_2 { get; set; }
        public string personal_email { get; set; }
        public string official_email { get; set; }
        public string employment_type { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string dist { get; set; }
        public string state { get; set; }
        public string pin { get; set; }
        public string country { get; set; }

        public string joining_date { get; set; }
        public string date_of_birth { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string reporting_mg_id { get; set; }
        public string last_edu_ql { get; set; }
        public string branch_id { get; set; }
        public string employment_role { get; set; }

        public string msg { get; set; }
        public Byte[] bank_detail { get; set; }
        public Byte[] aadhar { get; set; }
        public Byte[] pan { get; set; }
        public Byte[] voter_id { get; set; }
        public Byte[] joining_letter { get; set; }
        public Byte[] police_verification { get; set; }
        public Byte[] permanent_letter { get; set; }
        public Byte[] employee_img { get; set; }
        public string saveEmployeedetails(employee_masterViewModel model)
        {

            string sql = "Select * from Employee_Master where Employee_id = '" + model.employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {
                config.Insert("Employee_Master", new Dictionary<string, object>()
                {
                    { "employee_id",model.employee_id },
                    { "name",model.name },
                    { "gender",model.gender },
                    { "contact_number_1",model.contact_number_1 },
                    { "contact_number_2",model.contact_number_2 },
                    { "personal_email",model. personal_email},
                    { "official_email",model.official_email },
                    { "employment_type",model.employment_type },
                    { "address",model.address },
                    { "city",model.city },
                    { "dist",model.dist },
                    { "state",model. state},
                    { "country",model. country},
                    { "pin",model.pin },
                    { "joining_date",model.joining_date },
                    { "date_of_birth",model.date_of_birth },
                    { "designation",model.designation },
                    { "department",model.department },
                    { "reporting_mg_id",model.reporting_mg_id },
                    { "last_edu_ql",model.last_edu_ql },
                    { "branch_id",model.branch_id },
                    { "employment_role",model.employment_role },

                });
                config.Insert("users", new Dictionary<string, object>()
                {
                    { "User_Id",model.employee_id },
                    { "employee_id",model.employee_id },
                    { "password","Rishi" },
                    { "user_role",model.user_role },
                    { "allocated_branchid",model.branch_id },
                    { "created_by","users"},
                    { "created_on",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/") },

                });
                msg = "Saved Successfully";
            }
            else
            {
                msg = "Employee Id Alrady Exisd";
            }
            return (msg);
        }
        public List<Employee_Master> getemployeelists()
        {
            List<Employee_Master> emplst = new List<Employee_Master>();
            string sql = "Select * from Employee_Master order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employee_Master em = new Employee_Master();
                    em.employee_id = Convert.ToString(dr["employee_id"]);
                    em.branch_id = Convert.ToString(dr["branch_id"]);
                    em.name = Convert.ToString(dr["name"]);
                    em.gender = Convert.ToString(dr["gender"]);
                    em.contact_number_1 = Convert.ToString(dr["contact_number_1"]);
                    em.contact_number_2 = Convert.ToString(dr["contact_number_2"]);
                    em.personal_email = Convert.ToString(dr["personal_email"]);
                    em.official_email = Convert.ToString(dr["official_email"]);
                    em.employment_type = Convert.ToString(dr["employment_type"]);
                    em.address = Convert.ToString(dr["address"]);
                    em.city = Convert.ToString(dr["city"]);
                    em.dist = Convert.ToString(dr["dist"]);
                    em.state = Convert.ToString(dr["state"]);
                    em.country = Convert.ToString(dr["country"]);
                    em.pin = Convert.ToString(dr["pin"]);
                    em.joining_date = Convert.ToString(dr["joining_date"]);
                    em.date_of_birth = Convert.ToString(dr["date_of_birth"]);
                    em.designation = Convert.ToString(dr["designation"]);
                    em.department = Convert.ToString(dr["department"]);
                    em.reporting_mg_id = Convert.ToString(dr["reporting_mg_id"]);
                    em.last_edu_ql = Convert.ToString(dr["last_edu_ql"]);
                    em.employment_role = Convert.ToString(dr["employment_role"]);
                    emplst.Add(em);
                }

            }

            return (emplst);
        }
        public List<Employee_Master> getmanageremployeelists()
        {
            List<Employee_Master> emplst = new List<Employee_Master>();
            string sql = "Select * from Employee_Master where employment_role='Manager' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employee_Master em = new Employee_Master();
                    em.employee_id = Convert.ToString(dr["employee_id"]);

                    em.name = Convert.ToString(dr["name"]);

                    emplst.Add(em);
                }

            }

            return (emplst);
        }
        public void updateEmployeedetails(documentUploadViewModel model)
        {
            Employee_Master em = new Employee_Master();
            var fs = new MemoryStream();
            model.bank_detail.CopyTo(fs);
            em.bank_detail = fs.ToArray();
            model.aadhar.CopyTo(fs);
            em.aadhar = fs.ToArray();
            model.pan.CopyTo(fs);
            em.pan = fs.ToArray();
            model.joining_letter.CopyTo(fs);
            em.joining_letter = fs.ToArray();
            model.police_verification.CopyTo(fs);
            em.police_verification = fs.ToArray();
            model.permanent_letter.CopyTo(fs);
            em.permanent_letter = fs.ToArray();
            model.employee_img.CopyTo(fs);
            em.employee_img = fs.ToArray();
            model.voter_id.CopyTo(fs);
            em.voter_id = fs.ToArray();

            config.Insert("Employee_document", new Dictionary<string, object>()
                {
                    { "employee_id",model.employee_id },
                    { "bank_detail",em.bank_detail },
                    { "aadhar",em.aadhar },
                    { "pan",em.pan },
                    { "joining_letter",em.joining_letter },
                    { "police_verification",em.police_verification },
                    { "permanent_letter",em.permanent_letter},
                    { "employee_img",em.employee_img },
                    { "voter_id",em.voter_id },

                });
            config.Insert("Employee_bankId_details", new Dictionary<string, object>()
                {
                    { "employee_id",model.employee_id },
                    { "ac_name",model.ac_name },
                    { "ac_no",model.ac_no },
                    { "bank_name",model.bank_name },
                    { "branch_name",model.branch_name },
                    { "ifsc_code",model.ifsc_code },
                    { "branch_code",model.branch_code },
                    { "aadhar_no",model.aadhar_no },
                    { "pan_no",model.pan_no },
                    { "voter_id_no",model.voter_id_no },
                    { "esic_no",model.esic_no },
                    { "passport_no",model.passport_no },
                    { "pf_no",model.pf_no },


            });
            config.Insert("Employee_Emergency_details", new Dictionary<string, object>()
                {
                      { "employee_id",model.employee_id },
                    { "emg_contact_name",model.emg_contact_name },
                    { "emg_contact_relation",model.emg_contact_relation },
                    { "emg_contact_no",model.emg_contact_no }

                });


        }
        public List<Employee_Master> getempidMast()
        {
            string sql;
            sql = "select * from Employee_Master order by id";
            config.singleResult(sql);
            List<Employee_Master> lstdtm = new List<Employee_Master>();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employee_Master dtm = new Employee_Master();
                    dtm.employee_id = dr["employee_id"].ToString();
                    dtm.name = dr["employee_id"].ToString() + "-" + dr["name"].ToString();


                    lstdtm.Add(dtm);
                }
            }
            return lstdtm;
        }
        public Employee_Master getEmployeename(string empid)
        {
            string sql;
            sql = "select * from Employee_Master where employee_id='" + empid + "'";
            config.singleResult(sql);
            Employee_Master em = new Employee_Master();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {


                    em.name = dr["name"].ToString();

                }
            }
            return em;
        }
    }
}
