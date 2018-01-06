using Corporate.Contract;
using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public class AllProducts : IProductMaster<ProductMaster>, IProduct<Product>
    {
        ConnectionString db = null;
        public IEnumerable<ProductMasterVM> GetAllProductMaster()
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var xx = from q1 in db.ProductMasters
                         join u1 in db.Users on q1.User.UserID equals u1.UserID into result
                         from q2 in result
                         join u2 in db.Users on q1.User1.UserID equals u2.UserID
                         select new ProductMasterVM
                         {

                             ProductMasterName = q1.ProductMasterName,
                             ProductMasterID = q1.ProductMasterID,
                             IsActive = q1.IsActive,
                             CreatedByName = q2.FirstName,
                             CreatedDate = q1.CreatedDate,
                             ModifyByName = u2.FirstName,
                             ModifiedDate = q1.ModifiedDate,
                         };
                // var result = (IEnumerable<ProductMasterVM>)xx;
                return xx.ToList();
            }
            // return data;
        }

        public CorparateResult<ProductMaster> gettProductMasterById(int id)
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var us = db.ProductMasters.Where(x => x.ProductMasterID.Equals(id)).FirstOrDefault();
                if (us != null)
                {
                    var pur = (ProductMaster)us;
                    return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true, GenericOne = pur };
                }
                else
                    return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Successful, Message = "Not Inserted", Exist = false };
            }
        }

        public CorparateResult<ProductMaster> saveProductMaster(ProductMaster pur)
        {
            UserInfo userinfo = UserInfo.GetInstence;
            bool status = false;
            try
            {
                if (pur.ProductMasterID <= 0)
                {
                    using (db = new ConnectionString())
                    {
                        pur.CreatedDate = DateTime.Now;
                        pur.ModifiedDate = DateTime.Now;
                        pur.CreatedBy = userinfo.UserID;
                        pur.ModifiedBy = userinfo.UserID;
                        db.ProductMasters.Add(pur);
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = status };
                }
                else
                {//edit case
                    using (db = new ConnectionString())
                    {

                        var item = (from c in db.ProductMasters
                                    where c.ProductMasterID == pur.ProductMasterID
                                    select c).Single();
                        item.ProductMasterName = pur.ProductMasterName;

                        item.IsActive = pur.IsActive;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedBy = userinfo.UserID;
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Successful, Message = "Updated", Exist = status };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = status };
            }


        }

        public CorparateResult<ProductMaster> removeProductMaster(int ID)
        {
            bool reply = false;
            try
            {
                using (db = new ConnectionString())
                {

                    var itemToRemove = db.ProductMasters.SingleOrDefault(x => x.ProductMasterID == ID); //returns a single item.
                    if (itemToRemove != null)
                    {
                        db.ProductMasters.Remove(itemToRemove);
                        db.SaveChanges();
                        reply = true;

                    }
                    return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Successful, Message = "Deleted", Exist = reply };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<ProductMaster> { Status = Constants.CorparateStatus.Error, Message = ex.ToString(), Exist = reply };
            }
        }

        public IEnumerable<ProductVM> GetAllProduct()
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var xx = from q1 in db.Products
                         join pm in db.ProductMasters on q1.ProductMasterID equals pm.ProductMasterID into result1
                         from q2 in result1
                         join u1 in db.Users on q1.User.UserID equals u1.UserID into result2
                         from q3 in result2
                         join u2 in db.Users on q1.User1.UserID equals u2.UserID
                         select new ProductVM
                         {
                             ProductName = q1.ProductName,
                             ProductMasterName = q2.ProductMasterName,
                             ProductID = q1.ProductID,
                             ProductMasterID = q1.ProductMasterID,
                             IsActive = q1.IsActive,
                             CreatedByName = q3.FirstName,
                             CreatedDate = q1.CreatedDate,
                             ModifiedByName = u2.FirstName,
                             ModifiedDate = q1.ModifiedDate,
                         };
                return xx.ToList();
            }
        }

        public CorparateResult<Product> gettProductById(int id)
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var us = db.Products.Where(x => x.ProductID.Equals(id)).FirstOrDefault();
                if (us != null)
                {
                    var pur = (Product)us;
                    return new CorparateResult<Product> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true, GenericOne = pur };
                }
                else
                    return new CorparateResult<Product> { Status = Constants.CorparateStatus.Successful, Message = "Not Inserted", Exist = false };
            }
        }

        public CorparateResult<Product> saveProduct(Product pur)
        {
            UserInfo userinfo = UserInfo.GetInstence;
            bool status = false;
            try
            {
                if (pur.ProductID <= 0)
                {
                    using (db = new ConnectionString())
                    {
                        pur.CreatedDate = DateTime.Now;
                        pur.ModifiedDate = DateTime.Now;
                        pur.CreatedBy = userinfo.UserID;
                        pur.ModifiedBy = userinfo.UserID;
                        db.Products.Add(pur);
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<Product> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = status };
                }
                else
                {//edit case
                    using (db = new ConnectionString())
                    {

                        var item = (from c in db.Products
                                    where c.ProductID == pur.ProductID
                                    select c).Single();
                        item.ProductMasterID = pur.ProductMasterID;
                        item.ProductName = pur.ProductName;
                        item.IsActive = pur.IsActive;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedBy = userinfo.UserID;
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<Product> { Status = Constants.CorparateStatus.Successful, Message = "Updated", Exist = status };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<Product> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = status };
            }
        }

        public CorparateResult<Product> removeProduct(int ID)
        {
            bool reply = false;
            try
            {
                using (db = new ConnectionString())
                {

                    var itemToRemove = db.Products.SingleOrDefault(x => x.ProductID == ID); //returns a single item.
                    if (itemToRemove != null)
                    {
                        db.Products.Remove(itemToRemove);
                        db.SaveChanges();
                        reply = true;

                    }
                    return new CorparateResult<Product> { Status = Constants.CorparateStatus.Successful, Message = "Deleted", Exist = reply };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<Product> { Status = Constants.CorparateStatus.Error, Message = ex.ToString(), Exist = reply };
            }
        }
        public CorparateResult<DropDownVM> getCategories()
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var Categories = (from d in db.ProductMasters
                                  select new DropDownVM
                                      {
                                          Value = d.ProductMasterID,
                                          Text = d.ProductMasterName

                                      }).ToList();
                return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true, GenericList = Categories };
            }
           // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
        }

        public CorparateResult<DropDownItemVM> getCategoriesForDropDownItemVM()
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var Categories = (from d in db.ProductMasters
                                  select new DropDownItemVM
                                  {
                                      ProductMasterID = d.ProductMasterID,
                                      Text = d.ProductMasterName

                                  }).ToList();
                return new CorparateResult<DropDownItemVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true, GenericList = Categories };
            }
            // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
        }
    }
}
    