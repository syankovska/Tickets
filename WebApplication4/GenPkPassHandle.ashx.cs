using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    /// <summary>
    /// Summary description for GenPkPassHandle
    /// </summary>
    public class GenPkPassHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string passNum = "";
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["passNum"]))
                  passNum = Convert.ToString(HttpContext.Current.Request.QueryString["passNum"]);


            if (context.Session["PrintStream"] != null)
            {
                string printStream = context.Session["PrintStream"].ToString();

                context.Response.AddHeader("content-disposition", "attachment; filename=pass"+ passNum + ".pkpass");
        
                context.Response.ContentType = "application/file";


                System.IO.MemoryStream smt= new System.IO.MemoryStream();
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    smt = sc.GeneratePkPass(printStream, Convert.ToInt32(passNum));
                }


                smt.Seek(0, System.IO.SeekOrigin.Begin);


                context.Response.BinaryWrite(smt.ToArray());



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