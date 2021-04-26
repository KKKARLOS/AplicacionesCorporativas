using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Pruebas_Navegador_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string s = "Browser Capabilities<br>"
            + "Type = " + browser.Type + "<br>"
            + "Name = " + browser.Browser + "<br>"
            + "Version = " + browser.Version + "<br>"
            + "Major Version = " + browser.MajorVersion + "<br>"
            + "Minor Version = " + browser.MinorVersion + "<br>"
            + "Platform = " + browser.Platform + "<br>"
            + "Is Beta = " + browser.Beta + "<br>"
            + "Is Crawler = " + browser.Crawler + "<br>"
            + "Is AOL = " + browser.AOL + "<br>"
            + "Is Win16 = " + browser.Win16 + "<br>"
            + "Is Win32 = " + browser.Win32 + "<br>"
            + "Supports Frames = " + browser.Frames + "<br>"
            + "Supports Tables = " + browser.Tables + "<br>"
            + "Supports Cookies = " + browser.Cookies + "<br>"
            + "Supports VBScript = " + browser.VBScript + "<br>"
            + "Supports JavaScript = " +
                browser.EcmaScriptVersion.ToString() + "<br>"
            + "Supports Java Applets = " + browser.JavaApplets + "<br>"
            + "Supports ActiveX Controls = " + browser.ActiveXControls
                  + "<br>"
            + "Supports JavaScript Version = " + browser["JavaScriptVersion"] + "<br>"
            + "User Agent = " + Request.ServerVariables["http_user_agent"] + "<br>";
        Label1.Text = s;
    }
}
