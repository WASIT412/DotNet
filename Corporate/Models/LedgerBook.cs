using Corporate.ViewModel;
using Dapper;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corporate.Models
{
    public class LedgerBook
    {
        ConnectionString db = null;
        public IEnumerable<LedgerVM> GetLedger(string id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PurChaserID", id);
                var data = con.Query<LedgerVM>("GetLedgerBalance2", para, null, true, 0, CommandType.StoredProcedure).ToList();
                return data;
            }
        }

        public CorparateResult<PaymentDetail> AddPayment(PaymentDetail pay)
        {
           
            UserInfo userinfo = UserInfo.GetInstence;
            try
            {
                bool status = false;
              
                using (db = new ConnectionString())
                {
                    
                    pay.PaymentDate = DateTime.Now;
                    pay.CreatedDate = DateTime.Now;
                    pay.CreatedBy = userinfo.UserID;
                    db.PaymentDetails.Add(pay);
                    db.SaveChanges();
                    status = true;
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
                {

                    string q = "INSERT INTO Ledger (OrderID,Credit, Debit,TransDate, PurchaserID,CustomerID,PaymentDetailID) VALUES (null , " + pay.PaymentAmount + " , null , getdate()," + pay.PurchaserID + ",null,(SELECT IDENT_CURRENT('PaymentDetails')))";
                    SqlCommand cmd = new SqlCommand(q, con);
                    con.Open();
                    int p = cmd.ExecuteNonQuery();
                }

                return new CorparateResult<PaymentDetail> { Status = Constants.CorparateStatus.Successful, Message = "Inserted", Exist = true };
            }
            catch (Exception ex)
            {
                return new CorparateResult<PaymentDetail> { Status = Constants.CorparateStatus.Error, Message = ex.Message.ToString(), Exist = false };
            }
        }


        public IEnumerable<PaymentVM> GetPayments(string id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Purchaser", id);
                var data = con.Query<PaymentVM>("GetAllPayments", para, null, true, 0, CommandType.StoredProcedure).ToList();
                return data;
            }
        }
        public CorparateResult<PaymentVM> PaymentDetail(int id)
        {
            using (db = new ConnectionString())
            {
                var data = (from pd in db.PaymentDetails
                            join pm in db.PaymentModes on pd.PaymentModeID equals pm.PaymentModeID into result1
                            from q1 in result1
                            join p in db.Purchasers on pd.PurchaserID equals p.PurchaserID into result2
                            from q2 in result2
                            join u in db.Users on pd.CreatedBy equals u.UserID
                            where pd.PaymentID.Equals(id)
                            select new PaymentVM
                            {
                                PaymentAmount = (double)pd.PaymentAmount,
                                PaymentType = q1.PaymentType,
                                Reference = pd.Reference,
                                PaymentDate = pd.PaymentDate,
                                CreatedBy = u.FirstName+ " "+ u.LastName,
                                Firm= q2.PurchaserNo
                            }).ToList();

                return new CorparateResult<PaymentVM> { Status = Constants.CorparateStatus.Successful, Message = "Getted", Exist = true, GenericList = data };
            }
        }

        public object GetBalance(string pur)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PurChaserID", pur);
                var data = con.Query<object>("GetLedgerBalanceByID", para, null, true, 0, CommandType.StoredProcedure).ToList();
                return data;
               
            }
            
        }
    }
}