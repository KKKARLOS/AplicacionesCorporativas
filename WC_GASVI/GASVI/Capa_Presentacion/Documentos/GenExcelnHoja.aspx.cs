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
using GASVI.BLL;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;

public partial class Default : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        string pf = "ctl00$";

        // Varias Hojas

        Excel.Application excelApp = null;  
        //Excel.Workbooks workbooks = null;      
        Excel.Workbook workbook = null;             
        Excel.Sheets sheets = null;             
        Excel.Worksheet newSheet = null; 
        Excel.Range oRange = null; 

        try
        {   
            excelApp = new Excel.Application();                    
            workbook = (Excel.Workbook)(excelApp.Workbooks.Add( Type.Missing ));
            sheets = workbook.Sheets;
            string sTablas = Utilidades.unescape(Request.Form[pf + "hdnInputExcel"].ToString()).Replace("<tbody>", "");
            sTablas = sTablas.Replace("</tbody>", "");
            sTablas = sTablas.Replace("{{septabla}}", "");

            DataSet dataSet = ConvertHTMLTables.ToDataSet(sTablas);
            
            int i = 0;
            foreach (DataTable dt in dataSet.Tables)
            {
                i++;
                if (i<=3)
                    newSheet = (Excel.Worksheet)workbook.Sheets.get_Item(i); // Probar para seleccionar la hoja, en caso de tener más de 3, utilizar lo de abajo
                else
                    newSheet = (Excel.Worksheet)sheets.Add(Type.Missing, workbook.Sheets[sheets.Application.ActiveWorkbook.Sheets.Count],1, Type.Missing);

                //newSheet = (Excel.Worksheet)sheets.Add(sheets[i], Type.Missing, Type.Missing, Type.Missing);
                newSheet.Name = "Hoja"+i.ToString();                     
 
                // La primera fila es la cabecera
                
                int iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    newSheet.Cells[1, iCol] = c.ColumnName;
                }

                // Nos recorremos fila a fila para dar contenido a cada celda
               
                int iRow = 0;
                foreach (DataRow r in dt.Rows)
                {
                    iRow++;                                       
                    iCol = 0;
                    foreach (DataColumn c in dt.Columns)
                    {
                        iCol++;
                        newSheet.Cells[iRow + 1, iCol] = r[c.ColumnName];
                    }
                }

                // Poner en negrita la cabecera

                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    oRange = (Excel.Range)newSheet.Cells[1, iCol];
                    oRange.HorizontalAlignment = Excel.Constants.xlCenter; 
                    oRange.Columns.Font.Bold = true;
                    oRange.Columns.Interior.ColorIndex = 15;

                    //oRange.Columns.EntireColumn.AutoFit();
                }

                // Ajustamos las columnas a la anchura del contenido

                iRow = 0;
                foreach (DataRow r in dt.Rows)
                {
                    iRow++;                                  
                    iCol = 0;
                    foreach (DataColumn c in dt.Columns)
                    {
                        iCol++;
                        oRange = (Excel.Range)newSheet.Cells[iRow + 1, iCol];
                        oRange.Columns.EntireColumn.AutoFit();
                    }
                }
            }

            dataSet.Dispose();
            
            // Cada libro por defecto tiene 3 hojas. Borro desde el final el número de hojas que he insertado

            //int z = sheets.Application.ActiveWorkbook.Sheets.Count;
            //for (int j = z; j > z-i; j--)
            //{
            //    ((Excel.Worksheet)sheets.Application.ActiveWorkbook.Sheets[j]).Delete();
            //}

            ((Excel.Worksheet)workbook.Sheets[1]).Select(Type.Missing);

            excelApp.DisplayAlerts = false;
            
            //string FileName = Server.MapPath(".") + @"\\prueba.xls";
            string FileName = ConfigurationManager.AppSettings["CarpetaExcel"] + "output" + Session["IDFICEPI"].ToString() + ".xls";
                        
            if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName);

            workbook.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Close(true, Type.Missing, Type.Missing);

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;

            string attachment = "attachment; filename=" + FileName;
            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.oasis.opendocument.spreadsheet";
 
            Response.TransmitFile(FileName);
            //Response.Redirect(ConfigurationManager.AppSettings["CarpetaExcel"] + "output" + Session["IDFICEPI"].ToString() + ".xls");
            //Response.Flush();
            Response.End();
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {          
            workbook = null;
            sheets = null;
            newSheet = null;
            oRange = null;

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
