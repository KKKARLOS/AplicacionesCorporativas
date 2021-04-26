using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GEMO.BLL;
using Microsoft.JScript;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    //public string sUS = "", sMB = "", sRP = "", sMensajeMMOFF = "", sIG = "", sCI = "", sPP="";
    //public SqlConnection oConn;
    //public SqlTransaction tr;
    //protected bool bMostrarMMOFF = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //int a = 0;
            //int b = 1;
            //int c = b / a;
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                //Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                //Master.FuncionesJavaScript.Add("Capa_Presentacion/Inicio/Functions/pngfix.js");
                //Master.FicherosCSS.Add("Capa_Presentacion/Inicio/css/StyleSheet.css");

                //if (Request.QueryString["sMB"] != null)
                //{
                //    sMB = Request.QueryString["sMB"].ToString();
                //    switch (sMB)
                //    {
                //        case "D":
                //            desactivarMensajeBienvenida();
                //            bMostrarMMOFF = true;
                //            break;
                //        case "M":
                //            mostrarMensajeBienvenida();
                //            break;
                //        default:
                //            activarMensajeBienvenida(int.Parse(Request.QueryString["sMB"].ToString()));
                //            bMostrarMMOFF = true;
                //            break;
                //    }
                //}

                //if (Request.QueryString["sUS"] != null) sUS = Request.QueryString["sUS"].ToString();

                //if (Request.QueryString["sRP"] != null)
                //{
                //    sRP = Request.QueryString["sRP"].ToString();
                //    setResolucion(sRP);
                //    bMostrarMMOFF = true;
                //}
                //if (Request.QueryString["sCI"] != null)
                //{
                //    sCI = Request.QueryString["sCI"].ToString();
                //    setCorreosInformativos(sCI);
                //    bMostrarMMOFF = true;
                //}
                //if (Request.QueryString["sIG"] != null)
                //{
                //    sIG = Request.QueryString["sIG"].ToString();
                //    setImportacionGasvi(sIG);
                //    bMostrarMMOFF = true;
                //}
                //if (Request.QueryString["sPP"] != null)
                //{
                //    sPP = Request.QueryString["sPP"].ToString();
                //    setPeriodificacion(sPP);
                //    bMostrarMMOFF = true;
                //}

                //if (Session["HAYNOVEDADES"].ToString() == "1" && Session["NOVEDADESLEIDAS"].ToString() == "0" && !(bool)Session["NOVEDADESMOSTRADAS"])
                //{
                //    Session["NOVEDADESMOSTRADAS"] = true;
                //    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Novedades.ascx"));
                //}
                //if (Session["HAYAVISOS"].ToString() == "1")
                //{
                //    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Avisos.ascx"));
                //}
                //else
                //{   //Si no hay avisos de Admon y hemos accedido desde menú para ver los que hay
                //    if (Request.QueryString["a"] != null)
                //    {
                //        if (Request.QueryString["a"] == "a")
                //        {
                //            sMensajeMMOFF = "No existen comunicados de Administración para Ud.";
                //            bMostrarMMOFF = true;
                //        }
                //    }
                //}
                //try
                //{
                //    WeatherControlC o = (WeatherControlC)Page.FindControl(Constantes.sPrefijo + "Weather");
                //    o.LocationID = Session["CODWEATHER"].ToString();
                //    o.LocationName = Session["NOMWEATHER"].ToString();
                //}
                //catch (Exception)
                //{
                //    this.Weather.Visible = false;
                //}

                //if (bMostrarMMOFF)
                //    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/mensajeOff.ascx"));

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Error al mostrar la previsión metereológica.") Master.sErrores = Errores.mostrarError("Error al cargar los datos: ", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@";
        switch (aArgs[0])
        {
            case ("delFoto"):
                //if (Session["FOTOUSUARIO"] != null)
                //    Session["FOTOUSUARIO"] = null;
                //sResultado += "OK";
                break;
            case ("eliminar"):
                //sResultado += EliminarAviso(aArgs[1]);
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

    //public void activarMensajeBienvenida(int nSegundos)
    //{
    //    try
    //    {
    //        USUARIO.UpdateMensajeBienvenida((int)Session["NUM_EMPLEADO_ENTRADA"], nSegundos);
    //        Session["MostrarMensajeBienvenida"] = true;
    //        if (nSegundos==1) sMensajeMMOFF = "Mensaje de bienvenida activado y configurado a "+ nSegundos.ToString() +" segundo.";
    //        else sMensajeMMOFF = "Mensaje de bienvenida activado y configurado a " + nSegundos.ToString() + " segundos.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al activar el mensaje de bienvenida.", ex);
    //    }
    //}
    //public void desactivarMensajeBienvenida()
    //{
    //    try
    //    {
    //        USUARIO.UpdateMensajeBienvenida((int)Session["NUM_EMPLEADO_ENTRADA"], 0);
    //        Session["MostrarMensajeBienvenida"] = false;
    //        sMensajeMMOFF = "Mensaje de bienvenida desactivado.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al desactivar el mensaje de bienvenida.", ex);
    //    }
    //}
    //public void mostrarMensajeBienvenida()
    //{
    //    try
    //    {
    //        Session["BIENVENIDAMOSTRADA"] = false;
    //        Session["MostrarMensajeBienvenida"] = true;
    //        if (Session["FOTOUSUARIO"] == null)
    //        {
    //            Recurso objRec = new Recurso();
    //            bool bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), ((int)Session["UsuarioActual"]==0)? null: (int?)int.Parse(Session["UsuarioActual"].ToString()));
    //            if (bIdentificado) Session["FOTOUSUARIO"] = objRec.t001_foto;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al mostrar el mensaje de bienvenida.", ex);
    //    }
    //}
    //private string EliminarAviso(string sIdAviso)
    //{
    //    string sResul = "OK@#@";
    //    #region abrir conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        if (sIdAviso != "")
    //        {
    //            //Victor 17/06/2010: mostrar los avisos del usuario de entrada
    //            //USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), int.Parse(Session["UsuarioActual"].ToString()));
    //            //int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, int.Parse(Session["UsuarioActual"].ToString()));
    //            USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), (int)Session["NUM_EMPLEADO_ENTRADA"]);
    //            int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, (int)Session["NUM_EMPLEADO_ENTRADA"]);
    //            if (iNumAvisos == 0)
    //                Session["HAYAVISOS"] = "0";
    //        }
    //        Conexion.CommitTransaccion(tr);
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al eliminar el aviso " + sIdAviso, ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}
    //public void setResolucion(string nOpcion)
    //{
    //    try
    //    {
    //        USUARIO.UpdateResolucion(-1, (int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
    //        Session["CARRUSEL1024"] = (nOpcion == "1") ? true : false;
    //        Session["AVANCE1024"] = (nOpcion == "1") ? true : false;
    //        Session["RESUMEN1024"] = (nOpcion == "1") ? true : false;
    //        Session["DATOSRES1024"] = (nOpcion == "1") ? true : false;
    //        Session["FICHAECO1024"] = (nOpcion == "1") ? true : false;
    //        Session["SEGRENTA1024"] = (nOpcion == "1") ? true : false;
    //        Session["AVANTEC1024"] = (nOpcion == "1") ? true : false;
    //        Session["ESTRUCT1024"] = (nOpcion == "1") ? true : false;
    //        Session["FOTOPST1024"] = (nOpcion == "1") ? true : false;
    //        Session["PLANT1024"] = (nOpcion == "1") ? true : false;
    //        Session["CONST1024"] = (nOpcion == "1") ? true : false;
    //        Session["IAPFACT1024"] = (nOpcion == "1") ? true : false;
    //        Session["IAPDIARIO1024"] = (nOpcion == "1") ? true : false;

    //        sMensajeMMOFF = "Resolución configurada.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al activar el mensaje de bienvenida.", ex);
    //    }
    //}

    //public void setCorreosInformativos(string nOpcion)
    //{
    //    try
    //    {
    //        USUARIO.UpdateCorreosInformativos((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
    //        Session["RECIBIRMAILS"] = (nOpcion=="1")? true:false;

    //        sMensajeMMOFF = "Configuración modificada.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al activar el mensaje de bienvenida.", ex);
    //    }
    //}

    //public void setImportacionGasvi(string nOpcion)
    //{
    //    try
    //    {
    //        USUARIO.UpdateImportacionGasvi((int)Session["NUM_EMPLEADO_ENTRADA"], byte.Parse(nOpcion));
    //        Session["IMPORTACIONGASVI"] = byte.Parse(nOpcion);

    //        sMensajeMMOFF = "Importación configurada.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al configurar la importación de GASVI.", ex);
    //    }
    //}
    //public void setPeriodificacion(string nOpcion)
    //{
    //    try
    //    {
    //        USUARIO.UpdatePeriodificacionProyectos((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
    //        //Session["IMPORTACIONGASVI"] = byte.Parse(nOpcion);

    //        sMensajeMMOFF = "Periodificación configurada.";
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores += Errores.mostrarError("Error al configurar la periodificación de proyectos.", ex);
    //    }
    //}
    
}
