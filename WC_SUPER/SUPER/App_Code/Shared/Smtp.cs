using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;


namespace IB.SUPER.Shared
{
    public class Smtp
    {

        public Smtp()
        {
        }

        public static void SendSMTP(string asunto, string mensaje)
        {

            string para = ConfigurationManager.AppSettings["SMTP_to"].ToString();
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
            {
                asunto = "(DESARROLLO) " + asunto;
                para = ConfigurationManager.AppSettings["SMTP_to_DES"].ToString();
            }
            else if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() != "E")
            {
                asunto = "(" + ConfigurationManager.ConnectionStrings["ENTORNO"] + ")" + asunto;
            }

            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"], int.Parse(ConfigurationManager.AppSettings["SMTP_port"]));

                MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTP_from"], para, asunto, mensaje);
                client.Send(mm);
            }
            catch (Exception ex)
            {
            }


        }
    }
}