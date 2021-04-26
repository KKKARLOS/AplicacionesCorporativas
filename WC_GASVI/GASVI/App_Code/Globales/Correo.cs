using System;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Collections;
//using System.Runtime.InteropServices;
//using ROBOCOR_CLI;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace GASVI.BLL
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
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", "");// "ADM_GASVI"
            }
        }
        public static void EnviarCorreosCita(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                try
                {
                    EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", sAux[3]);//, "ADM_GASVI"
                }
                catch(Exception e)
                {
                    string sError = Errores.mostrarError("EnviarCorreosCita: fichero= " + sAux[3], e);
                }
            }
        }
        public static void EnviarAviso(string sAsunto, string sTexto, string sTO, string sCC, string sCCO, string sFich)//,string sRemitente
        {
            string webServer = "http://imagenes.intranet.ibermatica/GASVI/";
            #region cuerpo email
            string strTexto = @"<html>
                <style type='text/css'>
				            .titulo
				            {
					            FONT-WEIGHT: bold;
					            FONT-SIZE: 12px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif
				            }
                            .TBLINI
                            {
                                FONT-WEIGHT: bold;
                                FONT-SIZE: 12px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoEncabezamientoListas.gif);
                                COLOR: #ffffff;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .textoResultadoTabla 
                            {
                                BACKGROUND-POSITION: left center;
                                FONT-WEIGHT: normal;
                                FONT-SIZE: 11px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoTotalResListas.gif);
                                VERTICAL-ALIGN: middle;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .check
                            {
                                BACKGROUND-POSITION: center;
                                BACKGROUND-REPEAT: no-repeat;
                                BACKGROUND-IMAGE: url(" + webServer + @"imgOK.gif);
                            }
				            .texto
				            {
					            FONT-WEIGHT: normal;
					            FONT-SIZE: 12px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif;
					            TEXT-DECORATION: none
				            }
                            .FA
                            {
	                            HEIGHT:16px;
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #e6eef2;
                                TEXT-DECORATION: none;
                            }
                            .FB
                            {
	                            HEIGHT:16px;	
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #ffffff;
                                TEXT-DECORATION: none;
                            }
                            @media print and (width: 21cm) and (height: 29.7cm) {}
                            </style>							
            <body bgcolor='#f7fafb' scroll='auto' text='#000000' leftmargin='0' topmargin='0'>
				<table style='FONT-FAMILY: Arial;FONT-SIZE: 12px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
					<tr>
						<td width='132px' bgcolor='#5894AE'><img src='" + webServer + @"imgLogoAplicacion.gif' width='132' height='47' /></td> 
						<td bgcolor='#ffffff' style='text-align:left'><img src='" + webServer + @"bckSinTrainera.gif' width='500' height='47' /></td>
						<td bgcolor='#ffffff' style='text-align:right'><img src='" + webServer + @"logoIbermatica2.gif' width='124' height='33' /></td>
					</tr> 
					<tr><td colspan='3' background='" + webServer + @"imgLineaAzulada.gif'></td></tr>
					<td colspan='3'><br /><blockquote>" + sTexto + @"</blockquote></td>
					<tr><td colspan='3'><br /><br /><blockquote>Para cualquier consulta, póngase en contacto con <a href='mailto:cau-def@ibermatica.com' class='enlace'>CAU-DEF</a></blockquote>
                    <br /><br /><blockquote>Este mensaje no admite respuesta.</blockquote><br />
					<div style='text-align:right'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29' />&nbsp;</div></td>
					</tr>
				</table>
			</body>
			</html>";
            #endregion
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
            {
                sAsunto += "  (" + sTO + ")";
                //sTO = "EDA@ibermatica.com";
                sTO = DestMailDesarrollo();
            }
            if (sTO != "")
            {
                SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO);
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

            string sRteError = GASVI.BLL.Errores.RteMailError();
            string sToError = GASVI.BLL.Errores.DestMailError();
            //formatear los br (el body debe ser xhtml)
            //sTexto = sTexto.Replace("<br />", "<br />");

            sTexto = sTexto.Replace("-", "&#45;");
            sAsunto = sAsunto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>GASVI</App>\r\n");
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

            string certPath = HttpContext.Current.Request.MapPath("~/Certificado");
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "GASVI.pfx", "igueldo"));

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

                //objMail.From = new MailAddress("GASVI@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
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

                //objMail.From = new MailAddress("GASVI@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "GASVI: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
            #endregion
            }
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
    }
}
