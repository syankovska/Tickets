using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class frmCustomerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TotalValueCents"] == null)
                            Server.Transfer("~/frmError.aspx");


            if (!IsPostBack)
            {

                (Master.FindControl("HyperLinkNext") as HyperLink).Visible = true;
                (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("onclick", "hrefCustomerInfoSubmitClick(HyperLinkNext)");
                (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("href", "#");

                if (Session["CustomerName"] != null)
                    TextBoxName.Text = Convert.ToString(Session["CustomerName"]);

                if (Session["CustomerPhone"] != null)
                    TextBoxPhone.Text = Convert.ToString(Session["CustomerPhone"]);

                if (Session["CustomerEmail"] != null)
                    TextBoxEmail.Text = Convert.ToString(Session["CustomerEmail"]);

                if (Convert.ToInt32(Session["IsBooking"]) == -1)
                {
                    LabelIsBooking.Visible = true;
                    CheckBoxIsBooking.Visible = true;
                }
                else if (Convert.ToInt32(Session["IsBooking"]) == 1)
                {
                    LabelIsBooking.Visible = true;
                    CheckBoxIsBooking.Visible = true;
                    CheckBoxIsBooking.Checked = true;
                }
                else
                {
                    LabelIsBooking.Visible = false;
                    CheckBoxIsBooking.Visible = false;
                }

                Session["IsBookingCust"] = Session["IsBooking"];
                if (Convert.ToInt32(Session["IsBookingCust"]) == 0)
                {
                    (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Оплата>>";
                    (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("href", "#");

                }
                else if (Convert.ToInt32(Session["IsBookingCust"]) == 1)
                {
                    (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Бронирование>>";
                    (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("href", "#");
                }
            }

     
            if (!TextBoxPhone.Text.Trim().Equals("") && !TextBoxEmail.Text.Trim().Equals(""))
            {
                (Master.FindControl("HyperLinkNext") as HyperLink).Enabled = true;
            }
        }


        protected void CheckBoxIsBooking_CheckedChanged(object sender, EventArgs e)
        {
            
                if (CheckBoxIsBooking.Checked)
                {
                    Session["IsBookingCust"] = 1;
                  (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Бронирование>>";

                }
                else
                {
                    Session["IsBookingCust"] = 0;
                    (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Оплата>>";
                }

          
                Session["CustomerName"] = TextBoxName.Text;
                Session["CustomerPhone"] = TextBoxPhone.Text;
                Session["CustomerEmail"] = TextBoxEmail.Text;


        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (CheckBoxIsBooking.Checked)
            {
                Session["IsBookingCust"] = 1;
            }
            else
            {
                Session["IsBookingCust"] = 0;
            }

            Session["CustomerName"] = TextBoxName.Text;
            Session["CustomerPhone"] = TextBoxPhone.Text;
            Session["CustomerEmail"] = TextBoxEmail.Text;

            Response.Redirect("~/frmPayment.aspx");

        }
    }
}