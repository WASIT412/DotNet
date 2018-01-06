using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Corporate.Extension;
using Corporate.ViewModel;
using Corporate.Models;
using Corporate.Contract;
using JSM.MVC4;
using System.Web.UI;

namespace Corporate.Controllers
{
    public class ActorController : Controller
    {
        // ConnectionString db = null;
        IPurchaser<Purchaser> objActor;
      public  ActorController()
        {
          objActor = new Actor();
        }
        [MyAuthorize]
        public ActionResult Purchaser()
        {
            IEnumerable<GetAllPurchaser1_Result> data = from cat in objActor.GetAllPurchasers() select  cat;
                return View(data);
        }
        [MyAuthorize]
        public ActionResult Detail(int Selected)
        {
              CorparateResult<Purchaser> data = objActor.gettPurchaserById(Selected);
                if (data != null)
                {
                    var pur = data.GenericOne;
                    return View(pur);
                }
                else
                    return RedirectToAction("Purchaser", "Actor");
            }
            
        [HttpPost]
        public ActionResult Detail(Purchaser pur)
        {
            CorparateResult<Purchaser> data = objActor.savePurchaser(pur);
            bool reply = data.Exist;
            if(!reply)
            {
                 return View(pur);
              }
            
            return RedirectToAction("Purchaser", "Actor");
        }

        [HttpPost]
        public JsonResult SavePurchaser(Purchaser pur, HttpPostedFileBase file)
        {
            CorparateResult<Purchaser> data = objActor.savePurchaser(pur);
            bool reply = data.Exist;
              string str=  data.Message;
            int indx = str.IndexOf("Unique") + "methods".Length;
            return new JsonResult { Data = new { Respond = reply, Message = indx }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult DeletePurchaser(int ID)
        {
            CorparateResult<Purchaser> data = objActor.removePurchaser(ID); 
                  bool reply= data.Exist;
                return new JsonResult { Data = new { Respond = reply }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }
    }
}
