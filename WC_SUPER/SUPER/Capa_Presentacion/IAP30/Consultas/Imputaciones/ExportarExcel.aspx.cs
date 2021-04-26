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

public partial class Capa_Presentacion_Consulta_Imputaciones : System.Web.UI.Page
{
    byte[] ArchivoBinario = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string prefijo = Constantes.sPrefijo;
        DateTime dFDesde, dFHasta;
        string sPSN;
        int iUsuario;
        DataSet ds = null;

        try{

            dFDesde = DateTime.Parse(Request.QueryString["sFechaDesde"].ToString());
            dFHasta = DateTime.Parse(Request.QueryString["sFechaHasta"].ToString());
            sPSN = Request.QueryString["sPSN"].ToString();
            iUsuario = int.Parse(Request.QueryString["sUsuario"].ToString());

            if (HttpContext.Current.Cache["Consulta_Imputaciones_" + Session["IDFICEPI_IAP"].ToString()] != null)
                ds = (DataSet)HttpContext.Current.Cache["Consulta_Imputaciones_" + Session["IDFICEPI_IAP"].ToString()];
            else
            {
                IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP();
                try
                {
                    ds = cConsumoTecnicoIAP.ExportarExcel(iUsuario, (sPSN == "") ? null : (int?)int.Parse(sPSN), dFDesde, dFHasta);
                }
                catch (Exception ex)
                {
                    if (cConsumoTecnicoIAP != null) cConsumoTecnicoIAP.Dispose();
                    this.hdnError.Value = Errores.mostrarError("Error al cargar los datos", ex);
                }
                finally
                {
                    cConsumoTecnicoIAP.Dispose();
                }
            }

            //if (Request.QueryString["descargaToken"] != null)
            //    Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field
            
            //ds.Tables[0].Columns.Remove(ds.Tables[0].Columns[0]);
            getExcel(ds);
        }
        catch (Exception ex)
        {
            this.hdnError.Value = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    private void getExcel(DataSet ds)
    {
        svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
        try
        {
            string sExtension = ".xls";
            string sNombreArchivo = "Consulta_Imputaciones_" + Session["IDFICEPI_IAP"].ToString() + sExtension;

            osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
            ArchivoBinario = osvcEXCEL.getExcelFromDataSet(ds, sExtension);

            Response.ContentType = "application/xlsx; charset=ISO-8859-1";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.AddHeader("content-length", Convert.ToString(ArchivoBinario.Length));
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
