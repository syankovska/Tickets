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

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            Session["CinemaId"] = selKey;

            Session["CinemaName"] = (sender as GridView).SelectedRow.Cells[2].Text;
            Master.FindControl("HyperLinkNext").Visible = true;
        }
    }
}