using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_ECO_BBII_VerFactura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sPaso = "1";
        bool bHayError = false;

        wsSAP.z_get_invoice_pdf_b64 mywsSAP=null;
        wsSAP.ZsdSuperGetFacturaPdfXmlResponse oResponse = null;
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducada.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            //int invoicenumber = int.Parse(Request.Form["invoicenumber"]);
            long invoicenumber = long.Parse(Utilidades.decodpar(Request.QueryString["idf"].ToString()));

            SUPER.DAL.Log.Insertar("VerFactura.aspx.cs " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Factura= "+ invoicenumber);

            sPaso = "2";
            mywsSAP = new wsSAP.z_get_invoice_pdf_b64();
            sPaso = "3";
            wsSAP.ZsdSuperGetFacturaPdfXml oParamIn = new wsSAP.ZsdSuperGetFacturaPdfXml();
            sPaso = "4";
            oParamIn.IdFactura = invoicenumber.ToString();
            sPaso = "5";

            //mywsSAP.Credentials = new System.Net.NetworkCredential("SAP_WSRT", "2013WSRT");
            mywsSAP.Credentials = new System.Net.NetworkCredential("ADMINSAP", "Productivo_14");
            sPaso = "6";
            //throw new Exception("hola");
            mywsSAP.Timeout = 20000;
            System.Threading.Thread.Sleep(2000);
            try
            {
                //Llamo al servicio de SAP que me devuelve el PDF
                oResponse = mywsSAP.ZsdSuperGetFacturaPdfXml(oParamIn);
            }
            catch(Exception err)
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

                    byte[] bytes = Convert.FromBase64String(facturabase64);

                    sPaso = "10";

                    Response.Clear();
                    Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.pdf", invoicenumber.ToString()));
                    Response.ContentType = "application/pdf";

                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Expires", "0");
                    sPaso = "11";
                    Response.BinaryWrite(bytes);
                    sPaso = "12";
                }
                else
                {
                    sPaso = "13";
                    SUPER.DAL.Log.Insertar("VerFactura.aspx.cs(1) sPaso=" + sPaso + " " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Error " + xmldoc.SelectSingleNode("/getinvoice/message").InnerText);
                    ClientScript.RegisterStartupScript(Page.GetType(), "script2", "alert('Error:" + xmldoc.SelectSingleNode("/getinvoice/message").InnerText + "')", true);
                }
            }
        }
        catch (Exception ex)
        {
            string sError = " Error " + ex.Message + " GetType " + ex.GetType().ToString();// + " InnerException " + ex.InnerException.ToString();
            SUPER.DAL.Log.Insertar("VerFactura.aspx.cs(2) sPaso=" + sPaso + " " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + sError);
            //ClientScript.RegisterStartupScript(Page.GetType(), "script2", "alert('Ocurrió un error: " + ex.Message + "')", true);
            ClientScript.RegisterStartupScript(Page.GetType(), "script2", "alert('" + sError + "')", true);
            //ClientScript.RegisterStartupScript(Page.GetType(), "script2", "alert('El servidor SAP no responde en estos momentos. Espera unos segundos y vuelve a intentarlo.')", true);
        }
        try
        {
            System.Threading.Thread.Sleep(5000);
            //stop processing the script and return the current result
            HttpContext.Current.Response.End();
        }
        catch (Exception e1) {
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
    private void Reintentar()
    {
        System.Threading.Thread.Sleep(60000);

        wsSAP.z_get_invoice_pdf_b64 mywsSAP = null;
        wsSAP.ZsdSuperGetFacturaPdfXmlResponse oResponse = null;
        try
        {
            long invoicenumber = long.Parse(Utilidades.decodpar(Request.QueryString["idf"].ToString()));

            SUPER.DAL.Log.Insertar("VerFactura.aspx.cs->Reintentar " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Factura= " + invoicenumber);

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
            SUPER.DAL.Log.Insertar("VerFactura.aspx.cs -> Reintentar " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + sError);
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
