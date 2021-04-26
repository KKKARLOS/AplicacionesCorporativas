using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
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
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            Master.nBotonera = 0;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);

            Master.TituloPagina = "Consulta masiva de consumos por profesional";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            Utilidades.SetEventosFecha(this.txtFechaInicio);
            Utilidades.SetEventosFecha(this.txtFechaFin);

            DateTime dHoy = DateTime.Now, dtAux;
            int nDias = dHoy.Day;
            dtAux = dHoy.AddDays(-nDias + 1);
            txtFechaInicio.Text = dtAux.ToShortDateString();
            dtAux = dtAux.AddMonths(1).AddDays(-1);
            txtFechaFin.Text = dtAux.ToShortDateString();

            //try
            //{
            //    rdbAmbito.Items.Add(new ListItem("Nombre&nbsp;&nbsp;&nbsp;", "A"));
            //    rdbAmbito.Items.Add(new ListItem("Grupo funcional&nbsp;&nbsp;&nbsp;", "G"));
            //    rdbAmbito.Items.Add(new ListItem("Ambito de visión", "V"));
            //    rdbAmbito.SelectedValue = "A";
            //}
            //catch (Exception ex)
            //{
            //    Master.sErrores = Errores.mostrarError("Error al obtener los profesionales", ex);
            //}
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
                sResultado += ObtenerDatos(aArgs[1]);
                break;
            case "profesionales":
                sResultado += obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), aArgs[4]);
                break;
            case "GF":
                sResultado += obtenerProfesionalesGF(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case "CR":
                sResultado += obtenerProfesionalesCR(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case "Proveedor":
                sResultado += obtenerProfesionalesProv(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case ("getExcel"):
                sResultado += ObtenerDatosConsumos(aArgs[1], aArgs[2], aArgs[3]);
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
            sResul = "Error@#@" + Errores.mostrarError("Error al leer los profesionales (GF) ", objError);
        }
        return sResul;
    }
    private string obtenerProfesionalesCR(int iCR, string mostrarBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.GetProfCRAdm(iCR, ((mostrarBajas == "1") ? true : false));
            else
                dr = USUARIO.GetProfCRVisibles((int)Session["UsuarioActual"],
                                                       iCR, ((mostrarBajas == "1") ? true : false));

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><span class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</span></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer los profesionales  (CR)", objError);
        }
        return sResul;
    }
    private string obtenerProfesionalesProv(int iProv, string mostrarBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            SqlDataReader dr;

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = USUARIO.GetProfProvAdm(iProv, ((mostrarBajas == "1") ? true : false));
            else
                dr = USUARIO.GetProfProvVisibles((int)Session["UsuarioActual"],
                                                       iProv, ((mostrarBajas == "1") ? true : false));

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><span class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["profesional"].ToString() + "</span></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer los profesionales (Proveedor)", objError);
        }
        return sResul;
    }
    private string ObtenerDatos(string strEstado)
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
                       /* if (dr["t303_denominacion"].ToString() == "")
                            sb.Append(" tipo ='E'");
                        else
                            sb.Append(" tipo ='I'");*/
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
                }
                else
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                   /* if (dr["t303_denominacion"].ToString() == "")
                        sb.Append(" tipo ='E'");
                    else
                        sb.Append(" tipo ='I'");*/
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
    private string ObtenerDatosConsumos(string sDesde, string sHasta, string sProfesionales)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;
        DateTime dtDesde, dtHasta;
        bool bError = false;

        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                dtDesde = System.Convert.ToDateTime(sDesde);
                dtHasta = System.Convert.ToDateTime(sHasta);
                SqlDataReader dr = USUARIO.GetConsumosProf(int.Parse(Session["UsuarioActual"].ToString()), dtDesde, dtHasta, sProfesionales);

                sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
                sb.Append("<tr align=center style='background-color: #BCD4DF;'>");
                sb.Append("<td>F. consumo</td>");
                sb.Append("<td>Nº profesional</td>");
                sb.Append("<td>Profesional</td>");
                sb.Append("<td>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " del Profesional</td>");
                sb.Append("<td>Proveedor</td>");
                sb.Append("<td>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " del Proyecto</td>");
                sb.Append("<td>Nº proyecto</td>");
                sb.Append("<td>Denominación proyecto</td>");
                sb.Append("<td>Denominación proyecto técnico</td>");
                sb.Append("<td>Nº tarea</td>");
                sb.Append("<td>Denominación tarea</td>");
                sb.Append("<td>Facturable</td>");
                sb.Append("<td>Horas</td>");
                sb.Append("<td>Jornadas</td>");
                sb.Append("<td>Comentario</td>");
                sb.Append("<td>Cliente</td>");
                sb.Append("<td>Naturaleza</td>");
                sb.Append("<td>Modelo de contratación</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' border=1>");
                sb.Append("<COLGROUP>");
                sb.Append("<col />");//Fecha consumo
                sb.Append("<col />");
                sb.Append("<col />");//Profesional
                sb.Append("<col />");//Nodo del profesional
                sb.Append("<col />");//Proveedor del profesional
                sb.Append("<col />");//Nodo del proyecto
                sb.Append("<col />");//Nº proyecto
                sb.Append("<col />");//proyecto
                sb.Append("<col />");//PT
                sb.Append("<col />");//Nº tarea
                sb.Append("<col />");
                sb.Append("<col />");//facturable
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("</COLGROUP>");
                while (dr.Read())
                {
                    sb.Append("<tr style='vertical-align:top;'>");
                    sb.Append("<td>" + ((DateTime)dr["t337_fecha"]).ToShortDateString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["profesional"].ToString() + "</td>");
                    //CR del profesional
                    sb.Append("<td>" + dr["Nodo_Profesional"].ToString() + "</td>");
                    //Proveedor del profesional
                    sb.Append("<td>" + dr["Proveedor_Profesional"].ToString() + "</td>");
                    //CR del proyecto
                    sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["T331_despt"].ToString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t332_idtarea"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t332_destarea"].ToString() + "</td>");
                    if ((bool)dr["t332_facturable"])
                        sb.Append("<td style='text-align:center;'>X</td>");
                    else
                        sb.Append("<td>&nbsp;</td>");
                    //sb.Append("<td style='text-align:rigth;'>" + decimal.Parse(dr["TotalHorasReportadas"].ToString()).ToString("N") + "</td>");
                    //sb.Append("<td style='text-align:rigth;'>" + decimal.Parse(dr["TotalJornadasReportadas"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + dr["TotalHorasReportadas"].ToString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + double.Parse(dr["TotalJornadasReportadas"].ToString()).ToString("#,###.##") + "</td>");
                    sb.Append("<td>&nbsp;" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t323_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t316_denominacion"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                //sResul = "OK@#@" + sb.ToString();
                string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
                Session[sIdCache] = sb.ToString(); ;

                sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
                sb.Length = 0; //Para liberar memoria
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta masiva de profesionales", ex);
        }

        return sResul;

    }

}
