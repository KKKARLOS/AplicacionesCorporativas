using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.JScript;

namespace GESTAR.Capa_Presentacion.ASPX
{
	/// <summary>
	/// Summary description for OtrosDatos.
	/// </summary>
    public partial class MtoAreas : System.Web.UI.Page, ICallbackEventHandler
	{
        public SqlConnection oConn;
        public SqlTransaction tr;
		SqlDataReader dr = null;
		protected string strTitulo;
        public string strErrores;
        private string _callbackResultado = null;
        public string strTablaHtmlResponsable;
        public string strTablaHtmlCoordinador;
        public string strTablaHtmlSolicitante;
        public string strTablaHtmlTecnico;
        public string strTablaHTMLDocum;
        
        public string strTablaHtmlValores, strTablaHTMLOrdenes="";
        private int intContador = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                try
                {
                    txtPromotor.Attributes.Add("readonly", "readonly");
                    this.txtFechaAlta.Attributes.Add("readonly", "readonly");

                    hdnAdmin.Text = Request.QueryString["ADMIN"];
                    hdnCoordinador.Text = Request.QueryString["COORDINADOR"];
                    //if (hdnCoordinador.Text=="1" && hdnPromotor.Text != hdnIDFICEPI.Text) lblPromotor.CssClass = "texto";

                    hdnModoLectura.Text = Request.QueryString["MODOLECTURA"];

                    Session["MODOLECTURA"] = hdnModoLectura.Text;

                    if (hdnModoLectura.Text == "1") lblPromotor.CssClass = "texto";
                    hdnIDFICEPI.Text = Session["IDFICEPI"].ToString();

                    if (Request.QueryString["bNueva"] == "false")
                    {
                        //strTitulo="Modificación del área";
                        // Solapa 1
                        dr = null;
                        hdnIDArea.Text = Request.QueryString["ID"].ToString();
                        dr = Areas.LeerUnRegistro(int.Parse(hdnIDArea.Text));
                        if (dr.Read())
                        {
                            txtNombreArea.Text = (string)dr["NOMBRE"];
                            chkCorreo.Checked = (bool)dr["CORREO"];
                            chkSelCoord.Checked = (bool)dr["T042_SELCOORDI"];
                            chkResuelta.Checked = (bool)dr["RESUELTA"];

                            if (dr["T042_FECALTA"] == System.DBNull.Value)
                                txtFechaAlta.Text = "";
                            else
                                txtFechaAlta.Text = ((DateTime)dr["T042_FECALTA"]).ToShortDateString();

                            txtDescripcion.Text = (string)dr["DESCRIPCION"];
                            txtPromotor.Text = (string)dr["RECURSO"];
                            hdnPromotor.Text = dr["PROMOTOR"].ToString();
                            hdnPromotorCorreo.Text = dr["PROMOTOR"].ToString() + "/" + dr["T001_CODRED"].ToString() + "/" + dr["RECURSO"].ToString();
                            hdnPromotorCorreoOld.Text = hdnPromotorCorreo.Text;

                            if ((bool)dr["CATEGORIA"])
                                rdlCategoria.SelectedValue = "1";
                            else
                                rdlCategoria.SelectedValue = "0";

                            if ((bool)dr["ESTADO"])
                                rdlEstado.SelectedValue = "1";
                            else
                                rdlEstado.SelectedValue = "0";

                           //karlos tsPestanas.Items[8].DefaultStyle.Add("display", "none");
                            tsPestanas.Items[4].Disabled = true;

                            chkAutoaprobacion.Checked = (bool)dr["t042_autoaprobable"];

                            chkPermitirCambio.Checked = (bool)dr["t042_bPermitirCambios"];

                            if (((bool)dr["t042_bPermitirCambios"] && hdnIDFICEPI.Text == hdnPromotor.Text) || Session["ADMIN"].ToString() == "A")
                            {
                                //hdnIDFICEPI.Text = hdnPromotor.Text;
                                //if (hdnIDFICEPI.Text == hdnPromotor.Text)
                                //{
                                    //karlos tsPestanas.Items[8].DefaultStyle.Add("display", "block");
                                    tsPestanas.Items[4].Disabled = false;
                                    string[] aTablas = Regex.Split(ObtenerOrdenesParaCambio(hdnIDArea.Text), "@#@");
                                    if (aTablas[0] == "OK")
                                        strTablaHTMLOrdenes = aTablas[1];
                                    else
                                        hdnErrores.Text = aTablas[1];
                                //}
                            }
                        }
                        // Solapa 2

                        ObtenerIntegrantes(int.Parse(Request.QueryString["ID"]));

                        // Solapa 3

                        // Solapa 4

                        string strTabla = ObtenerDocumentos(hdnIDArea.Text);
                        string[] aTabla = Regex.Split(strTabla, "@@");
                        if (aTabla[0] != "N") strTablaHTMLDocum = aTabla[0];

                        hdnFilaSeleccionada.Text = Request.QueryString["FILASELECCIONADA"];

                        if (hdnModoLectura.Text == "1" || (hdnCoordinador.Text == "1" && hdnPromotor.Text != hdnIDFICEPI.Text))
                        {
                            Control Area = this.FindControl("frmDatos");
                            ModoLectura.Poner(Area.Controls);
                        }

                       // rdlCpto.Attributes.Add("onclick", "concepto();");
                        dr.Close();
                        dr.Dispose();
                        if ((hdnPromotor.Text != hdnIDFICEPI.Text) && (hdnAdmin.Text != "A")) lblPromotor.CssClass = "texto";
                    }
                    else
                    {
                        //strTitulo="Creación de un nuevo área";
                        lblPromotor.CssClass = "texto";
                        txtPromotor.Text = Session["NOMBRE2"].ToString();
                        hdnPromotor.Text = Session["IDFICEPI"].ToString();
                        hdnPromotorCorreo.Text = Session["IDFICEPI"].ToString() + "/" + Session["IDRED"] + "/" + Session["NOMBRE"].ToString();
                        txtFechaAlta.Text = System.DateTime.Now.ToShortDateString();

                        strTablaHtmlResponsable = "<table id='tblCatalogoResponsable' align='left' style='width:390px'></table>";
                        strTablaHtmlCoordinador = "<table id='tblCatalogoCoordinador' align='left' style='width:390px'></table>";
                        strTablaHtmlSolicitante = "<table id='tblCatalogoSolicitante' align='left' style='width:390px'></table>";
                        strTablaHtmlTecnico = "<table id='tblCatalogoTecnico' align='left' style='width:390px'></table>";
                    }

                    if (Session["ADMIN"].ToString() == "A")
                    {
                        chkPermitirCambio.Disabled = false;
                        tdPermitirCambio.Style.Add("visibility", "visible");
                    }
                }
                catch (Exception ex)
                {
                    hdnErrores.Text = Errores.mostrarError("Error al obtener los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
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
                case "recursos": // CARGAR RECURSOS
                    strbTabla.Append(ObtenerRecursos(Microsoft.JScript.GlobalObject.unescape(aArgs[1]), Microsoft.JScript.GlobalObject.unescape(aArgs[2]), Microsoft.JScript.GlobalObject.unescape(aArgs[3]), int.Parse(aArgs[4]), int.Parse(aArgs[5])));
                    break;
                case "Cpto":
                    strbTabla.Append(ObtenerCptos(Microsoft.JScript.GlobalObject.unescape(aArgs[1]), Microsoft.JScript.GlobalObject.unescape(aArgs[2])));
                    break;
                case ("documentos"):
                    strbTabla.Append(ObtenerDocumentos(aArgs[1]));
                    break;
                case ("elimdocs"):
                    strbTabla.Append(EliminarDocumentos(aArgs[1]));
                    break;
                case "grabar":
                    strbTabla.Append(Grabar(byte.Parse(aArgs[1]), Microsoft.JScript.GlobalObject.unescape(aArgs[2]), Microsoft.JScript.GlobalObject.unescape(aArgs[3]), byte.Parse(aArgs[4]), byte.Parse(aArgs[5]), byte.Parse(aArgs[6]), byte.Parse(aArgs[7]), byte.Parse(aArgs[8]), Microsoft.JScript.GlobalObject.unescape(aArgs[9]), Microsoft.JScript.GlobalObject.unescape(aArgs[10]), Microsoft.JScript.GlobalObject.unescape(aArgs[11]), Microsoft.JScript.GlobalObject.unescape(aArgs[12]), Microsoft.JScript.GlobalObject.unescape(aArgs[13]), Microsoft.JScript.GlobalObject.unescape(aArgs[14]), Microsoft.JScript.GlobalObject.unescape(aArgs[15]), Microsoft.JScript.GlobalObject.unescape(aArgs[16]), Microsoft.JScript.GlobalObject.unescape(aArgs[17]), Microsoft.JScript.GlobalObject.unescape(aArgs[18]), Microsoft.JScript.GlobalObject.unescape(aArgs[19]), Microsoft.JScript.GlobalObject.unescape(aArgs[20]), Microsoft.JScript.GlobalObject.unescape(aArgs[21]), Microsoft.JScript.GlobalObject.unescape(aArgs[22]), Microsoft.JScript.GlobalObject.unescape(aArgs[23]), Microsoft.JScript.GlobalObject.unescape(aArgs[24]), Microsoft.JScript.GlobalObject.unescape(aArgs[25]), Microsoft.JScript.GlobalObject.unescape(aArgs[26]), Microsoft.JScript.GlobalObject.unescape(aArgs[27]), Microsoft.JScript.GlobalObject.unescape(aArgs[28]), Microsoft.JScript.GlobalObject.unescape(aArgs[29]), aArgs[30], aArgs[31]));
                    break;
                //case "grabarCpto":
                //    strbTabla.Append(GrabarCpto(Microsoft.JScript.GlobalObject.unescape(aArgs[1]),Microsoft.JScript.GlobalObject.unescape(aArgs[2])));
                //    break;                 
                case "setDato":
                    strbTabla.Append(setDato(aArgs[1], aArgs[2], aArgs[3], aArgs[4]));
                    break;
                case "getTareas":
                    strbTabla.Append(getTareas(aArgs[1]));
                    break;
                case "getCronologia":
                    strbTabla.Append(getCronologia(aArgs[1]));
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
        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }
        private void ObtenerIntegrantes(int intIdArea)
        {
            dr = null;
            dr = Integrante.Catalogo(int.Parse(Request.QueryString["ID"].ToString()));

            //int i = 0;
            //int j = 0;
            //int k = 0;
            //int l = 0;

            StringBuilder strBuilderResponsable = new StringBuilder();
            StringBuilder strBuilderCoordinador = new StringBuilder();
            StringBuilder strBuilderSolicitante = new StringBuilder();
            StringBuilder strBuilderTecnico = new StringBuilder();
            StringBuilder sbAux = new StringBuilder();

            strBuilderResponsable.Append("<table id='tblCatalogoResponsable' style='width:390px;text-align:left'>" + (char)13);
            strBuilderCoordinador.Append("<table id='tblCatalogoCoordinador' style='width:390px;text-align:left'>" + (char)13);
            strBuilderSolicitante.Append("<table id='tblCatalogoSolicitante' style='width:390px;text-align:left'>" + (char)13);
            strBuilderTecnico.Append("<table id='tblCatalogoTecnico'         style='width:390px;text-align:left'>" + (char)13);

            while (dr.Read())
            {
                sbAux.Length = 0;
                sbAux.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + "' ");
                sbAux.Append(" onclick=ms(this); ");
                if (dr["T043_FIGURA"].ToString() == "2")
                {
                    //if (i % 2 == 0)
                    //    sbAux.Append("class='FA' ");
                    //else
                    //    sbAux.Append("class='FB' ");

                    //i++;
                    //sbAux.Append(" onclick=marcarUnaFila('tblCatalogoSolicitante',this); ");
                    sbAux.Append(" ondblclick=this.className='FS';eliminarSolicitantes(); ");
                }
                else if (dr["T043_FIGURA"].ToString() == "3")
                {
                    //if (j % 2 == 0)
                    //    sbAux.Append("class='FA' ");
                    //else
                    //    sbAux.Append("class='FB' ");
                    //j++;

                    //sbAux.Append(" onclick=marcarUnaFila('tblCatalogoResponsable',this); ");
                    sbAux.Append(" ondblclick=this.className='FS';eliminarResponsables(); ");
                }
                else if (dr["T043_FIGURA"].ToString() == "4")
                {
                    //if (k % 2 == 0)
                    //    sbAux.Append("class='FA' ");
                    //else
                    //    sbAux.Append("class='FB' ");

                    //k++;

                    //sbAux.Append(" onclick=marcarUnaFila('tblCatalogoCoordinador',this); ");
                    sbAux.Append(" ondblclick=this.className='FS';eliminarCoordinadores(); ");
                }
                else if (dr["T043_FIGURA"].ToString() == "5")
                {
                    //if (l % 2 == 0)
                    //    sbAux.Append("class='FA' ");
                    //else
                    //    sbAux.Append("class='FB' ");

                    //l++;

                    //sbAux.Append(" onclick=marcarUnaFila('tblCatalogoTecnico',this); ");
                    sbAux.Append(" ondblclick=this.className='FS';eliminarTecnicos(); ");
                }

                sbAux.Append(" style='cursor: pointer;height:14px'>");

                sbAux.Append("<td width='100%'>&nbsp;&nbsp;" + dr["RECURSO"].ToString());
                sbAux.Append("</td></tr>" + (char)13);

                if (dr["T043_FIGURA"].ToString() == "2")
                    strBuilderSolicitante.Append(sbAux.ToString());
                else if (dr["T043_FIGURA"].ToString() == "3")
                    strBuilderResponsable.Append(sbAux.ToString());
                else if (dr["T043_FIGURA"].ToString() == "4")
                    strBuilderCoordinador.Append(sbAux.ToString());
                else if (dr["T043_FIGURA"].ToString() == "5")
                    strBuilderTecnico.Append(sbAux.ToString());
            }

            dr.Close();
            dr.Dispose();

            strBuilderResponsable.Append("</table>");
            strBuilderCoordinador.Append("</table>");
            strBuilderSolicitante.Append("</table>");
            strBuilderTecnico.Append("</table>");

            strTablaHtmlResponsable = strBuilderResponsable.ToString();
            strTablaHtmlCoordinador = strBuilderCoordinador.ToString();
            strTablaHtmlSolicitante = strBuilderSolicitante.ToString();
            strTablaHtmlTecnico = strBuilderTecnico.ToString();
        }

        protected string ObtenerCptos(string strConcepto, string sIdArea)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

                int i = 0;

                sbuilder.Append("<table id='tblCatValores' style='width: 500px;'>" + (char)13);
                sbuilder.Append("<colgroup><col style='width:425px;' /><col style='width:75px;' /></colgroup>");
// ojo con las altas el área tiene que existir poder grabar y leer.
                if (sIdArea != "") 
                {
                    if (strConcepto == "Tipo")
                        dr = TIPO.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Alcance")
                        dr = ALCANCE.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Proceso")
                        dr = PROCESO.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Producto")
                        dr = PRODUCTO.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Requisito")
                        dr = REQUISITO.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Causa")
                        dr = CAUSA.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Origen")
                        dr = ORIGEN.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);
                    else if (strConcepto == "Entrada")
                        dr = ENTRADA.Catalogo(null, short.Parse(sIdArea), "", null, 4, 0);

                    string sMaxLen = "50";
                    if (strConcepto == "Entrada") sMaxLen = "100";

                    while (dr.Read())
                    {
                        i++;
                        sbuilder.Append("<tr id='" + dr["ID"].ToString() + "' bd='' ");

                        sbuilder.Append(" onmouseover='TTip(event)' onclick=mm(event); ");

                        if ( (strConcepto == "Entrada") && !(hdnModoLectura.Text == "1" || (hdnPromotor.Text != hdnIDFICEPI.Text && hdnAdmin.Text != "A")))
                            sbuilder.Append(" class='MA' ondblclick=Det_Entrada(this); ");

                        sbuilder.Append(" style='height:22px'>");

                        if (strConcepto == "Entrada")
                        {
                            sbuilder.Append("<td style='padding-left:5px'><nobr class='NBR W425'>" + dr["DESCRIPCION"].ToString() + "</nobr></td>");
                            sbuilder.Append("<td>&nbsp;&nbsp;" + dr["ORDEN"].ToString() + "</td>");
                        }
                        else
                        {
                            if (hdnModoLectura.Text == "1" || (hdnPromotor.Text != hdnIDFICEPI.Text && hdnAdmin.Text != "A"))
                            {
                                sbuilder.Append("<td><input type='text' id='txtcpto" + i.ToString() + "' class='txtL' style='width:420px' value='" + dr["DESCRIPCION"].ToString() + "' MaxLength='" + sMaxLen + "' readonly=true></td>");
                                sbuilder.Append("<td><input type='text' id='txtOrden" + i.ToString() + "' class='txtL'  style='width:70px' value='" + dr["ORDEN"].ToString() + "' MaxLength='4' readonly=true></td>");
                            }
                            else
                            {
                                sbuilder.Append("<td><input type='text' id='txtcpto" + i.ToString() + "' class='txtL' onFocus=\"inFoco(this);\" onBlur=\"outFoco(this);\" style='width:420px' value='" + dr["DESCRIPCION"].ToString() + "' onKeyUp='actualizarDatos(this);' MaxLength='" + sMaxLen + "'></td>");
                                sbuilder.Append("<td><input type='text' id='txtOrden" + i.ToString() + "' class='txtL' onFocus=\"inFoco(this);\" onBlur=\"outFoco(this);\" style='width:70px' value='" + dr["ORDEN"].ToString() + "' onKeyUp='actualizarDatos(this);' MaxLength='4'></td>");
                            }
                        }
                        sbuilder.Append("</tr>");
                    }

                    dr.Close();
                    dr.Dispose();
                }
                sbuilder.Append("</table>");

                return sbuilder.ToString();
            }
            catch (System.Exception objError)
            {
                return "N@@" + Errores.mostrarError("Error al leer los conceptos.", objError);
            }
        }
        private string ObtenerDocumentos(string sIdArea)
        {
    		try
			{
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

                SqlDataReader dr = DOCAREA.Catalogo(int.Parse(sIdArea));

                sbuilder.Append("<table id='tblDocumentos' style='width:870px;'>");
                sbuilder.Append("<colgroup><col style='width:280px;' /><col style='width:225px' /><col style='width:215px' /><col style='width:'150px' /></colgroup>");
                int i = 0;
                while (dr.Read())
                {
                    if ( ((bool)dr["t083_privado"]) && (int.Parse(Session["IDFICEPI"].ToString()) != int.Parse(dr["t083_autor"].ToString())) && (hdnAdmin.Text!="A") ) continue;

                    //if (i % 2 == 0) sbuilder.Append("<tr class=FA ");
                    //else sbuilder.Append("<tr class=FB ");

                    sbuilder.Append("<tr id='" + dr["t083_iddocut"].ToString() + "' onclick='mm2(event);' sTipo='A' sAutor='" + dr["t083_autor"].ToString() + "' style='height:20px'>");

                    sbuilder.Append("<td style='padding-left:5px;'");

                    //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
                    if ((dr["t083_autor"].ToString() == Session["IDFICEPI"].ToString() ) 
                        || (!(bool)dr["t083_privado"] && !(bool)dr["t083_modolectura"])
                        || (hdnAdmin.Text == "A")
                       )
                        sbuilder.Append("ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\" ");

                    sbuilder.Append(" title=\"'" + dr["t083_descripcion"].ToString() + "\" ><nobr class='NBR' style='width:280px;text-overflow:ellipsis;overflow:hidden'>" + dr["t083_descripcion"].ToString() + "</nobr></td>");

                    if (dr["t083_nombrearchivo"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        //sbuilder.Append("<td><img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.sTipo, this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + dr["t083_nombrearchivo"].ToString() + "\">&nbsp;<nobr class='NBR' style='width:195px;text-overflow:ellipsis;overflow:hidden' title=\"" + dr["t083_nombrearchivo"].ToString() + "\">" + dr["t083_nombrearchivo"].ToString() + "</nobr></td>");
                        sbuilder.Append("<td>");
                        ////Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                        if ( (!(bool)dr["t083_privado"]) || 
                             ( (bool)dr["t083_privado"] && ( dr["t083_autor"].ToString() == Session["IDFICEPI"].ToString()||hdnAdmin.Text == "A") )
                            )
                            sbuilder.Append("<img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + dr["t083_nombrearchivo"].ToString() + "\">");

                        sbuilder.Append("&nbsp;<nobr class='NBR' style='width:195px;text-overflow:ellipsis;overflow:hidden' title=\"" + dr["t083_nombrearchivo"].ToString() + "\">" + dr["t083_nombrearchivo"].ToString() + "</nobr></td>");
                    }

                    if (dr["t083_weblink"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        string sHTTP = "";
                        if (dr["t083_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                        sbuilder.Append("<td title='" + dr["t083_weblink"].ToString() + "'><a href='" + sHTTP + dr["t083_weblink"].ToString() + "'><nobr class='NBR' style='width:205px;text-overflow:ellipsis;overflow:hidden'>" + dr["t083_weblink"].ToString() + "</nobr></a></td>");
                    }

                    sbuilder.Append("<td title='" + dr["autor"].ToString() + "'><nobr class='NBR' style='width:135px;text-overflow:ellipsis;overflow:hidden'>" + dr["autor"].ToString() + "</nobr></td>");

                    i++;
                }
                dr.Close();
                dr.Dispose();

                sbuilder.Append("</table>");

                return sbuilder.ToString();
            }
            catch (Exception ex)
            {
                return "N@@" + Errores.mostrarError("Error al obtener los documentos", ex);
            }
        }
        protected string EliminarDocumentos(string strIdsDocs)
        {
            string sResul = "";

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);                
                sResul = "N@@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            try
            {

                #region eliminar documentos

                string[] aDocs = Regex.Split(strIdsDocs, "##");

                foreach (string oDoc in aDocs)
                {
                    DOCAREA.Delete(tr, int.Parse(oDoc));
                }

                #endregion

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "N@@" + Errores.mostrarError("Error al eliminar los documentos", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }
        protected string ObtenerRecursos(string strApellido1, string strApellido2, string strNombre, int intColuma, int intOrden)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();
                dr = null;
                dr = Recursos.CargarRecursos(strApellido1, strApellido2, strNombre, intColuma, intOrden);
                sbuilder.Append("<table id='tblCatalogo' style='width:390px;text-align:left'>" + (char)13);
                int i = 0;

                while (dr.Read())
                {
                    sbuilder.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + "' ");

                    //if (i % 2 == 0)
                    //    sbuilder.Append("class='FA' ");
                    //else
                    //    sbuilder.Append("class='FB' ");

                    sbuilder.Append(" onclick=ms(this); ");
                    //sbuilder.Append(" ondblclick=this.className='FS'; ");//anadirTecnicos();

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
        private string Nom_Apellidos(string sApellNombre)
        {
            string sNombre = "";
            int intPosi;
            int intLong;
            intPosi = sApellNombre.IndexOf(", ");

            if (intPosi != -1)
            {
                intLong = (sApellNombre.Length - 1) - intPosi - 1;
                sNombre = sApellNombre.Substring(intPosi + 2, intLong) + " " + sApellNombre.Substring(0, intPosi);
            }
            else
            {
                sNombre = sApellNombre;
            }
            return sNombre;
        }
        private string Grabar(byte byteNueva, string strNombreArea, string strDescripcion, byte byteCorreo, 
            byte byteEstado, byte byteCategoria, byte byteSelCoord, byte byteResuelta, string sResponsablesIn, 
            string sCoordinadoresIn, string sSolicitantesIn, string sTecnicosIn, string sResponsables, 
            string sCoordinadores, string sSolicitantes, string sTecnicos, string sTipo, string sAlcance,
            string sProceso, string sProducto, string sRequisito, string sCausa, string sOrigen, string sEntrada,
            string strPromotor, string strIDArea, string strCorreo, string strPromotorCorreo, string strPromotorCorreoOld,
            string sPermitirCambios, string sAutoaprobacion)
        {
            string[] aResponsablesIn = Regex.Split(sResponsablesIn, ",");
            string[] aCoordinadoresIn = Regex.Split(sCoordinadoresIn, ",");
            string[] aSolicitantesIn = Regex.Split(sSolicitantesIn, ",");
            string[] aTecnicosIn = Regex.Split(sTecnicosIn, ",");

            string[] aResponsables = Regex.Split(sResponsables, ",");
            string[] aCoordinadores = Regex.Split(sCoordinadores, ",");
            string[] aSolicitantes = Regex.Split(sSolicitantes, ",");
            string[] aTecnicos = Regex.Split(sTecnicos, ",");

            string strMensaje = "";
            string strAsunto = "";
            string strTO;
            string sResul = "";

//SqlConnection conexion = GESTAR.Capa_Negocio.Conexion.Abrir();
//SqlTransaction transaccion = GESTAR.Capa_Negocio.Conexion.AbrirTransaccion(conexion);

            if (strIDArea == "-1") strIDArea = hdnIDArea.Text;
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

// Datos Generales


            if (byteNueva == 0)
            {
                try
                {
                    Areas.Actualizar(tr, "U", int.Parse(strIDArea), strNombreArea, byteCorreo, strDescripcion, int.Parse(strPromotor), byteCategoria, byteEstado, byteSelCoord, byteResuelta, (sPermitirCambios=="1")? true:false, (sAutoaprobacion=="1")? true:false);
                    intContador = int.Parse(hdnIDArea.Text);

                    //if (int.Parse(rdlEstado.SelectedValue) == 1) intContador = -1;
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al modificar el área.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }
            else
            {
                try
                {
                    intContador = Areas.Actualizar(tr, "I", 0, strNombreArea, byteCorreo, strDescripcion, int.Parse(strPromotor), byteCategoria, byteEstado, byteSelCoord, byteResuelta, (sPermitirCambios == "1") ? true : false, (sAutoaprobacion == "1") ? true : false);
                    hdnIDArea.Text = intContador.ToString();
                    strIDArea = hdnIDArea.Text;
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al crear el área.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }

// Integrantes

            //catch (Exception ex)
            //{
            //    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr);
            //    sResul = "N@@" + Errores.mostrarError("Error al grabar los datos del concepto", ex);
            //}
            //finally
            //{
            //    GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
            //}

            //return sResul;


            try
            {
                Integrante.Eliminar(tr, int.Parse(strIDArea));
            }
            catch (System.Exception objError)
            {
                sResul = Errores.mostrarError("Error al borrar los Integrantes.", objError);
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                return "N@@" + sResul;
            }


            //ListDto<Integrante> ListaIntegrantes = new ListDto<Integrante>();
            //Integrante oInte;
            ////oInte.ID=int.Parse(aID[0]);
            ////oInte.CODRED=aID[0];
            ////oInte.ESCOORDINADOR="S";
            ////oInte.ESRESPONSABLE="N";
            ////oInte.ESSOLICITANTE="N";
            ////oInte.ESTECNICO="N";

            // Busca por un solo Criterio


            for (int j = 0; j < aCoordinadores.Length; j++)
            {
                if (aCoordinadores[j] == "") continue;
                try
                {
                    string[] aID = Regex.Split(aCoordinadores[j], "/");
                    Integrante.Insertar(tr, int.Parse(strIDArea), int.Parse(aID[0]), "4");                   
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al insertar los Coordinadores.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }

            for (int j = 0; j < aResponsables.Length; j++)
            {
                if (aResponsables[j] == "") continue;

                try
                {
                    string[] aID = Regex.Split(aResponsables[j], "/");
                    Integrante.Insertar(tr, int.Parse(strIDArea), int.Parse(aID[0]), "3");
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al insertar los Responsables.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }

            for (int j = 0; j < aSolicitantes.Length; j++)
            {
                if (aSolicitantes[j] == "") continue;
                try
                {
                    string[] aID = Regex.Split(aSolicitantes[j], "/");
                    Integrante.Insertar(tr, int.Parse(strIDArea), int.Parse(aID[0]), "2");
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al insertar los Solicitantes.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }

            for (int j = 0; j < aTecnicos.Length; j++)
            {
                if (aTecnicos[j] == "") continue;

                try
                {
                    string[] aID = Regex.Split(aTecnicos[j], "/");
                    Integrante.Insertar(tr, int.Parse(strIDArea), int.Parse(aID[0]), "5");
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al insertar los Técnicos.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }
 
            try
            {
                GrabarCpto(sTipo, "Tipo", strIDArea);
                GrabarCpto(sAlcance, "Alcance", strIDArea);
                GrabarCpto(sProceso, "Proceso", strIDArea);
                GrabarCpto(sProducto, "Producto", strIDArea);
                GrabarCpto(sRequisito, "Requisito", strIDArea);
                GrabarCpto(sCausa, "Causa", strIDArea);
                GrabarCpto(sOrigen, "Origen", strIDArea);
                GrabarCpto(sEntrada, "Entrada", strIDArea);

                GESTAR.Capa_Negocio.Conexion.CommitTransaccion(tr);
                sResul = "";
            }
            catch (System.Exception objError)
            {
                sResul = Errores.mostrarError("Error al insertar los conceptos.", objError);
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                sResul = "N@@" + sResul;
            }
            finally
            {
                GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            
            // ENVIO DE CORREO

            if (sResul != "") return sResul;
 
            string[] aCorreo = Regex.Split(strCorreo, "///");
            strAsunto = "Modificación de los integrantes";
            strMensaje = "";

            foreach (string oCorreo in aCorreo)
            {
                if (oCorreo == "") break;
                string[] aDetCorreo = Regex.Split(oCorreo, "##");

                ///aValoresCpto[0] = id;
                ///aValoresCpto[1] = codred;
                ///aValoresCpto[2] = esResponsable;
                ///aValoresCpto[3] = esCoordinador;
                ///aValoresCpto[4] = esNotificador;
                ///aValoresCpto[5] = esTecnico;

                if ((aDetCorreo[2] == "" || aDetCorreo[2] == "B") &&
                      (aDetCorreo[3] == "" || aDetCorreo[3] == "B") &&
                      (aDetCorreo[4] == "" || aDetCorreo[4] == "B") &&
                      (aDetCorreo[5] == "" || aDetCorreo[5] == "B")
                    )
                {
                    strMensaje = "<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += "<tr><td>";
                    //strMensaje += Session["NOMBRE"] + @" le informa que actualmente ya no tiene ninguna responsabilidad dentro del área: " + strNombreArea + ".";
                    strMensaje += Session["NOMBRE"] + @" ha modificado la relación de integrantes del área: '" + strNombreArea + "'.<br><br>";
                    strMensaje += @" Ud. ya no tiene asignada ninguna figura.";
                    strMensaje += "</td></tr>";
                }
                else
                {
                    strMensaje = "<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += "<tr><td>";
                    //strMensaje += Session["NOMBRE"] + @" le informa que actualmente tiene las siguientes responsabilidades dentro del área: " + strNombreArea + ":";
                    strMensaje += Session["NOMBRE"] + @" ha modificado la relación de integrantes del área: '" + strNombreArea + "'.<br><br>";
                    strMensaje += @" Ud. tiene asignadas las siguientes figuras: <br><br>";
                    strMensaje += "</td></tr>";

                    //if ((hdnPromotor.Text == aDetCorreo[0]) || (hdnPromotor.Text == "" && aDetCorreo[0] == Session["IDFICEPI"].ToString()))
                    if (hdnPromotor.Text == aDetCorreo[0])
                    {
                        strMensaje += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        strMensaje += "- Promotor.";
                        strMensaje += "</td></tr>";
                    }

                    if (aDetCorreo[2] != "" && aDetCorreo[2] != "B")
                    {
                        strMensaje += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        strMensaje += "- Responsable.";
                        strMensaje += "</td></tr>";                        
                    }
                    if (aDetCorreo[3] != "" && aDetCorreo[3] != "B")
                    {
                        strMensaje += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        strMensaje += "- Coordinador.";
                        strMensaje += "</td></tr>";
                    }
                    if (aDetCorreo[4] != "" && aDetCorreo[4] != "B")
                    {
                        strMensaje += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        strMensaje += "- Solicitante.";
                        strMensaje += "</td></tr>";
                    }
                    if (aDetCorreo[5] != "" && aDetCorreo[5] != "B")
                    {
                        strMensaje += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        strMensaje += "- Especialista.";
                        strMensaje += "</td></tr>";
                    }

                    strMensaje += "</table>";
                }

                if (aDetCorreo[1].Trim() == "")
                {
                    strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aDetCorreo[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                    strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                    strTO = "EDA";
                }
                else
                {
                    //strAsunto = " Usuario ( " + aDetCorreo[0] + "/" + aDetCorreo[1];
                    strTO = aDetCorreo[1];
                }

                Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
            }


            // NOTIFICACIÓN AL PROMOTOR ( NUEVO Y AL ANTIGUO)

            if ((strPromotorCorreoOld != "") && (strPromotorCorreoOld != strPromotorCorreo) && (byteNueva == 0))
            {

                // NUEVO PROMOTOR

                string[] aID = Regex.Split(strPromotorCorreo, "/");
                string[] aID2 = Regex.Split(strPromotorCorreoOld, "/");

                strAsunto = "Cambio de promotor";

                strMensaje = "<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                strMensaje += "<tr><td>";
                //strMensaje += Session["NOMBRE"] + @" le informa que actualmente ya no tiene ninguna responsabilidad dentro del área: " + strNombreArea + ".";
                strMensaje += @"El área '" + strNombreArea + "' ha cambiado de promotor.<br><br>";
                strMensaje += @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<LABEL class='TITULO'>ANTIGUO PROMOTOR:</LABEL>&nbsp;&nbsp;" + Nom_Apellidos(aID2[2]) + "<br>";
                strMensaje += @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<LABEL class='TITULO'>NUEVO   PROMOTOR:</LABEL>&nbsp;&nbsp;" + Nom_Apellidos(aID[2]);
                strMensaje += "</td></tr>";
                strMensaje += "</table>";

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

                // ANTIGUO PROMOTOR

                if (aID2[1].Trim() == "")
                {
                    strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aID2[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                    strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                    strTO = "EDA";
                }
                else
                {
                    strTO = aID2[1];
                }
                Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
            }

            return sResul;
        }

        protected void GrabarCpto(string strDatosCpto,string strOpcion, string strIDArea)
        {
            string[] aCPTO = Regex.Split(strDatosCpto, "///");

            foreach (string oCPTO in aCPTO)
            {
                if (oCPTO == "") break;
                string[] aValoresCpto = Regex.Split(oCPTO, "##");

                ///aValoresCpto[0] = opcionBD;
                ///aValoresCpto[1] = id;
                ///aValoresCpto[2] = Descripcion;
                ///aValoresCpto[3] = Orden;

                switch (aValoresCpto[0])
                {
                    case "I":
                        if (strOpcion == "Tipo")
                            TIPO.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Alcance")
                            ALCANCE.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Proceso")
                            PROCESO.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Producto")
                            PRODUCTO.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Requisito")
                            REQUISITO.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Causa")
                            CAUSA.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Origen")
                            ORIGEN.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        //else if (strOpcion == "Importancia")
                        //    IMPORTANCIA.Insert(tr, int.Parse(strIDArea), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        
                        break;
                    case "U":
                        if (strOpcion == "Tipo")
                            TIPO.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Alcance")
                            ALCANCE.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Proceso")
                            PROCESO.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Producto")
                            PRODUCTO.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Requisito")
                            REQUISITO.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Causa")
                            CAUSA.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        else if (strOpcion == "Origen")
                            ORIGEN.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));
                        //else if (strOpcion == "Importancia")
                        //    IMPORTANCIA.Update(tr, int.Parse(aValoresCpto[1]), aValoresCpto[2], short.Parse(aValoresCpto[3]));

                        break;
                    case "D":
                        if (strOpcion == "Tipo")
                            TIPO.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Alcance")
                            ALCANCE.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Proceso")
                            PROCESO.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Producto")
                            PRODUCTO.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Requisito")
                            REQUISITO.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Causa")
                            CAUSA.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Origen")
                            ORIGEN.Delete(tr, int.Parse(aValoresCpto[1]));
                        else if (strOpcion == "Entrada")
                            ENTRADA.Delete(tr, short.Parse(aValoresCpto[1]));                
                        break;
                }
            }
        }					

        private string ObtenerOrdenesParaCambio(string sIdArea)
        {
    		try
			{
                StringBuilder sb = new StringBuilder();

                SqlDataReader dr = Areas.ObtenerOrdenesParaCambios(int.Parse(sIdArea));

                sb.Append(@"<table id='tblOrdenes' class='texto MANO' style='width: 1140px;height: 17px; z-index:5;' mantenimiento='1'>
	                        <colgroup>
	                            <col style='width:70px;' />
	                            <col style='width:200px;' />
	                            <col style='width:130px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px;' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px;' />
                                <col style='width:110px' />
	                        </colgroup>");

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t044_iddeficiencia"].ToString() + "' onclick='msmant(this);iio(this);' style='height:20px;'>");
                    sb.Append("<td style='text-align:right; padding-right: 10px;' >" + int.Parse(dr["t044_iddeficiencia"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td style='text-align:left;'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["T044_ASUNTO"].ToString() + "</nobr></td>");
                    sb.Append("<td>" + ((dr["T044_FECALTA"] != DBNull.Value)? ((DateTime)dr["T044_FECALTA"]).ToString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_NOTIFICADA"] != DBNull.Value)? ((DateTime)dr["T044_NOTIFICADA"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FECLIMITE"] != DBNull.Value)? ((DateTime)dr["T044_FECLIMITE"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FECPACT"] != DBNull.Value)? ((DateTime)dr["T044_FECPACT"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FINIPREV"] != DBNull.Value)? ((DateTime)dr["T044_FINIPREV"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FFINPREV"] != DBNull.Value)? ((DateTime)dr["T044_FFINPREV"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((dr["T044_TIEMPOESTI"] != DBNull.Value) ? double.Parse(dr["T044_TIEMPOESTI"].ToString()).ToString() : "") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FECINIREAL"] != DBNull.Value)? ((DateTime)dr["T044_FECINIREAL"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FECFINREAL"] != DBNull.Value)? ((DateTime)dr["T044_FECFINREAL"]).ToShortDateString():"") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:10px;'>" + ((dr["T044_TIEMPOINVER"] != DBNull.Value) ? double.Parse(dr["T044_TIEMPOINVER"].ToString()).ToString() : "") + "</td>");
                    sb.Append("<td>" + ((dr["T044_FECHAMODIF"] != DBNull.Value) ? ((DateTime)dr["T044_FECHAMODIF"]).ToString() : "") + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                return "OK@#@"+sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener las órdenes", ex);
            }
        }
        private string getTareas(string sIdOrden)
        {
    		try
			{
                StringBuilder sb = new StringBuilder();

                SqlDataReader dr = Deficiencias.ObtenerTareasParaCambios(int.Parse(sIdOrden));

                sb.Append(@"<table id='tblTareas' class='texto MANO' style='width: 550px; BORDER-COLLAPSE: collapse; table-layout:fixed; height: 17px; z-index:5;' cellSpacing='0' cellpadding='0' border='0' mantenimiento='1'>
	                        <colgroup>
	                            <col style='width:70px; text-align: right; padding-right: 10px;' />
	                            <col style='width:200px; text-align:left;' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                            <col style='width:70px' />
	                        </colgroup>");

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["T072_IDTAREA"].ToString() + "' onclick='msmant(this);iit(this);' style='height:20px;'>");
                    sb.Append("<td>" + int.Parse(dr["T072_IDTAREA"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["T072_DENOMINACION"].ToString() + "</nobr></td>");
                    sb.Append("<td>" + ((dr["T072_FECINIPREV"] != DBNull.Value) ? ((DateTime)dr["T072_FECINIPREV"]).ToShortDateString() : "") + "</td>");
                    sb.Append("<td>" + ((dr["T072_FECFINPREV"] != DBNull.Value) ? ((DateTime)dr["T072_FECFINPREV"]).ToShortDateString() : "") + "</td>");
                    sb.Append("<td>" + ((dr["T072_FECINIREAL"] != DBNull.Value) ? ((DateTime)dr["T072_FECINIREAL"]).ToShortDateString() : "") + "</td>");
                    sb.Append("<td>" + ((dr["T072_FECFINREAL"] != DBNull.Value) ? ((DateTime)dr["T072_FECFINREAL"]).ToShortDateString() : "") + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "n@@" + Errores.mostrarError("Error al obtener las tareas", ex);
            }
        }
        private string getCronologia(string sIdOrden)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                SqlDataReader dr = CRONOLOGIA.Catalogo((int?)int.Parse(sIdOrden));

                sb.Append(@"<table id='tblCronologia' class='texto MANO' style='width: 280px; BORDER-COLLAPSE: collapse; table-layout:fixed; height: 17px; z-index:5;' mantenimiento='1'>
	                        <colgroup>
	                            <col style='width:160px;' />
	                            <col style='width:120px;' />
	                        </colgroup>");

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t096_idcronologia"].ToString() + "' onclick='msmant(this);iic(this);' style='height:20px;'>");
                    sb.Append("<td style='padding-left:2px;'><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["T044_ESTADO"].ToString() + "</nobr></td>");
                    sb.Append("<td>" + ((DateTime)dr["T096_FECHA"]).ToString() + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "n@@" + Errores.mostrarError("Error al obtener las tareas", ex);
            }
        }
        protected string setDato(string sTipo, string sOpcion, string sIDElemento, string sValor)
        {
            try
            {
                if (sOpcion != "7" && sOpcion != "10" && sValor != "")
                {
                    try { DateTime o = DateTime.Parse(sValor); }
                    catch (Exception ex) { return "N@@La cadena introducida no se corresponde con una fecha válida."; }
                }
                Areas.setDatoCambio(sTipo, int.Parse(sOpcion), int.Parse(sIDElemento), sValor);
                return "";
            }
            catch (Exception ex)
            {
                return "N@@"+ ex.Message;
            }

        }
    }
}
