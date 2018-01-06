using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class PaymentVM
    {
        public string CreatedBy { get; set; }
        public string Firm { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string Reference { get; set; }
        public double PaymentAmount { get; set; }
        public int TotalRows { get; set; }
        public int PurchaserID { get; set; }
        public int PaymentID { get; set; }
    }
}