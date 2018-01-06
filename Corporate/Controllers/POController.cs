using Corporate.Models;
using Corporate.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Corporate.Controllers
{
    public class POController : Controller
    {
        //
        // GET: /PO/
        PO obj = new PO();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePO()
        {
            string query = Request.QueryString["route"];
            string Param;

            if (query == null)
            {
                Param = "null";
            }
            else
            {
                Param = query;
            }

            IEnumerable<POGridVM> data = (IEnumerable<POGridVM>)obj.GetAllPO(Param);
            return View(data);
           
        }
        public ActionResult ViewOrderDetails(int OrderID,string Mini, string Macro, DateTime Date)
        {
            var data = obj.ViewOrderDetails(OrderID);
          //  data.GenericList[0].OrderNo = OrderNo;
            return View(data.GenericList);

        }
        [HttpPost]
        public JsonResult CreateOrder(OrderMaster pur)
        {
            CorparateResult<OrderMaster> data = obj.Save(pur);
            bool reply = data.Exist;
            string msg = data.Message;

            return new JsonResult { Data = new { Respond = reply, Message = msg }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getOrderType()
        {
            CorparateResult<DropDownVM> data = obj.getPaymentType();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getPurchaser()
        {
            CorparateResult<DropDownVM> data = obj.getPurchaser();
            ViewData["Firm"] = data;
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getCategories()
        {
            CorparateResult<DropDownVM> data = obj.getCategories();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getProducts(int CatID)
        {
            CorparateResult<DropDownVM> data = obj.getProducts(CatID);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Purchase_Invoice(string OrderNo)
        {
            LocalReport lr = new LocalReport();
            string path = Server.MapPath("~/Reports/PurchaseOrder.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            using (ConnectionString db = new ConnectionString())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var go = (from a in db.Generate_POInvoice(OrderNo) select a);
                ReportDataSource rd = new ReportDataSource("POInvoiceDSet", go.ToList());
                  lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;
                string deviceinfo = "<DeviceInfo>" + "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>7.5in</PageWidth>" +
                    "<PageHeight>11in</PageHeight>" +
                    "<MarginTop>0.2in</MarginTop>" +
                    "<MarginLeft>0.3in</MarginLeft>" +
                    "<MarginRight>0.3in</MarginRight>" +
                      "<MarginBottom>0.5in</MarginBottom></DeviceInfo>";
                byte[] RenderedBytes;
                string[] streams;
                Warning[] warnings;
                RenderedBytes = lr.Render(reportType, deviceinfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                //For download option Commet to stop for download below
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + OrderNo + "_" + date.ToString("dd/MM/yyyy") + "." + fileNameExtension);
                Response.BinaryWrite(RenderedBytes); // create the file
                Response.Flush();
                //End download option
                return File(RenderedBytes, mimeType);
            }
        }

    }
}
