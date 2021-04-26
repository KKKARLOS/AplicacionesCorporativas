using IB.Progress.Shared;
using System;
using System.Collections;


public partial class private_uc_FileDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            byte[] oArchivo = null;
            long? iddocumento = null; // Identificador del documento en ATENEA
            IB.Conserva.svcConserva.CSVDocument oDoc = new IB.Conserva.svcConserva.CSVDocument();

            Hashtable ht = Utils.ParseQuerystring(Utils.unescape(Request.QueryString.ToString()));
            iddocumento = ht["d"] != null ? (long?) long.Parse(ht["d"].ToString()) : null;


            if (iddocumento != null)
            {
                oDoc = IB.Conserva.ConservaHelper.ObtenerDocumento((long)iddocumento);
                oArchivo = oDoc.content;
            }

            if (oArchivo != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = ContentType(oDoc.docName);
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDoc.docName + "\"");
                Response.BinaryWrite(oArchivo);

                Response.Flush();
                //Response.Close();
                //Response.End();
                //Javi 18.05.2015 Evitar que se produzca error con el response.end
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }


        }
        catch (ConservaException cex)
        {
            hdnError.Value = cex.Message;
        }
        catch (Exception ex)  //Javi 18.05.2015 Controlar excepcion general
        {
            hdnError.Value = "Error al descargar el documento.<br /><br />" + ex.Message;
        }
    }

    private string ContentType(string fileName)
    {
        int pos = 0;
        string ext;
        pos = fileName.LastIndexOf(".");
        ext = fileName.Substring(pos + 1, fileName.Length - pos - 1);

        switch (ext)
        {
            case "txt":
                return "text/plain";
            case "doc":
                return "application/ms-word";
            case "xls":
                return "application/vnd.ms-excel";
            case "gif":
                return "image/gif";
            case "jpg":
            case "jpeg":
                return "image/jpeg";
            case "bmp":
                return "image/bmp";
            case "wav":
                return "audio/wav";
            case "ppt":
                return "application/mspowerpoint";
            case "dwg":
                return "image/vnd.dwg";
            case "pdf":
                return "application/pdf";
            default:
                return "application/octet-stream";
        }
    }
}
 