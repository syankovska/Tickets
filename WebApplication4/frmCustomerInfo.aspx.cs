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
            if (!IsPostBack)
            {
                if (Session["CustomerName"] != null)
                    TextBoxName.Text = Convert.ToString(Session["CustomerName"]);
                if (Session["CustomerPhone"] != null)
                    TextBoxPhone.Text = Convert.ToString(Session["CustomerPhone"]);
                if (Session["CustomerEmail"] != null)
                    TextBoxEmail.Text = Convert.ToString(Session["CustomerEmail"]);
                if (Session["IsBooking"] != null)
                { 
                LabelIsBooking.Visible = false;
                CheckBoxIsBooking.Visible = false;
                }

                if (!TextBoxPhone.Text.Trim().Equals("") && !TextBoxEmail.Text.Trim().Equals(""))
                {
                    Master.FindControl("HyperLinkNext").Visible = true;
                }
            }

        }

        protected void ButtonInfo_Click(object sender, EventArgs e)
        {
          
        }

        protected void TextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxPhone.Text.Trim().Equals("") && !TextBoxEmail.Text.Trim().Equals(""))
            {
                Session["CustomerName"] = TextBoxName.Text;
                Session["CustomerPhone"] = TextBoxPhone.Text;
                Session["CustomerEmail"] = TextBoxEmail.Text;

                Master.FindControl("HyperLinkNext").Visible = true;
            }
        }

        protected void CheckBoxIsBooking_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxIsBooking.Visible)
                 {
                if (CheckBoxIsBooking.Checked)
                Session["IsBooking"] = "1";
               else Session["IsBooking"] = "0";
            }
        }
    }
}