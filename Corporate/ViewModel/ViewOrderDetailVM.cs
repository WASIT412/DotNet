using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class ViewOrderDetailVM
    {
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public string Product { get; set; }
        public string ItemDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
    }
}