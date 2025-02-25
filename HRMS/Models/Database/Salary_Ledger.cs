using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Includes;
using System.Globalization;
using HRMS.Models.ViewModel;

namespace HRMS.Models.Database
{
    public class Salary_Ledger
    {
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public decimal actual_net_pay { get; set; }
        public decimal calculated_net_pay { get; set; }
        public string lop { get; set; }
        public string days_in_month { get; set; }
        public decimal emp_basic { get; set; }
        public decimal emp_hra { get; set; }
        public decimal emp_Other_Allowance { get; set; }
        public decimal emp_Total_Earning { get; set; }
        public decimal emp_PTAX { get; set; }
        public decimal emp_PF { get; set; }
        public decimal emp_esic { get; set; }
        public decimal emp_tds { get; set; }
        public decimal tot_emp_ded { get; set; }
        public decimal comp_pf { get; set; }
        public decimal comp_esic { get; set; }
        public decimal tot_comp_con { get; set; }
        public decimal tot_ctc { get; set; }
        public string effect_date { get; set; }
        public string financial_year { get; set; }
        public string tds_type { get; set; }
        public int Designation { get; set; }
        public string Designation_name { get; set; }
        public string department { get; set; }
        public string joining_date { get; set; }
        public string date_of_birth { get; set; }
        public string contact_no_1 { get; set; }
        public string bank_name { get; set; }
        public string ifsc_code { get; set; }
        public string ac_no { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string pf_no { get; set; }
        public string esic_no { get; set; }
        public string msg { get; set; }




        public void SaveSalaryLedger(Salary_Ledger sl)
        {
            string sql = "";
            if (sl.month != "" && sl.month != null)
            {
                sl.month = sl.month.ToUpper();
            }
            sql = "Select * from salary_master where employee_id='" + sl.employee_id + "' order by Convert(date,effect_date,103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                {

                    
                    sl.emp_hra = !Convert.IsDBNull(dr["emp_hra"]) ? Convert.ToDecimal(dr["emp_hra"]) : Convert.ToDecimal("0");
                    sl.emp_basic = !Convert.IsDBNull(dr["emp_basic"]) ? Convert.ToDecimal(dr["emp_basic"]) : Convert.ToDecimal("0");
                    sl.emp_Other_Allowance = !Convert.IsDBNull(dr["emp_Other_Allowance"]) ? Convert.ToDecimal(dr["emp_Other_Allowance"]) : Convert.ToDecimal("0");
                    sl.emp_Total_Earning = !Convert.IsDBNull(dr["emp_Total_Earning"]) ? Convert.ToDecimal(dr["emp_Total_Earning"]) : Convert.ToDecimal("0");
                    sl.emp_PTAX = !Convert.IsDBNull(dr["emp_PTAX"]) ? Convert.ToDecimal(dr["emp_PTAX"]) : Convert.ToDecimal("0");
                    sl.emp_PF = !Convert.IsDBNull(dr["emp_PF"]) ? Convert.ToDecimal(dr["emp_PF"]) : Convert.ToDecimal("0");
                    sl.emp_esic = !Convert.IsDBNull(dr["emp_esic"]) ? Convert.ToDecimal(dr["emp_esic"]) : Convert.ToDecimal("0");
                    sl.tot_emp_ded = !Convert.IsDBNull(dr["tot_emp_ded"]) ? Convert.ToDecimal(dr["tot_emp_ded"]) : Convert.ToDecimal("0");
                    sl.comp_pf = !Convert.IsDBNull(dr["comp_pf"]) ? Convert.ToDecimal(dr["comp_pf"]) : Convert.ToDecimal("0");
                    sl.comp_esic = !Convert.IsDBNull(dr["comp_esic"]) ? Convert.ToDecimal(dr["comp_esic"]) : Convert.ToDecimal("0");
                    sl.tot_comp_con = !Convert.IsDBNull(dr["tot_comp_con"]) ? Convert.ToDecimal(dr["tot_comp_con"]) : Convert.ToDecimal("0");
                    sl.tot_ctc = !Convert.IsDBNull(dr["tot_ctc"]) ? Convert.ToDecimal(dr["tot_ctc"]) : Convert.ToDecimal("0");
                    sl.Designation = Convert.ToInt32(dr["Designation"]);
                    sl.emp_tds = !Convert.IsDBNull(dr["emp_tds"]) ? Convert.ToDecimal(dr["emp_tds"]) : Convert.ToDecimal("0");
                    sl.tds_type = Convert.ToString(dr["tds_type"]);

                }
            }
            sql = "Select * from salary_ledger where employee_id='" + sl.employee_id + "' and month='" + sl.month.ToUpper() + "' and year='" + sl.year + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "update salary_ledger set ";
                sql = sql + "actual_net_pay='" + sl.actual_net_pay + "',";
                sql = sql + "calculated_net_pay='" + sl.calculated_net_pay + "',";
                sql = sql + "lop='" + sl.lop + "',";               
                sql += "emp_hra=" + Convert.ToDecimal(sl.emp_hra) + ",";
                sql += "emp_basic=" + Convert.ToDecimal(sl.emp_basic) + ",";
                sql += "emp_Other_Allowance=" + Convert.ToDecimal(sl.emp_Other_Allowance) + ",";
                sql += "emp_Total_Earning=" + Convert.ToDecimal(sl.emp_Total_Earning) + ",";
                sql += "emp_PTAX=" + Convert.ToDecimal(sl.emp_PTAX) + ",";
                sql += "emp_PF=" + Convert.ToDecimal(sl.emp_PF) + ",";
                sql += "emp_esic=" + Convert.ToDecimal(sl.emp_esic) + ",";
                sql += "tds_type='" + sl.tds_type + "',";
                sql += "emp_tds=" + Convert.ToDecimal(sl.emp_tds) + ",";
                sql += "tot_emp_ded=" + Convert.ToDecimal(sl.tot_emp_ded) + ",";
                sql += "comp_pf=" + Convert.ToDecimal(sl.comp_pf) + ",";
                sql += "comp_esic=" + Convert.ToDecimal(sl.comp_esic) + ",";
                sql += "tot_comp_con=" + Convert.ToDecimal(sl.tot_comp_con) + ",";
                sql += "tot_ctc=" + Convert.ToDecimal(sl.tot_ctc) + ",";
                sql += "Designation=" + Convert.ToInt32(sl.Designation) + ",";
                sql = sql + "days_in_month='" + sl.days_in_month + "' ";
                sql = sql + "where ";
                sql = sql + "employee_id='" + sl.employee_id + "'and ";
                sql = sql + "month='" + sl.month + "'and ";
                sql = sql + "year='" + sl.year + "'";
                config.Update_Execute_Query(sql);
            }
            else
            {
                config.Insert("salary_ledger", new Dictionary<string, object>()
                    {
                        { "employee_id",sl.employee_id },
                        { "month",sl.month },
                        { "year",sl.year },
                        { "actual_net_pay",sl.actual_net_pay},
                        { "calculated_net_pay",sl.calculated_net_pay},
                        { "lop",sl.lop},
                        { "days_in_month",sl.days_in_month},
                        { "emp_hra",sl.emp_hra},
                        { "emp_basic",sl.emp_basic},
                        { "emp_Other_Allowance",sl.emp_Other_Allowance},
                        { "emp_Total_Earning",sl.emp_Total_Earning},
                        { "emp_PTAX",sl.emp_PTAX},
                        { "emp_PF",sl.emp_PF},
                        { "emp_esic",sl.emp_esic},
                        { "tds_type",sl.tds_type},
                       { "emp_tds",sl.emp_tds},
                       { "tot_emp_ded",sl.tot_emp_ded},
                       { "comp_pf",sl.comp_pf},
                       { "comp_esic",sl.comp_esic},
                       { "tot_comp_con",sl.tot_comp_con},
                       { "tot_ctc",sl.tot_ctc},
                       { "Designation",sl.Designation}



                });
            }
        }
        public Salary_Ledger GetSalaryLedgerForPayslip(string empid,string year,string month)
        {
            Salary_Ledger sl = new Salary_Ledger();
            sl.msg = "Payslip Not Generated Yet.Please Contact Administrator.";
            string sql = "Select sl.*,em.name,em.department,em.contact_number_1,em.joining_date,em.date_of_birth,ebd.bank_name,ebd.ifsc_code,ebd.ac_no,ebd.aadhar_no,ebd.pan_no,ebd.pf_no,ebd.esic_no,dm.designation as dsg,dpm.department as dept from salary_ledger sl,employee_master em ,employee_bankid_details ebd,designation_master dm,department_master dpm where em.employee_id = sl.employee_id and ebd.employee_id = sl.employee_id and dm.id = sl.designation and dpm.id = em.department and sl.employee_id = '"+ empid + "' and sl.year = '"+ year + "' and sl.month = '"+ month + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {

                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                {


                    sl.emp_hra = !Convert.IsDBNull(dr["emp_hra"]) ? Convert.ToDecimal(dr["emp_hra"]) : Convert.ToDecimal("0");
                    sl.emp_basic = !Convert.IsDBNull(dr["emp_basic"]) ? Convert.ToDecimal(dr["emp_basic"]) : Convert.ToDecimal("0");
                    sl.emp_Other_Allowance = !Convert.IsDBNull(dr["emp_Other_Allowance"]) ? Convert.ToDecimal(dr["emp_Other_Allowance"]) : Convert.ToDecimal("0");
                    sl.emp_Total_Earning = !Convert.IsDBNull(dr["emp_Total_Earning"]) ? Convert.ToDecimal(dr["emp_Total_Earning"]) : Convert.ToDecimal("0");
                    sl.emp_PTAX = !Convert.IsDBNull(dr["emp_PTAX"]) ? Convert.ToDecimal(dr["emp_PTAX"]) : Convert.ToDecimal("0");
                    sl.emp_PF = !Convert.IsDBNull(dr["emp_PF"]) ? Convert.ToDecimal(dr["emp_PF"]) : Convert.ToDecimal("0");
                    sl.emp_esic = !Convert.IsDBNull(dr["emp_esic"]) ? Convert.ToDecimal(dr["emp_esic"]) : Convert.ToDecimal("0");
                    sl.tot_emp_ded = !Convert.IsDBNull(dr["tot_emp_ded"]) ? Convert.ToDecimal(dr["tot_emp_ded"]) : Convert.ToDecimal("0");
                    sl.comp_pf = !Convert.IsDBNull(dr["comp_pf"]) ? Convert.ToDecimal(dr["comp_pf"]) : Convert.ToDecimal("0");
                    sl.comp_esic = !Convert.IsDBNull(dr["comp_esic"]) ? Convert.ToDecimal(dr["comp_esic"]) : Convert.ToDecimal("0");
                    sl.tot_comp_con = !Convert.IsDBNull(dr["tot_comp_con"]) ? Convert.ToDecimal(dr["tot_comp_con"]) : Convert.ToDecimal("0");
                    sl.actual_net_pay = !Convert.IsDBNull(dr["actual_net_pay"]) ? Convert.ToDecimal(dr["actual_net_pay"]) : Convert.ToDecimal("0");
                    sl.tot_ctc = !Convert.IsDBNull(dr["tot_ctc"]) ? Convert.ToDecimal(dr["tot_ctc"]) : Convert.ToDecimal("0");
                    sl.calculated_net_pay = !Convert.IsDBNull(dr["calculated_net_pay"]) ? Convert.ToDecimal(dr["calculated_net_pay"]) : Convert.ToDecimal("0");
                    sl.Designation = Convert.ToInt32(dr["Designation"]);
                    sl.emp_tds = !Convert.IsDBNull(dr["emp_tds"]) ? Convert.ToDecimal(dr["emp_tds"]) : Convert.ToDecimal("0");
                    sl.tds_type = Convert.ToString(dr["tds_type"]);
                    sl.employee_name = Convert.ToString(dr["name"]);
                    sl.lop = Convert.ToString(dr["lop"]);
                    sl.contact_no_1 = Convert.ToString(dr["contact_number_1"]);
                    sl.department=Convert.ToString(dr["dept"]);
                    sl.Designation_name=Convert.ToString(dr["dsg"]);
                    sl.date_of_birth=Convert.ToString(dr["date_of_birth"]);
                    sl.joining_date=Convert.ToString(dr["joining_date"]);
                    sl.bank_name=Convert.ToString(dr["bank_name"]);
                    sl.ifsc_code = Convert.ToString(dr["ifsc_code"]);
                    sl.ac_no = Convert.ToString(dr["ac_no"]);
                    sl.aadhar_no = Convert.ToString(dr["aadhar_no"]);
                    sl.pan_no = Convert.ToString(dr["pan_no"]);
                    sl.pf_no = Convert.ToString(dr["pf_no"]);
                    sl.esic_no = Convert.ToString(dr["esic_no"]);
                    


                }
                sl.msg = "Payslip Found";
            }
            return sl;
        }


    }
}
