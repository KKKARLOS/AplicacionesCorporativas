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
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strTablaHtml = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.TituloPagina = "Asignación de calendario a profesional";

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
            case "grabar":
                sResultado += Grabar(aArgs[1]);
                break;
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3]);
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
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr;
            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "")
                dr = USUARIO.ObtenerProfesionalesCalendarioUsu((int)Session["UsuarioActual"], Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom));
            else
                dr = USUARIO.ObtenerProfesionalesCalendario(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom));

            sb.Append("<table id='tblDatos' class='texto MA' style='width:600px;'>");
            sb.Append("<colgroup><col style='width:300px;' /><col style='width:300px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("cal='" + dr["t066_idcal"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("bd='' onClick='ms(this)' ondblclick='getCal(this);' style='height:16px'>");
                //sb.Append("<td><nobr class='NBR W290' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" onDblClick='getCal(this.parentNode.parentNode);' >" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:5px'><nobr class='NBR W290' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" onDblClick='getCal(this.parentNode.parentNode);' >" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t066_descal"].ToString() + "</td></tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
            return sResul;
        }
    }

    protected string Grabar(string strDatos)
    {
        string sResul = "";
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

        try
        {
            string[] aProfCal = Regex.Split(strDatos, "///");

            foreach (string oProfCal in aProfCal)
            {
                if (oProfCal == "") break;
                string[] aDatos = Regex.Split(oProfCal, "##");
                ///aDatos[0] = num_empleado;
                ///aDatos[1] = calendario;

                Recurso.ActualizarCalendario(tr, int.Parse(aDatos[0]), int.Parse(aDatos[1])); 
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + ID.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de los calendarios", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
