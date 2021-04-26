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
    public string strTablaHtml, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Apertura / Cierre de meses de un Proyecto Económico";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHtml = "<table id='tblDatos'></table>";//<tbody id='tbodyDatos'></tbody>
                    //strTablaHtml = "<table id='tblDatos' class='texto MANO' style='WIDTH: 260px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0' mantenimiento='1'><colgroup><col style='width:10px;' /><col style='width:190px;' /><col style='width:60px;' /></colgroup><tbody><tr id='1242' bd='' estado='A'><td><img src='../../../../images/imgFN.gif'></td><td>Noviembre 2008</td><td class='MA'><img src='../../../../images/imgFN.gif' ondblclick='setEstado(this)'></td></tr><tr id='18547' bd='' estado='A'><td><img src='../../../../images/imgFN.gif'></td><td>Octubre 2009</td><td class='MA'><img src='../../../../images/imgFN.gif' ondblclick='setEstado(this)'></td></tr></tbody></table>";
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
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
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
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
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("buscar"):
                sResultado += ObtenerPEs(aArgs[1]);
                break;
            //case ("meses"):
            //    sResultado += ObtenerMeses(aArgs[1]);
            //    break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
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
    private string ObtenerPEs(string sIdPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //Obtengo los datos del proyecto
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(tr, int.Parse(sIdPSN));
            PROYECTO oProy = PROYECTO.Obtener(tr, oPSN.t301_idproyecto);
            return "OK@#@" + sb.ToString() + "@#@" + oPSN.t301_idproyecto.ToString("#,###") + "##" +
                    Utilidades.escape(oProy.t301_denominacion) + "##" +
                    Utilidades.escape(oProy.t302_denominacion) + "##" +
                    oProy.t301_estado + "##" + oProy.t301_categoria + "##" + oPSN.t305_cualidad + "##" + 
                    oPSN.t303_denominacion + "##" + Utilidades.escape(oPSN.des_responsable) + "##" + 
                    ObtenerMeses(sIdPSN, oProy.t301_estado);
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener el Proyecto Económico", ex);
            return "error@#@Error al obtener el Proyecto Económico " + ex.Message;
        }
    }
    private string ObtenerMeses(string sPSN, string sEstProy)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width: 260px; text-align:left' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:180px;' /><col style='width:60px;' /></colgroup>");
        sb.Append("<tbody>");
        SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sPSN));
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t325_idsegmesproy"].ToString() + "' bd='' estado='" + dr["t325_estado"].ToString() + "' style='height:20px'>");
            sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
            sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t325_anomes"].ToString())) + "</td>");
            if (sEstProy == "A")
            {//Solo permito cambiar estado del mes en proyectos abiertos
                //sb.Append("<td><img src='../../../../images/imgFN.gif' onclick=\"mfa(this,'U')\"></td></tr>");
                if (dr["t325_estado"].ToString()=="A")
                    sb.Append("<td style='text-align:center;' class='MA' title='Mes abierto'><img src='../../../../images/imgMesAbierto.gif' ondblclick='setEstado(this)'/></td></tr>");
                else
                    sb.Append("<td style='text-align:center;' class='MA' title='Mes cerrado'><img src='../../../../images/imgMesCerrado.gif' ondblclick='setEstado(this)'/></td></tr>");
            }
            else
            {
                if (dr["t325_estado"].ToString() == "A")
                    sb.Append("<td style='text-align:center;' title='Mes abierto'><img src='../../../../images/imgMesAbierto.gif' onclick=\"alert('El estado del proyecto no permite la modificación de meses')\"></td></tr>");
                else
                    sb.Append("<td style='text-align:center;' title='Mes cerrado'><img src='../../../../images/imgMesCerrado.gif' onclick=\"alert('El estado del proyecto no permite la modificación de meses')\"></td></tr>");
            }
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        //strTablaHtml = sb.ToString();
        return sb.ToString();
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString(); 
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }

    private string Grabar(string sPSN, string strMeses)
    {
        string sResul = "OK@#@";
        #region abrir conexión y transacción serializable
        try
        {
            oConn = Conexion.Abrir();
            //tr = Conexion.AbrirTransaccion(oConn);
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
            bool bContinuar = true;
            string[] aMes = Regex.Split(strMeses, "///");
            foreach (string oMes in aMes)
            {
                if (bContinuar)
                {
                    string[] aValores = Regex.Split(oMes, "##");
                    switch (aValores[0])
                    {
                        case "U":
                            SEGMESPROYECTOSUBNODO.UpdateEstado(tr, int.Parse(aValores[1]), aValores[2]);
                            break;
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //Master.sErrores = Errores.mostrarError("Error al eliminar el proyecto económico " + sNumPE, ex);
            sResul = "error@#@Error al actualizar el estado del mes del proyecto económico.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}