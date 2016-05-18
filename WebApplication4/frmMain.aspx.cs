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
           
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selKey = (sender as GridView).SelectedDataKey.Value.ToString();
            GridViewRow row = GridView1.SelectedRow;
            Session["FilmId"] = selKey;
            Session["FilmName"] = Convert.ToString(row.Cells[2].Text);
                
        }
    }

}