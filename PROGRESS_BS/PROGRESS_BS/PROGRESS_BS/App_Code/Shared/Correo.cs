using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;


	
    public class Correo
    {


        private const string _tplEmail1 = @"<html>
                                        <head><title>PROGRESS</title>
                                        <style>
                                        

                                        html{font-family:arial !important;font-size:10pt}
                                        h1
                                        {
                                            text-shadow: 0 1px 0 rgba(255, 255, 255, .7), 0px 2px 0 rgba(0, 0, 0, .5);
                                            text-transform: uppercase;
                                            text-align: left;
                                            color: #666;
                                            margin: 0 0 30px 0;
                                            letter-spacing:4px;                                            
                                            position: relative;
                                        }

                                        table {font-family : verdana !important;}
                                        
                                        #tblfkCorreo{border:1px solid #ccc}
                                     

                                        #tblfkCorreo td span {font-size:9pt !important;}
                                       
                                        #tblfkCorreo tr:nth-child(even){
                                            background-color:#F2F2F2;
                                       }
                                        </style>
                                        </head>
                                        <body bgcolor='#f7fafb' width='1100px' style='overflow:auto' text='#000000' leftmargin='0' topmargin='0'>

                                        <table width='100%' border='0' cellspacing='0' cellpadding='0'> 
							            <tr>
								            <td width='10%'><img src='http://imagenes.ibermatica.com/progress20/logoProgressCorreo.png'></td>                    								            
							            </tr> 
							           
							            <tr>
								            <td colspan='3'><br><br><br></td>
							            </tr>
							            <tr></table>
                                        
                                        <div>
                                            %MENSAJE%
                                        </div>
                                                      
                                        </body>
                                        </html>";



        #region Envio Correo automatico

        public static void Enviar(string strAsunto, string strMensaje, string strTO, string ip)
        {

            string strTexto = _tplEmail1;

            strTexto = strTexto.Replace("%MENSAJE%", strMensaje);
            strTexto = strTexto.Replace("%IP%", ip);

            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "D" || ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "P")
            {
                strAsunto = strAsunto + " (" + strTO + ") correo de PRUEBA";
                strTO = "EDA";
            }

            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "E" && strTO != "")
            {
                SendMailRBC2(strAsunto, strTexto, strTO);
            }
        }


        public static void Enviar(string strAsunto, string strMensaje, string strTO)
        {

            string strTexto = _tplEmail1;

            strTexto = strTexto.Replace("%MENSAJE%", strMensaje);

            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "D" || ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "P")
            {
                
                strAsunto = strAsunto + " (" + strTO + ") correo de PRUEBA";
                //strTO = "EDA_DES";
                strTO = System.Configuration.ConfigurationManager.AppSettings["SMTP_to"].ToString();
                SendMailRBC2(strAsunto, strTexto, strTO);
            }

            else if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "E" && strTO != "")
            {
                SendMailRBC2(strAsunto, strTexto, strTO);
            }
        }



        private static void SendMailRBC2(string sAsunto, string sTexto, string sTO)
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();

            sTexto = sTexto.Replace("-", "&#45;");
            sAsunto = sAsunto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>PROGRESS</App>\r\n");
            strb.Append("<RcptTo>" + sTO + "</RcptTo>\r\n");            
            strb.Append("<Subject><!--" + sAsunto + "--></Subject>\r\n");
            strb.Append("<Body><!--" + sTexto + "--></Body>\r\n");
            strb.Append("<BodyFormat>H</BodyFormat>\r\n");           
            strb.Append("</Email>\r\n");

            svcEmail.svcEmail MySvcEmail = new svcEmail.svcEmail();

            string certPath = HttpContext.Current.Request.MapPath("~/Certificado");
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "RHNET.pfx", "igueldo"));


            string xmlretval = "";
            try
            {
                xmlretval = MySvcEmail.SendMessage(strb.ToString());
            }
            catch (Exception ex)
            {
                //el servicio ROBOCOR2 está caido. Avisar a EDA por smtp
                System.Net.Mail.MailMessage objMail = new System.Net.Mail.MailMessage();

                string msgex = ex.Message;
                if (ex.InnerException != null) msgex = msgex + " ::: " + ex.InnerException.Message;

                objMail.From = new MailAddress("progress@ibermatica.com");
                objMail.To.Add(new MailAddress("EDA@ibermatica.com"));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a las partes implicadas.", new Exception(msgex));
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
                System.Net.Mail.MailMessage objMail = new System.Net.Mail.MailMessage();

                objMail.From = new MailAddress("progress@ibermatica.com");
                objMail.To.Add(new MailAddress("EDA@ibermatica.com"));

                objMail.Subject = "PROGRESS: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
            }

        }

        #endregion

    }