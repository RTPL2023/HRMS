
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
using System.Globalization;

namespace HRMS.Models.Database
{
    public class Employee_Master
    {
        UtilityController u = new UtilityController();

        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;



        public DateTimeFormatInfo UsCinfo { get => usCinfo; set => usCinfo = value; }
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
        public string joining_date2 { get; set; }
        public string date_of_birth2 { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string reporting_mg_id { get; set; }
        public string last_edu_ql { get; set; }
        public string branch_id { get; set; }
        public string employment_role { get; set; }
        public string user_role { get; set; }
        public string departmetName { get; set; }
        public string designationName { get; set; }
        public string blood_group { get; set; }
        public string emg_Name1 { get; set; }
        public string emg_Name2 { get; set; }
        public string emg_Relatio1 { get; set; }
        public string emg_Relatio2 { get; set; }
        public string emg_Phone1 { get; set; }
        public string emg_Phone2 { get; set; }
        public string emg_Address1 { get; set; }
        public string emg_Address2 { get; set; }
        public string image { get; set; }

        public string msg { get; set; }
        public Byte[] bank_detail { get; set; }
        public Byte[] aadhar { get; set; }
        public Byte[] pan { get; set; }
        public Byte[] voter_id { get; set; }
        public Byte[] joining_letter { get; set; }
        public Byte[] police_verification { get; set; }
        public Byte[] permanent_letter { get; set; }
        public Byte[] employee_img { get; set; }
        public string saveEmployeedetails(employee_masterViewModel model, string user)
        {
            UtilityController u = new UtilityController();


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
                    { "joining_date", Convert.ToDateTime(model.joining_date, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/")  },
                    { "date_of_birth", Convert.ToDateTime(model.date_of_birth, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/")},
                    { "designation",model.designation },
                    { "department",model.department },
                    { "reporting_mg_id",model.reporting_mg_id },
                    { "last_edu_ql",model.last_edu_ql },
                    { "branch_id",model.branch_id },
                    { "employment_role",model.employment_role },
                    { "blood_group",model.blood_group },

                });
                config.Insert("users", new Dictionary<string, object>()
                {
                    { "User_Id",model.employee_id },
                    { "employee_id",model.employee_id },
                    { "password",u.Encryptdata("Rishi") },
                    { "user_role",model.user_role },
                    { "allocated_branchid",model.branch_id },
                    { "created_by",user},
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
            string sql = " SELECT Employee_Master.*, Designation_Master.Designation as designationName, Employee_document.*,Department_Master.department as departmetName ,users.Blocked FROM Employee_Master LEFT JOIN Designation_Master ON Employee_Master.Designation = Designation_Master.id LEFT JOIN Department_Master ON Employee_Master.department = Department_Master.id LEFT JOIN Employee_document ON Employee_Master.Employee_id = Employee_document.employee_id LEFT JOIN Users ON Employee_Master.employee_id = Users.employee_id order by Employee_Master.employee_id ";
            config.singleResult(sql);
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (Convert.ToString(dr["Blocked"]) == "0" && Convert.ToString(dr["employee_id"])!= "RTPLM001")
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
                        em.designationName = Convert.ToString(dr["designationName"]);
                        em.departmetName = Convert.ToString(dr["departmetName"]);
                        em.employee_img = dr["employee_img"] != System.DBNull.Value ? (byte[])dr["employee_img"] : null;
                        if (em.employee_img != null)
                        {
                            em.image = "data:image/jpg;base64," + Convert.ToBase64String(em.employee_img);
                        }
                        emplst.Add(em);
                    }
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
            sql = "select * from Employee_Master where employee_id <> 'RTPLM001' order by id";
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
        public List<Employee_Master> getemployeelist(string user)
        {
            string sql;
            if (user == "RTPLM001")
            {
                sql = "select * from Employee_Master where employee_id <> 'RTPLM001' order by id";

            }
            else
            {
                sql = "select * from Employee_Master where employee_id <> 'RTPLM001' and Reporting_mg_id='" + user + "' order by id";

            }
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
        public List<Employee_Master> getemployeelist(string user)
        {
            string sql;
            if(user == "RTPLM001")
            {
                sql = "select * from Employee_Master where employee_id <> 'RTPLM001' order by id";

            }
            else
            {
                sql = "select * from Employee_Master where employee_id <> 'RTPLM001' and Reporting_mg_id='"+user+"' order by id";

            }
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
                }
            }
            return em;
        }
        public Employee_Master getEmployeeDetailByemployee_id(string empid)
        {
            string sql;
            sql = "select em.*,us.User_Role,dpm.department as departmetName,dsm.Designation as designationName from Employee_Master em,users us ,Department_Master dpm,Designation_Master dsm where us.employee_id = em.Employee_id and em.employee_id = '" + empid + "' and dsm.id = em.Designation and dpm.id = em.Department";
            config.singleResult(sql);
            Employee_Master em = new Employee_Master();

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
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
                    em.joining_date = Convert.ToDateTime(dr["joining_date"], usCinfo).ToString("yyyy/MM/dd");
                    em.date_of_birth = Convert.ToDateTime(dr["date_of_birth"], usCinfo).ToString("yyyy/MM/dd");
                    em.joining_date2 = Convert.ToDateTime(dr["joining_date"], usCinfo).ToString("dd/MM/yyyy");
                    em.date_of_birth2 = Convert.ToDateTime(dr["date_of_birth"], usCinfo).ToString("dd/MM/yyyy");
                    em.designation = Convert.ToString(dr["designation"]);
                    em.department = Convert.ToString(dr["department"]);
                    em.departmetName = Convert.ToString(dr["departmetName"]);
                    em.designationName = Convert.ToString(dr["designationName"]);
                    em.reporting_mg_id = Convert.ToString(dr["reporting_mg_id"]);
                    em.last_edu_ql = Convert.ToString(dr["last_edu_ql"]);
                    em.employment_role = Convert.ToString(dr["employment_role"]);
                    em.blood_group = Convert.ToString(dr["blood_group"]);
                    em.user_role = Convert.ToString(dr["user_role"]);
                }
            }
            return em;
        }
        public string UpdateEmployeedetailsByEmployee_id(employee_masterViewModel model, string user)
        {

            string sql = "Select * from Employee_Master where Employee_id = '" + model.employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "update Employee_Master set ";
                sql = sql + "employee_id='" + model.employee_id + "',";
                sql = sql + "name='" + model.name + "',";
                sql = sql + "gender='" + model.gender + "',";
                sql = sql + "contact_number_1='" + model.contact_number_1 + "',";
                sql = sql + "contact_number_2='" + model.contact_number_2 + "',";
                sql = sql + "personal_email='" + model.personal_email + "',";
                sql = sql + "official_email='" + model.official_email + "',";
                sql = sql + "employment_type='" + model.employment_type + "',";
                sql = sql + "address='" + model.address + "', ";
                sql = sql + "city='" + model.city + "', ";
                sql = sql + "dist='" + model.dist + "', ";
                sql = sql + "state='" + model.state + "',";
                sql = sql + "country='" + model.country + "',";
                sql = sql + "pin='" + model.pin + "',";
                sql = sql + "joining_date='" + Convert.ToDateTime(model.joining_date, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/") + "', ";
                sql = sql + "date_of_birth='" + Convert.ToDateTime(model.date_of_birth, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/") + "', ";
                sql = sql + "designation='" + model.designation + "', ";
                sql = sql + "department='" + model.department + "', ";
                sql = sql + "reporting_mg_id='" + model.reporting_mg_id + "', ";
                sql = sql + "last_edu_ql='" + model.last_edu_ql + "', ";
                sql = sql + "branch_id='" + model.branch_id + "', ";
                sql = sql + "employment_role='" + model.employment_role + "',";
                sql = sql + "blood_group='" + model.blood_group + "'";
                sql = sql + "where ";
                sql = sql + "employee_id='" + model.employee_id + "'";

                config.Execute_Query(sql);
                //users update role
                sql = "update users set ";
                sql = sql + "user_role='" + model.user_role + "',";
                sql = sql + "Modified_By='" + user + "',";
                sql = sql + "Modified_On='" + u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/") + "'";

                sql = sql + "where ";
                sql = sql + "employee_id='" + model.employee_id + "'";
                config.Execute_Query(sql);

                msg = "Updated Successfull";
            }
            else
            {
                msg = "Something Wrong!!";
            }
            return (msg);
        }
        public Employee_Master getEmargencyContact(string empid)
        {
            string sql;
            sql = "SELECT * FROM Employee_Emergency_details where Employee_id='" + empid + "';";
            config.singleResult(sql);
            Employee_Master em = new Employee_Master();
            em.employee_id = empid;
            if (config.dt.Rows.Count >= 1)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];

                em.emg_Name1 = Convert.ToString(dr["Emg_contact_name"]);
                em.emg_Phone1 = Convert.ToString(dr["Emg_contact_no"]);
                em.emg_Relatio1 = Convert.ToString(dr["Emg_contact_relation"]);
                em.emg_Address1 = Convert.ToString(dr["emg_address"]);
            }
            if (config.dt.Rows.Count == 2)
            {
                DataRow dr2 = (DataRow)config.dt.Rows[1];
                em.emg_Name2 = Convert.ToString(dr2["Emg_contact_name"]);
                em.emg_Phone2 = Convert.ToString(dr2["Emg_contact_no"]);
                em.emg_Relatio2 = Convert.ToString(dr2["Emg_contact_relation"]);
                em.emg_Address2 = Convert.ToString(dr2["emg_address"]);
            }
            return em;
        }
        public string SAVEUpdateEmployee_Emergency_details(employee_masterViewModel model)
        {
            string sql = "SELECT * FROM Employee_Emergency_details where Employee_id='" + model.employee_id + "';";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "delete FROM Employee_Emergency_details where Employee_id = '" + model.employee_id + "'";
                config.Execute_Query(sql);
            }
            if (model.emg_Name1 != null && model.emg_Name1 != "")
            {
                config.Insert("Employee_Emergency_details", new Dictionary<string, object>()
                         {
                         { "employee_id",model.employee_id },
                         { "emg_contact_name",model.emg_Name1 },
                         { "emg_contact_relation",model.emg_Relatio1 },
                         { "emg_contact_no",model.emg_Phone1 },
                         { "emg_address",model.emg_Address1 }

                         });

            }
            if (model.emg_Name2 != null && model.emg_Name2 != "")
            {
                config.Insert("Employee_Emergency_details", new Dictionary<string, object>()
                            {
                            { "employee_id",model.employee_id },
                            { "emg_contact_name",model.emg_Name2 },
                            { "emg_contact_relation",model.emg_Relatio2 },
                            { "emg_contact_no",model.emg_Phone2 },
                            { "emg_address",model.emg_Address2 }
                });

            }

            return "Saved";
        }

    }
}
