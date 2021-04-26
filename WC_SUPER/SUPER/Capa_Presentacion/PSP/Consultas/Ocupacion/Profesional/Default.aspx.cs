using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_Consultas_Ocupacion_Profesional_Default : System.Web.UI.Page, ICallbackEventHandler
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
                    Utilidades.SetEventosFecha(this.txtFechaInicio);
                    Utilidades.SetEventosFecha(this.txtFechaFin);

                    if (Request.QueryString["id"] != null)
                    {
                        txtCodTecnico.Text = Request.QueryString["id"].ToString();
                    }
                    if (Request.QueryString["des"] != null)
                    {
                        txtNombreTecnico.Text = Request.QueryString["des"].ToString();
                    }
                    if (Request.QueryString["ini"] != null)
                    {
                        txtFechaInicio.Text = Request.QueryString["ini"].ToString();
                    }
                    else
                    {
                        DateTime hoy = DateTime.Today;
                        txtFechaInicio.Text = hoy.ToShortDateString();
                    }
                    if (Request.QueryString["fin"] != null)
                    {
                        txtFechaFin.Text = Request.QueryString["fin"].ToString();
                    }
                    else
                    {
                        DateTime hoy2 = DateTime.Today;
                        txtFechaFin.Text = hoy2.AddMonths(1).ToShortDateString();
                    }
                    //Recojo la lista de profesionales
                    if (Request.QueryString["prof"] != null)
                    {
                        this.hdnListaProf.Text = Request.QueryString["prof"].ToString();
                    }

                    hdnEmpleado.Text = Session["UsuarioActual"].ToString();

                    string strTabla = obtenerDatosTe("TECNICO", txtFechaInicio.Text, txtFechaFin.Text, txtCodTecnico.Text, "");
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
                sResultado += obtenerDatosTe("TECNICO", aArgs[2], aArgs[3], aArgs[1], "");//ini, fin, idRecurso
                break;
            case ("tecnicomes"):
                //sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3]);
                sResultado += obtenerDatosTeMes("TECNICO", aArgs[2], aArgs[3], aArgs[1], "");//ini, fin, idRecurso
                break;
            case ("tecnicoano"):
                //sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3]);
                sResultado += obtenerDatosTeAno("TECNICO", aArgs[2], aArgs[3], aArgs[1], "");//ini, fin, idRecurso
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
    private string flGetTituloDia(DateTime dFecha)
    {
        string sRes = "";
        try
        {
            //sRes = dFecha.ToShortDateString().Substring(0, 6) + dFecha.Year.ToString().Substring(2, 2);
            sRes = dFecha.ToShortDateString();
        }
        catch (Exception)
        {
            //sRes = "";
        }
        return sRes;
    }
    private string flGetTituloMes(DateTime dFecha)
    {
        string sRes = "", sMes = "";
        int iMes;
        try
        {
            iMes = dFecha.Month;
            switch (iMes)
            {
                case 1:
                    sMes = "Ene.";
                    break;
                case 2:
                    sMes = "Feb.";
                    break;
                case 3:
                    sMes = "Mar.";
                    break;
                case 4:
                    sMes = "Abr.";
                    break;
                case 5:
                    sMes = "May.";
                    break;
                case 6:
                    sMes = "Jun.";
                    break;
                case 7:
                    sMes = "Jul.";
                    break;
                case 8:
                    sMes = "Ago.";
                    break;
                case 9:
                    sMes = "Sep.";
                    break;
                case 10:
                    sMes = "Oct.";
                    break;
                case 11:
                    sMes = "Nov.";
                    break;
                case 12:
                    sMes = "Dic.";
                    break;
            }
            sRes = sMes + dFecha.Year.ToString().Substring(2, 2);
        }
        catch (Exception)
        {
            //sRes = "";
        }
        return sRes;
    }
    private string flGetTituloMesAnt(DateTime dFecha)
    {
        string sRes = "", sMes = "";
        int iMes;
        try
        {
            dFecha = dFecha.AddMonths(-1);
            iMes = dFecha.Month;
            switch (iMes)
            {
                case 1:
                    sMes = "Ene.";
                    break;
                case 2:
                    sMes = "Feb.";
                    break;
                case 3:
                    sMes = "Mar.";
                    break;
                case 4:
                    sMes = "Abr.";
                    break;
                case 5:
                    sMes = "May.";
                    break;
                case 6:
                    sMes = "Jun.";
                    break;
                case 7:
                    sMes = "Jul.";
                    break;
                case 8:
                    sMes = "Ago.";
                    break;
                case 9:
                    sMes = "Sep.";
                    break;
                case 10:
                    sMes = "Oct.";
                    break;
                case 11:
                    sMes = "Nov.";
                    break;
                case 12:
                    sMes = "Dic.";
                    break;
            }
            sRes = sMes + dFecha.Year.ToString().Substring(2, 2);
        }
        catch (Exception)
        {
            //sRes = "";
        }
        return sRes;
    }
    private int flGetAltura(double nOcupacion, double nOcupMax)
    {
        int iRes = 0;
        double dAux = 0;
        if (nOcupMax == 0) dAux = 0;
        else dAux = (780 * nOcupacion) / nOcupMax;//780 es el nº de pixels que reservamos en la pantalla para dibujar los gráficos
        iRes = System.Convert.ToInt32(dAux);
        return iRes;
    }
    private string obtenerDatosTe(string sTipo, string sFechaDesde, string sFechaHasta, string sNumEmpleado, string sGF)
    {
        /* Obtiene las ocupaciones de un empleado en un periodo determinado
         */
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        DateTime dAux,dIniTarea,dFinTarea;
        DateTime dtD, dtH;//fecha de inicio y fin del intervalo solicitado por el usuario
        double nOcupacion = 0, nHorasEstimadas=0, nHorasCalendario=0, nMaxOcupacion=0;
        int iAltura = 0;
        try
        {
            dtD = System.Convert.ToDateTime(sFechaDesde);
            dtH = System.Convert.ToDateTime(sFechaHasta);
            int iNumDias = Fechas.DateDiff("day", dtD, dtH) + 1;
            int nWidth = iNumDias * 60;
            //Creo un dataset con tres consultas. 
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
            dr.Close();
            dr.Dispose();
            ds.Tables.Add(table);
            //Establezco una relación entre las dos tablas para poder recorrerlas como maestro-detalle
            DataColumn dcPadre = ds.Tables[0].Columns["t314_idusuario"];
            DataColumn dcHijo = ds.Tables[1].Columns["t314_idusuario"];
            DataColumn dcHijo2 = ds.Tables["DIARIO"].Columns["t314_idusuario"];
            DataRelation drRelacion = new DataRelation("rel1", dcPadre, dcHijo);
            DataRelation drRelacion2 = new DataRelation("rel2", dcPadre, dcHijo2);
            ds.Relations.Add(drRelacion);
            ds.Relations.Add(drRelacion2);
            //Recorro la primera tabla
            //sb.Append("<div style='background-image:url(../../../../../Images/imgFT20.gif); width:0%; height:0%'>");
            sb.Append("<table id='tblDatos' style='width: 950px;'>");
            sb.Append("<colgroup><col style='width:60px;' /><col style=\"width:890px;\" /></colgroup>");
            sb.Append("<tbody>");
            DataRow[] oTareas;
            DataRow[] oDias;
            foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados
            {
                oTareas = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de tareas del empleado
                oDias = oEmpleado.GetChildRows(drRelacion2);//Cargo tabla de días del empleado
                //Obtengo el valor de la máxima ocupación para dimensionar gráficamente cada día
                nMaxOcupacion = 0;
                dAux = dtD;
                //Compruebo que no falte ningun día del intervalo
                if (iNumDias != oDias.Length)
                {
                    sResul = "Error@#@El profesional " + oEmpleado["t314_idusuario"].ToString() + " " + oEmpleado["NOMBRE"].ToString() +
                             "\nno tiene desglose de calendario en el intervalo de fechas indicado." +
                             "\n\nNo se puede ejecutar la consulta.";
                    return sResul;
                }
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    nHorasEstimadas = 0;
                    nHorasCalendario = 0;
                    if (oDias[i]["T067_HORAS"].ToString() != "0")
                    {//Si el dia es laborable acumulo horas
                        nHorasCalendario = double.Parse(oDias[i]["T067_HORAS"].ToString());
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
                    if (nHorasCalendario == 0) nOcupacion = 0;
                    else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                    if (nOcupacion > nMaxOcupacion) nMaxOcupacion = nOcupacion;
                    dAux = dAux.AddDays(1);
                }
                if (nMaxOcupacion < 100) nMaxOcupacion = 100;
                //Construyo el HTML de la tabla
                dAux = dtD;
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    //if (i % 2 == 0) sb.Append("<tr class=FA style='cursor:default;height:20px;'>");
                    //else sb.Append("<tr class=FB style='cursor:default;height:20px;'>");
                    sb.Append("<tr style='cursor:default;height:20px;'>");
                    sb.Append("<td style='padding-left:5px;'");
                    if (oDias[i]["T067_HORAS"].ToString() == "0")
                        sb.Append(" color:red'>");
                    else
                        sb.Append("'>");
                    sb.Append(flGetTituloDia(dtD.AddDays(i)));
                    sb.Append("</td>");

                    nHorasEstimadas = 0;
                    nHorasCalendario = 0;
                    if (oDias[i]["T067_HORAS"].ToString() != "0")
                    {//Si el dia es laborable acumulo horas
                        nHorasCalendario = double.Parse(oDias[i]["T067_HORAS"].ToString());
                        for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                        {
                            dIniTarea = (DateTime)oTareas[j]["FINI"];
                            dFinTarea = (DateTime)oTareas[j]["FFIN"];
                            if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                        }
                    }
                    if (nHorasCalendario == 0) nOcupacion = 0;
                    else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                    iAltura = flGetAltura(nOcupacion, nMaxOcupacion);
                    sb.Append("<td style=\"background-image:url(../../../../../Images/imgGanttBGSemana.gif);\">");
                    if (iAltura != 0)
                    {
                        if (nOcupacion > 100)
                            sb.Append("<img src='../../../../../Images/imgRojo.gif' height='15px' width='" + iAltura.ToString() + "px'>&nbsp;");
                        else
                        {
                            if (nOcupacion > 75)
                                sb.Append("<img src='../../../../../Images/imgNaranja.gif' height='15px' width='" + iAltura.ToString() + "px'>&nbsp;");
                            else
                                sb.Append("<img src='../../../../../Images/imgGanttT.gif' height='15px' width='" + iAltura.ToString() + "px'>&nbsp;");
                        }
                    }
                    else
                        sb.Append("<img src='../../../../../Images/imgSeparador.gif' height='15px' width='1px'>&nbsp;");
                    //Posiciono el label con el valor de la ocupación encima de la imagen
                    sb.Append(nOcupacion.ToString("N"));
                    sb.Append("%</td></tr>");
                    dAux = dAux.AddDays(1);
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
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de grado de ocupación para un profesional", ex);
        }
        return sResul;
    }
    private string obtenerDatosTeMes(string sTipo, string sFechaDesde, string sFechaHasta, string sNumEmpleado, string sGF)
    {
        /* Obtiene las ocupaciones de un empleado en un periodo de meses determinado
         */
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        DateTime dAux, dIniTarea, dFinTarea;
        DateTime dtD, dtH;//fecha de inicio y fin del intervalo solicitado por el usuario
        double nOcupacion = 0, nHorasEstimadas = 0, nHorasCalendario = 0, nMaxOcupacion = 0;
        int iAltura = 0, iDiaAct, iMesAnt, iMesAct, iNumMeses, iNumColumns;
        try
        {
            dtD = System.Convert.ToDateTime(sFechaDesde);
            //calculo el primer día del mes de la fecha Desde
            iDiaAct = dtD.Day - 1;
            dtD = dtD.AddDays(-iDiaAct);
            dtH = System.Convert.ToDateTime(sFechaHasta);
            //calculo el último día del mes de la fecha Hasta
            dtH = dtH.AddMonths(1);
            iDiaAct = dtH.Day;
            dtH=dtH.AddDays(-iDiaAct);

            int iNumDias = Fechas.DateDiff("day", dtD, dtH) + 1;
            iNumColumns = Fechas.DateDiff("month", dtD, dtH);
            DataSet ds = null;
            SqlDataReader dr = null;
            switch (sTipo.ToUpper())
            {
                case "TECNICO":
                    ds = Recurso.OcupacionProfesional(int.Parse(sNumEmpleado), dtD, dtH);
                    dr = Recurso.OcupacionProfesional2(int.Parse(sNumEmpleado), dtD, dtH);
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
            dr.Close();
            dr.Dispose();
            ds.Tables.Add(table);
            //Establezco una relación entre las dos tablas para poder recorrerlas como maestro-detalle
            DataColumn dcPadre = ds.Tables[0].Columns["t314_idusuario"];
            DataColumn dcHijo = ds.Tables[1].Columns["t314_idusuario"];
            DataColumn dcHijo2 = ds.Tables[2].Columns["t314_idusuario"];
            DataRelation drRelacion = new DataRelation("rel1", dcPadre, dcHijo);
            DataRelation drRelacion2 = new DataRelation("rel2", dcPadre, dcHijo2);
            ds.Relations.Add(drRelacion);
            ds.Relations.Add(drRelacion2);
            //Recorro la primera tabla
            DataRow[] oTareas;
            DataRow[] oDias;
            foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados
            {
                oTareas = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de tareas del empleado
                oDias = oEmpleado.GetChildRows(drRelacion2);//Cargo tabla de días del empleado
                //Obtengo el valor de la máxima ocupación para dimensionar gráficamente cada día
                nMaxOcupacion = 0;
                dAux = dtD;
                //Compruebo que no falte ningun día del intervalo
                if (iNumDias != oDias.Length)
                {
                    sResul = "Error@#@El profesional " + oEmpleado["t314_idusuario"].ToString() + " " + oEmpleado["NOMBRE"].ToString() +
                             "\nno tiene desglose de calendario en el intervalo de fechas indicado." +
                             "\n\nNo se puede ejecutar la consulta.";
                    return sResul;
                }
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    nHorasEstimadas = 0;
                    nHorasCalendario = 0;
                    if (oDias[i]["T067_HORAS"].ToString() != "0")
                    {//Si el dia es laborable acumulo horas
                        nHorasCalendario = double.Parse(oDias[i]["T067_HORAS"].ToString());
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
                    if (nHorasCalendario == 0) nOcupacion = 0;
                    else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                    if (nOcupacion > nMaxOcupacion) nMaxOcupacion = nOcupacion;
                    dAux = dAux.AddDays(1);
                }
                if (nMaxOcupacion < 100) nMaxOcupacion = 100;
                //Construyo el HTML de la tabla
                int nWidth = iNumColumns * 60;

                sb.Append("<table id='tblDatos' style='width: 950px;'>");
                sb.Append("<colgroup><col style='width:60px;' /><col style=\"width:890px;\" /></colgroup>");
                sb.Append("<tbody>");


                //sb.Append("<div style='background-image:url(../../../../../Images/imgFT20.gif); width:0%; height:0%'>");
                //sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 934px;margin:5px;' cellSpacing='0' border='0'>");
                //sb.Append("<colgroup><col style='width:60px;padding-left:5px;' /><col style=\"background-image:url('../../../../../Images/imgGanttBGSemana.gif');\" /></colgroup>");
                //sb.Append("<tbody>");
                nHorasEstimadas = 0;
                nHorasCalendario = 0;
                dAux = dtD;
                iMesAnt = dAux.Month;
                iNumMeses = 0;
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    iMesAct = dAux.Month;
                    if (iMesAct == iMesAnt)
                    {
                        if (oDias[i]["T067_HORAS"].ToString() != "0")
                        {//Si el dia es laborable acumulo horas
                            nHorasCalendario += double.Parse(oDias[i]["T067_HORAS"].ToString());
                            for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                            {
                                dIniTarea = (DateTime)oTareas[j]["FINI"];
                                dFinTarea = (DateTime)oTareas[j]["FFIN"];
                                if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                    nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                            }
                        }
                        if (i == oDias.Length - 1)//Para pintar el último mes
                        {
                            if (nHorasCalendario == 0) nOcupacion = 0;
                            else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                            iAltura = flGetAltura(nOcupacion, nMaxOcupacion);
                            //iAltura = flGetLargura(nOcupacion);
                            //sb.Append("<td width='45px' align='center'>");
                            sb.Append("<tr style='cursor:default;'>");
                            sb.Append("<td style='padding-left:5px;'>" + flGetTituloMes(dAux) + "</td>");

                            sb.Append("<td style=\"background-image:url('../../../../../Images/imgGanttBGSemana.gif');\">");
                            if (iAltura != 0)
                            {
                                if (nOcupacion > 100)
                                    sb.Append("<img src='../../../../../Images/imgRojo.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                                else
                                {
                                    if (nOcupacion > 75)
                                        sb.Append("<img src='../../../../../Images/imgNaranja.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                                    else
                                        sb.Append("<img src='../../../../../Images/imgGanttT.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                                }
                            }
                            else
                                sb.Append("<img src='../../../../../Images/imgSeparador.gif' height='15px' width='1px'>");
                            //Posiciono el label con el valor de la ocupación encima de la imagen
                            //int nAux = 450 - iAltura - 18;
                            //int nAux2 = 3 + (iNumMeses * 60);
                            sb.Append("&nbsp;");
                            sb.Append(nOcupacion.ToString("N"));
                            sb.Append("%");
                            iNumMeses++;
                            //sb.Append("<label id=lblOc" + i.ToString() + " style='position:absolute; top: " + nAux.ToString() + "px;left:" + nAux2.ToString() + "px;width:50px;align-text:left;'>" + nOcupacion.ToString("N") + " %</label>");
                            sb.Append("</td></tr>");

                            nHorasEstimadas = 0;
                            nHorasCalendario = 0;
                        }
                    }
                    else
                    {
                        if (nHorasCalendario == 0) nOcupacion = 0;
                        else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                        iAltura = flGetAltura(nOcupacion, nMaxOcupacion);
                        //iAltura = flGetLargura(nOcupacion);
                        //sb.Append("<td width='45px' align='center'>");
                        sb.Append("<tr><td style='padding-left:5px;'>" + flGetTituloMesAnt(dAux) + "</td>");
                        sb.Append("<td style=\"background-image:url('../../../../../Images/imgGanttBGSemana.gif');\">");
                        if (iAltura != 0)
                        {
                            if (nOcupacion > 100)
                                sb.Append("<img src='../../../../../Images/imgRojo.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                            else
                            {
                                if (nOcupacion > 75)
                                    sb.Append("<img src='../../../../../Images/imgNaranja.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                                else
                                    sb.Append("<img src='../../../../Images/imgGanttT.gif' height='15px' width='" + iAltura.ToString() + "px'>");
                            }
                        }
                        else
                            sb.Append("<img src='../../../../../Images/imgSeparador.gif' height='15px' width='1px'>");
                        //Posiciono el label con el valor de la ocupación encima de la imagen
                        //int nAux = 450 - iAltura - 18;
                        //int nAux2 = 3 + (iNumMeses * 60);
                        sb.Append("&nbsp;");
                        sb.Append(nOcupacion.ToString("N"));
                        sb.Append("%");
                        iNumMeses++;
                        //sb.Append("<label id=lblOc" + i.ToString() + " style='position:absolute; top: " + nAux.ToString() + "px;left:" + nAux2.ToString() + "px;width:50px;align-text:left;'>" + nOcupacion.ToString("N") + " %</label>");
                        sb.Append("</td></tr>");

                        nHorasEstimadas = 0;
                        nHorasCalendario = 0;
                        if (oDias[i]["T067_HORAS"].ToString() != "0")
                        {//Si el dia es laborable acumulo horas
                            nHorasCalendario += double.Parse(oDias[i]["T067_HORAS"].ToString());
                            for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                            {
                                dIniTarea = (DateTime)oTareas[j]["FINI"];
                                dFinTarea = (DateTime)oTareas[j]["FFIN"];
                                if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                    nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                            }
                        }
                    }
                    dAux = dAux.AddDays(1);
                    iMesAnt = iMesAct;
                }
                //sb.Append("</tr>");
                ////Añado fila con las fechas
                //sb.Append("<tr style='height:17px'>");
                ////Calculo la linea de pie con los meses
                //dAux = dtD;
                //iMesAnt = -1;
                //for (int i = 0; i < iNumDias; i++)
                //{
                //    iMesAct = dAux.Month;
                //    if (iMesAct != iMesAnt)
                //    {
                //        sb.Append("<td width='45px' align='center' style='font-size:8pt;'>");
                //        sb.Append(flGetTituloMes(dAux));
                //        sb.Append("</td>");
                //    }
                //    dAux = dAux.AddDays(1);
                //    iMesAnt = iMesAct;
                //}
                //sb.Append("</TR>");
            }

            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table></div>");
            sResul = "OK@#@" + sb.ToString();

            sb.Length = 0; //Para liberar memoria   
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de grado de ocupación para un profesional", ex);
        }
        return sResul;
    }
    private string obtenerDatosTeAno(string sTipo, string sFechaDesde, string sFechaHasta, string sNumEmpleado, string sGF)
    {
        /* Obtiene las ocupaciones de un empleado en un periodo de años determinado
         */
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        DateTime dAux, dIniTarea, dFinTarea;
        DateTime dtD, dtH;//fecha de inicio y fin del intervalo solicitado por el usuario
        double nOcupacion = 0, nHorasEstimadas = 0, nHorasCalendario = 0, nMaxOcupacion = 0;
        int iAltura = 0, iAnoAnt, iAnoAct, iNumAnos, iNumColumns;
        try
        {
            dtD = System.Convert.ToDateTime(sFechaDesde);
            //calculo el primer día del año de la fecha Desde
            iAnoAct = dtD.Year;
            dtD = System.Convert.ToDateTime("01/01/" + iAnoAct.ToString());
            dtH = System.Convert.ToDateTime(sFechaHasta);
            //calculo el último día del año de la fecha Hasta
            iAnoAct = dtH.Year;
            dtH = System.Convert.ToDateTime("31/12/" + iAnoAct.ToString());

            int iNumDias = Fechas.DateDiff("day", dtD, dtH) + 1;
            iNumColumns = Fechas.DateDiff("year", dtD, dtH);
            DataSet ds = null;
            SqlDataReader dr = null;
            switch (sTipo.ToUpper())
            {
                case "TECNICO":
                    ds = Recurso.OcupacionProfesional(int.Parse(sNumEmpleado), dtD, dtH);
                    dr = Recurso.OcupacionProfesional2(int.Parse(sNumEmpleado), dtD, dtH);
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
            dr.Close();
            dr.Dispose();
            ds.Tables.Add(table);
            //Establezco una relación entre las dos tablas para poder recorrerlas como maestro-detalle
            DataColumn dcPadre = ds.Tables[0].Columns["t314_idusuario"];
            DataColumn dcHijo = ds.Tables[1].Columns["t314_idusuario"];
            DataColumn dcHijo2 = ds.Tables[2].Columns["t314_idusuario"];
            DataRelation drRelacion = new DataRelation("rel1", dcPadre, dcHijo);
            DataRelation drRelacion2 = new DataRelation("rel2", dcPadre, dcHijo2);
            ds.Relations.Add(drRelacion);
            ds.Relations.Add(drRelacion2);
            //Recorro la primera tabla
            DataRow[] oTareas;
            DataRow[] oDias;
            foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados
            {
                oTareas = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de tareas del empleado
                oDias = oEmpleado.GetChildRows(drRelacion2);//Cargo tabla de días del empleado
                //Obtengo el valor de la máxima ocupación para dimensionar gráficamente cada día
                nMaxOcupacion = 0;
                dAux = dtD;
                //Compruebo que no falte ningun día del intervalo
                if (iNumDias != oDias.Length)
                {
                    sResul = "Error@#@El profesional " + oEmpleado["t314_idusuario"].ToString() + " " + oEmpleado["NOMBRE"].ToString() +
                             "\nno tiene desglose de calendario en el intervalo de fechas indicado." +
                             "\n\nNo se puede ejecutar la consulta.";
                    return sResul;
                }
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    nHorasEstimadas = 0;
                    nHorasCalendario = 0;
                    if (oDias[i]["T067_HORAS"].ToString() != "0")
                    {//Si el dia es laborable acumulo horas
                        nHorasCalendario = double.Parse(oDias[i]["T067_HORAS"].ToString());
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
                    if (nHorasCalendario == 0) nOcupacion = 0;
                    else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                    if (nOcupacion > nMaxOcupacion) nMaxOcupacion = nOcupacion;
                    dAux = dAux.AddDays(1);
                }
                if (nMaxOcupacion < 100) nMaxOcupacion = 100;
                //Construyo el HTML de la tabla
                int nWidth = iNumColumns * 60;
                //sb.Append("<div style='background-image:url(../../../../../Images/imgFT20.gif); width:0%; height:0%'>");
                sb.Append("<table id='tblDatos' class='texto' style='width: " + nWidth.ToString() + "px;height:467px;' cellSpacing='0' border='0'>");
                sb.Append("<tbody>");
                sb.Append("<tr class=FB style='height:450px; vertical-align:bottom;'>");
                nHorasEstimadas = 0;
                nHorasCalendario = 0;
                dAux = dtD;
                iAnoAnt = dAux.Year;
                iNumAnos = 0;
                for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                {
                    iAnoAct = dAux.Year;
                    if (iAnoAct == iAnoAnt)
                    {
                        if (oDias[i]["T067_HORAS"].ToString() != "0")
                        {//Si el dia es laborable acumulo horas
                            nHorasCalendario += double.Parse(oDias[i]["T067_HORAS"].ToString());
                            for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                            {
                                dIniTarea = (DateTime)oTareas[j]["FINI"];
                                dFinTarea = (DateTime)oTareas[j]["FFIN"];
                                if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                    nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                            }
                        }
                        if (i == oDias.Length - 1)//Para pintar el último año
                        {
                            if (nHorasCalendario == 0) nOcupacion = 0;
                            else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                            iAltura = flGetAltura(nOcupacion, nMaxOcupacion);
                            sb.Append("<td width='45px' align='center'>");
                            if (iAltura != 0)
                            {
                                if (nOcupacion > 100)
                                    sb.Append("<img src='../../../../../Images/imgRojo.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                                else
                                {
                                    if (nOcupacion > 75)
                                        sb.Append("<img src='../../../../../Images/imgNaranja.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                                    else
                                        sb.Append("<img src='../../../../../Images/imgGanttT.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                                }
                            }
                            else
                                sb.Append("<img src='../../../../../Images/imgSeparador.gif' width='40px' height='1px'>");
                            //Posiciono el label con el valor de la ocupación encima de la imagen
                            int nAux = 450 - iAltura - 18;
                            int nAux2 = 3 + (iNumAnos * 60);
                            iNumAnos++;
                            sb.Append("<label id=lblOc" + i.ToString() + " style='position:absolute; top: " + nAux.ToString() + "px;left:" + nAux2.ToString() + "px;width:50px;align-text:left;'>" + nOcupacion.ToString("N") + " %</label>");
                            sb.Append("</td>");

                            nHorasEstimadas = 0;
                            nHorasCalendario = 0;
                        }
                    }
                    else
                    {
                        if (nHorasCalendario == 0) nOcupacion = 0;
                        else nOcupacion = (nHorasEstimadas / nHorasCalendario) * 100;
                        iAltura = flGetAltura(nOcupacion, nMaxOcupacion);
                        sb.Append("<td width='45px' align='center'>");
                        if (iAltura != 0)
                        {
                            if (nOcupacion > 100)
                                sb.Append("<img src='../../../../../Images/imgRojo.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                            else
                            {
                                if (nOcupacion > 75)
                                    sb.Append("<img src='../../../../../Images/imgNaranja.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                                else
                                    sb.Append("<img src='../../../../../Images/imgGanttT.gif' width='40px' height='" + iAltura.ToString() + "px'>");
                            }
                        }
                        else
                            sb.Append("<img src='../../../../../Images/imgSeparador.gif' width='40px' height='1px'>");
                        //Posiciono el label con el valor de la ocupación encima de la imagen
                        int nAux = 450 - iAltura - 18;
                        int nAux2 = 3 + (iNumAnos * 60);
                        iNumAnos++;
                        sb.Append("<label id=lblOc" + i.ToString() + " style='position:absolute; top: " + nAux.ToString() + "px;left:" + nAux2.ToString() + "px;width:50px;align-text:left;'>" + nOcupacion.ToString("N") + " %</label>");
                        sb.Append("</td>");

                        nHorasEstimadas = 0;
                        nHorasCalendario = 0;
                        if (oDias[i]["T067_HORAS"].ToString() != "0")
                        {//Si el dia es laborable acumulo horas
                            nHorasCalendario += double.Parse(oDias[i]["T067_HORAS"].ToString());
                            for (int j = 0; j < oTareas.Length; j++)//Recorro tabla de tareas
                            {
                                dIniTarea = (DateTime)oTareas[j]["FINI"];
                                dFinTarea = (DateTime)oTareas[j]["FFIN"];
                                if ((dAux >= dIniTarea) && (dAux <= dFinTarea))
                                    nHorasEstimadas += double.Parse(oTareas[j]["HORASPORDIALAB"].ToString());
                            }
                        }
                    }
                    dAux = dAux.AddDays(1);
                    iAnoAnt = iAnoAct;
                }
                sb.Append("</tr>");
                //Añado fila con las fechas
                sb.Append("<tr style='height:17px'>");
                //Calculo la linea de pie con los años
                iAnoAnt = dtD.Year;
                iAnoAct = dtH.Year;
                for (int i = iAnoAnt; i <= iAnoAct; i++)
                {
                    sb.Append("<td width='45px' align='center' style='font-size:8pt;'>");
                    sb.Append(i.ToString());
                    sb.Append("</td>");
                }
                sb.Append("</TR>");
            }

            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();

            sb.Length = 0; //Para liberar memoria   
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
        dAux = 3 * nOcupacion;
        iRes = System.Convert.ToInt32(dAux);
        return iRes;
    }
}