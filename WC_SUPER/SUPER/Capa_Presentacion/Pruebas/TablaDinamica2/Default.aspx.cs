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

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strDatos = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //Master.nBotonera = 58;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Tabla Dinamica Servidor";

                if (Session["DS_TABLADINAMICA"] != null)
                {
                    Session["DS_TABLADINAMICA"] = null;
                }
                //strDatos = PROYECTOSUBNODO.PruebaDatosTablaDinamica();
                //DataSet ds = PROYECTOSUBNODO.PruebaDatosTablaDinamicaServidor();


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
                sResultado += obtener(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
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

    private string obtener(string sEvolucionMensual, string sAgrupacion, string sVisualizacion, string sDato)
    {
        try
        {
            if (Session["DS_TABLADINAMICA"] == null)
            {
                Session["DS_TABLADINAMICA"] = PROYECTOSUBNODO.PruebaDatosTablaDinamicaServidor();


            }
            bool bEvolucionMensual = (sEvolucionMensual == "1") ? true : false;
            DataSet ds = (DataSet)Session["DS_TABLADINAMICA"];

            #region Actualización de tablas auxiliares
            ds.Tables["Agrupaciones"].Rows.Clear();
            string[] aAgrupacion = Regex.Split(sAgrupacion, "{sep}");
            foreach (string oAg in aAgrupacion)
            {
                if (oAg == "") continue;
                DataRow dr = ds.Tables["Agrupaciones"].NewRow();
                dr[0] = oAg;
                ds.Tables["Agrupaciones"].Rows.Add(dr);
            }

            /* Creo la tabla de Visualizaciones */
            ds.Tables["Visualizaciones"].Rows.Clear();
            string[] aVisualizacion = Regex.Split(sVisualizacion, "{sep}");
            foreach (string oVi in aVisualizacion)
            {
                if (oVi == "") continue;
                DataRow dr = ds.Tables["Visualizaciones"].NewRow();
                dr[0] = oVi;
                ds.Tables["Visualizaciones"].Rows.Add(dr);
            }

            /* Creo la tabla de Datos */
            ds.Tables["Datos"].Rows.Clear();
            string[] aDato = Regex.Split(sDato, "{sep}");
            foreach (string oDa in aDato)
            {
                if (oDa == "") continue;
                DataRow dr = ds.Tables["Datos"].NewRow();
                dr[0] = oDa;
                ds.Tables["Datos"].Rows.Add(dr);
            }

            #endregion

            #region Creación de tabla de resultado

            if (ds.Tables["Resultado"] != null)
            {
                ds.Tables.Remove("Resultado");

            }
            /* Creo la tabla de Resultado */
            DataTable dtResultado = ds.Tables.Add("Resultado");
            dtResultado.Columns.Add("t305_idproyectosubnodo", typeof(int));
            dtResultado.Columns.Add("t325_anomes", typeof(int));
            dtResultado.Columns.Add("t303_idnodo", typeof(int));
            dtResultado.Columns.Add("t303_denominacion", typeof(string));
            dtResultado.Columns.Add("t301_idproyecto", typeof(int));
            dtResultado.Columns.Add("t301_denominacion", typeof(string));
            dtResultado.Columns.Add("t302_idcliente", typeof(int));
            dtResultado.Columns.Add("t302_denominacion", typeof(string));
            dtResultado.Columns.Add("t314_idusuario_responsable", typeof(int));
            dtResultado.Columns.Add("ResponsableProyecto", typeof(string));
            dtResultado.Columns.Add("t305_cualidad", typeof(string));
            dtResultado.Columns.Add("t323_idnaturaleza", typeof(int));
            dtResultado.Columns.Add("t323_denominacion", typeof(string));

            if (!bEvolucionMensual)
            {
                dtResultado.Columns.Add("Ingresos_Netos", typeof(decimal));
                dtResultado.Columns.Add("Margen", typeof(decimal));
                dtResultado.Columns.Add("Obra_en_curso", typeof(decimal));
                dtResultado.Columns.Add("Saldo_de_Clientes", typeof(decimal));
                dtResultado.Columns.Add("Total_Cobros", typeof(decimal));
                dtResultado.Columns.Add("Total_Gastos", typeof(decimal));
                dtResultado.Columns.Add("Total_Ingresos", typeof(decimal));
                dtResultado.Columns.Add("Volumen_de_Negocio", typeof(decimal));
                dtResultado.Columns.Add("Otros_consumos", typeof(decimal));
                dtResultado.Columns.Add("Consumo_recursos", typeof(decimal));
            }
            else
            {
                foreach (DataRow oFilaTotal in ds.Tables["Datos"].Rows)
                {
                    foreach (DataRow oFilaMes in ds.Tables["Meses"].Rows)
                    {
                        dtResultado.Columns.Add(oFilaTotal["Dato"].ToString() + "_" + oFilaMes["Mes"].ToString(), typeof(decimal));
                    }
                }
            }

            ds.Tables["Resultado"].Rows.Clear();



            foreach (DataRow oFilaDato in ds.Tables["Consulta"].Rows){
                int sw = 0;
                foreach (DataRow oFilaResultado in ds.Tables["Resultado"].Rows){
                    int sw_agrupacion = 0;
                    foreach (DataRow oFilaAgrupacion in ds.Tables["Agrupaciones"].Rows){
                        if (oFilaDato[oFilaAgrupacion["Agrupacion"].ToString()].ToString() == oFilaResultado[oFilaAgrupacion["Agrupacion"].ToString()].ToString())
                        {
                            sw_agrupacion++;
                        }
                    }
                    if (sw_agrupacion == ds.Tables["Agrupaciones"].Rows.Count)
                    {
                        foreach (DataRow oFilaTotal in ds.Tables["Datos"].Rows){
                            if (!bEvolucionMensual)
                            {
                                oFilaResultado[oFilaTotal["Dato"].ToString()] = decimal.Parse(oFilaResultado[oFilaTotal["Dato"].ToString()].ToString()) + decimal.Parse(oFilaDato[oFilaTotal["Dato"].ToString()].ToString());
                            } else {
                                foreach (DataRow oFilaMes in ds.Tables["Meses"].Rows){
                                    oFilaResultado[oFilaTotal["Dato"].ToString() + "_" + oFilaMes["Mes"].ToString()] = decimal.Parse(oFilaResultado[oFilaTotal["Dato"].ToString() + "_" + oFilaMes["Mes"].ToString()].ToString()) + (((int)oFilaMes["Mes"] == (int)oFilaDato["t325_anomes"]) ? decimal.Parse(oFilaDato[oFilaTotal["Dato"].ToString()].ToString()) : 0);
                                }
                            }
                        }
                        sw = 1;
                        break;
                    }
                }
                if (sw == 1) continue;

                DataRow dr = ds.Tables["Resultado"].NewRow();
                dr["t305_idproyectosubnodo"] = (int)oFilaDato["t305_idproyectosubnodo"];
                dr["t325_anomes"] = (int)oFilaDato["t325_anomes"];//: oDato.anomes,
                dr["t303_idnodo"] = (int)oFilaDato["t303_idnodo"];//: oDato.idnodo,
                dr["t303_denominacion"] = (string)oFilaDato["t303_denominacion"];//: oDato.desnodo,
                dr["t301_idproyecto"] = (int)oFilaDato["t301_idproyecto"];//: oDato.idproyecto,
                dr["t301_denominacion"] = (string)oFilaDato["t301_denominacion"];//: oDato.desproyecto,
                dr["t302_idcliente"] = (int)oFilaDato["t302_idcliente"];//: oDato.idcliente,
                dr["t302_denominacion"] = (string)oFilaDato["t302_denominacion"];//: oDato.descliente,
                dr["t314_idusuario_responsable"] = (int)oFilaDato["t314_idusuario_responsable"];//: oDato.idresponsableproyecto,
                dr["ResponsableProyecto"] = (string)oFilaDato["ResponsableProyecto"];//: oDato.desresponsableproyecto,
                dr["t305_cualidad"] = (string)oFilaDato["t305_cualidad"];//: oDato.cualidad,
                dr["t323_idnaturaleza"] = (int)oFilaDato["t323_idnaturaleza"];//: oDato.idnaturaleza,
                dr["t323_denominacion"] = (string)oFilaDato["t323_denominacion"];//: oDato.desnaturaleza,
                
                if (!bEvolucionMensual) {
                    dr["Ingresos_Netos"] = (decimal)oFilaDato["Ingresos_Netos"];//: oDato.Ingresos_Netos,
                    dr["Margen"] = (decimal)oFilaDato["Margen"];//: oDato.Margen,
                    dr["Obra_en_curso"] = (decimal)oFilaDato["Obra_en_curso"];//: oDato.Obra_en_curso,
                    dr["Saldo_de_Clientes"] = (decimal)oFilaDato["Saldo_de_Clientes"];//: oDato.Saldo_de_Clientes,
                    dr["Total_Cobros"] = (decimal)oFilaDato["Total_Cobros"];//: oDato.Total_Cobros,
                    dr["Total_Gastos"] = (decimal)oFilaDato["Total_Gastos"];//: oDato.Total_Gastos,
                    dr["Total_Ingresos"] = (decimal)oFilaDato["Total_Ingresos"];//: oDato.Total_Ingresos,
                    dr["Volumen_de_Negocio"] = (decimal)oFilaDato["Volumen_de_Negocio"];//: oDato.Volumen_de_Negocio,
                    dr["Otros_consumos"] = (decimal)oFilaDato["Otros_consumos"];//: oDato.Otros_consumos,
                    dr["Consumo_recursos"] = (decimal)oFilaDato["Consumo_recursos"];//: oDato.Consumo_recursos
                }
                else
                {
                    foreach (DataRow oFilaTotal in ds.Tables["Datos"].Rows)
                    {
                        foreach (DataRow oFilaMes in ds.Tables["Meses"].Rows)
                        {
                            dr[oFilaTotal["Dato"].ToString() + "_" + oFilaMes["Mes"].ToString()] = (decimal)oFilaDato[oFilaTotal["Dato"].ToString()];
                        }
                    }
                }
                ds.Tables["Resultado"].Rows.Add(dr);
            }

            #endregion

            #region Creación de tabla HTML con el resultado

            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' style='width:auto' cellpadding='0' cellspacing='0' border='0'>");
//            var tblDatos = $I("tblDatos");
//            if (tblDatos != null) {
//                var oNF = null;
//                var oNC = null;
                sb.Append("<tr class='TBLINI' style='text-align:center; height:20px; border:solid 1px #FFFFF; border-collapse:collapse; border-spacing:0px;background-repeat: repeat-x;'>");
                foreach (DataRow oFilaVis in ds.Tables["Visualizaciones"].Rows){
                    sb.Append("<td>" + oFilaVis["Visualizacion"].ToString() + "</td>");
                }
                foreach (DataRow oFilaTotal in ds.Tables["Datos"].Rows)
                {
                    if (!bEvolucionMensual)
                    {
                        sb.Append("<td>" + oFilaTotal["Dato"].ToString() + "</td>");
                    }
                    else
                    {
                        foreach (DataRow oMes in ds.Tables["Meses"].Rows)
                        {
                            sb.Append("<td>" + oFilaTotal["Dato"].ToString() + "_" + oMes["Mes"].ToString() + "</td>");
                        }
                    }
                }
                sb.Append("</tr>");
                foreach (DataRow oFilaResultado in ds.Tables["Resultado"].Rows){
                //for (var i = 0; i < this.tabla_resultado.length; i++) {
                    //oNF = tblDatos.insertRow(-1);
                    sb.Append("<tr>");

                    foreach (DataRow oFilaVis in ds.Tables["Visualizaciones"].Rows)
                    {
                        sb.Append("<td>" + oFilaResultado[oFilaVis["Visualizacion"].ToString()].ToString() + "</td>");
                    }

                    foreach (DataRow oFilaTotal in ds.Tables["Datos"].Rows)
                    {
                        if (!bEvolucionMensual)
                        {
                            sb.Append("<td style='text-align:right;'>" + decimal.Parse(oFilaResultado[oFilaTotal["Dato"].ToString()].ToString()).ToString("N") + "</td>");
                        }
                        else
                        {
                            foreach (DataRow oMes in ds.Tables["Meses"].Rows)
                            {
                                sb.Append("<td>" + decimal.Parse(oFilaResultado[oFilaTotal["Dato"].ToString() + "_" + oMes["Mes"].ToString()].ToString()).ToString("N") + "</td>");
                            }
                        }
                    }
                }
               // $I("divCatalogo").children[0].style.width = tblDatos.scrollWidth + "px";
//            }
            //$I("divCatalogo").scrollTop = 0;
            //$I("divCatalogo").scrollLeft = 0;

            sb.Append("</table>");

            #endregion

            ds.Dispose();
            return "OK@#@" + sb.ToString();// + PROYECTOSUBNODO.PruebaDatosTablaDinamica2();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }

}
