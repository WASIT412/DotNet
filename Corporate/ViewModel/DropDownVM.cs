using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class DropDownVM
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class DropDownItemVM
    {
        public int ProductMasterID { get; set; }
        public string Text { get; set; }
    }
}