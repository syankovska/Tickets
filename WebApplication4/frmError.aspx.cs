using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class frmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if  (Session["Error"] == null) Session["Error"] = "Session expired";
            LabelError.Text = Convert.ToString(Session["Error"]);
        }
    }
}