using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Consulta_Descargar : System.Web.UI.Page
{
    byte[] ArchivoBinario = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string prefijo = Constantes.sPrefijo;
        DateTime dFInicio, dFFin;
        string sProfesionales = "", sIdEstructura = "";
        bool bMiEquipoDirecto = false, bTareasFueraPlazoSinHacer = false;
        int nTareasVencidas = 0;

        DataSet ds = null;

        try{
            dFInicio = DateTime.Parse(Request.Form[prefijo + "txtFechaInicio"].ToString());
            dFFin = DateTime.Parse(Request.Form[prefijo + "txtFechaFin"].ToString());
            sProfesionales = Request.Form[prefijo + "hdnProfesionales"].ToString();
            sIdEstructura = Request.Form[prefijo + "hdnIdEstructura"].ToString();
            bMiEquipoDirecto = (Request.Form[prefijo + "chkMiEquipoDirecto"] == "on") ? true : false;
            bTareasFueraPlazoSinHacer = (Request.Form[prefijo + "chkTareasFueraPlazoSinHacer"] == "on") ? true : false;
            nTareasVencidas = int.Parse(Request.Form[prefijo + "txtTareasVencidas"].ToString());

            if (HttpContext.Current.Cache["Incumplimientos_MiEquipo_" + Session["IDFICEPI_CVT_ACTUAL"].ToString()] != null)
                ds = (DataSet)HttpContext.Current.Cache["Incumplimientos_MiEquipo_" + Session["IDFICEPI_CVT_ACTUAL"].ToString()];
            else
                ds = Incumplimientos.DeMiEquipo(null, (int)Session["IDFICEPI_CVT_ACTUAL"], dFInicio, dFFin,
                    sProfesionales, sIdEstructura, nTareasVencidas, bMiEquipoDirecto, bTareasFueraPlazoSinHacer);

            if (Request.QueryString["descargaToken"] != null)
                Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns[4]);
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns[3]);
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns[2]);
            getExcel(ds);
        }
        catch (Exception ex)
        {
            this.hdnErrores.Value = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    private void getExcel(DataSet ds)
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = ".xls";
            string sNombreArchivo = "Incumplimientos_Mi_Equipo_" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + sExtension;

            osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
            ArchivoBinario = osvcEXCEL.getExcelFromDataSet(ds, sExtension);
            Response.ContentType = "application/xlsx";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.AddHeader("content-length", Convert.ToString(ArchivoBinario.Length));
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();

        }
        catch (FaultException<svcEXCEL.IBOfficeException> cex)
        {
            Response.ContentType = "text/HTML";
            this.hdnErrores.Value = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.InnerException != null)
                this.hdnErrores.Value += ". InnerException: descripción=" + cex.InnerException.Message;
        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            this.hdnErrores.Value = "Error: " + ex.ToString();
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
