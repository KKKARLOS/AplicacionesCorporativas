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

public partial class Capa_Presentacion_JornReportadas_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string sMSC, sMSCMA; //Mes siguiente al último mes cerrado
    protected DataSet ds;
    public SqlConnection oConn;
    public SqlTransaction tr;
    private bool bHayPreferencia = false;
    public short nPantallaPreferencia = 9;
    public string es_rgf = "N";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Consumos reportados";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            try
            {
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}
                gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                gomaGF.Attributes.Add("title", "Borra el grupo funcional");
                this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                this.lbl1.InnerText = "Asociados a proyectos del " + this.lblNodo.InnerText;
                this.lblOtrosNodos.InnerText = "Otros " + this.lblNodo.InnerText + "s";
                
                es_rgf = (User.IsInRole("RG")) ? "S" : "N";
                bool bEsAdminProduccion = false;
                try {
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        bEsAdminProduccion=true;    
                }
                catch {
                    bEsAdminProduccion = false;
                    SUPER.DAL.Log.Insertar("PSP.Consultas.JornReportadas->Error al llamar a SUPER.Capa_Negocio.Utilidades.EsAdminProduccion" + Session["IDFICEPI_ENTRADA"].ToString());
                }

                if (bEsAdminProduccion)
                {
                    cboCR.Visible = false;
                    hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = true;
                    gomaNodo.Visible = true;

                    cboGF.Visible = false;
                    hdnIdGF.Visible = true;
                    txtGF.Visible = true;
                    gomaGF.Visible = true;
                }
                else
                {
                    cboCR.Visible = true;
                    hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = false;
                    cargarNodos();
                    if (es_rgf == "S")
                    {
                        cboGF.Visible = true;
                        hdnIdGF.Visible = true;
                        txtGF.Visible = false;
                        if (es_rgf == "N")
                        {
                            gomaNodo.Visible = false;
                            gomaGF.Visible = false;
                        }
                        cargarGF();
                    }
                }

                DateTime UMC = Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1);

                DateTime MSUMC = new DateTime(UMC.Year, UMC.Month, 1);
                MSUMC = MSUMC.AddMonths(2);
                MSUMC = MSUMC.AddDays(-1);

                this.sMSCMA = MSUMC.ToShortDateString().Substring(6, 4) + MSUMC.ToShortDateString().Substring(3, 2);

                //PREFERENCIAS
                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    if (bEsAdminProduccion)
                    {
                        if (aDatosPref[1] != "")
                        {
                            hdnIdNodo.Text = aDatosPref[1];
                            txtDesNodo.Text = aDatosPref[2];
                        }
                        else
                        {
                            hdnIdGF.Text = aDatosPref[9];
                            txtGF.Text = aDatosPref[10];
                        }
                    }
                    else
                    {
                        if (aDatosPref[1] != "")
                        {
                            hdnIdNodo.Text = aDatosPref[1];
                            cboCR.SelectedValue = aDatosPref[1];
                        }
                        else
                        {
                            if (es_rgf == "S")
                            {
                                hdnIdGF.Text = aDatosPref[9];
                                cboGF.SelectedValue = aDatosPref[9];

                            }
                        }
                    }

                    //string sIncompletos = "1";
                    if (aDatosPref[3] == "0")
                    {
                        //sIncompletos = "0";
                        rdbIncompletos.Items[0].Selected = true;
                        cboIncompletos.Enabled = false;
                    }
                    else
                    {
                        rdbIncompletos.Items[1].Selected = true;
                        cboIncompletos.SelectedValue = aDatosPref[3];
                    }

                    chkExternos.Checked = (aDatosPref[4] == "1") ? true : false;
                    chkOtroNodo.Checked = (aDatosPref[5] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[6] == "1") ? true : false;
                    chkRPA.Checked = (aDatosPref[7] == "1") ? true : false;
                    chkForaneos.Checked = (aDatosPref[8] == "1") ? true : false;



                    if (chkActuAuto.Checked)
                    {
                        //btnObtener.Disabled = true;

                        string strTabla = ObtenerJornadasReportadas(MSUMC, aDatosPref[1], aDatosPref[9], aDatosPref[3], aDatosPref[4], aDatosPref[5], aDatosPref[6], aDatosPref[7]);
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK") divCatalogo2.InnerHtml = aTabla[1];
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                    else
                        divCatalogo2.InnerHtml = "<table id='tblDatos'></table>";
                }
                else
                {
                    rdbIncompletos.Items[1].Selected = true;
                    if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                    else
                    {
                        divCatalogo2.InnerHtml = "<table id='tblDatos'></table>";
                        //string strTabla = ObtenerJornadasReportadas(UMC,"","","","","1");
                        //string[] aTabla = Regex.Split(strTabla, "@#@");
                        //if (aTabla[0] == "OK") divCatalogo.InnerHtml = aTabla[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener la consulta de jornadas reportadas.", ex);
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
            case ("buscar"):
                string sFecha = "01/"+ aArgs[1];
                DateTime dFecha = DateTime.Parse(sFecha);//.AddDays(-1);
                sResultado += ObtenerJornadasReportadas(dFecha, aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
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
        try
        {
            //Obtener los datos necesarios
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            //SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"]);
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private void cargarGF()
    {
        try
        {
            //Obtener los datos necesarios

            ListItem oLI = null;
            SqlDataReader dr = GrupoFun.VisionGruposFuncionales((int)Session["UsuarioActual"]);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboGF.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los grupos funcionales", ex);
        }
    }
    private string ObtenerJornadasReportadas(DateTime dUMC, string sCR, string sGF, string sIncompletos, string sExternos, string sOtrosNodos, 
                                                string sSoloProyAbiertos, string sForaneos)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        double dDLR = 0, dDLC=0, dHLC=0, dHR=0;
        bool bHaTrabajadoTodoElMes = true;
        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width:950px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:20px' />");
            sb.Append("     <col style='width:335px' />");
            sb.Append("     <col style='width:65px' />");
            sb.Append("     <col style='width:180px' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("     <col style='width:50px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            ds = Consumo.ObtenerJornadasReportadasDS(Fechas.FechaAAnnomes(dUMC), (int)Session["UsuarioActual"], 
                                                    (sCR == "") ? null : (int?)int.Parse(sCR),
                                                    (sGF == "") ? null : (int?)int.Parse(sGF),
                                                    (sExternos == "1") ? true : false, 
                                                    (sOtrosNodos == "1") ? true : false,
                                                    (sSoloProyAbiertos == "1") ? true : false,
                                                    (sForaneos == "1") ? true : false
                                                    );

            //Gente cuya fecha de alta o de baja está dentro del mes siguiente al último mes cerrado
            //Hay que calcularles el número de días laborables para los días que han trabajado en el mes.
            DateTime dInicioMes = dUMC;//.AddDays(1);
            DateTime dFinMes = dUMC.AddMonths(1).AddDays(-1);
            DateTime dAlta;
            DateTime? dBaja = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                //}
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                    #region Indices de campos
                    /*
                     * 0 - t314_idusuario,
                     * 1 - t001_sexo
                     * 2 - tecnico,
                     * 3 - UDR,
                     * 4 - Calendario,
                     * 5 - JLCalendario,
                     * 6 - HLCalendario
                     * 7 - Dias laborables reportados,(jornadas reportadas)
                     * 8 - Horas,
                     * 9 - Jornadas,
                     * 10 - t314_falta,
                     * 11 - t314_fbaja,
                     * 12 - denominacion une,
                     * 13 - denominacion empresa o proveedor
                     * 14 - estado baja (FICEPI y SUPER)
                     * 15- jornadas económicas,
                     */
                    #endregion
                    dBaja = null;
                    dAlta = DateTime.Parse(oFila["t314_falta"].ToString());
                    if (oFila["t314_fbaja"] != System.DBNull.Value)
                        dBaja = DateTime.Parse(oFila["t314_fbaja"].ToString());
                    //Días laborables reportados
                    dDLR = Math.Round(double.Parse(oFila["JReportadas"].ToString()), 2, MidpointRounding.AwayFromZero);
                    //Días laborables del calendario
                    dDLC = Math.Round(double.Parse(oFila["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero);
                    //Horas laborables del calendario
                    dHLC = Math.Round(double.Parse(oFila["HLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero);
                    //Horas reportadas
                    dHR = Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero);

                    int nIncompleto = 0;
                    //Control de fechas
                    
                    //Calcular días laborables de una persona en un mes, en función de las fechas de alta y baja
                    #region nº días laborables en el mes
                    if ((dAlta > dInicioMes && dAlta < dFinMes) && (dBaja != null && dBaja >= dInicioMes && dBaja <= dFinMes))
                    {
                        bHaTrabajadoTodoElMes = false;
                        #region alta y baja en el mismo mes.
                        /*
                        switch (sIncompletos)
                        {
                            case "1":
                                SqlDataReader drUsuario1 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                //nDias = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                if (drUsuario1.Read())
                                {
                                    if (Math.Round(double.Parse(oFila["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario1["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 1;
                                }
                                drUsuario1.Close();
                                drUsuario1.Dispose();
                                break;
                            case "2":
                                SqlDataReader drUsuario2 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                if (drUsuario2.Read())
                                {
                                    if (Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario2["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 2;
                                }
                                drUsuario2.Close();
                                drUsuario2.Dispose();
                                break;
                            case "3":
                                SqlDataReader drUsuario3 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                if (drUsuario3.Read())
                                {
                                    if (Math.Round(double.Parse(oFila["horas_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario3["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 3;
                                }
                                drUsuario3.Close();
                                drUsuario3.Dispose();
                                break;
                            case "4":
                                SqlDataReader drUsuario4 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                if (drUsuario4.Read())
                                {
                                    if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario4["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 4;
                                }
                                drUsuario4.Close();
                                drUsuario4.Dispose();
                                break;
                            case "5":
                                SqlDataReader drUsuario5 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                                if (drUsuario5.Read())
                                {
                                    if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) > Math.Round(double.Parse(drUsuario5["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 5;
                                }
                                drUsuario5.Close();
                                drUsuario5.Dispose();
                                break;
                        }
                        */
                        #endregion
                    }
                    else
                    {
                        if (dAlta > dInicioMes && dAlta < dFinMes)
                        {
                            bHaTrabajadoTodoElMes = false;
                            #region alta en el mes
                            /*
                            switch (sIncompletos)
                            {
                                case "1":
                                    SqlDataReader drUsuario1 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    //nDias = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    if (drUsuario1.Read())
                                    {
                                        if (dDLR < Math.Round(double.Parse(drUsuario1["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 1;
                                    }
                                    drUsuario1.Close();
                                    drUsuario1.Dispose();
                                    break;
                                case "2":
                                    SqlDataReader drUsuario2 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    if (drUsuario2.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario2["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 2;
                                    }
                                    drUsuario2.Close();
                                    drUsuario2.Dispose();
                                    break;
                                case "3":
                                    SqlDataReader drUsuario3 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    if (drUsuario3.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["horas_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario3["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 3;
                                    }
                                    drUsuario3.Close();
                                    drUsuario3.Dispose();
                                    break;
                                case "4":
                                    SqlDataReader drUsuario4 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    if (drUsuario4.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario4["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 4;
                                    }
                                    drUsuario4.Close();
                                    drUsuario4.Dispose();
                                    break;
                                case "5":
                                    SqlDataReader drUsuario5 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dFinMes);
                                    if (drUsuario5.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) > Math.Round(double.Parse(drUsuario5["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 5;
                                    }
                                    drUsuario5.Close();
                                    drUsuario5.Dispose();
                                    break;
                                   
                           
                            }
                            */
                            #endregion
                            dBaja = dFinMes;
                        }
                        else if (dBaja != null && dBaja >= dInicioMes && dBaja <= dFinMes)
                        {
                            bHaTrabajadoTodoElMes = false;
                            #region baja en el mes
                            /*
                            switch (sIncompletos)
                            {
                                case "1":
                                    SqlDataReader drUsuario1 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    //nDias = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    if (drUsuario1.Read())
                                    {
                                        if (dDLR < Math.Round(double.Parse(drUsuario1["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 1;
                                    }
                                    drUsuario1.Close();
                                    drUsuario1.Dispose();
                                    break;
                                case "2":
                                    SqlDataReader drUsuario2 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    if (drUsuario2.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario2["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 2;
                                    }
                                    drUsuario2.Close();
                                    drUsuario2.Dispose();
                                    break;
                                case "3":
                                    SqlDataReader drUsuario3 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    if (drUsuario3.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["horas_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario3["HLC"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 3;
                                    }
                                    drUsuario3.Close();
                                    drUsuario3.Dispose();
                                    break;
                                case "4":
                                    SqlDataReader drUsuario4 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    if (drUsuario4.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(drUsuario4["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 4;
                                    }
                                    drUsuario4.Close();
                                    drUsuario4.Dispose();
                                    break;
                                case "5":
                                    SqlDataReader drUsuario5 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dInicioMes, dBaja);
                                    if (drUsuario5.Read())
                                    {
                                        if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) > Math.Round(double.Parse(drUsuario5["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                            nIncompleto = 5;
                                    }
                                    drUsuario5.Close();
                                    drUsuario5.Dispose();
                                    break;
                            }
                            */
                            #endregion
                            dAlta = dInicioMes;
                        }
                        else
                        {
                            #region de alta todo el mes
                            /*
                            switch (sIncompletos)
                            {
                                case "1":
                                    //nDias = int.Parse(oFila["JReportadas"].ToString());
                                    if (dDLR < Math.Round(double.Parse(oFila["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 1;
                                    break;
                                case "2":
                                    if (Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(oFila["HLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 2;
                                    break;
                                case "3":
                                    if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(oFila["HLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 3;
                                    break;
                                case "4":
                                    if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) < Math.Round(double.Parse(oFila["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 4;
                                    break;
                                case "5":

                                    if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) > Math.Round(double.Parse(oFila["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero))
                                        nIncompleto = 5;
                                    break;
                            }
                            */
                            #endregion
                            dAlta = dInicioMes;
                            dBaja = dFinMes;
                        }
                    }
                    if (!bHaTrabajadoTodoElMes)
                    {
                        SqlDataReader drUsuario1 = Consumo.ObtenerJornadasReportadasUsuario((int)oFila["t314_idusuario"], dAlta, dBaja);
                        if (drUsuario1.Read())
                        {
                            dDLC = Math.Round(double.Parse(drUsuario1["JLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero);
                            dHLC = Math.Round(double.Parse(drUsuario1["HLC"].ToString()), 2, MidpointRounding.AwayFromZero);
                        }
                        drUsuario1.Close();
                        drUsuario1.Dispose();
                    }
                    #endregion
                    switch (sIncompletos)
                    {
                        case "1"://dias reportados < dias laborables del calendario
                            if (dDLR < dDLC) nIncompleto = 1;
                            break;
                        case "2"://horas reportadas < horas laborables del calendario
                            if (dHR < dHLC)
                                    nIncompleto = 2;
                            break;
                        case "3"://unidades económicas (horas + extraerHoras[dias]) < horas laborables del calendario
                            if (Math.Round(double.Parse(oFila["unidades_cmp"].ToString()), 2, MidpointRounding.AwayFromZero) != dHR)
                            {//09/11/2015 Xabi dice que podemos aplicar un margen de diferencia de 0,5
                                double dDiff = double.Parse(oFila["unidades_cmp"].ToString()) - dHR;
                                if (Math.Abs(dDiff) >= 0.5)
                                    nIncompleto = 3;
                            }
                            break;
                    }
                    

                    //Si el usuario ha pedido solo los incompletos, solo saco esas líneas
                    if (sIncompletos == "0"
                            || (sIncompletos == "1" && nIncompleto == 1)
                            || (sIncompletos == "2" && nIncompleto == 2)
                            || (sIncompletos == "3" && nIncompleto == 3)
                        )
                    {
                        #region Poner datos
                        //sb.Append("<tr onmouseover='TTip(this);' incom='" + nIncompleto + "'>");
                        //Nº usuario
                        //sb.Append("<td>" + int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()).ToString("#,###") + "</td>");
                        sb.Append("<tr R=" + ((int)oFila["t314_idusuario"]).ToString("#,###") + " style='height:20px;'");
                        if (oFila["t001_tiporecurso"].ToString() == "F")
                            sb.Append(" tipo ='F'");
                        else
                        {
                            if (oFila["t303_denominacion"].ToString() == "")
                                sb.Append(" tipo ='E'");
                            else
                                sb.Append(" tipo ='P'");
                        }
                        sb.Append(" nodo =\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\"");
                        sb.Append(" sexo ='" + oFila["t001_sexo"].ToString() + "'");
                        sb.Append(" baja ='" + oFila["baja"].ToString() + "'>");
                        //sb.Append("<td><IMG src='../../../../images/imgUsu" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + ".gif' class='ICO' style='margin-right:2px;'></td>");
                        sb.Append("<td style=\"border-right:''\"></td>");
                        sb.Append("<td><span class='NBR W330' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)oFila["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Visión por:</label>" + oFila["desmotivo"].ToString() + "] hideselects=[off]\" >" + oFila["tecnico"].ToString() + "</span></td>");

                        //Profesional
                        //sb.Append("<td title='" + ds.Tables[0].Rows[i].ItemArray[11].ToString() + "'><nobr class='NBR' style='width:360px;'>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</nobr></td>");
                        //UDR
                        if (oFila["UDR"] != DBNull.Value) sb.Append("<td style='text-align:center;'>" + DateTime.Parse(oFila["UDR"].ToString()).ToShortDateString() + "</td>");
                        else sb.Append("<td></td>");
                        //Calendario
                        sb.Append("<td><span class='NBR' style='width:180px;'>" + oFila["Calendario"].ToString() + "</span></td>");
                        //Días laborables Calendario
                        sb.Append("<td style='text-align:right;'>" + oFila["JLCalendario"].ToString() + "</td>");

                        //Dias laborables reportados
                        //oFila["Jornadas"] -> esfuerzo reportado en jornadas
                        //oFila["JReportadas"] -> nº de días naturales reportados
                        sb.Append("<td");
                        if (nIncompleto == 1)
                            sb.Append(" style='color:red;text-align:right;'");//DLR < DLC
                        else if (Math.Round(double.Parse(oFila["Jornadas"].ToString()), 2, MidpointRounding.AwayFromZero) < dDLR)
                            sb.Append(" style='color:blue;text-align:right;'");//Esfuerzo en jornadas < Días naturales reportados
                        else
                            sb.Append(" style='text-align:right;'");
                        sb.Append(" >" + oFila["JReportadas"].ToString() + "</td>");

                        //Horas laborables Calendario
                        sb.Append("<td style='text-align:right;'>" + Math.Round(double.Parse(oFila["HLCalendario"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("N") + "</td>");
                        //Horas reportadas
                        sb.Append("<td style='text-align:right;'>" + Math.Round(double.Parse(oFila["Horas"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("N") + "</td>");
                        //Jornadas reportadas
                        sb.Append("<td style='text-align:right;'>" + Math.Round(double.Parse(oFila["Jornadas"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("N") + "</td>");
                        //Horas económicas
                        sb.Append("<td style='text-align:right;'>" + Math.Round(double.Parse(oFila["horas_cmp"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("N") + "</td>");
                        //Jornadas económicas
                        sb.Append("<td style=\"border-right:'';text-align:right;\">" + Math.Round(double.Parse(oFila["jornadas_cmp"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("N") + "</td>");

                        sb.Append("</tr>");
                        #endregion
                    }
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
    private string setPreferencia(string sIncompletos, string sActuAuto, string sNodo, string sExternos, string sOtrosNodos, string sSoloProyAbiertos, string sForaneos, string sGF)
    {
        string sResul = "";
        string strNodo = null, strActuAuto = null, strIncompletos=null, strGF=null;

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
            if (sGF != "") strGF = sGF;
            if (sIncompletos != "") strIncompletos = sIncompletos;
            if (sActuAuto != "") strActuAuto = sActuAuto;

            int nPref = PREFERENCIAUSUARIO.Insertar(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 9, strNodo, strIncompletos, sExternos, sOtrosNodos,
                                                    strActuAuto, sSoloProyAbiertos, sForaneos, sGF, 
                                                    null, null, null, null, null, null, null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 9);
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
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 9);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["idNodo"].ToString() + "@#@"); //0
                sb.Append(dr["t303_denominacion"].ToString() + "@#@"); //1
                sb.Append(dr["Incompletos"].ToString() + "@#@"); //2
                sb.Append(dr["Externos"].ToString() + "@#@"); //3
                sb.Append(dr["otrosNodos"].ToString() + "@#@"); //4  
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //5
                sb.Append(dr["sSoloProyAbiertos"].ToString() + "@#@"); //6
                sb.Append(dr["Foraneos"].ToString() + "@#@"); //7
                sb.Append(dr["idGF"].ToString() + "@#@"); //8
                sb.Append(dr["t342_desgrupro"].ToString() + "@#@"); //9
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
