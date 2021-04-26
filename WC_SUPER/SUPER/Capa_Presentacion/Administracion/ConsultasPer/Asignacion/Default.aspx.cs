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
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "", strTablaHTMLIntegrantes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            string sConsAux = Request.QueryString["nIdCons"];
            if (sConsAux != null) Master.nBotonera = 23;// grabar, regresar
            else Master.nBotonera = 9;// 
           
            Master.TituloPagina = "Asociación de profesionales a consultas personalizadas";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            if (!Page.IsPostBack)
            {
                try
                {
                    obtenerConsultas();
                    if (sConsAux != null)
                    {
                        this.hdnIdCons.Text = sConsAux;
                    }
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
                string sUrl = Request.QueryString["nIdCons"];
                if (sUrl != null)
                    sUrl = HistorialNavegacion.Leer() + "?nIdCons=" + sUrl;
                else
                    sUrl = HistorialNavegacion.Leer();

                try
                {
                    Response.Redirect(sUrl, true);
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
            case ("getProf"):
                sResultado += ObtenerIntegrantes(int.Parse(aArgs[1]));
                break;
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3]);
                break;
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

    private void obtenerConsultas()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CONSULTAPERSONAL.ObtenerCatalogo(null, true);

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 500px;'>");
            sb.Append("<colgroup><col style='width:500px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='"+dr["t472_idconsulta"].ToString()+"' onclick='ms(this);getProf(this.id)'>");
                //sb.Append("<td><input type='text' class='txtL' style='width:500px' value='" + dr["t472_denominacion"].ToString() + "' maxlength='50'></td></tr>");
                sb.Append("<td>" + dr["t472_denominacion"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            strTablaHTML= sb.ToString();
        }
        catch (Exception ex)
        {
            Errores.mostrarError("Error al cargar las consultas", ex);
        }
    }
    private string ObtenerPersonas(string sAP1, string sAP2, string sNom)
    {// Devuelve el código HTML para la lista de candidatos
        StringBuilder sb = new StringBuilder();
        string sCod, sDes, sV1, sV2, sV3;
        try
        {
            sV1 = Utilidades.unescape(sAP1);
            sV2 = Utilidades.unescape(sAP2);
            sV3 = Utilidades.unescape(sNom);
            SqlDataReader dr = USUARIO.GetProfAdm(sV1, sV2, sV3, false, null);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='width:350px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:330px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyOrigen'>");

            while (dr.Read())
            {
                sCod = dr["t314_idusuario"].ToString();
                sDes = dr["Profesional"].ToString().Replace((char)34, (char)39);

                sb.Append("<tr id='"+sCod+"'  style='height:20px'");

                //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("><td></td><td><nobr class='NBR W320'>" + sDes + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody></table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@Error al obtener las personas";
        }
    }
    public string ObtenerIntegrantes(int iCodCons)
    {// Devuelve el código HTML del catalogo de personas que están asociadas a la consulta que se pasa como parametro
        StringBuilder sb = new StringBuilder();
        string sCod, sDes;
        bool bActivo;
        try
        {
            SqlDataReader dr = CONSULTAPERSONAL.GetProf(null, iCodCons);
            sb.Append("<table id='tblOpciones2' class='texto MM' style='width:400px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:15px;' />");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:330px;' />");
            sb.Append(" <col style='width:35px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sCod = dr["t314_idusuario"].ToString();
                sDes = dr["nombre"].ToString();
                bActivo = bool.Parse(dr["t473_estado"].ToString());

                sb.Append("<tr id='" + sCod + "' bd='' onClick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sDes.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sDes.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append(" baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("><td></td><td></td><td><nobr class='NBR W320'>" + sDes + "</NOBR></label></td>");
                sb.Append("<td style='padding-rigth:10px;'><input type='checkbox' class='checkTabla'");
                if (bActivo) sb.Append(" checked='true' ");
                sb.Append(" onclick=\"activarGrabar();mfa(this.parentNode.parentNode,'U')\"></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception)
        {
            //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@";
        }
    }
    private string Grabar(string sConsulta, string sIntegrantes)
    {//En el primer parametro de entrada tenemos codCons#descCons 
        //y en el segundo una lista de codigos de personas separados por comas (persona#estado,)
        string sCad, sResul = "", sOperacion;
        int iCodCons, iEmpleado;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            iCodCons = int.Parse(sConsulta);
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

            if (sIntegrantes != "")
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aTareas = Regex.Split(sIntegrantes, @",");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    if (sCad != "")
                    {
                        string[] aElem = Regex.Split(sCad, @"##");
                        sOperacion = aElem[0];
                        iEmpleado = System.Convert.ToInt32(aElem[1]);
                        switch (sOperacion)
                        {
                            case "D":
                                USUARIO_CONSULTAPERSONAL.Delete(tr, iEmpleado, iCodCons);
                                break;
                            case "U":
                                USUARIO_CONSULTAPERSONAL.Update(tr, iEmpleado, iCodCons, (aElem[2] == "1") ? true : false);
                                break;
                            case "I":
                                USUARIO_CONSULTAPERSONAL.Insert(tr, iEmpleado, iCodCons, (aElem[2] == "1") ? true : false);
                                break;
                        }//switch
                    }
                }//for
            }
            //Cierro transaccion
            Conexion.CommitTransaccion(tr);
            //sCad = ObtenerIntegrantes(iCodCons);
            sResul = "OK@#@" + iCodCons;// +"@#@" + sCad.Substring(5);
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