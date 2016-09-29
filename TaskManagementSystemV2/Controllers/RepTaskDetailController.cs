using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace TaskManagementSystemV2.Controllers
{
    public class RepTaskDetailController : Controller
    {
        // Easyfis data context
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        public ActionResult TaskDetailController(Int32 taskId)
        {
            // PDF settings
            MemoryStream workStream = new MemoryStream();
            Rectangle rectangle = new Rectangle(PageSize.A3);
            Document document = new Document(rectangle, 72, 72, 72, 72);
            document.SetMargins(30f, 30f, 30f, 30f);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            string imagepath = Server.MapPath("images");

            Image logo = Image.GetInstance(imagepath + "/callTicketHeader.png");

            logo.ScalePercent(70f);

            // Document Starts
            document.Open();

            // Fonts Customization
            Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font fontArial12Bold = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font fontArial12 = FontFactory.GetFont("Arial", 12);
            Font fontArial9 = FontFactory.GetFont("Arial", 9);
            Font fontArial10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);

            // line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

            //Header
            Paragraph header = new Paragraph("Innosoft Solution Inc.");
            header.Alignment = Element.ALIGN_JUSTIFIED;

            var tasks = from d in db.trnTasks
                        where d.Id == taskId
                        select d;

            var taskNo = from t in db.trnTasks
                         where t.TaskNo == tasks.FirstOrDefault().TaskNo
                         select t;

            var staff = from s in db.mstStaffs
                        where s.Id == tasks.FirstOrDefault().StaffId
                        select s;

            var verifiedBy = from v in db.mstStaffs
                             where v.Id == tasks.FirstOrDefault().VerifiedBy
                             select v;

            var action = from a in db.trnTaskSubs
                         where a.TaskId == taskId
                         select a;

            var product = from p in db.mstProducts
                          where p.Id == tasks.FirstOrDefault().ProductId
                          select p;

            var client = from c in db.mstClients
                         where c.Id == tasks.FirstOrDefault().ClientId
                         select c;
           

            var tasksub = from task in db.trnTaskSubs
                          where task.TaskId == taskId

                          select new Models.MstTaskSub
                          {
                              Id = task.Id,
                              TaskId = task.TaskId,
                              DateCalled = task.DateCalled,
                              Action = task.Action,
                              TimeCalled = task.TimeCalled,
                              FinishedDate = task.FinishedDate,
                              FinishedTime = task.FinishedTime,
                              Remarks = task.Remarks
                          };

            //tableHeaderPage.AddCell(new PdfPCell(new Phrase("Printed " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt"), fontArial11)) { Border = 0, PaddingTop = 5f, HorizontalAlignment = 2 });
            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] widthsCellsheaderPage2 = new float[] { 35f, 100f, 40f, 100f };

            table.SetWidths(widthsCellsheaderPage2);
            table.WidthPercentage = 100;
            PdfPCell cellTicket = new PdfPCell(new Phrase("Call Ticket", fontArial17Bold)) { HorizontalAlignment = 2 };
            PdfPCell blank = new PdfPCell(new Phrase(" ")) { HorizontalAlignment = 2 };
            cellTicket.Colspan = 4;
            cellTicket.BorderColor = BaseColor.WHITE;
            cellTicket.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            blank.Colspan = 4;
            blank.BorderColor = BaseColor.WHITE;
            blank.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            table.AddCell(cellTicket);
            table.AddCell(blank);
            table.AddCell(new Phrase("Date:", fontArial12Bold));
            table.AddCell(tasks.FirstOrDefault().TaskDate.ToShortDateString());
            table.AddCell(new Phrase("Problem/Concern:", fontArial12Bold));
            table.AddCell(" ");



            table.AddCell(new Phrase("Task/Ticket No.:", fontArial12Bold));
            table.AddCell(tasks.FirstOrDefault().TaskNo.ToString());

            PdfPCell problemRowCell = new PdfPCell(new Phrase(tasks.FirstOrDefault().Concern.ToString()));
            problemRowCell.Rowspan = 3;
            problemRowCell.Colspan = 2;
            problemRowCell.BorderColor = BaseColor.WHITE;
            table.AddCell(problemRowCell);


            table.AddCell(new Phrase("Customer:", fontArial12Bold));
            table.AddCell(client.FirstOrDefault().CompanyName.ToString());

            table.AddCell(new Phrase("Product:", fontArial12Bold));
            table.AddCell(product.FirstOrDefault().ProductDescription.ToString());

            table.AddCell(new Phrase("Called By:", fontArial12Bold));
            table.AddCell(tasks.FirstOrDefault().Caller.ToString());

            table.AddCell(new Phrase("Problem Type:", fontArial12Bold));
            table.AddCell(tasks.FirstOrDefault().ProblemType.ToString());

            table.AddCell(new Phrase("Assigned To:", fontArial12Bold));
            table.AddCell(staff.FirstOrDefault().StaffName.ToString());

            table.AddCell(new Phrase("Severity:", fontArial12Bold));
            table.AddCell(tasks.FirstOrDefault().Severity.ToString());
           

            PdfPTable actionTable = new PdfPTable(2);
            float[] widthsCellsheaderPage3 = new float[] { 40f, 300f };
            actionTable.SetWidths(widthsCellsheaderPage3);
            //actionTable.DefaultCell.Border = Rectangle.NO_BORDER;
            actionTable.WidthPercentage = 100;

            PdfPCell actionCell = new PdfPCell(new Phrase("Action", fontArial17Bold)) { HorizontalAlignment = 2 };
            actionCell.Colspan = 2;
            //actionCell.BorderColor = BaseColor.WHITE;
            actionCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            actionTable.AddCell(actionCell);


            PdfPCell actionCellDate= new PdfPCell(new Phrase("Date", fontArial12Bold)) { HorizontalAlignment = 2 };
            //actionCell.Colspan = 1;
            //actionCell.BorderColor = BaseColor.WHITE;
            actionCellDate.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            actionTable.AddCell(actionCellDate);

            PdfPCell actionCellAction = new PdfPCell(new Phrase("Particulars/Action", fontArial12Bold)) { HorizontalAlignment = 2 };
            //actionCell.Colspan = 1;
            //actionCell.BorderColor = BaseColor.WHITE;
            actionCellAction.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            actionTable.AddCell(actionCellAction);
            
            if (tasksub.Any()) {
                foreach (var taskSub in tasksub)
                            {
                    string ver = taskSub.DateCalled.ToString();
                                string[] v = ver.Split(' ');

                                PdfPCell date = new PdfPCell(new Phrase(v[0])) { HorizontalAlignment = 2 };
                                //actionCell.Colspan = 1;
                                actionCell.BorderColor = BaseColor.WHITE;
                                date.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                actionTable.AddCell(date);

                                //actionTable.AddCell() { HorizontalAlignment = 0 };
                                actionTable.AddCell(taskSub.Action);
                }
            }

            PdfPCell space = new PdfPCell(new Phrase(" "));
            space.Colspan = 2;
            space.BorderColor = BaseColor.WHITE;
            space.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            actionTable.AddCell(space);
            
           
            //Remarks
            PdfPTable remarksTable = new PdfPTable(4);
            float[] widthsCellsheaderPage4 = new float[] { 25f, 100f, 25f, 100f };
            remarksTable.SetWidths(widthsCellsheaderPage4);
            //actionTable.DefaultCell.Border = Rectangle.NO_BORDER;
            remarksTable.WidthPercentage = 100;

            PdfPCell rLabel = new PdfPCell(new Phrase("Remarks:" , fontArial12Bold));
            rLabel.Colspan = 4;
            rLabel.BorderColor = BaseColor.WHITE;
            rLabel.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(rLabel);

            
            PdfPCell remarks = new PdfPCell(new Phrase(tasks.FirstOrDefault().Remarks.ToString())) { HorizontalAlignment = 0 };
            remarks.Colspan = 4;
            remarks.BorderColor = BaseColor.WHITE;
            remarks.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(remarks);

            PdfPCell remarksSpace = new PdfPCell(new Phrase(" ")) { HorizontalAlignment = 0 };
            remarksSpace.Colspan = 4;
            remarksSpace.BorderColor = BaseColor.WHITE;
            remarksSpace.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(remarksSpace);

            PdfPCell preparedBy = new PdfPCell(new Phrase("Prepared By: _________________________________________", fontArial12Bold)) { HorizontalAlignment = 0 };
            preparedBy.Colspan = 2;
            preparedBy.BorderColor = BaseColor.WHITE;
            preparedBy.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(preparedBy);

            PdfPCell verifiedL = new PdfPCell(new Phrase("Verified By:", fontArial12Bold)) { HorizontalAlignment = 0 };
            verifiedL.Colspan = 1;
            verifiedL.BorderColor = BaseColor.WHITE;
            verifiedL.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(verifiedL);
            
            PdfPCell verified = new PdfPCell(new Phrase(verifiedBy.FirstOrDefault().StaffName)) { HorizontalAlignment = 0 };
            verified.Colspan = 1;
            verified.BorderColor = BaseColor.WHITE;
            verified.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            remarksTable.AddCell(verified);

            document.Add(logo);
            //document.Add(header);
            document.Add(line);
            //document.Add(Chunk.NEWLINE);
            document.Add(table);
            document.Add(Chunk.NEWLINE);
            document.Add(actionTable);
            document.Add(remarksTable);


            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }


    }

    
}