using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Corporate.Models;
using Corporate.Contract;
using Highsoft.Web.Mvc.Charts;
using System.Web.Security;

namespace Corporate.Controllers
{
    public class AccountController : Controller
    {
        ICredential objCredential;
      public AccountController()
        {
            objCredential = new Credential();
           
        }
        // GET: /Account/
       // ConnectionString db = null;DBAccess.GetInstence;
      UserInfo userinfo = UserInfo.GetInstence;
      UserInfo ii = new UserInfo();
     
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Credential user)
        {

            CorparateResult<Credential> User = objCredential.GetLogin(user);
           if (User.Exist)
           {
               Session["UserId"] = userinfo.UserID.ToString();
               return RedirectToAction("DashBoard");
           }
           else
           {
               ModelState.AddModelError("Error", User.Message.ToString());
               return View(user);
           }  
        }
        [MyAuthorize]
        public ActionResult DashBoard()
        {
            var data =objCredential.LoadDashBoard();

            List<PieSeriesData> pieData = new List<PieSeriesData>();
          // pieData.Add(new PieSeriesData { Name = "Total "+data.Orders.SevenDaysPO.Count.ToString() });
            foreach(var i in data.Orders.SevenDaysPO)
            {
                pieData.Add(new PieSeriesData { Name = i.Date + "(" + i.SevenDaysPO.ToString() + ")", Y = i.SevenDaysPO, ClassName = i.SevenDaysPO.ToString(), Drilldown = i.SevenDaysPO.ToString() });
            }

           // foreach (var i in data.Orders.SevenDaysSO)
           // {
           //     pieData.Add(new PieSeriesData { Name = i.Date, Y = i.SevenDaysSO, Description = data.Orders.SevenDaysPO.Count.ToString() });
           // }

           // pieData.Add(new PieSeriesData { Name = "PurchaseOrder", Y = data.Orders.SevenDaysPO.Count, Description = data.Orders.SevenDaysPO.Count.ToString() });
          //  pieData.Add(new PieSeriesData { Name = "SalesOrder", Y = data.Orders.SevenDaysSO.Count });
          //  pieData.Add(new PieSeriesData { Name = "Chrome", Y = 12.8, Sliced = true, Selected = true });
         //   pieData.Add(new PieSeriesData { Name = "Safari", Y = 8.5 });
         //   pieData.Add(new PieSeriesData { Name = "Opera", Y = 6.2 });
         //   pieData.Add(new PieSeriesData { Name = "Others", Y = 0.7 });


           // List<double> tokyoValues = new List<double> { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 };
           // List<double> nyValues = new List<double> { -0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5 };
           // List<double> berlinValues = new List<double> { -0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0 };
           // List<double> londonValues = new List<double> { 3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8 };
           // List<LineSeriesData> tokyoData = new List<LineSeriesData>();
           // List<LineSeriesData> nyData = new List<LineSeriesData>();
           // List<LineSeriesData> berlinData = new List<LineSeriesData>();
            List<LineSeriesData> mydata = new List<LineSeriesData>();

            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.April) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.May) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.July) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.July) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.August) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Sept) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Oct) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Nov) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Decm) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Jan) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.Feb) }));
            data.PayFin.FinYear.ForEach(p => mydata.Add(new LineSeriesData { Y = Convert.ToDouble(p.March) }));
           // nyValues.ForEach(p => nyData.Add(new LineSeriesData { Y = p }));
           // berlinValues.ForEach(p => berlinData.Add(new LineSeriesData { Y = p }));
           // londonValues.ForEach(p => londonData.Add(new LineSeriesData { Y = p }));


            ViewData["AvgPay"] = mydata;
         //   ViewData["nyData"] = nyData;
          //  ViewData["berlinData"] = berlinData;
         //   ViewData["londonData"] = londonData;

            ViewData["pieData"] = pieData;

             return View();
          
        }

        public ActionResult LogOut()
        {
           
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
           // return RedirectToAction("Login","Account");
            Response.Redirect("~/Account/Login");
           return View();
            
        }
       
    }
}
