using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    /// <summary>
    /// Summary description for GenPDFHandle
    /// </summary>
    public class GenPDFHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["printStream"]))
            {
                string printStream = context.Request.QueryString["printStream"];

                context.Response.AddHeader("content-disposition", "attachment; filename=Tickets.pdf");
                context.Response.ContentType = "application/pdf";

                System.IO.MemoryStream pdfStm = new System.IO.MemoryStream();
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    //string clientName, string cinemaName, string filmName, string showTime
                    pdfStm = sc.GeneratePdf(printStream);
                }

                //          context.Response.WriteFile("~/Requirements Specification_Payments_Multiplex_ENG.pdf");

                //byte[] fileBytes = null;
                //byte[] buffer = new byte[4096];
                //System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                //int chunkSize = 0;
                //do
                //{
                //    chunkSize = fileStream.Read(buffer, 0, buffer.Length);
                //    memoryStream.Write(buffer, 0, chunkSize);
                //} while (chunkSize != 0);
                //fileBytes = memoryStream.ToArray();

                pdfStm.Seek(0, System.IO.SeekOrigin.Begin);

                //=====================================================================================//
                //=======>>>MASUN!!!! Nam nugen bil context.Response.BinaryWrite vmesto prosto Write =)
                // A vot tut vasche na letu delaut - http://www.pdfsharp.com/PDFsharp/index.php?option=com_content&task=view&id=26&Itemid=37
                //=====================================================================================//
                context.Response.BinaryWrite(pdfStm.ToArray());



                //context.Response.ContentType = "text/plain";
                //context.Response.Write("Hello World");
                //sofa.png
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