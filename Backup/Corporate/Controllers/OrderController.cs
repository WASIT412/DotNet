using Corporate.Models;
using Corporate.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corporate.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
       Inventory obj = new Inventory();
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult GetStock()
        {
          IEnumerable<ShowInventoryVM> data =obj.GetStock("null","null");
         // var s = data.Count(e => e.Quantity);
            return View(data);
           
        }
    }
}
