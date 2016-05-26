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

            if (SiteMap.CurrentNode.Title.Equals("Выбор фильма") ||
                SiteMap.CurrentNode.Title.Equals("Выбор сеанса") ||
                SiteMap.CurrentNode.Title.Equals("Выбор кинотеатра"))
            {
                DropDownListDate.Visible = true;
                DropDownListCity.Visible = true;
            }
            else
            { 
                DropDownListDate.Visible = false;
                DropDownListCity.Visible = false;
            }

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

                    if (Session["CityName"] != null)
                        ObjectDataSource1.SelectParameters["city"].DefaultValue =
                     Session["CityName"].ToString();


                    if (Session["TicketDate"] == null)
                    {

                        DateTime ticketDate = sc.GetDistinctSessionDateByCity(Session["CityName"].ToString()).OrderBy(x => x).FirstOrDefault();
                        if (ticketDate != null)
                        {
                            DropDownListDate.SelectedIndex = 0;
                            Session["TicketDate"] = DropDownListDate.SelectedValue;
                        }
                    }
                    else
                    {
                        DropDownListDate.SelectedValue = Convert.ToString(Session["TicketDate"]);
                    }



                }
        }

        protected void DropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CityName"] = DropDownListCity.SelectedValue;
        }

        protected void DropDownListCity_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    if (Session["CityName"] == null)
                    {

                        string city = sc.GetCities().OrderBy(x => x).FirstOrDefault();
                        if (city != null)
                        {
                            DropDownListCity.SelectedIndex = 0;
                            Session["CityName"] = DropDownListCity.SelectedValue;
                        }
                    }
                    else DropDownListDate.SelectedValue = Convert.ToString(Session["TicketDate"]);

                }
        }
    }
}