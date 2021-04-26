using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;


   public class Messages
    {


       public static string GetMessage(int messageId)
       {
           ResourceManager resmgr = new ResourceManager("CodeGenerator2005.Messages", Assembly.GetExecutingAssembly());
           string msg = resmgr.GetString("msg" + messageId, Application.CurrentCulture);
           return msg;

       }

       public static string ShowErrorMessage(int messageId)
       {
           string msg=GetMessage(messageId);
           MessageBox.Show(msg, "Code Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
           return msg;

       }

       public static string ShowErrorMessage(string message)
       {
           MessageBox.Show(message, "Code Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
           return message;

       }

       public static string ShowMessage(int messageId)
       {
           string msg = GetMessage(messageId);
           MessageBox.Show(msg, "Code Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
           return msg;

       }
       public static string ShowMessage(string message)
       {
           MessageBox.Show(message, "Code Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
           return message;

       }

       public static string ShowErrorMessage(Exception ex)
       {
           MessageBox.Show(ex.ToString(), "Code Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
           return ex.ToString();

       }

    }

