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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    //public int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Cargo la denominacion del label Nodo
            string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            if (sAux.Trim() != "")
            {
                this.lblNodo.InnerText = sAux;
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                rdbAmbito.Items[1].Text = sAux;
            }
            Master.TituloPagina = "Calendario de ocupación por profesional";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                if (!Page.IsPostBack)
                {
                    //DateTime hoy = DateTime.Today;
                    //hdnFechaDesde.Text = hoy.ToShortDateString();
                    hdnEmpleado.Text = Session["UsuarioActual"].ToString();
                    USUARIO oUser = USUARIO.Select(null, int.Parse(hdnEmpleado.Text));
                    if (oUser.t303_idnodo != null)
                    {
                        int iNodo;
                        iNodo = (int)oUser.t303_idnodo;
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
            case ("tecnico"):// idRecurso, campo orden, ascendente o descentente
                sResultado += obtenerDatosTe("TECNICO", aArgs[3], "", int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("cr"):// CR, campo orden, ascendente o descentente
                sResultado += obtenerDatosTe("CR", aArgs[3], "", int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("gf"):// GF
                sResultado += obtenerDatosTe("GF", "", aArgs[3], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("pe"):// PE
                sResultado += obtenerDatosTe("PE", "", aArgs[3], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("fu"):// funcion
                sResultado += obtenerDatosTe("FU", "", aArgs[3], int.Parse(aArgs[1]), int.Parse(aArgs[2]));
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
    private string obtenerDatosTe(string sTipo, string sNumEmpleado, string sGF, int nOrden, int nAscDesc)
    {
        /* Obtiene las ocupaciones de empleados en un periodo determinado
         */
        string sResul = "", sEmpleado, sNomEmpleado, sListaErrores="";//, sTarea;
        StringBuilder sb = new StringBuilder();
        DateTime dAux;
        double nOcupacion = 0, nOcupacionMax = 0, nHorasEstimadas = 0, nHorasCalendario = 0, nPendiente=0;
        int iLargura = 0,idFila=0, iNumDias=0;
        try
        {
            #region Leer datos
            //Creo un dataset con dos consultas. 
            //Una con empleados-tareas, total de horas estimadas y total de horas consumidas
            //y otra con empleados y horas pendientes
            DataSet ds = null;
            //SqlDataReader dr = null;
            switch (sTipo.ToUpper())
            {
                case "TECNICO":
                    ds = Recurso.OcupacionProfesionalCalendario(int.Parse(sNumEmpleado));
                    //dr = Recurso.OcupacionProfesionalCalendario2(int.Parse(sNumEmpleado));
                    break;
                case "CR":
                    //En este caso en el parametro sNumEmpleado viene el código de CR
                    ds = Recurso.OcupacionProfesionalCalendarioNodo(int.Parse(sNumEmpleado));
                    //dr = Recurso.OcupacionProfesionalCalendarioCR2(int.Parse(sNumEmpleado));
                    break;
                case "GF":
                    //Grupo Funcional. En este caso en el parametro sNumEmpleado viene el código de CR
                    //ds = Recurso.OcupacionProfesionalCalendarioGF(int.Parse(sNumEmpleado), int.Parse(sGF));
                    ds = Recurso.OcupacionProfesionalCalendarioGF(int.Parse(sGF));
                    //dr = Recurso.OcupacionProfesionalCalendarioGF2(int.Parse(sNumEmpleado), int.Parse(sGF));
                    break;
                case "PE":
                    //Proyecto economico. En este caso en el parametro sGF viene el id de PE
                    ds = Recurso.OcupacionProfesionalCalendarioPE(int.Parse(sGF));
                    //El desglose diario lo cargo a mano porque porque sino, si hay muchas líneas da TimeOut
                    //dr = Recurso.OcupacionProfesionalCalendarioPE2(int.Parse(sNumEmpleado), int.Parse(sGF));
                    break;
                case "FU":
                    //Funcion. En este caso en el parametro sNumEmpleado viene el código de CR
                    //y en el parametro sGF el código de funcion
                    //ds = Recurso.OcupacionProfesionalCalendarioFU(int.Parse(sNumEmpleado), int.Parse(sGF));
                    ds = Recurso.OcupacionProfesionalCalendarioFU(int.Parse(sGF));
                    //dr = Recurso.OcupacionProfesionalCalendarioFU2(int.Parse(sNumEmpleado), int.Parse(sGF));
                    break;
            }
            if (ds.Tables[0].Rows.Count < 1)
            {
                return "Atención@#@No hay datos disponibles";
            }
            #endregion
            #region Creo DataTable con el calendario para añadirlo manualmente al dataset
            //DataTable table = new DataTable("DIARIO");
            //// Create a DataColumn and set various properties. 
            //DataColumn column1 = new DataColumn();
            //column1.DataType = System.Type.GetType("System.Int32");
            //column1.ColumnName = "NUM_EMPLEADO";
            //table.Columns.Add(column1);

            //DataColumn column2 = new DataColumn();
            ////column2.DataType = System.Type.GetType("System.DateTime");
            //column2.DataType = System.Type.GetType("System.String");
            //column2.ColumnName = "T040_DIA";
            //table.Columns.Add(column2);

            //DataColumn column3 = new DataColumn();
            //column3.DataType = System.Type.GetType("System.Double");
            //column3.ColumnName = "T040_HORAS";
            //table.Columns.Add(column3);
            //// Añado las filas
            //DataRow row;
            //while (dr.Read())
            //{
            //    row = table.NewRow();
            //    row["NUM_EMPLEADO"] = dr["NUM_EMPLEADO"];
            //    row["T040_DIA"] = dr["T040_DIA"];
            //    row["T040_HORAS"] = dr["T040_HORAS"];
            //    table.Rows.Add(row);
            //}
            //ds.Tables.Add(table);
            //dr.Close();
            //dr.Dispose();
            #endregion
            #region Establezco relaciones entre las tablas para poder recorrerlas como maestro-detalle
            //Entre Empleados-Tareas-Previstos y Empleado-Tareas-Consumidos
            //DataColumn[] oColumnPadre, oColumnHijo;
            //oColumnPadre=new DataColumn[] {ds.Tables[0].Columns["NUM_EMPLEADO"],ds.Tables[0].Columns["IDTAREA"]};
            //oColumnHijo = new DataColumn[] { ds.Tables[1].Columns["NUM_EMPLEADO"], ds.Tables[1].Columns["IDTAREA"] };
            //DataRelation drRelacion = new DataRelation("rel1", oColumnPadre, oColumnHijo);
            //ds.Relations.Add(drRelacion);
            //Entre Empleados y Calendario
            DataColumn dcPadre = ds.Tables[2].Columns["t314_idusuario"];
            DataColumn dcHijo = ds.Tables[1].Columns["t314_idusuario"];
            DataRelation drRelacion2 = new DataRelation("rel2", dcPadre, dcHijo);
            ds.Relations.Add(drRelacion2);
            //Entre Empleados y Empleados-Tareas-Previstos-Consumidos
            DataColumn dcHijo2 = ds.Tables[0].Columns["t314_idusuario"];
            DataRelation drRelacion3 = new DataRelation("rel3", dcPadre, dcHijo2);
            ds.Relations.Add(drRelacion3);
            #endregion
            #region calculo el pendiente para cada empleado/tarea (previsto - consumido)
            //idFila = 0;
            //DataRow[] oConsumos;
            //foreach (DataRow oEmpleado in ds.Tables[0].Rows)//Recorro tabla de empleados y tareas
            //{
            //    sTarea = oEmpleado["IDTAREA"].ToString();
            //    nHorasEstimadas = double.Parse(oEmpleado["PREVISTO"].ToString());//Previsto en la tarea
            //    nHorasConsumidas = 0;
            //    nPendiente = nHorasEstimadas;
            //    nHorasCalendario = 0;
            //    if (nHorasEstimadas > 0)
            //    {
            //        oConsumos = oEmpleado.GetChildRows(drRelacion);//Cargo tabla de consumos del empleado/tarea
            //        if (oConsumos.Length == 0)
            //            nHorasConsumidas = 0;
            //        else
            //        {
            //            nHorasConsumidas = double.Parse(oConsumos[0]["CONSUMIDO"].ToString());
            //        }
            //        nPendiente = nHorasEstimadas - nHorasConsumidas;
            //        //Si ya he consumido mas de lo previsto dejo el pendiente a cero
            //        if (nPendiente <= 0) nPendiente = 0;
            //    }
            //    //Actualizo el pendiente de la tarea en la tabla de empleado para luego CALCULAR EL Nº DE DÍAS OCUPADO
            //    ds.Tables[0].Rows[idFila]["PDTE"] = nPendiente;
            //    idFila++;
            //}
            #endregion
            #region Cargo para cada empleado el total de horas pendientes evitando las tareas con pendiente negativo
            DataRow[] oPendientes;
            idFila = 0;
            foreach (DataRow oEmpleado in ds.Tables[2].Rows)
            {
                nPendiente = 0;
                oPendientes = oEmpleado.GetChildRows(drRelacion3);
                for (int i = 0; i < oPendientes.Length; i++)
                {
                    nOcupacion = double.Parse(oPendientes[i]["Pendiente"].ToString());
                    //Los pendientes negativos (consumido > previsto) no los cargo
                    if (nOcupacion>0)
                        nPendiente += nOcupacion;
                }
                ds.Tables[2].Rows[idFila]["PDTE"] = nPendiente;
                idFila++;
            }
            #endregion
            #region calculo el nº de dias que va a estar ocupado en base al total pendiente y el calendario
            DataRow[] oDias;
            idFila = 0;
            foreach (DataRow oEmpleado in ds.Tables[2].Rows)//Recorro tabla de empleados 
            {
                sEmpleado = oEmpleado["t314_idusuario"].ToString();
                sNomEmpleado = oEmpleado["Profesional"].ToString();
                nPendiente = double.Parse(oEmpleado["PDTE"].ToString());//Pendiente total del empleado
                nHorasEstimadas = nPendiente;//Saco de donde restaré las horas de cada día hasta llegar a <=0
                nHorasCalendario = 0;
                iNumDias = 0;
                dAux = DateTime.Today;
                if (nHorasEstimadas > 0)
                {
                    oDias = oEmpleado.GetChildRows(drRelacion2);//Cargo tabla de días del empleado
                    //Si ya he consumido mas de lo previsto en principio estoy libre desde hoy
                    if (nPendiente > 0)
                    {
                        //Voy restando día a día las horas que tengo pendientes para ir avanzando en el calendario
                        nHorasEstimadas = nPendiente;
                        for (int i = 0; i < oDias.Length; i++)//Para cada día del intervalo solicitado
                        {
                            if (oDias[i]["T067_HORAS"].ToString() != "0")
                            {//Si el dia es laborable resto horas
                                nHorasCalendario = double.Parse(oDias[i]["T067_HORAS"].ToString());
                                nHorasEstimadas -= nHorasCalendario;
                                iNumDias++;
                                if (nHorasEstimadas < 0) break;
                            }
                            else iNumDias++;
                            dAux = dAux.AddDays(1);
                        }
                        if (nHorasEstimadas > 0)
                        {
                            //sResul = "Error@#@El profesional " + sEmpleado + "-" + sNomEmpleado +
                            //         "\ntiene tareas cuya duración excede del desglose de calendarios definidos.\n\nNo se puede ejecutar la consulta.";
                            //return sResul;
                            sListaErrores += int.Parse(oEmpleado["t314_idusuario"].ToString()).ToString("#,###") + " " + oEmpleado["Profesional"].ToString() + "\n";
                            //oEmpleado.Delete();
                            //continue;
                        }
                    }
                }
                //Inserto los valores en la tabla de empleado para luego generar el HTML
                //DataRow rowEmp;
                //rowEmp = tableEmp.NewRow();
                //rowEmp["NUM_EMPLEADO"] = sEmpleado;
                //rowEmp["Profesional"] = sEmpleado;
                //rowEmp["HORAS_PDTES"] = nPendiente;
                //if (nPendiente == 0) rowEmp["FECHAHASTA"] = "";
                //else rowEmp["FECHAHASTA"] = dAux.ToShortDateString();
                //rowEmp["NUMDIAS"] = iNumDias;
                //tableEmp.Rows.Add(rowEmp);
                ds.Tables[2].Rows[idFila]["NUMDIAS"] = iNumDias;
                if (nPendiente == 0) ds.Tables[2].Rows[idFila]["FECHAHASTA"] = "";
                else ds.Tables[2].Rows[idFila]["FECHAHASTA"] = dAux.ToShortDateString();

                if (iNumDias > nOcupacionMax) nOcupacionMax = iNumDias;
                idFila++;
            }
            #endregion
            #region Una vez construido el dataset lo recorro con la ordenación deseada y construyo el HTML de la tabla
            idFila = 0;
            DataRow[] oEmpleados;
            switch (nOrden)
            {
                case 2:
                    if (nAscDesc == 0) oEmpleados = ds.Tables[2].Select("", "NUMDIAS ASC");
                    else oEmpleados = ds.Tables[2].Select("", "NUMDIAS DESC");
                    break;
                case 3:
                    if (nAscDesc == 0) oEmpleados = ds.Tables[2].Select("", "PDTE ASC");
                    else oEmpleados = ds.Tables[2].Select("", "PDTE DESC");
                    break;
                default:
                    if (nAscDesc == 0) oEmpleados = ds.Tables[2].Select("", "Profesional ASC");
                    else oEmpleados = ds.Tables[2].Select("", "Profesional DESC");
                    break;
            }
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 960px;'>");
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:300px;' /><col style='width:79px;' /><col style='width:561px;' /></colgroup>");
            sb.Append("<tbody>");
            foreach (DataRow oEmpleado in oEmpleados)//Recorro tabla de empleados
            {
                sEmpleado = oEmpleado["t314_idusuario"].ToString();
                sNomEmpleado = oEmpleado["Profesional"].ToString();
                nPendiente = double.Parse(oEmpleado["PDTE"].ToString());
                nOcupacion = double.Parse(oEmpleado["NUMDIAS"].ToString());
                iLargura = flGetAltura(nOcupacion, nOcupacionMax);
                sb.Append("<tr id=" + idFila.ToString() + " idR=" + sEmpleado + " sNomEmpleado='" + sNomEmpleado);
                //sb.Append("' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sNomEmpleado.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(sEmpleado).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oEmpleado["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + oEmpleado["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + sNomEmpleado.Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(sEmpleado).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oEmpleado["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" style='height:20px'");

                if (oEmpleado["t303_denominacion"].ToString() == "")
                    sb.Append(" tipo ='E'");
                else
                    sb.Append(" tipo ='I'");

                sb.Append(" sexo ='" + oEmpleado["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + oEmpleado["baja"].ToString() + "'");

                sb.Append("><td></td>");

                sb.Append("<td style='padding-left:5px;'><span class='NBR W290'>");
                sb.Append(sNomEmpleado);
                sb.Append("</span></td><td style='padding-right:5px;text-align:right;'>");
                sb.Append(nPendiente.ToString("N"));
                sb.Append("</td><td");
                if (iLargura != 0)
                {
                    sb.Append(" style=\"background-image:url('../../../../../Images/imgGanttBGSemana.gif');\">");
                    sb.Append("<img src='../../../../../Images/imgGanttT.gif' height='13px' width='");
                    sb.Append(iLargura.ToString());
                    sb.Append("px'>&nbsp;");
                    sb.Append(oEmpleado["FECHAHASTA"].ToString());
                }
                else
                {
                    sb.Append(">&nbsp;");
                }
                sb.Append("</td></tr>");
                idFila++;
            }
            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
            #endregion

            sb.Length = 0; //Para liberar memoria   
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de horizonte de ocupación para un profesional", ex);
        }
        return sResul + "@#@" + sListaErrores;
    }
    private int flGetAltura(double nOcupacion, double nOcupMax)
    {
        int iRes = 0;
        double dAux = 0;
        if (nOcupMax == 0) dAux = 0;
        else dAux = (465 * nOcupacion) / nOcupMax;//465 es el nº de pixels que reservamos en la pantalla para dibujar los gráficos
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