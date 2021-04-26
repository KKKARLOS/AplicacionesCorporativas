using System;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.BLL;
using System.IO;
using SUPER.Capa_Negocio;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Xml;

public partial class Capa_Presentacion_ECO_BBII_VerFactura2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Control de sesión
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        #endregion
        byte[] ArchivoBinario = null;
        //if (Request.QueryString["descargaToken"] != null)
        //    Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

        
        try
        {
            long invoicenumber = long.Parse(Utilidades.decodpar(Request.QueryString["idf"].ToString()));
            ArchivoBinario = GetFacturaSAP(invoicenumber);
            if (this.hdnError.Value != "")
            {
                throw new Exception(this.hdnError.Value);
            }
            Response.ContentType = "application/octet-stream";
            //Response.ContentType = "application/pdf";
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");


            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.pdf", invoicenumber.ToString()));
            if (HttpContext.Current.Request.Browser.Browser.ToString() == "Chrome") Response.AddHeader("Content-Length", "999999999999");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();
        }
        catch (Exception ex)
        {
            this.hdnError.Value = "No se ha podido obtener el archivo.<br /><br />Error: " + ex.Message;
            if (ex.InnerException != null)
                this.hdnError.Value += "<br />Detalle error: " + ex.InnerException.Message;
        }
        finally
        {
            if (this.hdnError.Value == "")
                Response.Close();
            //else
            //{
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.Buffer = false;
            //    Response.Flush();
            //    Response.Close();
            //}
        }
    }

    protected byte[] GetFacturaSAP(long invoicenumber)
    {
        string sPaso = "1";
        bool bHayError = false;
        byte[] ArchivoBinario = null;

        wsSAP.z_get_invoice_pdf_b64 mywsSAP = null;
        wsSAP.ZsdSuperGetFacturaPdfXmlResponse oResponse = null;
        try
        {
            SUPER.DAL.Log.Insertar("VerFactura2.aspx.cs " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Factura= " + invoicenumber);

            sPaso = "2";
            mywsSAP = new wsSAP.z_get_invoice_pdf_b64();
            sPaso = "3";
            wsSAP.ZsdSuperGetFacturaPdfXml oParamIn = new wsSAP.ZsdSuperGetFacturaPdfXml();
            sPaso = "4";
            oParamIn.IdFactura = invoicenumber.ToString();
            sPaso = "5";

            mywsSAP.Credentials = new System.Net.NetworkCredential("ADMINSAP", "Productivo_14");
            sPaso = "6";

            mywsSAP.Timeout = 20000;
            System.Threading.Thread.Sleep(2000);

            try
            {
                //Llamo al servicio de SAP que me devuelve el PDF
                oResponse = mywsSAP.ZsdSuperGetFacturaPdfXml(oParamIn);
            }
            catch (Exception err)
            {
                bHayError = true;
                mywsSAP.Dispose();
                string sError = " Error " + err.Message + " GetType " + err.GetType().ToString();// + " InnerException " + ex.InnerException.ToString();
                SUPER.DAL.Log.Insertar("VerFactura.aspx.cs(2) sPaso=" + sPaso + " " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + sError);
                Reintentar();
            }
            sPaso = "7";

            if (!bHayError)
            {
                //Si el resultado es ok, guardar el pdf en disco y abrirlo
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(oResponse.Xml);
                sPaso = "8";
                string coderror = xmldoc.SelectSingleNode("/getinvoice/error").InnerText;
                if (coderror == "0" || coderror == "")
                {
                    string facturabase64 = xmldoc.SelectSingleNode("/getinvoice/pdfinvoice").InnerText;
                    sPaso = "9";

                    ArchivoBinario = Convert.FromBase64String(facturabase64);

                    sPaso = "10";
                }
                else
                {
                    sPaso = "13";
                    this.hdnError.Value = "Se ha producido un error en la llamada al servicio SAP.\n" + xmldoc.SelectSingleNode("/getinvoice/message").InnerText;
                    SUPER.DAL.Log.Insertar("VerFactura.aspx.cs(1) sPaso=" + sPaso + " " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "\nError " + xmldoc.SelectSingleNode("/getinvoice/message").InnerText);
                }
            }
            
        }
        catch (Exception ex)
        {
            string sError = " Error " + ex.Message + " GetType " + ex.GetType().ToString();// + " InnerException " + ex.InnerException.ToString();
            SUPER.DAL.Log.Insertar("VerFactura.aspx.cs(2) sPaso=" + sPaso + " " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + sError);
            this.hdnError.Value = " Error " + ex.Message + "\nEl servicio SAP no devuelve el PDF con la factura.";
        }
        finally
        {
            mywsSAP.Dispose();
        }
        return ArchivoBinario;
    }
    private void Reintentar()
    {
        System.Threading.Thread.Sleep(30000);

        wsSAP.z_get_invoice_pdf_b64 mywsSAP = null;
        wsSAP.ZsdSuperGetFacturaPdfXmlResponse oResponse = null;
        try
        {
            long invoicenumber = long.Parse(Utilidades.decodpar(Request.QueryString["idf"].ToString()));

            SUPER.DAL.Log.Insertar("VerFactura2.aspx.cs->Reintentar " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Factura= " + invoicenumber);

            mywsSAP = new wsSAP.z_get_invoice_pdf_b64();
            wsSAP.ZsdSuperGetFacturaPdfXml oParamIn = new wsSAP.ZsdSuperGetFacturaPdfXml();
            oParamIn.IdFactura = invoicenumber.ToString();

            mywsSAP.Credentials = new System.Net.NetworkCredential("ADMINSAP", "Productivo_14");
            mywsSAP.Timeout = 20000;
            System.Threading.Thread.Sleep(2000);

            oResponse = mywsSAP.ZsdSuperGetFacturaPdfXml(oParamIn);

            //Si el resultado es ok, guardar el pdf en disco y abrirlo
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(oResponse.Xml);

            string coderror = xmldoc.SelectSingleNode("/getinvoice/error").InnerText;
            if (coderror == "0" || coderror == "")
            {
                string facturabase64 = xmldoc.SelectSingleNode("/getinvoice/pdfinvoice").InnerText;

                byte[] bytes = Convert.FromBase64String(facturabase64);

                Response.Clear();
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.pdf", invoicenumber.ToString()));
                Response.ContentType = "application/pdf";
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                Response.BinaryWrite(bytes);
            }
            else
            {
                SUPER.DAL.Log.Insertar("VerFactura.aspx.cs -> Reintentar " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Error " + xmldoc.SelectSingleNode("/getinvoice/message").InnerText);
                ClientScript.RegisterStartupScript(Page.GetType(), "script2", "alert('Error:" + xmldoc.SelectSingleNode("/getinvoice/message").InnerText + "')", true);
            }
        }
        catch (Exception ex)
        {
            string sError = " Error " + ex.Message + " GetType " + ex.GetType().ToString();// + " InnerException " + ex.InnerException.ToString();
            this.hdnError.Value = "Se ha producido un error en la llamada al servicio SAP (reintento).";
            SUPER.DAL.Log.Insertar("VerFactura2.aspx.cs -> Reintentar " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + sError);
        }
        try
        {

            //stop processing the script and return the current result
            HttpContext.Current.Response.End();
        }
        catch (Exception e1)
        {
            //string sErr = e1.Message;
        }
        finally
        {
            //Sends the response buffer
            HttpContext.Current.Response.Flush();
            // Prevents any other content from being sent to the browser
            HttpContext.Current.Response.SuppressContent = true;
            //Directs the thread to finish, bypassing additional processing
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Suspends the current thread
            System.Threading.Thread.Sleep(5000);

            mywsSAP.Dispose();
        }
    }

}