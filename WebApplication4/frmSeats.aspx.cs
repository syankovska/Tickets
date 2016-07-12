using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Tickets.SyTicketsSvc;


namespace Tickets
{
    public partial class frmSeats : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["IsBooking"]))
            {
                Session["IsBooking"] = (Request.QueryString["IsBooking"]);
            }


            if (!string.IsNullOrEmpty(Request.QueryString["SessionId"]) && !string.IsNullOrEmpty(Request.QueryString["CinemaId"]))
            {

                Session["SessionId"] = (Request.QueryString["SessionId"]);
                Session["CinemaId"] = (Request.QueryString["CinemaId"]);
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                    var c = sc.GetCinemas().Where(x => x.ID.Equals(Session["CinemaId"])).FirstOrDefault();
                    if (c != null) Session["CinemaName"] = c.Name;

                    var session = sc.GetSessionsByCinema(Convert.ToString(Session["CinemaId"])).Where(x => x.SessionId.Equals(Session["SessionId"])).FirstOrDefault();
                    if (session != null)
                    {
                        string scheduledFilmId = session.ScheduledFilmId;
                        DateTime showTime = session.Showtime;
                        Session["FilmName"] = sc.GetAllFilms().Where(x => x.ScheduledFilmId.Equals(scheduledFilmId)).FirstOrDefault().Title;
                        Session["ShowTime"] = Convert.ToString(showTime);
                    }

                }

            }

            if (Session["CinemaId"] != null && Session["SessionId"] != null)
            {
                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                    List<SyRestTicketType> restTicketType = sc.GetTicketTypes(Session["CinemaId"].ToString(),
                                                                   Session["SessionId"].ToString());

                    SyTicketsSvc.SySeatLayoutData sySeatLayoutData = new SyTicketsSvc.SySeatLayoutData();

                    bool processOrderValue = false;
                    bool userSelectedSeatingSupported = true;
                    bool skipAutoAllocation = true;

                    //if ((Session["TicketTypes"] as List<SyTicketType>) != null
                    //    && (Session["TicketTypes"] as List<SyTicketType>).Count() > 0 && !IsPostBack)
                    //{
                    //    // 'autoallocation seats' case is not used now
                    //    processOrderValue = false;
                    //    userSelectedSeatingSupported = false;
                    //    skipAutoAllocation = false;
                    //}

                    // method should be invoked every page loading to be sure that ordered seats are still accessable  
                    sySeatLayoutData = sc.GetRestAddTickets(Session["CinemaId"].ToString(),
                                                            Session["SessionId"].ToString(),
                                                            (Session["TicketTypes"] as List<SyTicketType>),
                                                            processOrderValue, userSelectedSeatingSupported, skipAutoAllocation);

                    Session["TicketTypes"] = null;

                    if (!IsPostBack)
                    {
                        Session["UserSessionId"] = Convert.ToString(sySeatLayoutData.UserSessionId);

             
                        if (Session["UserSessionId"] != null)
                        {
                            (Master.FindControl("HyperLinkNext") as HyperLink).Visible = true;
                            (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("onclick", "hrefSeatsSubmitClick(HyperLinkNext)");
                            (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("href", "#");
                            if (Session["OrderUserSessionId"] == null)

                            {
                                // initial load without seat allocation

                                (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("class", "linkDisable");

                                Session["TotalValueCents"] = null;
                                Session["TotalOrderCount"] = null;
                                TextBoxTotalValueCents.Text = "0";
                                TextBoxTotalOrderCount.Text = "0";

                            }
                            else
                            {
                                (Master.FindControl("HyperLinkNext") as HyperLink).Attributes.Add("class", "linkEnable");
                                TextBoxTotalValueCents.Text = Convert.ToString(Session["TotalValueCents"]);
                                TextBoxTotalOrderCount.Text = Convert.ToString(Session["TotalOrderCount"]);
                            }
                        }
                        else
                        {
                            Session["Error"] = "Извините,  продажа билетов Онлайн недоступна. Выберите другой сеанс";
                            Server.Transfer("~/frmError.aspx");
                        }

                    }

                    int scaleFactor = 4;
                    double cellWidthScale = 0.5; //should be <1
                    int maxcolcount = 15;
                    int cellwidth = 5;
                    int cellHeight = 4;
                    int leftb = 10;
                    int topb = 53;
                    int rowcount = 11;

                    leftb = sySeatLayoutData.BoundaryLeft;
                    topb = sySeatLayoutData.BoundaryTop;

                    SyArea area = sySeatLayoutData.Areas.OrderByDescending(x => x.ColumnCount).FirstOrDefault();
                    if (area != null)
                    {
                        maxcolcount = area.ColumnCount;
                        cellwidth = area.Width / area.ColumnCount;
                    }
                    area = sySeatLayoutData.Areas.Where(x => x.RowCount == 1).FirstOrDefault();
                    if (area != null) cellHeight = area.Height;

                    var distinctHeights = sySeatLayoutData.Areas
                             .GroupBy(p => new { p.Top, p.Height })
                             .Select(g => g.First())
                             .ToList();


                    if (distinctHeights != null)
                    {
                        rowcount = distinctHeights.Sum(g => g.Height) / cellHeight;
                        foreach (var a in sySeatLayoutData.Areas)
                        {
                            if ((a.Top - topb) % cellHeight > 0)
                            {
                                a.Top = a.Top + cellHeight - (a.Top - topb) % cellHeight;
                                rowcount++;
                            }

                        }
                    }
                    DrawAreas(sySeatLayoutData, DrawGrid(maxcolcount, rowcount + 2, cellwidth, cellHeight, leftb, topb, cellWidthScale, scaleFactor), cellWidthScale, cellwidth, scaleFactor, restTicketType);
                }
                if (!IsPostBack)
                {
                     if (Session["OrderUserSessionId"] != null)
                    {
                        ButtonCancel.Enabled = true;
                    }
                }
            }
            else
            {
                Server.Transfer("~/frmError.aspx");
            }
         }



        protected void DrawAreas(SyTicketsSvc.SySeatLayoutData sySeatLayoutData, List<GridPoint> listPoints, double cellWidthScale, int cellwidth, int scaleFactor, List<SyRestTicketType> restTicketType)
        {

            //        int ibreak = 0;
            foreach (var a in sySeatLayoutData.Areas)
            {
                //              ibreak++;
                //   if (ibreak > 2) break;
                GridPoint pointOfList = listPoints.Where(p => p.X == a.Left && p.Y == a.Top).FirstOrDefault();
                if (pointOfList == null)
                {
                    var pointOfListNearet = listPoints.Where(p => p.X >= a.Left && p.Y == a.Top).OrderBy(p => p.X).FirstOrDefault();
                    if (pointOfListNearet != null)
                    {
                        pointOfList = new GridPoint();
                        pointOfList = pointOfListNearet;

                    }
                }
                if (pointOfList != null)
                {
                    int spanCol  = Convert.ToInt32((a.Width / a.ColumnCount) / (cellwidth * cellWidthScale));
                    for (int i = 0; i < a.RowCount; i++)
                    {

                        int curTableRow = pointOfList.RowId - a.RowCount + 1 + i;
                        if (curTableRow >= 0)
                        {
                            for (int j = 0; j < Table1.Rows[curTableRow].Cells.Count; j++)
                            {
                                if (j == pointOfList.ColumnId)
                                {
                                    for (int jj = j; jj < j + a.ColumnCount; jj++)
                                    {
                                        Table1.Rows[curTableRow].Cells[jj].ColumnSpan = Convert.ToInt32(spanCol);
                                        Table1.Rows[curTableRow].Cells[jj].Attributes.Remove("Style");
                                        //   Table1.Rows[curTableRow].Cells[jj].Attributes.Add("Style", String.
                                        //          Format("width: {0}px",
                                        //                       scaleFactor * cellwidth * cellWidthScale * spanCol));
                                        Table1.Rows[curTableRow].Cells[jj].CssClass = "cell" + a.AreaCategoryCode;
                                    }


                                    for (int jj = 0; jj < a.ColumnCount * spanCol - a.ColumnCount; jj++)
                                    {
                                        if (j + a.ColumnCount < Table1.Rows[curTableRow].Cells.Count)
                                        {
                                            try
                                            {
                                                var remove = Table1.Rows[curTableRow].Cells[j + a.ColumnCount];
                                                Table1.Rows[curTableRow].Cells.Remove(remove);
                                            }
                                            catch (Exception)
                                            {

                                            }

                                        }
                                    }

                                    var listPointsForUpdate = listPoints.Where(p => p.ColumnId > pointOfList.ColumnId + a.ColumnCount * spanCol && p.Y == a.Top).ToList();
                                    foreach (var p in listPointsForUpdate)
                                    { p.ColumnId = p.ColumnId - a.ColumnCount; }

                                    int priceInCents = 0;
                                    string ticketTypeCode = "";
                                    var areaCategoryCode = restTicketType.Where(p => p.AreaCategoryCode.Equals(a.AreaCategoryCode)).FirstOrDefault();
                                    if (areaCategoryCode != null)
                                    {
                                        priceInCents = areaCategoryCode.PriceInCents;
                                        ticketTypeCode = areaCategoryCode.TicketTypeCode;
                                    }



                                    SetSeats(a.Rows[a.RowCount - i - 1], curTableRow, pointOfList.ColumnId, a.Description, a.AreaCategoryCode, priceInCents, ticketTypeCode);

                                }
                            }
                        }
                        
                    }
                    
                }
               
            }



            foreach (TableRow r in Table1.Rows)
            {
                TableRow row = new TableRow();

                int cellsCount = r.Cells.Count;
                for (int j = 0; j < cellsCount; j++)
                {
                    TableCell cell = new TableCell();
                    cell = r.Cells[r.Cells.Count - 1];
                    row.Cells.Add(cell);
                }
                TableTransp.Rows.Add(row);
            }
            Table1.Rows.Clear();
            Table1.Visible = false;

            UpdateCellsID();
        }

        protected List<GridPoint> DrawGrid(int maxcolcount, int rowcount, int cellwidth, int cellheight, int leftb, int topb, double cellWidthScale, int scaleFactor)
        {

            //    Table1.Attributes.Add("Style", String.Format("left: 6px; top: -1px; width: {0}px;height: {1}px;",
            //                                            maxcolcount * scaleFactor * cellwidth, rowcount * scaleFactor * cellheight));


            List<GridPoint> listPoints = new List<GridPoint>();
            for (int i = 0; i < rowcount + 1; i++)
            {
                TableRow row = new TableRow();
                row.Attributes.Add("Style", String.Format("height: {0}px;",
                                                     scaleFactor * cellheight));
                row.ID = i.ToString();
                for (int j = 0; j < maxcolcount / cellWidthScale; j++)
                {
                    TableCell cell = new TableCell();
                    row.Cells.Add(cell);
                    GridPoint gridPoint = new GridPoint();
                    gridPoint.X = leftb + j * cellwidth * cellWidthScale;
                    //    gridPoint.X = leftb + cellwidth * maxcolcount  - j * cellwidth * cellWidthScale;
                    gridPoint.Y = topb - cellheight + rowcount * cellheight - cellheight * i;
                    gridPoint.RowId = i;
                    gridPoint.ColumnId = j;

                    listPoints.Add(gridPoint);
                }
                Table1.Rows.Add(row);
            }

            foreach (TableRow r in Table1.Rows)
            {
                foreach (TableCell c in r.Cells)
                {
                    c.CssClass = "";
                    c.Style.Clear();
                    c.Attributes.Add("Style", String.Format("width: {0}px,height: {1}px;",
                                                 scaleFactor * cellwidth * cellWidthScale, scaleFactor * cellheight));
                    c.ColumnSpan = 0;
                }
            }

            return listPoints;
        }


        protected void SetSeats(SyRow syRow, int curTableRow, int firstCellId, string arDescription, string areaCategoryCode, int price, string ticketTypeCode)
        {

           foreach ( SySeat  s in syRow.Seats)
            {

                TableCell cell = new TableCell();
                cell = Table1.Rows[curTableRow].Cells[firstCellId + s.Position.ColumnIndex];
                Image image = new Image();
                image.ImageUrl = "~/sofa16_blue.png";

                TextBox tb = new TextBox();
                tb.Attributes.Add("type", "hidden");
                tb.Text = "empty";


                if (s.Status == 1) {
                    if (Session["SelectedSeats"] != null) {

                        var ss = (Session["SelectedSeats"] as List<SySelectedSeat>).Where(p =>
                        p.AreaNumber == s.Position.AreaNumber &&
                        p.ColumnIndex == s.Position.ColumnIndex &&
                        p.RowIndex == s.Position.RowIndex).FirstOrDefault();
                        if (ss !=null) { 
                        image.ImageUrl = "~/sofa16_green.png";
                        tb.Text = "reordered";
                        }
                        else image.ImageUrl = "~/sofa16_red.png";
                    }
                    else image.ImageUrl = "~/sofa16_red.png";
                }
                else if (s.Status == 4)
                {
                    image.ImageUrl = "~/sofa16_green.png";
                    tb.Text = "ordered";
                }
                else if (price == 0 || ticketTypeCode.Equals(""))
                {
                    image.ImageUrl = "~/sofa16_yellow.png";
                }

                image.Attributes.Add("Area", arDescription);
                image.Attributes.Add("AreaCategoryCode", areaCategoryCode);
                image.Attributes.Add("AreaNumber", Convert.ToString(s.Position.AreaNumber));
                image.Attributes.Add("ColumnIndex", Convert.ToString(s.Position.ColumnIndex));
                image.Attributes.Add("RowIndex", Convert.ToString(s.Position.RowIndex));
                image.Attributes.Add("Status", Convert.ToString(s.Status));
                image.Attributes.Add("Price", Convert.ToString(price));
                image.Attributes.Add("TicketTypeCode", Convert.ToString(ticketTypeCode));
                image.Attributes.Add("RowNumber", Convert.ToString(syRow.PhysicalName));
                image.Attributes.Add("SeatNumber", Convert.ToString(s.Id));

                cell.Controls.Add(image);
                cell.Controls.Add(tb);
            }

            foreach (TableCell c in Table1.Rows[curTableRow].Cells)
            {
                if (c.Controls == null || c.Controls.Count == 0)
                {
                    c.CssClass = "";
                }
            }

        }

        protected void UpdateCellsID()
        {
            int rNum = 1;
            int sNum = 1;
            for (int i = 0; i < TableTransp.Rows.Count; i++)
            {
                sNum = 1;
                for (int j = 0; j < TableTransp.Rows[i].Cells.Count; j++)
                {
                    if (TableTransp.Rows[i].Cells[TableTransp.Rows[i].Cells.Count - j - 1].CssClass.Contains("cell"))
                    {
                        TableCell c = new TableCell();
                        c = TableTransp.Rows[i].Cells[TableTransp.Rows[i].Cells.Count - j - 1];
                        c.ID = Convert.ToString(rNum) + "_" + Convert.ToString(sNum);
                        if (c.Controls != null && c.Controls.Count != 0)
                        {
                            (c.Controls[0] as Image).ID = "i_" + c.ID;
                            (c.Controls[1] as TextBox).ID = (c.Controls[0] as Image).ID + "_tb";

                            (c.Controls[0] as Image).Attributes.Add("onclick", "imgClick(MainContent_" + (c.Controls[0] as Image).ID + ")");
                            //image.Attributes.Add("onmouseover", "this.style.cursor = 'pointer';");
                            (c.Controls[0] as Image).Attributes.Add("onmouseover", "showhint('" +
                                (c.Controls[0] as Image).ID +
                                " price: " + (c.Controls[0] as Image).Attributes["price"] +
                           //   " TicketTypeCode: " + (c.Controls[0] as Image).Attributes["TicketTypeCode"] +
                                " area: " + (c.Controls[0] as Image).Attributes["area"] +
                          //      " AreaCategoryCode: " + (c.Controls[0] as Image).Attributes["AreaCategoryCode"] +
                          //      " Status: " + (c.Controls[0] as Image).Attributes["Status"] +
                          //      " AreaNumber: " + (c.Controls[0] as Image).Attributes["AreaNumber"] +
                          //      " ColumnIndex: " + (c.Controls[0] as Image).Attributes["ColumnIndex"] +
                          //      " RowIndex: " + (c.Controls[0] as Image).Attributes["RowIndex"] +
                                " RowNumber: " + (c.Controls[0] as Image).Attributes["RowNumber"] +
                                " SeatNumber: " + (c.Controls[0] as Image).Attributes["SeatNumber"] +
                               "', this, event, '150px')");

                        }
                        sNum++;
                    }

                }
                rNum++;
            }
        }



        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
            {
      

                    LabelResponseResult.Text = sc.CancelOrder(Convert.ToString(Session["OrderUserSessionId"]));
            
                if (LabelResponseResult.Text.Equals("OK"))
                {
                    UpdateCanceledSeats();
                    TextBoxTotalValueCents.Text = "0";
                    TextBoxTotalOrderCount.Text = "0";


                    LabelNote.Text = "Please select your seats:";
                    LabelResponseResult.Text = "Canceled " + LabelResponseResult.Text + " " + Convert.ToString(Session["OrderUserSessionId"]);
                    Session["TotalValueCents"] = null;
                    Session["TotalOrderCount"] = null;
                    Session["OrderUserSessionId"] = null;
                }

            }
           
            
     }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            ButtonCancel.Enabled = false;
            if (IsSeatsChanged())
            {
                GetSessionTicketTypes(); // to do check if places have been ordered

                using (SyTicketsSvc.SessionsClient sc = new SessionsClient())
                {
                    if (Session["OrderUserSessionId"] != null)
                        LabelResponseResult.Text = "Canceled " + sc.CancelOrder(Convert.ToString(Session["OrderUserSessionId"]));

                    List<SyRestTicketType> restTicketType = sc.GetTicketTypes(Session["CinemaId"].ToString(),
                                                                              Session["SessionId"].ToString());

                    SyTicketsSvc.SySeatLayoutData sySeatLayoutData = new SyTicketsSvc.SySeatLayoutData();

                    bool processOrderValue = false;
                    bool userSelectedSeatingSupported = true;
                    bool skipAutoAllocation = true;

                    sySeatLayoutData = sc.GetRestAddTickets(Session["CinemaId"].ToString(),
                                                             Session["SessionId"].ToString(),
                                                             (Session["TicketTypes"] as List<SyTicketType>),
                                                             processOrderValue, userSelectedSeatingSupported, skipAutoAllocation);

                    Session["UserSessionId"] = Convert.ToString(sySeatLayoutData.UserSessionId);
                    if (sySeatLayoutData.UserSessionId != null)
                    {

                        TextBoxTotalValueCents.Text = Convert.ToString(sySeatLayoutData.TotalValueCents);
                        TextBoxTotalOrderCount.Text = Convert.ToString(sySeatLayoutData.TotalOrderCount);

                        Session["TotalValueCents"] = Convert.ToString(sySeatLayoutData.TotalValueCents);
                        Session["TotalOrderCount"] = Convert.ToString(sySeatLayoutData.TotalOrderCount);

                        LabelResponseResult.Text = sc.SySetSelectedSeats(Convert.ToString(Session["UserSessionId"]),
                                                                               Session["CinemaId"].ToString(),
                                                                Session["SessionId"].ToString(),
                                                                (Session["SelectedSeats"] as List<SySelectedSeat>));
                        if (!LabelResponseResult.Text.Equals("OK"))
                        {

                            LabelNote.Text = "Please select others seats";
                            ButtonCancel.Enabled = false;
                            LabelResponseResult.Text = "Submit result " + LabelResponseResult.Text;
                            Session["Error"] = "Please select others seats " + sySeatLayoutData.ErrorDescription;
                            Session["SelectedSeats"] = null;
                            Session["OrderedSeats"] = null;
                            Server.Transfer("~/frmError.aspx");

                        }

                        else
                        {
                            ButtonCancel.Enabled = true;
                            Session["OrderUserSessionId"]= Convert.ToString(sySeatLayoutData.UserSessionId);
                            Response.Redirect("~/frmCustomerInfo.aspx");
                        }
           
                
                    }
                    else
                    {
                        LabelNote.Text = sySeatLayoutData.ErrorDescription;
                        TextBoxTotalValueCents.Text = "0";
                        TextBoxTotalOrderCount.Text = "0";

                        Session["TotalValueCents"] = null;
                        Session["TotalOrderCount"] = null;

                        ButtonCancel.Enabled = false;

                        Session["Error"] = "Order error: " + sySeatLayoutData.ErrorDescription;
                        Server.Transfer("~/frmError.aspx");
                    }
                }

            }
            else
            {
                LabelNote.Text = "Nothing to submit. Go to Registration";
                TextBoxTotalValueCents.Text = Convert.ToString(Session["TotalValueCents"]);
                TextBoxTotalOrderCount.Text = Convert.ToString(Session["TotalOrderCount"]);
                ButtonCancel.Enabled = true;
            }

            UpdateOrderedSeats();
            Session["TicketTypes"] = null;
         //   Session["SelectedSeats"] = null;
            Master.FindControl("HyperLinkNext").Visible = true;
        }

        protected void UpdateOrderedSeats()
        {
            foreach (TableRow r in TableTransp.Rows)
            {
                foreach (TableCell c in r.Cells)
                {
                    if (c.Controls.Count > 0 && !(c.Controls[0] as Image).ImageUrl.Contains("red") &&
                                                    ((c.Controls[1] as TextBox).Text.Equals("reordered")
                                                 || (c.Controls[1] as TextBox).Text.Equals("ordered")))
                    {
                        (c.Controls[0] as Image).ImageUrl = "~/sofa16_green.png";
                        (c.Controls[1] as TextBox).Text = "ordered";
                    }

                }
            }
        }
        protected bool IsSeatsChanged()
        {
            bool result = false;

            foreach (TableRow r in TableTransp.Rows)
            {

                foreach (TableCell c in r.Cells)
                {

                    if (c.Controls.Count > 0 && (c.Controls[1] as TextBox).Text.Equals("reordered"))
                    {
                        result = true;
                        return result;
                    }
                }
            }

            return result;
        }

        protected void GetSessionTicketTypes()
        {
            (Session["TicketTypes"] as List<SyTicketType>)?.Clear();
            (Session["SelectedSeats"] as List<SyTicketType>)?.Clear();
            Session["OrderedSeats"] = null;  
            List<SyTicketType> stt = new List<SyTicketType>();
            List<SySelectedSeat> ss = new List<SySelectedSeat>();

            foreach (TableRow r in TableTransp.Rows)
            {
                foreach (TableCell c in r.Cells)
                {
                    if (c.Controls.Count > 0 && ((c.Controls[1] as TextBox).Text.Equals("reordered") ||
                                                 (c.Controls[1] as TextBox).Text.Equals("ordered")))
                    {
                        SyTicketType syTicketType = new SyTicketType();
                        syTicketType.Qty = 1;
                        syTicketType.TicketTypeCode = (c.Controls[0] as Image).Attributes["TicketTypeCode"];
                        syTicketType.PriceInCents = Convert.ToInt32((c.Controls[0] as Image).Attributes["price"]);

                        var tt = stt.Where(p => p.TicketTypeCode.Equals(syTicketType.TicketTypeCode)).ToList();
                        if (tt.Count != 0) tt[0].Qty = ++tt[0].Qty;
                        else stt.Add(syTicketType);

                        SySelectedSeat sySelectedSeat = new SySelectedSeat();
                        sySelectedSeat.AreaCategoryCode = (c.Controls[0] as Image).Attributes["AreaCategoryCode"];
                        sySelectedSeat.AreaNumber = Convert.ToInt32((c.Controls[0] as Image).Attributes["AreaNumber"]);
                        sySelectedSeat.ColumnIndex = Convert.ToInt32((c.Controls[0] as Image).Attributes["ColumnIndex"]);
                        sySelectedSeat.RowIndex = Convert.ToInt32((c.Controls[0] as Image).Attributes["RowIndex"]);
                        Session["OrderedSeats"] = Convert.ToString(Session["OrderedSeats"]) +
                           " Row:" + Convert.ToString((c.Controls[0] as Image).Attributes["RowNumber"]) +
                           "Seat:" + Convert.ToString((c.Controls[0] as Image).Attributes["SeatNumber"]) + ";";
                        ss.Add(sySelectedSeat);

                    }
                }
            }

            Session["TicketTypes"] = stt;
            Session["SelectedSeats"] = ss;
        }
        protected void UpdateCanceledSeats()
        {
            foreach (TableRow r in TableTransp.Rows)
            {
                foreach (TableCell c in r.Cells)
                {

                    if (c.Controls.Count > 0 && ((c.Controls[1] as TextBox).Text.Equals("ordered") ||
                            (c.Controls[1] as TextBox).Text.Equals("reordered")))
                    {
                        (c.Controls[0] as Image).ImageUrl = "~/sofa16_blue.png";
                        (c.Controls[1] as TextBox).Text = "empty";
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ButtonSubmit.Visible = false;
        }
    }
}