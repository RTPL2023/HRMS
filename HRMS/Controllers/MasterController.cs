using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using HRMS.Models.DataBase;

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
            string msg = em.saveEmployeedetails(model, User.Identity.Name);

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
                //tableemenent = "<tr><th>Name</th><th>Employee Id</th><th>Edit</th></tr>";
                foreach (var a in emlst)
                {

                    //tableemenent = tableemenent + "<tr><td>" + a.name + "</td><td>" + a.employee_id + "</td><td><a href = '" + @Url.Action("Employee_Profile", "Master", new { id = a.employee_id }) + "' class = \"table__icon edit\"><i class = \"fa-solid fa-pen fa-lg\"></i></a></td></tr>";
                    tableemenent = tableemenent + "<div class=\"col-12 col-sm-3 col-md-3  col-lg-4 mt-2\"><div class=\"card\"><div class=\"card-body text-center\"><img src =\""+ a.image + "\" width =\"100\" height=\"100\" class=\"rounded-circle\"/><h5 class=\"card-title\">" + a.name + "<h5> <p class=\"fw-light fs-5\">" + a.designationName + "</p><p class=\"fw-light fs-6\">" + a.departmetName + "</p> <div class=\"col-sm-12 mb-12 mb-sm-0\"><a class=\"btn btn-outline-primary\" href=\"/hrm/employee-profile/1\">View</a>  <a class=\"btn btn-outline-primary\" href=\"hrm/employee-profile/1\">Block</a></div></div></div></div>";

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
        public JsonResult getEmployeeDataByEmployeeId(string employee_id)
        {

            Employee_Master em = new Employee_Master();

            var data = em.getEmployeeDetailByemployee_id(employee_id);
            return Json(data);
        }
        public JsonResult UpdateEmployeedetailsByid(employee_masterViewModel model)
        {
            Employee_Master em = new Employee_Master();
            string msg = em.UpdateEmployeedetailsByEmployee_id(model, User.Identity.Name);

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

        public IActionResult DownloadDocument(string value, string employee_id)
        {

            Employee_Document ed = new Employee_Document();
            ed = ed.getdocument(employee_id);
            if (value == "bank_detail" && ed.aadhar != null)
            {
                var document = ed.bank_detail;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "bank_detail_" + employee_id + ".pdf");
            }
            if (value == "aadhar" && ed.aadhar != null)
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
                return File(bytes, "application/pdf", "pan_" + employee_id + ".pdf");
            }
            if (value == "voter_id" && ed.voter_id != null)
            {
                var document = ed.voter_id;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "voter_id_" + employee_id + ".pdf");
            }
            if (value == "joining_letter" && ed.joining_letter != null)
            {
                var document = ed.joining_letter;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "joining_letter_" + employee_id + ".pdf");
            }
            if (value == "police_verification" && ed.police_verification != null)
            {
                var document = ed.police_verification;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "police_verification_" + employee_id + ".pdf");
            }
            if (value == "permanent_letter" && ed.permanent_letter != null)
            {
                var document = ed.permanent_letter;
                MemoryStream stream = new MemoryStream(document);
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-length", stream.Length.ToString());
                byte[] bytes = stream.ToArray();
                stream.Close();
                return File(bytes, "application/pdf", "permanent_letter_" + employee_id + ".pdf");
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
                data = "data:image/jpg;base64," + Convert.ToBase64String(ed.employee_img);
            }
            return Json(data);
        }


        [HttpGet]
        public IActionResult AddAllMasterOfAddress(AddAllMasterOfAddressViewModel model)
        {
            UtilityController u = new UtilityController();
            //    model.countrydesc = u.getCountryMastDetails();
            return View(model);
        }

        // --------------Master Country Start----------------------------------

        public JsonResult getCountryMasterList(AddAllMasterOfAddressViewModel model)
        {
            MasterCountry mc = new MasterCountry();
            List<MasterCountry> mclst = new List<MasterCountry>();
            mclst = mc.getAllCountryList();
            string tableemenent = "";
            if (mclst.Count > 0)
            {
                tableemenent = "<tr><th>Country</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in mclst)
                {
                    tableemenent = tableemenent + "<tr><td>" + a.countryname + "</td>" +
                       "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#CountryMasterEdit\" onclick=btnCountryEditOnclick('" + a.countryid + "','" + a.countryname + "')> <i class=\"fa-solid fa-pen\"></i></ button></td>" +
                       "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#designationDelete\" onclick=btnCountryDeleteOnclick('" + a.countryid + "')> <i class=\"fa-solid fa-trash\"></i></button></td></tr>";

                }
            }
            return Json(tableemenent);
        }
        public JsonResult SavedCountry(AddAllMasterOfAddressViewModel model)
        {
            MasterCountry mc = new MasterCountry();
            string cname = model.countryname.ToUpper();

            mc = mc.CheckCountryName(cname);
            if (mc.checkdata == false)
            {
                mc.countrysubid = cname.Substring(0, 1).ToUpper();
                mc = mc.getcountryid(mc);
                if (mc.ccode == 0)
                {
                    mc.countryid = cname.Substring(0, 1).ToUpper() + "00001";

                }
                else
                {
                    string code = (mc.ccode + 1).ToString().PadLeft(5, '0');
                    mc.countryid = cname.Substring(0, 1).ToUpper() + code;
                }
                mc.countryname = cname.ToUpper();
                mc.created_by = Convert.ToString(User.Identity.Name);
                mc.create_date = DateTime.Now.ToString("dd-MM-yyyy");
                mc.create_time = System.DateTime.Now.ToShortTimeString();
                mc.device_name = Environment.MachineName;
                model.msg = mc.saveCountry(mc);

            }
            else
            {
                model.msg = "Country Name Already Exist";
            }


            return Json(model.msg);
        }
        public JsonResult EditCountryName(AddAllMasterOfAddressViewModel model)
        {
            MasterCountry mc = new MasterCountry();

            mc.countryname = model.countryname;
            mc.countryid = model.countryid;
            mc.modified_by = Convert.ToString(User.Identity.Name);
            mc.modified_date = DateTime.Now.ToString("dd-MM-yyyy");
            mc.modified_time = System.DateTime.Now.ToShortTimeString();
            mc.m_device_name = Environment.MachineName;
            model.msg = mc.updateCountry(mc);

            return Json(model.msg);
        }
        public ActionResult DeleteCountry(AddAllMasterOfAddressViewModel model)
        {

            MasterCountry mc = new MasterCountry();
            MasterState ms = new MasterState();
            ms = ms.CheckAllStateDeletedOrNot(model.countryid);
            mc.countryid = model.countryid;
            mc.modified_by = Convert.ToString(User.Identity.Name);
            mc.modified_date = DateTime.Now.ToShortDateString();
            mc.modified_time = DateTime.Now.ToShortTimeString();
            mc.m_device_name = Environment.MachineName;
            if (ms.checkdata == false)
            {
                model.msg = mc.DeleteCountry(mc);
            }
            else
            {
                model.msg = "Please Delete All State Of This Country First";
            }
            return Json(model.msg);
        }

        // --------------Master Country End--------------------------------
        // --------------Master State Start--------------------------------
        public JsonResult getStateMasterList(AddAllMasterOfAddressViewModel model)
        {
            MasterState ms = new MasterState();
            List<MasterState> mslst = new List<MasterState>();
            mslst = ms.getAllStateList();
            string tableemenent = "";
            if (mslst.Count > 0)
            {
                tableemenent = "<tr><th>State Name</th><th>Country Name</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in mslst)
                {
                    tableemenent = tableemenent + "<tr><td>" + a.statename + "</td><td>" + a.CountryName + "</td>" +
                       "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#stateMasterEdit\" onclick=btnStateEditOnclick('" + a.stateid + "','" + a.statename.Replace(" ", "-") + "','" + a.countryid + "')><i class=\"fa-solid fa-pen\"></i></button></td>" +
                       "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#designationDelete\" onclick=btnStateDeleteOnclick('" + a.stateid + "')> <i class=\"fa-solid fa-trash\"></i></button></td></tr>";

                }
            }
            return Json(tableemenent);
        }
        public JsonResult SavedState(AddAllMasterOfAddressViewModel model)
        {
            MasterState ms = new MasterState();
            string sname = model.statename.ToUpper();
            ms = ms.CheckStateName(sname, model.country);
            if (ms.checkdata == false)
            {
                ms.StateSubId = sname.Substring(0, 1).ToUpper();
                ms = ms.getstateid(ms);
                if (ms.scode == 0)
                {
                    ms.stateid = sname.Substring(0, 1).ToUpper() + "00001";
                }
                else
                {
                    string code = (ms.scode + 1).ToString().PadLeft(5, '0');
                    ms.stateid = sname.Substring(0, 1).ToUpper() + code;
                }
                ms.countryid = model.country;
                ms.statename = sname.ToUpper();
                ms.Created_by = Convert.ToString(User.Identity.Name);
                ms.Create_date = DateTime.Now.ToString("dd-MM-yyyy");
                ms.Create_Time = System.DateTime.Now.ToShortTimeString();
                ms.Device_name = Environment.MachineName;
                model.msg = ms.savestate(ms);
            }
            else
            {
                MasterCountry mc = new MasterCountry();
                mc = mc.GetCountryNameByCountryId(model.country);
                model.msg = "State Name Already Exist Under Country : " + mc.countryname;
            }
            return Json(model.msg);
        }
        public JsonResult EditStateName(AddAllMasterOfAddressViewModel model)
        {
            MasterState ms = new MasterState();
            ms.countryid = model.countryid;
            ms.stateid = model.editstateid;
            ms.statename = model.editstatename.ToUpper();
            ms.Modified_by = Convert.ToString(User.Identity.Name);
            ms.Modified_Date = DateTime.Now.ToString("dd-MM-yyyy"); ;
            ms.Modified_Time = System.DateTime.Now.ToShortTimeString();
            ms.M_Device_name = Environment.MachineName;
            model.msg = ms.UpdateState(ms);


            return Json(model.msg);
        }

        public ActionResult DeleteState(AddAllMasterOfAddressViewModel model)
        {

            MasterState ms = new MasterState();
            MasterDistrict md = new MasterDistrict();
            md = md.CheckAllDistrictDeletedOrNot(model.stateid);
            ms.stateid = model.stateid;
            ms.Modified_by = Convert.ToString(User.Identity.Name);
            ms.Modified_Date = DateTime.Now.ToShortDateString();
            ms.Modified_Time = DateTime.Now.ToShortTimeString();
            ms.M_Device_name = Environment.MachineName;
            if (md.checkdata == false)
            {
                model.msg = ms.DeleteState(ms);
            }
            else
            {
                model.msg = "Please Delete All District Of This State First";
            }
            return Json(model.msg);
        }
        // --------------Master State End--------------------------------

        // --------------Master State Start--------------------------------
        public JsonResult getDistrictMasterList(AddAllMasterOfAddressViewModel model)
        {
            List<MasterDistrict> mdlist = new List<MasterDistrict>();
            MasterDistrict mb = new MasterDistrict();
            mdlist = mb.getAllDistrictList();
            string tableemenent = "";
            if (mdlist.Count > 0)
            {
                tableemenent = "<tr><th>District Name</th><th>State Name</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in mdlist)
                {
                    tableemenent = tableemenent + "<tr><td>" + a.districtname + "</td><td>" + a.statename + "</td>" +
                       "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#DistrictMasterEdit\" onclick=btnDistrictEditOnclick('" + a.districtid + "','" + a.districtname.Replace(" ", "-") + "','" + a.stateid + "')><i class=\"fa-solid fa-pen\"></i></button></td>" +
                       "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#designationDelete\" onclick=btnDistrictDeleteOnclick('" + a.districtid + "')> <i class=\"fa-solid fa-trash\"></i></button></td></tr>";

                }
            }
            return Json(tableemenent);
        }
        public JsonResult SavedDistrict(AddAllMasterOfAddressViewModel model)
        {
            MasterDistrict md = new MasterDistrict();
            string dname = model.districtname.ToUpper();

            md = md.CheckDistrictName(dname, model.stateid);
            if (md.checkdata == false)
            {
                md.DistrictSubId = dname.Substring(0, 1).ToUpper();
                md = md.getdistrictid(md);
                if (md.dcode == 0)
                {
                    md.districtid = dname.Substring(0, 1).ToUpper() + "00001";

                }
                else
                {
                    string code = (md.dcode + 1).ToString().PadLeft(5, '0');
                    md.districtid = dname.Substring(0, 1).ToUpper() + code;
                }
                md.stateid = model.stateid;
                md.districtname = dname.ToUpper();
                md.Created_by = Convert.ToString(User.Identity.Name);
                md.Create_date = DateTime.Now.ToString("dd-MM-yyyy");
                md.Create_Time = System.DateTime.Now.ToShortTimeString();
                md.Device_name = Environment.MachineName;
                model.msg = md.saveDistrict(md);

            }
            else
            {
                MasterState mc = new MasterState();
                mc = mc.GetStateNameByStateId(model.stateid);
                model.msg = "District Name Already Exist Under State : " + mc.statename;
            }
            return Json(model.msg);
        }
        public JsonResult EditDistrictName(AddAllMasterOfAddressViewModel model)
        {
            MasterDistrict md = new MasterDistrict();
            md.stateid = model.stateid;
            md.districtid = model.editdistrictid;
            md.districtname = model.editdistrictname.Trim().ToUpper();
            md.Modified_by = Convert.ToString(User.Identity.Name);
            md.Modified_Date = DateTime.Now.ToString("dd-MM-yyyy"); ;
            md.Modified_Time = System.DateTime.Now.ToShortTimeString();
            md.M_Device_name = Environment.MachineName;
            model.msg = md.UpdateDistrict(md);


            return Json(model.msg);
        }

        public ActionResult DeleteDistrict(AddAllMasterOfAddressViewModel model)
        {

            MasterDistrict md = new MasterDistrict();
            MasterCity mc = new MasterCity();
            mc = mc.CheckAllCityDeletedOrNot(model.districtid);
            md.districtid = model.districtid;
            md.Modified_by = Convert.ToString(User.Identity.Name);
            md.Modified_Date = DateTime.Now.ToShortDateString();
            md.Modified_Time = DateTime.Now.ToShortTimeString();
            md.M_Device_name = Environment.MachineName;
            if (mc.checkdata == false)
            {
                model.msg = md.DeleteDistrict(md);
            }
            else
            {
                model.msg = "Please Delete All Cities Of This District First";
            }
            return Json(model.msg);
        }
        // --------------Master State End--------------------------------
        // --------------Master City Start--------------------------------
        public JsonResult getCityMasterList(AddAllMasterOfAddressViewModel model)
        {
            List<MasterCity> mclist = new List<MasterCity>();
            MasterCity mc = new MasterCity();
            mclist = mc.getAllCityList();
            string tableemenent = "";
            if (mclist.Count > 0)
            {
                tableemenent = "<tr><th>City Name</th><th>District Name</th><th colspan=\"2\">Action</th></tr>";
                foreach (var a in mclist)
                {
                    tableemenent = tableemenent + "<tr><td>" + a.cityname + "</td><td>" + a.districtname + "</td>" +
                       "<td><button type=\"button\" class=\"table__icon edit\" data-bs-toggle=\"modal\" data-bs-target=\"#CityMasterEdit\" onclick=btnCityEditOnclick('" + a.cityid + "','" + a.cityname.Replace(" ", "-") + "','" + a.districtid + "')><i class=\"fa-solid fa-pen\"></i></button></td>" +
                       "<td><button type=\"button\" class=\"table__icon delete\" data-bs-toggle=\"modal\" data-bs-target=\"#designationDelete\" onclick=btnCityDeleteOnclick('" + a.cityid + "')> <i class=\"fa-solid fa-trash\"></i></button></td></tr>";

                }
            }
            return Json(tableemenent);
        }
        public JsonResult SavedCity(AddAllMasterOfAddressViewModel model)
        {
            MasterCity mc = new MasterCity();
            string cname = model.cityname.ToUpper();

            mc = mc.CheckCityName(cname, model.districtid);
            if (mc.checkdata == false)
            {
                mc.CitySubId = cname.Substring(0, 1).ToUpper();
                mc = mc.getcityId(mc);
                if (mc.codenum == 0)
                {
                    mc.cityid = cname.Substring(0, 1).ToUpper() + "00001";

                }
                else
                {
                    string code = (mc.codenum + 1).ToString().PadLeft(5, '0');
                    mc.cityid = cname.Substring(0, 1).ToUpper() + code;
                }
                mc.districtid = model.districtid;
                mc.cityname = cname.ToUpper();
                mc.Created_by = Convert.ToString(User.Identity.Name);
                mc.Create_date = DateTime.Now.ToString("dd-MM-yyyy");
                mc.Create_Time = System.DateTime.Now.ToShortTimeString();
                mc.Device_name = Environment.MachineName;
                model.msg = mc.saveCity(mc);

            }
            else
            {
                MasterDistrict md = new MasterDistrict();
                md = md.GetDistrictNameByDistrictId(model.districtid);
                model.msg = "City Name Already Exist Under District : " + md.districtname;
            }
            return Json(model.msg);
        }

        public JsonResult EditCityName(AddAllMasterOfAddressViewModel model)
        {
            MasterCity mc = new MasterCity();
            mc.districtid = model.districtid;
            mc.cityid = model.editcityid;
            mc.cityname = model.editcityname.Trim().ToUpper();
            mc.Modified_by = Convert.ToString(User.Identity.Name);
            mc.Modified_Date = DateTime.Now.ToString("dd-MM-yyyy"); ;
            mc.Modified_Time = System.DateTime.Now.ToShortTimeString();
            mc.M_Device_name = Environment.MachineName;
            model.msg = mc.UpdateCity(mc);


            return Json(model.msg);
        }
        public JsonResult Deletecity(AddAllMasterOfAddressViewModel model)
        {
           
            MasterCity mc = new MasterCity();
            MasterBranch mb = new MasterBranch();
            mb = mb.CheckAllBranchesDeletedOrNot(model.cityid);
            mc.cityid = model.cityid;
            mc.Modified_by = Convert.ToString(User.Identity.Name);
            mc.Modified_Date = DateTime.Now.ToShortDateString();
            mc.Modified_Time = DateTime.Now.ToShortTimeString();
            mc.M_Device_name = Environment.MachineName;
            if (mb.checkdata == false)
            {
                model.msg = mc.DeleteCity(mc);
            }
            else
            {
                model.msg = "Please Delete All Branches Of This City First";
            }
            return Json(model.msg);
        }
    }

}

