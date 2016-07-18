using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    /// <summary>
    /// Summary description for GenPDFHandle
    /// </summary>
    public class GenPDFHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
         
            if (context.Session["PrintStream"] != null)
            { 
                string printStream  = context.Session["PrintStream"].ToString();
            
                context.Response.AddHeader("content-disposition", "attachment; filename=Tickets.pdf");
                context.Response.ContentType = "application/pdf";

                System.IO.MemoryStream pdfStm = new System.IO.MemoryStream();
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {


                    pdfStm = sc.GeneratePdf(printStream,Convert.ToInt32(context.Session["TotalOrderCount"]));
                }


                pdfStm.Seek(0, System.IO.SeekOrigin.Begin);
                context.Response.BinaryWrite(pdfStm.ToArray());

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}