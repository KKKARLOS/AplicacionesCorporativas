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
using Microsoft.JScript;
using System.Text.RegularExpressions;

namespace GESTAR.Capa_Presentacion.ASPX
{
	/// <summary>
	/// Summary description for Formacion.
	/// </summary>
    public partial class MtoEntradas : System.Web.UI.Page, ICallbackEventHandler
	{
        private string _callbackResultado = null;
        SqlDataReader dr_resultado = null;
        public SqlConnection oConn;
        public SqlTransaction tr;
        private int intContador = 0;


		protected void Page_Load(object sender, System.EventArgs e)
		{        
            if (!Page.IsCallback)
            {
                try
                {
                    hdnIDArea.Text = Request.QueryString["IDAREA"].ToString();

                    Utilidades.SetEventosFecha(this.txtFechaAnalisis);
                    
                    if (Request.QueryString["bNueva"].ToString() == "false")
                    {
                        hdnIDEntrada.Text = Request.QueryString["IDENTRADA"].ToString();
                        dr_resultado = null;
                        dr_resultado = ENTRADA.Select(null, short.Parse(Request.QueryString["IDENTRADA"]));
                        if (dr_resultado.Read())
                        {
                            txtDenominacion.Text = (string)dr_resultado["T074_DENOMINACION"];
                            txtCreador.Text = (string)dr_resultado["CREADOR"];
                            txtOrigen.Text = (string)dr_resultado["ORIGEN"];
                            txtComunicante.Text = (string)dr_resultado["T074_COMUNICANTE"];
                            txtMedio.Text = (string)dr_resultado["T074_MEDIO"];
                            txtOrganizacion.Text = (string)dr_resultado["T074_ORGANIZACION"];
                            txtAnalista.Text = (string)dr_resultado["ANALISTA"];
                            hdnAnalista.Text = dr_resultado["T074_ANALISTA"].ToString();
                            hdnOrigen.Text = dr_resultado["T075_ORIGEN"].ToString();
                            txtOrden.Text = dr_resultado["T074_ORDEN"].ToString();

                            if (dr_resultado["T074_FECHAANAL"] == System.DBNull.Value)
                                txtFechaAnalisis.Text = "";
                            else
                                txtFechaAnalisis.Text = ((DateTime)dr_resultado["T074_FECHAANAL"]).ToShortDateString();

                            txtDescripcion.Text = (string)dr_resultado["T074_DESCRIPCION"];
                            txtNotas.Text = (string)dr_resultado["T074_NOTAS"];
                        }
                        dr_resultado.Close();
                        dr_resultado.Dispose();
                        dr_resultado = null;

                        hdnModoLectura.Text = Session["MODOLECTURA"].ToString();

                        if (hdnModoLectura.Text == "1")
                        {
                            Control Area = this.FindControl("frmDatos");
                            ModoLectura.Poner(Area.Controls);
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    hdnErrores.Text = Errores.mostrarError("Error al obtener los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context");
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            //this.txtIdEntrada.Attributes.Add("readonly", "readonly");
            //txtDescripcion.Attributes.Add("readonly", "readonly");
            //txtFechaInicio.Attributes.Add("readonly", "readonly");
            //txtFechaFin.Attributes.Add("readonly", "readonly");


			// Put user code to initialize the page here
            //if(!this.IsPostBack){
            //    dr_resultado = null;
            //    Provincia objProvincia = new Provincia();
            //    dr_resultado = objProvincia.Catalogo();

            //    ddlProvincia.DataSource = dr_resultado;
            //    ddlProvincia.DataTextField = "DESCRIPCION";
            //    ddlProvincia.DataValueField = "CODIGO";
            //    ddlProvincia.DataBind();
            //    ddlProvincia.Items.Insert(0, new ListItem("", "0"));

            //    if (Request.QueryString["bNueva"]=="false"){
            //        lblTituloEnlace.Visible=false;
            //        lblTituloSinEnlace.Visible=true;
            //        btnBorrarTitulo.Visible=false;
            //    }

            //    imgbtnGrabar.Attributes.Add("onclick", "return false");
            //    imgbtnCancelar.Attributes["onclick"] = "javascript:salir();";
            //    hdnbNueva.Text = Request.QueryString["bNueva"].ToString();
            //    if (hdnbNueva.Text == "false")
            //    {
            //        dr_resultado = null;
            //        Formacion objFormacion = new Formacion();
            //        dr_resultado = objFormacion.LeerUnRegistro(Request.QueryString["IDFICEPI"],int.Parse(Request.QueryString["IDCODTITULO"]));
            //        dr_resultado.Read();

            //        txtDescripcion.Text = (string)dr_resultado["T019_DESCRIPCION"];
            //        txtEspecialidad.Text = (string)dr_resultado["T012_ESPECIALIDAD"];
	
            //        if (dr_resultado["T012_FECINICIO"]==System.DBNull.Value)
            //            txtFechaInicio.Text = "";
            //        else
            //            txtFechaInicio.Text = ((DateTime)dr_resultado["T012_FECINICIO"]).ToShortDateString();

            //        if (dr_resultado["T012_FECFIN"]==System.DBNull.Value)
            //            txtFechaFin.Text = "";
            //        else
            //            txtFechaFin.Text = ((DateTime)dr_resultado["T012_FECFIN"]).ToShortDateString();

            //        txtCentroFormacion.Text = (string)dr_resultado["T012_CENTROFORMACION"];
            //        txtExpediente.Text = (string)dr_resultado["T012_EXPEDIENTE"];
            //        txtObservaciones.Text =(string)dr_resultado["T012_OBSERVACIONES"];
            //        ddlProvincia.SelectedValue = dr_resultado["T002_IDPROVIN"].ToString();
            //        lblFecultmodif.Text = dr_resultado["T012_FECULTMODIF"].ToString();
            //        chkIbermatica.Checked = (bool)dr_resultado["T012_IBERMATICA"];
            //        dr_resultado.Close();

            //        SqlDataReader dr_resultado2 = null;
            //        dr_resultado2 = null;
            //        Formacion objFormacion2 = new Formacion();
            //        dr_resultado2 = objFormacion2.LeerFUMA(Request.QueryString["IDFICEPI"]);
            //        dr_resultado2.Read();
            //        lblFecultmodifApar.Text = dr_resultado2["T012_FECULTMODIFA"].ToString();
            //        dr_resultado.Close();
            //        dr_resultado.Dispose();					
            //    }
            //    else
            //    {
            //        lblFecultmodif.Width = Unit.Pixel(93);
            //    }
            //}

            //strPerfilCV = Session["PERFIL_CV"].ToString();

            //if ( Session["PERFIL_CV"].ToString()!="4")
            //{
            //    ModoLectura.Poner(this.Controls);
            //}
            //btnBorrarTitulo.Attributes.Add("hidefocus","true");
            //btnBorrarFechaInicio.Attributes.Add("hidefocus","true");
            //btnBorrarFechaFin.Attributes.Add("hidefocus","true");
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
                    case "grabar":
                    strbTabla.Append(Grabar(byte.Parse(aArgs[1]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[2]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[3]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[4]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[5]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[6]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[7]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[8]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[9]), 
                        Microsoft.JScript.GlobalObject.unescape(aArgs[10]),
                        Microsoft.JScript.GlobalObject.unescape(aArgs[11]),
                        Microsoft.JScript.GlobalObject.unescape(aArgs[12])
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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{   
			//this.imgbtnGrabar.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnGrabar_Click);

		}
		#endregion

        private string Grabar( byte byteNueva, string sDenominacion, string sComunicante, string sMedio,
          	string sOrganizacion, string sDescripcion, string sNotas, string sFechaAnalisis, string sOrigen, 
            string sAnalista, string sIDEntrada, string sOrden)
        {
            string sResul;
            if (sIDEntrada == "-1") sIDEntrada = hdnIDEntrada.Text; ;

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

            int? intAnalista = null;
            if (sAnalista != "") intAnalista = int.Parse(sAnalista);
            short? intOrigen = null;
            if (sOrigen != "") intOrigen = short.Parse(sOrigen);

            DateTime? dFechaAnalisis = null;
            if (sFechaAnalisis != "") dFechaAnalisis = DateTime.Parse(sFechaAnalisis);

            if (byteNueva == 0)
            {
                try
                {
                    ENTRADA.Update(tr, short.Parse(sIDEntrada), sDenominacion,
                                                intOrigen, int.Parse(hdnIDArea.Text),
												sComunicante, sMedio, sOrganizacion,
                                                sDescripcion, intAnalista, dFechaAnalisis, sNotas,
												int.Parse(Session["IDFICEPI"].ToString()),
                                                short.Parse(sOrden));
                    intContador = int.Parse(hdnIDEntrada.Text);
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
                    intContador = ENTRADA.Insert(tr, sDenominacion,
                                                intOrigen, int.Parse(hdnIDArea.Text),
												sComunicante, sMedio, sOrganizacion,
                                                sDescripcion, intAnalista, dFechaAnalisis, sNotas,
												int.Parse(Session["IDFICEPI"].ToString()),
                                                short.Parse(sOrden));
                    hdnIDEntrada.Text = intContador.ToString();
                    sIDEntrada = hdnIDEntrada.Text;
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al crear el área.", objError);
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

            return sResul;
           
        }

    }
}
