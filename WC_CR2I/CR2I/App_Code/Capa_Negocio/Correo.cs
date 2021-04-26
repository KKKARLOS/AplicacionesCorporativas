using System;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using ROBOCOR_CLI;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Correo.
	/// </summary>
	public class Correo
	{
		public Correo()
		{
		}

        public static void EnviarCorreos(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", sAux[3], sAux[4], sAux[5]);
            }
        }

        public static void EnviarAviso(string sAsunto, string sTexto, string sTO, string sCC, string sCCO, string sFich, string sDestino, string sRemitente)
		{
			string webServer = "http://imagenes.intranet.ibermatica/CR2I/";

			string strTexto = @"<html>
                            <style>
                            @media print and (width: 21cm) and (height: 29.7cm) {}
                            </style>
							<body bgcolor='#f7fafb' scroll='auto' text='#000000' leftmargin='0' topmargin='0'>
								<table style='FONT-FAMILY: Arial;FONT-SIZE: 12px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
									<tr>
										<td style='width:132px'><img src='" + webServer + @"imgLogoAplicacion.gif' width='132px' height='47px' /></td> 
										<td width='100%' bgcolor='#ffffff'><img src='" + webServer + @"bckSinTrainera.gif' width='344px' height='47px' /></td>
										<td style='width:124px;text-align:right;' bgcolor='#ffffff'><img src='" + webServer + @"logoIbermatica2.gif' width='124px' height='33px' /></td>
									</tr> 
									<tr>
										<td colspan='3' background='" + webServer +@"imgLineaAzulada.gif'></td>
									</tr>
									<tr>
										<td colspan='3'><br /><blockquote>"+ sTexto +@"</blockquote></td>
									</tr>
									<tr>
										<td>&nbsp;&nbsp;&nbsp;</td>
										<td>&nbsp;&nbsp;&nbsp;</td>
										<td>&nbsp;&nbsp;&nbsp;</td>
									</tr>
									<tr><td colspan='3'><br /><br /><br /><br /><br /><br />
									<br /><blockquote>Este mensaje no admite respuesta.</blockquote>
									<br />
									<div style='text-align:right;'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29' />&nbsp;&nbsp;&nbsp;<BR /><BR /></div></td>
									</tr>
								</table>
							</body>
						</html>";

            //Para evitar lios en desarrollo...
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString == "D")
            {
                sAsunto += " (" + sTO + ")";
                //sTO = "EDA@ibermatica.com";
                sTO = DestMailDesarrollo();
                sDestino = "I";  //los correos con @ibermatica.com casca si se envian por smtp
            }

            if (sDestino == "I")
            {
                SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO);
            }
            else
            {//Son correos a externos
                MailMessage objMail = new MailMessage();

                objMail.From = new MailAddress(sRemitente);
                objMail.To.Add(new MailAddress(sTO.Trim()));

                objMail.Subject = sAsunto;
                objMail.IsBodyHtml = true;

                objMail.Body = strTexto;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);
            }
			
			//Si se ha enviado algún fichero adjunto, hay que eliminarlo.
			if (sFich != "")
			{
				File.Delete(sFich);
			}
		}

        private static void SendMailRBC2(string sAsunto, string sTexto, string sTO, string sFich, string sCC, string sCCO)
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            string sRteError = RteMailError();
            string sToError = DestMailError();
            
            sAsunto = sAsunto.Replace("-", "&#45;");
			sTexto = sTexto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>CR2I</App>\r\n");
            strb.Append("<RcptTo>" + sTO  + "</RcptTo>\r\n");
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

            string certPath = HttpContext.Current.Request.MapPath("~/Certificado");
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "CR2I.pfx", "igueldo"));


            string xmlretval = "";
            try
            {
                xmlretval = MySvcEmail.SendMessage(strb.ToString());
            }
            catch (Exception ex)
            {
                //el servicio ROBOCOR2 está caido. Avisar a EDA por smtp
                MailMessage objMail = new MailMessage();

                string msgex = ex.Message;
                if (ex.InnerException != null) msgex = msgex + " ::: " + ex.InnerException.Message;
                
                //objMail.From = new MailAddress("CR2I@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La reserva se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los asistentes.", new Exception(msgex));
            }
            finally
            {
            }


            //Validar la respuesta
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlretval);

            int errnum = int.Parse(xmldoc.SelectSingleNode("/Datos/Error").InnerText);

            //Si error de envío...
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

                //objMail.From = new MailAddress("CR2I@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "CR2I: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La reserva se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
            }

        }

        private static string EncodeFile(string fichero)
        {
            FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string retval =  Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
            fs.Close();

            return retval;

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
    }
}
