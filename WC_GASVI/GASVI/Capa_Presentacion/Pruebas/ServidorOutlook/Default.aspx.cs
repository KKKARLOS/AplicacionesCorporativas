using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using GASVI.BLL;
using System.Net.Mail;

public partial class Capa_Presentacion_Pruebas_ServidorOutlook_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MailMessage objMail = new MailMessage();

            objMail.From = new MailAddress("GASVI@ibermatica.com");
            objMail.To.Add(new MailAddress("EDA@ibermatica.com"));

            objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
            objMail.IsBodyHtml = false;

            objMail.Body = "Esto es una prueba";
            SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
            myClient.Send(objMail);

            Master.bFuncionesLocales = true;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al enviar el correo", ex);
        }

    }
}
