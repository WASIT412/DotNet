using Corporate;
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
using System.Web;
using System.Web.Mvc;

namespace APIService.Controllers
{
    public class LedgerAPIController : Controller
    {
        //
        // GET: /LedgerAPI/

        public ActionResult Index()
        {
            return View();
        }

        public HttpResponseMessage PaymentsReport(string PurchaserID, string FromDate, string ToDate)
        {

            LocalReport lr = new LocalReport();
            string path = Server.MapPath("~/Reports/PurchaseOrder.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringOld"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Purchaser", PurchaserID);
                para.Add("@StartDate", FromDate);
                para.Add("@EndDate", ToDate);
                var data = con.Query<PaymentVM>("PaymentStatementByDate", para, null, true, 0, CommandType.StoredProcedure).ToList();

                ReportDataSource rd = new ReportDataSource("POInvoiceDSet", data.ToList());
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
                Response.AddHeader("content-disposition", "attachment; filename=PaymentREport" + date.ToString("dd/MM/yyyy") + "." + fileNameExtension);
                Response.BinaryWrite(RenderedBytes); // create the file
                Response.Flush();
                //End download option
                return File(RenderedBytes, mimeType);
            }

        }
    }
}
