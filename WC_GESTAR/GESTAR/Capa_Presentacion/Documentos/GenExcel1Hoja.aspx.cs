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
using GESTAR.Capa_Negocio;
using System.Net.Mime;
using System.Text.RegularExpressions;


//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;

public partial class Default : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        string sPaso = "1";
        string sInputExcel = "";

        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            // Una Hoja

            string pf = "ctl00$";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;


            //            Response.ContentType = "application/octet-stream";
            //            Response.ContentType = "application/xls";
            //            Response.ContentType = "application/vnd.ms-excel";
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "hoja.xls"));
            Response.ContentType = "application/xls";
            string sCadena = "";
            //El churro a pasar a Excel puede venir en cache o en el objeto hdnInputExcel de la ventana llamante
            string sIdCache = "";


            if (Request.QueryString["cache"] != null)
                sIdCache = Request.QueryString["cache"].ToString();

            if (sIdCache != "")
            {
                /*
                sCadena = (string)HttpContext.Current.Cache.Get(sIdCache);
                sPaso = "5.1";
                HttpContext.Current.Cache.Remove(sIdCache);
                sPaso = "5.2";
                */
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
                if (sCadena != "")
                {
                    if (sCadena.Substring(0, 1) == ",") sCadena = sCadena.Substring(1);

                    sCadena = sCadena.Replace("{{septabla}}", "");
                    sCadena = sCadena.Replace(",<TABLE", "<TABLE");
                }
            }

            Response.Write(sCadena);
            //    Response.Flush();
            //    Response.Close();
            Response.End();
        }
        catch (Exception e1)
        {
            GESTAR.Capa_Datos.Log.Insertar("GenExcel1Hoja sPaso=" + sPaso + HttpContext.Current.Session["NOMBRE"].ToString() + " Error " + e1.Message + " sImputExcel: " + sInputExcel);
        }
    }
}
