﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            if (!string.IsNullOrEmpty(Request.QueryString["oid"]))
            {
                string oid = Request.QueryString["oid"];

                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {

                    TaslinkStatusResponse taslinkStatusResponse = new TaslinkStatusResponse();
                    taslinkStatusResponse = sc.GetTaslinkStatus(oid);
                    TextBox1.Text = taslinkStatusResponse.oid;
                    Label1.Text = "oid";
                    TextBox2.Text = taslinkStatusResponse.respcode;
                    Label2.Text = "respcode";
                    TextBox3.Text = taslinkStatusResponse.reverse;
                    Label3.Text = "reverse";
                    TextBox4.Text = taslinkStatusResponse.tranid;
                    Label4.Text = "tranid";

                    Label5.Text = "Complete Order Result";

                    if (TextBox2.Text.Trim().Equals("Удачное выполнение транзакции"))
                    {
                        if (Session["UserSessionId"] != null)
                        {
                            SyCompleteOrderResponse syCompleteOrderResponse = new SyCompleteOrderResponse();
                            syCompleteOrderResponse = sc.CompleteOrder(Convert.ToString(Session["UserSessionId"]),
                               Convert.ToInt32(Session["TotalValueCents"]), taslinkStatusResponse.oid, false,
                               Convert.ToString(Session["CustomerEmail"]),
                               Convert.ToString(Session["CustomerName"]),
                               Convert.ToString(Session["CustomerPhone"])
                               );
                            TextBox5.Text = syCompleteOrderResponse.Result;
                            HyperLink1.NavigateUrl = "~/GenPDFHandle.ashx?printStream=" + syCompleteOrderResponse.PrintStream;

                           // Response.Redirect("~/GenPDFHandle.ashx");
                        }
                         else TextBox5.Text = "No session UserSessionId";


                    }
                    else TextBox5.Text = "Ошибка оплаты";

                }

            }
        }
    }
}