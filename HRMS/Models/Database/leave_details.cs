﻿using HRMS.Models.ViewModel;
using HRMS.Models.DataBase;
using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using HRMS.Controllers;
using System.Globalization;

namespace HRMS.Models.Database
{
    public class leave_details
    {
        UtilityController u = new UtilityController();
        DateTimeFormatInfo usCinfo = new CultureInfo("en-GB", false).DateTimeFormat;
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string employee_id { get; set; }
        public string name { get; set; }
        public string leave_type { get; set; }
        public string leave_duration { get; set; }
        public string leave_from_date { get; set; }
        public string leave_to_date { get; set; }
        public string leave_reason { get; set; }
        public string apply_date { get; set; }
        public int is_approved { get; set; }
        public string manager_remarks { get; set; }
        public string approved_rejected_by { get; set; }
        public string approved_rejected_on { get; set; }


        public string UpdateLeaveApplydetails(LeaveApplyViewModel model, string employee_id)
        {
        
            string msg = "";
            string sql = "Select * from leave_details where employee_id='" + employee_id + "' and id=" + model.id + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                int lv_ledger_id = Convert.ToInt32(dr["lv_ledger_id"]);
                string lv_hd = Convert.ToString(dr["leave_type"]);
                sql = "delete from leave_ledger where employee_id='" + employee_id + "' and id=" + lv_ledger_id + "";
                config.Execute_Query(sql);
                sql = "delete from leave_details where employee_id='" + employee_id + "' and id=" + model.id + "";
                config.Execute_Query(sql);
                ResetLeaveLedger(lv_hd, employee_id);

                msg = "Leave Updated successfully!";
            }

            return (msg);
        }

        public void ResetLeaveLedger(string lv_hd, string employee_id)
        {
            Leave_ledger ld = new Leave_ledger();
            decimal lbal_prin = 0;
           
            int i = 1;
          
            string sql = "SELECT * FROM Leave_ledger WHERE lv_hd='" + lv_hd + "' AND employee_id='" + employee_id + "' ORDER BY id";
            config.singleResult(sql);

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 1)
                    {

                        lbal_prin = !Convert.IsDBNull(dr["lv_amount"]) ? Convert.ToDecimal(dr["lv_amount"]) : Convert.ToDecimal(00);


                       
                        ld.id = Convert.ToInt32(dr["id"]);

                        string qry = "Update Leave_ledger set lv_balance=" + lbal_prin + " where id=" + ld.id + "";
                        config.Execute_Query(qry);
                      
                    }
                    else
                    {
                        ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                        if (ld.dr_cr == "D")
                        {
                            lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["lv_amount"]) ? Convert.ToDecimal(dr["lv_amount"]) : Convert.ToDecimal(00));
                           
                        }
                        else
                        {
                            lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["lv_amount"]) ? Convert.ToDecimal(dr["lv_amount"]) : Convert.ToDecimal(00));
                            
                        }

                        ld.id = Convert.ToInt32(dr["id"]);

                        string qry = "Update Leave_ledger set lv_balance=" + lbal_prin + " where id=" + ld.id + "";
                        config.Execute_Query(qry);

                       
                    }

                    i = i + 1;

                }

            }







        }
        public List<leave_details> getemployeeLeaveListByEmployee_id(string employee_id)
        {
            List<leave_details> ldlist = new List<leave_details>();
            string sql = "Select * from leave_details Where employee_Id='" + employee_id + "' order by id desc";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    leave_details ld = new leave_details();
                    ld.id = Convert.ToInt32(dr["id"]);
                    ld.leave_type = Convert.ToString(dr["leave_type"]);
                    ld.leave_duration = Convert.ToString(dr["leave_duration"]);
                    ld.leave_from_date = Convert.ToString(dr["leave_from_date"]);
                    ld.leave_to_date = Convert.ToString(dr["leave_to_date"]);
                    ld.leave_reason = Convert.ToString(dr["leave_reason"]);
                    ld.apply_date = Convert.ToString(dr["apply_date"]);
                    ld.is_approved = !Convert.IsDBNull(dr["is_approved"]) ? Convert.ToInt32(dr["is_approved"]) : Convert.ToInt32("2");
                    ld.manager_remarks = Convert.ToString(dr["manager_remarks"]);
                    ld.approved_rejected_on = Convert.ToString(dr["approved_rejected_on"]);
                    ldlist.Add(ld);
                }
            }
            return (ldlist);
        }
        public string SaveLeaveApplydetails(LeaveApplyViewModel model, string employee_id)
        {
            string msg = "";
            string dt = "";
            string sql = "";
            decimal leave_bal = 0;
            string lv_dur = "0";
           
            if (Convert.ToDecimal(model.leave_duration) == Convert.ToDecimal(0.5))
            {
                lv_dur = "1";
            }
            else
            {
                lv_dur = model.leave_duration;
            }
            //model.leave_from_date = DateTime.ParseExact(model.leave_from_date, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy").Replace("-", "/");
             model.leave_from_date = Convert.ToDateTime(model.leave_from_date, usCinfo).ToString("dd/MM/yyyy").Replace("-", "/");

            //model.leave_to_date = DateTime.ParseExact(model.leave_from_date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(Convert.ToInt32(lv_dur) - 1).ToString("dd/MM/yyyy").Replace("-", "/");


            model.leave_to_date = Convert.ToDateTime(model.leave_from_date, usCinfo).AddDays(Convert.ToInt32(lv_dur) - 1).ToString("dd/MM/yyyy").Replace("-", "/");

            sql = "select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='" + model.leave_type + "' order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                leave_bal = !Convert.IsDBNull(dr["lv_balance"]) ? Convert.ToDecimal(dr["lv_balance"]) : Convert.ToDecimal(0);
            }
            if (Convert.ToDecimal(model.leave_duration) <= leave_bal)
            {
                for (int i = 0; i < Convert.ToInt32(lv_dur); i++)
                {
                    dt = Convert.ToDateTime(model.leave_from_date, usCinfo).AddDays(i).ToString("dd/MM/yyyy").Replace("-", "/");
                    //dt= DateTime.ParseExact(model.leave_from_date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(i).ToString("dd/MM/yyyy").Replace("-", "/");


                    sql = "Select * from leave_details where employee_id='" + employee_id + "' and Convert(date,'" + dt + "',103)>=Convert(date,leave_from_date,103) and Convert(date,'" + dt + "',103)<= Convert(date,leave_to_date,103) and is_approved<>0";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        msg = "Leave Alrady Applied! For " + dt;
                        break;
                    }
                    if (model.leave_to_date == dt)
                    {
                        string date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                        config.Insert("Leave_ledger", new Dictionary<string, object>()
                            {
                            { "employee_id",employee_id },
                            { "lv_hd",model.leave_type },
                            { "date",date},
                            { "lv_amount",Convert.ToDecimal(model.leave_duration)},
                            { "dr_cr","D"},
                            { "lv_balance",leave_bal-Convert.ToDecimal(model.leave_duration)},
                            { "remarks","Debit For Leave Apply"},
                            { "create_date",date},
                            { "created_by",employee_id},
                            });
                        sql = "Select * from Leave_ledger where employee_id='" + employee_id + "' and lv_hd='"+model.leave_type+"' and date='"+ date + "' and dr_cr='D' " +
                            "and create_date='"+ date + "' and lv_amount="+ Convert.ToDecimal(model.leave_duration) + " and remarks='Debit For Leave Apply'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            config.Insert("leave_details", new Dictionary<string, object>()
                            {
                            { "employee_id",employee_id },
                            { "leave_type",model.leave_type },
                            { "leave_duration",model.leave_duration },
                            { "leave_from_date",model.leave_from_date},
                            { "leave_to_date",model.leave_to_date},
                            { "leave_reason",model.leave_reason},
                            { "apply_date",date},
                            { "lv_ledger_id",Convert.ToInt32(dr["id"])},
                            });
                        }
                           
                        msg = "Leave Applied successfully!";
                    }
                }

            }
            else
            {
                msg = "Leave Balance Cannot Be Less Than Applied Leave Duration";
            }


            //sql = "Select * from leave_details where employee_id='" + employee_id + "' and leave_from_date='" + model.leave_from_date + "' and leave_to_date='" + model.leave_to_date + "'";
            //config.singleResult(sql);
            //if (config.dt.Rows.Count == 0)
            //{

            //    config.Insert("leave_details", new Dictionary<string, object>()
            //        {
            //            { "employee_id",employee_id },
            //            { "leave_type",model.leave_type },
            //            { "leave_duration",model.leave_duration },
            //            { "leave_from_date",model.leave_from_date},
            //            { "leave_to_date",model.leave_to_date},
            //            { "leave_reason",model.leave_reason},
            //            { "apply_date",DateTime.Now.ToString("dd/MM/yyyy").Replace("-","/")},


            //        });
            //    msg = "Leave Applied successfully!";

            //}
            //else
            //{
            //    msg = "Alrady Applied!";
            //}
            return (msg);
        }
        public List<leave_details> getleaveDetailsbyemployee_id(string empl_id)
        {
            List<leave_details> ldlist = new List<leave_details>();
            string sql = "Select * from Employee_Master Where Reporting_mg_id=" + empl_id + " order by id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in config.dt.Rows)
                {
                   
                    string employee_id = Convert.ToString(dr1["employee_id"]);
                    sql = "Select * from leave_details Where employee_Id='" + employee_id + "' and is_approved is NUll order by id";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in config.dt.Rows)
                        {
                            leave_details ld = new leave_details();
                            ld.name = Convert.ToString(dr1["name"]);
                            ld.employee_id = Convert.ToString(dr["employee_id"]);
                            ld.id = Convert.ToInt32(dr["id"]);
                            ld.leave_type = Convert.ToString(dr["leave_type"]);
                            ld.leave_duration = Convert.ToString(dr["leave_duration"]);
                            ld.leave_from_date = Convert.ToString(dr["leave_from_date"]);
                            ld.leave_to_date = Convert.ToString(dr["leave_to_date"]);
                            ld.leave_reason = Convert.ToString(dr["leave_reason"]);
                            ld.apply_date = Convert.ToString(dr["apply_date"]);
                            ld.is_approved = !Convert.IsDBNull(dr["is_approved"]) ? Convert.ToInt32(dr["is_approved"]) : Convert.ToInt32("2");
                            ld.manager_remarks = Convert.ToString(dr["manager_remarks"]);
                            ld.approved_rejected_on = Convert.ToString(dr["approved_rejected_on"]);
                            ldlist.Add(ld);
                        }
                    }
                }
            }

            return (ldlist);
        }
        public string UpdateApprovrdLeavebyEmployeeId(string employee_id, string id, string appreason, string user)
        {
            string msg = "";
            string sql = "Select * from leave_details Where employee_Id='" + employee_id + "' and id='" + id + "'  ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {

                //---- leave_details update------

                sql = "update leave_details set ";
                sql = sql + "is_approved=1,";
                sql = sql + "manager_remarks='" + appreason + "',";
                sql = sql + "approved_rejected_by='" + user + "',";
                sql = sql + "approved_rejected_on='" + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and id='" + id + "'";
                config.Execute_Query(sql);

            }
            msg = "Leave Approved";
            return (msg);
        }
        public string UpdateRejectedLeavebyEmployeeId(string employee_id, string id, string rejleavereason, string user)
        {
            string msg = "";
            string sql = "Select * from leave_details Where employee_Id='" + employee_id + "' and id=" + id + "";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {

                //---- leave_details update------

                sql = "update leave_details set ";
                sql = sql + "is_approved=0,";
                sql = sql + "manager_remarks='" + rejleavereason + "',";
                sql = sql + "approved_rejected_by='" + user + "',";
                sql = sql + "approved_rejected_on='" + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "'";
                sql = sql + "where ";
                sql = sql + "employee_Id='" + employee_id + "' and id=" + id + "";
                config.Execute_Query(sql);

                 sql = "Select * from leave_details where employee_id='" + employee_id + "' and id=" + id + "";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr = (DataRow)config.dt.Rows[0];
                    int lv_ledger_id = Convert.ToInt32(dr["lv_ledger_id"]);
                    string lv_hd = Convert.ToString(dr["leave_type"]);
                    sql = "delete from leave_ledger where employee_id='" + employee_id + "' and id=" + lv_ledger_id + "";
                    config.Execute_Query(sql);
                    ResetLeaveLedger(lv_hd, employee_id);
                }
            }
            msg = "Leave Rejected";
            return (msg);
        }

    }
}
