using System;
using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GESTAR.Capa_Negocio;
using System.Text.RegularExpressions;
using System.IO;

public partial class Capa_Presentacion_Documentos_getExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Ejemplo en: http://www.experts-exchange.com/Web_Development/Web_Languages-Standards/ASP/Q_22059338.html

        string pf = "ctl00$";
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();

        string sCadena = "";
        string sInputExcel = "";

        //El churro a pasar a Excel puede venir en cache o en el objeto hdnInputExcel de la ventana llamante
        string sIdCache = "";
        if (Request.QueryString["cache"] != null) sIdCache = Request.QueryString["cache"].ToString();

        if (sIdCache != "")
        {
            if (Session[sIdCache] != null)
            {
                sCadena = Session[sIdCache].ToString();
                Session[sIdCache] = null;
            }
        }
        else
        {
            sInputExcel = Request.Form[pf + "hdnInputExcel"].ToString();
            sCadena = Utilidades.unescape(sInputExcel);
        }

        string sTablas = Utilidades.unescape(sCadena);
        //string sTablas = Utilidades.unescape(Request.Form[pf + "hdnInputExcel"].ToString());

        if (sTablas.Substring(0, 1) == ",") sTablas = sTablas.Substring(1);
        string[] aTablas = Regex.Split(sTablas, "{{septabla}}");

        Response.AddHeader("Content-Disposition", "attachment;filename=" + "GESTARExcel-" + Session["IDFICEPI"].ToString() + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Buffer = true;

        //Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\ \http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\>");
        Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
        Response.Write("<head>");
        Response.Write("<meta name=\"Excel Workbook Frameset\">");
        Response.Write("<xml>");
        Response.Write("      <x:ExcelWorkbook>");
        Response.Write("            <x:ExcelWorksheets>");

        //Response.Write("                  <x:ExcelWorksheet>");
        //Response.Write("                        <x:Name>First Sheet</x:Name>");
        //Response.Write("                        <x:WorksheetSource HRef=\"sheet1a.htm\"/>");
        //Response.Write("                  </x:ExcelWorksheet>");

        //Response.Write("                  <x:ExcelWorksheet>");
        //Response.Write("                        <x:Name>Second Sheet</x:Name>");
        //Response.Write("                        <x:WorksheetSource HRef=\"sheet2a.htm\"/>");
        //Response.Write("                  </x:ExcelWorksheet>");

        int i = 0;

        string ImgExpression = "src='(.*?)'";

        foreach (string sTabla in aTablas)
        {
            if (sTabla == "") continue;
            string sTablaAux = sTabla;
            i++;

            ArrayList aImages = new ArrayList();
            MatchCollection Images = Regex.Matches(sTablaAux, ImgExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            #region Conversión de imágenes a objetos embebidos en Base64 /* Por ahora no */
            //// Loop through each table element    
            //foreach (Match sImage in Images)
            //{
            //    int nPosDesde = 5; // src='
            //    int nPosHasta = sImage.ToString().IndexOf("?");
            //    int nLength = sImage.ToString().Length;

            //    string sImagenAux = sImage.ToString().Substring(nPosDesde, nPosHasta-nPosDesde);
            //    sImagenAux = sImagenAux.Substring(sImagenAux.LastIndexOf("/")+1);

            //    //string a = Image.ToString().Substring(5, Image.ToString().Length - 6);
            //    string a1 = Request.PhysicalApplicationPath + "TempImagesGraficos\\" + sImagenAux;// a.Substring(a.LastIndexOf("/") + 1);
            //    string b = Utilidades.ImageToBase64(a1, System.Drawing.Imaging.ImageFormat.Png);

            //    //sImage = sTabla.Replace(sImage.ToString(), "src='data:image/gif;base64," + b + "'");
            //    aImages.Add(new string[] { sImage.ToString(), "src='data:image/png;base64," + b + "'" });
            //}

            //for (int x = 0; x < aImages.Count; x++)
            //{
            //    string[] aStr = (string[])aImages[x];
            //   // sTablaAux = sTablaAux.Replace(aStr[0], aStr[1]);
            //}
            #endregion

            Response.Write("                  <x:ExcelWorksheet>");
            Response.Write("                        <x:Name>Hoja " + i.ToString() + "</x:Name>");

            #region Crear archivo
            string sFichero = "GESTARExcel-" + Session["IDFICEPI"].ToString() + "-" + DateTime.Now.Ticks.ToString() + ".htm";
            string sPath = Request.PhysicalApplicationPath + "TempImagesGraficos\\" + sFichero;
            string sURL = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("Capa_Presentacion")) + "TempImagesGraficos/" + sFichero;

            FileStream fs = new FileStream(sPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter fp = new StreamWriter(fs, System.Text.Encoding.UTF8); // create a stream writer 
            //StreamWriter fp = File.CreateText(sPath);
            fp.WriteLine(sTablaAux);//sTabla
            fp.Close();

            #endregion

            Response.Write("                        <x:WorksheetSource HRef=\"" + sURL + "\"/>");//sPath
            Response.Write("                  </x:ExcelWorksheet>");
        }

        Response.Write("            </x:ExcelWorksheets>");
        Response.Write("      </x:ExcelWorkbook>");
        Response.Write("</xml>");
        Response.Write("</head>");
        Response.Write("</html>");

        if (Response.IsClientConnected)
            Response.Flush();
        Response.Close();
        //Response.End();
    }
}