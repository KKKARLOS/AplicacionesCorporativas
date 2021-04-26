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
using EO.Web; 
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class ConsultaprofesionalPGE : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    protected string sNodo;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
        if (!Page.IsCallback)
        {
            Master.nBotonera = 0;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);

            Master.TituloPagina = "Consulta masiva de consumos por profesional";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}

            hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
            txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
            hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
            txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.InnerText = sNodo;
            this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                cboCR.Visible = false;
                hdnIdNodo.Visible = true;
                txtDesNodo.Visible = true;
            }
            else
            {
                cboCR.Visible = true;
                hdnIdNodo.Visible = false;
                txtDesNodo.Visible = false;
                cargarNodos();
            }

            try
            {
                rdbAmbito.Items.Add(new ListItem("Nombre&nbsp;&nbsp;&nbsp;", "A"));
                rdbAmbito.Items.Add(new ListItem(sNodo + "&nbsp;&nbsp;&nbsp;", "G"));
                rdbAmbito.Items.Add(new ListItem("Proyecto", "V"));
                rdbAmbito.SelectedValue = "A";
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("PSN"):
                sResultado += obtenerProfesionalesPSN(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
                break;
            case "profesionales":
                sResultado += obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), aArgs[4]);
                break;
            case "CR":
                sResultado += obtenerProfesionalesCR(int.Parse(Utilidades.unescape(aArgs[1])), aArgs[2]);
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

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                dr = USUARIO.GetProfAdm(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                        bMostrarBajas, null);
            else
                dr = USUARIO.GetProfVisibles((int)Session["UsuarioActual"], null,
                                                       Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                                       bMostrarBajas);

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
    private string obtenerProfesionalesCR(int iCR, string mostrarBajas)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            SqlDataReader dr;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                dr = USUARIO.GetProfCRAdm(iCR,((mostrarBajas == "1") ? true : false));
            else
                dr = USUARIO.GetProfCRVisibles((int)Session["UsuarioActual"], iCR,((mostrarBajas == "1") ? true : false));

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
                if (dr["t303_denominacion"].ToString() == "")
                    sb.Append(" tipo ='E'");
                else
                    sb.Append(" tipo ='I'");
                //sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
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
    private string ObtenerDatosConsumos(string sDesde, string sHasta, string sProfesionales)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;

        try
        {
            SqlDataReader dr = 
                Consumo.ObtenerConsumosEconomicosProf(int.Parse(Session["UsuarioActual"].ToString()), 
                                                        int.Parse(sDesde), int.Parse(sHasta), sProfesionales);

            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("<tr align='center' style='background-color: #BCD4DF;'>");
            sb.Append("<td>Nº profesional</td>");
            sb.Append("<td>Profesional</td>");
            sb.Append("<td>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</td>");
            sb.Append("<td>Nº proyecto</td>");
            sb.Append("<td>Denominación proyecto</td>");
            sb.Append("<td>Mes consumo</td>");
            sb.Append("<td>Unidades</td>");
            sb.Append("<td>Modelo coste</td>");
            sb.Append("</tr></table>");

            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' border=1>");
            sb.Append("<colgroup>");
            sb.Append("<col />");
            sb.Append("<col />");//Profesional
            sb.Append("<col />");//Nodo
            sb.Append("<col />");//Nº proyecto
            sb.Append("<col />");//proyecto
            sb.Append("<col />");//mes consumo
            sb.Append("<col />");//unidades consumidas
            sb.Append("<col />");//modelo de coste
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align:top;'>");
                sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["tecnico"].ToString() + "</td>");
                sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + (dr["descAnomes"]).ToString() + "</td>");
                sb.Append("<td style='text-align:rigth;'>" + decimal.Parse(dr["unidades"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + (dr["ModeloCoste"]).ToString() + "</td>");
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
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta masiva de profesionales", ex);
        }

        return sResul;

    }

    private void cargarNodos()
    {
        int iNodos = 0;
        try
        {
            //Obtener los datos necesarios
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], false);
            //SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"],false);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboCR.Items.Add(oLI);
                iNodos++;
            }
            dr.Close();
            dr.Dispose();
            if (iNodos == 1)
            {//Si solo tiene acceso a un nodo, se lo pongo
                oLI.Selected = true;
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }

}
