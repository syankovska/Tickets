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
                           Convert.ToString(Session["CustomerName"]),
                           Convert.ToString(Session["CustomerPhone"])
                           );
                        TextBox5.Text = syCompleteOrderResponse.Result;
                        HyperLink1.NavigateUrl = "~/GenPDFHandle.ashx?printStream=" + syCompleteOrderResponse.PrintStream;
                    }
                    // Response.Redirect("~/GenPDFHandle.ashx");
                }
                else TextBox5.Text = "No session UserSessionId";
                       
        }
    }
}