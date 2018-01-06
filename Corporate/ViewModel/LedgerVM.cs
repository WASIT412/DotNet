using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class LedgerVM
    {
        public int LedgerID { get; set; }
        public int OrderID { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public DateTime Date { get; set; }
        public string Firm { get; set; }
        public string OrderNo { get; set; }
        public double Balance { get; set; }
        public int TotalRows { get; set; }
        public int PurchaserID { get; set; }
        public int PaymentDetailId { get; set; }
    }
}