using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using SUPER.Capa_Negocio;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Text;

public partial class _Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        //FileDocumento.UploadedComplete += new EventHandler<AsyncFileUploadEventArgs>(FileDocumento_UploadedComplete);
        //FileDocumento.UploadedFileError += new EventHandler<AsyncFileUploadEventArgs>(FileDocumento_UploadedFileError);
        FileDocumento.Style.Add("visibility", "hidden");

        if (!Page.IsCallback)
        {
            try
            {
                if (Request.QueryString["ID"] != null) hdnID.Text = Utilidades.decodpar(Request.QueryString["ID"].ToString());
                if (Request.QueryString["sAccion"] != null) hdnAccion.Text = Utilidades.decodpar(Request.QueryString["sAccion"].ToString());
                if (hdnAccion.Text == "U")
                {
                    DOCUIDFICEPI oDocu = DOCUIDFICEPI.Select(null, int.Parse(hdnID.Text));
                    txtDescrip.Text = oDocu.t184_descripcion;
                    this.hdnContentServer.Value = oDocu.t2_iddocumento.ToString();
                    this.hdnNomArchivo.Value = oDocu.t184_nombrearchivo.ToString();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    protected void FileDocumento_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {
        try
        {
            if (FileDocumento.HasFile)
            {
                string strFichero = Server.MapPath(".") + @"\" + Path.GetFileName(e.FileName);
                /*
                 
                FileDocumento.SaveAs(strFichero);
                FileStream fs = new FileStream(strFichero, FileMode.Open, FileAccess.Read);

                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Session["NOMBRE_ARCHIVO"] = Path.GetFileName(e.filename);
                Session["ARCHIVO"] = filebytes;

                */
                HttpPostedFile selectedFile = FileDocumento.PostedFile;

                byte[] ArchivoEnBinario = new Byte[0];
                if (selectedFile.ContentLength != 0)
                {
                    string sFichero = selectedFile.FileName;
                    //Grabo el archivo en base de datos
                    ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                    selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array
                }

                Session["NOMBRE_ARCHIVO"] = Path.GetFileName(e.FileName);
                Session["ARCHIVO"] = ArchivoEnBinario;
            }
            else
            {
                Session["NOMBRE_ARCHIVO"] = null;
                Session["ARCHIVO"] = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "size", "top.$get(\"" + lblStatus.ClientID + "\").innerHTML = 'Se ha producido un error procesando la petición  : " + ex.Message.ToString() + "';", true);
        }
    }

    protected void FileDocumento_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
    {
        string sError="";
        if (e.StatusMessage == "The file attached is empty.") sError = "El fichero no puede estar vacío.";
        else sError = e.StatusMessage;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "size", "top.$get(\"" + lblStatus.ClientID + "\").innerHTML = 'Se ha producido un error procesando la petición : " + sError + "';", true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@";
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(Utilidades.unescape(aArgs[1]));
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    protected string Grabar(string strDenominacion)
    {
        string sResul = "", sNombreArchivo="";
        long? idContentServer = null;
        #region Conexión
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region Obtengo el contenido del archivo y lo guardo en ATENEA
            sNombreArchivo = Path.GetFileName(Session["NOMBRE_ARCHIVO"].ToString());
            if (Session["ARCHIVO"] != null)
            {
                if (this.hdnContentServer.Value != "")
                {//Cuando hemos cargado la página ya teníamos un Id de documento en ATENEA
                    idContentServer = long.Parse(this.hdnContentServer.Value);
                    if (sNombreArchivo == this.hdnNomArchivo.Value)
                    {//Si el nombre del nuevo archivo es el mismo que el inicial
                        IB.Conserva.ConservaHelper.ActualizarContenidoDocumento((long)idContentServer, (byte[])Session["ARCHIVO"]);
                    }
                    else
                    {//El archivo a cargar es dierente
                        IB.Conserva.ConservaHelper.ActualizarDocumento((long)idContentServer, (byte[])Session["ARCHIVO"], sNombreArchivo);
                    }
                }
                else
                    idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(sNombreArchivo, (byte[])Session["ARCHIVO"]);
            }
            #endregion
            if (hdnAccion.Text == "I")
            {
                //DOCUIDFICEPI.Insert(null, int.Parse(hdnID.Text), strDenominacion, sNombreArchivo,
                //                    (byte[])Session["ARCHIVO"], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), idContentServer);
                DOCUIDFICEPI.Insert(null, int.Parse(hdnID.Text), strDenominacion, sNombreArchivo,
                                    int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), idContentServer);
            }
            else
            {
                if (Session["ARCHIVO"] != null)
                {
                    //DOCUIDFICEPI.Update(null, int.Parse(hdnID.Text), strDenominacion, sNombreArchivo,
                    //                    (byte[])Session["ARCHIVO"], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), idContentServer);
                    DOCUIDFICEPI.Update(null, int.Parse(hdnID.Text), strDenominacion, sNombreArchivo,
                                         int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), idContentServer);
                }
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + ID.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del documento", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}


