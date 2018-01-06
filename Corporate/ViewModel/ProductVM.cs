using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class ProductVM
    {
        public long ProductID { get; set; }
        public long ProductMasterID { get; set; }
        public string ProductName { get; set; }
        public string ProductMasterName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}