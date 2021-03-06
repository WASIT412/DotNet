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
    
    public partial class User
    {
        public User()
        {
            this.Customers = new HashSet<Customer>();
            this.Customers1 = new HashSet<Customer>();
            this.OrderMasters = new HashSet<OrderMaster>();
            this.OrderMasters1 = new HashSet<OrderMaster>();
            this.PaymentDetails = new HashSet<PaymentDetail>();
            this.PaymentModes = new HashSet<PaymentMode>();
            this.PaymentModes1 = new HashSet<PaymentMode>();
            this.Products = new HashSet<Product>();
            this.Products1 = new HashSet<Product>();
            this.ProductMasters = new HashSet<ProductMaster>();
            this.ProductMasters1 = new HashSet<ProductMaster>();
            this.Purchasers = new HashSet<Purchaser>();
            this.Purchasers1 = new HashSet<Purchaser>();
        }
    
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Customer> Customers1 { get; set; }
        public virtual ICollection<OrderMaster> OrderMasters { get; set; }
        public virtual ICollection<OrderMaster> OrderMasters1 { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<PaymentMode> PaymentModes { get; set; }
        public virtual ICollection<PaymentMode> PaymentModes1 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Product> Products1 { get; set; }
        public virtual ICollection<ProductMaster> ProductMasters { get; set; }
        public virtual ICollection<ProductMaster> ProductMasters1 { get; set; }
        public virtual ICollection<Purchaser> Purchasers { get; set; }
        public virtual ICollection<Purchaser> Purchasers1 { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
