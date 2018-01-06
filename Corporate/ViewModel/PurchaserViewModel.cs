using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class PurchaserViewModel
    {
        public long PurchaserID { get; set; }
        public string FirmName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string PAN { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public string PurchaserFullName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string AdminFullname { get; set; }
       
        public Nullable<int> ModifiedBy { get; set; }
        public HttpPostedFile ImageFile { get; set; }
    }
}