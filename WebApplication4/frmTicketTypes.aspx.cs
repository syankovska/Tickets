using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;

namespace Tickets
{
    public partial class frmTicketTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SyTicketsSvc.SessionsClient sc = new SessionsClient();

            Session["TicketTypes"] = null;

            List <SyRestTicketType> restTicketType = sc.GetTicketTypes(Session["CinemaId"].ToString(),
                Session["SessionId"].ToString());
 

            for (int i = 0; i < restTicketType.Count; i++)
            {
                TableRow row = new TableRow();
                //0
                TableCell cell = new TableCell();
                cell.Text = restTicketType[i].CinemaId;
                row.Cells.Add(cell);
                //1
                cell = new TableCell();
                cell.Text = restTicketType[i].AreaCategoryCode;
                row.Cells.Add(cell);
                //2
                cell = new TableCell();
                cell.Text = restTicketType[i].Description;
                row.Cells.Add(cell);
                //3
                cell = new TableCell();
                cell.Text = restTicketType[i].TicketTypeCode;
                row.Cells.Add(cell);
                //4
                cell = new TableCell();
                cell.Text = (restTicketType[i].QuantityAvailablePerOrder).ToString();
                row.Cells.Add(cell);
                //5
                cell = new TableCell();
                cell.Text = (restTicketType[i].PriceInCents).ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = (restTicketType[i].PriceInCents).ToString();
                TextBox tb = new TextBox();
                tb.ID = "tb_" + restTicketType[i].TicketTypeCode;
                tb.Text = "0";
                cell.Controls.Add(tb);
                row.Cells.Add(cell);

                Table1.Rows.Add(row);
            }

        }

  
        protected void Button1_Click(object sender, EventArgs e)
        {
            // if (Session["TicketTypes"] != null) 
            //  (Session["TicketTypes"] as List<SyTicketType>).Clear();
            // else (Session["TicketTypes"] as List<SyTicketType>).

            (Session["TicketTypes"] as List<SyTicketType>)?.Clear();

            List <SyTicketType>  stt = new List<SyTicketType>();

            foreach (TableRow tr in Table1.Rows) {

                    if (!(tr.Cells[6].Controls[0] as TextBox).Text.Equals("0"))
                    {
                    SyTicketType syTicketType = new SyTicketType();
                        syTicketType.Qty = Convert.ToInt32((tr.Cells[6].Controls[0] as TextBox).Text);
                        syTicketType.TicketTypeCode = tr.Cells[3].Text;
                        syTicketType.PriceInCents = Convert.ToInt32(tr.Cells[5].Text);
                        stt.Add(syTicketType);
                    }
          }

            Session["TicketTypes"]  = stt;
       }
    }
}