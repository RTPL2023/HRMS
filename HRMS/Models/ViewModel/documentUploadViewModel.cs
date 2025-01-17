using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models.ViewModel
{
    public class documentUploadViewModel
    {
        public string employee_id { get; set; }
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
        public string emg_contact_name { get; set; }
        public string emg_contact_relation { get; set; }
        public string emg_contact_no { get; set; }
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
