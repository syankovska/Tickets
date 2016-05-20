using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            if (!SiteMap.CurrentNode.Equals(SiteMap.RootNode))
            {
                HyperLinkPrev.Visible = true;
                HyperLinkPrev.Text = "<<"+SiteMap.CurrentNode.ParentNode.ToString();
                HyperLinkPrev.NavigateUrl = SiteMap.CurrentNode.ParentNode.Url;
            }
            else HyperLinkPrev.Visible = false;

            if (SiteMap.CurrentNode.HasChildNodes)
            {
             //   HyperLinkNext.Visible = true;
                HyperLinkNext.Text = SiteMap.CurrentNode.ChildNodes[0].ToString() + ">>";
                HyperLinkNext.NavigateUrl = SiteMap.CurrentNode.ChildNodes[0].Url;
            }
            else HyperLinkNext.Visible = false;
        }

        protected void DropDownListDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["TicketDate"] = DropDownListDate.SelectedValue;
          //  Server.Transfer(SiteMap.CurrentNode.Url);
        }

       
        protected void DropDownListDate_DataBound1(object sender, EventArgs e)
        {
            if (!IsPostBack)
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    if (Session["TicketDate"] == null)
                    {

                        DateTime ticketDate = sc.GetDistinctSessionDate().OrderBy(x => x).FirstOrDefault();
                        if (ticketDate != null)
                        {
                            DropDownListDate.SelectedIndex = 0;
                            Session["TicketDate"] = DropDownListDate.SelectedValue;
                        }
                    }
                    else DropDownListDate.SelectedValue = Convert.ToString(Session["TicketDate"]);

                }
        }
    }
}