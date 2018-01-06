using Corporate.Models;
using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corporate.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/
        PaymentMod obj = new PaymentMod();
        public ActionResult ConfigSetUp()
        {
            return View();
        }

        public ActionResult PaymentMode()
        {
            IEnumerable<PaymentModeVM> data = (IEnumerable<PaymentModeVM>)obj.GetAllPayments();
            return View(data);
            
        }

        [HttpPost]
        public JsonResult savePayment(PaymentMode pur)
        {
            CorparateResult<PaymentMode> data = obj.savePayment(pur);
            bool reply = data.Exist;
            string str = data.Message;
            int indx = str.IndexOf("Unique") + "methods".Length;
            return new JsonResult { Data = new { Respond = reply, Message = indx }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            CorparateResult<PaymentMode> data = obj.removePaymentMode(ID);
            bool reply = data.Exist;
            return new JsonResult { Data = new { Respond = reply }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [MyAuthorize]
        public ActionResult Detail(int Selected)
        {
            CorparateResult<PaymentMode> data = obj.getPaymentModeId(Selected);
            if (data != null)
            {
                var pur = data.GenericOne;
                return View(pur);
            }
            else
                return RedirectToAction("Masters", "Product");
        }

        [HttpPost]
        public ActionResult Detail(PaymentMode pur)
        {
            CorparateResult<PaymentMode> data = obj.savePayment(pur);
            bool reply = data.Exist;
            if (!reply)
            {
                return View(pur);
            }

            return RedirectToAction("PaymentMode", "Setting");
        }


    }
}
