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
//using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int SN4 = 0, SN3 = 0, SN2 = 0, SN1 = 0, nIDItem = 0;
    public string strHTMLSoporteAdminis = "<table id='tblSoporte' class='texto' style='width:260px;'></table>";
    public string strHTMLSubNodomaniobra3 = "<table id='tblSubNodomaniobra3' class='texto' style='width:500px;'></table>";

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
                SN4 = int.Parse(Utilidades.decodpar(Request.QueryString["SN4"].ToString()));
                SN3 = int.Parse(Utilidades.decodpar(Request.QueryString["SN3"].ToString()));
                SN2 = int.Parse(Utilidades.decodpar(Request.QueryString["SN2"].ToString()));
                SN1 = int.Parse(Utilidades.decodpar(Request.QueryString["SN1"].ToString()));
                nIDItem = int.Parse(Utilidades.decodpar(Request.QueryString["ID"].ToString()));

                // Leer Monedas
                //cboMoneda.DataValueField = "ID";
                //cboMoneda.DataTextField = "DENOMINACION";
                //cboMoneda.DataSource = MONEDA.CatalogoSAP();
                cboMoneda.DataValueField = "t422_idmoneda";
                cboMoneda.DataTextField = "t422_denominacion";
                cboMoneda.DataSource = MONEDA.ObtenerMonedasGestionarProyectos();
                cboMoneda.DataBind();
                if (nIDItem <= 0) 
                    cboMoneda.SelectedValue = "EUR";

                cboPerfiles.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.NODO), "N"));
                cboPerfiles.Items.Add(new ListItem("Cliente", "C"));
                cboOrgVtas.Items.Add(new ListItem("", ""));
                ObtenerOrgVtas();
                CargarDatosEstructura();
                if (nIDItem > 0) 
                    CargarDatosItem();
                else 
                    cargarDatosDefecto();   

                string strTabla0 = obtenerParametros("1", nIDItem);
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strHTMLSoporteAdminis = aTabla0[2];
                else sErrores = aTabla0[2];

                if (nIDItem > 0)
                {
                    strTabla0 = obtenerSubNodosManiobraTipo3("6", nIDItem);
                    aTabla0 = Regex.Split(strTabla0, "@#@");
                    if (aTabla0[0] != "Error") strHTMLSubNodomaniobra3 = aTabla0[2];
                    else sErrores = aTabla0[2];
                }
                if (Utilidades.decodpar(Request.QueryString["origen"]) == "MantFiguras")
                {
                    tsPestanas.SelectedIndex = 1;
                    tsPestanas.Items[0].Disabled = true;
                    tsPestanas.Items[2].Disabled = true;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del "+ Estructura.getDefLarga(Estructura.sTipoElem.NODO), ex);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("duplicar"):
                sResultado += Duplicar(aArgs[1]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasItem(aArgs[1], aArgs[2]);
                        break;
                    case 2://Centros de coste
                        sResultado += obtenerCentrosCoste(aArgs[1], aArgs[2]);
                        break;
                    case 3://Figuras virtuales de proyecto
                        sResultado += obtenerFigurasPSN_Nodo(aArgs[1], aArgs[2]);
                        break;
                    case 4://Alertas
                        sResultado += obtenerAlertas(aArgs[1], aArgs[2]);
                        break;
                    case 5://Subnodos de maniobra tipo 3 (defecto para réplica)
                        sResultado += obtenerSubNodosManiobraTipo3(aArgs[1], int.Parse(aArgs[2]));
                        break;
                }
                break;
            case ("getDatosPestanaGenParam"):
                switch (int.Parse(aArgs[1]))
                {
                    case 1://Soporte administrativo
                        sResultado += obtenerParametros(aArgs[1], int.Parse(aArgs[2]));
                        break;
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("tecnicosV"):
                sResultado += obtenerProfesionalesFiguraV(aArgs[1], aArgs[2], aArgs[3]);
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

    private void cargarDatosDefecto()
    {
        SUPER.BLL.Cualificador o = SUPER.BLL.Cualificador.getDefectoParaNodos();
        
        this.hdnIDCualificador.Text = o.t055_idcalifOCFA.ToString();
        this.txtDesCualificador.Text = o.t055_denominacion;
    }
    private void CargarDatosEstructura()
    {
        if (SN4 > 0)
        {
            SUPERNODO4 oSN4Aux = SUPERNODO4.Select(tr, SN4);
            txtDesSN4.Text = oSN4Aux.t394_denominacion;
            hdnIDSN4.Text = oSN4Aux.t394_idsupernodo4.ToString();
        }
        if (SN3 > 0)
        {
            SUPERNODO3 oSN3Aux = SUPERNODO3.Select(tr, SN3);
            txtDesSN3.Text = oSN3Aux.t393_denominacion;
            hdnIDSN3.Text = oSN3Aux.t393_idsupernodo3.ToString();
        }
        if (SN2 > 0)
        {
            SUPERNODO2 oSN2Aux = SUPERNODO2.Select(tr, SN2);
            txtDesSN2.Text = oSN2Aux.t392_denominacion;
            hdnIDSN2.Text = oSN2Aux.t392_idsupernodo2.ToString();
        }
        if (SN1 > 0)
        {
            SUPERNODO1 oSN1Aux = SUPERNODO1.Select(tr, SN1);
            txtDesSN1.Text = oSN1Aux.t391_denominacion;
            hdnIDSN1.Text = oSN1Aux.t391_idsupernodo1.ToString();
        }

    }
    private void CargarDatosItem()
    {
        NODO oNodo = NODO.ObtenerNodo(null, nIDItem);

        txtID.Text = oNodo.t303_idnodo.ToString();
        txtDenominacion.Text = oNodo.t303_denominacion;
        txtAbreviatura.Text = oNodo.t303_denabreviada;
        hdnIDResponsable.Text = oNodo.t314_idusuario_responsable.ToString();
        txtDesResponsable.Text = oNodo.DesResponsable.Replace("&nbsp;"," ");
        chkActivo.Checked = (bool)oNodo.t303_estado;
        txtOrden.Text = oNodo.t303_orden.ToString();
        cboModoCoste.SelectedValue = oNodo.t303_modelocostes;
        cboModoTarifa.SelectedValue = oNodo.t303_modelotarifas;
        cboHermes.SelectedValue = oNodo.t303_interfacehermes;
        chkMasUnGF.Checked = (bool)oNodo.t303_masdeungf;
        chkGR.Checked = (bool)oNodo.t303_generareplica;
        chkPGRCG.Checked = (bool)oNodo.t303_pgrcg;
        chkRepresentativo.Checked = (bool)oNodo.t303_representativo;
        hdnRepresentativo.Text = (chkRepresentativo.Checked) ? "1" : "0";
        if (chkRepresentativo.Checked == false) hdnMensajeRepresentativo.Text = cargarEmpresasSubNodos();

        chkMailCIA.Checked = (bool)oNodo.t303_defectomailiap;
        chkCierreIAPest.Checked = (bool)oNodo.t303_cierreIAPestandar;
        chkCierreECOest.Checked = (bool)oNodo.t303_cierreECOestandar;
        chkImprodGen.Checked = (bool)oNodo.t303_defectoPIG;
        chkCuadre.Checked = (bool)oNodo.t303_compcontprod;
        txtTolerancia.Text = oNodo.t303_porctolerancia.ToString();
        chkCalcular.Checked = (bool)oNodo.t303_defectocalcularGF;
        chkTipolInterna.Checked = (bool)oNodo.t303_tipolinterna;
        chkTipolEspecial.Checked = (bool)oNodo.t303_tipolespecial;
        chkTipolProdSC.Checked = (bool)oNodo.t303_tipolproductivaSC;
        chkDesglose.Checked = (bool)oNodo.t303_desglose;
        chkControlhuecos.Checked = (bool)oNodo.t303_controlhuecos;
        chkPermitirPST.Checked = (bool)oNodo.t303_defectoadmiterecursospst;

        txtUltRecGF.Text = oNodo.ultrecalculoGF.ToString();
        this.hdnCierreECO.Text = oNodo.t303_ultcierreeco.ToString();
        this.hdnCierreIAP.Text = oNodo.t303_ultcierreIAP.ToString();
        txtDesEmpresa.Text = oNodo.t313_denominacion;
        hdnIDEmpresa.Text = oNodo.t313_idempresa.ToString();
        txtMargenCesion.Text = oNodo.t303_margencesionprof.ToString("N");
        txtInteresGF.Text = oNodo.t303_interesGF.ToString("#,##0.0000");
        txtCualificador.Text = oNodo.t303_denominacion_CNP;
        chkCualifObl.Checked = (bool)oNodo.t303_obligatorio_CNP;
        cboPerfiles.SelectedValue = oNodo.t303_asignarperfiles;
        cboOrgVtas.SelectedValue = oNodo.t621_idovsap.ToString();
        chkSoporte.Checked = (bool)oNodo.t303_msa;
        chkAlertas.Checked = (bool)oNodo.t303_noalertas;
        chkCualiCVT.Checked = (bool)oNodo.t303_cualificacionCVT;
        this.cboMoneda.SelectedValue = oNodo.t422_idmoneda;
        this.hdnIDCualificador.Text = oNodo.t055_idcalifOCFA.ToString();
        this.txtDesCualificador.Text = oNodo.t055_denominacion;
        if (oNodo.ta212_idorganizacioncomercial != null)
            this.txtIdOrgCom.Text = oNodo.ta212_idorganizacioncomercial.ToString();
        else
            this.txtIdOrgCom.Text="";
        this.txtDesOrgCom.Text = oNodo.ta212_denominacion;

        chkQEQ.Checked = (bool)oNodo.activoqeq;
        this.chkInstrumental.Checked = (bool)oNodo.instrumental;
        //Guardo el valor inicial de instrumental para que si al grabar el nodo, el check de instrumental a pasado de NO a SI, hacer la comprobación de que el nodo
        //no tiene profesionales ni proyectos asignados
        if ((bool)oNodo.instrumental)
            this.hdnInstrumentalIni.Value = "S";
        else
            this.hdnInstrumentalIni.Value = "N";
    }
    private string cargarEmpresasSubNodos()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = SUBNODO.ObtenerConEmpresaAsignada(null, nIDItem);

            while (dr.Read())
            {
                sb.Append("- " + dr["t304_denominacion"].ToString() + " (" + dr["t313_denominacion"].ToString() + ").{salto}");
            }
            dr.Close();
            dr.Dispose();

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    public void ObtenerOrgVtas()
    {
        SqlDataReader dr = ORGVENTASSAP.Catalogo();
        ListItem oLI = null;
        while (dr.Read())
        {
            oLI = new ListItem(dr["Denominacion"].ToString(), dr["codigo"].ToString());
            cboOrgVtas.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
    }
    private string obtenerFigurasItem(string sPestana, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = NODO.CatalogoFiguras(int.Parse(sIDItem));

            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='WIDTH: 420px;' mantenimiento='1'>");
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
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;' onclick='mm(event)' onmousedown='DD(event);' ");
//                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIV.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                        switch (dr["tipo"].ToString())
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
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIM.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEM.gif'>");
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

                    sb.Append("</td><td><span class='NBR W275' ");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append(">" + dr["Profesional"].ToString() + "</span></td>");

                    sb.Append("<td><div><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                        case "G": sb.Append("<li id='G' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgGestor.gif' title='Gestor' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "P": sb.Append("<li id='P' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgPerseguidor.gif' title='Receptor de Informes de Actividad' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                        case "G": sb.Append("<li id='G' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgGestor.gif' title='Gestor' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "P": sb.Append("<li id='P' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgPerseguidor.gif' title='Receptor de Informes de Actividad' /></li>"); break;
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

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }
    private string obtenerFigurasPSN_Nodo(string sPestana, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIniV = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURAPSN_NODO.CatalogoFiguras(int.Parse(sIDItem));

            sb.Append("<TABLE id='tblFiguras2V' class='texto MM' style='WIDTH: 420px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 10px' />");
            sb.Append("    <col style='width: 20px' />");
            sb.Append("    <col style='width: 290px;' />");
            sb.Append("    <col style='width: 100px;' />");
            sb.Append("</colgroup>");
            int nUsuario = 0;
            bool bHayFilas = false;
            string sColor = "black";
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIniV[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");
                i++;
                sColor = "black";
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;color:" + sColor + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                    //else sb.Append("tipo='I' ");

                    sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    //sb.Append("<td></td>");
                    sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIV.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                        switch (dr["tipo"].ToString())
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
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIM.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEM.gif'>");
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
                    //sb.Append("<td><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    sb.Append("<td style='padding-left:3px;'><span class='NBR W275' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'

                    //sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                    //Figuras
                    sb.Append("<td><div><ul id='boxv-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Secretaria' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
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
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras virtuales.", ex);
        }
    }
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='WIDTH: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:22px; noWrap:true;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td style='width:padding-left:5px;' >");
                //sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<span ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string obtenerProfesionalesFiguraV(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1V' class='texto MAM' style='WIDTH: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:22px; noWrap:true;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td  style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<span ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string Grabar(string sIDAux, string strDatosBasicos, string strSoporte, string strFiguras, string strFigurasV, 
                         string srtAlertas, string sSubNodosDefectoReplica)
    {
        string sResul = "";
        int nID = -1, nUltCierre;
        string[] aDatosBasicos = null;
        bool bErrorControlado = false;
        int? idOrgCom = null;

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
            if (sIDAux != ""){
                nID = int.Parse(sIDAux);
            }
            #region Datos Generales
            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                #region Datos
                ///aDatosBasicos[0] = ID
                ///aDatosBasicos[1] = Denominacion
                ///aDatosBasicos[2] = IDResponsable
                ///aDatosBasicos[3] = hdnIDSN1
                ///aDatosBasicos[4] = Orden
                ///aDatosBasicos[5] = cboModoCoste
                ///aDatosBasicos[6] = cboModoTarifa
                ///aDatosBasicos[7] = cboHermes
                ///aDatosBasicos[8] = chkActivo
                ///aDatosBasicos[9] = chkMasUnGF
                ///aDatosBasicos[10] = chkGR
                ///aDatosBasicos[11] = chkPGRCG
                ///aDatosBasicos[12] = chkRepresentativo
                ///aDatosBasicos[13] = chkMailCIA
                ///aDatosBasicos[14] = chkCuadre
                ///aDatosBasicos[15] = txtTolerancia
                ///aDatosBasicos[16] = chkCalcular
                ///aDatosBasicos[17] = hdnIDEmpresa
                ///aDatosBasicos[18] = chkCierreIAPest
                ///aDatosBasicos[19] = chkCierreECOest
                ///aDatosBasicos[20] = chkImprodGen
                ///aDatosBasicos[21] = ULTIMO CIERRE IAP
                ///aDatosBasicos[22] = ULTIMO CIERRE ECONOMICO
                ///aDatosBasicos[23] = t303_margencesionprof
                ///aDatosBasicos[24] = t303_interesGF
                ///aDatosBasicos[25] = txtCualificador
                ///aDatosBasicos[26] = chkCualifObl
                ///aDatosBasicos[27] = cboPerfiles
                ///aDatosBasicos[28] = chkTipolInterna
                ///aDatosBasicos[29] = chkTipolEspecial
                ///aDatosBasicos[30] = chkTipolProdSC
                ///aDatosBasicos[31] = chkDesglose
                ///aDatosBasicos[32] = chkControlhuecos
                ///aDatosBasicos[33] = chkPermitirPST
                ///aDatosBasicos[34] = cboOrgVtas
                ///aDatosBasicos[35] = chkSoporte
                ///aDatosBasicos[36] = txtAbreviatura
                ///aDatosBasicos[37] = chkAlertas
                ///aDatosBasicos[38] = chkCualiCVT
                ///aDatosBasicos[39] = cboMoneda
                ///aDatosBasicos[40] = hdnIDCualificador
                ///aDatosBasicos[41] = txtIdOrgCom
                ///aDatosBasicos[42] = QEQ
                ///aDatosBasicos[43] = Instrumental original
                ///aDatosBasicos[44] = Instrumental actual
                #endregion
                nUltCierre = Fechas.FechaAAnnomes(DateTime.Now.AddMonths(-1));
                if (aDatosBasicos[41] != "")
                    idOrgCom = int.Parse(aDatosBasicos[41]);

                if (aDatosBasicos[0] == "") //insert
                {
                    #region insert
                    //string sNodoAux = SUPER.BLL.OrganizacionComercial.NodosOrgCom(tr, -1, idOrgCom);
                    //if (sNodoAux != "")
                    //{
                    //    bErrorControlado = true;
                    //    throw (new Exception("La organización comercial ya está asignada al " + Estructura.getDefLarga(Estructura.sTipoElem.NODO)+ " " + sNodoAux));
                    //}
                    nID = NODO.Insert(tr, Utilidades.unescape(aDatosBasicos[1]),//Denominacion 
                        (aDatosBasicos[9] == "1") ? true : false,//chkMasUnGF
                        int.Parse(aDatosBasicos[3]),//hdnIDSN1
                        int.Parse(aDatosBasicos[17]),//hdnIDEmpresa
                        (aDatosBasicos[19] == "1") ? true : false,//chkCierreECOest
                        int.Parse(aDatosBasicos[22]),//ultimo cierre economico
                        (aDatosBasicos[8] == "1") ? true : false,//chkActivo
                        aDatosBasicos[5],//cboModoCoste
                        aDatosBasicos[6],//cboModoTarifa
                        (aDatosBasicos[10] == "1") ? true : false,//genera replica J
                        byte.Parse((aDatosBasicos[15] == "") ? "0" : aDatosBasicos[15]),//txtTolerancia
                        int.Parse(aDatosBasicos[2]),//IDResponsable
                        int.Parse(aDatosBasicos[4]),//Orden
                        (aDatosBasicos[12] == "1") ? true : false,//chkRepresentativo
                        aDatosBasicos[7],//cboHermes
                        (aDatosBasicos[16] == "1") ? true : false,//chkCalcular GF
                        (aDatosBasicos[13] == "1") ? true : false,//chkMailCIA
                        (aDatosBasicos[18] == "1") ? true : false,//chkCierreIAPest
                        int.Parse(aDatosBasicos[21]),//ultimo cierre IAP
                        (aDatosBasicos[14] == "1") ? true : false,//chkCuadre
                        (aDatosBasicos[20] == "1") ? true : false,
                        float.Parse(aDatosBasicos[23]),
                        float.Parse(aDatosBasicos[24]),
                        Utilidades.unescape(aDatosBasicos[25]),
                        (aDatosBasicos[26] == "1") ? true : false,
                        (aDatosBasicos[27] == "") ? null : aDatosBasicos[27],
                        (aDatosBasicos[31] == "1") ? true : false,
                        (aDatosBasicos[11] == "1") ? true : false,//Permite generar replicas con gestión
                        (aDatosBasicos[32] == "1") ? true : false,
                        (aDatosBasicos[28] == "1") ? true : false,
                        (aDatosBasicos[29] == "1") ? true : false,
                        (aDatosBasicos[30] == "1") ? true : false,
                        (aDatosBasicos[33] == "1") ? true : false,
                        aDatosBasicos[34],
                        (aDatosBasicos[35] == "1") ? true : false,
                        Utilidades.unescape(aDatosBasicos[36]),
                        (aDatosBasicos[37] == "1") ? true : false,
                        (aDatosBasicos[38] == "1") ? true : false,
                        aDatosBasicos[39],//Moneda
                        int.Parse(aDatosBasicos[40]),//Cualificador 
                        idOrgCom,//Organización Comercial
                        (aDatosBasicos[42] == "1") ? true : false,//chkQEQ
                        (aDatosBasicos[44] == "1") ? true : false//Instrumental
                        );
                    #endregion
                }
                else //update
                {
                    #region update
                    nID = int.Parse(aDatosBasicos[0]);
                    //string sNodoAux = SUPER.BLL.OrganizacionComercial.NodosOrgCom(tr, nID, idOrgCom);
                    //if (sNodoAux != "")
                    //{
                    //    bErrorControlado = true;
                    //    throw (new Exception("La organización comercial ya está asignada al " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " " + sNodoAux));
                    //}
                    if (NODO.bExistenMesesAbiertos(tr, nID, int.Parse(aDatosBasicos[22])))
                    {
                        bErrorControlado = true;
                        throw (new Exception("Existen meses abiertos en proyectos contratantes o replicados con gestión iguales o anteriores al último cierre económico indicado."));
                    }
                    //Si el check de instrumental a pasado de NO a SI, hacer la comprobación de que el nodo no tiene profesionales ni proyectos asignados
                    if (aDatosBasicos[43] == "N" && aDatosBasicos[44] == "1")
                    {
                        if (NODO.GetNumEmpleadosTotal(tr, nID) > 0)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No se puede indicar como instrumental porque existen usuarios asociados."));
                        }
                        if (NODO.GetNumProyectos(tr, nID) > 0)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No se puede indicar como instrumental porque existen proyectos asociados."));
                        }
                    }
                    NODO.Update(tr,
                    nID,
                    Utilidades.unescape(aDatosBasicos[1]),
                    (aDatosBasicos[9] == "1") ? true : false,
                    int.Parse(aDatosBasicos[3]),
                    int.Parse(aDatosBasicos[17]),
                    (aDatosBasicos[18] == "1") ? true : false,
                    int.Parse(aDatosBasicos[22]),
                    (aDatosBasicos[8] == "1") ? true : false,
                    aDatosBasicos[5],
                    aDatosBasicos[6],
                    (aDatosBasicos[10] == "1") ? true : false,
                    byte.Parse((aDatosBasicos[15] == "") ? "0" : aDatosBasicos[15]),
                    int.Parse(aDatosBasicos[2]),
                    int.Parse(aDatosBasicos[4]),
                    (aDatosBasicos[12] == "1") ? true : false,
                    aDatosBasicos[7],
                    (aDatosBasicos[16] == "1") ? true : false,
                    (aDatosBasicos[13] == "1") ? true : false,
                    (aDatosBasicos[18] == "1") ? true : false,
                    int.Parse(aDatosBasicos[21]),
                    (aDatosBasicos[14] == "1") ? true : false,
                    (aDatosBasicos[20] == "1") ? true : false,
                    float.Parse(aDatosBasicos[23]),
                    float.Parse(aDatosBasicos[24]),
                    Utilidades.unescape(aDatosBasicos[25]),
                    (aDatosBasicos[26] == "1") ? true : false,
                    (aDatosBasicos[27] == "") ? null : aDatosBasicos[27],
                    (aDatosBasicos[31] == "1") ? true : false,
                    (aDatosBasicos[11] == "1") ? true : false,//Permite generar replicas con gestión
                    (aDatosBasicos[32] == "1") ? true : false,
                    (aDatosBasicos[28] == "1") ? true : false,
                    (aDatosBasicos[29] == "1") ? true : false,
                    (aDatosBasicos[30] == "1") ? true : false,
                    (aDatosBasicos[33] == "1") ? true : false,
                    aDatosBasicos[34],
                    (aDatosBasicos[35] == "1") ? true : false,
                    Utilidades.unescape(aDatosBasicos[36]),
                    (aDatosBasicos[37] == "1") ? true : false,
                    (aDatosBasicos[38] == "1") ? true : false,
                    aDatosBasicos[39],//Moneda
                    int.Parse(aDatosBasicos[40]),//Cualificador
                    idOrgCom,
                    (aDatosBasicos[42] == "1") ? true : false,//chkQEQ
                    (aDatosBasicos[44] == "1") ? true : false//Instrumental
                    );
                    #endregion
                }
            }

            #endregion
            #region Datos Soporte al negocio
            if (strSoporte != "")//No se ha modificado nada de la pestaña de Soporte al negocio
            {
                string[] aDatosSoporte = null;
                aDatosSoporte = Regex.Split(strSoporte, "##");

                foreach (string oDatosSoporte in aDatosSoporte)
                {
                    if (oDatosSoporte == "") continue;
                    string[] aValores = Regex.Split(oDatosSoporte, "@@");
                    ///aValores[0] = bd
                    ///aValores[1] = t316_idmodalidad
                    ///
                    switch (aValores[0])
                    {
                        case "I":
                            NODOMODALIDADCONTRATO.Insert(tr, nID, byte.Parse(aValores[1]));
                            break;
                        case "D":
                            NODOMODALIDADCONTRATO.Delete(tr, nID, byte.Parse(aValores[1]));
                            break;
                    }
                }
            }
            #endregion
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

                    //FIGURANODO.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                    //if (aFig[0] != "D")
                    //{
                    //    string[] aFiguras = Regex.Split(aFig[2], ",");
                    //    foreach (string oFigura in aFiguras)
                    //    {
                    //        if (oFigura == "") continue;
                    //        FIGURANODO.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                    //    }
                    //}
                    if (aFig[0] == "D")
                        FIGURANODO.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
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
                                FIGURANODO.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                            else
                                FIGURANODO.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
                }
            }

            #endregion
            #region Datos Figuras Virtuales
            if (strFigurasV != "")//No se ha modificado nada de la pestaña de Figuras virtuales
            {
                string[] aSn = Regex.Split(strFigurasV, "///");
                foreach (string oSn in aSn)
                {
                    if (oSn == "") continue;
                    string[] aSubN = Regex.Split(oSn, "##");

                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras

                    if (aSubN[0] == "D")
                        FIGURAPSN_NODO.DeleteUsuario(tr, nID, int.Parse(aSubN[1]));
                    else
                    {
                        string[] aFigurasV = Regex.Split(aSubN[2], ",");
                        foreach (string oFiguraV in aFigurasV)
                        {
                            if (oFiguraV == "") continue;
                            string[] aFig2V = Regex.Split(oFiguraV, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2V[0] == "D")
                                FIGURAPSN_NODO.Delete(tr, nID, int.Parse(aSubN[1]), aFig2V[1]);
                            else
                                FIGURAPSN_NODO.Insert(tr, nID, int.Parse(aSubN[1]), aFig2V[1]);
                        }
                    }
                }
            }

            #endregion
            #region Datos Alertas
            if (srtAlertas != "")//No se ha modificado nada de la pestaña de Alertas
            {
                SUPER.BLL.NodoAlertas.Grabar(srtAlertas);
            }
            #endregion
            #region Subnodos por defecto para réplica (Maniobra tipo 3)
            if (sSubNodosDefectoReplica != "")//No se ha modificado nada de la pestaña de SubNodos por Defecto defecto para replica
            {
                string[] aSn = Regex.Split(sSubNodosDefectoReplica, "///");
                foreach (string oSn in aSn)
                {
                    if (oSn == "") continue;
                    string[] aSubN = Regex.Split(oSn, "##");
                    ///aSubN[0] = bd
                    ///aSubN[1] = idSubNodo
                    ///aSubN[2] = estado
                    if (aSubN[0] == "U")
                    {
                        //FIGURAPSN_NODO.Insert(tr, nID, int.Parse(aSubN[1]), aFig2V[1]);
                        if (aSubN[2] == "1")
                            SUPER.Capa_Negocio.SUBNODO.SetManiobra(tr, int.Parse(aSubN[1]), 3);//Maniobra tipo 3
                        else
                            SUPER.Capa_Negocio.SUBNODO.SetManiobra(tr, int.Parse(aSubN[1]), 0);//Maniobra tipo 0
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
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento de estructura", ex, false);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del nodo", ex, false);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;

        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string obtenerCentrosCoste(string sPestana, string sIDNodo)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = CENCOS.CatalogoByNodo(null, int.Parse(sIDNodo));

            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 800px;'>");
            sb.Append("<colgroup><col style='width: 50px;' /><col style='width: 350px;' /><col style='width: 350px;' /><col style='width: 50px;text-align:center;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t317_idcencos"].ToString() + "' style='height:20px;'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t317_idcencos"].ToString() + "</td>");
                sb.Append("<td>" + dr["t317_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t304_denominacion"].ToString() + "</td>");
                if ((bool)dr["t317_estadogasvi"]) sb.Append("<td><img src='../../../images/imgOK.gif' /></td>");
                else sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de centros de coste.", ex);
        }
    }
    private string obtenerParametros(string sPestana, int iIDNodo)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = MODALIDADCONTRATO.Catalogo(iIDNodo, false); 

            sb.Append("<TABLE id='tblSoporte' class='texto'  style='width:260px;'>");
            sb.Append("<colgroup><col style='width: 220px;' /><col style='width: 40px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t316_idmodalidad"].ToString() + "' old='" + dr["seleccionado"].ToString() +  "' style='height:18px;'>");
                //sb.Append("<tr style='height:22px;'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t316_denominacion"].ToString() + "</td>");
                sb.Append("<td><INPUT hideFocus class='check' ");
                if (dr["seleccionado"].ToString()=="1") sb.Append("checked ");
                sb.Append("type=checkbox onclick='aGGenParam(1,this);' runat='server'/></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los parámetros del C.R.", ex);
        }
    }
    private string obtenerAlertas(string sPestana, string sIDNodo)
    {
        try
        {
            return "OK@#@" + sPestana + "@#@" + SUPER.BLL.NodoAlertas.CatalogoByNodo(int.Parse(sIDNodo));
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de alertas.", ex);
        }
    }
    private string Duplicar(string sIdNodoOrigen)
    {
        string sResul = "";
        int nIDOrigen = int.Parse(sIdNodoOrigen), nID=-1;
        bool bErrorControlado = false;

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
            nID = NODO.Duplicar(tr, nIDOrigen);

            if (nID != -1)
            {
                NODOMODALIDADCONTRATO.Duplicar(tr, nIDOrigen, nID);
                FIGURANODO.Duplicar(tr, nIDOrigen, nID);
                FIGURAPSN_NODO.Duplicar(tr, nIDOrigen, nID);
                //SUPER.DAL.NodoAlertas.Duplicar(tr, nID);
                SUPER.BLL.NodoAlertas.Duplicar(tr, nIDOrigen, nID);
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento de estructura", ex, false);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del nodo", ex, false);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;

        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    /// <summary>
    /// Obtiene un catálogo de subnodos de maniobra 0 y 3
    /// </summary>
    /// <param name="sPestana"></param>
    /// <param name="idNodo"></param>
    /// <returns></returns>
    private string obtenerSubNodosManiobraTipo3(string sPestana, int idNodo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            var lstTiposmaniobra = new ArrayList();
            lstTiposmaniobra.Add(0);
            lstTiposmaniobra.Add(3);

            SqlDataReader dr = SUBNODO.CatalogoPorTipoManiobra(null, idNodo, lstTiposmaniobra);

            sb.Append("<table id='tblSubNodomaniobra3' class='texto' style='width:500px;' mantenimiento='1' >");
            sb.Append("<colgroup><col style='width:400px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t304_idsubnodo"].ToString() + "' bd='N' style='height:20px;'>");

                sb.Append("<td><nobr class='NBR W390' onmouseover='TTip(event)'>" + dr["t304_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center;'>");
                if (dr["t304_maniobra"].ToString() == "3")
                    sb.Append("<input type='checkbox' class='check' checked onclick='setSnD(event)'>");
                else
                    sb.Append("<input type='checkbox' class='check' onclick='setSnD(event)'>");
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de subnodos de maniobra de tipo 3.", ex);
        }
    }


}
