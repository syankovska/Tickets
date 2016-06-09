using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class frmCinemas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ObjectDataSource1 != null)
            {
               if (Session["CityName"] != null)
                    ObjectDataSource1.SelectParameters["city"].DefaultValue =
                 Session["CityName"].ToString();
                if (Session["FilmId"] != null)
                    ObjectDataSource1.SelectParameters["scheduledFilmId"].DefaultValue =
                 Session["FilmId"].ToString();
            }

            //if (!IsPostBack)
            //{
            //    Session["FilmId"] = null;
            //    Session["FilmName"] = null;
            //}
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            Session["CinemaId"] = selKey;

            Session["CinemaName"] = (sender as GridView).SelectedRow.Cells[2].Text;
            //Master.FindControl("HyperLinkNext").Visible = true;
            Response.Redirect((Master.FindControl("HyperLinkNext") as HyperLink).NavigateUrl);
        }
    }
}