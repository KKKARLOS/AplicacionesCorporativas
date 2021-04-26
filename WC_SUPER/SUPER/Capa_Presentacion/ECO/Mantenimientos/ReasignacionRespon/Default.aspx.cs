using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 0;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.sbotonesOpcionOn = "50,87,31";
                Master.sbotonesOpcionOff = "";

                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Reasignación de proyectos (Responsable)";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                string strTabla = cargarSubnodos();
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else Master.sErrores = aTabla[1];
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

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
            case ("proyectos"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2]);
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1]);
                break;
            case ("recuperar"):
                sResultado += Recuperar();
                break;
            case ("getSubnodos"):
                sResultado += cargarSubnodos();
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

    private string cargarSubnodos()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUBNODO.CatalogoParaReasignacion((int)Session["UsuarioActual"]);
            string sTootTip = "";

            sb.Append("<table id='tblSubnodos' class='texto MANO' style='width:800px;'>");
            sb.Append("<colgroup><col style='width:400px;' /><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString();

                sb.Append("<tr id='" + dr["t304_idsubnodo"].ToString() + "' nodo='" + dr["t303_idnodo"].ToString() + "' onclick='mm(event);'  ");
                sb.Append("style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:3px;'>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("<td style='border-right: solid 1px #569BBD; padding-left:3px;'>" + dr["t304_denominacion"].ToString() + "</td>");
                
                //sb.Append("<tr style='height:20px;' >");
                //sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                //sb.Append("<td>" + dr["t304_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los subnodos", ex);
        }
    }

    protected string ObtenerProyectos(string sIdSubnodo, string sIdUsuario)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosReasignacionMultiSubnodo(sIdSubnodo, (sIdUsuario == "") ? null : (int?)int.Parse(sIdUsuario));

            sb.Append("<table id='tblDatos' class='texto MAM' style='width:440px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:190px;' />");
            sb.Append("    <col style='width:190px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("responsable_origen='" + dr["t314_idusuario_responsable"].ToString() + "' ");
                //sb.Append("onclick='mmse(this);' ondblclick='insertarProyecto(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W180' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W185' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["responsable"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos.", ex, false);
        }

        return sResul;
    }
    private string Procesar(string strDatos)
    {
        string sResul = "";

        int idPSN=0, responsable_origen=0, responsable_destino=0;

        try
        {
            REASIGNACIONPSN.DeleteReasig(null, (int)Session["UsuarioActual"]);

            string[] aDatos = Regex.Split(strDatos, "///");
            foreach (string oProyecto in aDatos)
            {
                try
                {
                    if (oProyecto == "") continue;
                    string[] aProyecto = Regex.Split(oProyecto, "##");
                    ///aProyecto[0] = idPSN
                    ///aProyecto[1] = responsable_origen
                    ///aProyecto[2] = responsable_destino
                    ///aProyecto[3] = procesado

                    idPSN = int.Parse(aProyecto[0]);
                    responsable_origen = int.Parse(aProyecto[1]);
                    responsable_destino = int.Parse(aProyecto[2]);

                    REASIGNACIONPSN.Insertar(null, idPSN, (int)Session["UsuarioActual"], responsable_destino, false, "");
                       
                    if (aProyecto[3] == "1" || aProyecto[1]==aProyecto[2])
                    {
                        REASIGNACIONPSN.Modificar(null, idPSN, (int)Session["UsuarioActual"], responsable_destino, true, "");
                        continue;
                    }

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

                    PROYECTOSUBNODO.UpdateReasignacion(tr, idPSN, responsable_destino);
                    
                    //update proceso OK
                    REASIGNACIONPSN.Modificar(null, idPSN, (int)Session["UsuarioActual"], responsable_destino, true, "");

                    Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    //update proceso KO
                    REASIGNACIONPSN.Modificar(null, idPSN, (int)Session["UsuarioActual"], responsable_destino, false, ex.Message);
                    sResul = "Error@#@" + Errores.mostrarError("Error al procesar la reasignación.", ex);
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }

            }// fin foreach
            sResul = "OK";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar la reasignación.", ex);
        }
        return sResul;
    }
    protected string Recuperar()
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = REASIGNACIONPSN.CatalogoDestinoResp(null, (int)Session["UsuarioActual"]);

            sb.Append("<table id='tblDatos2' class='texto MM' style='width:400px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:167px;' />");
            sb.Append("    <col style='width:180px;' />");
            sb.Append("    <col style='width:30px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("responsable_origen='" + dr["t314_idusuario_responsable_origen"].ToString() + "' ");
                sb.Append("responsable_destino='" + dr["t314_idusuario_responsable_destino"].ToString() + "' ");
                if (dr["t706_procesado"].ToString() == "") sb.Append("procesado='' ");
                else if ((bool)dr["t706_procesado"]) sb.Append("procesado='1' ");
                else sb.Append("procesado='0' ");
                sb.Append("excepcion='" + Utilidades.escape(dr["t706_excepcion"].ToString()) + "' ");

                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W170' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["responsable"].ToString() + "</nobr></td>");
                sb.Append("<td></td></tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos aparcados.", ex);
        }

        return sResul;
    }

}
