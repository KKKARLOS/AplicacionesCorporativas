using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using SUPER.Capa_Negocio;
using System.Net.Mime;
using System.Text.RegularExpressions;
using ExcelInterop = Microsoft.Office.Interop.Excel;

//using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

public partial class Default : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        // Put user code to initialize the page here
    }
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        string pf = "ctl00$";

        // Varias Hojas

        ExcelInterop.Application excelApp = null;  
        //ExcelInterop.Workbooks workbooks = null;      
        ExcelInterop.Workbook workbook = null;             
        ExcelInterop.Sheets sheets = null;             
        //ExcelInterop.Worksheet newSheet = null; 
        //ExcelInterop.Range oRange = null; 

        try
        {
            //prjXL = new ExcelInterop.HTML
            excelApp = new ExcelInterop.Application();                    
            workbook = (ExcelInterop.Workbook)(excelApp.Workbooks.Add( Type.Missing ));
            //workbook = excelApp.Workbooks.Add( Type.Missing );

            sheets = workbook.Sheets;
            //string sTablas = Utilidades.unescape(Request.Form[pf + "hdnInputExcel"].ToString()).Replace("<tbody>", "");
            string sTablas = Utilidades.unescape(Request.Form[pf + "hdnInputExcel"].ToString());
           // string sTablas = "<table><tr><td>A1</td><td>B1</td></tr><tr><td>A2</td><td>B2</td></tr></table>{{septabla}}<table><tr><td>A1</td><td>B1</td></tr><tr><td>A2</td><td>B2</td></tr></table>";
            if (sTablas.Substring(0, 1) == ",") sTablas = sTablas.Substring(1, sTablas.Length-1);
            string[] aTablas = Regex.Split(sTablas, "{{septabla}}");

            sTablas = sTablas.Replace("{{septabla}}", "");

            int i = 0;
            object[] objIndex = new object[]{ 1, 2 };
            
            foreach (string sTabla in aTablas)
            {
                if (sTabla == "") continue;
                i++;
                //if (i <= 3)
                //    newSheet = (ExcelInterop.Worksheet)workbook.Sheets.get_Item(i); // Probar para seleccionar la hoja, en caso de tener m?s de 3, utilizar lo de abajo
                //else
                //    newSheet = (ExcelInterop.Worksheet)sheets.Add(Type.Missing, workbook.Sheets[sheets.Application.ActiveWorkbook.Sheets.Count], 1, Type.Missing);

                ////newSheet = (ExcelInterop.Worksheet)sheets.Add(sheets[i], Type.Missing, Type.Missing, Type.Missing);
                //newSheet.Name = "Hoja" + i.ToString();
                object obj = i;
//                workbook.HTMLProject.HTMLProjectItems.Item(ref objIndex[i - 1]).Text = sTabla;
                workbook.HTMLProject.HTMLProjectItems.Item(ref obj).Text = sTabla;
                //                workbook.HTMLProject.RefreshDocument(true);

                //  .Text = sTabla; ;
                //object nIndice;
                //workbook.HTMLProject.HTMLProjectItems.Item
                //HTMLProjectItem oPI = oPIcol.Item(nIndice);
                //= workbook.HTMLProject.HTMLProjectItems.Item(i);

                //workbook.HTMLProject.HTMLProjectItems.Item(newSheet).Text = sTabla;  //("Sheet1").Text = "";

                
                //workbook.HTMLProject.HTMLProjectItems("Hoja" + i.ToString()).Text = sTabla;
               // workbook.HTMLProject.HTMLProjectItems.Application("Sheet1").Text = sTabla;

                //// La primera fila es la cabecera
                //int iCol = 0;
                //foreach (DataColumn c in dt.Columns)
                //{
                //    iCol++;
                //    newSheet.Cells[1, iCol] = c.ColumnName;
                //}

                //// Nos recorremos fila a fila para dar contenido a cada celda
               
                //int iRow = 0;
                //foreach (DataRow r in dt.Rows)
                //{
                //    iRow++;                                       
                //    iCol = 0;
                //    foreach (DataColumn c in dt.Columns)
                //    {
                //        iCol++;
                //        newSheet.Cells[iRow + 1, iCol] = r[c.ColumnName];
                //    }
                //}

                //// Poner en negrita la cabecera

                //iCol = 0;
                //foreach (DataColumn c in dt.Columns)
                //{
                //    iCol++;
                //    oRange = (ExcelInterop.Range)newSheet.Cells[1, iCol];
                //    oRange.HorizontalAlignment = ExcelInterop.Constants.xlCenter; 
                //    oRange.Columns.Font.Bold = true;
                //    oRange.Columns.Interior.ColorIndex = 15;

                //    //oRange.Columns.EntireColumn.AutoFit();
                //}

                //// Ajustamos las columnas a la anchura del contenido

                //iRow = 0;
                //foreach (DataRow r in dt.Rows)
                //{
                //    iRow++;                                  
                //    iCol = 0;
                //    foreach (DataColumn c in dt.Columns)
                //    {
                //        iCol++;
                //        oRange = (ExcelInterop.Range)newSheet.Cells[iRow + 1, iCol];
                //        oRange.Columns.EntireColumn.AutoFit();
                //    }
                //}
            }
            //dataSet.Dispose();
            
            // Cada libro por defecto tiene 3 hojas. Borro desde el final el n?mero de hojas que he insertado

            //int z = sheets.Application.ActiveWorkbook.Sheets.Count;
            //for (int j = z; j > z-i; j--)
            //{
            //    ((ExcelInterop.Worksheet)sheets.Application.ActiveWorkbook.Sheets[j]).Delete();
            //}

            //((ExcelInterop.Worksheet)workbook.Sheets[1]).Select(Type.Missing);

            excelApp.DisplayAlerts = false;
           // excelApp.Visible = true;
            //excelApp.UserControl = true;
            //workbook.HTMLProject.RefreshDocument(true);

            //string FileName = ConfigurationManager.AppSettings["CarpetaExcel"] + "output" + Session["IDFICEPI"].ToString() + ".xls";
            string FileName = ConfigurationManager.AppSettings["CarpetaExcel"] + "output" + Session["IDFICEPI_ENTRADA"].ToString() + ".xls";
                        
            if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName);

            workbook.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Close(true, Type.Missing, Type.Missing);
            //workbook.HTMLProject.RefreshDocument(true);

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "application/octet-stream";

            string attachment = "attachment; filename=" + FileName;
            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.oasis.opendocument.spreadsheet";
 
            Response.TransmitFile(FileName);
            //Response.Redirect(ConfigurationManager.AppSettings["CarpetaExcel"] + "output" + Session["IDFICEPI"].ToString() + ".xls");
            //Response.Flush();
            //Response.End();
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {          
            workbook = null;
            sheets = null;
            //newSheet = null;
            //oRange = null;

            // Close Excel.
            excelApp.Quit();
            excelApp = null;

            //Marshal.ReleaseComObject(newSheet); 
            //Marshal.ReleaseComObject(sheets);
            //Marshal.ReleaseComObject(workbook); 
            //Marshal.ReleaseComObject(xlApp);


        }
    }
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion
}
