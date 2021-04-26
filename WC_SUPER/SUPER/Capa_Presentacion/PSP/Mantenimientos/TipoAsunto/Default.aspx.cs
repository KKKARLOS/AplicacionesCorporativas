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
using EO.Web; 
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Capa_Presentacion_Mantenimientos_TipoAsunto_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 9;// 52;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Relación de tipos de asunto";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    ObtenerTiposAsunto("3","0");
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getTipos"):
                sResultado += ObtenerTiposAsunto(aArgs[1], aArgs[2]);
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

    private string ObtenerTiposAsunto(string sOrden, string sAscDesc)
    {
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 450px;' mantenimiento='1'>");
        strBuilder.Append("<colgroup><col style='width:15px;' /><col style='width:435px;' /></colgroup>");
        strBuilder.Append("<tbody>");
        SqlDataReader dr = TIPOASUNTO.Catalogo("", null, null, byte.Parse(sOrden), byte.Parse(sAscDesc));
        while (dr.Read())
        {
            strBuilder.Append("<tr id='" + dr["t384_idtipo"].ToString() + "' orden='" + dr["T384_orden"].ToString());
            strBuilder.Append("' bd='' onclick='mm(event)' onKeyUp=\"mfa(this,'U')\" style='height:20px;'>");
            strBuilder.Append("<td><img src='../../../../images/imgFN.gif'></td>");
            strBuilder.Append("<td><input type='text' id='txtFun' class='txtL' style='width:95%' value='");
            strBuilder.Append(dr["t384_destipo"].ToString() + "' maxlength='40'></td></tr>");
        }
        dr.Close();
        dr.Dispose();
        strBuilder.Append("</tbody>");
        strBuilder.Append("</table>");
        strTablaHtml = strBuilder.ToString();
        return "OK@#@" + strTablaHtml;
    }

    protected string Grabar(string strTiposAsunto)
    {
        string sResul = "";
        #region abrir conexión
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
            string[] aFun = Regex.Split(strTiposAsunto, "///");
            foreach (string oFun in aFun)
            {
                string[] aValores = Regex.Split(oFun, "##");
                switch (aValores[0])
                {
                    case "I":
                        TIPOASUNTO.Insert(tr, Utilidades.unescape(aValores[2]), byte.Parse(aValores[3]));
                        break;
                    case "U":
                        TIPOASUNTO.Update(tr, Utilidades.unescape(aValores[2]), int.Parse(aValores[1]), byte.Parse(aValores[3]));
                        break;
                    case "D":
                        TIPOASUNTO.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@";
            sResul = ObtenerTiposAsunto("3", "0");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar los tipos de asunto", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}