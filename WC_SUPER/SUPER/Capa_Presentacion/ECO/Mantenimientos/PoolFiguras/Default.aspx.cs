using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

//using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "", sErrores;
    protected string sLectura = "false";
    public string strTablaHTMLIntegrantes="", sDefNodo = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Figuras de proyectos económicos";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Mantenimientos/PoolFiguras/Functions/ddfiguras.js");
            Master.FicherosCSS.Add("App_Themes/Corporativo/ddfiguras.css");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //}
                    //R -> asignación de figuras por responsable
                    //N -> asignación de figuras por Nodo
                    this.hdnTipo.Value = Request.QueryString["sT"].ToString();
                    //string sAux = Utilidades.DecodeFrom64("U1VTQU5BIE1BQ0FaQUdBIyMyODU0"); 
                    //Cargo la denominacion del label Nodo
                    sDefNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.InnerText = sDefNodo;
                    //strTablaHTMLIntegrantes = "<table id='tblDatos'></table>";
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        //txtDesNodo.Visible = true;
                        //strTablaHTMLIntegrantes = "<table id='tblDatos'></table>";
                    }
                    else
                    {
                        //txtDesNodo.Visible = false;
                        this.hdnIdUser.Value = Session["UsuarioActual"].ToString();
                        ObtenerIntegrantes(this.hdnTipo.Value, this.hdnIdUser.Value);
                    }
                    txtApellido1.Focus();
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
                Response.Redirect(HistorialNavegacion.Leer(), true);
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
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getFiguras"):
                sResultado += "OK@#@" + ObtenerIntegrantes(aArgs[1], aArgs[2]);
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

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //No dejamos seleccionar foráneos
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom), false);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 400px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:380px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' ");
                //sb.Append("onClick='mmse(this)' onmousedown='DD(this)' onDblClick='convocar(this);' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");

                //sb.Append("><td></td><td><nobr class='NBR W375' onDblClick='insertarFigura(this.parentNode.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("><td></td><td><nobr class='NBR W375' ondblclick='insertarFigura(this.parentNode.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); 
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "error@#@" + Errores.mostrarError("Error al obtener profesionales.", ex);
        }
    }
    private string Grabar(string sTipo,string sID, string strFiguras)
    {
        string sResul = "";
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
            if (strFiguras != "")//No se ha modificado nada de la pestaña de Figuras
            {
                string[] aUsuarios = Regex.Split(strFiguras, "///");
                foreach (string oUsuario in aUsuarios)
                {
                    if (oUsuario == "") continue;
                    string[] aFig = Regex.Split(oUsuario, "##");
                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras
                    if (aFig[0] == "D")
                    {
                        if (sTipo == "R")
                            FIGURAPSN_RESPONSABLE.DeleteUsuario(tr, int.Parse(sID), int.Parse(aFig[1]));
                        else
                            FIGURAPSN_NODO.DeleteUsuario(tr, int.Parse(sID), int.Parse(aFig[1]));
                    }
                    else
                    {
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFigura in aFiguras)
                        {
                            if (oFigura == "") continue;
                            string[] aFig2 = Regex.Split(oFigura, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2[0] == "D")
                            {
                                if (sTipo == "R")
                                    FIGURAPSN_RESPONSABLE.Delete(tr,int.Parse(sID),int.Parse(aFig[1]), aFig2[1]);
                                else
                                    FIGURAPSN_NODO.Delete(tr,int.Parse(sID),int.Parse(aFig[1]), aFig2[1]);
                            }
                            else
                            {
                                if (sTipo == "R")
                                    FIGURAPSN_RESPONSABLE.Insert(tr, int.Parse(sID), int.Parse(aFig[1]), aFig2[1]);
                                else
                                    FIGURAPSN_NODO.Insert(tr, int.Parse(sID), int.Parse(aFig[1]), aFig2[1]);
                            }
                        }
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la lista de figuras.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ObtenerIntegrantes(string sTipo, string sID)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            //SqlDataReader dr = FIGURAPROYECTOSUBNODO.CatalogoFiguras(int.Parse(sIDProySubNodo));
            SqlDataReader dr;
            if (sTipo == "R")
                dr = FIGURAPSN_RESPONSABLE.CatalogoFiguras(int.Parse(sID));
            else
                dr = FIGURAPSN_NODO.CatalogoFiguras(int.Parse(sID));

            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='width: 410px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width: 10px' /><col style='width: 20px' /><col style='width: 280px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody>");
            int nUsuario = 0;
            bool bHayFilas = false;
            string sColor = "black";
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");
                i++;
                sColor = "black";
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;color:" + sColor + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else sb.Append("tipo='P' ");
                    sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    //Figuras
                    sb.Append("<td><div style='height:20px;'><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='J' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='B' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='J' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='B' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSecretaria.gif' title='Secretaria' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTMLIntegrantes = sb.ToString();
            this.hdnAux.Value = sbuilder.ToString();

            return sb.ToString() + "@#@" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }
}
