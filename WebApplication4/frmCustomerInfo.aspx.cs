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
            (Master.FindControl("HyperLinkNext") as HyperLink).Visible = true;
            (Master.FindControl("HyperLinkNext") as HyperLink).Enabled = false;
            if (!IsPostBack)
            {
                if (Session["CustomerName"] != null)
                    // TextBoxName.Text = Convert.ToString(Session["CustomerName"]);
                    TextBoxName.Text = string.Empty;
                if (Session["CustomerPhone"] != null)
                    // TextBoxPhone.Text = Convert.ToString(Session["CustomerPhone"]);
                    TextBoxPhone.Text = string.Empty;
                if (Session["CustomerEmail"] != null)
                    // TextBoxEmail.Text = Convert.ToString(Session["CustomerEmail"]);
                    TextBoxEmail.Text = string.Empty;

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
                /*else
                {
                    LabelIsBooking.Visible = false;
                    CheckBoxIsBooking.Visible = false;
                }*/
                Session["IsBookingCust"] = Session["IsBooking"];
                if (Convert.ToInt32(Session["IsBookingCust"]) == 0)
                    (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Оплата>>";
                else if (Convert.ToInt32(Session["IsBookingCust"]) == 1)
                    (Master.FindControl("HyperLinkNext") as HyperLink).Text = "Бронирование>>";
            }

     

                      

            if (!TextBoxPhone.Text.Trim().Equals("") && !TextBoxEmail.Text.Trim().Equals(""))
            {
                (Master.FindControl("HyperLinkNext") as HyperLink).Enabled = true;
            }
        }

        protected void TextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxPhone.Text.Trim().Equals("") && !TextBoxEmail.Text.Trim().Equals(""))
            {
                Session["CustomerName"] = TextBoxName.Text;
                Session["CustomerPhone"] = TextBoxPhone.Text;
                Session["CustomerEmail"] = TextBoxEmail.Text;

         
                (Master.FindControl("HyperLinkNext") as HyperLink).Enabled  = true;

            } else
                (Master.FindControl("HyperLinkNext") as HyperLink).Enabled = false;

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
            
          
        }
    }
}