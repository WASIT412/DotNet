using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class ProductMasterVM
    {
        public int ProductMasterID { get; set; }
        public string ProductMasterName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifyByName { get; set; }
    }
}