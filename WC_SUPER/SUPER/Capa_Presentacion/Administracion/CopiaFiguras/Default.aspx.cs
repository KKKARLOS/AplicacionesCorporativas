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

//using System.Collections.Generic;
using EO.Web;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "", sErrores;//, sLectura = "false";
    public string sNodo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 43;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Copia de permisos de un usuario";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
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
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    SqlDataReader dr = USUARIO.ObtenerDatosProfUsuario((int)Session["UsuarioActual"]);
                    if (dr.Read())
                        this.hdnCRActual.Value = dr["t303_idnodo"].ToString();
                    dr.Close();
                    dr.Dispose();
                    txtApellido1.Focus();
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], (aArgs[4] == "1") ? true : false);
                break;
            case ("procesar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
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

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, bool bSoloActivos)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //SqlDataReader dr =Recurso.ObtenerRelacionProfesionalesTarifa("N", Utilidades.unescape(sAP1), Utilidades.unescape(sAP2),Utilidades.unescape(sNom), "", "", "C", "", bSoloActivos);
            SqlDataReader dr = USUARIO.GetProfAdm(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom), bSoloActivos, null);
            sb.Append("<table id='tblOpciones' class='texto MANO' style='width:440px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional&nbsp;:</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");

                sb.Append("<label style='width:60px'>Usuario&nbsp;:</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));

                sb.Append("<br><label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("onclick='ms(this); '>");
                sb.Append("<td></td><td><nobr class='NBR W420'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los usuarios", ex);
            return "error@#@";
        }
    }
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

            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "", true);
            sb.Append("<table id='tblOpciones2' class='texto MAM' style='WIDTH: 440px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39));

                sb.Append("<br><label style='width:60px'>Usuario:&nbsp;</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append("<br><label style='width:60px'>");

                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event);' ondblclick='insertarRecurso(this);' onmousedown='DD(event)'>");
                sb.Append("<td></td><td><nobr class='NBR W420'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");

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

    private string Grabar(string sIdRecursoOrigen, string sRecursos)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sResul = "";
        int idUserDestino = -1;
        try
        {
            if (sIdRecursoOrigen == "" || sRecursos == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento

                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);

                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    idUserDestino = int.Parse(aRecursos[i]);
                    USUARIO.CopiarPermisos(tr, int.Parse(sIdRecursoOrigen), idUserDestino);
                }//for

                Conexion.CommitTransaccion(tr);
            }
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar el usuario destino " + idUserDestino.ToString(), ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
