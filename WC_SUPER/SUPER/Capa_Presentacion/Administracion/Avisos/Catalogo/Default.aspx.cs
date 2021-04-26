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
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlGF, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "2,7,13,74";//,71
            Master.sbotonesOpcionOff = "7,13,74";
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Catálogo de avisos";
            Master.bFuncionesLocales = true;

            if (!Page.IsPostBack)
            {
                try
                {
                    ObtenerAvisos();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {

            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerAvisos();
                break;
            case ("eliminar"):
                sResultado += EliminarAviso(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerAvisos()
    {// Devuelve el código HTML del catalogo de avisos. 
        StringBuilder sb = new StringBuilder();
        string sResul;
        try
        {
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width:600px'>");
            sb.Append("<table id='tblDatos' class='texto MA' name='tblDatos' style='WIDTH: 600px;'>");
            sb.Append("<colgroup><col style='width:500px;'/><col style='width:100px;'/></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = TEXTOAVISOS.Catalogo2(2, 0);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t448_idaviso"].ToString() + "' bd=''");
                sb.Append(" texto=\"" + Utilidades.escape(dr["t448_texto"].ToString()) + "\"");
                sb.Append(" style='height:16px' onclick='mm(event);mostrarTexto(this)' ondblclick='mostrarDetalle(this.id)'>");
                sb.Append("<td>" + dr["t448_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["numero"].ToString()).ToString("#,##0") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close(); dr.Dispose();
            sb.Append("</tbody></table></div>");

            sResul = sb.ToString();
            this.strTablaHtmlGF = sResul;
            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los avisos", ex);
            return "error@#@Error al obtener los avisos " + ex.Message;
        }
    }
    private string EliminarAviso(string strAviso)
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
            string[] aAviso = Regex.Split(strAviso, "##");
            foreach (string oAviso in aAviso)
            {
                if (oAviso !="")
                    TEXTOAVISOS.Delete(tr, int.Parse(oAviso));
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            Master.sErrores = Errores.mostrarError("Error al eliminar el aviso " + strAviso, ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string Grabar(string sAccion, string strAvisos)
    {
        string sResul = "";
        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            #region Avisos
            if (strAvisos != "")
            {
                string[] aAvisos = Regex.Split(strAvisos, "##");
                foreach (string sIdAviso in aAvisos)
                {
                    if (sIdAviso != "")
                    {
                        USUARIOAVISOS.BorrarTodos(tr, int.Parse(sIdAviso));
                        if (sAccion == "T")
                             USUARIOAVISOS.InsertarTodos(tr, int.Parse(sIdAviso));
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los profesionales asignados al aviso", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}