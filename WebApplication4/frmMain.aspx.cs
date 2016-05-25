using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string booking = "";
            if (!string.IsNullOrEmpty(Request.QueryString["IsBooking"]))
            {
                Session["IsBooking"] = (Request.QueryString["IsBooking"]);
                booking = "&&IsBooking=" + Session["IsBooking"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["SessionId"]) && !string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["SessionId"] = (Request.QueryString["SessionId"]);
                Session["CinemaId"] = (Request.QueryString["CinemaId"]);

              
                Server.Transfer("~/frmSeats.aspx?" + "SessionId=" + Session["SessionId"]+"&&" + "CinemaId=" + Session["CinemaId"]+ booking);
                Master.FindControl("SiteMapPath1").Visible = false;
                //http://localhost:4349/frmSeats.aspx?SessionId=10706&&CinemaId=0000000002

            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["CinemaId"] = (Request.QueryString["CinemaId"]);
                Server.Transfer("~/frmSessionsByCinema.aspx?" + "CinemaId=" + Session["CinemaId"]+ booking);
                //0000000002
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["FilmId"]))
            {
            
                Session["FilmId"] = (Request.QueryString["FilmId"]);
                Server.Transfer("~/frmSessions.aspx?"+"FilmId="+ Session["FilmId"]+ booking);
               //HO00000184
            }

          


            if (Session["UserSessionId"] != null)
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                    sc.CancelOrder(Convert.ToString(Session["UserSessionId"]));
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            GridViewRow row = GridView1.SelectedRow;
            Session["FilmId"] = selKey;
            Session["FilmName"] = Convert.ToString(row.Cells[2].Text);
            Master.FindControl("HyperLinkNext").Visible = true;

        }
    }

}