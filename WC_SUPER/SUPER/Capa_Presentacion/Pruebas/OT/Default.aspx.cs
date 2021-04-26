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

using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial;
    protected string sLectura;
    public string strTablaHTMLIntegrantes;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Miembros de la oficina técnica";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.bFuncionesLocales = true;

            if (!Page.IsPostBack)
            {
                try
                {
                    //Obtener los datos necesarios
//                    int iCodNodo = 65;// int.Parse(Session["NodoActivo"].ToString());
                    //Cargo la lista de integrantes que ya pertenezcan a este Nodo
                    //string strHTML = obtenerIntegrantesOTNodo(iCodNodo);
                    //divCatalogo2.InnerHtml = strHTML;

                    //txtApellido1.Focus();
                    throw (new Exception("Error de prueba"));
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
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    private string obtenerIntegrantesOTNodo(int nNodo)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        SqlDataReader dr = OFICINATECNICA.Catalogo(nNodo, null, 3, 0);

        sb.Append("<table id='tblOpciones2' class='texto' style='WIDTH: 390px;' cellSpacing='0' border='0'>" + (char)10);
        sb.Append("<colgroup><col style='width:10px;' /><col style='width:380px;padding-left:5px' /></colgroup>" + (char)10);
        sb.Append("<tbody id='tbodyDestino'>" + (char)10);
        while (dr.Read())
        {
            if (i % 2 == 0) sb.Append("<tr class='FA ");
            else sb.Append("<tr class='FB ");
            i++;

            sb.Append("MANO' id='" + dr["t314_idusuario"].ToString() + "' onClick='mm(this)' onmousedown='DD(this)'>");
            sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
            sb.Append("<td><label class='texto NBR W320'");// ondragstart='FromCtrl=this' ondragover='nIndiceDD=this.rowIndex;fnClone(this);'
            if (dr["Profesional"].ToString().Length > 80)
                sb.Append(" title='" + dr["Profesional"] + "'");

            sb.Append("><NOBR>" + dr["Profesional"] + "</NOBR></label></td></tr>" + (char)10);
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>" + (char)10);
        sb.Append("</table>" + (char)10);

        return sb.ToString();
    }
    private string ObtenerPersonas(string sAP1, string sAP2, string sNom)
    {// Devuelve el código HTML del catalogo de tareas de la plantilla que se pasa por parámetro
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            string sV1 = Utilidades.unescape(sAP1);
            string sV2 = Utilidades.unescape(sAP2);
            string sV3 = Utilidades.unescape(sNom);
            //Los foráneos no pueden ser miembros de la Oficina Técnica
            SqlDataReader dr = Recurso.Catalogo(sV1, sV2, sV3, false);

            sb.Append("<table id='tblOpciones' class='texto' style='WIDTH: 390px;' cellSpacing='0' border='0'>" + (char)10);
            sb.Append("<colgroup><col style='padding-left:5px' /></colgroup>" + (char)10);
            sb.Append("<tbody id='tbodyOrigen'>" + (char)10);
            while (dr.Read())
            {
                if (i % 2 == 0) sb.Append("<tr class='FA ");
                else sb.Append("<tr class='FB ");
                i++;
                sb.Append("MANO' id='" + dr["codigo"].ToString() + "' onClick='mm(this)' onDblClick='convocar(this.id,children[0].innerText,true);' onmousedown='DD(event)' onmouseover='TTip(event)'>");
                sb.Append("<td><label class='texto NBR W320'><NOBR>" + dr["descripcion"] + "</NOBR></label></td></tr>" + (char)10);
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>" + (char)10);
            sb.Append("</table>" + (char)10);

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los profesionales", ex);
            return "error@#@";
        }
    }
    private string Grabar(string sCadena)
    {//En el parametro de entrada tenemos una lista de codigos de personas separados por comas 
        string sResul = "";
        int iCodCR;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            //Recojo el código de CR
            iCodCR = 65; // int.Parse(Session["NodoActivo"].ToString());
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

            //Borrar los integrantes existentes
            //OfiTec.BorrarIntegrantes(iCodCR);
            OFICINATECNICA.DeleteByT303_idnodo(tr, iCodCR);

            if (sCadena != "")
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aProf = Regex.Split(sCadena, @",");

                for (int i = 0; i < aProf.Length - 1; i++)
                {
                    OFICINATECNICA.Insert(tr, iCodCR, int.Parse(aProf[i]));
//                    OfiTec.InsertarIntegrante(tr, iCodCR, int.Parse(sCad));
                }//for
            }
            //Cierro transaccion
            Conexion.CommitTransaccion(tr);
            //sCad = OfiTec.ObtenerIntegrantes(iCodCR);
            //sResul = "OK@#@" + strTablaHTMLIntegrantes;
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
}
