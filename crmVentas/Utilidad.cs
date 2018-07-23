using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System.IO;
using System.Data;
using System.Text;

namespace DataTableToExcel
{
    public class Utilidad
    {
        HSSFWorkbook hssfworkbook;

        MemoryStream WriteToStream()
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }

        void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        public static int LoadImage(string path, HSSFWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.JPEG);
        }

        // <summary>
        // Render DataTable to Excel File
        // </summary>
        // <param name="sourceTable">Source DataTable</param>
        // <param name="fileName">Destination File name</param>
        public static void ExportDataTableToExcel(DataTable SourceTable, string FileName, string Servicio, string Reporte, string FechaDesde, string FechaHasta)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream memoryStream = new MemoryStream();
            Sheet sheet = workbook.CreateSheet("Hoja1");

            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //create the anchor
            HSSFClientAnchor anchor;
            anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 4, 7);
            anchor.AnchorType = 1;
            //load the picture and get the picture index in the workbook
            //HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(@"C:\Documents and Settings\Administrador\Escritorio\sisss\images\Logo_Dydcom.jpg", workbook));

            //Reset the image to the original size.
            //picture.Resize();

            //picture.LineStyle = HSSFPicture.LINESTYLE_NONE;

            //Definimos la fila inicial para crear los encabezados
            Row headerRow = sheet.CreateRow(0);

            sheet.DisplayGridlines = true;
            //Cell CellServicio = sheet.CreateRow(3).CreateCell(0);
            //CellServicio.SetCellValue(new HSSFRichTextString("Reporte desde el periodo " + FechaDesde + " hasta " + FechaHasta));

            //Cell CellFecha = sheet.CreateRow(2).CreateCell(4);
            //CellFecha.SetCellValue(new HSSFRichTextString("Fecha: " + DateTime.Now));

            //Cell CellReporte = sheet.CreateRow(3).CreateCell(4);
            //CellReporte.SetCellValue(new HSSFRichTextString("Reporte: " + Reporte));

            // handling header.
            foreach (DataColumn column in SourceTable.Columns)
            {
                //headerRow.RowNum = 4;

                // Create New Cell
                Cell headerCell = headerRow.CreateCell(column.Ordinal);
                
                // Set Cell Value
                headerCell.SetCellValue(column.ColumnName);

                // Create Style
                CellStyle headerCellStyle = workbook.CreateCellStyle();
                //headerCellStyle.FillForegroundColor = HSSFColor.WHITE.index;
                //headerCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                
                // Add Style to Cell
                headerCell.CellStyle = headerCellStyle;

            }
            //Fila inicial para el la escritura de datos.
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                Row dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    if (column.Ordinal == 0)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToString(row[column]));
                    }
                }

                rowIndex++;
            }
            rowIndex++;

            //sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue("Total:");
            //sheet.GetRow(rowIndex).CreateCell(1).CellFormula = "SUM(B7:B" + (rowIndex) + ")";
            //sheet.GetRow(rowIndex).CreateCell(2).CellFormula = "SUM(C7:C" + (rowIndex) + ")";
            //sheet.GetRow(rowIndex).CreateCell(3).CellFormula = "SUM(D7:D" + (rowIndex) + ")";

            //sheet.GetRow(rowIndex).GetCell(3).CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");
            
            //Autodimencionando el ancho de las columnas
            foreach (DataColumn column in SourceTable.Columns)
            {
                sheet.AutoSizeColumn(column.Ordinal);
            }
            
            workbook.Write(memoryStream);
            memoryStream.Flush();

            HttpResponse response = HttpContext.Current.Response;
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", FileName));
            response.Clear();

            response.BinaryWrite(memoryStream.GetBuffer());
            response.End();
        }


        public static String ExportToCSVFile(DataTable dtTable)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ';');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        sbldr.Append(row[column].ToString() + ';');
                    }
                    sbldr.Append("\r\n");
                }
            }

            //UTF8Encoding utf8 = new UTF8Encoding();
            //var preamble = utf8.GetPreamble();
            //string data = utf8.GetBytes(sbldr.ToString()).ToString();
            return Convert.ToString(sbldr);
        }
    }
}