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
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "", strNat = "", strNN = "";
    public int nAnno = DateTime.Now.Year;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 47;
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Proyectos improductivos genéricos";
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                this.txtAnnoVisible.Text = nAnno.ToString();
                cargarNodos();
                cargarNaturalezas();
                cargarNaturalezasNodosMarcados();
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

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
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("borrar"):
                sResultado += Borrar();
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
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NODO.CatalogoNodosNaturalezas();
            string sTootTip = "";

            sb.Append("<table id='tblNodos' class='texto MANO' style='width: 380px;'>");
            sb.Append("<colgroup><col style='width:50px;' /><col style='width:30px;' /><col style='width:270px;' /><col style='width:30px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString();

                sb.Append("<tr id=" + dr["t303_idnodo"].ToString() + " style='height:20px;' onclick='ms(this);getNaturalezas(this);'>");
                if ((bool)dr["t303_defectoPIG"]) sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setEstadistica()' checked></td>");
                else sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setEstadistica()'></td>");
                //Si el nodo está parametrizado, lo pongo en rojo
                if (int.Parse(dr["num_naturalezas"].ToString()) > 0)
                {
                    sb.Append("<td style='text-align:right;padding-right:3px; color:red;'>" + dr["t303_idnodo"].ToString() + "</td>");
                    sb.Append("<td style='padding-left:8px'><nobr class='NBR W260' style='noWrap:true; color:red;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                }
                else
                {
                    sb.Append("<td style='text-align:right;padding-right:3px'>" + dr["t303_idnodo"].ToString() + "</td>");
                    sb.Append("<td style='padding-left:8px'><nobr class='NBR W260' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                }
                
                sb.Append("<td style='text-align:right;padding-right:4px'>" + dr["num_naturalezas"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private void cargarNaturalezas()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NATURALEZA.CatalogoPIG();

            //sb.Append("aNat = new Array();\n"); // aNN --> Array de Naturalezas
            /// [0] --> idNaturaleza
            /// [1] --> Denominación
            /// [2] --> meses vigencia
            /// [3] --> idPlantilla
            /// [4] --> replicaPIG
            /// [5] --> hereda nodo
            /// [6] --> imputable GASVI
            int i = 0;
            while (dr.Read())
            {
                sb.Append("aNat[" + i.ToString() + "] = new Array(" + dr["t323_idnaturaleza"].ToString() + ",\"" + dr["t323_denominacion"].ToString() + "\"," + dr["t323_mesesvigenciaPIG"].ToString() + "," + dr["t338_idplantilla"].ToString() + ",");
                
                if ((bool)dr["t323_replicaPIG"]) sb.Append("1,");
                else sb.Append("0,");

                if ((bool)dr["t323_heredanodo_PIG"]) sb.Append("1,");
                else sb.Append("0,");

                if ((bool)dr["t323_imputableGASVI_PIG"]) sb.Append("1");
                else sb.Append("0");

                sb.Append(");\n");

                i++;
            }
            dr.Close();
            dr.Dispose();

            strNat = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar las naturalezas", ex);
        }
    }
    private void cargarNaturalezasNodosMarcados()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("aNN = new Array();\n"); // aNN --> Array de Nodos Naturalezas
            /// [0] --> idNodo
            /// [1] --> idNaturaleza
            /// [2] --> meses vigencia
            /// [3] --> marcado
            /// [4] --> FIV
            /// [5] --> FFV
            /// [6] --> a grabar
            /// [7] --> replicaPIG
            /// [8]

            SqlDataReader dr = NODO.CatalogoNodosNaturalezasMarcados();
            int i = 0;
            while (dr.Read())
            {
                sb.Append("aNN[" + i.ToString() + "] = new Array(" + dr["T303_IDNODO"].ToString() + "," 
                            + dr["T323_IDNATURALEZA"].ToString() + "," 
                            + dr["mesesvigencia"].ToString() + "," 
                            + dr["marcado"].ToString()
                            + ",\"\",\"\",0,"
                            + dr["t471_replicaPIG"].ToString() + ","
                            + dr["t471_heredanodo"].ToString() + ","
                            + dr["t471_imputableGASVI"].ToString() + ","
                            + dr["t314_idusuario_responsable"].ToString() + ",'"
                            + dr["Responsable"].ToString() + "','"
                            + dr["t001_idficepi_visador"].ToString() + "','"
                            + dr["Validador"].ToString() + "',"
                            + "0,'"//Indica si el usuario ha clicado sobre el nodo para cargarlo
                            + dr["Parametrizado"].ToString() //Indica si el nodo se había parametrizado previamente
                            + "');\n");

                i++;
            }
            dr.Close();
            dr.Dispose();
            strNN = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos y naturalezas marcados", ex);
        }
    }

    private string Procesar(string sForzar, string sAnno, string strDatos)
    {
        string sResul = "";
        int nSubnodo = 0, nPE = 0, nPSN = 0;//, nDesde = 0, nHasta = 0, nAux = 0
        Hashtable htSubnodos = new Hashtable();
        Hashtable htNodos = new Hashtable();
        NODO oNodoAux = null;
        int[] nDatosNodo;
        int? idFicepiValidador = null;

        try
        {
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

            string[] aProyectos = Regex.Split(strDatos, "///");
            foreach (string oProyecto in aProyectos)
            {
                if (oProyecto == "") continue;
                string[] aValores = Regex.Split(oProyecto, "##");
                /// aValores[0] = idNodo
                /// aValores[1] = idNaturaleza
                /// aValores[2] = FIV
                /// aValores[3] = FFV
                /// aValores[4] = idPlantilla
                /// aValores[5] = Denominación Naturaleza
                /// aValores[6] = Esreplicable
                /// 
                /// aValores[7] = Hereda Nodo
                /// aValores[8] = Id Usuario responsable
                /// aValores[9] = Imputable GASVI
                /// aValores[10] = Id Ficepi Validador GASVI

                if (sForzar == "N" && PROYECTOSUBNODO.ExistePIG(tr, int.Parse(aValores[0]), int.Parse(aValores[1]), short.Parse(sAnno))) 
                    continue;
                #region Datos de nodo y subnodo
                nDatosNodo = (int[])htSubnodos[int.Parse(aValores[0])];
                if (nDatosNodo == null)
                {
                    int nCountSubnodosManiobra2 = 0;
                    int idSubnodoManiobra2 = 0;
                    int nCountSubnodosNoManiobra = 0;
                    int idSubnodoNoManiobra = 0;

                    DataSet dsSubNodos = SUBNODO.CatalogoActivos(tr, int.Parse(aValores[0]), true);
                    foreach (DataRow oSN in dsSubNodos.Tables[0].Rows)
                    {

                        if ((byte)oSN["t304_maniobra"] == 2)
                        {
                            nCountSubnodosManiobra2++;
                            idSubnodoManiobra2 = (int)oSN["t304_idsubnodo"];
                        }
                        else if ((byte)oSN["t304_maniobra"] == 0)
                        {
                            nCountSubnodosNoManiobra++;
                            idSubnodoNoManiobra = (int)oSN["t304_idsubnodo"];
                        }
                    }
                    dsSubNodos.Dispose();

                    //nSubnodo = SUBNODO.ObtenerSubnodoManiobra2(tr, int.Parse(aValores[0]));
                    NODO oNodo = NODO.Select(tr, int.Parse(aValores[0]));

                    if (nCountSubnodosNoManiobra == 1)
                    {
                        nSubnodo = idSubnodoNoManiobra;
                    }
                    else if (nCountSubnodosManiobra2 >= 1)
                    {
                        nSubnodo = idSubnodoManiobra2;
                    }
                    else
                    {
                        nSubnodo = SUBNODO.Insert(tr, "Improductivos genéricos", int.Parse(aValores[0]), 0, true, 2, oNodo.t314_idusuario_responsable, null);
                    }
                    htSubnodos.Add(int.Parse(aValores[0]), new int[3] {int.Parse(aValores[0]),
                                                                     nSubnodo,
                                                                     oNodo.t314_idusuario_responsable});
                    nDatosNodo = (int[])htSubnodos[int.Parse(aValores[0])];
                }

                
                oNodoAux = (NODO)htNodos[int.Parse(aValores[0])];
                if (oNodoAux == null)
                {
                    oNodoAux = NODO.Select(tr, int.Parse(aValores[0]));
                    htNodos.Add(int.Parse(aValores[0]), oNodoAux);
                }
                #endregion
                string sDenominacion = sAnno + " " + Utilidades.unescape(aValores[5]) + " ("+ oNodoAux.t303_denabreviada +")";
                int nIdClientePIG = CLIENTE.ObtenerClientePIG(tr, int.Parse(aValores[0]));

                nPE = PROYECTO.Insert(tr, "A", sDenominacion.Substring(0, (sDenominacion.Length > 70) ? 70 : sDenominacion.Length), "",
                                        nIdClientePIG, null, null, int.Parse(aValores[1]), 4, DateTime.Parse(aValores[2]),
                                        DateTime.Parse(aValores[3]), "S", "J", "J", short.Parse(sAnno), false, false, 
                                        (aValores[6]=="1")? true:false,null,false,null,null, Constantes.gIdNLO_Defecto);

                //nPSN = PROYECTOSUBNODO.Insert(tr, nPE, nDatosNodo[1], false, "C", true, nDatosNodo[2],
                //                            sAnno + " " + Utilidades.unescape(aValores[5]),
                //                            "X", "X", false, true, false, false, false, "", "", "", null, null, null, null,
                //                            null, null, false, 0);
                //Mikel 30/12/2011. Pongo como seudónimo del subnodo la misma denominación que para el proyecto
                if (aValores[10] != "" && aValores[10] != "null")
                    idFicepiValidador = int.Parse(aValores[10]);
                else
                    idFicepiValidador=null;
                //Mikel 02/02/2016 Los PIG deben llevar el admite recurso PST a cero
                nPSN = PROYECTOSUBNODO.Insert(tr, nPE, nDatosNodo[1], false, "C", (aValores[7] == "1") ? true : false,
                                            int.Parse(aValores[8]),//nDatosNodo[2],//Id Usuario Responsable
                                            sDenominacion.Substring(0, (sDenominacion.Length > 70) ? 70 : sDenominacion.Length),
                                            "X", "X", (aValores[9] == "1") ? true : false, //Imputable GASVI
                                            false, //Admite recurso PST
                                            false, false, false, "", "", "", null, null, null, null, null,
                                            idFicepiValidador, //Id Ficepi Validador GASVI
                                            false, 0);

                //A falta de tener en cuenta si la naturaleza tiene plantilla.
                TAREAPSP.UpdateVigenciaByPSN(tr, nPSN, DateTime.Parse(aValores[2]), DateTime.Parse(aValores[3]));

                #region Grabación de plantilla
                if (aValores[4] != "0")
                {
                    //Hay que grabar la plantilla de PE.
                    int iPos, iMargen, iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iHito = -1, iAux = -1, iOrden = 0, idItemHitoPl;
                    double fDuracion;
                    decimal fPresupuesto;
                    string sTipo, sDesc, sFiniPL, sFfinPL, sFiniV, sFfinV, sAux, sAvisos, sIdTareaPL, sCad;
                    bool bFacturable, bObligaEst, bAvanceAutomatico, bEstadoTarea;
                    ArrayList alTareas = new ArrayList();

                    PROYECTOSUBNODO.BorrarPTByPSN(tr, nPSN);

                    #region 1º Se insertan las filas de la estructura
                    SqlDataReader dr = PlantTarea.Catalogo(int.Parse(aValores[4]));
                    while (dr.Read())
                    {
                        sTipo = dr["Tipo"].ToString();
                        if (sTipo == "H")
                        {
                            sTipo = "HT";
                        }
                        sDesc = Utilidades.escape(dr["Nombre"].ToString());
                        iMargen = int.Parse(dr["margen"].ToString());

                        //Si la linea es de hito compruebo si el hito es de tarea o no para actualizar la variable iTarea
                        if (sTipo == "HT" || sTipo == "HM" || sTipo == "HF")
                        {
                            switch (iMargen)
                            {
                                case 80://es un hito de tarea por lo que mantengo el código de tarea
                                    break;
                                case 60://es un hito de fase y actividad o de tarea con actividad sin fase
                                    if (iFase != -1) iTarea = -1;
                                    break;
                                case 40://es un hito de fase o de tarea sin actividad ni fase o de actividad sin fase
                                    if (iFase != -1)
                                    {
                                        iTarea = -1;
                                        iActiv = -1;
                                    }
                                    else
                                    {
                                        if (iActiv != -1)
                                        {
                                            iTarea = -1;
                                        }
                                    }
                                    break;
                                case 20://es un hito proyecto técnico
                                case 0://es un hito de proyecto económico
                                    iTarea = -1;
                                    iActiv = -1;
                                    iFase = -1;
                                    break;
                            }

                        }

                        fDuracion = 0;
                        sFiniPL = ""; //¿alguno es obligatorio?
                        sFfinPL = "";
                        sFiniV = Fechas.primerDiaMes(DateTime.Today).ToShortDateString();
                        sFfinV = "";
                        fPresupuesto = 0;
                        sIdTareaPL = dr["t339_iditems"].ToString();

                        bFacturable = (bool)dr["t339_facturable"];

                        //if (sEstado != "D") 
                        iOrden++;
                        //iOrden = int.Parse(aElem[8]);
                        //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                        switch (sTipo)
                        {
                            case "P":
                                iPT = -1;
                                iFase = -1;
                                iActiv = -1;
                                break;
                            case "F":
                                iFase = -1;
                                iActiv = -1;
                                break;
                            case "A":
                                iActiv = -1;
                                if (iMargen != 40)
                                    iFase = -1;
                                break;
                            case "T":
                                iTarea = -1;
                                if (iMargen == 40)
                                {
                                    iFase = -1;
                                }
                                else
                                {
                                    if (iMargen != 60)
                                    {
                                        iFase = -1;
                                        iActiv = -1;
                                    }
                                }
                                break;
                            case "HT":
                            case "HF":
                            case "HM":
                                iHito = -1;//int.Parse(aElem[7]);
                                break;
                        }

                        bObligaEst = (bool)dr["obliga"];
                        bAvanceAutomatico = (bool)dr["avance"];
                        sAux = EstrProy.Insertar(tr, int.Parse(aValores[0]), nPE, nPSN, sTipo, sDesc, iPT, iFase, iActiv, iMargen, iOrden,
                                                 sFiniPL, sFfinPL, fDuracion, sFiniV, sFfinV, fPresupuesto,
                                                 bFacturable, bObligaEst, bAvanceAutomatico, "1", "", 0);

                        iPos = sAux.IndexOf("##");
                        iAux = int.Parse(sAux.Substring(0, iPos));
                        sAvisos = sAux.Substring(iPos + 2);

                        switch (sTipo)
                        {
                            case "P": iPT = iAux; break;
                            case "F": iFase = iAux; break;
                            case "A": iActiv = iAux; break;
                            case "T":
                                iTarea = iAux;
                                if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                {
                                    string[] aDatosAux = new string[] { sIdTareaPL, iAux.ToString() };
                                    alTareas.Add(aDatosAux);
                                    //Grabo los atributos estadísticos provenientes de la plantilla. iAux=código de tarea
                                    TAREAPSP.InsertarAE(tr, int.Parse(sIdTareaPL), iAux);
                                }

                                //Hay que guardar las tareas que quedan pendientes, ya que luego hay que actualizar el estado en pantalla
                                bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, short.Parse(aValores[0]), iAux);
                                if (bEstadoTarea)
                                {
                                    //actualizo el estado de la tarea
                                    TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFiniPL, sFfinPL, fDuracion, sFiniV,
                                                       sFfinV, (int)Session["UsuarioActual"], fPresupuesto, 2, bFacturable);
                                    //sAvisos = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                                    //if (sTareasPendientes == "") sTareasPendientes = iAux.ToString();
                                    //else sTareasPendientes += "//"+ iAux.ToString();
                                }
                                break;
                            case "HT":
                                iHito = iAux;
                                break;
                        }
                        if (sTipo.Substring(0, 1) == "H")
                        {
                            AsociarTareasHitos(tr, nPSN, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                        }
                    }
                    dr.Close();
                    dr.Dispose();
                    #endregion

                    #region 2º Se insertan las filas de los hitos de cumplimiento discontinuo
                    dr = PlantTarea.CatalogoHitos(int.Parse(aValores[4]));
                    while (dr.Read())
                    {
                        sTipo = "HM";
                        sDesc = dr["t369_deshito"].ToString();
                        idItemHitoPl = (int)dr["t369_idhito"];
                        iOrden = int.Parse(dr["t369_orden"].ToString());

                        sAux = EstrProy.Insertar(tr, int.Parse(aValores[0]), nPE, nPSN, sTipo, sDesc, 0, 0, 0, 0, iOrden, "", "", 0, "", "", 0, false, false, false, "1", "", 0);
                        iPos = sAux.IndexOf("##");
                        iAux = int.Parse(sAux.Substring(0, iPos));
                        sAvisos = sAux.Substring(iPos + 2);
                        //Si es hito de cumplimiento discontinuo y se ha cargado desde plantilla hay que grabar sus tareas
                        if (sTipo == "HM")
                        {
                            if (idItemHitoPl > 0)
                            {
                                //Recojo las tareas de plantilla del código de hito en plantilla
                                sCad = HITOE_PLANT.fgListaTareasPlantilla(tr, idItemHitoPl);
                                string[] aElems2 = Regex.Split(sCad, @"##");
                                for (int j = 0; j < aElems2.Length; j++)
                                {
                                    sIdTareaPL = aElems2[j];
                                    if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                    {
                                        //Identifico el código de tarea real asociado al codigo de tarea de plantilla
                                        for (int n = 0; n < alTareas.Count; n++)
                                        {
                                            if (((string[])alTareas[n])[0] == sIdTareaPL)
                                            {//Inserto la tarea del hito
                                                sCad = ((string[])alTareas[n])[1];
                                                iTarea = int.Parse(sCad);
                                                EstrProy.InsertarTareaHito(tr, iAux, iTarea);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }//while
                    dr.Close();
                    dr.Dispose();
                    #endregion
                }
                #endregion
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al crear los proyectos improductivos genéricos.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string Borrar()
    {
        string sResul = "";
        try
        {
            NODO_NATURALEZA.DeleteAll(null);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar la parametrización de nodos-naturaleza.", ex, false);
        }
        return sResul;
    }

    private void AsociarTareasHitos(SqlTransaction tr, int nT305IdProy, int nPT, int nFase, int nActiv, int nTarea, int nHito, int nMargen)
    {
        string sTipo = "";
        int nCodigo = -1;

        try
        {
            #region 1º Identificar el tipo de Hito
            if (nMargen == 0)
            {
                sTipo = "PE"; //Hito de proyecto económico
                nCodigo = nT305IdProy;
            }
            else
            {
                if (nTarea != -1)
                {
                    sTipo = "T"; //Hito de tarea
                    nCodigo = nTarea;
                }
                else
                {
                    if (nPT != -1 && nFase == -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "PT"; //Hito de proyecto técnico
                        nCodigo = nPT;
                    }
                    else if (nFase != -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "F"; //Hito de Fase
                        nCodigo = nFase;
                    }
                    else if (nActiv != -1)// && nTarea == -1
                    {
                        sTipo = "A"; //Hito de Actividad
                        nCodigo = nActiv;
                    }
                }
            }
            #endregion

            ////2º Se borran las tareas que pudiera haber ligadas al hito
            //EstrProy.BorrarTareasHito(tr, nHito);

            //3º Se asocian al hito las tareas que correspondan al "tipo" (nivel) del hito.
            //EstrProy.AsociarTareasHito(tr, sTipo, nHito, nCodigo, short.Parse(Session["NodoActivo"].ToString()));
            EstrProy.AsociarTareasHito(tr, sTipo, nHito, nCodigo);
        }
        catch (Exception ex)
        {
            //Conexion.CerrarTransaccion(tr);
            Errores.mostrarError("Error al grabar las tareas asociadas a los hitos", ex);
        }
    }

}
