using IB.Progress.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Home_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        try
        {
            valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            List<VALORACIONESPROGRESS.TemporadaProgress> misvaloraciones = valoracionesBLL.TemporadaProgress();

            anyoactual.InnerText = misvaloraciones[0].Temporada.ToString();
            txtPeriodo.InnerText = misvaloraciones[0].periodoprogress.ToString();
            
            valoracionesBLL.Dispose();

        }
        catch (Exception ex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener la temporada Progress", ex.Message);
            throw ex;
        }
    }

    /// <summary>
    /// Envío de correo a 
    /// </summary>
    /// <param name="texto"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void enviarCorreo(string texto)
    {
        try
        {            
            StringBuilder sb = new StringBuilder();

            sb.Append(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha enviado a Oficina Técnica un mensaje.</br></br>");
            sb.Append("<span style='text-decoration:underline'>Texto:</span></br></br>");
            sb.Append(IB.Progress.Shared.Utils.decodpar(texto));

            Correo.Enviar("PROGRESS: Correo a Oficina Técnica", sb.ToString(), ConfigurationManager.AppSettings["SMTP_to_OTRRHH"]);            
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}