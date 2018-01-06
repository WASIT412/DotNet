using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Corporate.Models;
using Corporate.ViewModel;

namespace Corporate.Contract
{
    public interface IPurchaser <T>
    {
        IEnumerable<GetAllPurchaser1_Result> GetAllPurchasers();
        CorparateResult<T> gettPurchaserById(int cus);
        CorparateResult<T> savePurchaser(Purchaser cus);
        CorparateResult<T> editPurchaser(Purchaser cus);
        CorparateResult <T>removePurchaser(int cus);
    }
    public interface ICustomer<T>
    {
        IEnumerable<T> GetAllCustomers();
        CorparateResult<T> gettCustomerById(int cus);
        CorparateResult<T> saveCustomer(Customer cus);
         CorparateResult <T>editCustomer(Customer cus);
         CorparateResult <T>removeCustomer(int cus);
    
    }
}