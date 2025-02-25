using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models.ViewModel;
using HRMS.Models.Database;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel.Shapes;
using System.Globalization;

namespace HRMS.Controllers
{
    public class SalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult SalaryMaster(SalaryMasterViewModel model)
        {
            UtilityController u = new UtilityController();
            model.empiddesc = u.getempidDesc();
            model.effect_date = u.currentDateTime().ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult GetCalculatedSalary(SalaryMasterViewModel model)
        {
            //  model.tot_earn = (Convert.ToDecimal(model.basic) + Convert.ToDecimal(model.hra) + Convert.ToDecimal(model.other_allw)).ToString("0.00");
            if (Convert.ToDecimal(model.tot_earn) <= 15000)
            {
                model.emp_ptax = "110.00";
            }
            else if (Convert.ToDecimal(model.tot_earn) >= 15001 && Convert.ToDecimal(model.tot_earn) <= 25000)
            {
                model.emp_ptax = "130.00";
            }
            else
            {
                model.emp_ptax = "150.00";
            }
            model.basic = Math.Round((Convert.ToDecimal(model.tot_earn) * 50 / 100)).ToString("0.00");
            model.hra = Math.Round((Convert.ToDecimal(model.basic) * 15 / 100)).ToString("0.00");
            model.other_allw = Math.Round(Convert.ToDecimal(model.tot_earn) - Convert.ToDecimal(model.basic) - Convert.ToDecimal(model.hra)).ToString("0.00");
            model.emp_pf = Math.Round(Convert.ToDecimal((Convert.ToDecimal(model.basic) * 12) / 100)).ToString("0.00");
            model.emp_esic = Math.Round(Convert.ToDecimal((Convert.ToDecimal(model.tot_earn) * Convert.ToDecimal(0.75)) / 100)).ToString("0.00");
            model.comp_pf = Math.Round(Convert.ToDecimal((Convert.ToDecimal(model.basic) * 13) / 100) + Convert.ToDecimal(0.1)).ToString("0.00");
            model.comp_esic = Math.Round(Convert.ToDecimal((Convert.ToDecimal(model.tot_earn) * Convert.ToDecimal(3.25)) / 100)).ToString("0.00");
            model.tot_emp_ded = Math.Round(Convert.ToDecimal(model.emp_pf) + Convert.ToDecimal(model.emp_esic) + Convert.ToDecimal(model.emp_ptax)).ToString("0.00");
            model.tot_comp_con = Math.Round(Convert.ToDecimal(model.comp_pf) + Convert.ToDecimal(model.comp_esic)).ToString("0.00");
            model.tot_ctc = (Convert.ToDecimal(model.tot_earn) + Convert.ToDecimal(model.tot_comp_con)).ToString("0.00");
            model.emp_net = (Convert.ToDecimal(model.tot_earn) - Convert.ToDecimal(model.tot_emp_ded)).ToString("0.00");

            return Json(model);
        }
        public JsonResult SavedSalary(SalaryMasterViewModel model)
        {
            salary_master sm = new salary_master();
            string msg = sm.SavedSalarysByemployee_id(model);
            return Json(msg);
        }

        public JsonResult getSalaryList()
        {
            string data = "";
            salary_master sm = new salary_master();
            List<salary_master> smlist = new List<salary_master>();
            smlist = sm.GetEmployyeSalaryList();
            if (smlist.Count > 0)
            {
                data = "<tr><th>Name</th><th>Emp Id</th><th>Basic</th><th>HRA</th><th>Other Allowance</th><th>Total Earning</th>" +
                "<th>PTax</th><th>PF</th><th>ESIC</th><th>Total Deduction</th><th>NET PAY</th><th>Comp.PF</th><th>Com.ESIC</th><th>Comp.Total</th><th>Total CTC</th><th>Effect Date</th></tr>";
                foreach (var a in smlist)
                {
                    data += "<tr><td>" + a.name + "</td><td>" + a.employee_id + "</td><td>" + a.emp_basic + "</td><td>" + a.emp_hra + "</td><td>" + a.emp_Other_Allowance + "</td><td>" + a.emp_Total_Earning + "</td><td>" + a.emp_PTAX + "</td><td>" + a.emp_PF + "</td><td>" + a.emp_esic + "</td><td>" + a.tot_emp_ded + "</td><td>" + a.emp_net + "</td><td>" + a.comp_pf + "</td><td>" + a.comp_esic + "</td><td>" + a.tot_comp_con + "</td><td>" + a.tot_ctc + "</td><td>" + a.effect_date + "</td></tr>";
                }
            }
            return Json(data);
        }


        [HttpGet]
        public IActionResult SalaryStructure(SalaryMasterViewModel model)
        {
            UtilityController u = new UtilityController();
            model.financal_yeardesc = u.getfinYear(User.Identity.Name);
            return View(model);
        }

        public JsonResult getSalaryDetailsByEmpployee_id(string effect_date)
        {
            string data = "";
            salary_master sm = new salary_master();
            List<salary_master> smlist = new List<salary_master>();
            sm = sm.GetEmployyeSalaryByid(User.Identity.Name, effect_date);

            data = "<tr class=\"table-warning\"><th colspan=\"2\">Earning</th><th colspan=\"2\">Deductions</th></tr>";
            data += "<tr><td>Basic Salary</td><td>" + sm.emp_basic.ToString("0.00") + "</td><td class=\"table-danger\">Provident Fund (12% on Basic)</td><td class=\"table-danger\">" + sm.emp_PF.ToString("0.00") + "</td></tr>";
            data += "<tr><td>House Rent Allowance</td><td>" + sm.emp_hra.ToString("0.00") + "</td><td class=\"table-danger\">ESIC (0.75% on Gross)</td><td class=\"table-danger\">" + sm.emp_esic.ToString("0.00") + "</td></tr>";
            data += "<tr><td>Other Allowence</td><td>" + sm.emp_Other_Allowance.ToString("0.00") + "</td><td class=\"table-danger\">Professional Tax</td><td class=\"table-danger\">" + sm.emp_PTAX.ToString("0.00") + "</td></tr>";
            data += "<tr><th>Gross Salary</th><th>" + sm.emp_Total_Earning.ToString("0.00") + "</th><th class=\"table-danger\">Total Deduction</th><th class=\"table-danger\">" + sm.tot_emp_ded.ToString("0.00") + "</th></tr>";
            data += "<tr><th colspan=\"2\"></th><th class=\"table-success\">Take Home</th><th  class=\"table-success\">" + sm.emp_net.ToString("0.00") + "</th></tr>";

            data += "<tr class=\"table-warning\"><th colspan=\"4\">Employer's Contribution</th></tr>";
            data += "<tr><td colspan=\"2\">Provident Fund (13% on Basic)</td><td colspan=\"2\">" + sm.comp_pf.ToString("0.00") + "</td></tr>";
            data += "<tr><td colspan=\"2\">ESIC (3.25% on Gross)</td><td colspan=\"2\">" + sm.comp_esic.ToString("0.00") + "</td></tr>";
            data += "<tr><th colspan=\"2\">Total Contribtion</th><th colspan=\"2\">" + sm.tot_comp_con.ToString("0.00") + "</th></tr>";
            data += "<tr class=\"table-success\"><th colspan=\"2\">CTC(Monthly)</th><th colspan=\"2\">" + sm.tot_ctc.ToString("0.00") + "</th></tr>";
            data += "<tr class=\"table-success\"><th colspan=\"2\">CTC(Yearly)</th><th colspan=\"2\">" + (sm.tot_ctc * 12).ToString("0.00") + "</th></tr>";



            return Json(data);
        }

        [HttpGet]
        public IActionResult employeePayslip(SalaryMasterViewModel model)
        {
            UtilityController u = new UtilityController();
            model.financal_yeardesc = u.getYearForPayslip(User.Identity.Name);
            return View(model);
        }
        public JsonResult checkPayslip(string year, string month)
        {

            Salary_Ledger sl = new Salary_Ledger();
            sl = sl.GetSalaryLedgerForPayslip(User.Identity.Name, year, month);
            return Json(sl.msg);
        }
        public IActionResult GetPayslipPdf(string year, string month)
        {
           

            var document = GetDocument(year, month, User.Identity.Name);
            MemoryStream stream = new MemoryStream();
            document.Save(stream);


            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-length", stream.Length.ToString());
            byte[] bytes = stream.ToArray();
            stream.Close();
            return File(bytes, "application/pdf", "Payslip_" + User.Identity.Name + "_"+month+"_"+year+".pdf");
        }
        public PdfDocument GetDocument(string year, string month, string empid)
        {
            var document = new Document();
            BuildDocument(document, year, month, empid);
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            //return pdfRenderer.PdfDocument;
            return pdfRenderer.PdfDocument;
        }

        public void BuildDocument(Document document, string year, string month, string empid)
        {

            Salary_Ledger sl = new Salary_Ledger();
            sl = sl.GetSalaryLedgerForPayslip(empid, year, month);
            Section section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);

            //Image image = section.Headers.Primary.AddImage("D:\\CLW ALL Projects & Documentation\\clw final - Design\\Fin-Pro-v1\\wwwroot\\Images\\clw_logo1.jpeg");
            Image image = section.Headers.Primary.AddImage("wwwroot\\img\\logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.WrapFormat.Style = WrapStyle.Through;
            //Create Header
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 11;
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("Rishi Technovision PVT.LTD.");
            paragraph.AddLineBreak();
            paragraph.AddText("BF-88 SALTLAKE SECTOR-I, BIDHANNAGAR, WEST BENGAL      Phone: 0341 - 2526440");

            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("PAY SLIP FOR MONTH " + month + "-" + year);
            paragraph.AddLineBreak();

            paragraph.AddLineBreak();

            paragraph.AddLineBreak();
            paragraph.Format.SpaceAfter = 20;
            // Create footer          
            Paragraph paragraph1 = section.Footers.Primary.AddParagraph();
            paragraph1.AddText("** This is a computer generated PaySlip and does not require any signature **");
            paragraph1.Format.Font.Size = 9;
            paragraph1.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address          
            //var addressFrame = section.AddTextFrame();
            //addressFrame.Height = "3.0cm";
            //addressFrame.Width = "7.0cm";
            //addressFrame.Left = ShapePosition.Center;
            //addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            //addressFrame.Top = "5.0cm";
            //addressFrame.RelativeVertical = RelativeVertical.Page;


            var table = section.AddTable();

            table.Style = "Table";
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.Alignment = RowAlignment.Center;
            //table.Borders.Color  ;
            //table.Borders.Width = 0.25;
            // table.Borders.Left.Width = 0.5;
            //table.Borders.Right.Width = 0.5;
            // table.Rows.LeftIndent = 0;
            //table.Borders.Width = 0.5;
            Column column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            Row row = table.AddRow();
            row.Format.Font.Name = "Times New Roman";
            //row.Format.Font.Bold = true;
            row.Height = Unit.FromCentimeter(.6);

            //row.Format.Font.Size = 10;

            row.Cells[0].AddParagraph("NAME");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Font.Name = "Arial";
            row.Cells[1].AddParagraph(sl.employee_name.ToUpper());
            row.Cells[2].AddParagraph("DATE OF BIRTH");
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Name = "Arial";
            row.Cells[3].AddParagraph(sl.date_of_birth);
            row.Cells[4].AddParagraph("PAYMENT MODE");
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Name = "Arial";
            row.Cells[5].AddParagraph("NEFT");
            Row row1 = table.AddRow();
            // row1.Format.Font.Bold = true;
            row1.Height = Unit.FromCentimeter(.6);
            row1.Format.Font.Name = "Times New Roman";
            row1.Cells[0].AddParagraph("EMPLOYEE ID");
            row1.Cells[0].Format.Font.Bold = true;
            row1.Cells[0].Format.Font.Name = "Arial";
            row1.Cells[1].AddParagraph(User.Identity.Name);
            row1.Cells[2].AddParagraph("DATE OF JOINING");
            row1.Cells[2].Format.Font.Bold = true;
            row1.Cells[2].Format.Font.Name = "Arial";
            row1.Cells[3].AddParagraph(sl.joining_date);
            row1.Cells[4].AddParagraph("BANK");
            row1.Cells[4].Format.Font.Bold = true;
            row1.Cells[4].Format.Font.Name = "Arial";
            row1.Cells[5].AddParagraph(sl.bank_name);
            Row row2 = table.AddRow();
            //  row2.Format.Font.Bold = true;
            row2.Height = Unit.FromCentimeter(.6);
            row2.Format.Font.Name = "Times New Roman";
            row2.Cells[0].AddParagraph("DEPARTMENT");
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].Format.Font.Name = "Arial";
            row2.Cells[1].AddParagraph(sl.department);
            row2.Cells[2].AddParagraph("MOBILE NO.");
            row2.Cells[2].Format.Font.Bold = true;
            row2.Cells[2].Format.Font.Name = "Arial";
            sl.contact_no_1 = sl.contact_no_1.Substring(0, 2) + "*****" + sl.contact_no_1.Substring(7, 3);
            row2.Cells[3].AddParagraph(sl.contact_no_1);
            row2.Cells[4].AddParagraph("IFSC");
            row2.Cells[4].Format.Font.Bold = true;
            row2.Cells[4].Format.Font.Name = "Arial";
            row2.Cells[5].AddParagraph(sl.ifsc_code);
            Row row3 = table.AddRow();
            //   row3.Format.Font.Bold = true;
            row3.Height = Unit.FromCentimeter(.6);
            row3.Format.Font.Name = "Times New Roman";
            row3.Cells[0].AddParagraph("DESIGNATION ");
            row3.Cells[0].Format.Font.Bold = true;
            row3.Cells[0].Format.Font.Name = "Arial";
            row3.Cells[1].AddParagraph(sl.Designation_name);
            row3.Cells[2].AddParagraph("BASIC PAY");
            row3.Cells[2].Format.Font.Bold = true;
            row3.Cells[2].Format.Font.Name = "Arial";
            row3.Cells[3].AddParagraph(sl.emp_basic.ToString("0.00"));
            row3.Cells[4].AddParagraph("AC NO.");
            row3.Cells[4].Format.Font.Bold = true;
            row3.Cells[4].Format.Font.Name = "Arial";
            sl.ac_no = "******"+sl.ac_no.Substring(sl.ac_no.Length - 4); ;
            row3.Cells[5].AddParagraph(sl.ac_no);
            Row row4 = table.AddRow();
            // row4.Format.Font.Bold = true;
            row4.Height = Unit.FromCentimeter(.6);
            row4.Format.Font.Name = "Times New Roman";
            row4.Cells[0].AddParagraph("AADHAR");
            row4.Cells[0].Format.Font.Bold = true;
            row4.Cells[0].Format.Font.Name = "Arial";
            row4.Cells[1].AddParagraph(sl.aadhar_no);
            row4.Cells[2].AddParagraph("LOP DAYS");
            row4.Cells[2].Format.Font.Bold = true;
            row4.Cells[2].Format.Font.Name = "Arial";
            row4.Cells[3].AddParagraph(sl.lop.ToString());
            row4.Cells[4].AddParagraph("PAN");
            row4.Cells[4].Format.Font.Bold = true;
            row4.Cells[4].Format.Font.Name = "Arial";
            row4.Cells[5].AddParagraph(sl.pan_no);
            Row row5 = table.AddRow();
            //  row5.Format.Font.Bold = true;
            row5.Height = Unit.FromCentimeter(.6);
            row5.Format.Font.Name = "Times New Roman";
            row5.Cells[0].AddParagraph("TOTAL DAYS");
            row5.Cells[0].Format.Font.Bold = true;
            row5.Cells[0].Format.Font.Name = "Arial";
            row5.Cells[1].AddParagraph(Convert.ToString(DateTime.DaysInMonth(Convert.ToInt32(year), DateTime.ParseExact(month, "MMMM", new CultureInfo("en-GB")).Month)));
            row5.Cells[2].AddParagraph("LOP AMOUNT");
            row5.Cells[2].Format.Font.Bold = true;
            row5.Cells[2].Format.Font.Name = "Arial";
            row5.Cells[3].AddParagraph((sl.actual_net_pay-sl.calculated_net_pay).ToString("0.00"));
            row5.Cells[4].AddParagraph("PF AC.No.(UAN)");
            row5.Cells[4].Format.Font.Bold = true;
            row5.Cells[4].Format.Font.Name = "Arial";
            row5.Cells[5].AddParagraph(sl.pf_no);
            Row row6 = table.AddRow();
            //   row6.Format.Font.Bold = true;
            row6.Height = Unit.FromCentimeter(.6);
            row6.Format.Font.Name = "Times New Roman";
            //row6.Cells[0].AddParagraph("GROSS PAY");
            //row6.Cells[0].Format.Font.Bold = true;
            //row6.Cells[0].Format.Font.Name = "Arial";
            //row6.Cells[1].AddParagraph(sl.emp_Total_Earning.ToString("0.00"));
            //row6.Cells[2].AddParagraph("NET PAY");
            //row6.Cells[2].Format.Font.Bold = true;
            //row6.Cells[2].Format.Font.Name = "Arial";
            //row6.Cells[3].AddParagraph(sl.calculated_net_pay.ToString("0.00"));
            row6.Cells[4].AddParagraph("ESIC");
            row6.Cells[4].Format.Font.Bold = true;
            row6.Cells[4].Format.Font.Name = "Arial";
            row6.Cells[5].AddParagraph(sl.esic_no);

            var paragraph2 = section.AddParagraph();
            paragraph2.Format.Alignment = ParagraphAlignment.Center;
            paragraph2.Format.Font.Bold = true;
            paragraph2.Format.Font.Size = 11;
            paragraph2.AddLineBreak();
            paragraph2.AddLineBreak();
            paragraph2.AddLineBreak();
            var table2 = section.AddTable();

            table2.Style = "Table";
            table2.Format.Alignment = ParagraphAlignment.Center;
            table2.Rows.Alignment = RowAlignment.Center;
            table2.Borders.Visible = true;
            Column column2 = table2.AddColumn("4cm");
            column2.Format.Alignment = ParagraphAlignment.Left;
            column2 = table2.AddColumn("5cm");
            column2.Format.Alignment = ParagraphAlignment.Right;
            column2 = table2.AddColumn("4cm");
            column2.Format.Alignment = ParagraphAlignment.Left;
            column2 = table2.AddColumn("4cm");
            column2.Format.Alignment = ParagraphAlignment.Right;
            column2 = table2.AddColumn("5cm");
            column2.Format.Alignment = ParagraphAlignment.Left;
            column2 = table2.AddColumn("4cm");
            column2.Format.Alignment = ParagraphAlignment.Right;


            Row t2row = table2.AddRow();
            t2row.Format.Font.Name = "Times New Roman";
            t2row.Format.Font.Bold = true;
            t2row.Format.Font.Name = "Arial";
            t2row.Height = Unit.FromCentimeter(.6);
            t2row.Cells[0].AddParagraph("Earnings");
            t2row.Cells[1].AddParagraph("Rs.");
            t2row.Cells[2].AddParagraph("Deductions");
            t2row.Cells[3].AddParagraph("Rs.");
            t2row.Cells[4].AddParagraph("Employer's Contribution"); 
            t2row.Cells[5].AddParagraph("Rs.");

            Row t2row2 = table2.AddRow();
            //  row2.Format.Font.Bold = true;
            t2row2.Height = Unit.FromCentimeter(.6);
            t2row2.Format.Font.Name = "Times New Roman";
            t2row2.Cells[0].AddParagraph("BASIC PAY");
            t2row2.Cells[0].Format.Font.Bold = true;
            t2row2.Cells[0].Format.Font.Name = "Arial";
            t2row2.Cells[1].AddParagraph(sl.emp_basic.ToString("0.00"));
            t2row2.Cells[2].AddParagraph("Provident Fund");
            t2row2.Cells[2].Format.Font.Bold = true;
            t2row2.Cells[2].Format.Font.Name = "Arial";
            t2row2.Cells[3].AddParagraph(sl.emp_PF.ToString("0.00"));
            t2row2.Cells[4].AddParagraph("Provident Fund");
            t2row2.Cells[4].Format.Font.Bold = true;
            t2row2.Cells[4].Format.Font.Name = "Arial";
            t2row2.Cells[5].AddParagraph(sl.comp_pf.ToString("0.00"));

            Row t2row3 = table2.AddRow();
            //   row3.Format.Font.Bold = true;
            t2row3.Height = Unit.FromCentimeter(.6);
            t2row3.Format.Font.Name = "Times New Roman";
            t2row3.Cells[0].AddParagraph("HRA");
            t2row3.Cells[0].Format.Font.Bold = true;
            t2row3.Cells[0].Format.Font.Name = "Arial";
            t2row3.Cells[1].AddParagraph(sl.emp_hra.ToString("0.00"));
            t2row3.Cells[2].AddParagraph("ESIC");
            t2row3.Cells[2].Format.Font.Bold = true;
            t2row3.Cells[2].Format.Font.Name = "Arial";
            t2row3.Cells[3].AddParagraph(sl.emp_esic.ToString("0.00"));
            t2row3.Cells[4].AddParagraph("ESIC");
            t2row3.Cells[4].Format.Font.Bold = true;
            t2row3.Cells[4].Format.Font.Name = "Arial";
            t2row3.Cells[5].AddParagraph(sl.comp_esic.ToString("0.00"));
            Row t2row4 = table2.AddRow();
            //   row3.Format.Font.Bold = true;
            t2row4.Height = Unit.FromCentimeter(.6);
            t2row4.Format.Font.Name = "Times New Roman";
            t2row4.Cells[0].AddParagraph("Other Allowence");
            t2row4.Cells[0].Format.Font.Bold = true;
            t2row4.Cells[0].Format.Font.Name = "Arial";
            t2row4.Cells[1].AddParagraph(sl.emp_Other_Allowance.ToString("0.00"));
            t2row4.Cells[2].AddParagraph("Professional Tax");
            t2row4.Cells[2].Format.Font.Bold = true;
            t2row4.Cells[2].Format.Font.Name = "Arial";
            t2row4.Cells[3].AddParagraph(sl.emp_PTAX.ToString("0.00"));
            t2row4.Cells[4].AddParagraph("Total Contribtion");
            t2row4.Cells[4].Format.Font.Bold = true;
            t2row4.Cells[4].Format.Font.Name = "Arial";
            t2row4.Cells[5].AddParagraph(sl.tot_comp_con.ToString("0.00"));
            Row t2row5 = table2.AddRow();
            //   row3.Format.Font.Bold = true;
            t2row5.Height = Unit.FromCentimeter(.6);
            t2row5.Format.Font.Name = "Times New Roman";
            //t2row5.Cells[0].AddParagraph("GROSS PAY");
            //t2row5.Cells[0].Format.Font.Bold = true;
            //t2row5.Cells[0].Format.Font.Name = "Arial";
            //t2row5.Cells[1].AddParagraph(sl.emp_Total_Earning.ToString("0.00"));
            var cellB1 = t2row5[0];
            cellB1.AddParagraph();

            // Merge cellB1 one column right.
            cellB1.MergeRight = 1;
            t2row5.Cells[2].AddParagraph("Total Deduction");
            t2row5.Cells[2].Format.Font.Bold = true;
            t2row5.Cells[2].Format.Font.Name = "Arial";
            t2row5.Cells[3].AddParagraph(sl.tot_emp_ded.ToString("0.00"));
            t2row5.Cells[4].AddParagraph("Total Contribtion");
            t2row5.Cells[4].Format.Font.Bold = true;
            t2row5.Cells[4].Format.Font.Name = "Arial";
            t2row5.Cells[5].AddParagraph(sl.tot_comp_con.ToString("0.00"));
            Row t2row6 = table2.AddRow();
            //   row6.Format.Font.Bold = true;
            t2row6.Height = Unit.FromCentimeter(.6);
            t2row6.Format.Font.Name = "Times New Roman";
            t2row6.Cells[0].AddParagraph("GROSS PAY");
            t2row6.Cells[0].Format.Font.Bold = true;
            t2row6.Cells[0].Format.Font.Name = "Arial";
            t2row6.Cells[1].AddParagraph(sl.emp_Total_Earning.ToString("0.00"));
            t2row6.Cells[2].AddParagraph("NET PAY WITHOUT LOP");
            t2row6.Cells[2].Format.Font.Bold = true;
            t2row6.Cells[2].Format.Font.Name = "Arial";
            t2row6.Cells[3].AddParagraph(sl.actual_net_pay.ToString("0.00"));
            t2row6.Cells[4].AddParagraph("CTC(Monthly)");
            t2row6.Cells[4].Format.Font.Bold = true;
            t2row6.Cells[4].Format.Font.Name = "Arial";
            t2row6.Cells[5].AddParagraph(sl.tot_ctc.ToString("0.00"));
            UtilityController u = new UtilityController();



            var table3 = section.AddTable();

            table3.Style = "Table";

            table3.Format.Alignment = ParagraphAlignment.Center;
            table3.Rows.Alignment = RowAlignment.Center;
            table3.Borders.Visible = false;
            Column column3 = table3.AddColumn("0.2cm");
            column3.Format.Alignment = ParagraphAlignment.Left;
            column3 = table3.AddColumn("24cm");
            column3.Format.Alignment = ParagraphAlignment.Left;
            


            Row t3row = table3.AddRow();
            t3row.Format.Font.Name = "Times New Roman";
            t3row.Format.Font.Bold = true;
            t3row.Format.Font.Size = 12;
            t3row.Format.Font.Name = "Arial";
            t3row.Height = Unit.FromCentimeter(.6);
            t3row.Cells[1].AddParagraph("IN HAND SALARY :" + sl.calculated_net_pay + " (" + u.AmountInWords(Convert.ToDecimal(sl.calculated_net_pay)) + ")");
            




            
            
        }
    }
}
