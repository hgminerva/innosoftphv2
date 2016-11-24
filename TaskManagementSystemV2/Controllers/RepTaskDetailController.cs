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

        // current user
        public String getCurrentLoginUser()
        {
            String currentUserStaff = "";

            var mstUser = from d in db.mstUsers where d.AspNetUserId == User.Identity.GetUserId() select d;
            if (mstUser.Any())
            {
                currentUserStaff = mstUser.FirstOrDefault().mstStaff.StaffName;
            }

            return currentUserStaff;
        }
        public ActionResult TaskDetailController(Int32 taskId)
        {
            // PDF settings
            MemoryStream workStream = new MemoryStream();
            Rectangle rectangle = new Rectangle(PageSize.A3);
            Document document = new Document(rectangle, 72, 72, 72, 72);
            document.SetMargins(30f, 30f, 30f, 30f);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            // Fonts Customization
            Font fontArial18Bold = FontFactory.GetFont("Arial", 18, Font.BOLD);
            Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font fontArial15Bold = FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.WHITE);
            Font fontArial12Bold = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font fontArial12 = FontFactory.GetFont("Arial", 12);
            var headerColor = new BaseColor(17, 37, 64);

            // line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

            // image
            string imagepath = Server.MapPath("~/images/innosoft.png");
            Image logo = Image.GetInstance(imagepath);
            logo.ScalePercent(14f);
            PdfPCell imageCell = new PdfPCell(logo);

            // header company title w/ logo image
            PdfPTable headerCompanyTitle = new PdfPTable(2);
            float[] headerCompanyTitleWithCells = new float[] { 8f, 92f };
            headerCompanyTitle.SetWidths(headerCompanyTitleWithCells);
            headerCompanyTitle.WidthPercentage = 100;
            headerCompanyTitle.AddCell(new PdfPCell(imageCell) { HorizontalAlignment = 0, Rowspan = 3, Border = 0 });
            headerCompanyTitle.AddCell(new PdfPCell(new Phrase("Cebu Innosoft Solutions Incorporated", fontArial18Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            headerCompanyTitle.AddCell(new PdfPCell(new Phrase("Innosoft Bldg. Corner V. Rama Avenue & R. Duterte St. Guadalupe, Cebu City", fontArial12)) { HorizontalAlignment = 0, Border = 0 });
            headerCompanyTitle.AddCell(new PdfPCell(new Phrase("Contact No: (032) 263 2912 / 520 7245", fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 15f });
            document.Add(headerCompanyTitle);

            // call ticket title
            PdfPTable callTicketTitle = new PdfPTable(1);
            float[] callTicketTitleWithCells = new float[] { 100f };
            callTicketTitle.SetWidths(callTicketTitleWithCells);
            callTicketTitle.WidthPercentage = 100;
            callTicketTitle.AddCell(new PdfPCell(new Phrase("Call Ticket", fontArial15Bold)) { HorizontalAlignment = 1, Border = 1, PaddingTop = 3f, PaddingBottom = 7f, BackgroundColor = headerColor });
            document.Add(callTicketTitle);

            // queries
            var tasks = from d in db.trnTasks where d.Id == taskId select d;
            var taskNo = from t in db.trnTasks where t.TaskNo == tasks.FirstOrDefault().TaskNo select t;
            var staff = from s in db.mstStaffs where s.Id == tasks.FirstOrDefault().StaffId select s;
            var verifiedBy = from v in db.mstStaffs where v.Id == tasks.FirstOrDefault().VerifiedBy select v;
            var action = from a in db.trnTaskSubs where a.TaskId == taskId select a;
            var product = from p in db.mstProducts where p.Id == tasks.FirstOrDefault().ProductId select p;
            var client = from c in db.mstClients where c.Id == tasks.FirstOrDefault().ClientId select c;
            var tasksub = from task in db.trnTaskSubs where task.TaskId == taskId
                          select new Models.MstTaskSub
                          {
                              Id = task.Id,
                              TaskId = task.TaskId,
                              DateCalled = Convert.ToDateTime(task.DateCalled).ToShortDateString(),
                              Action = task.Action,
                              TimeCalled = Convert.ToDateTime(task.TimeCalled).ToShortDateString(),
                              FinishedDate = Convert.ToDateTime(task.FinishedDate).ToShortDateString(),
                              FinishedTime = Convert.ToDateTime(task.FinishedTime).ToShortDateString(),
                              Remarks = task.Remarks
                          };

            // call ticket data
            PdfPTable callTicketData = new PdfPTable(4);
            float[] callTicketDataWithCells = new float[] { 35f, 100f, 40f, 100f };
            callTicketData.SetWidths(callTicketDataWithCells);
            callTicketData.WidthPercentage = 100;
            callTicketData.AddCell(new PdfPCell(new Phrase("Date:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f, PaddingTop = 13f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().TaskDate.ToShortDateString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f, PaddingTop = 13f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Problem/Concern:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingTop = 13f });
            callTicketData.AddCell(new PdfPCell(new Phrase("", fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 15f, PaddingTop = 13f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Task/Ticket No:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().TaskNo.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().Concern.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, Rowspan = 3, Colspan = 2 });
            callTicketData.AddCell(new PdfPCell(new Phrase("Customer:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(client.FirstOrDefault().CompanyName.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Product:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(product.FirstOrDefault().ProductDescription.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Called By:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().Caller.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Problem Type:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().ProblemType.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 3f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Assigned To:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 20f });
            callTicketData.AddCell(new PdfPCell(new Phrase(staff.FirstOrDefault().StaffName.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 20f });
            callTicketData.AddCell(new PdfPCell(new Phrase("Severity:", fontArial12Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 20f });
            callTicketData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().Severity.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 20f });
            document.Add(callTicketData);

            document.Add(line);

            // action title
            PdfPTable actionTitle = new PdfPTable(1);
            float[] actionTitleWithCells = new float[] { 100f };
            actionTitle.SetWidths(actionTitleWithCells);
            actionTitle.WidthPercentage = 100;
            actionTitle.AddCell(new PdfPCell(new Phrase("Action", fontArial15Bold)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 3f, PaddingBottom = 7f, BackgroundColor = headerColor });
            document.Add(actionTitle);

            document.Add(line);

            // action ticket data
            PdfPTable actionData = new PdfPTable(2);
            float[] actionDataWithCells = new float[] { 15f, 85f };
            actionData.SetWidths(actionDataWithCells);
            actionData.WidthPercentage = 100;
            actionData.AddCell(new PdfPCell(new Phrase("Date", fontArial12Bold)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            actionData.AddCell(new PdfPCell(new Phrase("Particulars / Actions", fontArial12Bold)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

            if (tasksub.Any())
            {
                foreach (var taskSub in tasksub)
                {
                    actionData.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(taskSub.DateCalled).ToShortDateString(), fontArial12)) { HorizontalAlignment = 0, PaddingBottom = 3f, PaddingLeft = 5f });
                    actionData.AddCell(new PdfPCell(new Phrase(taskSub.Action, fontArial12)) { HorizontalAlignment = 0, PaddingBottom = 5f, PaddingLeft = 5f });
                }
            }

            document.Add(actionData);

            // remarks
            PdfPTable remarksData = new PdfPTable(1);
            float[] remarksDataWithCells = new float[] { 100f };
            remarksData.SetWidths(remarksDataWithCells);
            remarksData.WidthPercentage = 100;
            remarksData.AddCell(new PdfPCell(new Phrase("Remarks", fontArial12Bold)) { HorizontalAlignment = 0, PaddingTop = 15f, PaddingBottom = 5f, Border = 0 });
            remarksData.AddCell(new PdfPCell(new Phrase(tasks.FirstOrDefault().Remarks.ToString(), fontArial12)) { HorizontalAlignment = 0, Border = 0 });
            document.Add(remarksData);

            document.Add(Chunk.NEWLINE);

            // Table for Footer
            PdfPTable tableFooter = new PdfPTable(4);
            tableFooter.WidthPercentage = 100;
            float[] widthsCells2 = new float[] { 25f, 5f, 25f, 45f };
            tableFooter.SetWidths(widthsCells2);
            tableFooter.AddCell(new PdfPCell(new Phrase("Prepared by:", fontArial12Bold)) { Border = 0, HorizontalAlignment = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase("Verified by:", fontArial12Bold)) { Border = 0, HorizontalAlignment = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase(getCurrentLoginUser().ToUpper(), fontArial12)) { Border = 0, PaddingTop = 40f, PaddingBottom = 10f, HorizontalAlignment = 1 });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 40f, PaddingBottom = 10f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 40f, PaddingBottom = 10f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase("Signature Over Printed Name")) { Border = 1, HorizontalAlignment = 1, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase("Signature Over Printed Name")) { Border = 1, HorizontalAlignment = 1, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            document.Add(tableFooter);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
    }
}