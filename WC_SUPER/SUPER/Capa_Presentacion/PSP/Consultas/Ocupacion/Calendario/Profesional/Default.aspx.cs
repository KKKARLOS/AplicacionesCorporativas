using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EO.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_Consultas_Ocupacion_Calendario_Profesional_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    public int i = 0;

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

                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        txtCodTecnico.Text = Request.QueryString["id"].ToString();
                    }
                    if (Request.QueryString["nombre"] != null)
                    {
                        txtNombreTecnico.Text = Request.QueryString["nombre"].ToString();
                    }
                    //hdnCR.Text = Session["CRActual"].ToString();

                    string strTabla = obtenerDatosTe(txtCodTecnico.Text);
                    string[] aTabla = Regex.Split(strTabla, "@#@");
                    if (aTabla[0] == "OK")
                    {
                        //this.divTablaTitulo.InnerHtml = aTabla[1];
                        this.divCatalogo.InnerHtml = aTabla[1];
                    }
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("tecnico"):
                //sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3]);
                sResultado += obtenerDatosTe(aArgs[1]);//ini, fin, idRecurso
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
    private string obtenerDatosTe(string sNumEmpleado)
    {
        /* Obtiene la relación de tareas activas con su esfuerzo previsto, consumido y pendiente
         */
        string sResul = "", sCodTarea = "", sDesTarea="";
        StringBuilder sb = new StringBuilder();
        double nPlanificado = 0, nConsumido = 0, nPendiente = 0;
        int idFila = 0;
        try
        {
            #region Leer datos y Establezco relaciones entre las tablas para poder recorrerlas como maestro-detalle
            //Creo un dataset con tres consultas. 
            //Una con empleados-tareas y total de horas estimadas
            //otra con empleados-tareas y total de horas consumidas
            //y otra con empleados y horas pendientes
            DataSet ds = null;
            ds = Recurso.OcupacionProfesionalTarea(int.Parse(sNumEmpleado));
            //Establezco relaciones entre las tablas para poder recorrerlas como maestro-detalle
            //Entre Empleados-Tareas-Previstos y Empleado-Tareas-Consumidos
            DataColumn[] oColumnPadre, oColumnHijo;
            oColumnPadre = new DataColumn[] { ds.Tables[0].Columns["t314_idusuario"], ds.Tables[0].Columns["t332_idtarea"] };
            oColumnHijo = new DataColumn[] { ds.Tables[1].Columns["t314_idusuario"], ds.Tables[1].Columns["t332_idtarea"] };
            DataRelation drRelacion = new DataRelation("rel1", oColumnPadre, oColumnHijo);
            ds.Relations.Add(drRelacion);
            #endregion
            #region establezco el consumo para cada empleado/tarea 
            idFila = 0;
            DataRow[] oConsumos;
            foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados y tareas
            {
                oConsumos = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de consumos del empleado/tarea
                if (oConsumos.Length == 0) nConsumido = 0;
                else nConsumido = double.Parse(oConsumos[0]["CONSUMIDO"].ToString());
                //Actualizo datos en la tabla principal
                ds.Tables[0].Rows[idFila]["CONSUMIDO"] = nConsumido;
                idFila++;
            }
            #endregion
            #region Una vez construido el dataset lo recorro y construyo el HTML de la tabla
            idFila = 0;
            sb.Append("<div style='background-image:url(../../../../../../Images/imgFT16.gif); width:730px'>");
            sb.Append("<table id='tblDatos' class='texto' style='width: 730px;'>");

            sb.Append("<colgroup><col style='width:70px;' /><col width='400px' /><col style='width:60px;' /><col style='width:60px;' /><col style='width:50px;' /></colgroup>");
            sb.Append("<tbody>");

            foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados/tareas
            {
                sCodTarea = oEmpleado["t332_idtarea"].ToString();
                sDesTarea = oEmpleado["DESTAREA"].ToString();
                nPlanificado = double.Parse(oEmpleado["PREVISTO"].ToString());
                nConsumido = double.Parse(oEmpleado["CONSUMIDO"].ToString());
                if (nConsumido > nPlanificado) nPendiente = 0;
                else nPendiente = nPlanificado - nConsumido;

                //string sTitle = "<b>Proy. Eco.</b>: " + oEmpleado["nom_proyecto"].ToString().Replace((char)34, (char)39) + "<br><b>Proy. Téc.</b>: " + oEmpleado["t020_despt"].ToString().Replace((char)34, (char)39);
                //if (oEmpleado["t023_desfase"].ToString() != "")
                //    sTitle += "<br><b>Fase</b>:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + oEmpleado["t023_desfase"].ToString().Replace((char)34, (char)39);
                //if (oEmpleado["t024_desactividad"].ToString() != "")
                //    sTitle += "<br><b>Actividad</b>:&nbsp;&nbsp;" + oEmpleado["t024_desactividad"].ToString().Replace((char)34, (char)39);
                StringBuilder sbTitle = new StringBuilder();
                sbTitle.Append("<b>Proy. Eco.</b>: ");
                sbTitle.Append(oEmpleado["nom_proyecto"].ToString().Replace((char)34, (char)39));
                sbTitle.Append("<br><b>Proy. Téc.</b>: ");
                sbTitle.Append(oEmpleado["t331_despt"].ToString().Replace((char)34, (char)39));
                if (oEmpleado["t334_desfase"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Fase</b>:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sbTitle.Append(oEmpleado["t334_desfase"].ToString().Replace((char)34, (char)39));
                }
                if (oEmpleado["t335_desactividad"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Actividad</b>:&nbsp;&nbsp;");
                    sbTitle.Append(oEmpleado["t335_desactividad"].ToString().Replace((char)34, (char)39));
                }

                sb.Append("<tr style='cursor:default;height:16px;'");
                sb.Append("id='");
                sb.Append(idFila.ToString());
                sb.Append("' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Estructura] body=[");
                sb.Append(sbTitle);
                sb.Append("]\"");
                sb.Append("><td style='text-align:right;padding-right:15px;'>" + int.Parse(sCodTarea).ToString("#,###") + "</td>");
                sb.Append("<td>" + sDesTarea + "</td>");
                sb.Append("<td style='text-align:right;'>" + nPlanificado.ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;'>" + nConsumido.ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;'>" + nPendiente.ToString("N") + "</td></tr>");
                idFila++;
            }
            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table></div>");
            sResul = "OK@#@" + sb.ToString();
            #endregion

            sb.Length = 0; //Para liberar memoria   
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de calendario de ocupación para un profesional", ex);
        }
        return sResul;
    }
}