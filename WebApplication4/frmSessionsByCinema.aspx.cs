using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmSessionsByCinema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["IsBooking"]))
            {
                Session["IsBooking"] = (Request.QueryString["IsBooking"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["CinemaId"] = Request.QueryString["CinemaId"];

                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                    var c = sc.GetCinemas().Where(x => x.ID.Equals(Convert.ToString(Session["CinemaId"]))).FirstOrDefault();
                    if (c != null)
                        Session["CinemaName"] = c.Name;
                    //HO00000184
                }

            }

            if (Session["UserSessionId"] != null)
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                    sc.CancelOrder(Convert.ToString(Session["UserSessionId"]));

            if (ObjectDataSource1 != null)
            {
                if (Session["TicketDate"] == null)

                    Session["TicketDate"] = DateTime.Now.Date.ToString();

                ObjectDataSource1.SelectParameters["sessionBusinessDate"].DefaultValue =
                Session["TicketDate"].ToString();

                if (Session["CinemaId"] != null)
                    ObjectDataSource1.SelectParameters["cinemaId"].DefaultValue =
                 Session["CinemaId"].ToString();


            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedDataKey != null)
            {
                Session["SessionId"] = GridView1.SelectedDataKey.Values[0];
                Session["CinemaId"] = GridView1.SelectedDataKey.Values[1];
                Session["FilmId"] = GridView1.SelectedDataKey.Values[2];
            }

            Session["FilmName"] = (sender as GridView).SelectedRow.Cells[4].Text;
            Session["ShowTime"] = (sender as GridView).SelectedRow.Cells[6].Text;

           // Master.FindControl("HyperLinkNext").Visible = true;
            Response.Redirect((Master.FindControl("HyperLinkNext") as HyperLink).NavigateUrl);
        }

    }
}