﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class frmSeatsTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             Server.Transfer("~/frmSeats.aspx");
        }
    }
}