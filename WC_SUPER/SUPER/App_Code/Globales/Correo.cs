using System;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Collections;
//Para el stringbuilder
using System.Text;
//Para el RegEx
using System.Text.RegularExpressions;
//using ROBOCOR_CLI;
//using System.Runtime.InteropServices;
////PARA ROBOCOR2////////////////////////////////////
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
/////////////////////////////////////////////////////
using System.IO;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Configuration;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de Correo.
    /// </summary>
    public class Correo
    {
        //private cLog mLog;
        public Correo()
        {
		
        }
        public static void EnviarCorreos(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", "", true, 1);//, "ADM_SUPER"
            }
        }
        /// <summary>
        /// A la hora de enviar correos tiene en cuenta si se debe enviar la línea en el cuerpo del correo donde se indica el mail 
        /// de contacto con el CAU
        /// </summary>
        /// <param name="aListCorreo"></param>
        public static void EnviarCorreosContacto(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", "", (sAux[3] == "S") ? true : false, 1);//, "ADM_SUPER"
            }
        }
        public static void EnviarCorreosCita(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                try
                {
                    EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", sAux[3], true, 1);//, "ADM_SUPER"
                }
                catch(Exception e)
                {
                    string sError = Errores.mostrarError("EnviarCorreosCita: fichero= " + sAux[3], e);
                }
            }
        }

        public static void EnviarCorreosCVT(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                //try
                //{
                    EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", sAux[3], true, 2);//, "ADM_SUPER"
                //}
                //catch (Exception e)
                //{
                //    string sError = Errores.mostrarError("EnviarCorreosCVT: fichero= " + sAux[3], e);
                //}
            }
        }

        public static void EnviarCorreosCAUDEF(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                try
                {
                    EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", sAux[3], false, 1);
                }
                catch (Exception e)
                {
                    string sError = Errores.mostrarError("EnviarCorreosCAUDEF: fichero= " + sAux[3], e);
                }
            }
        }

        public static void EnviarCorreosCert(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];                
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", "", true, 3);              
                
            }
        }

        public static void EnviarAviso(string sAsunto, string sTexto, string sTO, string sCC, string sCCO, string sFich, bool bMostrarLineaContacto, byte idContacto)//, string sRemitente
        {
            //string webServer = "http://imagenes.intranet.ibermatica/SUPERNET/";
            string webServer = System.Configuration.ConfigurationManager.AppSettings["ImagenesCorreo"];
            #region HTML mail
            string sTextoContacto = "";

            if (bMostrarLineaContacto)
            {
                switch (idContacto)
                {
                    case 1:
                        sTextoContacto = "<br><br><blockquote>Para cualquier consulta, póngase en contacto con <a href='mailto:cau-def@ibermatica.com' class='enlace'>CAU-DEF</a></blockquote>";
                        break;
                    case 2:
                        sTextoContacto = "<br><br><blockquote>Para cualquier consulta, envíe un correo a la cuenta <a href='mailto:cvt@ibermatica.com' class='enlace'>CVT</a></blockquote>";
                        break;
                    case 3:
                        sTextoContacto = "<br><br><blockquote>Para cualquier consulta, envíe un correo a la cuenta <a href='mailto:Certificaciones@ibermatica.com' class='enlace'>Certificaciones</a></blockquote>";
                        break;
                }                
                
            }
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
                            .punteado
                            {
                                BACKGROUND-IMAGE: url(" + webServer + @"punteado.gif);
                            }

                            @media print and (width: 21cm) and (height: 29.7cm) {}
                            </style>							
            <body bgcolor='#f7fafb' scroll='auto' text='#000000' leftmargin='0' topmargin='0'>
				<table style='FONT-FAMILY: Arial;FONT-SIZE: 12px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
					<tr>
						<td width='132px' bgcolor='#5894AE'><img src='" + webServer + @"imgLogoAplicacion.gif' width='132' height='47' /></td> 
						<td bgcolor='#ffffff' align='left'><img src='" + webServer + @"bckSinTrainera.gif' width='500' height='47' /></td>
						<td bgcolor='#ffffff' align='right'><img src='" + webServer + @"logoIbermatica2.gif' width='124' height='33' /></td>
					</tr> 
					<tr><td colspan='3' background='" + webServer + @"imgLineaAzulada.gif'></td></tr>
					<tr><td colspan='3'><br><blockquote>" + sTexto + @"</blockquote></td></tr>
					<tr><td colspan='3'>" + sTextoContacto +
                    @"<br><br><blockquote>Este mensaje no admite respuesta.</blockquote><br>
					<div align='right'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29' />&nbsp;</div></td>
					</tr>
				</table>
			</body>
			</html>";
            #endregion
            #region Establecer destinatarios
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
            {
                sAsunto += "  (" + sTO + ")";
                //sTO = "EDA_DES@ibermatica.com";
                sTO = DestMailDesarrollo();
            }
            else
            {
                //Elimino de los destinatarios aquellos usuarios que se han configurado para no recibir mails de SUPER (t314_recibirmails=0)
                //string sAux = sTO.Replace(@"/", ",");
                string sAux = sTO;
                SqlDataReader dr = USUARIO.GetUsuariosReceptores(sAux);
                sAux = "";
                while (dr.Read())
                {
                    sAux += @";" + dr["codred"].ToString();
                }
                dr.Close();
                dr.Dispose();
                if (sAux != "")
                    sTO = sAux.Substring(1, sAux.Length - 1);
                else
                    sTO = "";
            }

            #endregion
            #region Enviar
            if (sTO != "")
            {
                SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO);
            }
            #endregion
        }
    
        private static void SendMailRBC2(string sAsunto, string sTexto, string sTO, string sFich, string sCC, string sCCO)
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            string sRteError = SUPER.Capa_Negocio.Errores.RteMailError();
            string sToError = SUPER.Capa_Negocio.Errores.DestMailError();

            //sTO = sTO.Replace(@"/",";");
            //formatear los br (el body debe ser xhtml)
            //sTexto = sTexto.Replace("<br />", "<br />");

            sTexto = sTexto.Replace("-", "&#45;");
            sAsunto = sAsunto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>SUPER</App>\r\n");
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

            //string certPath = System.Web.HttpContext.Current.Request.MapPath("~/Certificado");
            string certPath = ConfigurationManager.AppSettings["CertificadoPath"].ToString();
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "SUPER.pfx", "igueldo"));


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

                //objMail.From = new MailAddress("SUPER@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo.", new Exception(msgex));
            }
            finally
            {
                //11/11/2014: No se borran ficheros. Se pasará un proceso nocturno que borre lo que proceda.
                //if (sFich != "")
                //{
                //    //Si se ha enviado algún fichero adjunto, hay que eliminarlo.
                //    try
                //    {
                //        File.Delete(sFich);
                //    }
                //    catch { }
                //}
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

                //objMail.From = new MailAddress("SUPER@ibermatica.com");
                //objMail.To.Add(new MailAddress("EDA@ibermatica.com"));
                objMail.From = new MailAddress(sRteError);
                objMail.To.Add(new MailAddress(sToError));

                objMail.Subject = "SUPER: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
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
        /// Correos de petición de borrado en CVT
        /// </summary>
        /// <param name="sDatosCorreo"></param>
        /// <param name="sMotivo"></param>
        /// <returns></returns>

        public static string EnviarPetBorrado(string sTipo, string sDatosCorreo, string sMotivo)
        {
            string sResul = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sbuilder = new StringBuilder();
            string sAsunto = "";
            string sTexto = "", sTexto1="";
            string sTO = "";

            try
            {
                sTO = System.Configuration.ConfigurationManager.AppSettings["CorreoFormacion"].ToString();
                sTexto1 = "Os informamos de que han solicitado eliminar el siguiente registro de un CV:";
                switch (sTipo)
                {
                    case "FR"://formación recibida
                        sAsunto = "Petición de borrado de una acción formativa recibida y existente por En Forma.";
                        break;
                    case "FI"://formación impartida
                        sAsunto = "Petición de borrado de una acción formativa impartida y existente por En Forma.";
                        sTexto1 = "Os informamos de que han solicitado eliminar el siguiente registro de un CV, como monitor interno: ";
                        break;
                    case "EI"://Experiencia profesional en Ibermática
                        sAsunto = "Petición de borrado de una experiencia profesional en Ibermática.";
                        break;
                    case "PE"://Perfil de Experiencia profesional
                        sAsunto = "Petición de borrado de un perfil de una experiencia profesional.";
                        break;
                    case "EX"://Examen
                        sAsunto = "Petición de borrado de un examen validado.";
                        sTO = System.Configuration.ConfigurationManager.AppSettings["CorreoCertificaciones"].ToString();
                        break;
                    case "CE"://Certificados
                        sAsunto = "Petición de borrado de un certificado validado.";
                        sTO = System.Configuration.ConfigurationManager.AppSettings["CorreoCertificaciones"].ToString();
                        break;
                }

                string[] aDatos = Regex.Split(sDatosCorreo, "#/#");
                sbuilder.Append(@"<BR>" + sTexto1 + "<BR><BR>");
                sbuilder.Append("<label style='width:140px'><b>Solicitante: </b></label>" + aDatos[0] + "<br />");
                sbuilder.Append("<label style='width:140px'><b>CV del Profesional: </b></label>" + aDatos[1] + "<br />");
                sbuilder.Append("<label style='width:140px'><b>Apartado del CV: </b></label>" + aDatos[2] + "<br />");
                sbuilder.Append("<label style='width:140px'><b>Registro a eliminar: </b></label>" + aDatos[3] + "<br />");
                sbuilder.Append("<label style='width:400px'><b>Motivo por el que se solicita la eliminación: </b></label><br />" + sMotivo + "<br />");
                
                if (sTO != "")
                {
                    //sTO = sTO.Replace(";", @"/");
                    sTexto = sbuilder.ToString();
                    //El último parámetro indica que no se debe mostrar la línea del cuerpo del mensaje
                    string[] aMail = { sAsunto, sTexto, sTO, "N" };
                    aListCorreo.Add(aMail);

                    Correo.EnviarCorreosContacto(aListCorreo);
                }
                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de petición de borrado.", ex);
            }
            return sResul;
        }
        public static string EnviarPetDenegada(string sTipo, string sTO, string sCliente, string sElemento, string sMotivo, string sElemento2)
        {
            string sResul = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sbuilder = new StringBuilder();
            string sAsunto = "";
            string sTexto = "";

            try
            {
                switch (sTipo)
                {
                    case "E"://Experiencia profesional en Ibermática
                        sAsunto = "Petición de borrado de una experiencia ligada a un proyecto Super.";
                        sbuilder.Append(@"<BR>Se ha rechazado tu sugerencia de eliminar de tu CV la siguiente experiencia profesional:<BR><BR>");
                        sbuilder.Append("<label style='width:120px'><b>Experiencia: </b></label>" + sElemento + "<br />"); 
                        break;
                    case "P"://Perfil de Experiencia profesional
                        sAsunto = "Petición de borrado de un perfil de una experiencia ligada a un proyecto Super.";
                        sbuilder.Append(@"<BR>Se ha rechazado tu sugerencia de eliminar de tu CV el siguiente perfil de la experiencia profesional:<BR><BR>");
                        sbuilder.Append("<label style='width:120px'><b>Experiencia: </b></label>" + sElemento + "<br />");
                        sbuilder.Append("<label style='width:120px'><b>Perfil: </b></label>" + sElemento2 + "<br />");
                        break;
                }
                sbuilder.Append("<label style='width:120px'><b>Cliente: </b></label>" + sCliente + "<br /><br />");
                sbuilder.Append("<label style='width:400px'><b>Motivo por el que no se ha eliminado: </b></label><br />" + sMotivo + "<br />");

                if (sTO != "")
                {
                    //sTO = sTO.Replace(";", @"/");
                    sTexto = sbuilder.ToString();

                    string[] aMail = { sAsunto, sTexto, sTO, "N" };
                    aListCorreo.Add(aMail);

                    Correo.EnviarCorreosContacto(aListCorreo);
                }
                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de denegación de petición de borrado.", ex);
            }
            return sResul;
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
