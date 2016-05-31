using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label5.Text = "Complete Order Result";

            
                if (Session["UserSessionId"] != null)
                {
                    using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                    {
                        SyCompleteOrderResponse syCompleteOrderResponse = new SyCompleteOrderResponse();
                        syCompleteOrderResponse = sc.CompleteOrder(Convert.ToString(Session["UserSessionId"]),
                           Convert.ToInt32(Session["TotalValueCents"]), "", true,
                           Convert.ToString(Session["CustomerEmail"]),
                           Convert.ToString(Session["CustomerPhone"]),
                           Convert.ToString(Session["CustomerName"])
                                                      );
                        TextBox5.Text = syCompleteOrderResponse.Result;
                       Session["PrintStream"] = syCompleteOrderResponse.PrintStream;
                    
                    //SetPrintStream(syCompleteOrderResponse.PrintStream);
                    //  HttpContext.Current.Session["PrintStream"] = syCompleteOrderResponse.PrintStream;
                }
                    // Response.Redirect("~/GenPDFHandle.ashx");
                }
                else TextBox5.Text = "No session UserSessionId";
                       
        }

        [System.Web.Services.WebMethod(enableSession: true)]
        public static bool SetPrintStream(string printStream)
        {
            HttpContext.Current.Session["PrintStream"] = printStream.ToString(); 
            return true;
        }
    }
}