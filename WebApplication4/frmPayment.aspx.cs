using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsBooking"] == null || Convert.ToInt32(Session["IsBooking"]) == 0)
             using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    int totalValueCents = Convert.ToInt32(Session["TotalValueCents"]) / 100;

                    try
                    {
                        TaslinkOrderResponse taslinkOrderResponse = new TaslinkOrderResponse();
                        taslinkOrderResponse = sc.GetTaslinkOrder(Convert.ToString(totalValueCents), HttpContext.Current.Request.Url.AbsoluteUri);

                        Response.Redirect("http://multiplex.taslink.com.ua/?oid=" + taslinkOrderResponse.oid);
                    }
                    catch (TimeoutException)
                    {

                    }
                }
            else
            {
                Server.Transfer("~/frmBooking.aspx");
            }
         
        }
    }
}