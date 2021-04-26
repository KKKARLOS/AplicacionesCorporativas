using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;


namespace IB.Progress.Shared
{
    public class Smtp
    {
        //private static bool isErrorEnabled = Log.logger.IsErrorEnabled;
        public Smtp()
        {
        }

        public static void SendSMTP(string asunto, string mensaje)
        {
            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "D")
                asunto = "(DESARROLLO) " + asunto;
            try
            {
                //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP_server"], int.Parse(ConfigurationManager.AppSettings["SMTP_port"]));
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP_server"]);
                MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTP_from"],
                                                 //ConfigurationManager.AppSettings["SMTP_to"],
                                                 "eda@ibermatica.com",
                                                 asunto, mensaje);
                client.Send(mm);
            }
            catch (Exception ex)
            {
                //if (isErrorEnabled) Log.logger.Error("SendSMTP :: Error :: " + ex.Message);
            }
        }
    }
}