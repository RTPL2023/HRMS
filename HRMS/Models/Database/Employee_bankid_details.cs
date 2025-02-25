using HRMS.Includes;
using HRMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.Database
{
    public class Employee_bankid_details
    {
        SQLConfig config = new SQLConfig();
        public string employee_id { get; set; }
        public string ac_name { get; set; }
        public string ac_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string branch_code { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string voter_id_no { get; set; }
        public string pf_no { get; set; }
        public string esic_no { get; set; }
        public string passport_no { get; set; }
        public Employee_bankid_details getemployeeBankdetails(string employee_id)
        {
            Employee_bankid_details ebd = new Employee_bankid_details();
            ebd.employee_id = employee_id;
            string sql = "Select * from Employee_bankid_details where employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ebd.ac_name = Convert.ToString(dr["ac_name"]);
                    ebd.ac_no = Convert.ToString(dr["ac_no"]);
                    ebd.bank_name = Convert.ToString(dr["bank_name"]);
                    ebd.branch_name = Convert.ToString(dr["branch_name"]);
                    ebd.ifsc_code = Convert.ToString(dr["ifsc_code"]);
                    ebd.branch_code = Convert.ToString(dr["branch_code"]);
                    ebd.aadhar_no = Convert.ToString(dr["aadhar_no"]);
                    ebd.pan_no = Convert.ToString(dr["pan_no"]);
                    ebd.voter_id_no = Convert.ToString(dr["voter_id_no"]);
                    ebd.pf_no = Convert.ToString(dr["pf_no"]);
                    ebd.esic_no = Convert.ToString(dr["esic_no"]);
                    ebd.passport_no = Convert.ToString(dr["passport_no"]);

                }

            }

            return (ebd);
        }
        public string SaveUpdateBankdetails(employee_masterViewModel model)
        {
            Employee_bankid_details ebd = new Employee_bankid_details();

            string sql = "Select * from Employee_bankId_details where employee_id='" + model.employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "update Employee_bankId_details set ";
                sql += "ac_name='" + model.ac_name + "',";
                sql += "ac_no='" + model.ac_no + "',";
                sql += "bank_name='" + model.bank_name + "',";
                sql += "branch_name='" + model.branch_name + "',";
                sql += "ifsc_code='" + model.ifsc_code + "',";
                sql += "branch_code='" + model.branch_code + "',";
                sql += "aadhar_no='" + model.aadhar_no + "',";
                sql += "pan_no='" + model.pan_no + "',";
                sql += "voter_id_no='" + model.voter_id_no + "', ";
                sql += "esic_no='" + model.esic_no + "', ";
                sql += "passport_no='" + model.passport_no + "', ";
                sql += "pf_no='" + model.pf_no + "'";
                sql += "where ";
                sql +=  "employee_id='" + model.employee_id + "'";
                config.Execute_Query(sql);
            }
            else
            {
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
            }

            return "Saved";
        }
    }
}
