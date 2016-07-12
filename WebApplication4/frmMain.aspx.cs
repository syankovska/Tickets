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
            else
            {
                Session["IsBooking"] = -1;
                booking = "&&IsBooking=" + Session["IsBooking"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["SessionId"]) && !string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["SessionId"] = (Request.QueryString["SessionId"]);
                Session["CinemaId"] = (Request.QueryString["CinemaId"]);


                Response.Redirect("~/frmSeats.aspx?" + "SessionId=" + Session["SessionId"]+"&&" + "CinemaId=" + Session["CinemaId"]+ booking);
                Master.FindControl("SiteMapPath1").Visible = false;
                //http://localhost:4349/frmSeats.aspx?SessionId=10706&&CinemaId=0000000002

            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["CinemaId"] = (Request.QueryString["CinemaId"]);
                Response.Redirect("~/frmSessionsByCinema.aspx?" + "CinemaId=" + Session["CinemaId"]+ booking);
                //0000000002
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["FilmId"]))
            {
            
                Session["FilmId"] = (Request.QueryString["FilmId"]);
                Response.Redirect("~/frmSessions.aspx?"+"FilmId="+ Session["FilmId"]+ booking);
               //HO00000184
            }

          


  

            if (Session["CityName"] != null)
                ObjectDataSource1.SelectParameters["city"].DefaultValue =
             Session["CityName"].ToString();


            if (!IsPostBack)
            {
                if (Session["OrderUserSessionId"] != null)
                    using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                        sc.CancelOrder(Convert.ToString(Session["OrderUserSessionId"]));

                Session["CinemaId"] = null;
                Session["CinemaName"] = null;
                Session["FilmId"] = null;
                Session["FilmName"] = null;
                Session["SessionId"] = null;
                Session["ShowTime"] = null;
                Session["UserSessionId"] = null;
                Session["OrderUserSessionId"] = null;
                Session["TotalValueCents"] = null;
                Session["TotalOrderCount"] = null;
                Session["SelectedSeats"] = null;


            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            GridViewRow row = GridView1.SelectedRow;
            Session["FilmId"] = selKey;
            Session["FilmName"] = Convert.ToString(row.Cells[2].Text);
            // Master.FindControl("HyperLinkNext").Visible = true;
            Response.Redirect((Master.FindControl("HyperLinkNext") as HyperLink).NavigateUrl);
            
        }
    }

}