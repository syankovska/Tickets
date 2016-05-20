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
            }

            Session["CinemaName"] = (sender as GridView).SelectedRow.Cells[4].Text;
            Session["ShowTime"] = (sender as GridView).SelectedRow.Cells[6].Text;

            Master.FindControl("HyperLinkNext").Visible = true;
        }

    }
}