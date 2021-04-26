using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;
using EO.Web;
using System.Text.RegularExpressions;

namespace GESTAR.Capa_Presentacion.ASPX
{
	/// <summary>
	/// Descripción breve de Catalogos.
	/// </summary>
	/// 

    public partial class Catalogos : System.Web.UI.Page, ICallbackEventHandler
	{
        private string _callbackResultado = null;
        SqlDataReader dr = null;
        private int intContador = 0;
        public string strTablaHtmlCatalogo;
        public string strFecha="F.Li/Pac";
                      
        public Catalogos()
		{
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                if (!Page.IsCallback)
                {
                    Master.TituloPagina = "Catálogo";
                    Master.bFuncionesLocales = true;

                    Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

                    hdnOpcion.Text = Session["ORIGEN"].ToString();

                    string[] aToolTip1 = new string[] { "1", "Genera un fichero PDF, de las órdenes presentadas en el catálogo" };
                    Master.Tooltipes.Add(aToolTip1);

                    string[] aToolTip2 = new string[] { "2", "Genera un fichero Excel, de las órdenes presentadas en el catálogo" };
                    Master.Tooltipes.Add(aToolTip2);

                    if (hdnOpcion.Text == "PANT_VENCIMIENTO")
                    {
                        if (short.Parse((((Hashtable)Session["PANT_VENCIMIENTO"])["rdlOpciones"]).ToString())==2)
                            strFecha = "F.F.Real.";
                        strTablaHtmlCatalogo = ObtenerDeficiencias_Vto(1,0);
                        Master.nBotonera = 13;
                    }
                    else if (hdnOpcion.Text == "PANT_AVANZADO")
                    {
                        strTablaHtmlCatalogo = ObtenerDeficiencias_Avan(1,0);
                        Master.nBotonera = 10;

                        string[] aToolTip3 = new string[] { "3", "Vuelca a Excel, la información más relevante de las órdenes presentadas en el catálogo" };
                        Master.Tooltipes.Add(aToolTip3);
                    }
  
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                    ////2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar la página", ex);
            }

       }
        protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
                case "regresar":
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                    break;
            }
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

        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }
        public void RaiseCallbackEvent(string eventArg)
        {
            //1º Si hubiera argumentos, se recogen y tratan.
            string[] aArgs = Regex.Split(eventArg, @"@@");

            //2º Aquí realizaríamos el acceso a BD, etc,...


            System.Text.StringBuilder strbTabla = new System.Text.StringBuilder();
            strbTabla.Length = 0;

            switch (aArgs[0])
            {
                case ("leer"):
                    if (hdnOpcion.Text == "PANT_VENCIMIENTO")
                        strbTabla.Append(ObtenerDeficiencias_Vto(byte.Parse(aArgs[1]), byte.Parse(aArgs[2])));
                    else if (hdnOpcion.Text == "PANT_AVANZADO")
                        strbTabla.Append(ObtenerDeficiencias_Avan(byte.Parse(aArgs[1]), byte.Parse(aArgs[2])));                         
                    break;
                case ("volcar"):
                    strbTabla.Append(VolcarDeficiencias());
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
                _callbackResultado = aArgs[0] + "@@OK@@" + intContador.ToString(); //
            }
        }


        private string ObtenerDeficiencias_Avan(byte byColum, byte byOrden)
        {
            try
            {
                    // SI NO TIENE FECHA ES EL CASO 'ACTUAL' EN CASO CONTRARIO ES EL 'CRONOLOGIA'
                    //string sCaso="";

                    //if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "A") sCaso = "A";                       
                    //else  sCaso="C";

                    string strTramitadas;
                    string strPdteAclara;
                    string strAclaRta;
                    string strAceptadas;
                    string strRechazadas;
                    string strEnEstudio;
                    string strPdtesDeResolucion;
                    string strPdtesDeAcepPpta;
                    string strEnResolucion;
                    string strResueltas;
                    string strNoAprobadas;
                    string strAprobadas;
                    string strAnuladas;
                    short shOpLogica;

                    if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "A")
                    {
                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                        else strTramitadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                        else strPdteAclara = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                        else strAclaRta = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                        else strAceptadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                        else strRechazadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                        else strEnEstudio = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                        else strPdtesDeResolucion = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                        else strPdtesDeAcepPpta = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                        else strEnResolucion = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas"]).ToString())) strResueltas = "1";
                        else strResueltas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                        else strNoAprobadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas"]).ToString())) strAprobadas = "1";
                        else strAprobadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas"]).ToString())) strAnuladas = "1";
                        else strAnuladas = "0";

                        shOpLogica = short.Parse((((Hashtable)Session["PANT_AVANZADO"])["rdlCasoActual"]).ToString());
                    }
                    else
                    {
                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas2"]).ToString())) strTramitadas = "1";
                        else strTramitadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara2"]).ToString())) strPdteAclara = "1";
                        else strPdteAclara = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta2"]).ToString())) strAclaRta = "1";
                        else strAclaRta = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas2"]).ToString())) strAceptadas = "1";
                        else strAceptadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas2"]).ToString())) strRechazadas = "1";
                        else strRechazadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio2"]).ToString())) strEnEstudio = "1";
                        else strEnEstudio = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion2"]).ToString())) strPdtesDeResolucion = "1";
                        else strPdtesDeResolucion = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta2"]).ToString())) strPdtesDeAcepPpta = "1";
                        else strPdtesDeAcepPpta = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion2"]).ToString())) strEnResolucion = "1";
                        else strEnResolucion = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas2"]).ToString())) strResueltas = "1";
                        else strResueltas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas2"]).ToString())) strNoAprobadas = "1";
                        else strNoAprobadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas2"]).ToString())) strAprobadas = "1";
                        else strAprobadas = "0";

                        if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas2"]).ToString())) strAnuladas = "1";
                        else strAnuladas = "0";

                        shOpLogica = short.Parse((((Hashtable)Session["PANT_AVANZADO"])["rdlCasoCronologia"]).ToString());
                    }                    

                    System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();
                    dr = null;
                    dr = Deficiencias.BusqAvanzaDefi
                                    (
                        //2,0,"0",0,"0","","","","","","","","","","","","OR","OR","OR","0","0","T",0,0,"1",0,0,0,"",0,0,"","","",""
                                    short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboImportancia"]).ToString()),
                                    short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboPrioridad"]).ToString()),
                                    short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboRtado"]).ToString()),

                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnIDArea"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnEntrada"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnTipo"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnAlcance"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnProceso"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnProducto"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnRequisito"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnCausa"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnCoordinador"]).ToString()),
                                    int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnSolicitante"]).ToString()),

                                    (((Hashtable)Session["PANT_AVANZADO"])["txtFechaInicio"]).ToString(),
                                    (((Hashtable)Session["PANT_AVANZADO"])["txtFechaFin"]).ToString(),
                                    (((Hashtable)Session["PANT_AVANZADO"])["txtProveedor"]).ToString(),
                                    (((Hashtable)Session["PANT_AVANZADO"])["txtCliente"]).ToString(),
                                    (((Hashtable)Session["PANT_AVANZADO"])["txtCR"]).ToString(),
                                    strTramitadas,
                                    strPdteAclara,
                                    strAclaRta,
                                    strAceptadas,
                                    strRechazadas,
                                    strEnEstudio,
                                    strPdtesDeResolucion,
                                    strPdtesDeAcepPpta,
                                    strEnResolucion,
                                    strResueltas,
                                    strNoAprobadas,
                                    strAprobadas,
                                    strAnuladas,
                                    shOpLogica,
                                    (((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString(),
                                    int.Parse(Session["IDFICEPI"].ToString()),
                                    Session["ADMIN"].ToString(),
                                    byColum,
                                    byOrden
                                    );

                    //int i = 0;
                    sbuilder.Append("<table class='texto' id='tblCatalogo' style='width:980px;text-align:left'>" + (char)13);
                    //sbuilder.Append("<colgroup><col style='width:25px;padding-left:5px;' /><col style='width:326px;padding-top:2px;' /><col style='width:131px;padding-top:2px;' /><col style='width:200px;padding-top:2px;' /><col style='width:108px;padding-top:2px;' /><col style='width:60px;padding-top:2px;' /><col style='width:65px;padding-top:2px;' /><col style='width:65px;padding-top:2px;' /><col style='width:0px;display:none;' /></colgroup>");
                    sbuilder.Append("<colgroup><col style='width:25px;' /><col style='width:326px;' /><col style='width:131px;' /><col style='width:198px;' /><col style='width:108px;' /><col style='width:62px;' /><col style='width:65px;' /><col style='width:65px;' /></colgroup>");
                    //sbuilder.Append("<tbody id='tbodyDatos'>" + (char)10);
                    while (dr.Read())
                    {
                        sbuilder.Append("<tr style='cursor: pointer;height:22px' onmouseover='TTip(event);'>");

                        sbuilder.Append("<td style='padding-left:5px;'><INPUT title='Si está marcado se saca el detalle de la orden' id='" + dr["Id"].ToString() + "' onclick='D();' type=checkbox runat='server'/></td>");
                        sbuilder.Append("<td><nobr class='NBR' style='width:320px;'>" + int.Parse(dr["ID"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["ASUNTO"].ToString() + "</nobr></td>");
                        sbuilder.Append("<td><nobr class='NBR' style='width:128px;'>" + dr["T042_DENOMINACION"].ToString() + "</nobr></td>");
                        sbuilder.Append("<td><nobr class='NBR' style='width:198px;'>" + dr["SOLICITANTE"].ToString() + "</nobr></td>");

                        sbuilder.Append("<td>&nbsp;" + dr["ESTADO"].ToString() + "</td>");

                        string strFecha;
                        if (dr["FECHA_NOTIFICACION"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)dr["FECHA_NOTIFICACION"]).ToShortDateString();

                        sbuilder.Append("<td>" + strFecha + "</td>");

                        if (dr["FECHA_LIMITE"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)dr["FECHA_LIMITE"]).ToShortDateString();

                        sbuilder.Append("<td>" + strFecha + "</td>");
                        sbuilder.Append("<td>&nbsp;" + dr["IMPORTANCIA"].ToString() + "</td>");
                        sbuilder.Append("</tr>" + (char)13);
                        //sbuilder += strFilas;
                        //i = i + 1;
                    }

                    dr.Close();
                    dr.Dispose();
                    //sbuilder.Append("</tbody>");
                    sbuilder.Append("</table>");
					

                return sbuilder.ToString();
            }
            catch (Exception ex)
            {
                return "N@@" + Errores.mostrarError("Error al obtener las órdenes", ex);
            }
        }
        private string ObtenerDeficiencias_Vto(byte byColum, byte byOrden)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();
                dr = null;
                dr = Deficiencias.BusqVtoDefi
                                (
                                int.Parse((((Hashtable)Session["PANT_VENCIMIENTO"])["hdnIDArea"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_VENCIMIENTO"])["rdlOpciones"]).ToString()),
                                (((Hashtable)Session["PANT_VENCIMIENTO"])["txtFechaInicio"]).ToString(),
                                (((Hashtable)Session["PANT_VENCIMIENTO"])["txtFechaFin"]).ToString(),
                                int.Parse(Session["IDFICEPI"].ToString()),
                                Session["ADMIN"].ToString(),
                                byColum,
                                byOrden
                                );

                int i = 0;
                sbuilder.Append("<table id='tblCatalogo' style='width:980px;text-align:left;'>" + (char)13);
                //sbuilder.Append("<colgroup><col style='width:25px;padding-left:5px;' /><col style='width:326px;padding-top:2px;' /><col style='width:131px;padding-top:2px;' /><col style='width:200px;padding-top:2px;' /><col style='width:108px;padding-top:2px;' /><col style='width:60px;padding-top:2px;' /><col style='width:65px;padding-top:2px;' /><col style='width:65px;padding-top:2px;' /><col style='width:0px;display:none;' /></colgroup>");
                sbuilder.Append("<colgroup><col style='width:25px' /><col style='width:326px;' /><col style='width:131px;' /><col style='width:200px;' /><col style='width:108px;' /><col style='width:60px;' /><col style='width:65px;' /><col style='width:65px;' /></colgroup>");
                sbuilder.Append("<tbody id='tbodyDatos'>" + (char)10);
                while (dr.Read())
                {                 
                    sbuilder.Append("<tr style='cursor: pointer;height:22px' onmouseover='TTip(event);'>");

                    sbuilder.Append("<td  style='padding-left:5px;'><INPUT title='Si está marcado se saca el detalle de la orden' id='" + dr["Id"].ToString() + "' onclick='D();' type=checkbox runat='server'/></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:320px;'>" + int.Parse(dr["ID"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["ASUNTO"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:128px;'>" + dr["T042_DENOMINACION"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:198px;'>" + dr["SOLICITANTE"].ToString() + "</nobr></td>");

                    sbuilder.Append("<td>&nbsp;" + dr["ESTADO"].ToString() + "</td>");

                    string strFecha;
                    if (dr["FECHA_NOTIFICACION"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["FECHA_NOTIFICACION"]).ToShortDateString();

                    sbuilder.Append("<td>&nbsp;" + strFecha + "</td>");

                    if (dr["FECHA_LIMITE"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["FECHA_LIMITE"]).ToShortDateString();

                    sbuilder.Append("<td>" + strFecha + "</td>");
                    sbuilder.Append("<td>" + dr["IMPORTANCIA"].ToString() + "</td>");
                    sbuilder.Append("</tr>" + (char)13);
                    //sbuilder += strFilas;
                    i = i + 1;
                }

                dr.Close();
                dr.Dispose();

                sbuilder.Append("</tbody>");
                sbuilder.Append("</table>");

                return sbuilder.ToString();
            }
            catch (Exception ex)
            {
                return "N@@" + Errores.mostrarError("Error al obtener las órdenes", ex);
            }
        }
        private string VolcarDeficiencias()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbl = new System.Text.StringBuilder();
                string strFecha;
                string strCadena;

                SqlDataReader drDef = null;
                SqlDataReader drTar = null;
                SqlDataReader drCro = null;

                string strTramitadas;
                string strPdteAclara;
                string strAclaRta;
                string strAceptadas;
                string strRechazadas;
                string strEnEstudio;
                string strPdtesDeResolucion;
                string strPdtesDeAcepPpta;
                string strEnResolucion;
                string strResueltas;
                string strNoAprobadas;
                string strAprobadas;
                string strAnuladas;
                short shOpLogica;

                if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "A")
                {
                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                    else strTramitadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                    else strPdteAclara = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                    else strAclaRta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                    else strAceptadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                    else strRechazadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                    else strEnEstudio = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                    else strPdtesDeResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                    else strPdtesDeAcepPpta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                    else strEnResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas"]).ToString())) strResueltas = "1";
                    else strResueltas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                    else strNoAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas"]).ToString())) strAprobadas = "1";
                    else strAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas"]).ToString())) strAnuladas = "1";
                    else strAnuladas = "0";

                    shOpLogica = short.Parse((((Hashtable)Session["PANT_AVANZADO"])["rdlCasoActual"]).ToString());
                }
                else
                {
                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas2"]).ToString())) strTramitadas = "1";
                    else strTramitadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara2"]).ToString())) strPdteAclara = "1";
                    else strPdteAclara = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta2"]).ToString())) strAclaRta = "1";
                    else strAclaRta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas2"]).ToString())) strAceptadas = "1";
                    else strAceptadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas2"]).ToString())) strRechazadas = "1";
                    else strRechazadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio2"]).ToString())) strEnEstudio = "1";
                    else strEnEstudio = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion2"]).ToString())) strPdtesDeResolucion = "1";
                    else strPdtesDeResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta2"]).ToString())) strPdtesDeAcepPpta = "1";
                    else strPdtesDeAcepPpta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion2"]).ToString())) strEnResolucion = "1";
                    else strEnResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas2"]).ToString())) strResueltas = "1";
                    else strResueltas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas2"]).ToString())) strNoAprobadas = "1";
                    else strNoAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas2"]).ToString())) strAprobadas = "1";
                    else strAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas2"]).ToString())) strAnuladas = "1";
                    else strAnuladas = "0";

                    shOpLogica = short.Parse((((Hashtable)Session["PANT_AVANZADO"])["rdlCasoCronologia"]).ToString());
                }                    

                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();
                dr = null;
                dr = Deficiencias.BusqAvanzaDefi
                                (
                    //2,0,"0",0,"0","","","","","","","","","","","","OR","OR","OR","0","0","T",0,0,"1",0,0,0,"",0,0,"","","",""
                                short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboImportancia"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboPrioridad"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO"])["cboRtado"]).ToString()),

                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnIDArea"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnEntrada"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnTipo"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnAlcance"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnProceso"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnProducto"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnRequisito"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnCausa"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnCoordinador"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO"])["hdnSolicitante"]).ToString()),

                                (((Hashtable)Session["PANT_AVANZADO"])["txtFechaInicio"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO"])["txtFechaFin"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO"])["txtProveedor"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO"])["txtCliente"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO"])["txtCR"]).ToString(),
                                strTramitadas,
                                strPdteAclara,
                                strAclaRta,
                                strAceptadas,
                                strRechazadas,
                                strEnEstudio,
                                strPdtesDeResolucion,
                                strPdtesDeAcepPpta,
                                strEnResolucion,
                                strResueltas,
                                strNoAprobadas,
                                strAprobadas,
                                strAnuladas,
                                shOpLogica,
                                (((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString(),
                                int.Parse(Session["IDFICEPI"].ToString()),
                                Session["ADMIN"].ToString(),
                                1,
                                0
                                );

                sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
                sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
                sb.Append("     <td>Area</td>");
                sb.Append("     <td>Id.Orden</td>");
                sb.Append("     <td>Orden</td>");
                sb.Append("     <td>F.Creación</td>");
                sb.Append("     <td>Estado actual</td>");
                sb.Append("     <td>Solicitante</td>");
                sb.Append("     <td>Coordinador</td>");
                sb.Append("     <td>F.Notificación</td>");
                sb.Append("     <td>F.Limite</td>");
                sb.Append("     <td>F.Pactada</td>");
                sb.Append("     <td>Importancia</td>");
                sb.Append("     <td>Prioridad</td>");
                sb.Append("     <td>Avance</td>");
                sb.Append("     <td>Descripción</td>");
                sb.Append("     <td>Entrada</td>");
                sb.Append("     <td>Alcance</td>");
                sb.Append("     <td>Tipo</td>");
                sb.Append("     <td>Producto</td>");
                sb.Append("     <td>Proceso</td>");
                sb.Append("     <td>Requisito</td>");
                sb.Append("     <td>Causa</td>");
                sb.Append("     <td>C.R.</td>");
                sb.Append("     <td>Proveedor</td>");
                sb.Append("     <td>Cliente</td>");
                sb.Append("     <td>Causa/Beneficio</td>");
                sb.Append("     <td>Resultado</td>");
                sb.Append("     <td>Resultado Descrip.</td>");
                sb.Append("     <td>Observaciones</td>");
                sb.Append("     <td>Solicitud de aclaraciones</td>");
                sb.Append("     <td>Aclaraciones</td>");
                sb.Append("     <td>F.Inicio Prev.</td>");
                sb.Append("     <td>F.Fin Prev.</td>");
                sb.Append("     <td>Esfuerzo Prev.</td>");
                sb.Append("     <td>Unidad</td>");
                sb.Append("     <td>F.Inicio Real.</td>");
                sb.Append("     <td>F.Fin Real.</td>");
                sb.Append("     <td>Esfuerzo Real.</td>");
	            sb.Append("     <td>Tramitada</td>");
                sb.Append("     <td>Pte de Aclaración</td>");
	            sb.Append("     <td>Aceptada</td>");
	            sb.Append("     <td>Rechazada</td>");
	            sb.Append("     <td>En estudio</td>");
                sb.Append("     <td>Pte.Resolución</td>");
                sb.Append("     <td>Pte.Acep.Propuesta</td>");
	            sb.Append("     <td>En resolucion</td>");
	            sb.Append("     <td>Resuelta</td>");
	            sb.Append("     <td>No aprobada</td>");
	            sb.Append("     <td>Aprobada</td>");
                sb.Append("     <td>Anulada</td>");
                sb.Append("     <td>Aclaración resuelta</td>");					
                sb.Append("     <td>Id.Tarea</td>");
                sb.Append("     <td>Tarea</td>");
                sb.Append("	</tr>");
                sb.Append("</table>");


                sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");

                //string sDenArea = "";
                string sAsuntoDefi = "";
                string sDesDeficiencia = "";
                string sCausa = "";
                string sResultado = "";
                string sObservacion = "";
                string sSolicitarAclara = "";
                string sAclaracion = "";
                string sDesTarea = "";

                int iIDFICEPI = 0;
                while (dr.Read())
                {
                    // Leer Deficiencia
                    drDef = null;
                    drDef = Deficiencias.Select(null, int.Parse(dr["ID"].ToString()));
                    drDef.Read();

                    if (int.Parse(drDef["T044_IDDEFICIENCIA"].ToString()) != iIDFICEPI)
                        iIDFICEPI = int.Parse(drDef["T044_IDDEFICIENCIA"].ToString());
                    else continue;

                    sbl.Length = 0;
                    sbl.Append("<tr> ");

                    // Solapa General

                    sbl.Append("<td valign='top'>" + drDef["AREA"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>&nbsp;" + drDef["T044_IDDEFICIENCIA"].ToString() + "</td>");

                    sAsuntoDefi = drDef["T044_ASUNTO"].ToString().Replace("<", "&#60");
                    sAsuntoDefi = sAsuntoDefi.ToString().Replace("\"", "&#34");
                    sbl.Append("<td valign='top'>" + sAsuntoDefi + "</td>");        

                    if (drDef["T044_FECALTA"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FECALTA"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    sbl.Append("<td valign='top'>" + drDef["ESTADO"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["SOLICITANTE"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["COORDINADOR"].ToString() + "</td>");

                    if (drDef["T044_NOTIFICADA"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_NOTIFICADA"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if (drDef["T044_FECLIMITE"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FECLIMITE"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if (drDef["T044_FECPACT"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FECPACT"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    sbl.Append("<td valign='top'>" + drDef["IMPORTANCIA"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["PRIORIDAD"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["AVANCE"].ToString() + "</td>");

                    sDesDeficiencia = drDef["T044_DESCRIPCION"].ToString().Replace("<", "&#60");
                    sDesDeficiencia = sDesDeficiencia.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sDesDeficiencia + "</td>");

                    sbl.Append("<td valign='top'>" + drDef["ENTRADA"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["ALCANCE"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["TIPO"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["PRODUCTO"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["PROCESO"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["REQUISITO"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["CAUSA"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["T044_CR"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["T044_PROVEEDOR"].ToString() + "</td>");
                    sbl.Append("<td valign='top'>" + drDef["T044_CLIENTE"].ToString() + "</td>");

                    sCausa = drDef["T044_CAUSA"].ToString().Replace("<", "&#60");
                    sCausa = sCausa.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sCausa + "</td>");

                    sbl.Append("<td valign='top'>" + drDef["RESULTADO"].ToString() + "</td>");

                    sResultado = drDef["T044_RESULTADO"].ToString().Replace("<", "&#60");
                    sResultado = sResultado.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sResultado + "</td>");

                    sObservacion = drDef["T044_OBSERVACION"].ToString().Replace("<", "&#60");
                    sObservacion = sObservacion.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sObservacion + "</td>");

                    sSolicitarAclara = drDef["T044_SOLACLARA"].ToString().Replace("<", "&#60");
                    sSolicitarAclara = sSolicitarAclara.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sSolicitarAclara + "</td>");

                    sAclaracion = drDef["T044_ACLARA"].ToString().Replace("<", "&#60");
                    sAclaracion = sAclaracion.ToString().Replace("\"", "&#34");

                    sbl.Append("<td valign='top'>" + sAclaracion + "</td>");

                    // Solapa Planificación

                    if (drDef["T044_FINIPREV"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FINIPREV"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if (drDef["T044_FFINPREV"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FFINPREV"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if ((float)drDef["T044_TIEMPOESTI"] == 0)
                        strCadena = "";
                    else
                        strCadena = ((float)drDef["T044_TIEMPOESTI"]).ToString("#,##0.00");

                    sbl.Append("<td valign='top'>" + strCadena + "</td>");

                    if (drDef["T044_UNIDADESTIMA"].ToString() == "1")
                        strCadena = "Jornadas";
                    else
                        strCadena = "Horas";

                    sbl.Append("<td valign='top'>" + strCadena + "</td>");

                    if (drDef["T044_FECINIREAL"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FECINIREAL"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if (drDef["T044_FECFINREAL"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)drDef["T044_FECFINREAL"]).ToShortDateString();

                    sbl.Append("<td valign='top'>" + strFecha + "</td>");

                    if ((float)drDef["T044_TIEMPOINVER"] == 0)
                        strCadena = "";
                    else
                        strCadena = ((float)drDef["T044_TIEMPOINVER"]).ToString("#,##0.00");

                    sbl.Append("<td valign='top'>" + strCadena + "</td>");

                    // METO EL ESTADO Y LA FECHA DE LOS DIFERENTES ESTADOS

                    string[] aEstados = new string[14] { "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                    drCro = null;
                    drCro = CRONOLOGIA.Volcado(int.Parse(dr["ID"].ToString()));
                    while (drCro.Read())
                    {
                        if (drCro["T096_FECHA"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)drCro["T096_FECHA"]).ToString();

                        aEstados[short.Parse(drCro["T044_ESTADO"].ToString())] = strFecha;
                    }
                    // como empieza de cero siempre queda en blanco pero no lo tratamos

                    sbl.Append("<td valign='top'>" + aEstados[1] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[2] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[3] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[4] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[5] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[6] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[7] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[8] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[9] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[10] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[11] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[12] + "</td>");
                    sbl.Append("<td valign='top'>" + aEstados[13] + "</td>");
                    // CONTINUAR AQUÍ 

                    drTar = null;
                    drTar = TAREA.Catalogo(int.Parse(dr["ID"].ToString()), 1, 0, 0);
                    intContador = 0;
                    while (drTar.Read())
                    {
                        sb.Append(sbl.ToString());
                        sb.Append("<td valign='top'>&nbsp;" + drTar["ID"].ToString() + "</td>");

                        sDesTarea = drTar["DENOMINACION"].ToString().Replace("<", "&#60");
                        sDesTarea = sDesTarea.ToString().Replace("\"", "&#34");

                        sb.Append("<td valign='top'>" + sDesTarea + "</td>");
                        sb.Append("</tr>" + (char)13);
                        intContador = 1;
                    }

                    if (intContador == 0)
                    {
                        sb.Append(sbl.ToString());
                        sb.Append("<td valign='top'>&nbsp;</td><td valign='top'>&nbsp;</td>");
                        sb.Append("</tr>" + (char)13);
                    }

                    //

                    drCro.Close();
                    drCro.Dispose();

                    drTar.Close();
                    drTar.Dispose();

                    drDef.Close();
                    drDef.Dispose();
                }

                dr.Close();
                dr.Dispose();

                sb.Append("</table>");
                sbl = null;

                //return sb.ToString();
                string sIdCache = "GESTAR_EXCEL_CACHE_" + Session["IDFICEPI"].ToString() + "_" + DateTime.Now.ToString();
                Session[sIdCache] = sb.ToString(); ;

                string sResul = "cacheado@@" + sIdCache + "@@" + sb.ToString(); 
                sb.Length = 0; //Para liberar memoria
                return sResul;
            }
            catch (Exception ex)
            {
                return "N@@" + Errores.mostrarError("Error al volcar las órdenes", ex);
            }
        }
    }
}
