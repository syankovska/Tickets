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
             if (Session["OrderUserSessionId"] != null)
            {
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                    SyCompleteOrderResponse syCompleteOrderResponse = new SyCompleteOrderResponse();
                    syCompleteOrderResponse = sc.CompleteOrder(Convert.ToString(Session["OrderUserSessionId"]),
                       Convert.ToInt32(Session["TotalValueCents"]), "", true,
                       Convert.ToString(Session["CustomerEmail"]),
                       Convert.ToString(Session["CustomerPhone"]),
                       Convert.ToString(Session["CustomerName"])
                                                  );
                    TextBoxOrderCompleteResult.Text = syCompleteOrderResponse.Result;

                    if (TextBoxOrderCompleteResult.Text.Equals("OK"))
                    {
                        Session["PrintStream"] = syCompleteOrderResponse.PrintStream;
                        HyperLinkDownload.Visible = true;

                        TextBoxCinemaName.Text = Convert.ToString(Session["CinemaName"]);
                        TextBoxFilmName.Text = Convert.ToString(Session["FilmName"]);
                        TextBoxShowTime.Text = Convert.ToString(Session["ShowTime"]);
                        TextBoxTotalValueCents.Text = Convert.ToString(Session["TotalValueCents"]);
                        TextBoxTotalOrderCount.Text = Convert.ToString(Session["TotalOrderCount"]);
                        TextBoxOrderedSeats.Text = Convert.ToString(Session["OrderedSeats"]);

                        Session["CinemaId"] = null;
                        Session["CinemaName"] = null;
                        Session["FilmId"] = null;
                        Session["FilmName"] = null;
                        Session["SessionId"] = null;
                        Session["ShowTime"] = null;
                        Session["UserSessionId"] = null;
                        Session["OrderUserSessionId"] = null;
                        Session["TotalValueCents"] = null;
                        Session["SelectedSeats"] = null;
                        Session["CustomerName"] = null;
                        Session["CustomerPhone"] = null;
                        Session["CustomerEmail"] = null;
                        Session["OrderedSeats"] = null;
                        Session["Error"] = "Session expired";
                    }
                    else HyperLinkDownload.Visible = false;
                }
            }
            else
            {
                TextBoxOrderCompleteResult.Text = "No session OrderUserSessionId";
                HyperLinkDownload.Visible = false;
            }

           
        }

        [System.Web.Services.WebMethod(enableSession: true)]
        public static bool SetPrintStream(string printStream)
        {
            HttpContext.Current.Session["PrintStream"] = printStream.ToString(); 
            return true;
        }
    }
}