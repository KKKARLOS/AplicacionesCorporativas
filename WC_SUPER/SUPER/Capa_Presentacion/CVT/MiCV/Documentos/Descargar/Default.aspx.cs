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

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        byte[] ArchivoBinario = null;
        long? t2_iddocumento = null;
        try
        {

            int nIDDOC = int.Parse(Request.QueryString["nIDDOC"].ToString());

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "application/octet-stream";

            DOCUIDFICEPI oDoc = DOCUIDFICEPI.Select(null, nIDDOC);

            t2_iddocumento = oDoc.t2_iddocumento;
            if (t2_iddocumento != null)
                ArchivoBinario = IB.Conserva.ConservaHelper.ObtenerDocumento((long)t2_iddocumento).content;

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDoc.t184_nombrearchivo + "\"");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();
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
            this.hdnError.Value = "No se ha podido obtener el archivo.<br /><br />Error: " + ex.Message;
            if (ex.InnerException != null)
                this.hdnError.Value += "<br />Detalle error: " + ex.InnerException.Message;
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
