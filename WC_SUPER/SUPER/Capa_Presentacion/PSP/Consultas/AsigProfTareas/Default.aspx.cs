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
using System.Text;


public partial class ConsultaTecnicoMasiva : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 0;
            Master.TituloPagina = "Consulta de tareas asignadas a un profesional";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

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
            case ("VI"):
                sResultado += obtenerProfesionalesVI(aArgs[1]);
                break;
            case "profesionales":
                sResultado += obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), aArgs[4]);
                break;
            case "GF":
                sResultado += obtenerProfesionalesGF(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case ("PSN"):
                sResultado += obtenerProfesionalesPSN(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case ("getExcel"):
                sResultado += ObtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;

            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
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

    private string obtenerProfesionales(string sAp1, string sAp2, string sNombre, string sBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool bMostrarBajas = false;
        try
        {
            if (sBajas == "1")
                bMostrarBajas = true;

            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.GetProfAdm(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                        bMostrarBajas, null);
            else
                dr = USUARIO.GetProfVisibles((int)Session["UsuarioActual"], null,
                                                       Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                                       bMostrarBajas);

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
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
    private string obtenerProfesionalesGF(int iGF, string mostrarBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.GetProfGFAdm(iGF, ((mostrarBajas == "1") ? true : false));
            else
                dr = USUARIO.GetProfGFVisibles((int)Session["UsuarioActual"], iGF, ((mostrarBajas == "1") ? true : false));

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
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
    private string obtenerProfesionalesVI(string strEstado)
    {//strEstado T-> todos, A-> activos, B-> de baja
        string sResul = "";
        bool bMostrarBajas=true;
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;

        try
        {
            if (strEstado=="A") bMostrarBajas=false;
            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.GetProfAdm("", "", "", bMostrarBajas, null);
            else
                dr = USUARIO.GetProfVisibles(int.Parse(Session["UsuarioActual"].ToString()), null, "", "", "", bMostrarBajas);

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10); 
            while (dr.Read())
            {
                if (strEstado == "B")//Solo mostrar profesionales de baja
                {
                    if ((int)dr["baja"]==1)
                    {
                        sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                        //if (dr["t303_denominacion"].ToString() == "")
                        //    sb.Append(" tipo ='E'");
                        //else
                        //    sb.Append(" tipo ='I'");
                        sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                        sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                        sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                        //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                        sb.Append("style='height:20px'>");
                        sb.Append("<td></td>");
                        //sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                        sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                    }
                }
                else
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                    //if (dr["t303_denominacion"].ToString() == "")
                    //    sb.Append(" tipo ='E'");
                    //else
                    //    sb.Append(" tipo ='I'");
                    sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                    sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                    sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                    //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                    sb.Append("style='height:20px'>");
                    sb.Append("<td></td>");
                    //sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                    sb.Append("</tr>" + (char)10);
                }
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria

            return sResul;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de proyectos", ex);
        }

        return sResul;

    }
    private string obtenerProfesionalesPSN(int iPSN, string mostrarBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            SqlDataReader dr;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                dr = USUARIO.GetProfPROYAdm(iPSN, ((mostrarBajas == "1") ? true : false));
            else
                dr = USUARIO.GetProfPROYVisibles((int)Session["UsuarioActual"], iPSN, ((mostrarBajas == "1") ? true : false));

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
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

    private string ObtenerDatos(string sEstadoProyEcon, string sEstadoProyTec, string sEstadoTarea, string sProfesionales)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;
//        DateTime dtDesde, dtHasta;

        try
        {
            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.ProfesionalesTareas(0, sEstadoProyEcon, sEstadoProyTec, sEstadoTarea , sProfesionales);
            else
                dr = USUARIO.ProfesionalesTareas(int.Parse(Session["UsuarioActual"].ToString()),sEstadoProyEcon, sEstadoProyTec, sEstadoTarea , sProfesionales);

            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("<tr align=center style='background-color: #BCD4DF;'>");
            sb.Append("<td>Nº Usuario</td>");

            sb.Append("<td>Activo en tarea</td>");

            sb.Append("<td>Profesional</td>");
            sb.Append("<td>Nº proyecto</td>");

            sb.Append("<td>Estado proyecto</td>");

            sb.Append("<td>Denominación proyecto</td>");
            sb.Append("<td>Denominación proyecto técnico</td>");
            sb.Append("<td>Fase</td>");
            sb.Append("<td>Actividad</td>");
            sb.Append("<td>Nº tarea</td>");

            sb.Append("<td>Estado tarea</td>");

            sb.Append("<td>Denominación tarea</td>");
            sb.Append("<td>Perfil</td>");
            sb.Append("<td>Fecha Inicio Prev.</td>");
            sb.Append("<td>Fecha Fin Prev.</td>");

            sb.Append("<td>Fecha Inicio Vigencia</td>");
            sb.Append("<td>Fecha Fin Vigencia</td>");

            sb.Append("</tr>");
            sb.Append("</table>");

            sb.Append("<table style='font-family:Arial;font-size:8pt;' border=1>");
            sb.Append("<colgroup>");
            sb.Append("<col />");//Nº Usuario
            sb.Append("<col />");//Activo en tarea
            sb.Append("<col />");//Profesional
            sb.Append("<col />");//Nº proyecto
            sb.Append("<col />");//Estado proyecto
            sb.Append("<col />");//proyecto
            sb.Append("<col />");//PT
            sb.Append("<col />");//Fase
            sb.Append("<col />");//Actividad
            sb.Append("<col />");//Nº tarea
            sb.Append("<col />");//Estado tarea
            sb.Append("<col />");//Denominación tarea
            sb.Append("<col />");//Perfil
            sb.Append("<col />");//F.Inicio.Prevista
            sb.Append("<col />");//F.Fin.Prevista
            sb.Append("<col />");//FIV
            sb.Append("<col />");//FFV
            sb.Append("</colgroup>");
            string sFecha = "";

            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align:top;'>");

                sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("###,###") + "</td>");

                if ((bool)dr["t336_estado"])
                    sb.Append("<td>SI</td>");
                else
                    sb.Append("<td>NO</td>");

                sb.Append("<td>" + dr["profesional"].ToString() + "</td>");

                sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("###,###") + "</td>");

                switch (dr["t301_estado"].ToString())
                {
                    case "A":
                        sb.Append("<td>ABIERTO</td>");
                        break;
                    case "C":
                        sb.Append("<td>CERRADO</td>");
                        break;
                    case "P":
                        sb.Append("<td>PRESUPUESTADO</td>");
                        break;
                    case "H":
                        sb.Append("<td>HISTORICO</td>");
                        break;
                    default:
                        sb.Append("<td></td>");
                        break;
                }
                sb.Append("<td>&nbsp;" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["T331_despt"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["t334_desfase"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["t335_desactividad"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("###,###") + "</td>");
                switch (dr["t332_estado"].ToString())
                {
                    case "0":
                        sb.Append("<td>PARALIZADA</td>");
                        break;
                    case "1":
                        sb.Append("<td>ACTIVA</td>");
                        break;
                    case "2":
                        sb.Append("<td>PENDIENTE</td>");
                        break;
                    case "3":
                        sb.Append("<td>FINALIZADA</td>");
                        break;
                    case "4":
                        sb.Append("<td>CERRADA</td>");
                        break;
                    case "5":
                        sb.Append("<td>ANULADA</td>");
                        break;
                    default:
                        sb.Append("<td></td>");
                        break;
                }
                sb.Append("<td>&nbsp;" + dr["t332_destarea"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["t333_denominacion"].ToString() + "</td>");

                if (dr["t336_fip"] == System.DBNull.Value) sFecha = "";
                else sFecha = ((DateTime)dr["t336_fip"]).ToShortDateString();
                sb.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                if (dr["t336_ffp"] == System.DBNull.Value) sFecha = "";
                else sFecha = ((DateTime)dr["t336_ffp"]).ToShortDateString();
                sb.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                if (dr["t332_fiv"] == System.DBNull.Value) sFecha = "";
                else sFecha = ((DateTime)dr["t332_fiv"]).ToShortDateString();
                sb.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                if (dr["t332_ffv"] == System.DBNull.Value) sFecha = "";
                else sFecha = ((DateTime)dr["t332_ffv"]).ToShortDateString();
                sb.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            sResul= "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria

            return sResul;

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de las tareas ligadas a profesionales", ex);
        }

        return sResul;

    }

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar el proyecto", ex);
        }
        return sResul;
    }
    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy2(int.Parse(sT305IdProy));
            if (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4
                sb.Append(sT305IdProy + "@#@");  //5
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //6
                sb.Append(dr["responsable"].ToString() + "@#@");  //7
                if ((bool)dr["t320_facturable"]) sb.Append("1@#@");  //8
                else sb.Append("0@#@");  //8
                sb.Append(dr["t302_denominacion"].ToString() + "@#@");  //9
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //10
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }

}
