using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public class PaymentMod
    {
        ConnectionString db = null;
        public IEnumerable<PaymentModeVM> GetAllPayments()
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var xx = from q1 in db.PaymentModes
                         join u1 in db.Users on q1.User.UserID equals u1.UserID into result
                         from q2 in result
                         join u2 in db.Users on q1.User1.UserID equals u2.UserID
                         select new PaymentModeVM
                         {

                             PaymentType = q1.PaymentType,
                             PaymentModeID = q1.PaymentModeID,
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

        public CorparateResult<PaymentMode> getPaymentModeId(int id)
        {
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var us = db.PaymentModes.Where(x => x.PaymentModeID.Equals(id)).FirstOrDefault();
                if (us != null)
                {
                    var pur = (PaymentMode)us;
                    return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true, GenericOne = pur };
                }
                else
                    return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Successful, Message = "Not Inserted", Exist = false };
            }
        }

        public CorparateResult<PaymentMode> savePayment(PaymentMode pur)
        {
            UserInfo userinfo = UserInfo.GetInstence;
            bool status = false;
            try
            {
                if (pur.PaymentModeID <= 0)
                {
                    using (db = new ConnectionString())
                    {
                        pur.CreatedDate = DateTime.Now;
                        pur.ModifiedDate = DateTime.Now;
                        pur.CreatedBy = userinfo.UserID;
                        pur.ModifiedBy = userinfo.UserID;
                        db.PaymentModes.Add(pur);
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = status };
                }
                else
                {//edit case
                    using (db = new ConnectionString())
                    {

                        var item = (from c in db.PaymentModes
                                    where c.PaymentModeID == pur.PaymentModeID
                                    select c).Single();
                        item.PaymentType = pur.PaymentType;

                        item.IsActive = pur.IsActive;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedBy = userinfo.UserID;
                        db.SaveChanges();
                        status = true;
                    }
                    return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Successful, Message = "Updated", Exist = status };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = status };
            }


        }

        public CorparateResult<PaymentMode> removePaymentMode(int ID)
        {
            bool reply = false;
            try
            {
                using (db = new ConnectionString())
                {

                    var itemToRemove = db.PaymentModes.SingleOrDefault(x => x.PaymentModeID == ID); //returns a single item.
                    if (itemToRemove != null)
                    {
                        db.PaymentModes.Remove(itemToRemove);
                        db.SaveChanges();
                        reply = true;

                    }
                    return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Successful, Message = "Deleted", Exist = reply };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<PaymentMode> { Status = Constants.CorparateStatus.Error, Message = ex.ToString(), Exist = reply };
            }
        }
    }
}