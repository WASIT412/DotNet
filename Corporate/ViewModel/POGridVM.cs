using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class POGridVM
    {

        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public string Firm { get; set; }
        public int TotalItem { get; set; }
            public DateTime OrderDate { get; set; }
            public string Vendor { get; set; }
            public double Amount { get; set; }
            public string CreatedBy { get; set; }
        
    }
}