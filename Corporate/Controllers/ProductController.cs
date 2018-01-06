using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Corporate.ViewModel;
using Corporate.Contract;
using Corporate.Models;

namespace Corporate.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        IProductMaster<ProductMaster> obj;
        IProduct<Product> objpro;
        AllProducts objgen = new AllProducts();
        public ProductController()
        {
            obj = new AllProducts();
            objpro = new AllProducts();
           
        }
        public ActionResult Index()
        {
            return View();
        }
         [MyAuthorize]
        public ActionResult Masters()
        {
            IEnumerable<ProductMasterVM> data = (IEnumerable<ProductMasterVM>)obj.GetAllProductMaster();
            return View(data);
        }

         [MyAuthorize]
         public ActionResult Items()
         {
             IEnumerable<ProductVM> data = (IEnumerable<ProductVM>)objpro.GetAllProduct();
             return View(data);
         }

        [MyAuthorize]
        public ActionResult Detail(int Selected)
        {
            CorparateResult<ProductMaster> data = obj.gettProductMasterById(Selected);
            if (data != null)
            {
                var pur = data.GenericOne;
                return View(pur);
            }
            else
                return RedirectToAction("Masters", "Product");
        }
        public JsonResult getCategories()
        {
            CorparateResult<DropDownVM> data = objgen.getCategories();
               
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            // return Json(Categories, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Detail(ProductMaster pur)
        {
            CorparateResult<ProductMaster> data = obj.saveProductMaster(pur);
            bool reply = data.Exist;
            if (!reply)
            {
                return View(pur);
            }

            return RedirectToAction("Masters", "Product");
        }
        [MyAuthorize]
        public ActionResult ProductDetail(int Selected)
        {
            ViewBag.Cat = objgen.getCategories();
           
            CorparateResult<Product> data = objpro.gettProductById(Selected);
            if (data != null)
            {
                ViewBag.Category = new SelectList(objgen.getCategoriesForDropDownItemVM().GenericList, "ProductMasterID", "Text", data.GenericOne.ProductMasterID);
                var pur = data.GenericOne;
                return View(pur);
            }
            else
                return RedirectToAction("Items", "Product");
        }
        [HttpPost]
        public ActionResult ProductDetail(Product pur)
        {
            CorparateResult<Product> data = objpro.saveProduct(pur);
            bool reply = data.Exist;
            if (!reply)
            {
                return View(pur);
            }

            return RedirectToAction("Items", "Product");
        }

        [HttpPost]
        public JsonResult SaveMaster(ProductMaster pur)
        {
            CorparateResult<ProductMaster> data = obj.saveProductMaster(pur);
            bool reply = data.Exist;
            string str = data.Message;
            int indx = str.IndexOf("Unique") + "methods".Length;
            return new JsonResult { Data = new { Respond = reply, Message = indx }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult SaveItem(Product pur)
        {
            CorparateResult<Product> data = objpro.saveProduct(pur);
            bool reply = data.Exist;
            string str = data.Message;
            int indx = str.IndexOf("Unique") + "methods".Length;
            return new JsonResult { Data = new { Respond = reply, Message = indx }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            CorparateResult<ProductMaster> data = obj.removeProductMaster(ID);
            bool reply = data.Exist;
            return new JsonResult { Data = new { Respond = reply }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult DeleteItem(int ID)
        {
            CorparateResult<Product> data = objpro.removeProduct(ID);
            bool reply = data.Exist;
            return new JsonResult { Data = new { Respond = reply }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult products()
        {
            return View();
        }

    }
}
