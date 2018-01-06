using Corporate.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public class Inventory
    {
       ConnectionString db = null;
        public IEnumerable<ShowInventoryVM> GetStock(string fromDate, string toDate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@FromDate", fromDate);
                para.Add("@ToDate", toDate);
                var data = con.Query<ShowInventoryVM>("UspShowInventory", para, null, true, 0, CommandType.StoredProcedure).ToList();
                return data;
            }
            }
    }
}