using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class PaymentModeVM
    {
            public int PaymentModeID { get; set; }
            public string PaymentType { get; set; }
            public bool IsActive { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string CreatedByName { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string ModifyByName { get; set; }     
    }
}