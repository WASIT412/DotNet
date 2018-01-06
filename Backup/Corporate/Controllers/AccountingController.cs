using Corporate.Models;
using Corporate.ViewModel;
using Dapper;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Corporate.Controllers
{
    public class AccountingController : Controller
    {
        //
        // GET: /Accounting/
        LedgerBook obj = new LedgerBook();
        public ActionResult Index()
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


            IEnumerable<LedgerVM> data = (IEnumerable<LedgerVM>)obj.GetLedger(Param);
            return View(data);
        }

        public ActionResult Payment()
        {

            string search = Request.QueryString["query"];
            string query = Request.QueryString["route"];
            string Param;

            if (query == null)
            {
                Param = "null";
            IEnumerable<PaymentVM> data = (IEnumerable<PaymentVM>)obj.GetPayments(Param);
            if (search == null)
            {
                return View(data);
            }
            else
            {
                return View(data.Where(x => x.PaymentType == search));
            }
            
            }
            else
            {
                Param = query;
                IEnumerable<PaymentVM> data = (IEnumerable<PaymentVM>)obj.GetPayments(Param);
                if (search == null)
                {
                    return View(data);
                }
                else
                {
                    return View(data.Where(x => x.PaymentType == search));
                }
                
            }
        }
        [HttpPost]
        public JsonResult Save(PaymentDetail pay)
        {
            CorparateResult<PaymentDetail> data = obj.AddPayment(pay);
            bool reply = data.Exist;
            string msg = data.Message;
            return new JsonResult { Data = new { Respond = reply, Message = msg }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult PaymentDetail(int RecieptID, DateTime Date)
        {
            var data = obj.PaymentDetail(RecieptID);
            return View(data.GenericList);

        }
        public ActionResult PaymentReport(string PurchaserID, string FromDate, string ToDate)
        {
            string path="";
           //urchaserID = PurchaserID.ToString();
            if (PurchaserID == "null")
            {
                PurchaserID = "null";
                path = Server.MapPath("~/Reports/AllPayment.rdlc");
            }
            if (FromDate == "" || ToDate == "")
            {
                FromDate = "null";
                ToDate = "null";
                if (PurchaserID == "null")
                {
                    path = Server.MapPath("~/Reports/AllPayment.rdlc");
                }
                else
                {
                    path = Server.MapPath("~/Reports/PaymentReport.rdlc");
                }
            }
          
            LocalReport lr = new LocalReport();
           
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Purchaser", PurchaserID);
                para.Add("@StartDate", FromDate);
                para.Add("@EndDate", ToDate);
                var data = con.Query<PaymentVM>("PaymentStatementByDate", para, null, true, 0, CommandType.StoredProcedure).ToList();
                ReportDataSource rd;
                if (PurchaserID == "null")
                {
                    rd = new ReportDataSource("AllPaymentDS", data.ToList());
                }
                else
                {
                   rd = new ReportDataSource("PaymentReportDS", data.ToList());
                }
                // var go = (from a in db.PaymentStatementByDate("null","null","null) select a);
               // ReportDataSource rd = new ReportDataSource("AllPaymentDS", data.ToList());
                lr.DataSources.Add(rd);
                string OrderNo = "Pay";
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

        public JsonResult GetBalance(string ID)
        {
            var data = obj.GetBalance(ID);

            return new JsonResult { Data = new {Bal= data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult PaymentReceipt(string OrderNo)
        {
            LocalReport lr = new LocalReport();
            string path = Server.MapPath("~/Reports/PaymentReceipt.rdlc");
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
                var go = (from a in db.Generate_PaymentReceipt(OrderNo) select a);
                ReportDataSource rd = new ReportDataSource("PaymentReceiptDS", go.ToList());
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;
                string deviceinfo = "<DeviceInfo>" + "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>7.5in</PageWidth>" +
                    "<PageHeight>5.5in</PageHeight>" +
                    "<MarginTop>0.2in</MarginTop>" +
                    "<MarginLeft>0.3in</MarginLeft>" +
                    "<MarginRight>0.3in</MarginRight>" +
                      "<MarginBottom>0.1in</MarginBottom></DeviceInfo>";
                byte[] RenderedBytes;
                string[] streams;
                Warning[] warnings;
                RenderedBytes = lr.Render(reportType, deviceinfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                //For download option Commet to stop for download below
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + "PaymentReceipt" + OrderNo + "_" + date.ToString("dd/MM/yyyy") + "." + fileNameExtension);
                Response.BinaryWrite(RenderedBytes); // create the file
                Response.Flush();
                //End download option
                return File(RenderedBytes, mimeType);
            }
        }
    }
}
