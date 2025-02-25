using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.Models.ViewModel;

using HRMS.Models.Database;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;
using HRMS.Includes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRMS.Models.DataBase;
using System.Data;
using System.Text;

namespace HRMS.Controllers
{

    public class UtilityController : Controller
    {
        SQLConfig config = new SQLConfig();

        //****************For Branch Drop Down

        public JsonResult getBranchMastDetails()
        {
            MasterBranch mb = new MasterBranch();

            var mblst = mb.getBranchMast();
            return Json(mblst);
        }
        //****************For Country Drop Down
        public JsonResult getCountryMastDetails()
        {
            
            MasterCountry cm = new MasterCountry();
            var country = cm.getCountryMast();
            
            return  Json(country);
        }
        //****************For State Drop Down
        public JsonResult getStateMastDetails()
        {

            MasterState ms = new MasterState();
            var State = ms.getStateMast();

            return Json(State);
        }

        //****************For District Drop Down
        public JsonResult getDistrictMastDetails()
        {

            MasterDistrict md = new MasterDistrict();

            var District = md.getDistrictMast();

            return Json(District);
        }

        //****************For City Drop Down
        public JsonResult getCityMastDetails()
        {

            MasterCity mc = new MasterCity();


            var City = mc.getCityMast();

            return Json(City);
        }
       

        //****************District Wise City Drop Down
        public ActionResult FillCity(string District_Id)
        {
            MasterCity mc = new MasterCity();
            var cities = mc.GetCityByDistrictId(District_Id);
            return Json(cities);
        }

        //****************For Leave Drop Down

        public ActionResult GetleaveList()
        {
            leave_type_master ltm = new leave_type_master();
            var leavetype = ltm.GetleaveType();
            return Json(leavetype);
        } 
        //****************For Designation Drop Down

        public JsonResult getdesignation()
        {
            Designation_Master ltm = new Designation_Master();
            var dlst = ltm.getDesignation_Masterlists();
            return Json(dlst);
        }
        //****************For Designation Drop Down

        public JsonResult getdepartmentmaster()
        {
            Department_Master dm = new Department_Master();
            var dmlst = dm.getDepartment_Masterlists();
            return Json(dmlst);
        }
        //****************For ReportingManager Drop Down

        public JsonResult getReportingManager()
        {
            Employee_Master em = new Employee_Master();
            var emplst = em.getmanageremployeelists();
            return Json(emplst);
        }
        //=======================for Designation autocomplete
        public JsonResult GetDesignationforautocomplete(string designation)
        {
            Designation_Master dm = new Designation_Master();
            List<Designation_Master> dmList = new List<Designation_Master>();
            dmList = dm.getDesignation_Masterlistsforautocomplete(designation);
            return Json(dmList);
        }
        //Financial Year desc for salary
        public IEnumerable<SelectListItem> getfinYear(string employee_id)
        {
            SalaryMasterViewModel smv = new SalaryMasterViewModel();
            salary_master em = new salary_master();

            smv.financal_yeardesc = em.getfinancal_year(employee_id).ToList().Select(x => new SelectListItem
            {
                Value = x.financial_year.ToString(),
                Text = x.financial_year.ToString()

            });

            return smv.financal_yeardesc;
        }
        //Get Year desc for Payslip
        public IEnumerable<SelectListItem> getYearForPayslip(string employee_id)
        {
            SalaryMasterViewModel smv = new SalaryMasterViewModel();
            salary_master em = new salary_master();

            smv.financal_yeardesc = em.getPayslip_year(employee_id).ToList().Select(x => new SelectListItem
            {
                Value = x.year.ToString(),
                Text = x.year.ToString()

            });

            return smv.financal_yeardesc;
        }
        public JsonResult getEffctDateByFinYear(string finYear)
        {
            salary_master em = new salary_master();
            var effctdtlst = em.geteffectdate(finYear ,User.Identity.Name);
            return Json(effctdtlst);
        }
        
        public string getleaveType(string lid)
        {
            string leaves = "";
            if (lid == "CL")
                leaves = "Casual Leave";
            if (lid == "EL")
                leaves = "Earned Leave";
            if (lid == "SL")
                leaves = "Sick Leave";
        
            if (lid == "CO")
                leaves = "Comp-Off";
          

            return leaves;

        }
        //cloud
        public DateTime currentDateTime()
        {
            DateTime curdt = DateTime.Now.AddHours(13);
            curdt = curdt.AddMinutes(30);
            return curdt;
        }
        //local
        //public DateTime currentDateTime()
        //{
        //    DateTime curdt = DateTime.Now;

        //    return curdt;
        //}
        public IEnumerable<SelectListItem> getempidDesc()
        {
            DetailReportViewModel m = new DetailReportViewModel();
            Employee_Master em = new Employee_Master();

            m.empiddesc = em.getempidMast().ToList().Select(x => new SelectListItem
            {
                Value = x.employee_id.ToString(),
                Text = x.name.ToString()
            });

            return m.empiddesc;
        }
        //day count
        public int CountDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek)
        {
            DateTime startDate = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            int weekDayCount = 0;
            for (int day = 0; day < days; ++day)
            {
                weekDayCount += startDate.AddDays(day).DayOfWeek == dayOfWeek ? 1 : 0;
            }
            return weekDayCount;
        }

        //Optional Holiday count
        public int OpHolidaysInMonthTakenOrNot(string empid, string from_dt, string to_dt)
        {

            string date = "";
            int count = 0;

            string sql = "Select * from Holiday_List where convert(date,date,103)>=convert(date,'" + from_dt + "',103) and convert(date,date,103)<=convert(date,'" + to_dt + "',103) and alternative='Yes'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    date = Convert.ToString(dr["date"]);
                    sql = "Select * from employee_attendance where employee_id=empid and date='" + date + "' and punch_type='Valid'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count == 0)
                    {
                        count = count + 1;
                    }

                }

            }
            return count;
        }

        //Holiday count
        public int HolidaysInMonth(string from_dt, string to_dt)
        {
            int count = 0;
            string sql = "Select Count(*) as cnt from Holiday_List where convert(date,date,103)>=convert(date,'" + from_dt + "',103) and convert(date,date,103)<=convert(date,'" + to_dt + "',103) and alternative='No'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                count = Convert.ToInt32(dr["cnt"]);
            }
            return count;
        }
        //Reverse String 
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        //Password Encrypt
        public string Encryptdata(string password)
        {
            password = ReverseString(password);
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        //Password Decrypt
        public string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            decryptpwd = ReverseString(decryptpwd);
            return decryptpwd;
        }
        //******** Start Amount In Words
        public string AmountInWords(decimal Num)
        {
            string returnValue;
            //I have created this function for converting amount in indian rupees (INR).
            //You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.


            string strNum;
            string strNumDec;
            string StrWord;
            strNum = Num.ToString();


            if (strNum.IndexOf(".") + 1 != 0)
            {
                strNumDec = strNum.Substring(strNum.IndexOf(".") + 2 - 1);


                if (strNumDec.Length == 1)
                {
                    strNumDec = strNumDec + "0";
                }
                if (strNumDec.Length > 2)
                {
                    strNumDec = strNumDec.Substring(0, 2);
                }


                strNum = strNum.Substring(0, strNum.IndexOf(".") + 0);
                StrWord = ((double.Parse(strNum) == 1) ? "" : "") + NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNumDec) > 0) ? (" and" + cWord3((decimal)(double.Parse(strNumDec)))) : "");
                if (Convert.ToInt32(strNumDec) > 0)
                {
                    StrWord = StrWord + " Paisa";
                }

            }
            else
            {
                StrWord = ((double.Parse(strNum) == 1) ? "" : "") + NumToWord((decimal)(double.Parse(strNum)));
            }
            returnValue = StrWord + " Only";
            return returnValue;
        }
        static public string NumToWord(decimal Num)
        {
            string returnValue;


            //I divided this function in two part.
            //1. Three or less digit number.
            //2. more than three digit number.
            string strNum;
            string StrWord;
            strNum = Num.ToString();


            if (strNum.Length <= 3)
            {
                StrWord = cWord3((decimal)(double.Parse(strNum)));
            }
            else
            {
                StrWord = cWordG3((decimal)(double.Parse(strNum.Substring(0, strNum.Length - 3)))) + " " + cWord3((decimal)(double.Parse(strNum.Substring(strNum.Length - 2 - 1))));
            }
            returnValue = StrWord;
            return returnValue;
        }
        static public string cWordG3(decimal Num)
        {
            string returnValue;
            //2. more than three digit number.
            string strNum = "";
            string StrWord = "";
            string readNum = "";
            strNum = Num.ToString();
            if (strNum.Length % 2 != 0)
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
                if (readNum != "0")
                {
                    StrWord = retWord(decimal.Parse(readNum));
                    readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 1) + "000"));
                    StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
                }
                strNum = strNum.Substring(1);
            }
            while (!System.Convert.ToBoolean(strNum.Length == 0))
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 2)));
                if (readNum != "0")
                {
                    StrWord = StrWord + " " + cWord3(decimal.Parse(readNum));
                    readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 2) + "000"));
                    StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
                }
                strNum = strNum.Substring(2);
            }
            returnValue = StrWord;
            return returnValue;
        }
        static public string cWord3(decimal Num)
        {
            string returnValue;
            //1. Three or less digit number.
            string strNum = "";
            string StrWord = "";
            string readNum = "";
            if (Num < 0)
            {
                Num = Num * -1;
            }
            strNum = Num.ToString();


            if (strNum.Length == 3)
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
                StrWord = retWord(decimal.Parse(readNum)) + " Hundred";
                strNum = strNum.Substring(1, strNum.Length - 1);
            }


            if (strNum.Length <= 2)
            {
                if (double.Parse(strNum) >= 0 && double.Parse(strNum) <= 20)
                {
                    StrWord = StrWord + " " + retWord((decimal)(double.Parse(strNum)));
                }
                else
                {
                    StrWord = StrWord + " " + retWord((decimal)(System.Convert.ToDouble(strNum.Substring(0, 1) + "0"))) + " " + retWord((decimal)(double.Parse(strNum.Substring(1, 1))));
                }
            }


            strNum = Num.ToString();
            returnValue = StrWord;
            return returnValue;
        }
        static public string retWord(decimal Num)
        {
            string returnValue;
            //This two dimensional array store the primary word convertion of number.
            returnValue = "";
            object[,] ArrWordList = new object[,] { { 0, "" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }, { 20, "Twenty" }, { 30, "Thirty" }, { 40, "Forty" }, { 50, "Fifty" }, { 60, "Sixty" }, { 70, "Seventy" }, { 80, "Eighty" }, { 90, "Ninety" }, { 100, "Hundred" }, { 1000, "Thousand" }, { 100000, "Lakh" }, { 10000000, "Crore" } };


            int i;
            for (i = 0; i <= (ArrWordList.Length - 1); i++)
            {
                if (Num == System.Convert.ToDecimal(ArrWordList[i, 0]))
                {
                    returnValue = (string)(ArrWordList[i, 1]);
                    break;
                }
            }
            return returnValue;
        }
        static public string strReplicate(string str, int intD)
        {
            string returnValue;
            //This fucntion padded "0" after the number to evaluate hundred, thousand and on....
            //using this function you can replicate any Charactor with given string.
            int i;
            returnValue = "";
            for (i = 1; i <= intD; i++)
            {
                returnValue = returnValue + str;
            }
            return returnValue;
        }
        //******** End Amount In Words
    }
}