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
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Capa_Presentacion_Consultas_Ocupacion_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sNodo="C.R.";
    //protected int nIndice;
    public int i = 0;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Cargo la denominacion del label Nodo
            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            if (sNodo.Trim() != "")
            {
                this.lblNodo.InnerText = sNodo;
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                rdbAmbito.Items[1].Text = sNodo;
            }
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 29;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Grado de ocupación por profesional";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            try
            {
                if (!Page.IsPostBack)
                {
                    Utilidades.SetEventosFecha(this.txtFechaInicio);
                    Utilidades.SetEventosFecha(this.txtFechaFin);
 
                    DateTime hoy = DateTime.Today;
                    txtFechaInicio.Text = hoy.ToShortDateString();
                    txtFechaFin.Text = hoy.AddMonths(1).ToShortDateString();
                    hdnEmpleado.Text = Session["UsuarioActual"].ToString();
                    USUARIO oUser = USUARIO.Select(null, int.Parse(hdnEmpleado.Text));
                    if (oUser.t303_idnodo != null)
                    {
                        int iNodo;
                        iNodo = (int)oUser.t303_idnodo;
                        hdnCR.Text = iNodo.ToString();
                        this.txtCodCR.Text = oUser.t303_idnodo.ToString();
                        NODO oNodo = NODO.ObtenerNodo(null, iNodo);
                        this.txtCR.Text = oNodo.t303_denominacion;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("tecnico")://ini, fin, idRecurso, campo orden, ascendente o descentente
                sResultado += obtenerDatosTe("TECNICO", aArgs[4], aArgs[5], aArgs[3], "", int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("cr")://ini, fin, CR, campo orden, ascendente o descentente
                sResultado += obtenerDatosTe("CR", aArgs[4], aArgs[5], aArgs[3], "", int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("gf")://ini, fin, CR, GF
                sResultado += obtenerDatosTe("GF", aArgs[5], aArgs[6], aArgs[3], aArgs[4], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("pe")://ini, fin, CR, PE
                sResultado += obtenerDatosTe("PE", aArgs[5], aArgs[6], aArgs[3], aArgs[4], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("fu")://ini, fin, CR, funcion
                sResultado += obtenerDatosTe("FU", aArgs[5], aArgs[6], aArgs[3], aArgs[4], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
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

    private string obtenerDatosTe(string sTipo, string sFDesde, string sFHasta, string sNumEmpleado, string sGF, int nOrden, int nAscDesc)
    {
        /* Obtiene las ocupaciones de empleados en un periodo determinado
         */
        string sResul = "", sCodEmpleado = "", sNomEmpleado = "", sListaErrores="";
        StringBuilder sb = new StringBuilder();
        DateTime dAux,dIniTarea,dFinTarea;
        DateTime dtD, dtH;//fecha de inicio y fin del intervalo solicitado por el usuario
        double nOcupacion = 0, nHorasEstimadas=0, nHorasCalendario=0;
        int iLargura = 0,idFila=0;
        bool bError = false;
        try
        {
            if (!Utilidades.isDate(sFDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sFHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                dtD = System.Convert.ToDateTime(sFDesde);
                dtH = System.Convert.ToDateTime(sFHasta);
                int iNumDias = Fechas.DateDiff("day", dtD, dtH) + 1;
                //Creo un dataset con dos consultas. 
                //Una con empleados y total de horas laborables del calendario 
                //otra con empleados, tareas y horas por dia laborable
                //y otra con empleados,dias y horas laborables del calendario 
                DataSet ds = null;
                SqlDataReader dr = null;
                switch (sTipo.ToUpper())
                {
                    case "TECNICO":
                        ds = Recurso.OcupacionProfesional(int.Parse(sNumEmpleado), dtD, dtH);
                        dr = Recurso.OcupacionProfesional2(int.Parse(sNumEmpleado), dtD, dtH);
                        //sParam+=
                        break;
                    case "CR":
                        //En este caso en el parametro sNumEmpleado viene el código de CR
                        ds = Recurso.OcupacionProfesionalNodo(int.Parse(sNumEmpleado), dtD, dtH);
                        dr = Recurso.OcupacionProfesionalNodo2(int.Parse(sNumEmpleado), dtD, dtH);
                        break;
                    case "GF":
                        //Grupo Funcional. 
                        ds = Recurso.OcupacionProfesionalGF(int.Parse(sGF), dtD, dtH);
                        dr = Recurso.OcupacionProfesionalGF2(int.Parse(sGF), dtD, dtH);
                        break;
                    case "PE":
                        //Proyecto economico. En este caso en el parametro sGF viene el código de PE
                        ds = Recurso.OcupacionProfesionalPE(int.Parse(sGF), dtD, dtH);
                        //El desglose diario lo cargo a mano porque porque sino, si hay muchas líneas da TimeOut
                        dr = Recurso.OcupacionProfesionalPE2(int.Parse(sGF), dtD, dtH);
                        break;
                    case "FU":
                        //Funcion. En este caso en el parametro sGF viene el código de funcion
                        ds = Recurso.OcupacionProfesionalFU(int.Parse(sGF), dtD, dtH);
                        dr = Recurso.OcupacionProfesionalFU2(int.Parse(sGF), dtD, dtH);
                        break;
                }
                // Creo DataTable para añadirlo manualmente al dataset
                DataTable table = new DataTable("DIARIO");
                // Create a DataColumn and set various properties. 
                DataColumn column1 = new DataColumn();
                column1.DataType = System.Type.GetType("System.Int32");
                column1.ColumnName = "t314_idusuario";
                table.Columns.Add(column1);

                DataColumn column2 = new DataColumn();
                //column2.DataType = System.Type.GetType("System.DateTime");
                column2.DataType = System.Type.GetType("System.String");
                column2.ColumnName = "T067_DIA";
                table.Columns.Add(column2);

                DataColumn column3 = new DataColumn();
                column3.DataType = System.Type.GetType("System.Double");
                column3.ColumnName = "T067_HORAS";
                table.Columns.Add(column3);
                // Añado las filas
                DataRow row;
                while (dr.Read())
                {
                    row = table.NewRow();
                    row["t314_idusuario"] = dr["t314_idusuario"];
                    row["T067_DIA"] = dr["T067_DIA"];
                    row["T067_HORAS"] = dr["T067_HORAS"];
                    table.Rows.Add(row);
                }
                ds.Tables.Add(table);
                dr.Close();
                dr.Dispose();

                //Establezco una relación entre las dos tablas para poder recorrerlas como maestro-detalle
                DataColumn dcPadre = ds.Tables[0].Columns["t314_idusuario"];
                DataColumn dcHijo = ds.Tables[1].Columns["t314_idusuario"];
                DataColumn dcHijo2 = ds.Tables["DIARIO"].Columns["t314_idusuario"];
                DataRelation drRelacion = new DataRelation("rel1", dcPadre, dcHijo);
                DataRelation drRelacion2 = new DataRelation("rel2", dcPadre, dcHijo2);
                ds.Relations.Add(drRelacion);
                ds.Relations.Add(drRelacion2);
                //Cacheo el DataSet calculado
                //Creo una tabla nueva para guardar los parámetros de la consulta
                //HttpContext.Current.Cache.Insert("DataSetOcupacion", ds, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero);
                //Recorro la primera tabla y calculo y asigno el valor de la ocupación en el periodo para cada empleado
                idFila = 0;
                DataRow[] oTareas;
                DataRow[] oDias;
                foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados
                {
                    nHorasEstimadas = 0;
                    nHorasCalendario = double.Parse(oEmpleado["HORASLAB"].ToString());
                    oTareas = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de tareas del empleado
                    oDias = oEmpleado.GetChildRows(drRelacion2);//Cargo tabla de días del empleado
                    dAux = dtD;
                    //Compruebo que no falte ningun día del intervalo
                    if (iNumDias != oDias.Length)
                    {
                        //sListaErrores += "Error@#@El profesional " + oEmpleado["t314_idusuario"].ToString() + " " + oEmpleado["Profesional"].ToString() +
                        //         "no tiene desglose de calendario en el intervalo de fechas indicado.\n";// + 
                        sListaErrores += int.Parse(oEmpleado["t314_idusuario"].ToString()).ToString("#,###") + " " + oEmpleado["Profesional"].ToString() + "\n";
                        //"\n\nNo se puede ejecutar la consulta.";
                        //return sResul;
                        oEmpleado.Delete();
                        idFila++;
                        continue;
                    }
                    for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                    {
                        if (oDias[i]["T067_HORAS"].ToString() != "0")
                        {//Si el dia es laborable acumulo horas
                            for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                            {
                                dIniTarea = (DateTime)oTareas[j]["FINI"];
                                dFinTarea = (DateTime)oTareas[j]["FFIN"];
                                if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                {
                                    nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                                }
                            }
                        }
                        dAux = dAux.AddDays(1);
                    }
                    if (nHorasCalendario == 0) nOcupacion = 0;
                    else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                    //Asigno el valor de la ocupación calculada a la 1ª tabla del dataset
                    ds.Tables[0].Rows[idFila]["OCUPACION"] = nOcupacion;
                    idFila++;
                }

                //Una vez construido el dataset lo recorro con la ordenación deseada y construyo el HTML de la tabla
                idFila = 0;
                DataRow[] oEmpleados;
                if (nOrden == 1)
                {
                    if (nAscDesc == 0) oEmpleados = ds.Tables[0].Select("", "Profesional ASC");
                    else oEmpleados = ds.Tables[0].Select("", "Profesional DESC");
                }
                else
                {
                    if (nAscDesc == 0) oEmpleados = ds.Tables[0].Select("", "OCUPACION ASC");
                    else oEmpleados = ds.Tables[0].Select("", "OCUPACION DESC");
                }
                //sb.Append("<div style='background-image:url(../../../../Images/imgFT20.gif); width:0%; height:0%'>");
                sb.Append("<table id='tblDatos' class='texto MA' style='width: 960px;'>");
                sb.Append("<colgroup><col style='width:20px;'/><col style='width:259px;' /><col style='width:681px;' /></colgroup>");
                sb.Append("<tbody>");
                foreach (DataRow oEmpleado in oEmpleados)//Recorro tabla de empleados
                {
                    sCodEmpleado = oEmpleado["t314_idusuario"].ToString();
                    sNomEmpleado = oEmpleado["Profesional"].ToString();
                    nOcupacion = double.Parse(oEmpleado["OCUPACION"].ToString());
                    iLargura = flGetLargura(nOcupacion);

                    sb.Append("<tr id=" + idFila.ToString() + " idR=" + sCodEmpleado + " style='height:20px;noWrap:true;' ");

                    //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sNomEmpleado.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(sCodEmpleado).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oEmpleado["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + oEmpleado["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sNomEmpleado.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(sCodEmpleado).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oEmpleado["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    if (oEmpleado["t303_denominacion"].ToString() == "")
                        sb.Append(" tipo ='E'");
                    else
                        sb.Append(" tipo ='I'");
                    sb.Append(" sexo ='" + oEmpleado["t001_sexo"].ToString() + "'");
                    sb.Append(" baja ='" + oEmpleado["baja"].ToString() + "'");

                    sb.Append("><td></td>");
                    sb.Append("<td style='padding-left:3px;' ondblclick='mostrarTareas(" + sCodEmpleado + ",\"" + sNomEmpleado + "\")'><span class='NBR W255'>");
                    sb.Append(sNomEmpleado + "</span></td><td style='style=\"width:681px; background-image:url(../../../../Images/imgGanttBGSemana.gif);\"' ondblclick='mostrarDetalle(" + idFila.ToString() + ")'>");
                    if (iLargura != 0)
                    {
                        if (nOcupacion > 100)
                            sb.Append("<img src='../../../../Images/imgRojo.gif' height='13px' width='" + iLargura.ToString() + "px'>&nbsp;");
                        else
                        {
                            if (nOcupacion > 75)
                                sb.Append("<img src='../../../../Images/imgNaranja.gif' height='13px' width='" + iLargura.ToString() + "px'>&nbsp;");
                            else
                                sb.Append("<img src='../../../../Images/imgGanttT.gif' height='13px' width='" + iLargura.ToString() + "px'>&nbsp;");
                        }
                        sb.Append(nOcupacion.ToString("N") + "%");
                    }
                    sb.Append("</td></tr>");
                    idFila++;
                }
                ds.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");
                sResul = "OK@#@" + sb.ToString() + "@#@" + sListaErrores;

                sb.Length = 0; //Para liberar memoria   
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de grado de ocupación para un profesional", ex);
        }
        return sResul;
    }
    private int flGetLargura(double nOcupacion)
    {
        int iRes = 0;
        double dAux = 0;
        dAux = 4 * nOcupacion;
        iRes = System.Convert.ToInt32(dAux);
        return iRes;
    }
    private string recuperarPSN(string nIdProySubNodo)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nIdProySubNodo), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString());  //2
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }

}