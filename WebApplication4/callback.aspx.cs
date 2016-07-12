using System;
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
                        if (Session["OrderUserSessionId"] != null)
                        {
                            SyCompleteOrderResponse syCompleteOrderResponse = new SyCompleteOrderResponse();
                            syCompleteOrderResponse = sc.CompleteOrder(Convert.ToString(Session["OrderUserSessionId"]),
                               Convert.ToInt32(Session["TotalValueCents"]), taslinkStatusResponse.oid, false,
                               Convert.ToString(Session["CustomerEmail"]),
                               Convert.ToString(Session["CustomerPhone"]),
                                Convert.ToString(Session["CustomerName"])
                               );
                            TextBoxCompleteOrderResult.Text = syCompleteOrderResponse.Result;
                            if (TextBoxCompleteOrderResult.Text.Equals("OK"))
                            {
                                Session["PrintStream"] = syCompleteOrderResponse.PrintStream;
                                HyperLinkDownload.Visible = true;
                            }
                            else
                            {
                                HyperLinkDownload.Visible = false;
                            }
                        }
                        else
                        {
                            TextBoxCompleteOrderResult.Text = "No session UserSessionId";
                            HyperLinkDownload.Visible = false;
                        }


                    }
                    else
                    {
                        TextBoxCompleteOrderResult.Text = "Ошибка оплаты";
                        HyperLinkDownload.Visible = false;
                    }

                }

            }
        }
    }
}