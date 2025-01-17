using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
namespace HRMS.Controllers
{
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
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
            string msg = em.saveEmployeedetails(model);

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

                    tableemenent = tableemenent + "<tr><td>" + a.name + "</td><td>" + a.employee_id + "</td><td><a href = '" + @Url.Action("EmployeeDocumentAndOtherDetailsUpload", "Master", new { id = a.employee_id }) + "' class = \"fa-solid fa-pen fa-lg\"></a></td></tr>";


                }
            }
            return Json(tableemenent);
        }
    }
}
