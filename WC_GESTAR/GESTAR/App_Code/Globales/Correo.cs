using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ROBOCOR_CLI;
////PARA ROBOCOR2////////////////////////////////////
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.IO;
using System.Configuration;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Correo.
	/// </summary>
	public class Correo
	{
		public Correo()
		{
		}
		public static void Enviar(string strAsunto, string strMensaje, string strTO, string strCC, string strCCO, string strFich, int intBR)
		{
            string webServer = ConfigurationManager.AppSettings["ImagenesCorreo"];
			string strTexto = @"<html>
				<STYLE TYPE='text/css'>
				.TITULO
				{
					FONT-WEIGHT: bold;
					FONT-SIZE: 12px;
					FONT-FAMILY: Arial, Helvetica, sans-serif
				}
				.TEXTO
				{
					FONT-WEIGHT: normal;
					FONT-SIZE: 12px;
					FONT-FAMILY: Arial, Helvetica, sans-serif;
					TEXT-DECORATION: none
				}
			    @media print and (width: 21cm) and (height: 29.7cm) {
			    }
				</STYLE>
				<body bgcolor='#f7fafb' style='overflow-y:auto' text='#000000' leftmargin='0' topmargin='0'>
					<table class='TEXTO' width='100%' border='0' cellspacing='0' cellpadding='0'> 
							<tr>
								<td width='10%'><img src='" + webServer +@"imgLogoAplicacion.gif' width='132px' height='47px'></td> 
								<td width='344px' bgcolor='#ffffff'><img src='"+ webServer +@"bckSinTrainera.gif' width='344px' height='47px'></td>
								<td bgcolor='#ffffff' align='right'  width='100%'><img src='" + webServer + @"logoIbermatica.gif' width='124px' height='33px'></td>
							</tr> 
							<tr>
								<td colspan='3' background='" + webServer +@"imgLineaAzulada.gif'></td>
							</tr>
							<tr>
								<td colspan='3'><br /><br /><br /></td>
							</tr>
							<tr>
								<td colspan='3'><blockquote>"+ strMensaje +@"</blockquote></td>
							</tr>
							<tr><td colspan='3'>";
			
			for (int j=0;j<intBR;j++)
			{
				strTexto += @"<br />";
			}
			
			//strTexto +=	@"<blockquote>Este mensaje no admite respuesta.</blockquote>";
			strTexto +=	@"<br />
							<div align='right'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29'>&nbsp;&nbsp;&nbsp;<br /><br /></div></td>
							</tr>
					</table>
				</body>
			</html>";

            // ojo cuando se comentarice SOLO AL PASAR A EXPLOTACIÓN
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString.ToUpper() == "D")
            {
                strAsunto += "  (" + strTO + ")";
                //strTO = "EDA@ibermatica.com";
                strTO = DestMailDesarrollo();
            }
            //strTO = "DOPEOTCA";
			//ROBOCOR_CLI.cMailWHCliente objMail = new ROBOCOR_CLI.cMailWHClienteClass();
			//objMail.EnviaMailNET("ADM_GESTAR", strAsunto, strTexto, enumTipoMail.etmHTML , strTO, true, strFich, strCC, strCCO);
            SendMailRBC2(strAsunto, strTexto, strTO, strFich, strCC, strCCO);
		}
        private static void SendMailRBC2(string sAsunto, string sTexto, string sTO, string sFich, string sCC, string sCCO)
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();

            string sRteError = RteMailError();
            string sToError = DestMailError();
            //formatear los br (el body debe ser xhtml)
            //sTexto = sTexto.Replace("<br />", "<br />");

            sTexto = sTexto.Replace("-", "&#45;");
            sAsunto = sAsunto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>GESTAR</App>\r\n");
            strb.Append("<RcptTo>" + sTO + "</RcptTo>\r\n");
            strb.Append("<Cc>" + sCC + "</Cc>\r\n");
            strb.Append("<Cco>" + sCCO + "</Cco>\r\n");
            strb.Append("<Subject><!--" + sAsunto + "--></Subject>\r\n");
            strb.Append("<Body><!--" + sTexto + "--></Body>\r\n");
            strb.Append("<BodyFormat>H</BodyFormat>\r\n");
            strb.Append("<Adjuntos>\r\n");

            if (sFich != "")
            {
                strb.Append("<Adjunto>");
                strb.Append("<Nombre>" + new FileInfo(sFich).Name + "</Nombre>");
                strb.Append("<Fichero>" + EncodeFile(sFich) + "</Fichero>");
                strb.Append("</Adjunto>");
            }

            strb.Append("</Adjuntos>\r\n");
            strb.Append("</Email>\r\n");

            svcEmail.svcEmail MySvcEmail = new svcEmail.svcEmail();

            string certPath = System.Web.HttpContext.Current.Request.MapPath("~/Certificado");
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "GESTAR.pfx", "igueldo"));

            string xmlretval = "";
            try
            {
                xmlretval = MySvcEmail.SendMessage(strb.ToString());
            }
            catch (Exception ex)
            {
                #region el servicio ROBOCOR2 está caido. Avisar a EDA por smtp
                MailMessage objMail = new MailMessage();

                string msgex = ex.Message;
                if (ex.InnerException != null) msgex = msgex + " ::: " + ex.InnerException.Message;

                //objMail.From = new MailAddress("GESTAR@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo.", new Exception(msgex));
                #endregion
            }
            finally{}

            //Validar la respuesta
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlretval);

            int errnum = int.Parse(xmldoc.SelectSingleNode("/Datos/Error").InnerText);

            #region Si error de envío...
            if (errnum > 0)
            {
                string errmotivo = "";
                switch (errnum)
                {
                    case 1:
                        errmotivo = "Error general.";
                        break;
                    case 2:
                        errmotivo = "Aplicación no permitida.";
                        break;
                    case 3:
                        errmotivo = "Mensaje mal formado.";
                        break;
                    case 4:
                        errmotivo = "Destinatarios no existentes.";
                        break;
                }

                //error aplicación no permitidida, mal configurada o certificado erroneo.    
                MailMessage objMail = new MailMessage();

                //objMail.From = new MailAddress("gestar@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "GESTAR: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
            }
            #endregion
        }
        private static string EncodeFile(string fichero)
        {
            FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string retval = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
            fs.Close();

            return retval;

        }
        /// <summary>
        /// Obtiene el buzón destinatario de correos de desarrollo
        /// </summary>
        /// <returns></returns>
        public static string DestMailDesarrollo()
        {
            string sTo = "EDA_DES@ibermatica.com";
            try
            {
                sTo = ConfigurationManager.AppSettings["SMTP_to_DES"].ToString();

                if (sTo == "") sTo = "EDA_DES@ibermatica.com";
            }
            catch
            {
                sTo = "EDA_DES@ibermatica.com";
            }
            return sTo;
        }
        /// <summary>
        /// Obtiene el buzón destinatario de correos de error
        /// </summary>
        /// <returns></returns>
        public static string DestMailError()
        {
            string sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            try
            {
                sToError = ConfigurationManager.AppSettings["SMTP_to"].ToString();
                if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                    sToError = ConfigurationManager.AppSettings["SMTP_to_DES"].ToString();

                if (sToError == "") sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            }
            catch
            {
                sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            }
            return sToError;
        }
        /// <summary>
        /// Obtiene el buzón remitente de correos de error
        /// </summary>
        /// <returns></returns>
        public static string RteMailError()
        {
            string sRte = "EDA@ibermatica.com";
            try
            {
                sRte = ConfigurationManager.AppSettings["SMTP_from"].ToString();
                if (sRte == "") sRte = "EDA@ibermatica.com";
            }
            catch
            {
                sRte = "EDA@ibermatica.com";
            }
            return sRte;
        }
    }
}
