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

using System.Text.RegularExpressions;
using EO.Web;
//using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "", sErrores;
    protected string sLectura = "false";
    public string strTablaHTMLIntegrantes, sDefNodo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Miembros de la oficina técnica";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            if (!Page.IsPostBack)
            {
                try
                {
                    //Cargo la denominacion del label Nodo
                    sDefNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        txtDesNodo.Visible = true;
                        strTablaHTMLIntegrantes = "<table id='tblDatos'></table>";
                    }
                    else
                    {
                        cboCR.Visible = true;
                        txtDesNodo.Visible = false;
                        cargarNodos("");
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
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getNodo"):
                sResultado += "OK@#@" + ObtenerIntegrantes(int.Parse(aArgs[1]));
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

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, string sCR)
    {// Devuelve el código HTML del catalogo de tareas de la plantilla que se pasa por parámetro
        StringBuilder sb = new StringBuilder();
        try
        {
            //SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa("N", Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), 
            //                                                              Utilidades.unescape(sNom), sCR, "", "", "",true);
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom), false);
            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 390px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:360px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' ");
                sb.Append("onClick='mm(event)' onmousedown='DD(event)' onDblClick='convocar(this);' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='0' ");
                //sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                else if (dr["t303_idnodo"].ToString() == sCR) sb.Append("tipo='P' ");
                else sb.Append("tipo='N' ");

                //sb.Append("><td></td><td><nobr class='NBR W350' onDblClick='convocar(this.parentNode.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("><td></td><td><nobr class='NBR W350' onDblClick='convocar(this.parentNode.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); 
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
        }
    }
    private static string ObtenerIntegrantes(int iCodCR)
    {// Devuelve el código HTML del catalogo de personas que son integrantes de la Oficina Técnica del CR que se pasa como parametro
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = OfiTec.CatalogoIntegrantes(iCodCR);

            sb.Append("<table id='tblOpciones2' class='texto MM' style='width: 390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:360px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString());
                sb.Append("' bd='' onclick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                else if (dr["t303_idnodo"].ToString() == iCodCR.ToString()) sb.Append("tipo='P' ");
                else sb.Append("tipo='N' ");
                
                sb.Append("><td></td><td></td>");
                //sb.Append("<td onmouseover='TTip()'><NOBR class='NBR W350' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><span class='NBR W350' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' /> Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</span></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "error@#@" + Errores.mostrarError("Error al obtener los integrantes", ex);
        }
    }
    private string Grabar(string sCR, string sCadena)
    {//En el parametro de entrada tenemos una lista de codigos de personas separados por comas 
        string sResul = "";
        int iCodCR;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            iCodCR = int.Parse(sCR);
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
            if (sCadena != "")
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aFun = Regex.Split(sCadena, "///");
                foreach (string oFun in aFun)
                {
                    string[] aValores = Regex.Split(oFun, "##");
                    switch (aValores[0])
                    {
                        case "I":
                            OfiTec.InsertarIntegrante(tr, iCodCR, int.Parse(aValores[1]));
                            break;
                        case "D":
                            OfiTec.BorrarIntegrante(tr, iCodCR, int.Parse(aValores[1]));
                            break;
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la lista de integrantes", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private void cargarNodos(string sNodo)
    {
        try
        {
            bool bSeleccionado = false;
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                if (!bSeleccionado)
                {
                    if (sNodo == "")
                    {
                        oLI.Selected = true;
                        bSeleccionado = true;
                        strTablaHTMLIntegrantes = ObtenerIntegrantes(int.Parse(dr["t303_idnodo"].ToString()));
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            strTablaHTMLIntegrantes = ObtenerIntegrantes(int.Parse(sNodo));
                        }
                    }
                }
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
}
