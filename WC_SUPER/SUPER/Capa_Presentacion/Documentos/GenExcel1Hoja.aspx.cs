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
            sPaso = "2";
            // Una Hoja

            string pf = "ctl00$";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;

            sPaso = "3";
//            Response.ContentType = "application/octet-stream";
//            Response.ContentType = "application/xls";
//            Response.ContentType = "application/vnd.ms-excel";
//            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "hoja.xls"));
            //if (HttpContext.Current.Request.Browser.Browser.ToString() == "Chrome") Response.AddHeader("Content-Length", "999999999999");
            Response.ContentType = "application/xls";
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            string sCadena = "";

            sPaso = "4";

            //El churro a pasar a Excel puede venir en cache o en el objeto hdnInputExcel de la ventana llamante
            string sIdCache = "";
 
            
            if (Request.QueryString["cache"] != null)
                sIdCache = Request.QueryString["cache"].ToString();

            sPaso = "5";

            if (sIdCache != "")
            {
                /*
                sCadena = (string)HttpContext.Current.Cache.Get(sIdCache);
                sPaso = "5.1";
                HttpContext.Current.Cache.Remove(sIdCache);
                sPaso = "5.2";
                */
                sPaso = "5.1";
                if (Session[sIdCache] != null)
                {
                    sPaso = "5.2";
                    sCadena = Session[sIdCache].ToString();
                    Session[sIdCache]=null;
                }
                sPaso = "5.3";
            }
            else
            {
                sPaso = "5.4";
                sInputExcel = Request.Form[pf + "hdnInputExcel"].ToString();
                sPaso = "5.5";
                sCadena = Utilidades.unescape(sInputExcel);
                sPaso = "5.6";
                if (sCadena != "")
                {
                    sPaso = "5.7";
                    if (sCadena.Substring(0, 1) == ",") sCadena = sCadena.Substring(1);
                    sPaso = "5.8";
                    sCadena = sCadena.Replace("{{septabla}}", "");
                    sCadena = sCadena.Replace(",<TABLE", "<TABLE");
                    sPaso = "5.9";
                }
            }
            sPaso = "6";
            Response.Write(sCadena);
            sPaso = "7";
        }
        catch (Exception e1)
        {
            SUPER.DAL.Log.Insertar("GenExcel1Hoja sPaso=" + sPaso + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Error " + e1.Message + " sImputExcel: " + sInputExcel);
        }

        //    Response.Flush();
        //      Response.Close();


        //Response.End();
        try
        {
            //stop processing the script and return the current result
            HttpContext.Current.Response.End();
        }
        catch (Exception) { }
        finally
        {
            //Sends the response buffer
            HttpContext.Current.Response.Flush();
            // Prevents any other content from being sent to the browser
            HttpContext.Current.Response.SuppressContent = true;
            //Directs the thread to finish, bypassing additional processing
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Suspends the current thread
            System.Threading.Thread.Sleep(1);
        }
    }
}
