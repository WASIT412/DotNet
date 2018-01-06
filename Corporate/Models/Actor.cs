using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Corporate.Contract;
using System.IO;
using System.Web.Mvc;
using Corporate.Extension;

namespace Corporate.Models
{
    public class Actor : IPurchaser<Purchaser>, ICustomer<Customer>
    {
        ConnectionString db = null;
        public IEnumerable<GetAllPurchaser1_Result> GetAllPurchasers()
        {
            using (db = new ConnectionString())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var resulSet = db.GetAllPurchaser1();
                IEnumerable<GetAllPurchaser1_Result> data = resulSet.ToList();
                return data;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CorparateResult<Purchaser> gettPurchaserById(int id)
        {
             using (db = new ConnectionString())
            {
               // db.Configuration.LazyLoadingEnabled = false;
                var us = db.Purchasers.Where(x => x.PurchaserID.Equals(id)).FirstOrDefault();
                if (us != null)
                {
                    var pur = (Purchaser)us;
                     return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist=true,GenericOne=pur };
                }
                else
                     return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Successful, Message = "Not Inserted", Exist=false };
            }
            
        }

        public CorparateResult<Customer> gettCustomerById(int cus)
        {
            throw new NotImplementedException();
        }

        public CorparateResult<Purchaser> savePurchaser(Purchaser pur)
        {
             UserInfo userinfo = UserInfo.GetInstence;
            bool status = false;
            try
            {
                if (pur.PurchaserID <= 0)
                {
                    using (db = new ConnectionString())
                    {
                        if (pur.ImagePath != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(pur.ImagePath);
                            string ext = Path.GetExtension(pur.ImagePath);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + ext;
                            pur.ImagePath = "~/Photo/" + fileName;
                            var filename = Path.GetFileName(fileName);
                           // var path = Path.Combine(Server.MapPath("~/Photo/"), fileName);
                            // t.SaveAs(path); 
                        }
                        pur.CreatedDate = DateTime.Now;
                        pur.ModifiedDate = DateTime.Now;
                        pur.CreatedBy = userinfo.UserID;
                        pur.ModifiedBy = userinfo.UserID;
                        db.Purchasers.Add(pur);
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = status };
                }
                else
                {//edit case
                    using (db = new ConnectionString())
                    {
                        if (pur.ImagePath != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(pur.ImagePath);
                            string ext = Path.GetExtension(pur.ImagePath);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + ext;
                            pur.ImagePath = "~/Photo/" + fileName;
                            var filename = Path.GetFileName(fileName);
                           // var path = Path.Combine(Server.MapPath("~/Photo/"), fileName);
                            // t.SaveAs(path); 
                        }
                        var purchaser = (from c in db.Purchasers
                                        where c.PurchaserID == pur.PurchaserID
                                        select c).Single();
                        purchaser.PurchaserNo = pur.PurchaserNo;
                        purchaser.PAN = pur.PAN;
                        purchaser.FirstName = pur.FirstName;
                        purchaser.LastName = pur.LastName;
                        purchaser.Gender = pur.Gender;
                        purchaser.ContactNo = pur.ContactNo;
                        purchaser.DOB = pur.DOB;
                        purchaser.EmailID = pur.EmailID;
                        purchaser.Address = pur.Address;
                        purchaser.IsActive = pur.IsActive;
                        purchaser.ModifiedDate = DateTime.Now;
                        purchaser.ModifiedBy = userinfo.UserID;
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Successful, Message = "Updated", Exist = status };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = status };
            }
        }

        public CorparateResult<Customer> saveCustomer(Customer pur)
        {
            return new CorparateResult<Customer> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true };
        }

        public CorparateResult<Purchaser> editPurchaser(Purchaser cus)
        {
            throw new NotImplementedException();
        }

        public CorparateResult<Customer> editCustomer(Customer cus)
        {
            throw new NotImplementedException();
        }

        public CorparateResult<Customer> removeCustomer(int cus)
        {
            throw new NotImplementedException();
        }

        public CorparateResult<Purchaser> removePurchaser(int ID)
        {
            bool reply = false;
            try{
            using (db = new ConnectionString())
            {
                
                var itemToRemove = db.Purchasers.SingleOrDefault(x => x.PurchaserID == ID); //returns a single item.
                if (itemToRemove != null)
                {
                    db.Purchasers.Remove(itemToRemove);
                    db.SaveChanges();
                    reply = true;
               
                }
                 return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Successful, Message = "Deleted", Exist = reply };
            }
            }
                  catch(Exception ex)
                {
                       return new CorparateResult<Purchaser> { Status = Constants.CorparateStatus.Error, Message = ex.ToString(), Exist = reply };
                    }
            }
        }
    }
