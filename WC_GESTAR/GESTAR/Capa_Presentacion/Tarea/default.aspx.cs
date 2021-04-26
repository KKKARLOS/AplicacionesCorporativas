using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;
using System.Text.RegularExpressions;
using EO.Web;
using Microsoft.JScript;
namespace GESTAR.Capa_Presentacion.ASPX
{
    /// <summary>
    /// Descripción breve de main2.
    /// </summary>
    /// 

    public partial class DetalleTarea : System.Web.UI.Page, ICallbackEventHandler
    {
    	protected string strTitulo;
        private string _callbackResultado = null;
        public SqlConnection oConn;
        public SqlTransaction tr;
        SqlDataReader dr = null;
        private int intContador = 0;
        public string strTablaHtmlCatalogo;
        public string strTablaHtmlSeleccionados;

        // Ordena = campo de la select  TipoOrden = 1 asc 2 desc
            
        //public DetalleTarea()
        //{
        //    this.Title = "Detalle de la tarea";

        //    ArrayList aFuncionesJavaScript = new ArrayList();
        //    aFuncionesJavaScript.Add("Capa_Presentacion/Defiencias/Functions/funciones.js");
        //    aFuncionesJavaScript.Add("JavaScript/funciones.js");
        //    this.FuncionesJavaScript = aFuncionesJavaScript;
        //}

        protected void Page_Load(object sender, System.EventArgs e)
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
                    //if (Request.QueryString["bNueva"] == "false")
                    //    strTitulo = "Modificación de la tarea";
                    //else
                    //    strTitulo = "Creación de una nueva tarea";

                    strTablaHtmlCatalogo = "<table id='tblCatalogo' class='texto' style='width: 390px' cellSpacing='0' cellpadding='0' align='left' border='0'></table>";
                    strTablaHtmlSeleccionados = "<table id='tblSeleccionados' class='texto' style='width: 390px' cellSpacing='0' cellpadding='0' align='left' border='0'></table>";

                    strTitulo = "Detalle tarea";

                    cboAvance.Items.Insert(0, new ListItem("", "0"));
                    cboAvance.Items[0].Selected = true;

                    cboRtado.Items.Insert(0, new ListItem("", "0"));
                    cboRtado.Items[0].Selected = true;

                    hdnIDArea.Text = Request.QueryString["IDAREA"].ToString();
                    hdnIDDefi.Text = Request.QueryString["IDDEFICIENCIA"].ToString();
                    txtDeficiencia.Text = Request.QueryString["DEFICIENCIA"].ToString();
                    txtArea.Text = Request.QueryString["AREA"];
                    hdnModoLectura.Text = Session["MODOLECTURA"].ToString();
                    hdnEstado.Text = Request.QueryString["ESTADO"];
                    //Si anulada o aprobada ponemos en modo lectura
                    if (hdnEstado.Text == "12" || hdnEstado.Text == "11") hdnModoLectura.Text = "1";

                    if (Request.QueryString["bNueva"].ToString() == "false")
                    {
                        #region Modificar tarea
                        #region Carga datos Tarea
                        hdnIDTarea.Text = Request.QueryString["IDTAREA"].ToString();
                        dr = null;
                        dr = TAREA.Select(null, short.Parse(Request.QueryString["IDTAREA"]));
                        if (dr.Read())
                        {
                            txtDenominacion.Text = (string)dr["T072_DENOMINACION"];

                            if (dr["T072_FECINIPREV"] == System.DBNull.Value)
                                txtFechaInicioPrevista.Text = "";
                            else
                                txtFechaInicioPrevista.Text = ((DateTime)dr["T072_FECINIPREV"]).ToShortDateString();

                            if (dr["T072_FECINIREAL"] == System.DBNull.Value)
                                txtFechaInicioReal.Text = "";
                            else
                                txtFechaInicioReal.Text = ((DateTime)dr["T072_FECINIREAL"]).ToShortDateString();

                            if (dr["T072_FECFINPREV"] == System.DBNull.Value)
                                txtFechaFinPrevista.Text = "";
                            else
                                txtFechaFinPrevista.Text = ((DateTime)dr["T072_FECFINPREV"]).ToShortDateString();

                            if (dr["T072_FECFINREAL"] == System.DBNull.Value)
                                txtFechaFinReal.Text = "";
                            else
                                txtFechaFinReal.Text = ((DateTime)dr["T072_FECFINREAL"]).ToShortDateString();

                            txtDescripcion.Text = (string)dr["T072_DESCRIPCION"];

                            txtIdTarea.Text = dr["T072_IDTAREA"].ToString();
                            txtCausas.Text = (string)dr["T072_CAUSA"];
                            txtIntervenciones.Text = (string)dr["T072_INTERVENCION"];
                            txtConsideraciones.Text = (string)dr["T072_CONSIDERACION"];
                            cboRtado.SelectedValue = dr["T072_RESULTADO"].ToString();
                            cboAvance.SelectedValue = dr["T072_AVANCE"].ToString();
                            hdnAvanceIn.Text = dr["T072_AVANCE"].ToString();
                        }
                        dr.Close();
                        dr.Dispose();
                        dr = null;
                        #endregion
                        #region Solapa Especialistas

                        hdnEsCoordinador.Text = Request.QueryString["ES_COORDINADOR"].ToString();

                        strTablaHtmlCatalogo = CargarEspecialistas(int.Parse(hdnIDArea.Text));
                        string[] aTablaHtmlCatalogo = Regex.Split(strTablaHtmlCatalogo, @"##");

                        strTablaHtmlCatalogo = aTablaHtmlCatalogo[0];
                        hdnEspecialistas.Text = aTablaHtmlCatalogo[1];
                        #endregion
                        if (hdnEsCoordinador.Text == "true") this.hdnBusqueda.Text = "S";
      
                        strTablaHtmlSeleccionados = CargarEspecialistasSeleccionados(int.Parse(txtIdTarea.Text));

                        hdnAdmin.Text = Request.QueryString["ADMIN"];

                        int intCoordinador = (Request.QueryString["COORDINADOR"].ToString() == "") ? 0 : int.Parse(Request.QueryString["COORDINADOR"].ToString());

                        if  (
                             (
                                (hdnModoLectura.Text == "1" || hdnEsTecnico.Text == "true") 
                                && 
                                (hdnAdmin.Text != "A" && hdnEsCoordinador.Text != "true" || (hdnEsCoordinador.Text == "true" && intCoordinador != int.Parse(Session["IDFICEPI"].ToString())
                                )
                              )
                                ) 
                              || 
                              (hdnEstado.Text == "2" || hdnEstado.Text == "4" || hdnEstado.Text == "7" || hdnEstado.Text == "9" || hdnEstado.Text == "12" || hdnEstado.Text == "11")
                              ||
                              (hdnEsCoordinador.Text == "false" && hdnEsTecnico.Text == "false") 
                            ) 
                        {
                            System.Web.UI.Control Area = this.FindControl("frmDatos");
                            ModoLectura.Poner(Area.Controls);
                            //if (hdnEsTecnico.Text == "true" && (hdnEstado.Text != "12" && hdnEstado.Text != "11"))
                            if (hdnEsTecnico.Text == "true" && hdnEstado.Text == "8" )
                            {
                                Habilitar_campos_Tecnico();
                                hdnModoLectura.Text = "0";
                            }
                            if ((hdnEsCoordinador.Text == "false" && hdnEsTecnico.Text == "false") || (hdnEsCoordinador.Text == "false" && hdnEsTecnico.Text == "true" && hdnEstado.Text != "8") || (hdnEsCoordinador.Text == "true" && (hdnEstado.Text == "0" || hdnEstado.Text == "2" || hdnEstado.Text == "4" || hdnEstado.Text == "7" || hdnEstado.Text == "9")))
                            {
                                hdnModoLectura.Text = "1";
                            }
                        }
                        //12/05/2016 Por petición de Víctor solo podrán modificar fechas de tarea los coordinadores
                        //y en Órdenes(deficiencias) cuyos estados sean Tramitada, Aceptada, Es estudio, Pte. de resolución y En resolución
                        if (hdnModoLectura.Text == "0" && hdnEsCoordinador.Text == "true")
                        {
                            if (FechaModificable(hdnEstado.Text))
                                PonerEventosFecha();
                        }
                        #endregion
                    }
                    else
                    {
                        PonerEventosFecha();
                        hdnEsCoordinador.Text = "true";
                        strTablaHtmlCatalogo = CargarEspecialistas(int.Parse(hdnIDArea.Text));
                        string[] aTablaHtmlCatalogo = Regex.Split(strTablaHtmlCatalogo, @"##");

                        strTablaHtmlCatalogo = aTablaHtmlCatalogo[0];
                        hdnEspecialistas.Text = aTablaHtmlCatalogo[1];
                    }

                    this.txtArea.Attributes.Add("readonly", "readonly");
                    this.txtDeficiencia.Attributes.Add("readonly", "readonly");
                    //this.txtDenominacion.Attributes.Add("readonly", "readonly");
                    this.txtIdTarea.Attributes.Add("readonly", "readonly");
                }
                catch (Exception ex)
                {
                    hdnErrores.Text = Errores.mostrarError("Error al obtener los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context");
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }      
        }

        private void Habilitar_campos_Tecnico()
        {
            //if (hdnEstado.Text != "3") return;

            txtCausas.ReadOnly = false;
            txtIntervenciones.ReadOnly = false;
            txtConsideraciones.ReadOnly = false;
            cboAvance.Enabled = true;
        }
        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        public void RaiseCallbackEvent(string eventArg)
        {
            //1º Si hubiera argumentos, se recogen y tratan.
            string[] aArgs = Regex.Split(eventArg, @"@@");

            //2º Aquí realizaríamos el acceso a BD, etc,...


            System.Text.StringBuilder strbTabla = new System.Text.StringBuilder();
            strbTabla.Length = 0;

            switch (aArgs[0])
            {
                case "recursos": // OBTENER RECURSOS
                    strbTabla.Append(ObtenerRecursos(Microsoft.JScript.GlobalObject.unescape(aArgs[1]), Microsoft.JScript.GlobalObject.unescape(aArgs[2]), Microsoft.JScript.GlobalObject.unescape(aArgs[3]), int.Parse(aArgs[4]), int.Parse(aArgs[5])));
                    break;

                case "especialistas_del_area_tarea": // OBTENER ESPECIALISTAS DEL ÁREA Y DE LA TAREA
                    strbTabla.Append(CargarEspecialistas(int.Parse(hdnIDArea.Text)) + "##" + CargarEspecialistasSeleccionados(int.Parse(aArgs[1])));
                    break;

                case "grabar":
                    //  Argumentos:
                    //  Denominacion(1), Descripcion(2), FechaInicioPrevista(3), FechaFinPrevista(4), FechaInicioReal(5), FechaFinReal(6), Causas(7), 
                    //	Intervenciones(8), Consideraciones(9), Avance(10), Rtdo(11)

                    strbTabla.Append(Grabar
                        (
                        byte.Parse(aArgs[1]),
                        Microsoft.JScript.GlobalObject.unescape(aArgs[2]),  //  Denominacion
                        Microsoft.JScript.GlobalObject.unescape(aArgs[3]),  //  Descripcion
                        Microsoft.JScript.GlobalObject.unescape(aArgs[4]),  //  FechaInicioPrevista
                        Microsoft.JScript.GlobalObject.unescape(aArgs[5]),  //  FechaFinPrevista
                        Microsoft.JScript.GlobalObject.unescape(aArgs[6]),  //  FechaInicioReal
                        Microsoft.JScript.GlobalObject.unescape(aArgs[7]),  //  FechaFinReal
                        Microsoft.JScript.GlobalObject.unescape(aArgs[8]),  //  Causas
                        Microsoft.JScript.GlobalObject.unescape(aArgs[9]),  //  Intervenciones
                        Microsoft.JScript.GlobalObject.unescape(aArgs[10]),  //  Consideraciones
                        short.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[11])),    //  Avance
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[12])),      //  Rtdo
                        Microsoft.JScript.GlobalObject.unescape(aArgs[13]),  //  especialistas modificados (S/N)
                        Microsoft.JScript.GlobalObject.unescape(aArgs[14]),  //  especialistas a grabar en tareas_usuario
                        Microsoft.JScript.GlobalObject.unescape(aArgs[15]),   // //  especialistas a grabar en Áreas_usuario
                        Microsoft.JScript.GlobalObject.unescape(aArgs[16]),   // //  IDTarea
                        short.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[17])) //  Avance Old
                        ));
                    break;
            }

            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            try
            {
                if (strbTabla.ToString().Substring(0, 1) != "N") _callbackResultado = aArgs[0] + "@@OK@@" + strbTabla.ToString();
                else _callbackResultado = aArgs[0] + "@@" + strbTabla.ToString();
            }
            catch
            {
                _callbackResultado = aArgs[0] + "@@OK@@" + intContador.ToString(); //; //
            }

        }
        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }
        protected string ObtenerRecursos(string strApellido1, string strApellido2, string strNombre, int intColuma, int intOrden)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();
                dr = null;
                dr = Recursos.CargarRecursos(strApellido1, strApellido2, strNombre, intColuma, intOrden);
                sbuilder.Append("<table id='tblCatalogo' align='left' style='width:390px'>" + (char)13);
                int i = 0;

                while (dr.Read())
                {
                    sbuilder.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + "' ");

                    //if (i % 2 == 0)
                    //    sbuilder.Append("class='FA' ");
                    //else
                    //    sbuilder.Append("class='FB' ");

                    sbuilder.Append(" onclick=mm(event); ");
                    sbuilder.Append(" ondblclick=this.className='FS';anadirSeleccionados(); ");

                    sbuilder.Append(" style='cursor: pointer;height:16px'><td");

                    if (i == 0) sbuilder.Append(" width='100%'");
                    sbuilder.Append(">&nbsp;&nbsp;" + dr["TECNICO"].ToString() + "</td></tr>" + (char)13);
                    i++;
                }

                dr.Close();
                dr.Dispose();

                sbuilder.Append("</table>");
                return sbuilder.ToString();
            }
            catch (System.Exception objError)
            {
                return "N@@" + Errores.mostrarError("Error al leer catálogo de recursos.", objError);
            }
        }
        protected string CargarEspecialistas(int intIdArea)
        {
            try
            {
                dr = null;
                dr = Areas.LeerTecnicosArea(intIdArea);

                int i = 0;
                System.Text.StringBuilder strBuilderEspecialistas = new System.Text.StringBuilder();

                strBuilderEspecialistas.Append("<table id='tblCatalogo' style='width: 390px'>" + (char)13);
                strBuilderEspecialistas.Append("<colgroup><col style='width:100%' /></colgroup>");
                string strCadena = "";

                while (dr.Read())
                {
                    strBuilderEspecialistas.Append("<tr id='" + dr["ID"].ToString() + "/" + dr["T001_CODRED"].ToString() + "' ");
                    strCadena += dr["ID"].ToString() + "/" + dr["T001_CODRED"].ToString() + ",";

                    //if (i % 2 == 0)
                    //    strBuilderEspecialistas.Append("class='FA' ");
                    //else
                    //    strBuilderEspecialistas.Append("class='FB' ");

                    i++;
                    strBuilderEspecialistas.Append(" onclick=mm(event); ");
                    strBuilderEspecialistas.Append(" ondblclick=this.className='FS';anadirSeleccionados(); ");
                    strBuilderEspecialistas.Append(" style='cursor: pointer;height:16px'><td>&nbsp;&nbsp;" + dr["DESCRIPCION"].ToString());
                    strBuilderEspecialistas.Append("</td></tr>" + (char)13);
                }
                if (strCadena != "") strCadena = strCadena.Substring(0, strCadena.Length - 1);

                dr.Close();
                dr.Dispose();

                strBuilderEspecialistas.Append("</table>##" + strCadena);
                return strBuilderEspecialistas.ToString();
            }
            catch (System.Exception objError)
            {
                return "N@@" + Errores.mostrarError("Error al leer catálogo de recursos.", objError);
            }
        }
        protected string CargarEspecialistasSeleccionados(int intIdTarea)
        {
            try
            {
                dr = null;
                dr = TAREA.LeerTecnicosTarea(intIdTarea);

                int i = 0;
                System.Text.StringBuilder strBuilderEspecialistasSel = new System.Text.StringBuilder();

                strBuilderEspecialistasSel.Append("<table  id='tblSeleccionados' style='width: 390px'>" + (char)13);
                strBuilderEspecialistasSel.Append("<colgroup><col style='width:100%' /></colgroup>");

                while (dr.Read())
                {
                    hdnCorreoEspecialistaIn.Text += dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + ',';

                    if (dr["T001_IDFICEPI"].ToString() == Session["IDFICEPI"].ToString())
                        hdnEsTecnico.Text = "true";

                    strBuilderEspecialistasSel.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + "' ");

                    //if (i % 2 == 0)
                    //    strBuilderEspecialistasSel.Append("class='FA' ");
                    //else
                    //    strBuilderEspecialistasSel.Append("class='FB' ");

                    i++;
                    strBuilderEspecialistasSel.Append(" onclick=mm(event); ");
                    strBuilderEspecialistasSel.Append(" ondblclick=this.className='FS';quitarSeleccionados(); ");
                    strBuilderEspecialistasSel.Append(" style='cursor: pointer;height:14px'><td>&nbsp;&nbsp;" + dr["TECNICO"].ToString());
                    strBuilderEspecialistasSel.Append("</td></tr>" + (char)13);
                }

                dr.Close();
                dr.Dispose();

                strBuilderEspecialistasSel.Append("</table>");
                return strBuilderEspecialistasSel.ToString();
            }
            catch (System.Exception objError)
            {
                return "N@@" + Errores.mostrarError("Error al leer catálogo de recursos.", objError);
            }

        }
        private string Grabar(byte byteNueva, string sDenominacion, string sDescripcion, string sFechaInicioPrevista,
            string sFechaFinPrevista, string sFechaInicioReal, string sFechaFinReal, string sCausas,
            string sIntervenciones, string sConsideraciones, short shAvance, byte byRtdo, string sRecursos,
            string sEspecialistas, string sEspecialistasAreas, string sIDTarea, short shAvanceOld)
        {
            string sResul;
            if (sIDTarea == "-1") sIDTarea = hdnIDTarea.Text; ;

            try
            {
                oConn = GESTAR.Capa_Negocio.Conexion.Abrir();
                tr = GESTAR.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
                return "N@@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }

            DateTime? dFechaInicioPrevista = null;
            if (sFechaInicioPrevista != "") dFechaInicioPrevista = DateTime.Parse(sFechaInicioPrevista);

            DateTime? dFechaFinPrevista = null;
            if (sFechaFinPrevista != "") dFechaFinPrevista = DateTime.Parse(sFechaFinPrevista);

            DateTime? dFechaInicioReal = null;
            if (sFechaInicioReal != "") dFechaInicioReal = DateTime.Parse(sFechaInicioReal);

            DateTime? dFechaFinReal = null;
            if (sFechaFinReal != "") dFechaFinReal = DateTime.Parse(sFechaFinReal);

            if (byteNueva == 0)
            {
                try
                {
                    TAREA.Update(tr, int.Parse(sIDTarea), int.Parse(hdnIDDefi.Text), 
                                                sDenominacion, sDescripcion, dFechaInicioPrevista,
                                                dFechaFinPrevista, dFechaInicioReal, dFechaFinReal,
                                                sCausas, sIntervenciones, sConsideraciones, shAvance, byRtdo,
                                                int.Parse(Session["IDFICEPI"].ToString()));
                    intContador = int.Parse(sIDTarea);
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al modificar la tarea.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }
            else
            {
                try
                {
                    intContador = TAREA.Insert(tr, int.Parse(hdnIDDefi.Text), sDenominacion,
                                                sDescripcion, dFechaInicioPrevista,
                                                dFechaFinPrevista, dFechaInicioReal, dFechaFinReal,
                                                sCausas, sIntervenciones, sConsideraciones, shAvance, byRtdo,
                                                int.Parse(Session["IDFICEPI"].ToString()));
                    hdnIDTarea.Text = intContador.ToString();
                    sIDTarea = hdnIDTarea.Text;
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al crear la tarea.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }

            if (sRecursos == "S")
            {
                try
                {
                    TAREA_USUARIO.Delete(tr, int.Parse(sIDTarea));
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al borrar los especialistas.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }

                string[] aEspecialistas = Regex.Split(sEspecialistas, ",");

                for (int j = 0; j < aEspecialistas.Length; j++)
                {
                    if (aEspecialistas[j] == "") continue;

                    try
                    {
                        string[] aID = Regex.Split(aEspecialistas[j], "/");
                        TAREA_USUARIO.Insert(tr, int.Parse(sIDTarea), int.Parse(aID[0]));
                    }
                    catch (System.Exception objError)
                    {
                        sResul = Errores.mostrarError("Error al insertar los especialistas.", objError);
                        GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                        return "N@@" + sResul;
                    }
                }
            }
            
            string[] aEspecialistasAreas = Regex.Split(sEspecialistasAreas, ",");

            for (int j = 0; j < aEspecialistasAreas.Length; j++)
            {
                if (aEspecialistasAreas[j] == "") continue;
                try
                {
                    string[] aID = Regex.Split(aEspecialistasAreas[j], "/");
                    Integrante.Insertar(tr, int.Parse(hdnIDArea.Text), int.Parse(aID[0]), "5");
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al insertar los Especialistas-Areas.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }
            
            try
            {
                GESTAR.Capa_Negocio.Conexion.CommitTransaccion(tr);

                sResul = "";
            }
            catch (Exception ex)
            {
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                sResul = "N@@" + Errores.mostrarError("Error al grabar los datos ( commit )", ex);
            }
            finally
            {
                GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
            }

            string strMensaje = "";
            string strAsunto = "";
            string strTO;

            string[] aTecnicosIn = Regex.Split(this.hdnCorreoEspecialistaIn.Text, ",");
            string[] aTecnicosOut = Regex.Split(sEspecialistas, ",");

            // CORREO (TECNICOS) BAJAS, ALTAS CUANDO EL ESTADO DE LA DEFICIENCIA ESTÁ EN RESOLUCIÓN
            if (hdnEstado.Text == "8")
            {

                // NOTIFICACION BAJAS

                int intEncontrado;

                for (int j = 0; j < aTecnicosIn.Length; j++)
                {
                    if (aTecnicosIn[j] == "") continue;
                    intEncontrado = 0;

                    for (int i = 0; i < aTecnicosOut.Length; i++)
                    {
                        if (aTecnicosIn[j] == aTecnicosOut[i])
                        {
                            intEncontrado = 1;
                            break;
                        }
                    }

                    if (intEncontrado == 0)
                    {
                        strAsunto = "Desasignación de tarea";
                        strMensaje = Session["NOMBRE"] + @" le ha desasignado de una tarea.</p><br><br><br><br>";

                        strMensaje += @"<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Área:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + Request.QueryString["AREA"].ToString() + "<BR><BR></td><tr>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Orden:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + Request.QueryString["IDDEFICIENCIA"].ToString() + "-" + Request.QueryString["DEFICIENCIA"].ToString() + "<BR><BR></td><tr>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Tarea:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + sIDTarea + "-" + sDenominacion + "</td><tr>";
                        strMensaje += @"</table>";

                        string[] aID = Regex.Split(aTecnicosIn[j], "/");
                        //strAsunto += " Usuario ( " + aID[0] + "/" + aID[1] +" ) NOTIFICACION BAJAS";

                        //CASO BAJA

                        if (aID[1].Trim() == "")
                        {
                            strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aID[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                            strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                            Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                            strTO = "EDA";
                        }
                        else
                        {
                            strTO = aID[1];
                        }

                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                    }
                }

                // NOTIFICACION ALTAS

                for (int j = 0; j < aTecnicosOut.Length; j++)
                {
                    if (aTecnicosOut[j] == "") continue;
                    intEncontrado = 0;

                    for (int i = 0; i < aTecnicosIn.Length; i++)
                    {
                        if (aTecnicosOut[j] == aTecnicosIn[i])
                        {
                            intEncontrado = 1;
                            break;
                        }
                    }
                    if (intEncontrado == 0)
                    {
                        strAsunto = "Asignación de tarea";
                        strMensaje = @"<p>" + Session["NOMBRE"] + " le ha asignado como <LABEL class='TITULO'>ESPECIALISTA</LABEL></p><br><br><br><br>";

                        strMensaje += @"<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Área:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + Request.QueryString["AREA"].ToString() + "<BR><BR></td><tr>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Orden:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + Request.QueryString["IDDEFICIENCIA"].ToString() + "-" + Request.QueryString["DEFICIENCIA"].ToString() + "<BR><BR></td><tr>";
                        strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Tarea:</LABEL></td>";
                        strMensaje += @"<td width='85%' valign='top'>" + sIDTarea + "-" + sDenominacion + "</td><tr>";
                        strMensaje += @"</table>";

                        string[] aID = Regex.Split(aTecnicosOut[j], "/");
                        //strAsunto += " Usuario ( " + aID[0] + "/" + aID[1] +" ) NOTIFICACION ALTAS";

                        //CASO ALTA

                        if (aID[1].Trim() == "")
                        {
                            strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aID[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                            strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                            Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                            strTO = "EDA";
                        }
                        else
                        {
                            strTO = aID[1];
                        }
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                    }
                }
                if (shAvance != shAvanceOld)
                {
                    string[] aID = Regex.Split(Request.QueryString["CORREOCOORDINADOR"].ToString(), "/");

                    strAsunto = "Avance de tarea";

                    cboAvance.SelectedValue = shAvance.ToString();
                    strMensaje = Session["NOMBRE"] + @" ha modificado el % de avance.<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='90%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='20%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"] + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["IDDEFICIENCIA"].ToString() + "-" + Request.QueryString["DEFICIENCIA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";

                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Tarea:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sIDTarea + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";

                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Avance:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";

                    if (shAvance != 0) strMensaje += cboAvance.SelectedItem.Text;

                    strMensaje += @"<br><br></td></tr>";

                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Causas:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sCausas.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>") + "<br><br>"; 
                    strMensaje += @"</td></tr>";

                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Intervenciones:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sIntervenciones.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>") + "<br><br>";
                    strMensaje += @"</td></tr>";

                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Consideraciones - Comentarios:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sConsideraciones.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>") + "<br><br>";
                    strMensaje += @"</td></tr>";

                    strMensaje += @"</table>";

                    //strAsunto += " Usuario ( " + aID[0] + "/" + aID[1] +" ) MODIFICACION DE LA SOLAPA DE PARTES";

                    if (aID[1].Trim() == "")
                    {
                        strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aID[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                        strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                        strTO = "EDA";
                    }
                    else
                    {
                        strTO = aID[1];
                    }
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);                
                }
            }

            // FIN CORREO

            return sResul;

        }
        private void PonerEventosFecha()
        {
            Utilidades.SetEventosFecha(this.txtFechaInicioPrevista);
            Utilidades.SetEventosFecha(this.txtFechaFinPrevista);
            Utilidades.SetEventosFecha(this.txtFechaInicioReal);
            Utilidades.SetEventosFecha(this.txtFechaFinReal);
        }
        /// <summary>
        /// Comprueba si las fechas de la tarea son modificables en función del estado de la deficiencia (órden)
        /// </summary>
        /// <param name="sEstado"></param>
        /// <returns></returns>
        private bool FechaModificable(string sEstado)
        {
            /*
             * Tramitada(1) 
             * Pdte de aclaración(2) 
             * Aceptada(3) 
             * Rechazada(4) 
             * En estudio(5) 
             * Pdte de resolución (6) 
             * Pdte de aceptación de propuesta(7) 
             * En resolución(8) 
             * Resuelta(9)  
             * No aprobada(10) 
             * Aprobada(11) 
             * Anulada(12) 
             * Aclaración Resuelta(13)             
             * * */
            bool bRes = false;
            if (sEstado == "1" || sEstado == "3" || sEstado == "5" || sEstado == "6" || sEstado == "8")
                bRes = true;

            return bRes;
        }
    }
}
