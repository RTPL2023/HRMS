
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
    public class Employee_Document
    {
        SQLConfig config = new SQLConfig();
        public string employee_id { get; set; }
        public Byte[] bank_detail { get; set; }
        public Byte[] aadhar { get; set; }
        public Byte[] pan { get; set; }
        public Byte[] voter_id { get; set; }
        public Byte[] joining_letter { get; set; }
        public Byte[] police_verification { get; set; }
        public Byte[] permanent_letter { get; set; }
        public Byte[] employee_img { get; set; }
        public string updateDocument(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            var fs = new MemoryStream();

          

            string sql = "select * from Employee_document where employee_id ='" + model.employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {
                config.Insert("Employee_document", new Dictionary<string, object>()
                {
                    { "employee_id",model.employee_id },
                   

                });
            }



             sql = "select * from Employee_document where employee_id ='" + model.employee_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                
                if (model.bank_detail != null)
                {
                    //sql = "update Employee_document set ";
                    model.bank_detail.CopyTo(fs);
                    em.bank_detail = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "bank_detail", em.bank_detail},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });
                    //sql += "bank_detail=" + em.bank_detail + " ";
                    //sql += "where employee_id='" + model.employee_id + "'";
                    //config.Execute_Query(sql);

                }
                if (model.aadhar != null)
                {
                   
                    model.aadhar.CopyTo(fs);
                    em.aadhar = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "aadhar", em.aadhar},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }
                if (model.pan != null)
                {
                  
                    model.pan.CopyTo(fs);
                    em.pan = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "pan", em.pan},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }
                if (model.joining_letter != null)
                {
                   
                    model.joining_letter.CopyTo(fs);
                    em.joining_letter = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "joining_letter", em.joining_letter},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }
                if (model.police_verification != null)
                {
                  
                    model.police_verification.CopyTo(fs);
                    em.police_verification = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "police_verification", em.police_verification},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }
                if (model.permanent_letter != null)
                {
                 
                    model.permanent_letter.CopyTo(fs);
                    em.permanent_letter = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "permanent_letter", em.permanent_letter},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });
                }
                if (model.voter_id != null)
                {
                    
                    model.voter_id.CopyTo(fs);
                    em.voter_id = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "voter_id", em.voter_id},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }
                if (model.employee_img != null)
                {
                    
                    model.employee_img.CopyTo(fs);
                    em.employee_img = fs.ToArray();
                    config.Update("Employee_document", new Dictionary<String, object>()
                    {
                        { "employee_img", em.employee_img},
                    }, new Dictionary<string, object>()
                      {
                      { "employee_id", model.employee_id }
                    });

                }


            }
            else
            {
                //model.bank_detail.CopyTo(fs);
                //em.bank_detail = fs.ToArray();
                //config.Insert("Employee_document", new Dictionary<string, object>()
                //{
                //    { "employee_id",model.employee_id },
                //    { "bank_detail",em.bank_detail },
                //    { "aadhar",em.aadhar },
                //    { "pan",em.pan },
                //    { "joining_letter",em.joining_letter },
                //    { "police_verification",em.police_verification },
                //    { "permanent_letter",em.permanent_letter},
                //    { "employee_img",em.employee_img },
                //    { "voter_id",em.voter_id },

                //});

            }


            return "Saved";


        }
        public Employee_Document getdocument(string employee_id)
        {

            Employee_Document ed = new Employee_Document();

            string sql = "Select * from Employee_Document where employee_id='" + employee_id+"'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    ed.bank_detail = dr["bank_detail"] != System.DBNull.Value ? (byte[])dr["bank_detail"] : null;
                    ed.aadhar = dr["aadhar"] != System.DBNull.Value ? (byte[])dr["aadhar"] : null;
                    ed.pan = dr["pan"] != System.DBNull.Value ? (byte[])dr["pan"] : null;
                    ed.voter_id = dr["voter_id"] != System.DBNull.Value ? (byte[])dr["voter_id"] : null;
                    ed.joining_letter = dr["joining_letter"] != System.DBNull.Value ? (byte[])dr["joining_letter"] : null;
                    ed.police_verification = dr["police_verification"] != System.DBNull.Value ? (byte[])dr["police_verification"] : null;
                    ed.permanent_letter = dr["permanent_letter"] != System.DBNull.Value ? (byte[])dr["permanent_letter"] : null;
                    ed.employee_img = dr["employee_img"] != System.DBNull.Value ? (byte[])dr["employee_img"] : null;
                }
            }
            return ed;
        }
    }
}
