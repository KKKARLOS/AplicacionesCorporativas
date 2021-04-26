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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "", strTablaHTMLFijos="";
    public string sErrores = "";
    public string sMonedaProyecto = "", sMonedaImportes = "";
    public string sLectura = "false";
    public string sLecturaInsMes = "false";

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

                if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                {
                    this.cldAuditoria.Visible = false;
                    this.btnAuditoria.Visible = false;
                }

                if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) sLecturaInsMes = "true";
                cargarCombos(Request.QueryString["G"], Request.QueryString["S"], Request.QueryString["C"], Request.QueryString["CL"], Request.QueryString["T"], Request.QueryString["sCualidad"], Request.QueryString["sAnnoPIG"], Request.QueryString["sEsReplicable"]);

                #region Monedas y denominaciones
                sMonedaProyecto = Session["MONEDA_PROYECTOSUBNODO"].ToString();
                lblMonedaProyecto.InnerText = MONEDA.getDenominacion(Session["MONEDA_PROYECTOSUBNODO"].ToString());

                if (Session["MONEDA_VDP"] == null)
                {
                    sMonedaImportes = sMonedaProyecto;
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(sMonedaImportes);
                }
                else
                {
                    sMonedaImportes = Session["MONEDA_VDP"].ToString();
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString());
                }
                #endregion

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    divMonedaImportes.Style.Add("visibility", "visible");

                string strTabla = getDatosEconomicos(Request.QueryString["G"], Request.QueryString["S"], Request.QueryString["C"], 
                                                     Request.QueryString["CL"], Request.QueryString["T"], Request.QueryString["nSegMesProy"], 
                                                     Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], 
                                                     Request.QueryString["sCualidad"], sMonedaProyecto, sMonedaImportes);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                {
                    this.strTablaHTML = aTabla[1];
                    //this.strTablaHTMLFijos = aTabla[2];
                    lblImporteParcial.InnerText = aTabla[3];
                }
                else sErrores = aTabla[1];

                switch (Request.QueryString["T"]){
                    case "C":
                        divCabecera.InnerHtml = @"<table style='width: 960px; height: 17px; margin-top:5px; text-align:left'>
                                                <colgroup>
                                                    <col style='width:280px;'/>
                                                    <col style='width:280px;'/>
                                                    <col style='width:150px;'/>
                                                    <col style='width:150px;'/>
                                                    <col style='width:100px;'/>
                                                </colgroup>
                                                <tr class='TBLINI'>
                                                <td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=" + (char)34 + @"ot('tblDatos', 3, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 3, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + @"</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img4' border='0'><MAP name='img4'><AREA onclick=" + (char)34 + @"ot('tblDatos', 4, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 4, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Proveedor</td>
                                                <td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img5' border='0'><MAP name='img5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 5, 0, 'num')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 5, 1, 'num')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</td>
                                                </tr>
                                                </table>";
                        break;
                    case "P":
                        divCabecera.InnerHtml = @"<table style='width: 960px; height: 17px; margin-top:5px; text-align:left'>
                                                <colgroup>
                                                    <col style='width:430px;'/>
                                                    <col style='width:430px;'/>
                                                    <col style='width:100px;'/>
                                                </colgroup>
                                                <tr class='TBLINI'>
                                                <td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</td>
                                                <td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=" + (char)34 + @"ot('tblDatos', 3, 0, 'num')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 3, 1, 'num')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</td>
                                                </tr>
                                                </table>";
                        break;
                    default:
                        divCabecera.InnerHtml = @"<table style='width: 960px; height: 17px; margin-top:5px;'>
                                                <colgroup>
                                                    <col style='width:355px;'/>
                                                    <col style='width:75px;'/>
                                                    <col style='width:75px;'/>
                                                    <col style='width:75px;'/>
                                                    <col style='width:280px;'/>
                                                    <col style='width:100px;'/>
                                                </colgroup>
                                                <tr class='TBLINI'>
                                                <td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 1, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 0, 'fec')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 2, 1, 'fec')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Fecha</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=" + (char)34 + @"ot('tblDatos',3, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 3, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Serie</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img4' border='0'><MAP name='img4'><AREA onclick=" + (char)34 + @"ot('tblDatos', 4, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 4, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Nº Factura</td>
                                                <td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img5' border='0'><MAP name='img5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 5, 0, '')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 5, 1, '')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</td>
                                                <td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img6' border='0'><MAP name='img6'><AREA onclick=" + (char)34 + @"ot('tblDatos', 6, 0, 'num')" + (char)34 + @" shape='RECT' coords='0,0,6,5'><AREA onclick=" + (char)34 + @"ot('tblDatos', 6, 1, 'num')" + (char)34 + @" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</td>
                                                </tr>
                                                </table>";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getSE"):
                sResultado += getSE(aArgs[1]);
                break;
            case ("getCE"):
                sResultado += getCE(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("getDatosEco"):
                sResultado += getDatosEconomicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            case ("getMesesProy"):
                sResultado += getMesesProy(aArgs[1]);
                break;
            case ("addMesesProy"):
                sResultado += addMesesProy(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getClaseUnica"):
                sResultado += getClaseUnica(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case "getDisponibilidadFra":
                sResultado += getDisponibilidadFra(aArgs[1]);
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

    public void cargarCombos(string sG, string sS, string sC, string sCL, string sT, string sCualidad, string sAnnoPIG, string sEsReplicable)
    {
        ListItem oLI;
        SqlDataReader dr = GRUPOECO.Catalogo(null, "", null, "", 3, 0);
        while (dr.Read())
        {
            oLI = new ListItem(dr["t326_denominacion"].ToString(),dr["t326_idgrupoeco"].ToString());
            oLI.Attributes.Add("sTipo", dr["t326_tipogrupo"].ToString());

            if (dr["t326_idgrupoeco"].ToString() == sG)
                oLI.Selected = true;
            cboGE.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();

        cboSE.DataSource = SUBGRUPOECO.SelectByT326_idgrupoeco(null, byte.Parse(sG), false);
        cboSE.DataValueField = "t327_idsubgrupoeco";
        cboSE.DataTextField = "t327_denominacion";
        cboSE.DataBind();
        cboSE.SelectedValue = sS;
        bool bEsAdminProduccion = false;
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            bEsAdminProduccion = true;

        //dr = CONCEPTOECO.SelectByT327_idsubgrupoeco(null, byte.Parse(sS), sCualidad, (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A") ? true : false, (sAnnoPIG == "") ? false : true, (sEsReplicable == "0") ? false : true);
        dr = CONCEPTOECO.SelectByT327_idsubgrupoeco(null, byte.Parse(sS), sCualidad, bEsAdminProduccion, (sAnnoPIG == "") ? false : true, (sEsReplicable == "0") ? false : true, false);
        while (dr.Read())
        {
            oLI = new ListItem(dr["t328_denominacion"].ToString(), dr["t328_idconceptoeco"].ToString());
            oLI.Attributes.Add("count_clases_positivas", dr["count_clases_positivas"].ToString());
            oLI.Attributes.Add("count_clases_negativas", dr["count_clases_negativas"].ToString());

            if (dr["t328_idconceptoeco"].ToString() == sC)
                oLI.Selected = true;
            cboCE.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
    }
    public string getDatosEconomicos(string sG, string sS, string sC, string sCL, string sT, string sSegMesProy, string sEstadoMes, 
                                     string sEstadoProy, string sCualidad, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        //17/10/2008 Se van a mostrar todas las clases económicas del concepto,
        //aunque vengamos del árbol habiendo pinchado una clase en concreto.
        //Se marcarán en azul los datos económicos de la clase seleccionada.
        int? nCL = null;
        int? nS = (sS == "0")? null: (int?)int.Parse(sS);
        int? nC = (sC == "0")? null: (int?)int.Parse(sC);
        decimal nParcial = 0;
        SqlDataReader dr = null;
        string sFecha = "";
        string sColor = "", sMotivo="", sSerie="", sNumero="";
        bool bLecturaClase = false;
        bool bEsAdminProduccion = false;
        try
        {
            sLectura = "false";
            string sClase = " FS", sEstilo = "";
            if (sEstadoProy == "C" || sEstadoProy == "H") sLecturaInsMes = "true";
            
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                bEsAdminProduccion = true;

            
            #region Tabla de títulos
            sb.Append("<TABLE class=texto id=tblDatos style='WIDTH: 960px;' mantenimiento=1>");
            switch (sT){
                case "C":
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:15px;' />");
                    sb.Append("<col style='width:265px;' />");
                    sb.Append("<col style='width:280px;' />");
                    sb.Append("<col style='width:150px;' />");
                    sb.Append("<col style='width:150px;' />");
                    sb.Append("<col style='width:100px;' />");
                    sb.Append("</colgroup>");
                    break;
                case "P":
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:15px;' />");
                    sb.Append("<col style='width:415px;' />");
                    sb.Append("<col style='width:430px;' />");
                    sb.Append("<col style='width:100px;' />");
                    sb.Append("</colgroup>");
                    break;
                case "I":
                case "O":
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:15px;' />");
                    sb.Append("<col style='width:340px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:280px;' />");
                    sb.Append("<col style='width:100px;' />");
                    sb.Append("</colgroup>");
                    break;
            }
            sb.Append("<tbody>");
            #endregion

            if (sMonedaProyecto2 != sMonedaImportes2)
            {
                sLectura = "true";
            }
            else
            {
                if (sEstadoProy == "H" || sEstadoProy == "C" || (bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoMes == "C")
                {
                    sLectura = "true";
                }
                if ((sEstadoProy == "A" || sEstadoProy == "P") && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                {
                    sLectura = "false";
                }
            }

            #region Tabla de datos fijos
            switch (sCualidad)
            {
                case "C":
                    dr = PROYECTO.GetResumenFijosContratante(int.Parse(sSegMesProy), int.Parse(sG), nS, nC, 
                                        (sMonedaProyecto2 != sMonedaImportes2)? sMonedaImportes2:null,
                                        Session["MONEDA_PROYECTOSUBNODO"].ToString());
                    break;
                case "J":
                    dr = PROYECTO.GetResumenFijosRepJornadas(int.Parse(sSegMesProy), int.Parse(sG), nS, nC, 
                                        (sMonedaProyecto2 != sMonedaImportes2) ? sMonedaImportes2 : null,
                                        Session["MONEDA_PROYECTOSUBNODO"].ToString());
                    break;
                case "P":
                    dr = PROYECTO.GetResumenFijosRepPrecio(int.Parse(sSegMesProy), int.Parse(sG), nS, nC, 
                                        (sMonedaProyecto2 != sMonedaImportes2) ? sMonedaImportes2 : null,
                                        Session["MONEDA_PROYECTOSUBNODO"].ToString());
                    break;
            }
            while (dr.Read())
            {
                sEstilo = " onmouseover='return false;' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Grupo:</label>" + dr["t326_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Subgrupo:</label>" + dr["t327_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Concepto:</label>" + dr["t328_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ";

                sb.Append("<tr id='" + dr["idclaseeco"].ToString() + "' idCL='" + dr["idclaseeco"].ToString() + "' ");
                sb.Append("bd='' grupo='" + dr["t326_tipogrupo"].ToString() + "' ");

                //if (sLectura == "false" && dr["modificable"].ToString() == "1") 
                //if (sLectura == "false" && dr["modificable"].ToString() == "1" || (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A" && dr["idclaseeco"].ToString() == "-14")) sb.Append("class='MANO' onclick='mm(event)' ");
                if (sLectura == "false" && dr["modificable"].ToString() == "1" || (bEsAdminProduccion && dr["idclaseeco"].ToString() == "-14")) 
                    sb.Append("class='MANO' onclick='mm(event)' ");
                
                sb.Append("style='height:20px;'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td style='padding-left:5px;'" + sEstilo + ">" + dr["denominacion"].ToString() + "</td>");
                
                switch (sT)
                {
                    case "C":
                        sb.Append("<td><nobr class='NBR W250' onmouseover='TTip(event)'>" + dr["motivo"].ToString() + "</nobr></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        break;
                    case "P":
                        sb.Append("<td><nobr class='NBR W400' onmouseover='TTip(event)'>" + dr["motivo"].ToString() + "</nobr></td>");
                        break;
                    case "I":
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)'>" + dr["motivo"].ToString() + "</nobr></td>");
                        break;
                    case "O"://poner lectura
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)'>" + dr["motivo"].ToString() + "</nobr></td>");
                        break;
                }

                if (decimal.Parse(dr["IMPORTE_0"].ToString()) < 0) sColor = "textoR";
                else sColor = "";
                if (sLectura == "true") sb.Append("<td style='text-align:right; padding-right:2px;' class='" + sColor + "'>" + decimal.Parse(dr["IMPORTE_0"].ToString()).ToString("N") + "</td>");
                else
                {
                    //if (dr["modificable"].ToString() == "1" || (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A" && dr["idclaseeco"].ToString() == "-14"))
                    if (dr["modificable"].ToString() == "1" || (bEsAdminProduccion && dr["idclaseeco"].ToString() == "-14"))
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'><input type='text' class='txtNumL' style='text-align:right; padding-right:2px; width:80px; cursor:pointer;' value='" + decimal.Parse(dr["IMPORTE_0"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                    else
                        sb.Append("<td style='text-align:right; padding-right:2px;' class='" + sColor + "'>" + decimal.Parse(dr["IMPORTE_0"].ToString()).ToString("N") + "</td>");
                }

                //if (sLectura == "false" && dr["modificable"].ToString() == "1") sb.Append("<td class='" + sColor + "'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["IMPORTE_0"].ToString()).ToString("N") + "' onkeyup='fm(this)' onfocus='fn(this)' onchange='calcularTotal()' /></td>");
                //else sb.Append("<td class='" + sColor + "'>" + decimal.Parse(dr["IMPORTE_0"].ToString()).ToString("N") + "</td>");
                
                sb.Append("</tr>");
            }
            dr.Close();
            //dr.Dispose();
            #endregion

            #region Datos de clases
            dr = DATOECO.Catalogo(int.Parse(sSegMesProy), int.Parse(sG), nS, nC, nCL, (sMonedaProyecto2 != sMonedaImportes2)? sMonedaImportes2:null);
            while (dr.Read())
            {
                bLecturaClase = false;
                sMotivo = dr["t376_motivo"].ToString();
                sSerie = dr["t376_seriefactura"].ToString().Trim();
                sNumero = dr["t376_numerofactura"].ToString().Trim();
                if (
                    ((int)dr["t329_idclaseeco"] == Constantes.nIdClaseObraEnCurso && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "")
                    || ((int)dr["t329_idclaseeco"] == -38) //GASVI
                    ) //20% obra en curso. Es un dato que el usuario no administrador no puede modificar.
                    //-38 -> GASVI, no lo puede modificar ni el administrador.
                {
                    bLecturaClase = true;
                }
                if (dr["t329_idclaseeco"].ToString() == sCL)
                {
                    sClase = " blue";
                    nParcial += decimal.Parse(dr["t376_importe"].ToString());
                }
                else sClase = "";

                sb.Append("<tr id='" + dr["t376_iddatoeco"].ToString() + "' bd='' idCL='" + dr["t329_idclaseeco"].ToString() + "' idNodo='" + dr["t303_idnodo_destino"].ToString() + "' idProv='" + dr["t315_idproveedor"].ToString() + "' nece='" + dr["t329_necesidad"].ToString() + "' ");

                if (sT == "I")
                {
                    sb.Append(" idCli='" + dr["t302_codigoexterno"].ToString() + "' denCli=\"" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "\" ");
                    sb.Append(" idSocEmi='" + dr["IDSOCEMISORA"].ToString() + "' socEmi=\"" + dr["SOCEMISORA"].ToString().Replace((char)34, (char)39) + "\" ");
                }

                if (!bLecturaClase) sb.Append("class='MANO' onclick='mm(event)' ");
                if ((int)dr["t329_idclaseeco"] == -38)
                {
                    sb.Append("class='MA' onclick='mm(event)' ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\" ");
                    sMotivo = "(Ref: " + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + ")&nbsp;&nbsp;" + sMotivo;
                }
                
                sb.Append("style='height:20px;'>");
                
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sEstilo = " onmouseover='return false;' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Grupo:</label>" + dr["t326_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Subgrupo:</label>" + dr["t327_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Concepto:</label>" + dr["t328_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ";
                if (sLectura == "true" || bLecturaClase)
                {
                    switch (sT)
                    {
                        case "C":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W240" + sClase + "' " + sEstilo + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W260'" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + sMotivo + "</nobr></td>");
                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140'" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140'" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t315_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");
                            break;
                        case "P":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W380" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W410'>" + sMotivo + "</nobr></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");
                            break;
                        case "I":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sFecha = dr["t376_fecha"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["t376_fecha"].ToString()).ToShortDateString();
                            sb.Append("<td>" + sFecha + "</td>");
                            sb.Append("<td>" + sSerie + "</td>");

                            if (dr["t302_denominacion"].ToString() != "")
                                sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:50px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"><label style='width:50px;'>" + sNumero + "</label>");
                            else
                                sb.Append("<td><label style='width:50px;'>" + sNumero + "</label>");
                            if (sSerie != "" && sNumero != "")
                            {
                                sb.Append("<img src='../../../Images/botones/imgPDF.gif' style='cursor:pointer; vertical-align:middle;' ");
                                sb.Append(" onclick=\"getDisponibilidadFra(" + sSerie + "," + sNumero + ");\" title='Ver factura' />");
                            }
                            sb.Append("</td>");
                            
                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W260'>" + sMotivo + "</nobr></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");
                            break;
                        case "O"://poner lectura
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W260" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sFecha = dr["t376_fecha"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["t376_fecha"].ToString()).ToShortDateString();
                            sb.Append("<td>" + sFecha + "</td>");
                            sb.Append("<td>" + sSerie + "</td>");

                            if (dr["t302_denominacion"].ToString() != "")
                                sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:50px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"><label style='width:50px;'>" + sNumero + "</label>");
                            else
                                sb.Append("<td><label style='width:50px;'>" + sNumero + "</label>");
                            if (sSerie != "" && sNumero != "")
                            {
                                sb.Append("<img src='../../../Images/botones/imgPDF.gif' style='cursor:pointer; vertical-align:middle;' ");
                                sb.Append(" onclick=\"getDisponibilidadFra(" + sSerie + "," + sNumero + ");\" title='Ver factura' />");
                            }
                            sb.Append("</td>");

                            sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W260'>" + sMotivo + "</nobr></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");
                            break;
                    }
                }
                else
                {
                    switch (sT)
                    {
                        case "C":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W240" + sClase + "' " + sEstilo + "" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td><input type='text' class='txtL' style='width:270px; cursor:pointer' value='" + sMotivo + "' maxlength='50' onkeyup='fm(event)' /></td>");
                            switch (dr["t329_necesidad"].ToString())
                            {
                                case "":
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MANO'" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MANO'" + (((int)dr["t329_idclaseeco"] == -38) ? " ondblclick=\"mdng(" + dr["t420_idreferencia"].ToString() + ",'" + dr["TipoNota"].ToString() + "',event)\"" : "") + ">" + dr["t315_denominacion"].ToString() + "</nobr></td>");
                                    break;
                                case "N":
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MA' ondblclick='getNodo(this.parentNode.parentNode)'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MANO'>" + dr["t315_denominacion"].ToString() + "</nobr></td>");
                                    break;
                                case "P":
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MANO'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                                    sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W140 MA' ondblclick='getProveedor(this.parentNode.parentNode)'>" + dr["t315_denominacion"].ToString() + "</nobr></td>");
                                    break;
                            }
                            sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                            break;
                        case "P":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W380" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sb.Append("<td><input type='text' class='txtL' style='width:370px; cursor:pointer' value='" + sMotivo + "' maxlength='50' onkeyup='fm(event)' /></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                            break;
                        case "I":
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sFecha = dr["t376_fecha"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["t376_fecha"].ToString()).ToShortDateString();
                            sb.Append("<td>" + sFecha + "</td>");
                            sb.Append("<td>" + sSerie + "</td>");
                            //sb.Append("<td>" + dr["t376_numerofactura"].ToString() + "</td>");

                            if (dr["t302_denominacion"].ToString() != "")
                                sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:50px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"><label style='width:50px;'>" + sNumero + "</label>");
                            else
                                sb.Append("<td><label style='width:50px;'>" + dr["t376_numerofactura"].ToString() + "</label>");
                            if (sSerie != "" && sNumero != "")
                            {
                                sb.Append("<img src='../../../Images/botones/imgPDF.gif' style='cursor:pointer; vertical-align:middle;' ");
                                sb.Append(" onclick=\"getDisponibilidadFra(" + sSerie + "," + sNumero + ");\" title='Ver factura' />");
                            }
                            sb.Append("</td>");

                            sb.Append("<td><input type='text' class='txtL' style='width:280px; cursor:pointer' value='" + sMotivo + "' maxlength='50' onkeyup='fm(event)' /></td>");
                            sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                            break;
                        case "O"://poner lectura, salvo para el administrador.
                            sb.Append("<td style='padding-left:5px;'><nobr class='NBR W260" + sClase + "' " + sEstilo + ">" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                            sFecha = dr["t376_fecha"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["t376_fecha"].ToString()).ToShortDateString();
                            sb.Append("<td>" + sFecha + "</td>");
                            sb.Append("<td>" + sSerie + "</td>");
                            //sb.Append("<td>" + dr["t376_numerofactura"].ToString() + "</td>");

                            if (dr["t302_denominacion"].ToString() != "")
                                sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:50px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"><label style='width:50px;'>" + sNumero + "</label>");
                            else
                                sb.Append("<td><label style='width:50px;'>" + dr["t376_numerofactura"].ToString() + "</label>");
                            if (sSerie != "" && sNumero != "")
                            {
                                sb.Append("<img src='../../../Images/botones/imgPDF.gif' style='cursor:pointer; vertical-align:middle;' ");
                                sb.Append(" onclick=\"getDisponibilidadFra(" + sSerie + "," + sNumero + ");\" title='Ver factura' />");
                            }
                            sb.Append("</td>");

                            if ((sEstadoProy == "A" || sEstadoProy == "P") && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                            {
                                sb.Append("<td><input type='text' class='txtL' style='width:280px; cursor:pointer' value='" + sMotivo + "' maxlength='50' onkeyup='fm(event)' /></td>");
                                sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                            }
                            //else if ((sEstadoProy == "A" || sEstadoProy == "P") && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A" && sEstadoMes == "A")
                            else if ((sEstadoProy == "A" || sEstadoProy == "P") && bEsAdminProduccion && sEstadoMes == "A")
                            {
                                sb.Append("<td><input type='text' class='txtL' style='width:280px; cursor:pointer' value='" + sMotivo + "' maxlength='50' onkeyup='fm(event)' /></td>");
                                sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:80px; cursor:pointer' value='" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "' onkeyup='fm(event);calcularTotal();' onfocus='fn(this)' /></td>");
                            }
                            else
                            {
                                sb.Append("<td onmouseover=TTip(event)><nobr class='NBR W260'>" + sMotivo + "</nobr></td>");
                                sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");
                            }
                            break;
                    }
                }
                sb.Append("</tr>");
                
            }
            dr.Close();
            dr.Dispose();
            #endregion

            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + nParcial.ToString("N") +"@#@"+ MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos económicos", ex);
        }
    }
    private string getSE(string sGE)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUBGRUPOECO.SelectByT326_idgrupoeco(null, byte.Parse(sGE), false);

            while (dr.Read())
            {
                sb.Append(dr["t327_idsubgrupoeco"].ToString() + "##" + dr["t327_denominacion"].ToString() +"///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos económicos", ex);
        }
    }
    private string getCE(string sSE, string sCualidad, string sAnnoPIG, string sEsReplicable)
    {
        bool bEsAdminProduccion = false;
        try
        {
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                bEsAdminProduccion = true;
            StringBuilder sb = new StringBuilder();
           // SqlDataReader dr = CONCEPTOECO.SelectByT327_idsubgrupoeco(null, byte.Parse(sSE), sCualidad, (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A") ? true : false, (sAnnoPIG == "") ? false : true, (sEsReplicable == "0") ? false : true);
            SqlDataReader dr = CONCEPTOECO.SelectByT327_idsubgrupoeco(null, byte.Parse(sSE), sCualidad, bEsAdminProduccion, (sAnnoPIG == "") ? false : true, (sEsReplicable == "0") ? false : true, false);

            while (dr.Read())
            {
                sb.Append(dr["t328_idconceptoeco"].ToString() + "##" + dr["t328_denominacion"].ToString() + "##" + dr["count_clases_positivas"].ToString() + "##" + dr["count_clases_negativas"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos económicos", ex);
        }
    }
    private string getMesesProy(string sIDProySubnodo)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sIDProySubnodo));

            while (dr.Read())
            {
                sb.Append(dr["t325_idsegmesproy"].ToString() + "##");
                sb.Append(dr["t325_anomes"].ToString() + "##");
                sb.Append(dr["t325_estado"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del proyectosubnodo", ex);
        }
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }

    protected string Grabar(string sIDSegMes, string strDatos, string strDatosFijos)
    {
        string sResul = "", sDesc = "", sElementosInsertados = "";
        bool bErrorControlado = false;
        int nAux = 0;

        #region apertura de conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            if (!SEGMESPROYECTOSUBNODO.ExisteSegMesProyByID(tr, int.Parse(sIDSegMes)))
            {
                bErrorControlado = true;
                throw (new Exception("Durante su intervención en la pantalla, otro usuario ha eliminado el mes en curso.\nSi desea registrar los datos, previamente debe insertar el mes."));
            }

            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA")
            {
                SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(tr, int.Parse(sIDSegMes), null);
                if (oSMPSN.t325_estado == "C" && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA")
                {
                    bErrorControlado = true;
                    throw (new Exception("Durante su intervención en la pantalla, otro usuario ha cerrado el mes en curso."));
                }
            }

            
            #region Datos clases económicas
            if (strDatos != "")
            {
                string[] aClase = Regex.Split(strDatos, "///");
                foreach (string oClase in aClase)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID Dato Económico
                    //2. ID Clase
                    //3. Nodo
                    //4. Proveedor
                    //5. nSegMesProy
                    //6. Motivo
                    //7. Importe

                    sDesc = Utilidades.unescape(aValores[6]);
                    int? nNodo = null, nProv = null;
                    if (aValores[3] != "") nNodo = int.Parse(aValores[3]);
                    if (aValores[4] != "") nProv = int.Parse(aValores[4]);

                    switch (aValores[0])
                    {
                        case "I":
                            nAux = DATOECO.Insert(tr, int.Parse(aValores[5]), int.Parse(aValores[2]), sDesc, decimal.Parse(aValores[7]), nNodo, nProv, null);
                            if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                            else sElementosInsertados += "//" + nAux.ToString();
                            break;
                        case "U":
                            nAux = DATOECO.Update(tr, int.Parse(aValores[1]), int.Parse(aValores[5]), int.Parse(aValores[2]), sDesc, decimal.Parse(aValores[7]), nNodo, nProv);
                            break;
                        case "D":
                            DATOECO.Delete(tr, int.Parse(aValores[1]));
                            break;
                    }
                }
            }
            #endregion

            #region Datos fijos
            if (strDatosFijos != "")
            {
                string[] aClaseFija = Regex.Split(strDatosFijos, "///");
                foreach (string oClaseFija in aClaseFija)
                {
                    if (oClaseFija == "") continue;
                    string[] aValores = Regex.Split(oClaseFija, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. grupo economico
                    //2. ID Dato Economico
                    //3. nSegMesProy
                    //4. Importe
                    //5. ID Clase

                    if (aValores[0] == "D") aValores[4] = "0";

                    switch (aValores[5])
                    {
                        case "-14": //Gastos financieros
                            nAux = SEGMESPROYECTOSUBNODO.UpdateGastosFinancieros(tr, int.Parse(aValores[3]), (aValores[4] == "0") ? 0 : decimal.Parse(aValores[4]));
                            break;
                        case "-13": //Consumos por periodificación
                            nAux = SEGMESPROYECTOSUBNODO.UpdateConsumoPeriodificacion(tr, int.Parse(aValores[3]), (aValores[4] == "0") ? 0 : decimal.Parse(aValores[4]));
                            break;
                        case "-19": //Avance
                            nAux = SEGMESPROYECTOSUBNODO.UpdateAvanceProduccion(tr, int.Parse(aValores[3]), (aValores[4] == "0") ? 0 : decimal.Parse(aValores[4]));
                            break;
                        case "-20": //Producción por periodificación
                            nAux = SEGMESPROYECTOSUBNODO.UpdateProduccionPeriodificacion(tr, int.Parse(aValores[3]), (aValores[4] == "0") ? 0 : decimal.Parse(aValores[4]));
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
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos económicos.", ex) + "@#@" + sDesc;
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos económicos.", ex, false);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private string getClaseUnica(string sCE, string sCualidad, string sEsReplicable, string idsNegativos)
    {
        bool bEsAdminProduccion = false;
        try
        {
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                bEsAdminProduccion = true;
            StringBuilder sb = new StringBuilder();
            //SqlDataReader dr = CLASEECO.SelectActivasByT328_idconceptoeco(null, byte.Parse(sCE), sCualidad, (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A") ? true : false, (sEsReplicable == "0") ? false : true, idsNegativos);
            SqlDataReader dr = CLASEECO.SelectActivasByT328_idconceptoeco(null, byte.Parse(sCE), sCualidad, bEsAdminProduccion, (sEsReplicable == "0") ? false : true, idsNegativos);

            if (dr.Read())
            {
                sb.Append(dr["t329_idclaseeco"].ToString() + "##" + dr["t329_necesidad"].ToString() + "##" + dr["t329_denominacion"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos económicos", ex);
        }
    }
    private string getDisponibilidadFra(string sSerieNumeroFactura)
    {
        try
        {
            return "OK@#@" + Factura.getDatoDisponibilidadFra(sSerieNumeroFactura);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la disponibilidad de la factura.", ex);
        }
    }
}
