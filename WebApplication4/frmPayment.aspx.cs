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
            if (Convert.ToInt32(Session["IsBookingCust"]) == 0 || Convert.ToInt32(Session["IsBooking"]) == 0)
                try
                {
                    using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                    {

                        int totalValueCents = Convert.ToInt32(Session["TotalValueCents"]) / 100;

                        try
                        {
                            TaslinkOrderResponse taslinkOrderResponse = new TaslinkOrderResponse();
                            taslinkOrderResponse = sc.GetTaslinkOrder(Convert.ToString(totalValueCents), HttpContext.Current.Request.Url.AbsoluteUri);

                            Response.Redirect("http://multiplex.taslink.com.ua/?oid=" + taslinkOrderResponse.oid);
                        }
                        catch (System.ServiceModel.FaultException)
                        {
                            Session["Error"] = "Taslink is not accessable";
                            Server.Transfer("~/frmError.aspx");
                        }
                    }
                }
                catch (System.ServiceModel.CommunicationException)
                { }
              else
            {
                    Server.Transfer("~/frmBooking.aspx");
                }

                }
    }
}