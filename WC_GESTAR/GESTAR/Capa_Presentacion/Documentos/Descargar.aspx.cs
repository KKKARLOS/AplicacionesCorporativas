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

public partial class Capa_Presentacion_Documentos_Descargar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sTipo = Request.QueryString["sTipo"].ToString();
        int nIDDOC = int.Parse(Request.QueryString["nIDDOC"].ToString());
        string sNombreArchivo = "";
        long? t2_iddocumento = null;
        byte[] ArchivoBinario = null;

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.ContentType = "application/octet-stream";
        try
        {
            switch (sTipo)
            {
                case "A": // AREA
                    DOCAREA oDocA = DOCAREA.Select(null, nIDDOC, true);
                    sNombreArchivo = oDocA.t083_nombrearchivo;
                    t2_iddocumento = oDocA.t2_iddocumento;
                    break;
                case "D": // DEFICIENCIA
                    DOCDEFICIENCIA oDocD = DOCDEFICIENCIA.Select(null, nIDDOC, true);
                    sNombreArchivo = oDocD.t084_nombrearchivo;
                    t2_iddocumento = oDocD.t2_iddocumento;
                    break;
            }

            if (t2_iddocumento != null)
                ArchivoBinario = IB.Conserva.ConservaHelper.ObtenerDocumento((long)t2_iddocumento).content;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", sNombreArchivo));
            //Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            Response.BinaryWrite(ArchivoBinario);

            //if (Response.IsClientConnected) Response.Flush();
            //Response.Close();
            Response.End();
        }
        catch (ConservaException cex)
        {
            this.hdnError.Value = Utilidades.MsgErrorConserva("R", cex);
        }
        //catch (System.Web.HttpException hexc)
        //{
        //}
        catch (Exception ex)
        {
            this.hdnError.Value = "No se ha podido obtener el archivo." + (char)10 + (char)10 + " Error: " + ex.Message + (char)10;
            if (ex.InnerException != null)
                this.hdnError.Value += "Detalle error: " + ex.InnerException.Message;
        }
        //Response.Flush();
        finally
        {
            if (this.hdnError.Value == "")
            {
                Response.Close();
                //Response.End();
            }
        }
    }
}
