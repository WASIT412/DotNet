using Corporate.Models;
using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Contract
{
    public interface IProductMaster<T>
    {
        IEnumerable<ProductMasterVM> GetAllProductMaster();
            CorparateResult<T> gettProductMasterById(int cus);
            CorparateResult<T> saveProductMaster(ProductMaster p);
            CorparateResult<T> removeProductMaster(int cus);      
    }

    public interface IProduct<T>
    {
        IEnumerable<ProductVM> GetAllProduct();
        CorparateResult<T> gettProductById(int cus);
        CorparateResult<T> saveProduct(Product p);
        CorparateResult<T> removeProduct(int cus);
    }
}