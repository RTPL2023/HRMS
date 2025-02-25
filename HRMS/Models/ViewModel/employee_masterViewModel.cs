using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class employee_masterViewModel
    {
        public string branch_id { get; set; }
        public string employee_id { get; set; }
        public string user_role { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string contact_number_1 { get; set; }
        public string contact_number_2 { get; set; }
        public string personal_email { get; set; }
        public string official_email { get; set; }
        public string employment_type { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string dist { get; set; }
        public string state { get; set; }
        public string pin { get; set; }
        public string country { get; set; }
        public string joining_date { get; set; }
        public string date_of_birth { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string editdesignation { get; set; }
        public string editdepartment { get; set; }
        public string editid { get; set; }
        public string reporting_mg_id { get; set; }
        public string last_edu_ql { get; set; }
        public string employment_role { get; set; }
        public string blood_group { get; set; }
        public string emg_Name1 { get; set; }
        public string emg_Name2{ get; set; }
        public string emg_Relatio1 { get; set; }
        public string emg_Relatio2 { get; set; }
        public string emg_Phone1 { get; set; }
        public string emg_Phone2 { get; set; }
        public string emg_Address1 { get; set; }
        public string emg_Address2 { get; set; }

        public string ac_name { get; set; }
        public string ac_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string branch_code { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string voter_id_no { get; set; }
        public string pf_no { get; set; }
        public string esic_no { get; set; }
        public string passport_no { get; set; }
        public IFormFile bank_detail { get; set; }
        public IFormFile aadhar { get; set; }
        public IFormFile pan { get; set; }
        public IFormFile voter_id { get; set; }
        public IFormFile joining_letter { get; set; }
        public IFormFile police_verification { get; set; }
        public IFormFile permanent_letter { get; set; }
        public IFormFile employee_img { get; set; }

    }
}
