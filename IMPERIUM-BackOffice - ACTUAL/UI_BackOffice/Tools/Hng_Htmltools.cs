using ClosedXML.Excel;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Reflection;

namespace UI_BackOffice.Tools
{
    internal class Hng_Htmltools
    {
        public enum HtmlExportFormat { KardexValorizado_Detalle, KardexValorizado, Inventario, Libro_12_1, Libro_13_1 }
        public void ParseHtmlToExcel(string html, string excel_path, HtmlExportFormat format, bool open_file = true)
        {
            // Load HTML content into HTMLDocument
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Create a new Excel workbook
            XLWorkbook workbook = new XLWorkbook();

            // Add a worksheet to the workbook
            IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

            switch (format)
            {
                case HtmlExportFormat.KardexValorizado_Detalle:
                    {
                        worksheet.Column(1).Style.NumberFormat.Format = "@";
                        worksheet.Column(8).Style.NumberFormat.Format = "@";
                        worksheet.Column(9).Style.NumberFormat.Format = "@";
                        for (int i = 12; i < 19; i++)
                        {
                            worksheet.Column(i).Style.NumberFormat.Format = "#,##0.0000";
                            worksheet.Column(i).Width = 12;
                        }

                        worksheet.Column(1).Width = 12;
                        worksheet.Column(2).Width = 18;
                        worksheet.Column(3).Width = 18;
                        worksheet.Column(4).Width = 48;
                        worksheet.Column(5).Width = 12;

                        for (int col = 1; col < 20; col++)
                        {
                            for (int rw = 1; rw < 6; rw++)
                            {
                                worksheet.Cell(rw, col).Style.Font.Bold = true;
                                if (rw > 3)
                                {
                                    worksheet.Cell(rw, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(rw, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    worksheet.Cell(rw, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                }
                            }
                        }
                        //Merge Cells
                        foreach (var m in new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" })
                        {
                            worksheet.Range($"{m}4:{m}5").Merge();
                        }
                        worksheet.Range("M4:N4").Merge();
                        worksheet.Range("O4:P4").Merge();
                        worksheet.Range("Q4:R4").Merge();
                        worksheet.Range("S4:S5").Merge();

                        worksheet.Range("A4:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(198, 239, 206);
                        worksheet.Range("A4:L5").Style.Font.FontColor = XLColor.FromArgb(0, 97, 83);

                        worksheet.Range("M4:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(255, 235, 156);
                        worksheet.Range("M4:N5").Style.Font.FontColor = XLColor.FromArgb(156, 87, 0);

                        worksheet.Range("O4:P5").Style.Fill.BackgroundColor = XLColor.FromArgb(255, 199, 206);
                        worksheet.Range("O4:P5").Style.Font.FontColor = XLColor.FromArgb(156, 0, 49);

                        worksheet.Range("Q4:R5").Style.Fill.BackgroundColor = XLColor.FromArgb(198, 239, 206);
                        worksheet.Range("Q4:R5").Style.Font.FontColor = XLColor.FromArgb(0, 97, 83);

                        worksheet.Range("S4:S5").Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);
                        worksheet.Range("S4:S5").Style.Font.FontColor = XLColor.FromArgb(68, 84, 154);

                        worksheet.Range("A4:S5").Style.Alignment.WrapText = true;
                        break;
                    }
                case HtmlExportFormat.KardexValorizado:
                    {
                        worksheet.Column(1).Style.NumberFormat.Format = "@";
                        worksheet.Column(4).Style.NumberFormat.Format = "@";
                        worksheet.Column(5).Style.NumberFormat.Format = "@";
                        for (int i = 8; i <= 14; i++)
                        {
                            worksheet.Column(i).Style.NumberFormat.Format = "#,##0.0000";
                            worksheet.Column(i).Width = 12;
                        }

                        worksheet.Column(1).Width = 12;
                        worksheet.Column(2).Width = 12;
                        worksheet.Column(3).Width = 12;
                        worksheet.Column(4).Width = 12;
                        worksheet.Column(5).Width = 12;
                        worksheet.Column(7).Width = 20;

                        for (int col = 1; col <= 15; col++)
                        {
                            for (int rw = 1; rw <= 5; rw++)
                            {
                                worksheet.Cell(rw, col).Style.Font.Bold = true;
                                if (rw > 3)
                                {
                                    worksheet.Cell(rw, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(rw, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    worksheet.Cell(rw, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                }
                            }
                        }
                        //Merge Cells
                        foreach (var m in new string[] { "A", "B", "C", "D", "E", "F", "G", "H" })
                        {
                            worksheet.Range($"{m}4:{m}5").Merge();
                        }
                        worksheet.Range("I4:J4").Merge();
                        worksheet.Range("K4:L4").Merge();
                        worksheet.Range("M4:N4").Merge();
                        worksheet.Range("O4:O5").Merge();

                        worksheet.Range("A4:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(198, 239, 206);
                        worksheet.Range("A4:H5").Style.Font.FontColor = XLColor.FromArgb(0, 97, 83);

                        worksheet.Range("I4:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(255, 235, 156);
                        worksheet.Range("I4:J5").Style.Font.FontColor = XLColor.FromArgb(156, 87, 0);

                        worksheet.Range("K4:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(255, 199, 206);
                        worksheet.Range("K4:L5").Style.Font.FontColor = XLColor.FromArgb(156, 0, 49);

                        worksheet.Range("M4:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(198, 239, 206);
                        worksheet.Range("M4:N5").Style.Font.FontColor = XLColor.FromArgb(0, 97, 83);

                        worksheet.Range("O4:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);
                        worksheet.Range("O4:O5").Style.Font.FontColor = XLColor.FromArgb(68, 84, 154);

                        worksheet.Range("A4:O5").Style.Alignment.WrapText = true;
                        break;
                    }
                case HtmlExportFormat.Inventario:
                    {
                        worksheet.Column(1).Style.NumberFormat.Format = "@";
                        worksheet.Column(12).Style.NumberFormat.Format = "@";
                        for (int i = 7; i <= 11; i++)
                        {
                            worksheet.Column(i).Style.NumberFormat.Format = "#,##0.0000";
                            worksheet.Column(i).Width = 12;
                        }

                        worksheet.Column(1).Width = 12;
                        worksheet.Column(2).Width = 52;
                        worksheet.Column(3).Width = 12;
                        worksheet.Column(4).Width = 12;
                        worksheet.Column(5).Width = 12;
                        worksheet.Column(6).Width = 12;

                        for (int col = 1; col <= 12; col++)
                        {
                            for (int rw = 1; rw <= 6; rw++)
                            {
                                worksheet.Cell(rw, col).Style.Font.Bold = true;
                                if (rw > 3)
                                {
                                    worksheet.Cell(rw, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(rw, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    worksheet.Cell(rw, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                }
                            }
                        }
                        //Merge Cells
                        foreach (var m in new string[] { "A", "B" })
                        {
                            worksheet.Range($"{m}4:{m}6").Merge();
                        }
                        worksheet.Range("C4:F4").Merge();
                        worksheet.Range("G4:K4").Merge();
                        worksheet.Range("L4:L6").Merge();

                        worksheet.Range("C5:F5").Merge();
                        worksheet.Range("G5:j5").Merge();

                        worksheet.Range("A4:L6").Style.Fill.BackgroundColor = XLColor.FromArgb(198, 239, 206);
                        worksheet.Range("A4:L6").Style.Font.FontColor = XLColor.FromArgb(0, 97, 83);

                        worksheet.Range("A4:L6").Style.Alignment.WrapText = true;
                        break;
                    }
                case HtmlExportFormat.Libro_12_1:
                    {
                        worksheet.Column(4).Style.NumberFormat.Format = "@";
                        worksheet.Column(6).Style.NumberFormat.Format = "@";
                        for (int i = 8; i <= 10; i++)
                        {
                            worksheet.Column(i).Style.NumberFormat.Format = "#,##0.0000";
                            worksheet.Column(i).Width = 14;
                        }

                        worksheet.Column(1).Width = 12;
                        worksheet.Column(2).Width = 10;
                        worksheet.Column(3).Width = 10;
                        worksheet.Column(4).Width = 14;
                        worksheet.Column(5).Width = 22;
                        worksheet.Column(6).Width = 8;
                        worksheet.Column(7).Width = 28;

                        for (int col = 1; col <= 10; col++)
                        {
                            for (int rw = 1; rw <= 8; rw++)
                            {
                                worksheet.Cell(rw, col).Style.Font.Bold = true;
                                if (rw > 5)
                                {
                                    worksheet.Cell(rw, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(rw, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    worksheet.Cell(rw, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                }
                            }
                        }

                        //foreach (var m in new string[] { "A", "B" })
                        //{
                        //    worksheet.Range($"{m}4:{m}6").Merge();
                        //}
                        worksheet.Range("A6:G6").Merge();
                        worksheet.Range("A7:G7").Merge();
                        worksheet.Range("F8:G8").Merge();

                        worksheet.Range("H6:H8").Merge();
                        worksheet.Range("I6:I8").Merge();
                        worksheet.Range("J6:J8").Merge();

                        worksheet.Range("A6:J8").Style.Fill.BackgroundColor = XLColor.FromArgb(89, 139, 125);
                        worksheet.Range("A6:J8").Style.Font.FontColor = XLColor.FromArgb(248, 248, 248);

                        worksheet.Range("A6:J8").Style.Alignment.WrapText = true;
                        break;
                    }
                case HtmlExportFormat.Libro_13_1:
                    {
                        //worksheet.SheetView.ShowGridLines = false;
                        worksheet.SheetView.Worksheet.ShowGridLines = false;
                        worksheet.Column(4).Style.NumberFormat.Format = "@";
                        worksheet.Column(6).Style.NumberFormat.Format = "@";
                        worksheet.Range("A2:P7").Style.Font.FontSize = 9;

                        for (int i = 8; i <= 16; i++)
                        {
                            worksheet.Column(i).Style.NumberFormat.Format = "#,##0.0000";
                            worksheet.Column(i).Width = 14;
                        }

                        worksheet.Column(1).Width = 16;
                        worksheet.Column(2).Width = 10;
                        worksheet.Column(3).Width = 6;
                        worksheet.Column(4).Width = 10;
                        worksheet.Column(5).Width = 26;
                        worksheet.Column(6).Width = 6;
                        worksheet.Column(7).Width = 30;

                        for (int col = 1; col <= 16; col++)
                        {
                            for (int rw = 1; rw <= 10; rw++)
                            {
                                worksheet.Cell(rw, col).Style.Font.Bold = true;
                                if (rw > 7)
                                {
                                    worksheet.Cell(rw, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(rw, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    worksheet.Cell(rw, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    worksheet.Cell(rw, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                }
                            }
                        }


                        //foreach (var m in new string[] { "A", "B" })
                        //{
                        //    worksheet.Range($"{m}4:{m}6").Merge();
                        //}
                        worksheet.Range("A8:G8").Merge();
                        worksheet.Range("A9:G9").Merge();
                        worksheet.Range("H8:J8").Merge();
                        worksheet.Range("K8:M8").Merge();
                        worksheet.Range("N8:P8").Merge();

                        worksheet.Range("H9:H10").Merge();
                        worksheet.Range("I9:I10").Merge();
                        worksheet.Range("J9:J10").Merge();
                        worksheet.Range("K9:K10").Merge();
                        worksheet.Range("L9:L10").Merge();
                        worksheet.Range("M9:M10").Merge();
                        worksheet.Range("N9:N10").Merge();
                        worksheet.Range("O9:O10").Merge();
                        worksheet.Range("P9:P10").Merge();

                        worksheet.Range("F10:G10").Merge();

                        worksheet.Range("A8:P10").Style.Fill.BackgroundColor = XLColor.FromArgb(89, 139, 125);
                        worksheet.Range("A8:P10").Style.Font.FontColor = XLColor.FromArgb(248, 248, 248);

                        worksheet.Range("A8:P10").Style.Alignment.WrapText = true;

                        worksheet.Range("A2:P7").Style.Font.Bold = false;

                        break;
                    }
            }

            // Parse HTML and populate worksheet
            int row = 1;
            foreach (HtmlNode table in htmlDoc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode rowsNode in table.SelectNodes("thead"))
                {
                    foreach (HtmlNode rowNode in rowsNode.SelectNodes("tr"))
                    {
                        int col = 1;
                        foreach (HtmlNode cell in rowNode.SelectNodes("th"))//"th|td
                        {
                            worksheet.Cell(row, col).Value = cell.InnerHtml;
                            col++;
                        }
                        row++;
                    }
                }

                foreach (HtmlNode rowsNode in table.SelectNodes("tbody"))
                {
                    foreach (HtmlNode rowNode in rowsNode.SelectNodes("tr"))
                    {
                        int col = 1;
                        foreach (HtmlNode cell in rowNode.SelectNodes("td")) //th|td
                        {
                            if (cell.InnerHtml.StartsWith("Bold:")) { worksheet.Range($"A{row}:Z{row}").Style.Font.Bold = true; }
                            if (cell.InnerHtml.Contains("Red:")) { worksheet.Range($"A{row}:Z{row}").Style.Font.FontColor = XLColor.FromArgb(207, 49, 67); }
                            if (cell.InnerHtml.Contains("Blue:")) { worksheet.Range($"A{row}:Z{row}").Style.Font.FontColor = XLColor.FromArgb(0, 78, 234); }
                            var value = cell.InnerHtml.Replace("Bold:", "").Replace("Red:", "").Replace("Blue:", "");
                            worksheet.Cell(row, col).Value = value;

                            col++;
                        }
                        row++;
                    }
                }
            }



            /*-----*Configuración para pié*-----*/

            switch (format)
            {
                case HtmlExportFormat.KardexValorizado_Detalle:
                    {
                        worksheet.Range($"A{row - 3}:S{row}").Style.Font.Bold = true;
                        worksheet.Range($"A{row - 4}:S{row - 4}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        break;
                    }
                case HtmlExportFormat.KardexValorizado:
                    {
                        worksheet.Range($"A{row - 3}:O{row}").Style.Font.Bold = true;
                        worksheet.Range($"A{row - 4}:O{row - 4}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        break;
                    }
                case HtmlExportFormat.Inventario:
                    {
                        worksheet.Range($"E{row - 3}:O{row}").Style.NumberFormat.Format = "#,##0.0000";
                        worksheet.Range($"A{row - 3}:O{row}").Style.Font.Bold = true;
                        worksheet.Range($"A{row - 4}:O{row - 4}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        break;
                    }
                case HtmlExportFormat.Libro_12_1:
                    {
                        //worksheet.Range($"E{row - 3}:O{row}").Style.NumberFormat.Format = "#,##0.0000";
                        worksheet.Range($"A{row - 3}:J{row}").Style.Font.Bold = true;
                        worksheet.Range($"A{row - 2}:J{row - 2}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        break;
                    }
                case HtmlExportFormat.Libro_13_1:
                    {
                        //worksheet.Range($"E{row - 3}:O{row}").Style.NumberFormat.Format = "#,##0.0000";
                        worksheet.Range($"A{row - 3}:P{row}").Style.Font.Bold = true;
                        worksheet.Range($"A{row - 2}:P{row - 2}").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        break;
                    }
            }
            // Save the workbook as an Excel file
            string download_folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
            string unique = DateTime.Now.ToString("yyyyMMddHHmmss");
            string file = $"~{excel_path}__tmp{unique}.xlsx";
            string path = $"{download_folder}{file}";
            workbook.SaveAs(path);

            if (open_file)
                Process.Start(path);
        }


        public class ToolTable
        {
            private readonly int numColumns = 0;
            //private readonly bool cero = false;
            public ToolTable(int numColumns) { this.numColumns = numColumns; this.Clear(); }
            //public string C0 { get; set; }
            #region Attributes
            public string C1 { get; set; }
            public string C2 { get; set; }
            public string C3 { get; set; }
            public string C4 { get; set; }
            public string C5 { get; set; }
            public string C6 { get; set; }
            public string C7 { get; set; }
            public string C8 { get; set; }
            public string C9 { get; set; }
            public string C10 { get; set; }
            public string C11 { get; set; }
            public string C12 { get; set; }
            public string C13 { get; set; }
            public string C14 { get; set; }
            public string C15 { get; set; }
            public string C16 { get; set; }
            public string C17 { get; set; }
            public string C18 { get; set; }
            public string C19 { get; set; }
            public string C20 { get; set; }
            public string C21 { get; set; }
            public string C22 { get; set; }
            public string C23 { get; set; }
            public string C24 { get; set; }
            public string C25 { get; set; }
            public string C26 { get; set; }
            public string C27 { get; set; }
            public string C28 { get; set; }
            public string C29 { get; set; }
            public string C30 { get; set; }
            #endregion

            public void ColumnStyle() => Set("<th></th>");
            public void Clear() => Set("");
            public void RowStyle() => Set("<td></td>");

            private void Set(string value)
            {
                for (int i = 1; i <= numColumns; i++)
                {
                    PropertyInfo prop = this.GetType().GetProperty($"C{i}");
                    prop.SetValue(this, value, null);
                }
            }
            public string[] GetArray()
            {
                string[] array = new string[numColumns];
                for (int i = 1; i <= numColumns; i++)
                {
                    PropertyInfo prop = this.GetType().GetProperty($"C{i}");
                    var value = prop.GetValue(this, null);
                    array[i - 1] = value?.ToString();
                }
                return array;
            }
        }
        public string RowValue(object value, bool bold = false, bool red = false, bool blue = false) => $"<td>{(bold ? "Bold:" : "")}{(red ? "Red:" : "")}{(blue ? "Blue:" : "")}{value}</td>";
        public string HeaderValue(object value) => $"<th>{value}</th>";
        public string GetConcatRows(ToolTable values) => $"<tr>{string.Join(" ", values.GetArray())}</tr>";
        public string GetTemplate() => $"<table><thead>@cols</thead><tbody>@rows</tbody></table>";
    }
}
