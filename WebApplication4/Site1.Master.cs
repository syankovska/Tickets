using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TicketDate"] == null)
                Session["TicketDate"] = DateTime.Now.Date;

            Calendar1.SelectedDate = DateTime.Parse(Session["TicketDate"].ToString());

            if (SiteMap.CurrentNode.Title.Equals("Выбор фильма") ||
                SiteMap.CurrentNode.Title.Equals("Выбор сеанса") ||
                SiteMap.CurrentNode.Title.Equals("Выбор кинотеатра"))
                Calendar1.Visible = true;
            else Calendar1.Visible = false;


            if (!SiteMap.CurrentNode.Equals(SiteMap.RootNode))
            {
                HyperLinkPrev.Visible = true;
                HyperLinkPrev.Text = "<<"+SiteMap.CurrentNode.ParentNode.ToString();
                HyperLinkPrev.NavigateUrl = SiteMap.CurrentNode.ParentNode.Url;
            }
            else HyperLinkPrev.Visible = false;

            if (SiteMap.CurrentNode.HasChildNodes)
                {
                HyperLinkNext.Visible = true;
                HyperLinkNext.Text = SiteMap.CurrentNode.ChildNodes[0].ToString()+ ">>";
                HyperLinkNext.NavigateUrl = SiteMap.CurrentNode.ChildNodes[0].Url;
            }
            else HyperLinkNext.Visible = false;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["TicketDate"] = Calendar1.SelectedDate;
           

        }

        
    }
}