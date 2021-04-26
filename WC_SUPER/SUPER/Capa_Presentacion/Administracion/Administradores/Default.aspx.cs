using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
//using System.Web.UI.WebControls;
using EO.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaAdmin;
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de administradores";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                string strTabla0 = obtenerAdministradores();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaAdmin = aTabla0[1];
                else Master.sErrores = aTabla0[1];
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los profesionales", ex);
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
        
        //Session.Clear();
        //Session.Abandon();

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "profesionales":
                sResultado += obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]));
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
    private string obtenerAdministradores()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = ADMINISTRADORES.Catalogo();
            sb.Append("<TABLE id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:11px;' /><col style='width: 20px' /><col style='width: 300px;' /><col style='width: 40px;' /><col style='width: 40px;' /><col style='width: 40px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' style='height:20px;' tipo='" + dr["tipo"].ToString() + "'");
                sb.Append(" onmousedown='DD(event)' onclick='mm(event)'>");
                sb.Append("<td style='padding-left:1px'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td align='center'>");
                if (dr["t001_sexo"].ToString() == "V")
                {
                    switch(dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='../../../images/imgUsuPV.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../images/imgUsuFV.gif'>");
                            break;
                    }
                }
                else
                {
                    switch (dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='../../../images/imgUsuPM.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='../../../images/imgUsuEM.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../images/imgUsuFM.gif'>");
                            break;
                    }
                }
                sb.Append("</td>");
                sb.Append("<td style='text-align:left;' ><nobr class='NBR' style='width:295px'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><input type='checkbox' class='check' onclick='exclusivo(this,1)' ");
                if (dr["t399_figura"].ToString() == "A") sb.Append("checked=true");
                sb.Append("></td>");
                sb.Append("<td><input type='checkbox' class='check' onclick='exclusivo(this,2)' ");
                if (dr["t399_figura"].ToString() == "S") sb.Append("checked=true");
                sb.Append("></td>");
                sb.Append("<td><input type='checkbox' class='check' onclick='exclusivo(this,3)' ");
                if (dr["t399_figura"].ToString() == "P") sb.Append("checked=true");
                sb.Append("></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de administradores.", ex);
        }
    }
    private string obtenerProfesionales(string sAp1, string sAp2, string sNombre)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = USUARIO.GetProfAdm(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                    false, null);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH:450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;text-align:left' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='addItem(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer los profesionales ", objError);
        }
        return sResul;
    }
    private string Grabar(string strProfesionales)
    {
        string sResul = "", sElementosInsertados = "";

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
            #region Datos Profesionales
            if (strProfesionales != "")//No se ha modificado nada de la pestaña de Figuras
            {
                string[] aProfesionales = Regex.Split(strProfesionales, "///");
                foreach (string oProfesional in aProfesionales)
                {
                    if (oProfesional == "") continue;
                    string[] aValores = Regex.Split(oProfesional, "##");
                    ///aValores[0] = bd
                    ///aValores[1] = idFicepi
                    ///aValores[2] = 'A' es Administrador - 'S' Es SuperAdministrador - 'P' es Administrador de Personal

                    switch (aValores[0])
                    {
                        case "I":
                            ADMINISTRADORES.Insert(tr, int.Parse(aValores[1]), aValores[2]);
                            if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                            else sElementosInsertados += "//" + aValores[1];
                            break;
                        case "U":
                            ADMINISTRADORES.Update(tr, int.Parse(aValores[1]), aValores[2]);
                            break;
                        case "D":
                            ADMINISTRADORES.Delete(tr, int.Parse(aValores[1]));
                            break;
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del profesional", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
