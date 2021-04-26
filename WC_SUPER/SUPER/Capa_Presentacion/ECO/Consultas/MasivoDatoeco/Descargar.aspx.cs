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
    string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
            DataSet ds = (DataSet)HttpContext.Current.Cache["MasivoDatoEco_" + Session["IDFICEPI_ENTRADA"].ToString()];

            if (Request.QueryString["descargaToken"] != null)
                Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

            getExcel(ds);
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    private void getExcel(DataSet ds)
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = ".xls";
            string sNombreArchivo = "Consulta_masiva" + sExtension;

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
