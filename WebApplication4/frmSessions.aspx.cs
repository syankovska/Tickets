using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class frmSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ObjectDataSource1 != null)
            {
                if (Session["TicketDate"] == null)

                    Session["TicketDate"] = DateTime.Now.Date.ToString();

                ObjectDataSource1.SelectParameters["sessionBusinessDate"].DefaultValue =
                Session["TicketDate"].ToString();

                if (Session["FilmId"] != null)
                    ObjectDataSource1.SelectParameters["scheduledFilmId"].DefaultValue =
                 Session["FilmId"].ToString();


            }
        }

        public void SetDate()
        {
            return;
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
            Session["ShowTime"] = (sender as GridView).SelectedRow.Cells[7].Text;
     

        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}