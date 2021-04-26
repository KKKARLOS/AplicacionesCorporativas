using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;
using System.Text;
using EO.Web;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de los contratos", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("Pool"):
                sResultado += obtenerFigurasItem();
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al contrato.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al contrato.
        return _callbackResultado;
    }
    private string obtenerFigurasItem()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURAPSN_CONTRATO_POOL.CatalogoFiguras();
            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='width: 420px;' mantenimiento='1' cellpadding='0'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 20px' /><col style='width: 280px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody>");
            int nUsuario = 0;
            bool bHayFilas = false;
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");
                i++;
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:20px;' onclick='mm(event)' onmousedown='DD(event);' ");
                    //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIM.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPM.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEM.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFM.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td><td><nobr class='NBR W280'>" + dr["Profesional"].ToString() + "</nobr></td>");

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
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='J' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='B' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
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

            return "OK@#@" + sb.ToString() + "@#@" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='WIDTH: 400px; BORDER-COLLAPSE: collapse;table-layout:fixed;' cellSpacing='0' cellPadding='0' border='0' >");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px; noWrap:true;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string Grabar(string strFiguras)
    {
        string sResul = "";
        int nID = -1;
        //string[] aDatosBasicos = null;


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
            #region Datos Figuras
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
                        FIGURAPSN_CONTRATO_POOL.DeleteUsuario(tr, int.Parse(aFig[1]));
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
                                FIGURAPSN_CONTRATO_POOL.Delete(tr, int.Parse(aFig[1]), aFig2[1]);
                            else
                                FIGURAPSN_CONTRATO_POOL.Insert(tr, int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del contrato", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
