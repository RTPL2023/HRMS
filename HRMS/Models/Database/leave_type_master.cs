using HRMS.Models.ViewModel;
using HRMS.Models.DataBase;
using HRMS.Includes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace HRMS.Models.Database
{
    public class leave_type_master
    {
        SQLConfig config = new SQLConfig();
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public List<leave_type_master> GetleaveType()
        {

            string sql = "Select * from  leave_type_master order by id";
            config.singleResult(sql);

            List<leave_type_master> mcl = new List<leave_type_master>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    leave_type_master ltm = new leave_type_master();
                    ltm.code = Convert.ToString(dr["code"]);
                    ltm.name = Convert.ToString(dr["name"]);
                    mcl.Add(ltm);
                }
            }
            return mcl;
        }

    }
}
