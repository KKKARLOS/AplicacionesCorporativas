using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strDatos = "";
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bEstilosLocales = true;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Tabla Dinamica Servidor V4";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/TablaDinamica4/Functions/ColumnDrag.js");
                //Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/TablaDinamica4/Functions/resizable-tables.js");
                //Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/TablaDinamica4/Functions/colResizable-1.3.min.js");

                //Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/TablaDinamica4/css/main.css");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/TablaDinamica4/Functions/jquery.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/TablaDinamica4/Functions/colResizable-1.3.source.js");
                
                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                txtDesde.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnDesde.Text));
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtHasta.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnHasta.Text));

                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

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
        sResultado = aArgs[0] + @"@#@";
        switch (aArgs[0])
        {
            case ("obtener"):
                sResultado += obtener(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13]);
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

    private string obtener(string nAccederBDatos, string sEvolucionMensual, string sDesde, string sHasta, string sDimensiones, string sMagnitudes,
            string sNodo,
            string sProyecto,
            string sCliente,
            string sResponsable,
            string sCualidad,
            string sNaturaleza,
            string sTablasAuxiliares)
    {
        try
        {
            bool bNodo = false;
            bool bProyecto = false;
            bool bCliente = false;
            bool bResponsable = false;
            bool bCualidad = false;
            bool bNaturaleza = false;

            string sFormulas_aux = "", sOrdenDimensiones = "";

            int nTiempoBD = 0;
            int nTiempoHTML = 0;

            #region Actualización de tablas auxiliares
            string[] aDimensiones = Regex.Split(sDimensiones, "{sep}");
            foreach (string oDim in aDimensiones)
            {
                switch (oDim)
                {
                    case "nodo": bNodo = true; sOrdenDimensiones += "2,"; break;
                    case "proyecto": bProyecto = true; sOrdenDimensiones += "4,"; break;
                    case "cliente": bCliente = true; sOrdenDimensiones += "6,"; break;
                    case "responsable": bResponsable = true; sOrdenDimensiones += "8,"; break;
                    case "cualidad": bCualidad = true; sOrdenDimensiones += "9,"; break;
                    case "naturaleza": bNaturaleza = true; sOrdenDimensiones += "11,"; break;
                }
            }
            sOrdenDimensiones = sOrdenDimensiones.Substring(0, sOrdenDimensiones.Length - 1);

            string[] aMagnitud = Regex.Split(sMagnitudes, "{sep}");
            foreach (string oDa in aMagnitud)
            {
                switch (oDa)
                {
                    case "Ingresos_Netos": sFormulas_aux += "1,"; break;
                    case "Margen": sFormulas_aux += "2,"; break;
                    case "Obra_en_curso": sFormulas_aux += "3,"; break;
                    case "Saldo_de_Clientes": sFormulas_aux += "4,"; break;
                    case "Total_Cobros": sFormulas_aux += "5,"; break;
                    case "Total_Gastos": sFormulas_aux += "6,"; break;
                    case "Total_Ingresos": sFormulas_aux += "7,"; break;
                    case "Volumen_de_Negocio": sFormulas_aux += "8,"; break;
                    case "Otros_consumos": sFormulas_aux += "9,"; break;
                }
            }

            #endregion

            StringBuilder sb = new StringBuilder();
            StringBuilder sbAux = new StringBuilder();
            DataSet ds=null;
            DateTime? oDT1 = null, oDT2 = null, oDT3 = null;
            int nMeses = 0;
            bool sw_class = false;

            #region Creación del DataSet
            if (int.Parse(nAccederBDatos) == 1 || Session["DS_CUADROMANDO"] == null)
            {
                if (sEvolucionMensual == "1")
                {
                    oDT1 = DateTime.Now;
                    nMeses = Fechas.DateDiff("month", Fechas.AnnomesAFecha(int.Parse(sDesde)), Fechas.AnnomesAFecha(int.Parse(sHasta))) + 1;
                    ds = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamicaV5_EM(null, (int)Session["UsuarioActual"], int.Parse(sDesde), int.Parse(sHasta), Session["MONEDA_VDC"].ToString(),
                                bNodo, bProyecto, bCliente, bResponsable, bCualidad, bNaturaleza,
                                sNodo, sProyecto, sCliente, sResponsable, sCualidad, sNaturaleza, sFormulas_aux,
                                (sTablasAuxiliares == "1") ? true : false, sOrdenDimensiones);
                    oDT2 = DateTime.Now;
                }
                else
                {
                    oDT1 = DateTime.Now;
                    ds = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamicaV5(null, (int)Session["UsuarioActual"], int.Parse(sDesde), int.Parse(sHasta), Session["MONEDA_VDC"].ToString(),
                                bNodo, bProyecto, bCliente, bResponsable, bCualidad, bNaturaleza,
                                sNodo, sProyecto, sCliente, sResponsable, sCualidad, sNaturaleza,
                                (sTablasAuxiliares == "1") ? true : false, sOrdenDimensiones);
                    oDT2 = DateTime.Now;
                }

                Session["DS_CUADROMANDO"] = ds;
            }
            else
            {
                oDT1 = DateTime.Now;
                ds = (DataSet)Session["DS_CUADROMANDO"];
                oDT2 = DateTime.Now;
            }
            #endregion

            if (sEvolucionMensual == "1")
            {
                #region Con Evolución Mensual

                sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<tr id='rowTituloDatos'>");

                foreach (string oCol in aDimensiones)
                {
                    switch (oCol)
                    {
                        //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda para que se puedan
                        //arrastrar las columnas.
                        case "nodo": sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                        case "proyecto": sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                        case "cliente": sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Cliente</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                        case "responsable": sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Responsable</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                        case "cualidad": sb.Append("<th dimension='cualidad' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Cualidad</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cualidad') /></th>"); break;
                        case "naturaleza": sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                    }
                }

                sb.Append("<th class='Dimension'>Indicadores</th>");
                for (int i = 14; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (i == 14){
                        string[] aRango = Regex.Split(ds.Tables[0].Columns[i].ColumnName, "-");
                        sb.Append("<th>" + Fechas.AnnomesAFechaDescCorta(int.Parse(aRango[0])) + " - " + Fechas.AnnomesAFechaDescCorta(int.Parse(aRango[1])) + "</th>");   
                    }
                    else sb.Append("<th>" + Fechas.AnnomesAFechaDescCorta(int.Parse(ds.Tables[0].Columns[i].ColumnName)) + "</th>");
                }
                sb.Append("</tr>");
                sb.Append("</table>");
                #endregion
                sb.Append("{sep}");
                #region HTML Filas
                sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                {
                    sb.Append("<tr ");

                    if (bNodo)
                    {
                        sb.Append("idnodo='" + oFila["t303_idnodo"].ToString() + "' ");
                        sb.Append("desnodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                    }
                    if (bProyecto)
                    {
                        sb.Append("idproyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                        sb.Append("desproyecto=\"" + Utilidades.escape(oFila["t301_denominacion"].ToString()) + "\" ");
                    }
                    if (bCliente)
                    {
                        sb.Append("idcliente='" + oFila["t302_idcliente"].ToString() + "' ");
                        sb.Append("descliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");
                    }
                    if (bResponsable)
                    {
                        sb.Append("idresponsable='" + oFila["t314_idusuario_responsable"].ToString() + "' ");
                        sb.Append("desresponsable=\"" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "\" ");
                    }
                    if (bCualidad)
                    {
                        sb.Append("cualidad='" + oFila["t305_cualidad"].ToString() + "' ");
                        switch (oFila["t305_cualidad"].ToString())
                        {
                            case "C": sb.Append("descualidad=\"Contratante\" "); break;
                            case "P": sb.Append("descualidad=\"Replicada con gestión\" "); break;
                            case "J": sb.Append("descualidad=\"Replicada sin gestión\" "); break;
                        }
                    }
                    if (bNaturaleza)
                    {
                        sb.Append("idnaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                        sb.Append("desnaturaleza=\"" + Utilidades.escape(oFila["t323_denominacion"].ToString()) + "\" ");
                    }

                    sb.Append(">");

                    foreach (string oDato in aDimensiones)
                    {
                        switch (oDato)
                        {
                            case "nodo": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>"); break;
                            case "proyecto": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "</td>"); break;
                            case "cliente": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t302_denominacion"].ToString() + "</td>"); break;
                            case "responsable": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["ResponsableProyecto"].ToString() + "</td>"); break;
                            case "cualidad": 
                                switch (oFila["t305_cualidad"].ToString())
                                {
                                    case "C": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Contratante</td>"); break;
                                    case "P": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Replicada con gestión</td>"); break;
                                    case "J": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Replicada sin gestión</td>"); break;
                                }
                                break;
                            case "naturaleza": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t323_denominacion"].ToString() + "</td>"); break;
                        }
                    }



                    for (int i = 12; i < ds.Tables[0].Columns.Count; i++)
                    {
                        if (i == 13) continue; //numrow
                        if (i == 12) sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila[ds.Tables[0].Columns[i].ColumnName].ToString() + "</td>");
                        else
                        {
                            sb.Append("<td magnitud='" + oFila[ds.Tables[0].Columns[12].ColumnName].ToString() + "' formula='" + oFila[ds.Tables[0].Columns[11].ColumnName].ToString() + "' mes='" + ds.Tables[0].Columns[i].ColumnName + "' ondblclick='gp(this);' class='MA " + ((i == 14) ? "MagPeriodo" : "Mag") + "'>" + ((decimal.Parse(oFila[ds.Tables[0].Columns[i].ColumnName].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila[ds.Tables[0].Columns[i].ColumnName].ToString()).ToString("N")) + "</td>");
                        }
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                #endregion
            }
            else
            {
                #region Sin Evolución Mensual
                sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<tr id='rowTituloDatos'>");

                foreach (string oCol in aDimensiones)
                {
                    switch (oCol)
                    {
                        //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                        //para que se puedan arrastrar las columnas.
                        case "nodo": sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                        case "proyecto": sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                        case "cliente": sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Cliente</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                        case "responsable": sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Responsable</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                        case "cualidad": sb.Append("<th dimension='cualidad' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Cualidad</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cualidad') /></th>"); break;
                        case "naturaleza": sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                    }
                }

                foreach (string oCol in aMagnitud)
                {
                    switch (oCol)
                    {
                        case "Ingresos_Netos": sb.Append("<th class='MagTit' title='Ingresos netos'>Ing. netos</th>"); break;
                        case "Margen": sb.Append("<th class='MagTit' title='Margen de contribución'>Margen</th>"); break;
                        case "Obra_en_curso": sb.Append("<th class='MagTit' title='Obra en curso'>Obra en curso</th>"); break;
                        case "Saldo_de_Clientes": sb.Append("<th class='MagTit' title='Saldo de clientes'>Saldo cli.</th>"); break;
                        case "Total_Cobros": sb.Append("<th class='MagTit' title='Total cobros'>T. cobros</th>"); break;
                        case "Total_Gastos": sb.Append("<th class='MagTit' title='Total gastos'>T. gastos</th>"); break;
                        case "Total_Ingresos": sb.Append("<th class='MagTit' title='Total ingresos'>T. ingresos</th>"); break;
                        case "Volumen_de_Negocio": sb.Append("<th class='MagTit' title='Volumen de negocio'>Vol. negocio</th>"); break;
                        case "Otros_consumos": sb.Append("<th class='MagTit' title='Otros consumos'>Otros consumos</th>"); break;
                    }
                }
                sb.Append("</tr>");
                sb.Append("</table>");
                #endregion
                sb.Append("{sep}");
                #region HTML Filas
                sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                {
                    sb.Append("<tr ");

                    if (bNodo)
                    {
                        sb.Append("idnodo='" + oFila["t303_idnodo"].ToString() + "' ");
                        //sb.Append("desnodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                    }
                    if (bProyecto)
                    {
                        sb.Append("idproyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                        //sb.Append("desproyecto=\"" + Utilidades.escape(oFila["t301_denominacion"].ToString()) + "\" ");
                    }
                    if (bCliente)
                    {
                        sb.Append("idcliente='" + oFila["t302_idcliente"].ToString() + "' ");
                        //sb.Append("descliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");
                    }
                    if (bResponsable)
                    {
                        sb.Append("idresponsable='" + oFila["t314_idusuario_responsable"].ToString() + "' ");
                        //sb.Append("desresponsable=\"" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "\" ");
                    }
                    if (bCualidad)
                    {
                        sb.Append("idcualidad='" + oFila["t305_cualidad"].ToString() + "' ");
                        //switch (oFila["t305_cualidad"].ToString())
                        //{
                        //    case "C": sb.Append("descualidad=\"Contratante\" "); break;
                        //    case "P": sb.Append("descualidad=\"Replicada con gestión\" "); break;
                        //    case "J": sb.Append("descualidad=\"Replicada sin gestión\" "); break;
                        //}
                    }
                    if (bNaturaleza)
                    {
                        sb.Append("idnaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                        //sb.Append("desnaturaleza=\"" + Utilidades.escape(oFila["t323_denominacion"].ToString()) + "\" ");
                    }

                    sb.Append(">");

                    foreach (string oDato in aDimensiones)
                    {
                        switch (oDato)
                        {
                            case "nodo": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>"); break;
                            case "proyecto": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "</td>"); break;
                            case "cliente": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t302_denominacion"].ToString() + "</td>"); break;
                            case "responsable": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["ResponsableProyecto"].ToString() + "</td>"); break;
                            case "cualidad":
                                switch (oFila["t305_cualidad"].ToString())
                                {
                                    case "C": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Contratante</td>"); break;
                                    case "P": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Replicada con gestión</td>"); break;
                                    case "J": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">Replicada sin gestión</td>"); break;
                                }
                                break;
                            case "naturaleza": sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t323_denominacion"].ToString() + "</td>"); break;
                        }
                    }

                    foreach (string oCol in aMagnitud)
                    {
                        switch (oCol)
                        {
                            case "Ingresos_Netos": sb.Append("<td magnitud='Ingresos_Netos' formula='1' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Ingresos_Netos"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Ingresos_Netos"].ToString()).ToString("N")) + "</td>"); break;
                            case "Margen": sb.Append("<td magnitud='Margen' formula='2' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Margen"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Margen"].ToString()).ToString("N")) + "</td>"); break;
                            case "Obra_en_curso": sb.Append("<td magnitud='Obra_en_curso' formula='3' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Obra_en_curso"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Obra_en_curso"].ToString()).ToString("N")) + "</td>"); break;
                            case "Saldo_de_Clientes": sb.Append("<td magnitud='Saldo_de_Clientes' formula='4' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Saldo_de_Clientes"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Saldo_de_Clientes"].ToString()).ToString("N")) + "</td>"); break;
                            case "Total_Cobros": sb.Append("<td magnitud='Total_Cobros' formula='5' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Total_Cobros"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Total_Cobros"].ToString()).ToString("N")) + "</td>"); break;
                            case "Total_Gastos": sb.Append("<td magnitud='Total_Gastos' formula='6' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Total_Gastos"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Total_Gastos"].ToString()).ToString("N")) + "</td>"); break;
                            case "Total_Ingresos": sb.Append("<td magnitud='Total_Ingresos' formula='7' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Total_Ingresos"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Total_Ingresos"].ToString()).ToString("N")) + "</td>"); break;
                            case "Volumen_de_Negocio": sb.Append("<td magnitud='Volumen_de_Negocio' formula='8' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Volumen_de_Negocio"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Volumen_de_Negocio"].ToString()).ToString("N")) + "</td>"); break;
                            case "Otros_consumos": sb.Append("<td magnitud='Otros_consumos' formula='9' ondblclick='gp(this);' class='MA Mag'>" + ((decimal.Parse(oFila["Otros_consumos"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFila["Otros_consumos"].ToString()).ToString("N")) + "</td>"); break;
                        }
                    }
                    sb.Append("</tr>");
                    sw_class = true;
                }
                sb.Append("</table>");
                #endregion
            }

            #region Arrays javascript
            if (sTablasAuxiliares == "1")
            {
                int i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[1].Rows) //Nodos
                {
                    sbAux.Append("js_Nodo[" + i.ToString() + "] = { \"c\":" + oFila["t303_idnodo"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[2].Rows) //Proyecto
                {
                    sbAux.Append("js_Proyecto[" + i.ToString() + "] = { \"c\":" + oFila["t301_idproyecto"].ToString() + ", \"d\":\"" + Utilidades.escape(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[3].Rows) //Cliente
                {
                    sbAux.Append("js_Cliente[" + i.ToString() + "] = { \"c\":" + oFila["t302_idcliente"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[4].Rows) //Responsable proyecto
                {
                    sbAux.Append("js_Responsable[" + i.ToString() + "] = { \"c\":" + oFila["t314_idusuario_responsable"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[5].Rows) //Cualidad
                {
                    string sCualidadAux = "";
                    switch (oFila["t305_cualidad"].ToString())
                    {
                        case "C": sCualidadAux = "Contratante"; break;
                        case "P": sCualidadAux = "Replicada con gestión"; break;
                        case "J": sCualidadAux = "Replicada sin gestión"; break;
                    }

                    sbAux.Append("js_Cualidad[" + i.ToString() + "] = { \"c\":\"" + oFila["t305_cualidad"].ToString() + "\", \"d\":\"" + Utilidades.escape(sCualidadAux) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[6].Rows) //Naturaleza
                {
                    sbAux.Append("js_Naturaleza[" + i.ToString() + "] = { \"c\":" + oFila["t323_idnaturaleza"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t323_denominacion"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
            }
            #endregion

            ds.Dispose();


            oDT3 = DateTime.Now;
            //nTiempoBD = Fechas.DateDiff("mm", (DateTime)oDT1, (DateTime)oDT2);
            //nTiempoHTML = Fechas.DateDiff("mm", (DateTime)oDT2, (DateTime)oDT3);
            nTiempoBD = (int)((TimeSpan)(oDT2 - oDT1)).TotalMilliseconds;
            nTiempoHTML = (int)((TimeSpan)(oDT3 - oDT2)).TotalMilliseconds;

            

            return "OK@#@" + sb.ToString() + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoHTML.ToString()
                            + sbAux.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }

}
