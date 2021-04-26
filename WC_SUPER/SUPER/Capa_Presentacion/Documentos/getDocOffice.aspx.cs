using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.ServiceModel;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_Documentos_getDocOffice : System.Web.UI.Page
{
    string sOpcion = "";//, sError = "";
    byte[] ArchivoBinario = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.ContentType = "application/octet-stream";

        try
        {
            if (Request.QueryString["descargaToken"] != null)
                Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

            if (Request.QueryString["sOp"] != null)
            {
                sOpcion = Utilidades.decodpar(Request.QueryString["sOp"].ToString());

                switch (sOpcion)
                {
                    case "ValorGanado": getExcelValorGanado(); break;
                    case "ControlAbsentismo":
                        getControlAbsentismo(int.Parse(Utilidades.decodpar(Request.QueryString["annomes"].ToString())),
                            Utilidades.decodpar(Request.QueryString["sCentros"].ToString()),
                            Utilidades.decodpar(Request.QueryString["sEvaluadores"].ToString()),
                            Utilidades.decodpar(Request.QueryString["sPSN"].ToString()));
                        break;
                    case "CONSPROYMASIVO": getConsProyMasivo(); break;
                    case "ConsultaPersonalizada": 
                        getConsultaPersonalizada(Utilidades.decodpar(Request.QueryString["par"].ToString())); 
                        break;
                        
                }
            }
        }catch(Exception ex){
            this.hdnError.Value = "Error: " + ex.ToString();
        }

    }

    private void getExcelValorGanado()
    {
        svcSUPER.IsvcSUPERClient osvcSUPER = null;
        try
        {
            int t685_idlineabase = int.Parse(Utilidades.decodpar(Request.QueryString["nLB"].ToString()));
            int t325_anomes_referencia = int.Parse(Utilidades.decodpar(Request.QueryString["nAMR"].ToString()));
            int t305_idproyectosubnodo = int.Parse(Utilidades.decodpar(Request.QueryString["nPSN"].ToString()));
            string t422_idmoneda = Utilidades.decodpar(Request.QueryString["sMoneda"].ToString());
            int nIAP = int.Parse(Utilidades.decodpar(Request.QueryString["nIAP"].ToString()));
            int nEXT = int.Parse(Utilidades.decodpar(Request.QueryString["nEXT"].ToString()));
            int nOCO = int.Parse(Utilidades.decodpar(Request.QueryString["nOCO"].ToString()));
            int nIAPCPI = int.Parse(Utilidades.decodpar(Request.QueryString["nIAPCPI"].ToString()));
            int nEXTCPI = int.Parse(Utilidades.decodpar(Request.QueryString["nEXTCPI"].ToString()));
            int nOCOCPI = int.Parse(Utilidades.decodpar(Request.QueryString["nOCOCPI"].ToString()));
            string sExtension = "xls";
            string sImportesEn = MONEDA.getDenominacionImportes(t422_idmoneda);
            string sNombreArchivo = "DatosLineaBase." + sExtension;

            DataSet ds = LINEABASE.getDataSetParaExcel(
                t685_idlineabase,
                t325_anomes_referencia,
                t305_idproyectosubnodo,
                t422_idmoneda,
                nIAP,
                nEXT,
                nOCO,
                nIAPCPI,
                nEXTCPI,
                nOCOCPI
                );

            osvcSUPER = new svcSUPER.IsvcSUPERClient();
            ArchivoBinario = osvcSUPER.getExcelValorGanado(ds, sExtension,
                t325_anomes_referencia,
                nIAP,
                nEXT,
                nOCO,
                nIAPCPI,
                nEXTCPI,
                nOCOCPI,
                t422_idmoneda,
                sImportesEn);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();

        }
        catch (FaultException<svcSUPER.IBOfficeException> cex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: Código:" + cex.Detail.ErrorCode + ".\nDescripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.InnerException != null)
                this.hdnError.Value += ".\nInnerException: descripción=" + cex.InnerException.Message;

            if (cex.Detail.ErrorCode == 112)
                this.hdnError.Value = "El servidor Office tiene todos sus recursos ocupados.\nPor favor, espere unos minutos e inténtelo de nuevo.\n\n" + this.hdnError.Value;
        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: " + ex.ToString();
        }
        finally
        {
            if (osvcSUPER != null && osvcSUPER.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcSUPER.State != System.ServiceModel.CommunicationState.Faulted) osvcSUPER.Close();
                else if (osvcSUPER.State != System.ServiceModel.CommunicationState.Closed) osvcSUPER.Abort();
            }
        }

    }
    private void getControlAbsentismo(int annomes, string sCentros, string sEvaluadores, string sPSN)
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = "xls";
            string sNombreArchivo = "ControlAbsentismo." + sExtension;

            DataSet ds = null;
            if (HttpContext.Current.Cache.Get("CacheControlAbsentismo_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()) == null)
            {
                ds = SUPER.BLL.Profesional.ObtenerControlAbsentismo(annomes, sCentros, sEvaluadores, sPSN);
            }
            else
            {
                ds = (DataSet)HttpContext.Current.Cache.Get("CacheControlAbsentismo_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
            }

            osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
            ArchivoBinario = osvcEXCEL.getExcelFromDataSet(ds, sExtension);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();

        }
        catch (FaultException<svcEXCEL.IBOfficeException> cex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.InnerException != null)
                this.hdnError.Value += ". InnerException: descripción=" + cex.InnerException.Message;
        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: " + ex.ToString();
        }
        finally
        {
            if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                else if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed) osvcEXCEL.Abort();
            }
        }

    }
    private void getConsProyMasivo()
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = "xls";
            string sNombreArchivo = "ConsumosProyectoMasivo." + sExtension;

            DataSet ds = (DataSet)HttpContext.Current.Cache.Get("CONSPROYMASIVO_" + Session["IDFICEPI_ENTRADA"].ToString());

            osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
            ArchivoBinario = osvcEXCEL.getExcelFromDataSet(ds, sExtension);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.BinaryWrite(ArchivoBinario);

            ds.Dispose();
            HttpContext.Current.Cache.Remove("CONSPROYMASIVO_" + Session["IDFICEPI_ENTRADA"].ToString());

            if (Response.IsClientConnected)
                Response.Flush();

        }
        catch (FaultException<svcEXCEL.IBOfficeException> cex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.InnerException != null)
                this.hdnError.Value += ". InnerException: descripción=" + cex.InnerException.Message;
        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: " + ex.ToString();
        }
        finally
        {
            HttpContext.Current.Cache.Remove("CONSPROYMASIVO_" + Session["IDFICEPI_ENTRADA"].ToString());

            if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                else if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed) osvcEXCEL.Abort();
            }
        }

    }

    private void getConsultaPersonalizada(string sParametros)
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = "xls";
            string[] aParam = Regex.Split(sParametros, "@#@");
            //string sNombreArchivo = SUPER.Capa_Negocio.Utilidades.unescape(aParam[0]) + "_" + DateTime.Now.ToString() + "." + sExtension;
            string sNombreArchivo = aParam[0] + "-" + DateTime.Now.ToString() + "." + sExtension;
            string sProdAlm = aParam[1];
            string[] aParametros = Regex.Split(aParam[2], "///");
            //object[] aObjetos = new object[(sParametros == "") ? 1 : aParametros.Length + 1];
            object[] aObjetos = new object[(aParam[2] == "") ? 1 : aParametros.Length + 1];
            aObjetos[0] = Utilidades.GetUserActual();
            #region Cargo parámetros
            int v = 1;
            foreach (string oParametro in aParametros)
            {
                if (oParametro == "") continue;
                string[] aDatos = Regex.Split(oParametro, "##");
                switch (aDatos[0])
                {
                    case "A": aObjetos[v] = int.Parse(aDatos[1]); break;
                    case "M": aObjetos[v] = double.Parse(aDatos[1].Replace(".", ",")); break;
                    case "B": aObjetos[v] = (aDatos[1] == "1") ? true : false; break;
                    default: aObjetos[v] = aDatos[1]; break;
                }
                v++;
            }
            #endregion
            DataSet ds = CONSULTAPERSONAL.EjecutarConsultaDS(sProdAlm, aObjetos);

            osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
            ArchivoBinario = osvcEXCEL.getExcelFromDataSet(ds, sExtension);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Utilidades.CleanFileName(sNombreArchivo) + "\"");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();

        }
        catch (FaultException<svcEXCEL.IBOfficeException> cex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.InnerException != null)
                this.hdnError.Value += ". InnerException: descripción=" + cex.InnerException.Message;
        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            this.hdnError.Value = "Error: " + ex.ToString();
        }
        finally
        {
            if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                else if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed) osvcEXCEL.Abort();
            }
        }

    }
}
