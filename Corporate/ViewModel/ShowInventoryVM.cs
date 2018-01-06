using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using paytm;

namespace Corporate.ViewModel
{
    public class ShowInventoryVM
    {
        public string ProductMasterName { get; set; }
        public string ProductName { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public string Rate { get; set; } 
       
    }
 
}