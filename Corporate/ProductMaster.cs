//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Corporate
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMaster
    {
        public ProductMaster()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int ProductMasterID { get; set; }
        public string ProductMasterName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
