using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTituloMovilHTML = "<table id='tblTituloMovil' style='width:800px; text-align:right;' class='TBLINI'></table>";
    public string strBodyMovilHTML = "<table id='tblBodyMovil' style='width:800px; text-align:right;'></table>";
    public string strBodyFijoHTML = "<table id='tblBodyFijo' style='width:400px; text-align:right;'></table>";
    public string sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    public string sSubnodos = "", sNodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    ArrayList aNodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLRol = "", strHTMLSupervisor = "", strHTMLCentroTrabajo = "", strHTMLOficina = "", strHTMLProfesional = "", strHTMLResponsable = "";
    public string strIDsAmbito = "", strIDsRol = "", strIDsSupervisor = "", strIDsCentroTrabajo = "", strIDsOficina = "", strIDsProfesional = "", strIDsResponsable = "";

    public short nPantallaPreferencia = 37;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!User.IsInRole("DIS") && (int)Session["UsuarioActual"] != 1406){
            //    try { Response.Redirect("~/Capa_Presentacion/Ayuda/Obras/Default.aspx", true); }
            //    catch (System.Threading.ThreadAbortException) { }
            //}

            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

            if (!Page.IsCallback)
            {
                Master.TituloPagina = "Consulta de disponibilidad";
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.nPantallaAcceso = 2;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}

                hdnDesde.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtDesde.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();
                hdnFecha.Text = hdnDesde.Text;
                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerTabla(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            //case ("setResolucion"):
            //    sResultado += setResolucion();
            //    break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;
            case ("cargarArrays"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]), short.Parse(aArgs[3]));
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

    private string cargarCriterios(int nDesde, int nHasta, short iTipo)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            /*
             * t -> tipo
             * c -> codigo
             * d -> denominacion
             * ///datos auxiliares 
             * */
            SqlDataReader dr = ConsultasPGE.ObtenerDisponibilidadesCriterios((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios, iTipo);

            while (dr.Read())
            {
                if (dr["codigo"] == DBNull.Value) continue;
                if ((int)dr["codigo"] == -1)
                    sb.Append("\tjs_opsel[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    sb.Append("\tjs_opsel[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }
    private string ObtenerTabla(string sDisponibilidad, string sMisProyectos, string sFechaDesde, string sFechaHasta, string sRol, string sSupervisor, string sCentroTrab, string sOficina, string sProfesional, string sResponsable, string sEstructuraNodos)
    {
        int nWidthTabla = 400;
        int nColumnasACrear = 0;
        int nIndiceColPrimerMes = 19;
        string sComun = "";

        StringBuilder sbA = new StringBuilder(); //body fijo
        StringBuilder sbB = new StringBuilder(); //body móvil

        StringBuilder sbColgroupTitulo = new StringBuilder();
        StringBuilder sbTitulo = new StringBuilder();

        string sTablaTituloMovil = "";
        string sTablaBodyMovil = "";

        //string sTablaResultado = "";
        try
        {
            sbA.Append("<table id='tblBodyFijo' style='width:400px; text-align:left;' cellpadding='0' cellspacing='0' border='0'>");
            sbA.Append("<colgroup>");
            sbA.Append("<col style='width:20px;'/>");  //Icono (EXPANDIR/CONTRAER)
            sbA.Append("<col style='width:20px;'/>");  //Icono tipo recurso/sexo
            sbA.Append("<col style='width:360px;'/>"); //Profesional/Concepto
            sbA.Append("</colgroup>");
            //sbA.Append("<tbody>");

            DataSet ds = USUARIO.ConsultaDisponibilidad((int)Session["UsuarioActual"], int.Parse(sFechaDesde), int.Parse(sFechaHasta), sDisponibilidad, (sMisProyectos=="1")? true: false , sRol, sSupervisor, sCentroTrab, sOficina, sProfesional, sResponsable, sEstructuraNodos);

            bool bTitulos = false;
            int i = 0;
            int iUsuario = 0;
            int iContUsu = 0;

            DataRow oFilaCal = null;
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    if (!bTitulos)
                    {
                        sbTitulo.Append("<tr class='TBLINI'>");
                        for (int x = nIndiceColPrimerMes; x < ds.Tables[0].Columns.Count; x++)
                        {
                            if (x < nIndiceColPrimerMes) continue;
                            sbColgroupTitulo.Append("<col style='width:60px;' />");
                            sbTitulo.Append("<td align='center'>" + Fechas.AnnomesAFechaDescCorta(int.Parse(ds.Tables[0].Columns[x].ColumnName)) + "</td>");
                            nColumnasACrear++;
                        }
                        sbTitulo.Append("</tr>");
                        bTitulos = true;
                    }

                    if (oFila["tipodato"].ToString() == "0") { oFilaCal = oFila; continue; };

                    sComun = "<tr id='" + oFila["t314_idusuario"].ToString() + "/" + oFila["t305_idproyectosubnodo"].ToString() + "/" + oFila["tipodato"].ToString() + "' ";

                    if (iUsuario != int.Parse(oFila["t314_idusuario"].ToString()))
                    {
                        iUsuario = int.Parse(oFila["t314_idusuario"].ToString());
                        iContUsu++;
                    }

                    string sClass = "";
                    if (iContUsu % 2 == 0) sClass += "FA";
                    else sClass += "FB";

                    sbA.Append(sComun);
                    sbB.Append(sComun);

                    sbA.Append(" usu='" + oFila["t314_idusuario"].ToString() + "' ");
                    sbA.Append(" cr=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                    sbA.Append(" supervisor=\"" + Utilidades.escape(oFila["supervisor"].ToString()) + "\" ");
                    sbA.Append(" centro=\"" + Utilidades.escape(oFila["centroTrab"].ToString()) + "\" ");
                    sbA.Append(" oficina=\"" + Utilidades.escape(oFila["oficina"].ToString()) + "\" ");
                    sbA.Append(" rol=\"" + Utilidades.escape(oFila["rol"].ToString()) + "\" ");
                    sbA.Append(" responsable=\"" + Utilidades.escape(oFila["responsable"].ToString()) + "\" ");
                    sbA.Append(" cliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");
                    sbA.Append(" tipo='" + oFila["tipo"].ToString() + "'");
                    sbA.Append(" sexo='" + oFila["sexo"].ToString() + "'");
                    sbA.Append(" tipodato='" + oFila["tipodato"].ToString() + "'");
                    sbA.Append(" calendario=\"" + Utilidades.escape(oFila["t066_descal"].ToString()) + "\" ");

                    sbA.Append(" falta='" + oFila["faltaEmp"].ToString()+"'");

                    if (oFila["fbajaEmp"] == DBNull.Value) sbA.Append(" fbaja='000000' ");
                    else sbA.Append(" fbaja='" + oFila["fbajaEmp"].ToString() + "' ");

                    if (oFila["tipodato"].ToString() == "1") sComun = " class='" + sClass + "' nivel='1' desplegado='1' style='display:table-row;";
                    else sComun = " nivel='2' desplegado='0' class='" + sClass + " htr";
                    //else sComun = " nivel='2' desplegado='0' style='display:none;' class='" + sClass;
                    
                    if (oFila["tipodato"].ToString() == "1" && i!=0) sComun += "border-top: solid 1px #A6C3D2;";
                    sComun += "' ";

                    sbA.Append(sComun);
                    //sbA.Append("onclick='setFilaFija(this)'");

                    sbB.Append(sComun);
                    //sbB.Append("onclick='setFilaMovil(this)'");
                    
                    if (oFila["tipodato"].ToString() == "1")
                    {
                        //sbA.Append("><td><IMG class=NSEG1 onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'></td>");
                        sbA.Append("><td></td>");
                        sbA.Append("<td></td><td align='left' class='tdbr' style='padding-left:3px;font-weight: bold'>");
                        //sTooltip = Utilidades.escape("<label style='width:70px'>" + sNodo + ":</label>" + oFila["t303_denominacion"].ToString() + "<br/><label style='width:70px'>Supervisor:</label>" + oFila["supervisor"].ToString() + "<br/><label style='width:70px'>Centro:</label>" + oFila["centroTrab"].ToString() + "<br/><label style='width:70px'>Oficina:</label>" + oFila["oficina"].ToString() + "<br/><label style='width:70px'>Rol:</label>" + oFila["rol"].ToString() + "<br/><label style='width:70px'>Calendario:</label>" + oFila["t066_descal"].ToString());
                        //sbA.Append("<nobr style='font-weight: bold' class='NBR W360' onmouseover=showTTE(\"" + sTooltip + "\") onMouseout=\"hideTTE()\">" + oFila["descripcion"].ToString() + "</nobr>");
                        sbA.Append(oFila["descripcion"].ToString() + "</td>");
                    }
                    else
                    {
                        sbA.Append("><td></td><td></td>");
                        sbA.Append("<td align='left' class='tdbr' style='padding-left:10px;white-space:nowrap;'>");
                        if (oFila["tipodato"].ToString() == "3")
                        {
                            //sTooltip = Utilidades.escape("<label style='width:70px'>" + sNodo + ":</label>" + oFila["t303_denominacion"].ToString() + "<br/><label style='width:70px'>Responsable:</label>" + oFila["responsable"].ToString() + "<br/><label style='width:70px'>Cliente:</label>" + oFila["t302_denominacion"].ToString());
                            //sbA.Append("<nobr class='NBR W360' onmouseover=showTTE(\"" + sTooltip + "\") onMouseout=\"hideTTE()\">" + oFila["descripcion"].ToString() + "</nobr>");
                            sbA.Append(oFila["descripcion"].ToString() + "</td>");
                        }
                        else
                            //sbA.Append("<nobr class='NBR W360'>" + oFila["descripcion"].ToString() + "</nobr></td>");
                            sbA.Append(oFila["descripcion"].ToString() + "</td>");
                    }
                    sbB.Append(">");

                    for (int x = nIndiceColPrimerMes; x < ds.Tables[0].Columns.Count; x++)
                    {
                        if (x < nIndiceColPrimerMes) continue;
                        sbB.Append("<td align='right'");
                        if (x == nIndiceColPrimerMes) sbB.Append(" class='tdbrl'");

                        if (oFila["tipodato"].ToString() == "1")
                        {
                            if (decimal.Parse(oFilaCal.ItemArray[x].ToString()) == 0) sbB.Append(" style='background-color: #F58D8D;font-weight: bold;'");
                            else sbB.Append(" style='font-weight: bold'");
                        }

                        sbB.Append(">");

                        sbB.Append(decimal.Parse(oFila.ItemArray[x].ToString()).ToString("N"));

                        sbB.Append("</td>");
                    }
                    sbA.Append("</tr>");
                    sbB.Append("</tr>");
                    i++;
                }
            }
            sbA.Append("</table>");
            ds.Dispose();

            nWidthTabla = nColumnasACrear * 60;
            sTablaTituloMovil = "<table id='tblTituloMovil' class='texto' style='width:" + nWidthTabla.ToString() + "px;' cellpadding='0'>";
            sTablaTituloMovil += "<colgroup>";
            sTablaTituloMovil += sbColgroupTitulo.ToString();
            sTablaTituloMovil += "</colgroup>";
            sTablaTituloMovil += sbTitulo.ToString();
            sTablaTituloMovil += "</table>";

            sTablaBodyMovil = "<table id='tblBodyMovil' class='texto' style='width:" + nWidthTabla.ToString() + "px; ' mantenimiento='1' cellpadding='0' cellspacing='0' border='0'>";
            sTablaBodyMovil += "<colgroup>";
            sTablaBodyMovil += sbColgroupTitulo.ToString();
            sTablaBodyMovil += "</colgroup>";
            sTablaBodyMovil += sbB.ToString();
            sTablaBodyMovil += "</table>";

            return "OK@#@" + sTablaTituloMovil + "@#@" + sTablaBodyMovil + "@#@" + sbA.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
        }
    }

    //private string setResolucion()
    //{
    //    try
    //    {
    //        Session["DATOSRES1024"] = !(bool)Session["DATOSRES1024"];

    //        USUARIO.UpdateResolucion(4, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["DATOSRES1024"]);

    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
    //    }
    //}

    private string setPreferencia(string sDisponibilidad, string sMisProyectos, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sValoresMultiples)
    {
        string sResul = "";

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
            int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 37,
                                        (sDisponibilidad == "") ? null : sDisponibilidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sMisProyectos == "") ? null : sMisProyectos,
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            #region Valores Múltiples
            if (sValoresMultiples != "")
            {
                string[] aValores = Regex.Split(sValoresMultiples, "///");
                foreach (string oValor in aValores)
                {
                    if (oValor == "") continue;
                    string[] aDatos = Regex.Split(oValor, "##");
                    ///aDatos[0] = concepto
                    ///aDatos[1] = idValor
                    ///aDatos[2] = denominacion

                    PREFUSUMULTIVALOR.Insertar(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 37);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        int idPrefUsuario = 0; //, nConceptoEje=0;
        try
        {
            bHayPreferencia = false;
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "")? null:(int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 37);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["cboDisponibilidad"].ToString() + "@#@"); //2
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //3
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //4
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //5
                sb.Append(dr["chkMisProyectos"].ToString() + "@#@"); //6

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
            }

            dr.Close();
            //dr.Dispose();
            
            if (bHayPreferencia == false) return "OK@#@NO@#@";

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//7
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//8
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//10
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//7
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//8
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//10
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//7
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//8
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//10
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//7
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//8
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//10
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//7
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//8
                    sb.Append("207812" + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//10
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//7
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//8
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//10
                    break;
            }
            #endregion

            #region HTML, IDs 
            //int nNivelMinimo = 0;
            //bool bAmbito = false;
            string[] aID = null;
            dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
            while (dr.Read())
            {
                switch (int.Parse(dr["t441_concepto"].ToString()))
                {
                    case 1:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");

                        if (strIDsAmbito != "") strIDsAmbito += ",";
                        strIDsAmbito += aID[1];

                        aNodos = PREFUSUMULTIVALOR.SelectNodosAmbito(null, aNodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        //aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:18px;' idAux='";
                        //strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += NODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../../images/imgSN4.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../../images/imgSN3.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../../images/imgSN2.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../../images/imgSN1.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../../images/imgNodo.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../../images/imgSubNodo.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                        }

                        strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 2:
                        if (strIDsResponsable != "") strIDsResponsable += ",";
                        strIDsResponsable += dr["t441_valor"].ToString();
                        strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 23:
                        if (strIDsRol != "") strIDsRol += ",";
                        strIDsRol += dr["t441_valor"].ToString();
                        strHTMLRol += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 24:
                        if (strIDsSupervisor != "") strIDsSupervisor += ",";
                        strIDsSupervisor += dr["t441_valor"].ToString();
                        strHTMLSupervisor += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 25:
                        if (strIDsCentroTrabajo != "") strIDsCentroTrabajo += ",";
                        strIDsCentroTrabajo += dr["t441_valor"].ToString();
                        strHTMLCentroTrabajo += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 26:
                        if (strIDsOficina != "") strIDsOficina += ",";
                        strIDsOficina += dr["t441_valor"].ToString();
                        strHTMLOficina += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 27: if (strIDsProfesional != "") strIDsProfesional += ",";
                        strIDsProfesional += dr["t441_valor"].ToString();
                        strHTMLProfesional += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion


            //for (int i = 0; i < aSubnodos.Count; i++)
            //{
            //    if (i > 0) sSubnodos += ",";
            //    sSubnodos += aSubnodos[i];
            //}

            for (int i = 0; i < aNodos.Count; i++)
            {
                if (i > 0) sNodos += ",";
                sNodos += aNodos[i];
            }

            sb.Append(sNodos + "@#@"); //11
            sb.Append(strHTMLAmbito + "@#@"); //12
            sb.Append(strIDsAmbito + "@#@"); //13
            sb.Append(strHTMLRol + "@#@"); //14
            sb.Append(strIDsRol + "@#@"); //15
            sb.Append(strHTMLSupervisor + "@#@"); //16
            sb.Append(strIDsSupervisor + "@#@"); //17
            sb.Append(strHTMLCentroTrabajo + "@#@"); //18
            sb.Append(strIDsCentroTrabajo + "@#@"); //19
            sb.Append(strHTMLOficina + "@#@"); //20
            sb.Append(strIDsOficina + "@#@"); //21
            sb.Append(strHTMLProfesional + "@#@"); //22
            sb.Append(strIDsProfesional + "@#@"); //23
            //sb.Append((bHayPreferencia) ? "S@#@" : "N@#@"); //24
            sb.Append(strHTMLResponsable + "@#@"); //24
            sb.Append(strIDsResponsable + "@#@"); //25
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }

}
