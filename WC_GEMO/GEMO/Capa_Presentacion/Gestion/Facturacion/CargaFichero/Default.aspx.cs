using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
//Para probar la impersonación (para crear ficheros en ESCAFIX)
using System.Security.Permissions;
using GEMO.Security;
//Para el código de agregar usuario local
//using System.DirectoryServices;
//Para el codigo que displaya quien es el usuario actual
using System.Security.Principal;

using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using GEMO.BLL;
using Microsoft.JScript;
//Para el StreamReader
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sb = new StringBuilder();

    private string strFileName, strFileNamePath; //, strFileFolder;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.nBotonera = 0;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Carga del fichero de facturación";

            HttpPostedFile selectedFile = this.uplTheFile.PostedFile;
            if (selectedFile != null)
            {
                string fileExt = System.IO.Path.GetExtension(selectedFile.FileName);
                if (fileExt.ToUpper() == ".MDB")
                {
                    try
                    {
                        GuardarFicheroEnRepositorio(selectedFile);
                        if (sErrores == "") ControlarCargaFacturacion();
                        if (sErrores == "") ActualizarTablasBD();
                        if (sErrores == "") EnviarCorreoControladores();
                    }
                    catch (Exception x)
                    {
                        sErrores = "Error producido al enviar el fichero al servidor. " + x.Message;
                    }
                }
                else
                {
                    panel.Visible = false;
                    sErrores = "La extensión del fichero no es '.MDB' ";
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    private string EnviarCorreoControladores()
    {
        try
        {
            if (GEMO.BLL.FACTURACION.Correo(strFileName)==false) sErrores = "No hay controladores a los que notificar la carga del fichero de facturación. ";
        }
        catch (Exception x)
        {
            sErrores = "Error producido al intentar actualizar las tablas en base de datos. " + x.Message;
        }
        return sErrores;
    }
    private string GuardarFicheroEnRepositorio(HttpPostedFile selectedFile)
    {
        //Impersonator iPerson = null;

        //strFileFolder = Context.Server.MapPath(@"datos\");

        strFileName = selectedFile.FileName;

        strFileName = Path.GetFileName(strFileName);

        if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
            strFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_DES"] + strFileName;
        else
            strFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_EXP"] + strFileName;

        //if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() != "D")
        //{
        //    iPerson = new Impersonator("descafi", "IBWEB01", "Descafi123");
        //    iPerson.Impersonate();
        //}

        //strFileNamePath = strFileFolder + strFileName;
        try
        {
            if (System.IO.File.Exists(strFileNamePath)) System.IO.File.Delete(strFileNamePath);
        }
        catch (Exception x)
        {
            //if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() != "D")
            //{
            //    if (iPerson != null) iPerson.Undo();
            //}
            sErrores = "Error producido al intentar borrar el fichero en el servidor. " + x.Message;
            return sErrores;
        }

        try
        {
            selectedFile.SaveAs(strFileNamePath);
            //if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() != "D")
            //{
            //    if (iPerson != null) iPerson.Undo();
            //}
        }
        catch (Exception x)
        {
            //if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() != "D")
            //{
            //    if (iPerson != null) iPerson.Undo();
            //}
            sErrores = "Error producido al intentar guardar el fichero" + strFileNamePath + " en el servidor. " + x.Message;
            return sErrores;
        }

        lblFileName.Text = strFileName;
        lblFileLength.Text = int.Parse(selectedFile.ContentLength.ToString()).ToString("N");
        lblFileType.Text = selectedFile.ContentType;
        panel.Visible = true;
        return sErrores;
    }
    private string ControlarCargaFacturacion()
    {
        try
        {
            sErrores = GEMO.BLL.FACTURACION.Control(strFileName);
            return sErrores;
        }
        catch (Exception x)
        {
            sErrores = "Error al cargar el fichero de facturación. " + x.Message;
            return sErrores;
        }
    }
    private string ActualizarTablasBD()
    {
        try
        {
            GEMO.BLL.FACTURACION.Fichero(strFileName);
         //   if (System.IO.File.Exists(strFileNamePath)) System.IO.File.Delete(strFileNamePath);
            return sErrores;
        }
        catch (Exception x)
        {
            sErrores = "Error producido al intentar actualizar las tablas en base de datos. " + x.Message;
            return sErrores;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        switch (aArgs[0])
        {
            case ("procesar"):
                //sResultado += Procesar();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
}

