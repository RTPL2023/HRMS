﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace HRMS.Controllers
{
    // [Authorize]
    public class MasterController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult employee_master(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            List<Employee_Master> emlst = new List<Employee_Master>();
            emlst = em.getemployeelists();
            ViewBag.emlist = emlst;
            return View(model);
        }

        public JsonResult SaveEmployeedetails(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            string msg = em.saveEmployeedetails(model,User.Identity.Name);

            return Json(msg);
        }
        [HttpGet]
        public IActionResult EmployeeDocumentAndOtherDetailsUpload(string id)
        {
            documentUploadViewModel model = new documentUploadViewModel();
            Employee_Emergency_Details ecd = new Employee_Emergency_Details();
            ecd = ecd.getemployeeEmergency_Details(id);
            model.emg_contact_name = ecd.emg_contact_name;
            model.emg_contact_relation = ecd.emg_contact_relation;
            model.emg_contact_no = ecd.emg_contact_no;
            Employee_bankid_details ebd = new Employee_bankid_details();
            ebd = ebd.getemployeeBankdetails(id);
            model.ac_name = ebd.ac_name;
            model.ac_no = ebd.ac_no;
            model.bank_name = ebd.bank_name;
            model.branch_name = ebd.branch_name;
            model.ifsc_code = ebd.ifsc_code;
            model.branch_code = ebd.branch_code;
            model.aadhar_no = ebd.aadhar_no;
            model.pan_no = ebd.pan_no;
            model.voter_id_no = ebd.voter_id_no;
            model.pf_no = ebd.pf_no;
            model.esic_no = ebd.esic_no;
            model.passport_no = ebd.passport_no;
            model.employee_id = id;
            return View(model);
        }
        public string UpdateEmployeeDetails(documentUploadViewModel model)
        {
            Employee_Master em = new Employee_Master();
            em.updateEmployeedetails(model);
            return ("over");
        }
        public JsonResult getEmployeeList(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            List<Employee_Master> emlst = new List<Employee_Master>();
            emlst = em.getemployeelists();
            string tableemenent = "";
            if (emlst.Count > 0)
            {
                tableemenent = "<tr><th>Name</th><th>Employee Id</th><th>Edit</th></tr>";
                foreach (var a in emlst)
                {

                    tableemenent = tableemenent + "<tr><td>" + a.name + "</td><td>" + a.employee_id + "</td><td><a href = '" + @Url.Action("Employee_Profile", "Master", new { id = a.employee_id }) + "' class = \"table__icon edit\"><i class = \"fa-solid fa-pen fa-lg\"></i></a></td></tr>";


                }
            }
            return Json(tableemenent);
        }
        [HttpGet]
        public IActionResult DesignationsMaster(employee_masterViewModel model)
        {

            return View(model);
        }

        public JsonResult Savedesignation(employee_masterViewModel model)
        {
            Designation_Master em = new Designation_Master();
            string msg = em.saveDesignationMaster(model.designation);

            return Json(msg);
        }
        public JsonResult getdesignationByid(int id)
        {
            Designation_Master em = new Designation_Master();
            em = em.getdesignationByid(id);

            return Json(em);
        }
        public JsonResult editdesignationByid(employee_masterViewModel model)
        {
            Designation_Master em = new Designation_Master();
            string msg = em.modifydesignationByid(model);

            return Json(msg);
        }
        public JsonResult deletedesignationByid(employee_masterViewModel model)
        {
            Designation_Master em = new Designation_Master();
            string msg = em.deletedesignationByid(model);

            return Json(msg);
        }
        public JsonResult getDesignationsMasterList()
        {
            Designation_Master dm = new Designation_Master();
            List<Designation_Master> dmlst = new List<Designation_Master>();
            dmlst = dm.getDesignation_Masterlists();
            string tableemenent = "";
            if (dmlst.Count > 0)
            {
                tableemenent = "<tr><th>Designation</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in dmlst)
                {

                    tableemenent = tableemenent + "<tr><td>" + a.designation + "</td>" +
                        "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#designationedit\" onclick=btnEditOnclick('" + a.id + "')> <i class=\"fa-solid fa-pen\"></i></ button></td>" +
                        "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#designationDelete\" onclick=btnEditOnclick('" + a.id + "')> <i class=\"fa-solid fa-trash\"></i></ button ></td></tr>";


                }
            }
            return Json(tableemenent);
        }
        [HttpGet]
        public IActionResult DepartmentMaster(employee_masterViewModel model)
        {

            return View(model);
        }

        public JsonResult Savedepartment(employee_masterViewModel model)
        {
            Department_Master em = new Department_Master();
            string msg = em.saveDepartment_Master(model);

            return Json(msg);
        }
        public JsonResult getdepartmentByid(int id)
        {
            Department_Master em = new Department_Master();
            em = em.getdepartmentByid(id);

            return Json(em);
        }
        public JsonResult editDepartmentByid(employee_masterViewModel model)
        {
            Department_Master em = new Department_Master();
            string msg = em.modifydepartmentByid(model);

            return Json(msg);
        }
        public JsonResult deleteDepartmentByid(employee_masterViewModel model)
        {
            Department_Master em = new Department_Master();
            string msg = em.deletedepartmentByid(model);

            return Json(msg);
        }
        public JsonResult getDepartmentMasterList()
        {
            Department_Master dm = new Department_Master();
            List<Department_Master> dmlst = new List<Department_Master>();
            dmlst = dm.getDepartment_Masterlists();
            string tableemenent = "";
            if (dmlst.Count > 0)
            {
                tableemenent = "<tr><th>DepartmentMaster</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in dmlst)
                {

                    tableemenent = tableemenent + "<tr><td>" + a.department + "</td>" +
                        "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#Departmentedit\" onclick=btnEditOnclick('" + a.id + "')> <i class=\"fa-solid fa-pen\"></i></ button></td>" +
                        "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#DepartmentDelete\" onclick=btnEditOnclick('" + a.id + "')> <i class=\"fa-solid fa-trash\"></i></ button ></td></tr>";


                }
            }
            return Json(tableemenent);
        }

        [HttpGet]
        public IActionResult Employee_Profile(string id)
        {
            employee_masterViewModel model = new employee_masterViewModel();
            if (id != null && id != "")
            {
                model.employee_id = id;
            }
            else
            {
                model.employee_id = User.Identity.Name;
            }
           
            return View(model);
        }
        public JsonResult getEmployeeDataByEmployeeId( string employee_id)
        {

            Employee_Master em = new Employee_Master();

            var data = em.getEmployeeDetailByemployee_id(employee_id);
            return Json(data);
        }
        public JsonResult UpdateEmployeedetailsByid(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            string msg = em.UpdateEmployeedetailsByEmployee_id(model,User.Identity.Name);

            return Json(msg);
        }
        public JsonResult getemployeeEmargencyContactDetails(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            var data = em.getEmargencyContact(model.employee_id);

            return Json(data);
        }
        public JsonResult UpdateEmployee_EmergencybyEmployee_id(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            string msg = em.SAVEUpdateEmployee_Emergency_details(model);
            return Json(msg);
        }
        public JsonResult getemployeeBanktDetails(employee_masterViewModel model)
        {
            Employee_bankid_details ebd = new Employee_bankid_details();
            var data = ebd.getemployeeBankdetails(model.employee_id);

            return Json(data);
        }
        public JsonResult saveupdateBankAndOtherDocument(employee_masterViewModel model)
        {
            Employee_bankid_details ebd = new Employee_bankid_details();
            var data = ebd.SaveUpdateBankdetails(model);

            return Json(data);
        }
        public JsonResult EmployeeDocumentUoload(employee_masterViewModel model)
        {
            Employee_Document ed = new Employee_Document();
            var data = ed.updateDocument(model);

            return Json(data);
        }
        public JsonResult getdocumentbyemployee_id(employee_masterViewModel model)
        {
            Employee_Document ed = new Employee_Document();
            var data = ed.getdocument(model.employee_id);

            return Json(data);
        }

        public IActionResult DownloadDocument(string value,string employee_id)
        {
       
            Employee_Document ed = new Employee_Document();
            ed= ed.getdocument(employee_id);
            if (value== "bank_detail" && ed.aadhar != null)
            {
                var document = ed.bank_detail;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "bank_detail_"+ employee_id + ".pdf");
            }
            if (value == "aadhar" && ed.aadhar !=null)
            {
                var document = ed.aadhar;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "aadhar_" + employee_id + ".pdf");
            }
            if (value == "pan" && ed.pan != null)
            {
                var document = ed.pan;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "pan_"+ employee_id + ".pdf");
            }
            if (value == "voter_id" && ed.voter_id != null)
            {
                var document = ed.voter_id;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "voter_id_"+ employee_id + ".pdf");
            }
            if (value == "joining_letter" && ed.joining_letter != null)
            {
                var document = ed.joining_letter;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "joining_letter_"+ employee_id + ".pdf");
            } 
            if (value == "police_verification" && ed.police_verification != null)
            {
                var document = ed.police_verification;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "police_verification_"+ employee_id + ".pdf");
            } 
            if (value == "permanent_letter" && ed.permanent_letter != null)
            {
                var document = ed.permanent_letter;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "permanent_letter_"+ employee_id + ".pdf");
            } 
            else
            {

               
                return Json("");

            }
        }

        public JsonResult getphoto(string employee_id)
        {
            var data = "";

            Employee_Document ed = new Employee_Document();
            ed = ed.getdocument(employee_id);
            if (ed.employee_img != null)
            {
                data= "data:image/jpg;base64," + Convert.ToBase64String(ed.employee_img);  
            }
            return Json(data);
        }
    }
}
