using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Corporate.Contract;
using Corporate.Models;
using Corporate.ViewModel;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Corporate.Models
{
    public class Credential : ICredential
    {
        //  [Required] 
        public string UserName { get; set; }
        //  [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
        ConnectionString db = null;
        public CorparateResult<Credential> GetLogin(Credential login)
        {
            UserInfo userinfo = UserInfo.GetInstence;
            try
            {
                using (db = new ConnectionString())
                {
                    //db.Configuration.LazyLoadingEnabled = false;
                    var isUser = db.Users.Where(x => x.UserName.Equals(login.UserName) && x.Password.Equals(login.Password)).FirstOrDefault();
                    if (isUser != null)
                    {
                        userinfo.UserFullName = isUser.FirstName + ' ' + isUser.LastName;
                        userinfo.UserID = isUser.UserID;
                        return new CorparateResult<Credential> { Status = Constants.CorparateStatus.Successful, Message = "Login successfull", Exist = true };
                    }
                    return new CorparateResult<Credential> { Status = Constants.CorparateStatus.Error, Message = "Username or password invalid", Exist = false };
                }
            }
            catch (Exception ex)
            {
                return new CorparateResult<Credential> { Status = Constants.CorparateStatus.Error, Message = ex.ToString(), Exist = false };
            }
        }
        string ICredential.ChangePassword(LoginVM Login)
        {
            return "hello";
            throw new NotImplementedException();
        }

        public DashBoard LoadDashBoard()
        {
            db = new ConnectionString();
            var command = db.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[UspDashBoard]";
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                db.Database.Connection.Open();
                var reader = command.ExecuteReader();

                List<ThreeDayPayment> listThreeDayPayment = ((IObjectContextAdapter)db).ObjectContext.Translate<ThreeDayPayment>
                (reader).ToList();
                reader.NextResult();
                List<MonthPayments> listMonthPayments =
                    ((IObjectContextAdapter)db).ObjectContext.Translate<MonthPayments>
            (reader).ToList();
                var command2 = db.Database.Connection.CreateCommand();
                command2.CommandText = "[dbo].[UspDashBoardSevenDaysOrder]";
                command2.CommandType = CommandType.StoredProcedure;               
                var reader2 = command2.ExecuteReader();
                List<SevenDaySO> listSevenDaySO =
                     ((IObjectContextAdapter)db).ObjectContext.Translate<SevenDaySO>
             (reader2).ToList();
                reader2.NextResult();
                List<SevenDayPO> listSevenDayPO =
                    ((IObjectContextAdapter)db).ObjectContext.Translate<SevenDayPO>
            (reader2).ToList();

                DashBoard domainEntity = null;
                domainEntity = new DashBoard();
                domainEntity.Orders  = new DashBoardOrderEntity();
                domainEntity.PayFin  = new DashBoardMainEntity();

                domainEntity.PayFin.FinYear = listMonthPayments;
                domainEntity.PayFin.ThreeDays = listThreeDayPayment;
                domainEntity.Orders.SevenDaysPO = listSevenDayPO;
                domainEntity.Orders.SevenDaysSO = listSevenDaySO;
           
                db.Database.Connection.Close();
                return domainEntity;
            }

            finally
            {
                db.Database.Connection.Close();
            }
        }


    }
}
