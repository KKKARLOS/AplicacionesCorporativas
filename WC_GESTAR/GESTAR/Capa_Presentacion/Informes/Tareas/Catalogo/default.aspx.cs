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

    public partial class CatalogoProp : System.Web.UI.Page , ICallbackEventHandler
	{
        private string _callbackResultado = null;
        SqlDataReader dr = null;
        private int intContador = 0;
        public string strTablaHtmlCatalogo;
        public string strFecha = "F.F.P.";

        public CatalogoProp()
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
                    Master.nBotonera = 10;
                    Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                    hdnOpcion.Text = Session["ORIGEN"].ToString();
                    Session["MODOLECTURA"] = "0";

                    string[] aToolTip1 = new string[] { "1", "Genera un fichero PDF, de las tareas presentadas en el catálogo" };
                    Master.Tooltipes.Add(aToolTip1);

                    string[] aToolTip2 = new string[] { "2", "Genera un fichero Excel, de las tareas presentadas en el catálogo" };
                    Master.Tooltipes.Add(aToolTip2);

                    if (hdnOpcion.Text == "PANT_PROPIAS_TAR")
                    {
                        strTablaHtmlCatalogo = ObtenerTareasPropias(1,0);
                        Master.nBotonera = 13;
                    }
                    else if (hdnOpcion.Text == "PANT_AVANZADO_TAR")
                    {
                        strTablaHtmlCatalogo = ObtenerTareasAvanzado(1,0);
                        string[] aTabla0 = Regex.Split(strTablaHtmlCatalogo, "@@");
                        if (aTabla0[0] != "N") strTablaHtmlCatalogo = aTabla0[0];
                        else
                        {
                            strTablaHtmlCatalogo = "";
                            Master.sErrores = "Error al obtener el catálogo";
                        }
                        Master.nBotonera = 10;
                        string[] aToolTip3 = new string[] { "3", "Vuelca a Excel, toda la información de las tareas presentadas en el catálogo" };
                        Master.Tooltipes.Add(aToolTip3);
                        short iTipoFecha=short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["rdlOpciones"]).ToString());
                        if (iTipoFecha == 2 || iTipoFecha == 3)
                        {
                            strFecha = "F.F.R.";
                        }    

                    }

                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                    //2º Se "registra" la función que va a acceder al servidor.
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
                    if (hdnOpcion.Text == "PANT_PROPIAS_TAR")
                        strbTabla.Append(ObtenerTareasPropias(byte.Parse(aArgs[1]), byte.Parse(aArgs[2])));
                    else if (hdnOpcion.Text == "PANT_AVANZADO_TAR")
                        strbTabla.Append(ObtenerTareasAvanzado(byte.Parse(aArgs[1]), byte.Parse(aArgs[2])));
                    break;

                case ("volcar"):
                    strbTabla.Append(VolcarTareas());
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

        private string ObtenerTareasPropias(byte byColum, byte byOrden)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

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

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                else strTramitadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                else strPdteAclara = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                else strAclaRta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                else strAceptadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                else strRechazadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                else strEnEstudio = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                else strPdtesDeResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                else strPdtesDeAcepPpta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                else strEnResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkResueltas"]).ToString())) strResueltas = "1";
                else strResueltas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                else strNoAprobadas = "0";

                dr = null;
                dr = TAREA.TareasPropias
                                (
                                int.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["hdnIDArea"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["cboSituacion"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_PROPIAS_TAR"])["cboRtado"]).ToString()),
                                (((Hashtable)Session["PANT_PROPIAS_TAR"])["txtFecha"]).ToString(),
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
                                int.Parse(Session["IDFICEPI"].ToString()),
                                Session["ADMIN"].ToString(),
                                byColum,
                                byOrden
                                );

                int i = 0;
                sbuilder.Append("<table id='tblCatalogo' style='width:880px;' border='0'>");
                //sbuilder.Append("<colgroup><col style='width:3%' /><col style='width:24%' /><col style='width:25%' /><col style='width:19%' /><col style='width:12%' /><col style='width:8%' /><col style='width:7%' /><col style='width:2%' /></colgroup>");
                sbuilder.Append(@"<colgroup>
                                    <col style='width:25px;' />
                                    <col style='width:215px;' />
                                    <col style='width:215px;' />
                                    <col style='width:160px;' />
                                    <col style='width:105px;' />
                                    <col style='width:80px;' />
                                    <col style='width:80px;' />
                                </colgroup>");
                while (dr.Read())
                {
                    string strFecha;
                    if (dr["FECHA"] == System.DBNull.Value) strFecha = "";
                    else strFecha = ((DateTime)dr["FECHA"]).ToShortDateString();

                    sbuilder.Append("<tr id='" + dr["ID_AREA"].ToString().Replace((char)39, (char)34) + "//" + dr["AREA"].ToString().Replace((char)39, (char)34) + "//" + dr["ID_DEFI"].ToString() + "//" + dr["DEFICIENCIA"].ToString().ToString().Replace((char)39, (char)34) + "//" + dr["ID"].ToString() + "//" + dr["ESCOORDINADOR"].ToString() + "//" + dr["ESTADO"].ToString() + "//" + dr["CORREOCOORDINADOR"].ToString() + "//" + dr["COORDINADOR"].ToString());

                    sbuilder.Append("' ondblclick=D_Tarea(this)");
                    sbuilder.Append(" onclick=marcar2(this);");
                    sbuilder.Append(" onmouseover='TTip(event)';");
                    sbuilder.Append(" style='cursor: pointer;height:22px'>");

                    sbuilder.Append("<td><INPUT title='Si está marcado sacamos el detalle de la tarea' id='" + dr["Id"].ToString() + "' onclick='D();' type=checkbox runat='server'/></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:210px;'>" + int.Parse(dr["ID"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["TAREA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:210px;'>" + int.Parse(dr["ID_DEFI"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["DEFICIENCIA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td><nobr class='NBR' style='width:160px;'>" + dr["AREA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td>" + dr["AVANCE"].ToString() + "</td>");
                    sbuilder.Append("<td>" + dr["RESULTADO"].ToString() + "</td>");
                    sbuilder.Append("<td>" + strFecha + "</td>");

                    sbuilder.Append("</tr>" + (char)13);
                    //sbuilder += strFilas;
                    i = i + 1;
                }

                dr.Close();
                dr.Dispose();
                sbuilder.Append("</table>");


                return sbuilder.ToString();
            }
            catch (Exception ex)
            {
                return "N@@" + Errores.mostrarError("Error al obtener las tareas", ex);
            }
        }

        private string ObtenerTareasAvanzado(byte byColum, byte byOrden)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

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

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                else strTramitadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                else strPdteAclara = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                else strAclaRta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                else strAceptadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                else strRechazadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                else strEnEstudio = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                else strPdtesDeResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                else strPdtesDeAcepPpta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                else strEnResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkResueltas"]).ToString())) strResueltas = "1";
                else strResueltas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                else strNoAprobadas = "0";

                dr = null;
                dr = TAREA.Avanzado
                                (
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnIDArea"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnDeficiencia"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnCoordinador"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnEspecialista"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["cboSituacion"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["cboRtado"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["rdlOpciones"]).ToString()),
                                (((Hashtable)Session["PANT_AVANZADO_TAR"])["txtFechaInicio"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO_TAR"])["txtFechaFin"]).ToString(),
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
                                int.Parse(Session["IDFICEPI"].ToString()),
                                Session["ADMIN"].ToString(),
                                byColum,
                                byOrden
                                );

                int i = 0;
                sbuilder.Append("<table id='tblCatalogo' style='width:880px;text-align:left'>" + (char)13);
                //sbuilder.Append("<colgroup><col style='width:25px;padding-left:5px;padding-top:2px;' /><col style='width:213px;padding-top:2px;' /><col style='width:213px;padding-top:2px;' /><col style='width:166px;;padding-top:2px;' /><col style='width:108px;padding-top:2px;' /><col style='width:70px;padding-top:2px;' /><col style='width:84px;padding-top:2px;' /></colgroup>");
                sbuilder.Append("<colgroup><col style='width:25px;' /><col style='width:213px;' /><col style='width:213px;' /><col style='width:166px;' /><col style='width:108px;' /><col style='width:70px;' /><col style='width:84px;' /></colgroup>");
                sbuilder.Append("<tbody id='tbodyDatos'>" + (char)10);
                while (dr.Read())
                {
                    sbuilder.Append("<tr id='" + dr["ID_AREA"].ToString() + "//" + dr["AREA"].ToString() + "//" + dr["ID_DEFI"].ToString() + "//" + dr["DEFICIENCIA"].ToString() + "//" + dr["ID"].ToString() + "//" + dr["ESCOORDINADOR"].ToString() + "//" + dr["ESTADO"].ToString() + "//" + dr["CORREOCOORDINADOR"].ToString() + "//" + dr["COORDINADOR"].ToString());
                    //sbuilder.Append("<tr ");

                    //if (i % 2 == 0)
                    //    sbuilder.Append("' class='FA' ");

                    //else
                    //    sbuilder.Append("' class='FB' ");

                    sbuilder.Append("' ondblclick=D_Tarea(this)");
                    sbuilder.Append(" onclick=marcar2(this);");
                    sbuilder.Append(" onmouseover='TTip(event)';");

                    sbuilder.Append(" style='cursor: pointer;height:22px'>");

                    sbuilder.Append("<td style='width:auto;'><INPUT title='Si está marcado sacamos el detalle de la tarea' id='" + dr["Id"].ToString() + "' onclick='D();' type=checkbox runat='server'/></td>");
                    sbuilder.Append("<td style='width:auto;'><nobr class='NBR' style='width:210px;'>" + int.Parse(dr["ID"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["TAREA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td style='width:auto;'><nobr class='NBR' style='width:210px;'>" + int.Parse(dr["ID_DEFI"].ToString()).ToString("#,###,##0") + "&nbsp;-&nbsp;" + dr["DEFICIENCIA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td style='width:auto;'><nobr class='NBR' style='width:163px;'>" + dr["AREA"].ToString() + "</nobr></td>");
                    sbuilder.Append("<td style='width:auto;'>&nbsp;" + dr["AVANCE"].ToString() + "</td>");
                    sbuilder.Append("<td style='width:auto;'>&nbsp;" + dr["RESULTADO"].ToString() + "</td>");

                    string strFecha;
                    if (dr["FECHA"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["FECHA"]).ToShortDateString();

                    sbuilder.Append("<td style='width:auto;'>" + strFecha + "</td>");

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
                return "N@@" + Errores.mostrarError("Error al obtener las tareas", ex);
            }
        }
        private string VolcarTareas()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbl = new System.Text.StringBuilder();

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

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                else strTramitadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                else strPdteAclara = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                else strAclaRta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                else strAceptadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                else strRechazadas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                else strEnEstudio = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                else strPdtesDeResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                else strPdtesDeAcepPpta = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                else strEnResolucion = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkResueltas"]).ToString())) strResueltas = "1";
                else strResueltas = "0";

                if (bool.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                else strNoAprobadas = "0";

                //SqlDataReader drTec = null;
                dr = null;
                dr = TAREA.Volcar
                                (
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["rdlOpciones"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnIDArea"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnDeficiencia"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnCoordinador"]).ToString()),
                                int.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["hdnEspecialista"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["cboSituacion"]).ToString()),
                                short.Parse((((Hashtable)Session["PANT_AVANZADO_TAR"])["cboRtado"]).ToString()),
                                (((Hashtable)Session["PANT_AVANZADO_TAR"])["txtFechaInicio"]).ToString(),
                                (((Hashtable)Session["PANT_AVANZADO_TAR"])["txtFechaFin"]).ToString(),
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
                                int.Parse(Session["IDFICEPI"].ToString()),
                                Session["ADMIN"].ToString()
                                );

                sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
                sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
                sb.Append("     <td style='width:auto;'>Area</td>");
                sb.Append("     <td style='width:auto;'>Orden</td>");
                sb.Append("     <td style='width:auto;'>Id.Orden</td>");
                sb.Append("     <td style='width:auto;'>Tarea</td>");
                sb.Append("     <td style='width:auto;'>Id.Tarea</td>");
                sb.Append("     <td style='width:auto;'>Fec.In.Prev</td>");
                sb.Append("     <td style='width:auto;'>Fec.Fin.Prev</td>");
                sb.Append("     <td style='width:auto;'>Fec.In.Real</td>");
                sb.Append("     <td style='width:auto;'>Fec.Fin.Real</td>");
                sb.Append("     <td style='width:auto;'>Resultado</td>");
                sb.Append("     <td style='width:auto;'>Descripcion</td>");
                sb.Append("     <td style='width:auto;'>Avance</td>");
                sb.Append("     <td style='width:auto;'>Causas</td>");
                sb.Append("     <td style='width:auto;'>Intervenciones</td>");
                sb.Append("     <td style='width:auto;'>Consideraciones/Comentarios</td>");
                sb.Append("     <td>Especialista</td>");
                sb.Append("	</tr>");
                sb.Append("</table>");

                sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");

                string sDenDefi = "";
                string sDenTarea = "";
                string sDesTarea = "";
                string sCausa = "";
                string sIntervencion = "";
                string sConsideracion = "";

                while (dr.Read())
                {
                    sbl.Length = 0;
                    sbl.Append("<tr> ");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + dr["AREA"].ToString() + "</td>");

                    sDenDefi = dr["DEFICIENCIA"].ToString().Replace("<", "&#60");
                    sDenDefi = sDenDefi.ToString().Replace("\"", "&#34");

                    sbl.Append("<td style='width:auto;' valign='top'>" + sDenDefi + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>" + int.Parse(dr["ID_DEFI"].ToString()).ToString("#,###,##0") + "</td>");

                    sDenTarea = dr["TAREA"].ToString().Replace("<", "&#60");
                    sDenTarea = sDenTarea.ToString().Replace("\"", "&#34");

                    sbl.Append("<td style='width:auto;' valign='top'>" + sDenTarea + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>" + int.Parse(dr["ID"].ToString()).ToString("#,###,##0") + "</td>");

                    string strFecha;

                    if (dr["T072_FECINIPREV"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["T072_FECINIPREV"]).ToShortDateString();

                    sbl.Append("<td style='width:auto;' valign='top'>" + strFecha + "</td>");

                    if (dr["T072_FECFINPREV"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["T072_FECFINPREV"]).ToShortDateString();

                    sbl.Append("<td style='width:auto;' valign='top'>" + strFecha + "</td>");

                    if (dr["T072_FECINIREAL"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["T072_FECINIREAL"]).ToShortDateString();

                    sbl.Append("<td style='width:auto;' valign='top'>" + strFecha + "</td>");

                    if (dr["T072_FECFINREAL"] == System.DBNull.Value)
                        strFecha = "";
                    else
                        strFecha = ((DateTime)dr["T072_FECFINREAL"]).ToShortDateString();


                    sbl.Append("<td style='width:auto;' valign='top'>" + strFecha + "</td>");

                    // CONTINUAR AQUÍ dr["ID_AREA"].ToString().Replace((char)39, (char)34) 
                    //string sDesTarea = dr["DESCRIPCION_TAR"].ToString().Replace((char)60, (char)32);

                    sDesTarea = dr["DESCRIPCION_TAR"].ToString().Replace("<", "&#60");
                    sDesTarea = sDesTarea.ToString().Replace("\"", "&#34");

                    sCausa = dr["T072_CAUSA"].ToString().Replace("<", "&#60");
                    sCausa = sCausa.ToString().Replace("\"", "&#34");

                    sIntervencion = dr["T072_INTERVENCION"].ToString().Replace("<", "&#60");
                    sIntervencion = sIntervencion.ToString().Replace("\"", "&#34");

                    sConsideracion = dr["T072_CONSIDERACION"].ToString().Replace("<", "&#60");
                    sConsideracion = sConsideracion.ToString().Replace("\"", "&#34");

                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + dr["RESULTADO"].ToString() + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + sDesTarea + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + dr["AVANCE"].ToString() + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + sCausa + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + sIntervencion + "</td>");
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + sConsideracion + "</td>");
                    //11/05/2016 Juan Antonio Martin Mayoral solicita la inclusión de especialistas
                    sbl.Append("<td style='width:auto;' valign='top'>&nbsp;" + dr["Especialista"].ToString() + "</td>");
                    
/* 3/9/2008 Fernando no quiere mostrar los especialistas, dejamos lo comentarizado por se acaso */
                    sbl.Append("</tr>" + (char)13);
                    sb.Append(sbl.ToString());
/*fin 3/9/2008*/
/*                  drTec = null;
                    drTec = TAREA.LeerTecnicosTarea(int.Parse(dr["ID"].ToString()));

                    intContador = 0;
                    while (drTec.Read())
                    {
                        sb.Append(sbl.ToString());
                        sb.Append("<td valign='top'>&nbsp;" + drTec["TECNICO"].ToString() + "</td>");
                        sb.Append("</tr>" + (char)13);
                        intContador = 1;
                    }
                    if (intContador == 0)
                    {
                        sb.Append(sbl.ToString());
                        sb.Append("<td valign='top'>&nbsp;</td>");
                        sb.Append("</tr>" + (char)13);
                    }
                    drTec.Close();
                    drTec.Dispose();
*/
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
                return "N@@" + Errores.mostrarError("Error al volcar las tareas", ex);
            }
        }
 
    }
}
