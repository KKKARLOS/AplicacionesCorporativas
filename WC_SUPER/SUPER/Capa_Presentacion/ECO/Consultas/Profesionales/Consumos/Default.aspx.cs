using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Capa_Presentacion_JornEconomicas_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string sNodo, sMSC, sMSCMA; //Mes siguiente al último mes cerrado
    protected DataSet ds;
    public SqlConnection oConn;
    public SqlTransaction tr;
    private bool bHayPreferencia = false;
    public short nPantallaPreferencia = 33;
    	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Cuadre de consumos económicos";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            try
            {
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}
                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                this.lblNodo.InnerText = sNodo;
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                //this.lbl1.InnerText = "Asociados a proyectos del " + this.lblNodo.InnerText;
                this.lblInternos.InnerText = "Del " + sNodo;
                this.lblOtrosNodos.InnerText = "De otros " + sNodo + "s";
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                {
                    cboCR.Visible = false;
                    hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = true;
                    gomaNodo.Visible = true;
                }
                else
                {
                    cboCR.Visible = true;
                    hdnIdNodo.Visible = false;
                    txtDesNodo.Visible = false;
                    gomaNodo.Visible = false;
                    cargarNodos();
                }
                int iUMC;
                string sUMC = USUARIO.getUMCNodo(null, (int)Session["UsuarioActual"]);
                if (sUMC != "")
                    iUMC = Fechas.AddAnnomes(int.Parse(sUMC), 1);
                else
                {
                    //DateTime dtUMC = System.DateTime.Today;
                    //iUMC = (dtUMC.Year * 100) + dtUMC.Month;
                    iUMC = Fechas.AddAnnomes(Fechas.FechaAAnnomes(DateTime.Today), 1);
                }
                this.sMSCMA = iUMC.ToString();
                //PREFERENCIAS
                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    #region preferencia
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        hdnIdNodo.Text = aDatosPref[1];
                        txtDesNodo.Text = aDatosPref[2];
                    }
                    else
                        cboCR.SelectedValue = aDatosPref[1];

                    chkDelNodo.Checked = (aDatosPref[3] == "1") ? true : false;
                    chkExternos.Checked = (aDatosPref[4] == "1") ? true : false;
                    chkOtroNodo.Checked = (aDatosPref[5] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[6] == "1") ? true : false;
                    chkRPA.Checked = (aDatosPref[7] == "1") ? true : false;

                    this.rdbCoste.SelectedValue = aDatosPref[8];
                    this.hdnModeloCoste.Value = aDatosPref[8];

                    if (chkActuAuto.Checked)
                    {
                        btnObtener.Disabled = true;

                        string strTabla = ObtenerJornadasReportadas(iUMC, iUMC, aDatosPref[1], aDatosPref[3], aDatosPref[4], aDatosPref[5], aDatosPref[7], aDatosPref[8]);
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK") divCatalogo2.InnerHtml = aTabla[1];
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                    else
                        divCatalogo2.InnerHtml = "<table id='tblDatos'></table>";
                    #endregion
                }
                else
                {
                    if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                    else
                    {
                        divCatalogo2.InnerHtml = "<table id='tblDatos'></table>";
                    }
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener la consulta de jornadas reportadas.", ex);
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
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
            case ("buscar"):
                sResultado += ObtenerJornadasReportadas(int.Parse(aArgs[1]), int.Parse(aArgs[2]), aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
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
    private string ObtenerJornadasReportadas(int anomesD, int anomesH, string sCR, string sDelNodo, string sExternos, 
                                             string sOtrosNodos, string sSoloProyAbiertos, string sModeloCoste)
    {
        string sResul = "";
        //Andoni -> no sacamos el motivo por el que sale un usuario. Siempre va a ser porque está asociado a un proyecto
        //para el que tenemos ámbito de visión
        //string sUserAnt = "-1";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width: 950px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");//imagen
            sb.Append("<col style='width:430px' />");//profesional
            sb.Append("<col style='width:200px' />");//calendario
            sb.Append("<col style='width:50px;' />");//jornadas en el calendario
            sb.Append("<col style='width:125px;' />");//jornadas en proyectos bajo mi ambito
            sb.Append("<col style='width:125px;' />");//jornadas en todos los proyectos
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            DateTime dInicio = Fechas.AnnomesAFecha(anomesD);
            DateTime dtAux = Fechas.AnnomesAFecha(anomesH);
            DateTime dFin = dtAux.AddMonths(1).AddDays(-1);
            ds = Consumo.ObtenerConsumosEconomicos((int)Session["UsuarioActual"], dInicio, dFin, (sCR == "") ? null : (int?)int.Parse(sCR),
                        (sDelNodo == "1") ? true : false, (sExternos == "1") ? true : false, (sOtrosNodos == "1") ? true : false,
                        (sSoloProyAbiertos == "1") ? true : false, sModeloCoste);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    #region Indices de campos
                    /*
                     * 0 - t314_idusuario,
                     * 1 - t001_sexo
                     * 2 - tecnico,
                     * 3 - Calendario,
                     * 4 - JLCalendario,
                     * 5 - HLCalendario,
                     * 6 - t314_falta
                     * 7 - t314_fbaja
                     * 8 - denominacion une,
                     * 9 - denominacion empresa o proveedor
                     * 10 - baja,
                     * 11 - unidades económicas bajo mi ámbito de visión
                     * 12 - unidades ecónomicas totales
                     */
                    #endregion

                    //sb.Append("<tr R=" + ((int)oFila["t314_idusuario"]).ToString("#,###") + " style='DISPLAY: block; height:20px;'");
                    sb.Append("<tr style='height:20px;'");
                    //if (oFila["t303_denominacion"].ToString() == "")
                    //    sb.Append(" tipo ='E'");
                    //else
                    //{
                    //    if (sCR == oFila["t303_idnodo"].ToString())
                    //        sb.Append(" tipo ='P'");
                    //    else
                    //        sb.Append(" tipo ='N'");
                    //}
                    sb.Append("tipo='" + oFila["tipo"].ToString() + "' ");
                    sb.Append(" nodo ='" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "'");
                    sb.Append(" sexo ='" + oFila["t001_sexo"].ToString() + "'");
                    sb.Append(" baja ='" + oFila["baja"].ToString() + "'>");

                    //Icono
                    //sb.Append("<td style=\"border-right:''\"></td>");
                    sb.Append("<td></td>");
                    //profesional
                    //sb.Append("<td><nobr class='NBR W430' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)oFila["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + oFila["tecnico"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR' style='noWrap:true; width:430px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)oFila["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + oFila["tecnico"].ToString() + "</nobr></td>");
                    //Calendario
                    sb.Append("<td><nobr class='NBR' style='width:200px;'>" + oFila["Calendario"].ToString() + "</nobr></td>");
                    //Días laborables Calendario
                    if (sModeloCoste=="H")
                        sb.Append("<td style='text-align:right;'>" + oFila["HLCalendario"].ToString() + "</td>");
                    else
                        sb.Append("<td style='text-align:right;'>" + oFila["JLCalendario"].ToString() + "</td>");
                    //Jornadas económicas
                    sb.Append("<td style='text-align:right;'>" + double.Parse(oFila["mis_unidades"].ToString()).ToString("N") + "</td>");
                    //Jornadas económicas totales
                    //sb.Append("<td style=\"border-right:''\">" + double.Parse(oFila["tot_unidades"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(oFila["tot_unidades"].ToString()).ToString("N") + "</td>");
                    sb.Append("</tr>");
                }
            }

            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los consumos reportados.", ex);
        }

        return sResul;
    }

    #region Preferencias
    private string setPreferencia(string sActuAuto, string sNodo, string sDelNodo, string sExternos, string sOtrosNodos, 
                                  string sSoloProyAbiertos, string sModeloCoste)
    {
        string sResul = "";
        string strNodo = null, strActuAuto = null;

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
            if (sNodo != "") strNodo = sNodo;
            if (sActuAuto != "") strActuAuto = sActuAuto;

            int nPref = PREFERENCIAUSUARIO.Insertar(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 33, strNodo, sDelNodo, sExternos, sOtrosNodos, strActuAuto,
                                                    sSoloProyAbiertos,sModeloCoste, 
                                                    null, null, null, null, null, null, null, null, null, null, null, null, null, null);
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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 33);
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
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                        (int)Session["IDFICEPI_PC_ACTUAL"], 33);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["idNodo"].ToString() + "@#@"); //0
                sb.Append(dr["t303_denominacion"].ToString() + "@#@"); //1
                sb.Append(dr["DelNodo"].ToString() + "@#@"); //2
                sb.Append(dr["Externos"].ToString() + "@#@"); //3
                sb.Append(dr["otrosNodos"].ToString() + "@#@"); //4  
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //5
                sb.Append(dr["sSoloProyAbiertos"].ToString() + "@#@"); //6
                sb.Append(dr["sModeloCoste"].ToString()); //7
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
    #endregion
}
