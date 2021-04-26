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

using System.Text;
using System.Text.RegularExpressions;
using EO.Web;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    //public string sErrores = "";
    public string strTablaHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH:940px; table-layout:fixed;' mantenimiento='1'></table>";
//    SqlDataReader dr = null;
    private ArrayList aListCorreo = new ArrayList();
//    private string sMensaje = "";
    private string sTexto = "";
//    private string sAsunto = "";
//    private string sTO;
//    private string[] aCorreoUSA;
//    private string sCorreoUSA = "";
//    private int iCabecera = 0;
//    private string sUSA = "N";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                Master.bFuncionesLocales = true;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                switch (Request.QueryString["origen"])
                {
                    case "menu_agen":
                        Master.sbotonesOpcionOn = "4,38,18";
                        Master.sbotonesOpcionOff = "4";
                        break;
                    default:
                        Master.sbotonesOpcionOn = "4,38,18,6";
                        Master.sbotonesOpcionOff = "4";
                        break;
                }

                Master.TituloPagina = "Agenda del USA";
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                AGENDAUSA.CrearAgendaUSAAuto(null, (int)Session["UsuarioActual"], DateTime.Today.Year*100 + DateTime.Today.Month);

                int iFecha = DateTime.Now.Year * 100 + DateTime.Now.Month;

                hdnMesDesde.Text = iFecha.ToString();
                hdnMesHasta.Text = hdnMesDesde.Text;
                hdnMes.Text = hdnMesDesde.Text;

                //hdnProy.Text = Request.QueryString["nProy"].ToString();
                //hdnProyDen.Text = Request.QueryString["dProy"].ToString();
                //sUSA = Request.QueryString["USA"].ToString();

                if (Session["ID_PROYECTOSUBNODO"] != null && Session["ID_PROYECTOSUBNODO"].ToString() != "")
                {
                    hdnIdProyectoSubNodo.Text = Session["ID_PROYECTOSUBNODO"].ToString();
                    string[] aTabla = Regex.Split(obtenerAgenda(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString())), "@#@");
                    if (aTabla[0] == "OK")
                    {
                        strTablaHTML = aTabla[1];
                        txtNumPE.Text = aTabla[2];

                        txtDesPE.Text = aTabla[3];
                        hdnUSA.Text = aTabla[4];
                        hdnProyUSA.Text = aTabla[7];
                        hdnProyExternalizable.Text = aTabla[8];
                    }
                    else Master.sErrores += Errores.mostrarError(aTabla[1]);
                }
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2]);
                break;
            case ("getAgenda"):
                sResultado += obtenerAgenda(int.Parse(aArgs[1]));
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("ExportarExcel"):
                sResultado += ExportarExcel(aArgs[1], aArgs[2]);
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

    private string obtenerAgenda(int iPSN)
    {
        try
        {
            string sNumPE = "";
            string sDesc = "";
            string sUSA = "N", sProyUSA = "N", sProyExternalizable = "N";

            SqlDataReader dr = PROYECTO.fgGetDatosProy(iPSN);
            if (dr.Read())
            {
                sNumPE = int.Parse(dr["t301_idproyecto"].ToString()).ToString("###,###");
                sDesc = dr["t301_denominacion"].ToString();
                if (dr["t305_cualidad"].ToString() == "C" && dr["t301_estado"].ToString() == "A" && (Session["UsuarioActual"].ToString() == dr["t314_idusuario_SAT"].ToString() || Session["UsuarioActual"].ToString() == dr["t314_idusuario_SAA"].ToString()))
                    sUSA = "S";
                if (dr["t314_idusuario_SAT"].ToString() != "0" || dr["t314_idusuario_SAA"].ToString() != "0") sProyUSA = "S";
                sProyExternalizable = ((bool)dr["t301_externalizable"]) ? "S" : "N";  
            }

            StringBuilder sb = new StringBuilder();
            dr = AGENDAUSA.Catalogo(int.Parse(dr["t301_idproyecto"].ToString()));

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 940px; ' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:130px;' /><col style='width:200px;' /><col style='width:200px;' /><col style='width:200px;' /><col style='width:199px;' /><col style='width:1px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");
            int i = 0;
            while (dr.Read())
            {
                if (i == 0) 
                    hdnMesDesde.Text = dr["t641_anomes"].ToString();

                sb.Append("<tr id='" + dr["t641_idagendausa"].ToString() + "' bd='' onclick='mm(event)' onDblClick='detalle(this);' ");
                sb.Append(" anomes='" + dr["t641_anomes"].ToString());

                sb.Append("' style='height:20px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td style='padding-left:5px;'>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t641_anomes"].ToString())) + "</td>");

                sTexto = dr["t641_consumos"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br style='mso-data-placement:same-cell;'>").Replace(((char)34).ToString(), "&#34;");
                // El estilo mso-data-placement:same-cell hace que los br no generen celdas nuevas al exportar al excel.
                sb.Append("<td syle='text-align:left;'><nobr class='NBR W190' onDblClick='detalle(this.parentNode.parentNode);' style='noWrap:true;;height:16px;' ");
                if (dr["t641_consumos"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTexto + "] hideselects=[off]\" ");
                sb.Append(">" + sTexto + "</nobr></td>");
                
                sTexto = dr["t641_produccion"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br style='mso-data-placement:same-cell;'>").Replace(((char)34).ToString(), "&#34;");
                sb.Append("<td><nobr class='NBR W190' onDblClick='detalle(this.parentNode.parentNode);' style='noWrap:true;height:16px;' ");
                if (dr["t641_produccion"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTexto + "] hideselects=[off]\" ");
                sb.Append(">" + sTexto + "</nobr></td>");

                sTexto = dr["t641_facturacion"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br style='mso-data-placement:same-cell;'>").Replace(((char)34).ToString(), "&#34;");
                sb.Append("<td><nobr class='NBR W190' onDblClick='detalle(this.parentNode.parentNode);' style='noWrap:true;;height:16px;' ");
                if (dr["t641_facturacion"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTexto + "] hideselects=[off]\" ");
                sb.Append(">" + sTexto + "</nobr></td>");

                sTexto = dr["t641_otros"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br style='mso-data-placement:same-cell;'>").Replace(((char)34).ToString(), "&#34;");
                sb.Append("<td><nobr class='NBR W190' onDblClick='detalle(this.parentNode.parentNode);' style='noWrap:true;;height:16px;' ");
                if (dr["t641_otros"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTexto + "] hideselects=[off]\" ");
                sb.Append(">" + sTexto + "</nobr></td>");

                sb.Append("<td style='visibility:hidden;'>" + dr["t641_anomes"].ToString() + "</td>");
                sb.Append("</tr>");
                hdnMesHasta.Text = dr["t641_anomes"].ToString();
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sNumPE + "@#@" + sDesc + "@#@" + sUSA + "@#@" + hdnMesDesde.Text + "@#@" + hdnMesHasta.Text + "@#@" + sProyUSA + "@#@" + sProyExternalizable;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de la agenda", ex);
        }
    }
    protected string Grabar(int iProy, string strDatos)
    {
        string sResul = "", sElementosInsertados = "";
        int nAux = 0;

        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
             
        try
        {
            string[] aAgenda = Regex.Split(strDatos, "///");
            foreach (string oAgenda in aAgenda)
            {
                if (oAgenda == "") continue;
                string[] aValores = Regex.Split(oAgenda, "##");
                //0. Opcion BD. "I","D"
                //1. ID Agenda
                //2. Año-Mes

                switch (aValores[0])
                {
                    case "D":
                        AGENDAUSA.Delete(tr, int.Parse(aValores[1]));
                        break;
                    case "I":
                        nAux = AGENDAUSA.Insert(tr, iProy, int.Parse(aValores[2]), "", "", "", "");
                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la Agenda.", ex) + "@#@";
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "##");
                sb.Append(dr["t301_denominacion"].ToString() + "///");

            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }
    private string ExportarExcel(string sProyectos, string sAnomes)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {

            SqlDataReader dr = PROYECTOSUBNODO.ObtenerInformeUSAExcel(sProyectos, int.Parse(sAnomes));
            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("<tr align='center'>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Denominación de proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Responsable de proyecto</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cliente</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Tipo de facturación</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Mes</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Consumos</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Producción</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Facturación</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Otros</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Partida</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Comunicado</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>USA / Responsable comunicado</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº documentos</TD>");
            sb.Append("</TR>");

            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align:top;'>");
                if (dr["tipo"].ToString() == "A") sb.Append("<td>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                else sb.Append("<td></td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["ResponsableProyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["TipoFacturacion"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["Mes"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_consumos"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_produccion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_facturacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_otros"].ToString() + "</td>");

                sb.Append("<td>" + dr["partidas_comunicado"].ToString() + "</td>");
                sb.Append("<td>" + dr["desc_comunicado"].ToString() + "</td>");

                if (dr["tipo"].ToString() == "A")
                {
                    sb.Append("<td>USA: " + dr["SAT"].ToString());
                    if (dr["SAA"].ToString() != "")
                        sb.Append(" / " + dr["SAA"].ToString());
                    sb.Append("</td>");
                }
                else sb.Append("<td>RC: " + dr["usu_comunicado"].ToString() + "</td>");

                sb.Append("<td>" + dr["num_doc"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            //return "OK@#@" + sb.ToString();
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;
            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos mensuales de la agenda", ex);
        }

        return sResul;

    }

}
