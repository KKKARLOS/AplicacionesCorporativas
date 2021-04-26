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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sMensajeMMOFF = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected bool bMostrarMMOFF = false;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 0;
            Master.TituloPagina = "Detalle de comunicados";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.Modulo = "NEUTRAL";
            try
            {
                Session["HAYAVISOS"] = "0";
                //Mostrar avisos únicamente al usuario real, no los de reconexión.
                if ((int)Session["IDFICEPI_PC_ACTUAL"] == (int)Session["IDFICEPI_ENTRADA"] &&
                    (int)Session["IDFICEPI_CVT_ACTUAL"] == (int)Session["IDFICEPI_ENTRADA"])
                {
                    //SqlDataReader dr2 = USUARIOAVISOS.SelectByT314_idusuario(null, (int)Session["UsuarioActual"]);
                    //Victor 17/06/2010: mostrar los avisos del usuario de entrada
                    SqlDataReader dr = USUARIOAVISOS.SelectByT314_idusuario(null, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                    if (dr.Read()) Session["HAYAVISOS"] = "1";
                    dr.Close();
                    dr.Dispose();
                }
                if (Session["HAYAVISOS"].ToString() == "1")
                {
                    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/AvisosNav.ascx"));
                    Session["HAYAVISOS"] = "0"; //19/12/2014: Victor dixit: para que los avisos salgan una sola vez cuando se entra en la aplicación.
                }
                else
                {   //Si no hay avisos de Admon y hemos accedido desde menú para ver los que hay
                    sMensajeMMOFF = "No existen comunicados de Administración para Ud.";
                    bMostrarMMOFF = true;
                }

                if (bMostrarMMOFF)
                {
                    //this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/mensajeOff.ascx"));
                    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Msg/mmoff.ascx"));
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al carar la página", ex);
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("eliminar"):
                sResultado += EliminarAviso(aArgs[1]);
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
    private string EliminarAviso(string sIdAviso)
    {
        string sResul = "OK@#@";
        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            if (sIdAviso != "")
            {
                //Victor 17/06/2010: mostrar los avisos del usuario de entrada
                //USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), int.Parse(Session["UsuarioActual"].ToString()));
                //int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, int.Parse(Session["UsuarioActual"].ToString()));
                USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), (int)Session["NUM_EMPLEADO_ENTRADA"]);
                int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                if (iNumAvisos == 0)
                    Session["HAYAVISOS"] = "0";
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar el aviso " + sIdAviso, ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
