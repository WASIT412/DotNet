using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;


namespace Corporate.Models
{
    public class PO
    {
        ConnectionString db = null;
         public CorparateResult<DropDownVM> getPaymentType()
        {
          
            using (db = new ConnectionString())
            {
                // db.Configuration.LazyLoadingEnabled = false;
                var Categories = (from d in db.PaymentModes
                                  select new DropDownVM
                                  {
                                      Value = d.PaymentModeID,
                                      Text = d.PaymentType

                                  }).ToList();
                return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = Categories };
            }
           // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
        }

         public int GetAllPO()
         {
             using (db = new ConnectionString())
             {
                 var GetAllPO_Result =  db.GetAllPO("null");   
                 // var result = (IEnumerable<ProductMasterVM>)xx;
                 return GetAllPO_Result;
             }
         }

         public IEnumerable<POGridVM> GetAllPO(string id)
         {
             using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
             {
                 var para = new DynamicParameters();
                 para.Add("@Purchaser", id);
                 var data= con.Query<POGridVM>("GetAllPO", para, null, true, 0, CommandType.StoredProcedure).ToList();
                 return data;
             }
         }

         public CorparateResult<OrderMaster> Save(OrderMaster order)
         {
              int? id =null;
              UserInfo userinfo = UserInfo.GetInstence;
             try
             {
                 bool status = false;
                 var Aggregate = order.OrderDetails.Sum(r => r.Rate * r.Quantity);
                 using (db = new ConnectionString())
                 {
                     var autoOrder = (from p in db.GenerateOrderNumber() select p);
                     GenerateOrderNumber_Result ord = (GenerateOrderNumber_Result)autoOrder.First();//.Select(m =>m.MemberNo).ToString();
                     order.OrderNo = ord.OrderNo;
                     order.OrderDate = DateTime.Now;
                     order.CreatedBy = userinfo.UserID; 
                      id = ord.CurrentID;
                     db.OrderMasters.Add(order);
                     db.SaveChanges();
                     status = true;
                 }

                 using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
                 {

                     string q = "INSERT INTO Ledger (OrderID,Credit, Debit,TransDate, PurchaserID,CustomerID) VALUES ("+id +", null , "+Aggregate+", getdate(),'" + order.PurchaserID + "',null)";
                     SqlCommand cmd = new SqlCommand(q, con);
                     con.Open();
                     int p = cmd.ExecuteNonQuery();
                 }
                 
                 return new CorparateResult<OrderMaster> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true };
             }
             catch (Exception ex)
             {
                 return new CorparateResult<OrderMaster> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = false };
             }
             }

         public CorparateResult<ViewOrderDetailVM> ViewOrderDetails(int id)
         {
             using (db = new  ConnectionString())
             {
                 var data= (from od in db.OrderDetails
                            join om in db.OrderMasters on od.OrderID equals om.OrderID into result1
                            from q1 in result1
                            join p in db.Products on od.ProductID equals p.ProductID into result2
                            from q2  in result2
                            join l in db.Ledgers on q1.OrderID equals l.OrderID
                                where od.OrderID.Equals(id)
                                   select new ViewOrderDetailVM
                                {
                                    OrderID = od.OrderID,
                                    Quantity = od.Quantity,
                                    Product = q2.ProductName,
                                    Rate = (decimal)od.Rate,
                                    Total = (decimal)od.Rate * od.Quantity,
                                ItemDescription= od.ItemDescription
                                  //  GrandTotal += (decimal)(od.Rate * od.Quantity)
                                }).ToList();

                 return new CorparateResult<ViewOrderDetailVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = data };
             }
         }

             
         public CorparateResult<DropDownVM> getPurchaser()
         {
             using (db = new ConnectionString())
             {
                 // db.Configuration.LazyLoadingEnabled = false;
                 var Categories = (from d in db.Purchasers
                                   select new DropDownVM
                                   {
                                       Value = (int)d.PurchaserID,
                                       Text = d.PurchaserNo
                                   }).ToList();
                 return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = Categories };
             }
             // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
         }

         public CorparateResult<DropDownVM> getCategories()
         {
             using (db = new ConnectionString())
             {
                 // db.Configuration.LazyLoadingEnabled = false;
                 var Categories = (from d in db.ProductMasters
                                   select new DropDownVM
                                   {
                                       Value = (int)d.ProductMasterID,
                                       Text = d.ProductMasterName
                                   }).ToList();
                 return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = Categories };
             }
             // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
         }

         public CorparateResult<DropDownVM> getProducts(int id)
         {
             using (db = new ConnectionString())
             {
                 // db.Configuration.LazyLoadingEnabled = false;
                 var Categories = (from d in db.Products
                                   where d.ProductMasterID == id
                                   select new DropDownVM
                                   {
                                       Value = (int)d.ProductID,
                                       Text = d.ProductName
                                   }).ToList();
                 return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = Categories };
             }
             // return new CorparateResult<DropDownVM> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true};
         }

    }
}