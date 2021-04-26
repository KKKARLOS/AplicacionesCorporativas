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
    public string sNodo = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Candidatos a responsable de proyecto";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        cargarNodos();
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
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getCRP"):
                sResultado += ObtenerCRP(aArgs[1]);
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

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, string sNodo)
    {// Devuelve el código HTML del catalogo de tareas de la plantilla que se pasa por parámetro
        StringBuilder sb = new StringBuilder();
        try
        {
            string sV1 = Utilidades.unescape(sAP1);
            string sV2 = Utilidades.unescape(sAP2);
            string sV3 = Utilidades.unescape(sNom);

            SqlDataReader dr = USUARIO.GetProfesionalesNodo(int.Parse(sNodo), sV1, sV2, sV3);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 390px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:370px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' onClick='mm(event)' ");
                sb.Append("onmousedown='DD(event)' onDblClick='convocar(this.id,this.cells[1].innerText);' ");
                //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("><td></td><td><nobr class='NBR W350'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");

            return "OK@#@"+sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@";
        }
    }
    private static string ObtenerCRP(string nNodo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = USUARIO.GetProfCRPNodo(int.Parse(nNodo));

            sb.Append("<table id='tblOpciones2' class='texto MM' style='WIDTH: 390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:360px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString());
                sb.Append("' bd='' onclick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                sb.Append("><td></td><td></td>");
                sb.Append("<td><nobr class='NBR W350'>" + dr["PROFESIONAL"] + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los CRPs", ex, false);
        }
    }   
    private string Grabar(string strDatos)
    {
        string  sResul = "";
        SqlConnection oConn = null;
        SqlTransaction tr = null;

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
            #region CDP
            string[] aDatos = Regex.Split(strDatos, "///");

            foreach (string oCRP in aDatos)
            {
                if (oCRP == "") continue;
                string[] aCRP = Regex.Split(oCRP, "##");
                ///aCRP[0] = Opcion BD. "I", "U", "D"
                ///aCRP[1] = ID usuario

                USUARIO.UpdateCRP(tr, int.Parse(aCRP[1]), (aCRP[0] != "D") ? true : false);
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la lista de CRPs", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private void cargarNodos()
    {
        try
        {
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], false);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
}
