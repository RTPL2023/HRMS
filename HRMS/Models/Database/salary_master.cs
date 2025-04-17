using HRMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Includes;
using System.Data;
using System.Globalization;

namespace HRMS.Models.Database
{
    public class salary_master
    {
        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }
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
        public decimal emp_net { get; set; }
        public decimal tot_ctc { get; set; }
        public string effect_date { get; set; }
        public string financial_year { get; set; }
        public string name { get; set; }
        public int Designation { get; set; }
        public string tds_type { get; set; }
        public string SavedSalarysByemployee_id(SalaryMasterViewModel model)
        {


            Designation_Master dm = new Designation_Master();

            string msg = string.Empty;
            int designationid = 0;
            string sql = "Select* from Designation_Master where designation = '" + model.designation + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                designationid = Convert.ToInt32(dr["id"]);
            }
            else
            {
                dm.saveDesignationMaster(model.designation);
                sql = "Select* from Designation_Master where designation = " + model.designation + "";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr = (DataRow)config.dt.Rows[0];
                    designationid = Convert.ToInt32(dr["id"]);
                }
            }
            sql = "select * from salary_master where employee_id='" + model.employee_id + "' and Convert(varchar,effect_date,103)=Convert(varchar,'" + model.effect_date + "',103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "Update salary_master set ";
                sql += "emp_hra=" + Convert.ToDecimal(model.hra) + ",";
                sql += "emp_basic=" + Convert.ToDecimal(model.basic) + ",";
                sql += "emp_Other_Allowance=" + Convert.ToDecimal(model.other_allw) + ",";
                sql += "emp_Total_Earning=" + Convert.ToDecimal(model.tot_earn) + ",";
                sql += "emp_PTAX=" + Convert.ToDecimal(model.emp_ptax) + ",";
                sql += "emp_PF=" + Convert.ToDecimal(model.emp_pf) + ",";
                sql += "emp_esic=" + Convert.ToDecimal(model.emp_esic) + ",";
                sql += "tds_type='" + model.tds_type + "',";
                sql += "emp_tds=" + Convert.ToDecimal(model.emp_tds) + ",";
                sql += "tot_emp_ded=" + Convert.ToDecimal(model.tot_emp_ded) + ",";
                sql += "comp_pf=" + Convert.ToDecimal(model.comp_pf) + ",";
                sql += "comp_esic=" + Convert.ToDecimal(model.comp_esic) + ",";
                sql += "tot_comp_con=" + Convert.ToDecimal(model.tot_comp_con) + ",";
                sql += "tot_ctc=" + Convert.ToDecimal(model.tot_ctc) + ",";
                sql += "emp_net=" + Convert.ToDecimal(model.emp_net) + ",";
                sql += "Designation=" + Convert.ToInt32(designationid) + "";
                sql += " where employee_id='" + model.employee_id + "' and Convert(varchar,effect_date,103)=Convert(varchar,'" + model.effect_date + "',103)";
                try
                {
                    config.Execute_Query(sql);
                    msg = "Salary Updated";
                }
                catch (Exception ex)
                {
                    msg = "err" + ex;
                }
            }
            else
            {
                try
                {
                    config.Insert("salary_master", new Dictionary<string, object>()
                    {
                    { "emp_hra",Convert.ToDecimal(model.hra)},
                    { "emp_basic",Convert.ToDecimal(model.basic)},
                    { "emp_Other_Allowance",Convert.ToDecimal(model.other_allw)},
                    { "emp_Total_Earning",Convert.ToDecimal(model.tot_earn)  },
                    { "emp_PTAX",Convert.ToDecimal(model.emp_ptax)},
                    { "emp_PF",Convert.ToDecimal(model.emp_pf)},
                    { "emp_esic",Convert.ToDecimal(model.emp_esic)},
                    { "emp_tds",Convert.ToDecimal(model.emp_tds)},
                    { "tds_type",model.tds_type},
                    { "emp_net",Convert.ToDecimal(model.emp_net)},
                    { "tot_emp_ded",Convert.ToDecimal(model.tot_emp_ded)},
                    { "comp_pf",Convert.ToDecimal(model.comp_pf)},
                    { "comp_esic",Convert.ToDecimal(model.comp_esic)},
                    { "tot_comp_con",Convert.ToDecimal(model.tot_comp_con)},
                    { "Designation",Convert.ToInt32(designationid)},
                    { "tot_ctc",Convert.ToDecimal(model.tot_ctc)},

                    { "effect_date",model.effect_date },
                    { "employee_id",model.employee_id},
                    });
                    msg = "Salary Saved";
                }
                catch (Exception ex)
                {
                    msg = "err" + ex;
                }
            }
            return msg;
        }

        public List<salary_master> GetEmployyeSalaryList()
        {
            List<salary_master> smlist = new List<salary_master>();
            string sql = "Select em.name,sm.* from salary_master sm,Employee_Master em where em.Employee_id=sm.employee_id order by Convert(date,sm.effect_date,103) desc";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    salary_master sm = new salary_master();
                    sm.id = Convert.ToInt32(dr["id"]);
                    sm.emp_hra = !Convert.IsDBNull(dr["emp_hra"]) ? Convert.ToDecimal(dr["emp_hra"]) : Convert.ToDecimal("0");
                    sm.emp_basic = !Convert.IsDBNull(dr["emp_basic"]) ? Convert.ToDecimal(dr["emp_basic"]) : Convert.ToDecimal("0");
                    sm.emp_Other_Allowance = !Convert.IsDBNull(dr["emp_Other_Allowance"]) ? Convert.ToDecimal(dr["emp_Other_Allowance"]) : Convert.ToDecimal("0");
                    sm.emp_Total_Earning = !Convert.IsDBNull(dr["emp_Total_Earning"]) ? Convert.ToDecimal(dr["emp_Total_Earning"]) : Convert.ToDecimal("0");
                    sm.emp_PTAX = !Convert.IsDBNull(dr["emp_PTAX"]) ? Convert.ToDecimal(dr["emp_PTAX"]) : Convert.ToDecimal("0");
                    sm.emp_PF = !Convert.IsDBNull(dr["emp_PF"]) ? Convert.ToDecimal(dr["emp_PF"]) : Convert.ToDecimal("0");
                    sm.emp_esic = !Convert.IsDBNull(dr["emp_esic"]) ? Convert.ToDecimal(dr["emp_esic"]) : Convert.ToDecimal("0");
                    sm.emp_tds = !Convert.IsDBNull(dr["emp_tds"]) ? Convert.ToDecimal(dr["emp_tds"]) : Convert.ToDecimal("0");
                    sm.tds_type = Convert.ToString(dr["tds_type"]);
                    sm.tot_emp_ded = !Convert.IsDBNull(dr["tot_emp_ded"]) ? Convert.ToDecimal(dr["tot_emp_ded"]) : Convert.ToDecimal("0");
                    sm.comp_pf = !Convert.IsDBNull(dr["comp_pf"]) ? Convert.ToDecimal(dr["comp_pf"]) : Convert.ToDecimal("0");
                    sm.comp_esic = !Convert.IsDBNull(dr["comp_esic"]) ? Convert.ToDecimal(dr["comp_esic"]) : Convert.ToDecimal("0");
                    sm.tot_comp_con = !Convert.IsDBNull(dr["tot_comp_con"]) ? Convert.ToDecimal(dr["tot_comp_con"]) : Convert.ToDecimal("0");
                    sm.emp_net = !Convert.IsDBNull(dr["emp_net"]) ? Convert.ToDecimal(dr["emp_net"]) : Convert.ToDecimal("0");
                    sm.tot_ctc = !Convert.IsDBNull(dr["tot_ctc"]) ? Convert.ToDecimal(dr["tot_ctc"]) : Convert.ToDecimal("0");
                    sm.effect_date = Convert.ToString(dr["effect_date"]);
                    sm.employee_id = Convert.ToString(dr["employee_id"]);
                    sm.name = Convert.ToString(dr["name"]);
                    smlist.Add(sm);
                }
            }
            return (smlist);
        }
        public salary_master GetEmployyeSalaryByid(string employee_id, string effect_date)
        {
            salary_master sm = new salary_master();
            string sql = "Select em.name,sm.* from salary_master sm,Employee_Master em where em.Employee_id=sm.employee_id and sm.employee_id='" + employee_id + "' and Convert(date,effect_date,103)=Convert(date,'" + effect_date + "',103) order by Convert(date,sm.effect_date,103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    sm.id = Convert.ToInt32(dr["id"]);
                    sm.emp_hra = !Convert.IsDBNull(dr["emp_hra"]) ? Convert.ToDecimal(dr["emp_hra"]) : Convert.ToDecimal("0");
                    sm.emp_basic = !Convert.IsDBNull(dr["emp_basic"]) ? Convert.ToDecimal(dr["emp_basic"]) : Convert.ToDecimal("0");
                    sm.emp_Other_Allowance = !Convert.IsDBNull(dr["emp_Other_Allowance"]) ? Convert.ToDecimal(dr["emp_Other_Allowance"]) : Convert.ToDecimal("0");
                    sm.emp_tds = !Convert.IsDBNull(dr["emp_tds"]) ? Convert.ToDecimal(dr["emp_tds"]) : Convert.ToDecimal("0");
                    sm.tds_type = Convert.ToString(dr["tds_type"]);

                    sm.emp_Total_Earning = !Convert.IsDBNull(dr["emp_Total_Earning"]) ? Convert.ToDecimal(dr["emp_Total_Earning"]) : Convert.ToDecimal("0");
                    sm.emp_PTAX = !Convert.IsDBNull(dr["emp_PTAX"]) ? Convert.ToDecimal(dr["emp_PTAX"]) : Convert.ToDecimal("0");
                    sm.emp_PF = !Convert.IsDBNull(dr["emp_PF"]) ? Convert.ToDecimal(dr["emp_PF"]) : Convert.ToDecimal("0");
                    sm.emp_esic = !Convert.IsDBNull(dr["emp_esic"]) ? Convert.ToDecimal(dr["emp_esic"]) : Convert.ToDecimal("0");
                    sm.tot_emp_ded = !Convert.IsDBNull(dr["tot_emp_ded"]) ? Convert.ToDecimal(dr["tot_emp_ded"]) : Convert.ToDecimal("0");
                    sm.comp_pf = !Convert.IsDBNull(dr["comp_pf"]) ? Convert.ToDecimal(dr["comp_pf"]) : Convert.ToDecimal("0");
                    sm.comp_esic = !Convert.IsDBNull(dr["comp_esic"]) ? Convert.ToDecimal(dr["comp_esic"]) : Convert.ToDecimal("0");
                    sm.tot_comp_con = !Convert.IsDBNull(dr["tot_comp_con"]) ? Convert.ToDecimal(dr["tot_comp_con"]) : Convert.ToDecimal("0");
                    sm.emp_net = !Convert.IsDBNull(dr["emp_net"]) ? Convert.ToDecimal(dr["emp_net"]) : Convert.ToDecimal("0");
                    sm.tot_ctc = !Convert.IsDBNull(dr["tot_ctc"]) ? Convert.ToDecimal(dr["tot_ctc"]) : Convert.ToDecimal("0");
                    sm.effect_date = Convert.ToString(dr["effect_date"]);
                    sm.employee_id = Convert.ToString(dr["employee_id"]);
                    sm.name = Convert.ToString(dr["name"]);

                }
            }
            return (sm);
        }
        public List<salary_master> getfinancal_year(string employee_id)
        {
            int i = 0;
            List<salary_master> smlist = new List<salary_master>();
            string sql = "Select * from salary_master where employee_id='" + employee_id + "' order by convert(date,effect_date,103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 0)
                    {
                        salary_master sm = new salary_master();

                        sm.financial_year = "Select";
                        smlist.Add(sm);
                    }

                    salary_master sm1 = new salary_master();
                    sm1.effect_date = Convert.ToString(dr["effect_date"]);                    
                    sm1.financial_year = Convert.ToDateTime(sm1.effect_date, usCinfo).Year.ToString() + "-" + (Convert.ToDateTime(sm1.effect_date, usCinfo).Year + 1).ToString();
                    if(Convert.ToDateTime(sm1.effect_date, usCinfo)>= Convert.ToDateTime("01/04/"+sm1.financial_year.Substring(0,4), usCinfo) && Convert.ToDateTime(sm1.effect_date, usCinfo) <= Convert.ToDateTime("31/03/" + sm1.financial_year.Substring(5, 4), usCinfo))
                    {
                        smlist.Add(sm1);
                    }
                    

                    i++;
                }
            }
            return (smlist);
        }
        public List<salary_master> geteffectdate(string finyear, string employee_id)
        {
            int i = 0;
            string frdt = "01/04/" + finyear.Substring(0, 4);
            string todt = "31/03/" + finyear.Substring(5, 4);

            List<salary_master> smlist = new List<salary_master>();

            string sql = "Select * from salary_master where employee_id='" + employee_id + "'and convert(date,effect_date,103)>= convert(date,'" + frdt + "',103) and convert(date,effect_date,103)<= convert(date,'" + todt + "',103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    if (i == 0)
                    {
                        salary_master sm1 = new salary_master();
                        sm1.effect_date = "Select Date";
                        smlist.Add(sm1);
                    }
                    salary_master sm = new salary_master();
                    sm.effect_date = Convert.ToString(dr["effect_date"]);
                    smlist.Add(sm);
                    i = i + 1;
                }
            }
            return (smlist);
        }
        public salary_master GetSalaryDetailByEmployeeid(string empid,string to_date)
        {
            salary_master sm = new salary_master();
            string sql = "select * from salary_master where employee_id='" + empid + "' and convert(date,effect_date,103)<= convert(date,'"+ to_date + "',103) order by convert(date,effect_date,103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                sm.emp_net = !Convert.IsDBNull(dr["emp_net"]) ? Convert.ToDecimal(dr["emp_net"]) : Convert.ToDecimal(0);
            }
            return sm;
        }
        public List<Salary_Ledger> getPayslip_year(string employee_id)
        {
            int i = 0;
            List<Salary_Ledger> smlist = new List<Salary_Ledger>();
            string sql = "Select distinct year from salary_ledger where employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 0)
                    {
                        Salary_Ledger sm = new Salary_Ledger();

                        sm.year = "Select";
                        smlist.Add(sm);
                    }

                    Salary_Ledger sm1 = new Salary_Ledger();
                    sm1.year = Convert.ToString(dr["year"]);
                    smlist.Add(sm1);

                    i++;
                }
            }
            return (smlist);
        }
    }
}
