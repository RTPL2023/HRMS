using HRMS.Models.Database;
using HRMS.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRMS.Controllers
{

    public class AuthController : Controller
    {
      
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        

        public JsonResult Loginchack(LoginViewModel model)
        {

            try
            {
                users _user = new users();
                UtilityController u = new UtilityController();
                model.password = u.Encryptdata(model.password);
                model.emp_id = model.emp_id.ToUpper();
                _user = _user.getLoggin(model.emp_id, model.password);
                if (_user.valid == 1)
                {
                    //Create the identity for the user  
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.emp_id), new Claim(ClaimTypes.Role, _user.user_role) }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    model.msg = "loginDone";
                    model.role = _user.user_role;
                }
                else if (_user.valid == 2)
                    model.msg = "noLogin";
                else
                    model.msg = "err";
            }
            catch (Exception ex)
            {
                model.msg = "Unable to connect with database host.";

            }

            return Json(model);
        }
        public JsonResult forgatepassword(string employee_id, string oldpass, string newpass)
        {
            users _user = new users();
            UtilityController u = new UtilityController();
            oldpass = u.Encryptdata(oldpass);
            newpass = u.Encryptdata(newpass);
            string msg = _user.updatePassword(employee_id, oldpass, newpass);
            return Json(msg);
        }
    }
}
