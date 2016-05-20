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
                    TextBoxName.Text = Convert.ToString(Session["CustomerPhone"]);
                if (Session["CustomerEmail"] != null)
                    TextBoxName.Text = Convert.ToString(Session["CustomerEmail"]);
            }
        }

        protected void ButtonInfo_Click(object sender, EventArgs e)
        {
            Session["CustomerName"] = TextBoxName.Text;
            Session["CustomerPhone"] = TextBoxPhone.Text;
            Session["CustomerEmail"] = TextBoxEmail.Text;
            Master.FindControl("HyperLinkNext").Visible = true;
        }
    }
}