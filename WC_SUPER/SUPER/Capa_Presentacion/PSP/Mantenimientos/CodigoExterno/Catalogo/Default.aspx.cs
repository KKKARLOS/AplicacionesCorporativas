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
    protected string strInicial;
    protected string sLectura;
    public string strTablaHTMLIntegrantes;

    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Asignación de códigos externos";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;

            this.lblCR.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
            rdbAmbito.Items[1].Text = Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "  ";

            strTablaHTMLIntegrantes = "<table id='tblOpciones2' class='texto' style='width: 450px;' cellSpacing='0' border='0'><colgroup><col style='width:10px;' /><col style='width:320px;' /><col style='width:150px;' /></colgroup><tbody id='tbodyDestino'></tbody></table>";
            USUARIO oUser = USUARIO.Select(null, int.Parse(Session["UsuarioActual"].ToString()));
            this.hdnCRActual.Value = oUser.t303_idnodo.ToString();
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
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("integrantes"):
                sResultado += Integrantes(aArgs[1], aArgs[2]);
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

    //private string ObtenerPersonas(string sAP1, string sAP2, string sNom)
    //{// Devuelve el código HTML del 
    //    StringBuilder sb = new StringBuilder();
    //    string sV1, sV2, sV3;
    //    try
    //    {
    //        sV1 = Utilidades.unescape(sAP1);
    //        sV2 = Utilidades.unescape(sAP2);
    //        sV3 = Utilidades.unescape(sNom);
    //        //SqlDataReader dr = Recurso.Catalogo(sV1, sV2, sV3);
    //        //SqlDataReader dr = USUARIO.ObtenerProfesionalesPST(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom));
    //        SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa("N", sV1, sV2, sV3, "", "", "", "");

    //        sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 330px; table-layout:fixed;' cellSpacing='0' border='0'>");
    //        sb.Append("<colgroup><col style='width:20px;' /><col style='width:310px;' /></colgroup><tbody id='tbodyOrigen'>");
    //        while (dr.Read())
    //        {
    //            ////sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' red='" + dr["t001_codred"].ToString() + "' style='height:20px' ");
    //            //sb.Append("onClick='mmse(this)' onmousedown='DD(this)' onDblClick='convocar(this.id,this.cells[1].innerText,this.red);' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[Profesional:&nbsp;" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
    //            //sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
    //            //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
    //            //else sb.Append("tipo='P' ");
    //            //sb.Append("><td></td><td><nobr class='NBR W320'>" + dr["profesional"].ToString() + "</nobr></td></tr>");
    //            sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px' onClick='mmse(this)' ");
    //            sb.Append("onmousedown='DD(this)' onDblClick='convocar(this.id,this.cells[1].innerText);' style='noWrap:true;' ");
    //            sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'> ");
    //            sb.Append("Información] body=[Profesional:&nbsp;");
    //            sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
    //            sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
    //            sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    //            sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    //            sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
    //            sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
    //            sb.Append("baja='" + dr["baja"].ToString() + "' ");
    //            if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
    //            else sb.Append("tipo='P' ");

    //            sb.Append("><td></td><td><nobr class='NBR W300'>" + dr["profesional"].ToString() + "</nobr></td>");
    //            sb.Append("</tr>");
    //        }
    //        sb.Append("</tbody></table>");
    //        dr.Close(); dr.Dispose();
    //        //this.strTablaHTMLPersonas = sb.ToString();
    //        return "OK@#@"+sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
    //        return "error@#@";
    //    }
    //}
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                    string t305_idProyectoSubnodo, string sCualidad)
    {
        //Relacion de técnicos candidatos a ser asignados a la actividad
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "",true);
            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 330px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:310px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");

                sb.Append("<label style='width:60px'>Usuario:&nbsp;</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append("<br><label style='width:60px'>");

                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event);' onDblClick='convocar(this.id,this.cells[1].innerText,this.red);' onmousedown='DD(event)'>");
                sb.Append("<td></td><td><span class='NBR W305'>" + dr["Profesional"].ToString() + "</span></td></tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    private string Grabar(string sCadena)
    {//En el parametro de entrada tenemos en primer lugar el código de cliente y luego 
     //una lista de elementos tipoOperacion##numeroSuper##descripcion
        string  sCad="", sResul = "",sOperacion,sDesc;
        int iCodCliente, iCodSuper,iPos;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            if (sCadena == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {
                //Abro transaccion
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);

                iPos = sCadena.IndexOf(",");
                if (iPos < 0) return "Error@#@No se encuentra código de cliente al intentar la grabación";
                sCad = sCadena.Substring(0, iPos);
                iCodCliente = System.Convert.ToInt32(sCad);
                sCadena = sCadena.Substring(iPos + 1);
                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aTareas = Regex.Split(sCadena, @",,");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    if (sCad != "")
                    {
                        string[] aElem = Regex.Split(sCad, @"##");
                        sOperacion=aElem[0];
                        iCodSuper = System.Convert.ToInt32(aElem[1]);
                        sDesc = Utilidades.unescape(aElem[2]);
                        switch(sOperacion)
                        {
                            case "D":
                                codigoexterno.Delete(tr,iCodCliente,iCodSuper,sDesc);
                                break;
                            case "U":
                                codigoexterno.Update(tr,iCodCliente,iCodSuper,sDesc);
                                break;
                            case "I":
                                codigoexterno.Insert(tr,iCodCliente,iCodSuper,sDesc);
                                break;
                        }//switch
                    }//if
                }//for
                //Cierro transaccion
                Conexion.CommitTransaccion(tr);
                //sCad = codigoexterno.ObtenerIntegrantes(iCodCliente);
            }
            sResul = "OK@#@";// +sCad;
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
    private string Integrantes(string sCodCliente, string sEstado)
    {//Rellena la tabla de codigos externos asociados al cliente que se pasa como parámetro
        string sCad, sResul = "OK@#@";
        int iCodCliente;
        bool bMostrarBajas=true;
        //SqlConnection oConn = null;
        try
        {
            if (sCodCliente != "")
            {
                //oConn = Conexion.Abrir();
                iCodCliente =System.Convert.ToInt32(sCodCliente);
                if (sEstado == "0") bMostrarBajas = false;
                sCad = codigoexterno.ObtenerIntegrantes(iCodCliente, bMostrarBajas);
                sResul = "OK@#@" + sCad;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la lista de códigos externos", ex);
        }
        //finally
        //{
        //    Conexion.Cerrar(oConn);
        //}
        return sResul;
    }
}
