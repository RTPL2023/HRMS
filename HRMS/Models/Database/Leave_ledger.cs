using HRMS.Models.ViewModel;
using HRMS.Models.DataBase;
using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Controllers;

namespace HRMS.Models.Database
{
    public class Leave_ledger
    {
        UtilityController u = new UtilityController();

        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }
        public string lv_hd { get; set; }
        public string date { get; set; }
        public decimal lv_amount { get; set; }
        public string dr_cr { get; set; }
        public decimal lv_balance { get; set; }
        public decimal sl_balance { get; set; }
        public decimal cl_balance { get; set; }
        public decimal el_balance { get; set; }
        public decimal co_balance { get; set; }

        public string remarks { get; set; }
        public string create_date { get; set; }
        public string modify_date { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string compoff_against { get; set; }


        public string refreshleaveBalance(string employee_id, string date)
        {
            string employment_type = "";
            string msg = "";
            decimal lv_balance = 0;
            string sql = "select * from Employee_Master where employee_id='" + employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                employment_type = Convert.ToString(dr["employment_type"]);
                sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and date='" + date + "' and remarks='Credit' and dr_cr='C'";
                config.singleResult(sql);
                if (config.dt.Rows.Count == 0)
                {
                    if (employment_type == "Permanent")
                    {
                        sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='SL' order by id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count-1];
                            lv_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
                        }
                        config.Insert("Leave_ledger", new Dictionary<string, object>()
                            {
                            { "employee_id",employee_id },
                            { "lv_hd","SL" },
                            { "date",date},
                            { "lv_amount",0.5},
                            { "dr_cr","C"},
                            { "lv_balance",lv_balance+Convert.ToDecimal(0.5)},
                            { "remarks","Credit"},
                            { "create_date",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                            { "created_by",employee_id},
                            });
                        lv_balance = 0;
                        sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='CL' order by id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            lv_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
                        }
                       
                        config.Insert("Leave_ledger", new Dictionary<string, object>()
                        {
                            { "employee_id",employee_id },
                            { "lv_hd","CL" },
                            { "date",date},
                            { "lv_amount",0.5},
                            { "dr_cr","C"},
                            { "lv_balance",lv_balance+ Convert.ToDecimal(0.5)},
                            { "remarks","Credit"},
                            { "create_date",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                            { "created_by",employee_id},


                        });
                        lv_balance = 0;
                        sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='EL' order by id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            lv_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
                        }
                        config.Insert("Leave_ledger", new Dictionary<string, object>()
                        {
                            { "employee_id",employee_id },
                            { "lv_hd","EL" },
                            { "date",date},
                            { "lv_amount",1},
                            { "dr_cr","C"},
                            { "lv_balance",lv_balance+ Convert.ToDecimal(1)},
                            { "remarks","Credit"},
                            { "create_date",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                            { "created_by",employee_id},


                        });
                        msg = "Leave Credited Successfully";
                    }
                    else
                    {

                        sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='SL' order by id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            lv_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
                        }
                        config.Insert("Leave_ledger", new Dictionary<string, object>()
                            {
                            { "employee_id",employee_id },
                            { "lv_hd","SL" },
                            { "date",date},
                            { "lv_amount",0.5},
                            { "dr_cr","C"},
                            { "lv_balance",lv_balance+Convert.ToDecimal(0.5)},
                            { "remarks","Credit"},
                            { "create_date",u.currentDateTime().ToString("dd/MM/yyyy").Replace("-","/")},
                            { "created_by",employee_id},
                            });
                        msg = "Leave Credited Successfully";

                    }
                }
                else
                {
                    msg = "Leave For This Month Already Credited";
                }
            }
            return msg;

        }

        public Leave_ledger getleaveBalance(string employee_id)
        {
            Leave_ledger ll = new Leave_ledger();
           string sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='SL' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count- 1];
                ll.sl_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
            }
             sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='CL' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ll.cl_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
            }
             sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='EL' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ll.el_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
            }
            sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='CO' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ll.co_balance = !Convert.IsDBNull(drsl["lv_balance"]) ? Convert.ToDecimal(drsl["lv_balance"]) : Convert.ToDecimal("0");
            }
            return ll;
        }

        public List<Leave_ledger> getleaveLedger(string empl_id)
        {
            List<Leave_ledger> ealst = new List<Leave_ledger>();
            string sql = "Select* from Leave_ledger Where employee_Id='" + empl_id + "' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Leave_ledger ea = new Leave_ledger();
                    ea.id = Convert.ToInt32(dr["id"]);
                    ea.employee_id = Convert.ToString(dr["employee_id"]);
                    ea.date = Convert.ToString(dr["date"]);
                    ea.lv_hd = Convert.ToString(dr["lv_hd"]);
                    ea.lv_amount = Convert.ToDecimal(dr["lv_amount"]);
                    ea.dr_cr = Convert.ToString(dr["dr_cr"]);
                    ea.lv_balance = Convert.ToDecimal(dr["lv_balance"]);
                    ea.remarks = Convert.ToString(dr["remarks"]);
                    ealst.Add(ea);
                }
            }
            return (ealst);
        }
    }
}
