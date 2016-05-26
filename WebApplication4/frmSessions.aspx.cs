using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["IsBooking"]))
            {
                Session["IsBooking"] = (Request.QueryString["IsBooking"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["FilmId"]))
            {

                Session["FilmId"] = Request.QueryString["FilmId"];

                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                     var  f  = sc.GetAllFilms().Where(x => x.ScheduledFilmId.Equals(Convert.ToString(Session["FilmId"]))).FirstOrDefault();
                    if (f != null)
                        Session["FilmName"] = f.Title;
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

                if (Session["FilmId"] != null)
                    ObjectDataSource1.SelectParameters["scheduledFilm"].DefaultValue =
                 Session["FilmId"].ToString();

                if (Session["CityName"] != null)
                    ObjectDataSource1.SelectParameters["city"].DefaultValue =
                 Session["CityName"].ToString();
            }
        }

    

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            //string script = "alert(\"" + selKey + "\");";
            //ScriptManager.RegisterStartupScript(this, GetType(),
            //                      "ServerControlScript", script, true);
            if (GridView1.SelectedDataKey != null)
            {
                Session["SessionId"] =  GridView1.SelectedDataKey.Values[0];
                Session["CinemaId"] = GridView1.SelectedDataKey.Values[1];
            }

            Session["CinemaName"] = (sender as GridView).SelectedRow.Cells[4].Text;
            Session["ShowTime"] = (sender as GridView).SelectedRow.Cells[6].Text;

            Master.FindControl("HyperLinkNext").Visible = true;
        }

    }
}