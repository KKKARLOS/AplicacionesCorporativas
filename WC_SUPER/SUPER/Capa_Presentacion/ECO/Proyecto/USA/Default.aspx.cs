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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using EO.Web;
using System.Text;


public partial class USA : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaProyectos;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            //Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Asignación masiva de USA a proyectos";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            try
            {
                if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                {
                    //Si no es administrador es que es USA. por tanto hay que verificar si es SAT
                    //Pues en caso contrario hay que dejar la pantalla en modo lectura
                    if (USUARIO.bEsSAT(null, int.Parse(Session["UsuarioActual"].ToString())))
                        this.hdnLectura.Value = "N";
                }
                else
                    this.hdnLectura.Value = "N";
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("proyectos"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3]);
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
    private string Grabar(string sUserOrigen, string sUserDestino, string strProyectos)
    {
        string sResul = "", sTipo="";
        StringBuilder sb = new StringBuilder();
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
            if (sUserDestino != "" && strProyectos != "") 
            {
                string[] aProys = Regex.Split(strProyectos, "///");
                foreach (string oProy in aProys)
                {
                    if (oProy == "") continue;
                    string[] aValores = Regex.Split(oProy, "##");
                    ///aValores[0] = idproyecto
                    ///aValores[1] = tipo T-> titular, A-> Alternativo
                    ///aValores[2] = denProyecto
                    PROYECTO.UpdateUSA(tr, int.Parse(aValores[0]), aValores[1], int.Parse(sUserDestino));
                    if (aValores[1] == "T")
                        sTipo = "Usuario administración titular";
                    else
                        sTipo = "Usuario administración alternativo";
                    sb.Append(@"<tr><td style='padding-left:3px;'>" + aValores[2] + "</td><td>" + sTipo + "</td></tr>");
                }
                EnviarCorreoUsuarioSoporte(false, int.Parse(sUserOrigen), sb.ToString());
                EnviarCorreoUsuarioSoporte(true, int.Parse(sUserDestino), sb.ToString());
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string ObtenerProyectos(string sSAT, string sSAA, string sIdUser)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bSAT = false, bSAA = false;
        try
        {
            if (sSAT == "S") bSAT = true;
            if (sSAA == "S") bSAA = true;
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosUSA(int.Parse(sIdUser), bSAT, bSAA, int.Parse(Session["UsuarioActual"].ToString()));

            sb.Append("<table id='tblDatos' class='texto MANO' style='width:960px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:290px;' />");
            sb.Append("    <col style='width:290px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("sat='" + dr["t314_idusuario_SAT"].ToString() + "' ");
                sb.Append("saa='" + dr["t314_idusuario_SAA"].ToString() + "' ");

                sb.Append("style='height:20px' >");
                sb.Append("<td><input type='checkbox' style='width:15' class='checkTabla' checked='true' onclick='activarGrabar();'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W290' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W280' style='noWrap:true;'>" + dr["denSAT"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W280' style='noWrap:true;'>" + dr["denSAA"].ToString() + "</nobr></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos.", ex, false);
        }

        return sResul;
    }
    public static string EnviarCorreoUsuarioSoporte(bool bAlta, int nUser, string sCuerpo)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();
        
        string sAsunto = "", sTexto = "", sTO = "";

        try
        {
            sAsunto = "Soporte de administración de proyecto.";

            sTO = Recurso.CodigoRed(nUser);
            sTexto = flPonerCabecera(bAlta) + sCuerpo + flPonerPie();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo a usuarios de soporte administrativo.", ex);
        }
        return sResul;
    }

    private static string flPonerCabecera(bool bAlta)
    {
        StringBuilder sb = new StringBuilder();
        string webServer = "http://imagenes.intranet.ibermatica/SUPERNET/";
        if (bAlta)
            sb.Append(@"<BR>SUPER le informa de las siguientes asignaciones para el soporte administrativo de proyectos económicos:<BR><BR>");
        else
            sb.Append(@"<BR>SUPER le informa de las siguientes desasignaciones para el soporte administrativo de proyectos económicos:<BR><BR>");
        sb.Append(@"<table style='width: 700px; HEIGHT: 17px' cellSpacing='0'>");
        sb.Append(@"<colgroup><col style='width:500px;padding-left:3px;' /><col style='width:200px' /></colgroup>");
        sb.Append(@"<tr style='FONT-WEIGHT: bold; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + webServer + "fondoEncabezamientoListas.gif);");
        sb.Append(@" COLOR: #ffffff; FONT-FAMILY: Arial, Helvetica, sans-serif; '>");
        sb.Append(@"<td><b>&nbsp;Proyecto económico</b></td><td><b>Figura</b></td></TR></TABLE>");
        sb.Append(@"<div style='background-image:url(" + webServer + "imgFT16.gif); width:700px'>");
        sb.Append(@"<table style='width:700px;FONT-FAMILY: Arial;FONT-SIZE: 12px'>");
        sb.Append(@"<colgroup><col style='width:500px;' /><col style='width:200px' /></colgroup>");
        return sb.ToString();
    }
    private static string flPonerPie()
    {
        StringBuilder sb = new StringBuilder();
        string webServer = "http://imagenes.intranet.ibermatica/SUPERNET/";
        sb.Append(@"</table></div>");
        sb.Append(@"<table style='WIDTH: 700px; HEIGHT: 17px' cellSpacing='0' cellpadding='0' border='0'>");
        sb.Append(@"<tr style='FONT-WEIGHT: bold; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + webServer + "fondoEncabezamientoListas.gif);");
        sb.Append(@" COLOR: #ffffff; FONT-FAMILY: Arial, Helvetica, sans-serif; height:17px; '>");
        sb.Append(@"<td></td></TR></TABLE>");
        return sb.ToString();
    }
}
