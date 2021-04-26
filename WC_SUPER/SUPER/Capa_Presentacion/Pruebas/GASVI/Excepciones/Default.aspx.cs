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
    protected string sErrores, sLectura = "false";
    public string strTablaHTMLIntegrantes;
    protected string strInicial;

    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //Cargo la lista de integrantes de ese pool
                    strTablaHTMLIntegrantes = ObtenerIntegrantes();
                    txtApellido1.Focus();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
    private string ObtenerPersonas(string sAP1, string sAP2, string sNom)
    {
        StringBuilder sb = new StringBuilder();
        string sDes;
        try
        {
            string sV1 = Utilidades.unescape(sAP1);
            string sV2 = Utilidades.unescape(sAP2);
            string sV3 = Utilidades.unescape(sNom);
            SqlDataReader dr = Recurso.ObtenerProfesionales(sV1, sV2, sV3);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 380px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:360px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sDes = dr["TECNICO"].ToString();
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'");

                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["TECNICO"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["NUM_EMPLEADO"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" onclick='mm(event)' onDblClick='convocar(this.id,this.cells[1].innerText,true);' style='height:20px' onmousedown='DD(event)'");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") 
                //    sb.Append("tipo='E' ");
                //else 
                //    sb.Append("tipo='I' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("><td></td><td><label class=texto id='lbl' style='width:355px;text-overflow:ellipsis;overflow:hidden'");
                if (sDes.Length > 80)
                    sb.Append(" title='" + sDes + "'");
                sb.Append("><NOBR>" + sDes + "</NOBR></label></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); dr.Dispose();
            //this.strTablaHTMLPersonas = sb.ToString();
            return "OK@#@"+sb.ToString();
        }
        catch (Exception ex)
        {
            //return "Error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
            sErrores += Errores.mostrarError("Error al obtener los profesionales", ex);
            return "Error@#@Error al obtener los profesionales";
        }
    }
    private string ObtenerIntegrantes()
    {// Devuelve el código HTML del catalogo de personas que son excepciones al envío de correo
        StringBuilder sb = new StringBuilder();
        string sCod, sDes;
        try
        {
            SqlDataReader dr = VGNOENVIAR.Catalogo();
            sb.Append("<table id='tblOpciones2' class='texto MM' style='WIDTH: 390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:360px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sDes = dr["profesional"].ToString();
                sCod = dr["t001_idficepi"].ToString();
                sb.Append("<tr id='" + sCod + "' bd='' onClick='mm(event)' style='height:20px' onmousedown='DD(event)'");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");

                if (dr["t001_fecbaja"].ToString() == "")
                    sb.Append("baja='N' ");
                else
                {
                    if (System.Convert.ToDateTime(dr["t001_fecbaja"].ToString()) < System.DateTime.Today)
                        sb.Append("baja='S' ");
                    else
                        sb.Append("baja='N' ");
                }

                if (dr["T001_TIPORECURSO"].ToString() == "I") sb.Append("tipo='I' ");
                else sb.Append("tipo='E' ");
                sb.Append("><td></td><td></td>");
                sb.Append("<td><NOBR id='lbl" + sCod + "' class='NBR W350' title='" + sDes + "'>" + sDes + "</NOBR></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close();
            dr.Dispose();

            return sb.ToString();
        }
        catch (Exception)
        {
            //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@";
        }
    }
    private string Grabar(string sCadena)
    {//En el parametro de entrada tenemos una lista de codigos de personas separados por comas 
        string  sCad, sResul = "",sProf, sOp;
        //short iCodCR;
        int idFicepi;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
            if (sCadena != "")
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aProf = Regex.Split(sCadena, @"##");
                for (int i = 0; i < aProf.Length - 1; i++)
                {
                    sCad = aProf[i];
                    if (sCad != "")
                    {
                        string[] aTareas = Regex.Split(sCad, @",");
                        sOp = aTareas[0];
                        sProf = aTareas[1];
                        if (sProf != "")
                        {
                            idFicepi = int.Parse(sProf);
                            switch (sOp)
                            {
                                case "I":
                                    VGNOENVIAR.Insert(tr, idFicepi);
                                    break;
                                case "D":
                                    VGNOENVIAR.Delete(tr, idFicepi);
                                    break;
                            }
                        }
                    }
                }//for
            }
            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@" + strTablaHTMLIntegrantes;
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
}
