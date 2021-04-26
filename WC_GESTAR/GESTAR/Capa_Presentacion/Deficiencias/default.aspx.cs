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

    public partial class DetalleDeficiencia : System.Web.UI.Page, ICallbackEventHandler
    {
    	protected string strTitulo;

        private string _callbackResultado = null;
        public SqlConnection oConn;
        public SqlTransaction tr;
        SqlDataReader dr = null;
        private int intContador = 0;
        public string strTablaHtmlCronologia, strTablaCatalogo, strTablaCatalogoDoc, strTablaCatalogoDocArea;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            int idEstado = 0;
            if (!Page.IsCallback)
            {
                #region Control sesión
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                #endregion
                try
                {
                    #region Atributos del usuario
                    // VEMOS SI EL USUARIO LOGADO ES PROMOTOR, RESPONSABLE, COORDINADOR Y POR OTRA PARTE IDENTIFICAMOS 
                    // LAS CUENTAS DE CORREO A NOTIFICAR DEL PROMOTOTOR ,LOS COORDINADORES 
                    // Y RESPONSABLES DE LA ORDEN

                    hdnFICEPI.Text = Session["IDFICEPI"].ToString();
                    hdnIDArea.Text = Request.QueryString["IDAREA"].ToString();
                    txtArea.Text = Request.QueryString["AREA"].ToString();
                    hdnModoLectura.Text = Request.QueryString["MODOLECTURA"].ToString();
                    hdnAdmin.Text = Request.QueryString["ADMIN"].ToString();

                    Session["MODOLECTURA"] = hdnModoLectura.Text;
                    hdnFechaAlta.Text = System.DateTime.Now.ToShortDateString();
                    //txtFechaAlta.ReadOnly = true;

                    if (Request.QueryString["SOLICITANTE"].ToString() == "1") hdnEsSolicitante.Text = "true";
                    else hdnEsSolicitante.Text = "false";

                    if (Request.QueryString["COORDINADOR"].ToString() == "1")
                    {
                        hdnEsCoordinador.Text = "true";
                        hdnCorreoCoordinador.Text = Session["IDFICEPI"].ToString() + "/" + Session["IDRED"].ToString() + "/" + Session["NOMBRE2"].ToString();
                    }
                    else
                    {
                       hdnEsCoordinador.Text = "false";
                       hdnCorreoCoordinador.Text = "";
                    }

                    hdnCoordinadorOld.Text = hdnCorreoCoordinador.Text;

                    if (hdnAdmin.Text == "A") hdnEsCoordinador.Text = "true";

                    dr = null;
                    dr = Areas.PromotorResponsableCoordinador(int.Parse(Request.QueryString["IDAREA"]));
                    
                    hdnEsPromotor_o_Responsable.Text = "false";

                    hdnCorreoPromotor.Text = "";
                    hdnCorreoResponsable.Text = "";
                    hdnCorreoCoordinadores.Text = "";
                    hdnCorreoResponsables.Text = "";

                    while (dr.Read())
                    {
                        if (dr["T001_IDFICEPI"].ToString() == Session["IDFICEPI"].ToString() && dr["T043_FIGURA"].ToString() != "4")
                        {
                            hdnEsPromotor_o_Responsable.Text = "true";
                        }
                        if (dr["T043_FIGURA"].ToString() == "1")
                        {
                            hdnCorreoPromotor.Text = dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString();
                            if ((bool)dr["T042_CORREO"])
                                hdnEnviarCorreoPromotor.Text = "1";
                            else
                                hdnEnviarCorreoPromotor.Text = "0";
                        }
                        if (dr["T001_IDFICEPI"].ToString() == Session["IDFICEPI"].ToString() && dr["T043_FIGURA"].ToString() == "3")
                        {
                            hdnCorreoResponsable.Text += dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + ',';
                        }
                        if (dr["T043_FIGURA"].ToString() == "3")
                        {
                            hdnCorreoResponsables.Text += dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + ',';
                        }
                        if (dr["T043_FIGURA"].ToString() == "4")
                        {
                            hdnCorreoCoordinadores.Text += dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString() + ',';
                        }
                    }
                    dr.Close();
                    dr.Dispose();
                    #endregion
                    strTitulo = "Detalle orden";

                    cboRtado.Items.Insert(0, new ListItem("", "0"));
                    cboRtado.Items[0].Selected = true;
                    cboAvance.Items.Insert(0, new ListItem("", "0"));
                    cboAvance.Items[0].Selected = true;
                   
                    if (Request.QueryString["bNueva"].ToString() == "false")
                    {
                        #region Acceso a registro existente
                        // si no tiene coordinador asignado y el que entra es un coordinador .Toma la Orden
                        #region es tecnico
                        hdnIDDefi.Text = Request.QueryString["IDDEFI"].ToString();

                        dr = null;
                        dr = Deficiencias.EsTecnicoDeficenciaTarea(null,int.Parse(hdnIDDefi.Text), int.Parse(Session["IDFICEPI"].ToString()));
                        if (dr.Read())
                        {
                            if (int.Parse(dr["CONTADOR"].ToString()) > 0) hdnEsTecnico.Text = "true";
                            else hdnEsTecnico.Text = "false";
                        }
                        dr.Close();
                        dr.Dispose();
                        #endregion
                        #region Solapa General
                        dr = null;
                        dr = Deficiencias.Select(null, int.Parse(hdnIDDefi.Text));
                        if (dr.Read())
                        {
                            txtIdDeficiencia.Text = dr["T044_IDDEFICIENCIA"].ToString();
                            txtDenominacion.Text = (string)dr["T044_ASUNTO"];
                            cboEstado.SelectedValue = dr["T044_ESTADO"].ToString();
                            hdnIdEstado.Text = dr["T044_ESTADO"].ToString();
                            idEstado = int.Parse(hdnIdEstado.Text);
                            //Compruebo si el usuario actual es coordinador de la Orden
                            if (dr["T044_COORDINADOR"].ToString() == Session["IDFICEPI"].ToString())
                            {
                                hdnEsCoordinador.Text = "true";
                                hdnCorreoCoordinador.Text = Session["IDFICEPI"].ToString() + "/" + Session["IDRED"].ToString() + "/" + Session["NOMBRE2"].ToString();
                            }
                            #region Modo Lectura
                            cboEstado.Items.Clear();
                            hdnDenEstado.Text = dr["ESTADO"].ToString();

                            if ((dr["T044_ESTADO"].ToString() == "0" || dr["T044_ESTADO"].ToString() == "4" || dr["T044_ESTADO"].ToString() == "7") && hdnEsSolicitante.Text != "true" && hdnCoordinador.Text !="true" && hdnAdmin.Text != "A")
                            {
                                hdnModoLectura.Text = "1";
                            }

                            if (hdnDenEstado.Text == "Aprobada" || hdnDenEstado.Text == "Anulada")
                            {
                                hdnModoLectura.Text = "1";
                            }
                            #endregion
                            #region Control del combo de estado
                            if (dr["T044_ESTADO"].ToString() == "0")
                            {
                                cboEstado.Items.Insert(0, new ListItem("Aparcada", "0"));
                                cboEstado.Items[0].Selected = true;
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "1")
                            {
                                cboEstado.Items.Add(new ListItem("Tramitada", "1"));
                                cboEstado.Items.Add(new ListItem("Pte de Aclaración", "2"));
                                cboEstado.Items.Add(new ListItem("Aceptada", "3"));
                                cboEstado.Items.Add(new ListItem("Rechazada", "4"));
                                cboEstado.Items.Add(new ListItem("En estudio", "5"));
                                cboEstado.Items.Add(new ListItem("Pte.Resolución", "6"));
                                cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "2")
                            {
                                cboEstado.Items.Add(new ListItem("Pte de Aclaración", "2"));
                            }
                            if (hdnEsSolicitante.Text == "true" && hdnEsCoordinador.Text == "false" && dr["T044_ESTADO"].ToString() == "2")
                            {
                                cboEstado.Items.Add(new ListItem("Pte de Aclaración", "2"));
                                lblEstado.Visible = true;
                                cboEstado.Enabled = false;
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "13")
                            {
                                cboEstado.Items.Add(new ListItem("Aclaración resuelta", "13"));
                                cboEstado.Items.Add(new ListItem("Pte de Aclaración", "2"));
                                cboEstado.Items.Add(new ListItem("Aceptada", "3"));
                                cboEstado.Items.Add(new ListItem("En estudio", "5"));
                                cboEstado.Items.Add(new ListItem("Pte.Resolución", "6"));
                                cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }

                            if ((hdnEsCoordinador.Text == "true" || hdnAdmin.Text == "A") && dr["T044_ESTADO"].ToString() == "3")
                            {
                                cboEstado.Items.Add(new ListItem("Aceptada", "3"));
                                cboEstado.Items.Add(new ListItem("En estudio", "5"));
                                cboEstado.Items.Add(new ListItem("Pte.Resolución", "6"));
                                cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "4")
                            {
                                cboEstado.Items.Add(new ListItem("Rechazada", "4"));
                                cboEstado.Enabled = false;
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "5")
                            {
                                cboEstado.Items.Add(new ListItem("En estudio", "5"));
                                cboEstado.Items.Add(new ListItem("Pte.Resolución", "6"));
                                cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }

                            if ((hdnEsCoordinador.Text == "true" || hdnAdmin.Text  == "A") && dr["T044_ESTADO"].ToString() == "6")
                            {
                                cboEstado.Items.Add(new ListItem("Pte.Resolución", "6"));
                                //cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "7")
                            {
                                cboEstado.Items.Add(new ListItem("Pte.Acep.Propuesta", "7"));
                                //cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                //cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                                cboEstado.Enabled = false;
                            }


                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "8")
                            {
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }


                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text =="A") && dr["T044_ESTADO"].ToString() == "9")
                            {
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                cboEstado.Enabled = false;
                                hdnModoLectura.Text = "1";
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text  == "A") && dr["T044_ESTADO"].ToString() == "10")
                            {
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                cboEstado.Items.Add(new ListItem("Resuelta", "9"));
                                cboEstado.Items.Add(new ListItem("No aprobada", "10"));
                                //cboEstado.Items.Add(new ListItem("Anulada", "12"));
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text  == "A") && dr["T044_ESTADO"].ToString() == "11")
                            {
                                cboEstado.Items.Add(new ListItem("Aprobada", "11"));
                                cboEstado.Enabled = false;
                            }

                            if ((hdnEsCoordinador.Text == "true"||hdnAdmin.Text  == "A") && dr["T044_ESTADO"].ToString() == "12")
                            {
                                cboEstado.Items.Add(new ListItem("Anulada", "12"));
                                cboEstado.Enabled = false;
                            }

                            if (dr["T044_ESTADO"].ToString() == "9" && (hdnEsSolicitante.Text == "true"||hdnAdmin.Text  == "A"))
                            {
                                //lblEstado.Visible = false;
                                //cboEstado.Visible = false;
                                cboEstado.Enabled = false;
                            }

                            if (hdnEsTecnico.Text == "true" && hdnEsCoordinador.Text == "false" && dr["T044_ESTADO"].ToString() == "8")
                            {
                                cboEstado.Items.Add(new ListItem("En resolución", "8"));
                                lblEstado.Visible = true;
                                cboEstado.Enabled = false;
                            }

                            if (cboEstado.Items.Count == 0)
                                cboEstado.Items.Add(new ListItem(hdnDenEstado.Text, dr["T044_ESTADO"].ToString()));

                            cboEstado.SelectedValue = dr["T044_ESTADO"].ToString();
                            #endregion
                            #region Otros datos
                            txtSolicitante.Text = (string)dr["SOLICITANTE"];
                            hdnSolicitante.Text = dr["T044_SOLICITANTE"].ToString();

                            if (hdnSolicitante.Text == Session["IDFICEPI"].ToString()) hdnEsSolicitante.Text = "true";
                            else hdnEsSolicitante.Text = "false";

                            if ((dr["T044_COORDINADOR"] == System.DBNull.Value || dr["T044_COORDINADOR"].ToString() == "0") && idEstado == 1 && hdnEsCoordinador.Text == "true")
                            {
                                if (Areas.VerSiEsCoordinadorArea(int.Parse(hdnIDArea.Text), int.Parse(Session["IDFICEPI"].ToString())) >= 1)
                                {
                                    Deficiencias.UpdateCoordi(null, int.Parse(hdnIDDefi.Text), int.Parse(Session["IDFICEPI"].ToString()));
                                    hdnCoordinador.Text = Session["IDFICEPI"].ToString();
                                    txtCoordinador.Text = Session["NOMBRE2"].ToString();
                                }
                            }
                            else
                            {
                                hdnCoordinador.Text = dr["T044_COORDINADOR"].ToString();
                                txtCoordinador.Text = (string)dr["COORDINADOR"];
                            }

                            if (hdnEsCoordinador.Text == "true" && hdnEsSolicitante.Text == "false" && hdnCoordinador.Text != Session["IDFICEPI"].ToString())
                            //if (hdnEsCoordinador.Text == "true" && hdnCoordinador.Text != Session["IDFICEPI"].ToString() && hdnEsSolicitante.Text == "false")
                            {
                                hdnModoLectura.Text = "1";
                                Session["MODOLECTURA"] = hdnModoLectura.Text;
                            }

                            //if (hdnCoordinador.Text != Session["IDFICEPI"].ToString() && hdnCoordinador.Text=="" && hdnEsCoordinador.Text == "true")
                            //    hdnCoordinador.Text = Session["IDFICEPI"].ToString();

                            if (dr["T044_COORDINADOR"].ToString() != "0" && dr["T044_COORDINADOR"] != System.DBNull.Value)
                            {
                                //hdnCorreoCoordinador.Text = dr["T044_COORDINADOR"].ToString() + "/" + dr["CORREO_COORDINADOR"].ToString() + "/" + dr["COORDINADOR"].ToString();
                                //hdnCoordinadorOld.Text = hdnCorreoCoordinador.Text;
                                hdnCoordinadorOld.Text = dr["T044_COORDINADOR"].ToString() + "/" + dr["CORREO_COORDINADOR"].ToString() + "/" + dr["COORDINADOR"].ToString();
                                hdnCorreoCoordinador.Text = hdnCoordinadorOld.Text;
                            }
                            hdnCorreoSolicitante.Text = dr["T044_SOLICITANTE"].ToString() + "/" + dr["CORREO_SOLICITANTE"].ToString();
                            if (dr["T044_FECALTA"] == System.DBNull.Value)
                                txtFechaAlta.Text = "";
                            else
                                txtFechaAlta.Text = ((DateTime)dr["T044_FECALTA"]).ToShortDateString();

                            if (dr["T044_NOTIFICADA"] == System.DBNull.Value)
                                txtFechaNotificacion.Text = "";
                            else
                                txtFechaNotificacion.Text = ((DateTime)dr["T044_NOTIFICADA"]).ToShortDateString();

                            if (dr["T044_FECLIMITE"] == System.DBNull.Value)
                                txtFechaLimite.Text = "";
                            else
                                txtFechaLimite.Text = ((DateTime)dr["T044_FECLIMITE"]).ToShortDateString();

                            if (int.Parse(cboEstado.SelectedValue) > 0 && int.Parse(cboEstado.SelectedValue) != 2 && int.Parse(cboEstado.SelectedValue) != 4 && int.Parse(cboEstado.SelectedValue) != 7)
                            {
                                cboImportancia.Enabled = false;
                                cboPrioridad.Enabled = false;

                                txtDescripcion.ReadOnly = true;
                                txtDenominacion.ReadOnly = true;
                                txtObservaciones.ReadOnly = false;
                                txtAclara.ReadOnly = true;
                                if (cboEstado.SelectedValue == "0" || cboEstado.SelectedValue == "1")
                                {
                                    txtSolAclar.ReadOnly = true;
                                    if (hdnEsCoordinador.Text != "true") txtObservaciones.ReadOnly = true;
                                }
                            }
                            else
                            {
                                cboEstado.Enabled = false;

                                if (cboEstado.SelectedValue != "0") txtDenominacion.ReadOnly = true;
                                txtDescripcion.ReadOnly = false;
                                txtSolAclar.ReadOnly = true;
                                txtObservaciones.ReadOnly = true;
                                if (cboEstado.SelectedValue == "0" || cboEstado.SelectedValue == "1")
                                    txtAclara.ReadOnly = true;
                            }


                            if (dr["T044_FECPACT"] == System.DBNull.Value)
                                txtFechaPactada.Text = "";
                            else
                                txtFechaPactada.Text = ((DateTime)dr["T044_FECPACT"]).ToShortDateString();

                            cboImportancia.SelectedValue = dr["T044_IMPORTANCIA"].ToString();
                            cboPrioridad.SelectedValue = dr["T044_PRIORIDAD"].ToString();
                            cboAvance.SelectedValue = dr["T044_AVANCE"].ToString();
                            txtDescripcion.Text = (string)dr["T044_DESCRIPCION"];
                            //txtFechaNotificacion.ReadOnly = true;
                            #endregion
                            #region Solapa Avanzada

                            // PDTE CODIGOS hdnProveedor,hdnCliente,hdnCR

                            txtEntrada.Text = (string)dr["ENTRADA"];
                            hdnEntrada.Text = dr["T074_IDENTRADA"].ToString();

                            txtAlcance.Text = (string)dr["ALCANCE"];
                            hdnAlcance.Text = dr["T077_IDALCANCE"].ToString();

                            txtTipo.Text = (string)dr["TIPO"];
                            hdnTipo.Text = dr["T076_IDTIPO"].ToString();

                            txtCR.Text = (string)dr["T044_CR"];
                            txtCliente.Text = (string)dr["T044_CLIENTE"];
                            txtProveedor.Text = (string)dr["T044_PROVEEDOR"];

                            txtProducto.Text = (string)dr["PRODUCTO"];
                            hdnProducto.Text = dr["T079_IDPRODUCTO"].ToString();

                            txtProceso.Text = (string)dr["PROCESO"];
                            hdnProceso.Text = dr["T078_IDPROCESO"].ToString();

                            txtRequisito.Text = (string)dr["REQUISITO"];
                            hdnRequisito.Text = dr["T081_IDREQUISITO"].ToString();

                            txtCausa.Text = (string)dr["CAUSA"];
                            hdnCausa.Text = dr["T082_CAUSA_CAT"].ToString();

                            txtCausaBfcio.Text = (string)dr["T044_CAUSA"];

                            txtResultado.Text = (string)dr["T044_RESULTADO"];
                            cboRtado.SelectedValue = dr["T044_RTDO"].ToString();
                            #endregion
                            #region Solapa Planificación
                            if (dr["T044_FINIPREV"] == System.DBNull.Value)
                                txtFechaInicioPrevista.Text = "";
                            else
                                txtFechaInicioPrevista.Text = ((DateTime)dr["T044_FINIPREV"]).ToShortDateString();

                            if (dr["T044_FFINPREV"] == System.DBNull.Value)
                                txtFechaFinPrevista.Text = "";
                            else
                                txtFechaFinPrevista.Text = ((DateTime)dr["T044_FFINPREV"]).ToShortDateString();

                            if (dr["T044_FECINIREAL"] == System.DBNull.Value)
                                txtFechaInicioReal.Text = "";
                            else
                                txtFechaInicioReal.Text = ((DateTime)dr["T044_FECINIREAL"]).ToShortDateString();

                            if (dr["T044_FECFINREAL"] == System.DBNull.Value)
                                txtFechaFinReal.Text = "";
                            else
                                txtFechaFinReal.Text = ((DateTime)dr["T044_FECFINREAL"]).ToShortDateString();

                            if ((float)dr["T044_TIEMPOESTI"] == 0)
                                txtTiempoEstimado.Text = "";
                            else
                                txtTiempoEstimado.Text = ((float)dr["T044_TIEMPOESTI"]).ToString("#,##0.00");

                            if ((float)dr["T044_TIEMPOINVER"] == 0)
                                txtTiempoInvertido.Text = "";
                            else
                                txtTiempoInvertido.Text = ((float)dr["T044_TIEMPOINVER"]).ToString("#,##0.00");

                            cboUnidadEstimacion.SelectedValue = dr["T044_UNIDADESTIMA"].ToString();
                            #endregion
                            #region Solapa Cronologia

                            if (dr["T044_FECHAMODIF"] == System.DBNull.Value)
                                lblFUM.Text = "";
                            else
                                lblFUM.Text = ((DateTime)dr["T044_FECHAMODIF"]).ToString();

                            this.lblUsuario.Text = dr["USUARIO"].ToString();
                            #endregion
                            #region Solapa Comentarios

                            this.txtObservaciones.Text = dr["T044_OBSERVACION"].ToString();
                            this.txtSolAclar.Text = dr["T044_SOLACLARA"].ToString();
                            this.txtAclara.Text = dr["T044_ACLARA"].ToString();
                            #endregion
                            #region Solapa Pruebas
                            this.txtPruebas.Text = dr["T044_PRUEBAS"].ToString();
                            #endregion

                            if (hdnEsCoordinador.Text == "true" && !(idEstado == 0 || idEstado == 2 || idEstado == 4 || idEstado == 7 || idEstado == 9 || idEstado == 11 || idEstado == 12))
                                lblCoordinador.CssClass = "enlace";

                            if ((hdnEsTecnico.Text == "true" || hdnEsPromotor_o_Responsable.Text == "true") &&  
                                (hdnAdmin.Text != "A") && (hdnEsCoordinador.Text != "true") && (hdnEsSolicitante.Text != "true") )
                                {
                                    hdnModoLectura.Text = "1";
                                    //tsPestanas.Items[4].Disabled = true;
                                }

                            if (hdnEsSolicitante.Text == "true" && hdnAdmin.Text != "A" && hdnEsCoordinador.Text != "true")
                            {
                                if (idEstado != 0 && idEstado != 2 && idEstado != 4 && idEstado != 7) 
                                    hdnModoLectura.Text = "1";
                                cboEstado.Enabled = false;
                            }

                        }

                        dr.Close();
                        dr.Dispose();
                        #endregion
                        #region Datos del area
                        dr = null;
                        dr = Areas.LeerUnRegistro(int.Parse(Request.QueryString["IDAREA"]));
                        if (dr.Read())
                        {
                            //23/05/2016 Por petición de Víctor este control solo se realiza en altas
                            //if ((bool)dr["T042_SELCOORDI"] == false)
                            //   {
                            //    lblCoordinador.CssClass = "texto";
                            //    btnCoordinador.Visible = false;
                            //}
                            if ((bool)dr["RESUELTA"])
                                hdnResuelta.Text = "1";
                            else
                                hdnResuelta.Text = "0";
                        }

                        dr.Close();
                        dr.Dispose();
                        dr = null;
                        #endregion

                        if (hdnModoLectura.Text == "1" || idEstado == 7)
                        {
                            System.Web.UI.Control Area = this.FindControl("frmDatos");
                            ModoLectura.Poner(Area.Controls);
                        }

                        if ((idEstado != 0 && hdnAdmin.Text != "A") || hdnModoLectura.Text == "1")
                        {
                            if ((hdnEsCoordinador.Text != "true" && hdnAdmin.Text != "A") || hdnModoLectura.Text == "1" || idEstado == 7)
                            {
                                //System.Web.UI.Control Area = this.FindControl("tbCrono");
                                //ModoLectura.Poner(Area.Controls);
                                #region Establecer visibilidad de campos
                                txtDescripcion.ReadOnly = true;
                                if (hdnEsCoordinador.Text != "true")
                                {
                                    lblCoordinador.CssClass = "texto";
                                    btnCoordinador.Visible = false;
                                }
                                lblEntrada.CssClass = "texto";
                                lblAlcance.CssClass = "texto";
                                lblTipo.CssClass = "texto";
                                lblProducto.CssClass = "texto";
                                lblProceso.CssClass = "texto";
                                lblRequisito.CssClass = "texto";
                                lblCausa.CssClass = "texto";
                                lblCR.CssClass = "texto";
                                lblProveedor.CssClass = "texto";
                                lblCliente.CssClass = "texto";

                                btnEntrada.Attributes.Add("style", "visibility:hidden");
                                btnAlcance.Attributes.Add("style", "visibility:hidden");
                                btnTipo.Attributes.Add("style", "visibility:hidden");
                                btnProducto.Attributes.Add("style", "visibility:hidden");
                                btnProceso.Attributes.Add("style", "visibility:hidden");
                                btnRequisito.Attributes.Add("style", "visibility:hidden");
                                btnCausa.Attributes.Add("style", "visibility:hidden");
                                btnCR.Attributes.Add("style", "visibility:hidden");
                                btnProveedor.Attributes.Add("style", "visibility:hidden");
                                btnCliente.Attributes.Add("style", "visibility:hidden");
                                #endregion
                            }
                        }

                        if (hdnEsSolicitante.Text == "true" && (idEstado == 0 || idEstado == 2 || idEstado == 4 || idEstado == 7 || idEstado == 9))
                        {
                            //tsPestanas.Items[8].Enabled = false;
                            //System.Web.UI.Control Area = this.FindControl("tbCrono");
                            //ModoLectura.Poner(Area.Controls);
                            //tsPestanas.Items[4].Disabled = true;
                            cboAvance.Enabled = false;
                            cboRtado.Enabled = false;
                            txtCausaBfcio.ReadOnly = true;
                            txtResultado.ReadOnly = true;
                            txtObservaciones.ReadOnly = true;
                            //txtSolAclar.ReadOnly = true;
                            if (idEstado == 2) txtAclara.ReadOnly = false;
                        }
                        if (hdnEsCoordinador.Text == "true" && idEstado != 2) txtSolAclar.ReadOnly = true;

                        if ((hdnEsSolicitante.Text == "true") && (hdnAdmin.Text != "A") && (hdnEsPromotor_o_Responsable.Text != "true") &&
                            (hdnEsCoordinador.Text != "true") && (hdnEsTecnico.Text != "true")
                            )
                        {
                            //System.Web.UI.Control Area = this.FindControl("tbCrono");
                            //ModoLectura.Poner(Area.Controls);

                            //tsPestanas.Items[8].Enabled = false;
                            //tsPestanas.Items[4].Disabled = true;
                            cboAvance.Enabled = false;
                            cboRtado.Enabled = false;
                            txtCausaBfcio.ReadOnly = true;
                            txtResultado.ReadOnly = true;
                            txtObservaciones.ReadOnly = true;
                            txtSolAclar.ReadOnly = true;
                        }

                        strTablaHtmlCronologia = CargarCronologia(int.Parse(hdnIDDefi.Text));
                        #region Carga de catalogos
                        //                  Solapa Tareas

                        string strTabla0 = ObtenerTareas(hdnIDDefi.Text, 1, 0);
                        string[] aTabla0 = Regex.Split(strTabla0, "@@");
                        if (aTabla0[0] != "N") strTablaCatalogo = aTabla0[0];

    //                  Solapa Documentos
                        string strTabla2 = ObtenerDocumentos(hdnIDDefi.Text);
                        string[] aTabla2 = Regex.Split(strTabla2, "@@");
                        if (aTabla2[0] != "N") strTablaCatalogoDoc = aTabla2[0];
   
    //                  Solapa Pruebas
                        if (hdnEsSolicitante.Text == "true" && idEstado == 9)
                            txtPruebas.ReadOnly = false;
                        else
                            txtPruebas.ReadOnly = true;

                        dr.Close();
                        dr.Dispose();
                        dr = null;
                        #endregion
                        if (hdnModoLectura.Text != "1" && (hdnEsCoordinador.Text == "true" || hdnAdmin.Text == "A"))
                        {
                            Utilidades.SetEventosFecha(this.txtFechaPactada);
                            this.txtFechaPactada.Attributes.Add("readonly", "readonly");
                        }
                        //Víctor 18/05/2016 la cronología solo es editable para el coordinador y siempre que no esté
                        //Resuelta, Aprobada o Anulada
                        if ((hdnEsCoordinador.Text == "true" && hdnCoordinador.Text == Session["IDFICEPI"].ToString()) && 
                            (idEstado != 9 && idEstado != 11 && idEstado != 12))
                        {
                            Utilidades.SetEventosFecha(this.txtFechaInicioPrevista);
                            Utilidades.SetEventosFecha(this.txtFechaFinPrevista);
                            Utilidades.SetEventosFecha(this.txtFechaInicioReal);
                            Utilidades.SetEventosFecha(this.txtFechaFinReal);
                        }
                        else
                        {
                            System.Web.UI.Control Area = this.FindControl("tbCrono");
                            ModoLectura.Poner(Area.Controls);
                        }
                        #endregion
                    }
                    else//if (Request.QueryString["bNueva"].ToString() == "false")
                    {
                        #region Nuevo Registro
                        cboAvance.Items.Insert(0, new ListItem("", "0"));

                        //tsPestanas.Items[10].Enabled = false;
                        //tsPestanas.Items[8].Enabled = false;

                        //tsPestanas.Items[4].Disabled = true;
                        strTablaHtmlCronologia = "<table id='tblCronologia' style='width: 460px'></table>";
                        //Víctor 18/05/2016 la cronología solo es editable para el coordinador y siempre que no esté
                        //Resuelta, Aprobada o Anulada
                        if ((hdnEsCoordinador.Text == "true" && hdnCoordinador.Text == Session["IDFICEPI"].ToString()) &&
                            (idEstado != 9 && idEstado != 11 && idEstado != 12))
                        {
                            Utilidades.SetEventosFecha(this.txtFechaInicioPrevista);
                            Utilidades.SetEventosFecha(this.txtFechaFinPrevista);
                            Utilidades.SetEventosFecha(this.txtFechaInicioReal);
                            Utilidades.SetEventosFecha(this.txtFechaFinReal);
                        }
                        else
                        {
                            System.Web.UI.Control Area = this.FindControl("tbCrono");
                            ModoLectura.Poner(Area.Controls);
                        }

                        Utilidades.SetEventosFecha(this.txtFechaNotificacion);
                        Utilidades.SetEventosFecha(this.txtFechaLimite);

                        hdnSolicitante.Text = Session["IDFICEPI"].ToString();
                        txtSolicitante.Text = Session["NOMBRE2"].ToString();
                        txtFechaAlta.Text = System.DateTime.Now.ToShortDateString();
                        txtFechaNotificacion.Text = System.DateTime.Now.ToShortDateString();
                        //txtFechaPactada.ReadOnly = true;
                        //lblEstado.Visible = false;
                        //cboEstado.Visible = false;
                        cboAvance.SelectedValue = "0";
                        cboEstado.Enabled = false;
                        cboAvance.Enabled = false;

                        cboRtado.Enabled = false;
                        txtCausaBfcio.ReadOnly = true;
                        txtResultado.ReadOnly = true;
                        txtObservaciones.ReadOnly = true;
                        txtSolAclar.ReadOnly = true;
                        txtAclara.ReadOnly = true;

                        dr = null;
                        dr = Areas.LeerUnRegistro(int.Parse(Request.QueryString["IDAREA"]));
                        if (dr.Read())
                        {
                            if ((bool)dr["T042_SELCOORDI"] == false)
                            {
                                lblCoordinador.CssClass = "texto";
                                btnCoordinador.Visible = false;
                            }
                        }
                        dr.Close();
                        dr.Dispose();
                        dr = null;
                        //hdnCorreoCoordinador.Text = Session["IDFICEPI"].ToString() + "/" + Session["IDRED"] + "/" + Session["NOMBRE"].ToString();
                        hdnCorreoCoordinador.Text = "";
                        #endregion
                    }

                    string strTabla1 = ObtenerDocumentosArea(hdnIDArea.Text);
                    string[] aTabla1 = Regex.Split(strTabla1, "@@");
                    if (aTabla1[0] != "N") strTablaCatalogoDocArea = aTabla1[0];
                }
                catch (Exception ex)
                {
                    hdnErrores.Text = Errores.mostrarError("Error al obtener los datos", ex);
                }
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context");
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        private string UltimaModificacion(int intIdDefi)
        {
            string strFUM = "";
            string strUsuario = "";

            dr = null;
            dr = Deficiencias.Select(null, intIdDefi);
            if (dr.Read())
            {
                if (dr["T044_FECHAMODIF"] == System.DBNull.Value)
                    strFUM = "";
                else
                    strFUM = ((DateTime)dr["T044_FECHAMODIF"]).ToString();

                strUsuario = dr["USUARIO"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return strFUM + "@" +strUsuario;
        }    
        private string CargarCronologia(int intIdDefi)
        {
            dr = null;
            dr = CRONOLOGIA.Catalogo(intIdDefi);

            int i = 0;
            System.Text.StringBuilder strBuilderCronologia = new System.Text.StringBuilder();
            System.Text.StringBuilder sbTitle = new System.Text.StringBuilder();

            strBuilderCronologia.Append("<table id='tblCronologia' style='width: 540px;text-align:left'>" + (char)13);
            strBuilderCronologia.Append("<colgroup><col style='width:120px;' /><col width='130px' /><col width='290px' /></colgroup>");

            while (dr.Read())
            {
                strBuilderCronologia.Append("<tr ");

                //if (i % 2 == 0)
                //    strBuilderCronologia.Append("class='FA' ");
                //else
                //    strBuilderCronologia.Append("class='FB' ");

                i++;

                strBuilderCronologia.Append(" style='cursor: pointer;height:16px' ");

                if (dr["T044_ESTADO"].ToString() == "No aprobada" || dr["T044_ESTADO"].ToString() == "Anulada" || dr["T044_ESTADO"].ToString() == "Rechazada")
                {
                    sbTitle.Length = 0;
                    sbTitle.Append(dr["T096_MOTIVO"].ToString().Replace(((char)10).ToString(), "<br>").Replace((char)34, (char)39));

                    strBuilderCronologia.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Motivo] body=[" + sbTitle + "]\"");
                }

                strBuilderCronologia.Append("><td>&nbsp;");

                //strFilas += ">&nbsp;<LABEL TITLE='" + dr["T096_MOTIVO"].ToString() + "'>";

                if (dr["T044_ESTADO"].ToString() == "")
                    strBuilderCronologia.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                else
                    strBuilderCronologia.Append(dr["T044_ESTADO"].ToString());
                
                strBuilderCronologia.Append("</td>");

                string strFecha;
                if (dr["T096_FECHA"] == System.DBNull.Value)
                    strFecha = "";
                else
                    strFecha = ((DateTime)dr["T096_FECHA"]).ToString();

                strBuilderCronologia.Append("<td>" + strFecha + "</td><td><nobr class='NBR' style='width:290px;'> " + dr["USUARIO"].ToString() + "</nobr></td><tr>" + (char)13);
            }

            dr.Close();
            dr.Dispose();

            strBuilderCronologia.Append("</table>");

            return strBuilderCronologia.ToString();
        }

        private void Habilitar_campos_Tecnico()
        {
            //if (hdnIdEstado.Text != "5") return;

            //txtCausa.ReadOnly = false;
            //txtIntervencion.ReadOnly = false;
            //txtConsideracion.ReadOnly = false;
        }
        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            //base.OnInit(e);
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
                case ("abrir"):
                    strbTabla.Append(Abrir(int.Parse(aArgs[1])));	  // ID Deficiencia                                
                    break;
                case ("tareas"):
                    //strbTabla.Append(ObtenerTareas(aArgs[1],byte.Parse(aArgs[2]),byte.Parse(aArgs[3]),byte.Parse(aArgs[4]),aArgs[5],aArgs[6]));
                    strbTabla.Append(ObtenerTareas(aArgs[1], byte.Parse(aArgs[2]), byte.Parse(aArgs[3])));
                    break;
                case ("delete_tareas"):
                    strbTabla.Append(BorrarTareas(aArgs[1]));
                    break;
                case ("documentos"):
                    strbTabla.Append(ObtenerDocumentos(aArgs[1]));
                    break;
                case ("elimdocs"):
                    strbTabla.Append(EliminarDocumentos(aArgs[1]));
                    break;
                case "eliminar":       // Eliminación de una deficiencia
                    strbTabla.Append(Eliminar(aArgs[1]));
                    break;
                case ("cronologia"):
                    strbTabla.Append(CargarCronologia(int.Parse(aArgs[1]))+ "@@" +
                                    UltimaModificacion(int.Parse(aArgs[1]))
                                    );
                    break;
                case "grabar":
                   if (aArgs[31] == "") aArgs[31] = "0";
                   if (aArgs[32] == "") aArgs[32] = "0";

                   strbTabla.Append
                   (
                        Grabar
                        (
                        byte.Parse(aArgs[1]), 		    			                    // 0=UPDATE 1=INSERT
                        Microsoft.JScript.GlobalObject.unescape(aArgs[2]),  			// Denominacion
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[3])),  // Estado
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[4])),  // Estado anterior
                        int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[5])),  	// Solicitante
                        int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[6])),  	// Coordinador
                        Microsoft.JScript.GlobalObject.unescape(aArgs[7]),  			// FechaNotificacion
                        Microsoft.JScript.GlobalObject.unescape(aArgs[8]),  			// FechaLimite
                        Microsoft.JScript.GlobalObject.unescape(aArgs[9]),  			// FechaPactada
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[10])), // Importancia
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[11])), // Prioridad
                        short.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[12])),// Avance
						Microsoft.JScript.GlobalObject.unescape(aArgs[13]), 			// Descripcion
						short.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[14])),// Entrada
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[15])), 	// Alcance
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[16])), 	// Tipo
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[17])), 	// Producto
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[18])), 	// Proceso
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[19])), 	// Requisito
						int.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[20])), 	// Causa
						Microsoft.JScript.GlobalObject.unescape(aArgs[21]), 	        // CR
						Microsoft.JScript.GlobalObject.unescape(aArgs[22]), 	        // Proveedor
						Microsoft.JScript.GlobalObject.unescape(aArgs[23]), 	        // Cliente
						Microsoft.JScript.GlobalObject.unescape(aArgs[24]), 			// CausaBfcio
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[25])), // Rtado
						Microsoft.JScript.GlobalObject.unescape(aArgs[26]), 			// Resultado
						Microsoft.JScript.GlobalObject.unescape(aArgs[27]), 			// FechaInicioPrevista
						Microsoft.JScript.GlobalObject.unescape(aArgs[28]), 			// FechaInicioReal
						Microsoft.JScript.GlobalObject.unescape(aArgs[29]), 			// FechaFinPrevista
						Microsoft.JScript.GlobalObject.unescape(aArgs[30]), 			// FechaFinReal
                        float.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[31])),// TiempoEstimado
                        float.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[32])),// TiempoInvertido
                        byte.Parse(Microsoft.JScript.GlobalObject.unescape(aArgs[33])), // UnidadEstimacion
                        Microsoft.JScript.GlobalObject.unescape(aArgs[34]),  			// Observaciones						
      					Microsoft.JScript.GlobalObject.unescape(aArgs[35]), 			// Motivo	
	   					Microsoft.JScript.GlobalObject.unescape(aArgs[36]),  			// IDDEFICI						
                        Microsoft.JScript.GlobalObject.unescape(aArgs[37]),  			// NOMBRE DEL COORDINADOR
                        Microsoft.JScript.GlobalObject.unescape(aArgs[38]),  			// NOMBRE DEL SOLICITANTE
                        Microsoft.JScript.GlobalObject.unescape(aArgs[39]),  			// CORREO COORDINADOR UNICO
                        Microsoft.JScript.GlobalObject.unescape(aArgs[40]), 	        // Coordinador Old (id+codred+nombre)
                        Microsoft.JScript.GlobalObject.unescape(aArgs[41]), 	        // Entrada
                        Microsoft.JScript.GlobalObject.unescape(aArgs[42]), 	        // Alcance
                        Microsoft.JScript.GlobalObject.unescape(aArgs[43]), 	        // Tipo
                        Microsoft.JScript.GlobalObject.unescape(aArgs[44]), 	        // Producto
                        Microsoft.JScript.GlobalObject.unescape(aArgs[45]), 	        // Proceso
                        Microsoft.JScript.GlobalObject.unescape(aArgs[46]), 	        // Requisito
                        Microsoft.JScript.GlobalObject.unescape(aArgs[47]), 	        // Causa
                        Microsoft.JScript.GlobalObject.unescape(aArgs[48]), 	        // Solicitud de aclaración
                        Microsoft.JScript.GlobalObject.unescape(aArgs[49]), 	        // Aclaración resuelta                        
                        Microsoft.JScript.GlobalObject.unescape(aArgs[50]) 	            // Pruebas realizadas por el solicitante                       
                        )
                    );
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
        private string Eliminar(string strIDDefi)
        {
            string sResul = "";
            try
            {
                Deficiencias.Delete(null, int.Parse(strIDDefi));
            }
            catch (System.Exception objError)
            {
                sResul = "N@@" + Errores.mostrarError("Error al borrar una orden.", objError);
            }
            return sResul;
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
        private string Grabar(
            byte byteNueva, string sDenominacion, byte byEstado, byte byEstadoAnt, int iSolicitante, int iCoordinador, string sFechaNoti,
            string sFechaLimi,string sFechaPacta, byte byImportancia, byte byPrioridad, short shAvance, string sDescripcion, 
            short iEntrada, int iAlcance, int iTipo, int iProducto, int iProceso, int iRequisito, int iCausa, string sCR, 
            string sProveedor, string sCliente, string sCausaBfcio, byte byRtdo, string sResultado, string sFechaInicioPrevista, 
            string sFechaInicioReal, string sFechaFinPrevista, string sFechaFinReal, float dblTiempoEstimado, 
            float dblTiempoInvertido, byte byUnidadEstimacion, string sObservaciones, string sMotivo, string strIDDefici,
            string sNomCoordinador, string sNomSolicitante, string sCorreoCoordinador, string sCoordinadorOld,
            string sEntrada, string sAlcance, string sTipo, string sProducto, string sProceso, string sRequisito, string sCausa, string sSolicAcla, string sAclaraResu, string sPruebas
            )
        {
            if (strIDDefici == "-1") strIDDefici = hdnIDDefi.Text; ;

            sNomCoordinador = Nom_Apellidos(sNomCoordinador);
            sNomSolicitante = Nom_Apellidos(sNomSolicitante);

            string sResul;
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

            int? intSolicitante = null;
            if (iSolicitante != 0) intSolicitante = iSolicitante;

            int? intCoordinador = null;
            if (iCoordinador != 0) intCoordinador = iCoordinador;

            DateTime? dFechaNoti = null;
            if (sFechaNoti != "") dFechaNoti = DateTime.Parse(sFechaNoti);

            DateTime? dFechaLimi = null;
            if (sFechaLimi != "") dFechaLimi = DateTime.Parse(sFechaLimi);

            DateTime? dFechaPacta = null;
            if (sFechaPacta != "") dFechaPacta = DateTime.Parse(sFechaPacta);

            short? intEntrada = null;
            if (iEntrada != 0) intEntrada = iEntrada;
            int? intAlcance = null;
            if (iAlcance != 0) intAlcance = iAlcance;
            int? intTipo = null;
            if (iTipo != 0) intTipo = iTipo;
            int? intProducto = null;
            if (iProducto != 0) intProducto = iProducto;
            int? intProceso = null;
            if (iProceso != 0) intProceso = iProceso;
            int? intRequisito = null;
            if (iRequisito != 0) intRequisito = iRequisito;
            int? intCausa = null;
            if (iCausa != 0) intCausa = iCausa;

            DateTime? dFechaInicioPrevista = null;
            if (sFechaInicioPrevista != "") dFechaInicioPrevista = DateTime.Parse(sFechaInicioPrevista);

            DateTime? dFechaInicioReal = null;
            if (sFechaInicioReal != "") dFechaInicioReal = DateTime.Parse(sFechaInicioReal);

            DateTime? dFechaFinPrevista = null;
            if (sFechaFinPrevista != "") dFechaFinPrevista = DateTime.Parse(sFechaFinPrevista);

            DateTime? dFechaFinReal = null;
            if (sFechaFinReal != "") dFechaFinReal = DateTime.Parse(sFechaFinReal);

            float? dbTiempoEstimado = null;
            if (dblTiempoEstimado != 0) dbTiempoEstimado = dblTiempoEstimado;

            float? dbTiempoInvertido = null;
            if (dblTiempoInvertido != 0) dbTiempoInvertido =dblTiempoInvertido;

            if (byteNueva == 0)
            {
                try
                {
                    Deficiencias.Update
                                        (tr,
                                        int.Parse(strIDDefici),
                                        int.Parse(hdnIDArea.Text),
                                        byEstado,
                                        byEstadoAnt,
                                        sDenominacion,
                                        sDescripcion,
                                        byImportancia,  
                                        byPrioridad, 
                                        shAvance,
                                        dFechaLimi,
                                        dFechaPacta,
                                        sObservaciones,
                                        byUnidadEstimacion,
                                        dbTiempoEstimado,
                                        dFechaInicioPrevista,
                                        dFechaFinPrevista,
                                        dblTiempoInvertido,
                                        dFechaInicioReal,
                                        dFechaFinReal,
                                        dFechaNoti,
                                        intCoordinador,
                                        intSolicitante,
                                        sCliente,         
                                        intEntrada,
                                        sCR,
                                        intTipo,
                                        intAlcance,
                                        intProceso,
                                        intProducto,
                                        sProveedor,          
                                        intRequisito,
                                        intCausa,
                                        sCausaBfcio,
                                        sResultado,
                                        byRtdo,
                                        sMotivo,
                                        int.Parse(Session["IDFICEPI"].ToString()),
                                        sSolicAcla,
                                        sAclaraResu,
                                        sPruebas
                                        );
                    intContador = int.Parse(strIDDefici);
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al modificar la orden.", objError);
                    GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                    return "N@@" + sResul;
                }
            }
            else
            {
                try
                {
                    intContador = Deficiencias.Insert
                                                (tr,
                                                int.Parse(hdnIDArea.Text),
                                                byEstado,
                                                sDenominacion,
                                                sDescripcion,
                                                byImportancia,
                                                byPrioridad,
                                                shAvance,
                                                dFechaLimi,
                                                dFechaPacta,
                                                sObservaciones,
                                                byUnidadEstimacion,
                                                dbTiempoEstimado,
                                                dFechaInicioPrevista,
                                                dFechaFinPrevista,
                                                dblTiempoInvertido,
                                                dFechaInicioReal,
                                                dFechaFinReal,
                                                dFechaNoti,
                                                intCoordinador,
                                                intSolicitante,
                                                sCliente,        
                                                intEntrada,
                                                sCR,
                                                intTipo,
                                                intAlcance,
                                                intProceso,
                                                intProducto,
                                                sProveedor,     
                                                intRequisito,
                                                intCausa,
                                                sCausaBfcio,
                                                sResultado,
                                                byRtdo,
                                                int.Parse(Session["IDFICEPI"].ToString()),
                                                sSolicAcla,
                                                sAclaraResu
                                                );
                    hdnIDDefi.Text = intContador.ToString();
                    strIDDefici = hdnIDDefi.Text;
                }
                catch (System.Exception objError)
                {
                    sResul = Errores.mostrarError("Error al crear la orden.", objError);
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
 //           if (byEstado == 1 && byEstadoAnt == 0)        // tramitación
            if (byEstado == 1 && byEstadoAnt != 1)        // tramitación
            {
                // CORREO POR TRAMITACION

                ListDto<Integrante> ListaCorreo = new ListDto<Integrante>();
                string[] aCorreoResponsable = Regex.Split(this.hdnCorreoResponsables.Text, ",");
                string[] aCorreoCoordinador = Regex.Split(this.hdnCorreoCoordinadores.Text, ",");

                string strMensaje = "";
                string strAsunto = "";
                string strTO;


                strAsunto = "Tramitación";
                strMensaje = Session["NOMBRE"] + @" ha creado una orden 
										para el área '" + Request.QueryString["AREA"].ToString() + @"'.";
                strMensaje += @"<p>
										Los datos son:
										</p>
										";

                strMensaje += "<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                strMensaje += "<tr><td>";
                strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>&nbsp;&nbsp;" + Request.QueryString["AREA"].ToString() + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Nº de orden:</LABEL>&nbsp;&nbsp;" + strIDDefici + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Denominación (Asunto):</LABEL>&nbsp;&nbsp;" + sDenominacion + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Descripción:</LABEL>&nbsp;&nbsp;" + sDescripcion.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>") + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Solicitante:</LABEL>&nbsp;&nbsp;" + Session["NOMBRE"] + @"<BR><BR>";


                strMensaje += @"<LABEL class='TITULO'>Coordinador:</LABEL>&nbsp;&nbsp;" + sNomCoordinador + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Fecha notificación:</LABEL>&nbsp;&nbsp;" + sFechaNoti + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Fecha límite:</LABEL>&nbsp;&nbsp;" + sFechaLimi + @"<BR><BR>";

                cboImportancia.SelectedValue = byImportancia.ToString();
                cboPrioridad.SelectedValue = byPrioridad.ToString();

                strMensaje += @"<LABEL class='TITULO'>Importancia:</LABEL>&nbsp;&nbsp;" + cboImportancia.SelectedItem.Text + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Prioridad:</LABEL>&nbsp;&nbsp;" + cboPrioridad.SelectedItem.Text + @"<BR><BR>";

                strMensaje += @"<LABEL class='TITULO'>Entrada:</LABEL>&nbsp;&nbsp;" + sEntrada + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Alcance:</LABEL>&nbsp;&nbsp;" + sAlcance + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Tipo:</LABEL>&nbsp;&nbsp;" + sTipo+ @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Producto:</LABEL>&nbsp;&nbsp;" + sProducto + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Proceso:</LABEL>&nbsp;&nbsp;" + sProceso + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Requisito:</LABEL>&nbsp;&nbsp;" + sRequisito + @"<BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Causa:</LABEL>&nbsp;&nbsp;" + sCausa + @"<BR><BR>";

                strMensaje += "</td></tr>";
                strMensaje += "</table>";

                if (hdnEnviarCorreoPromotor.Text == "1")
                {
                    string[] aID = Regex.Split(hdnCorreoPromotor.Text, "/");
                    //strMensaje = strCabecera + strDatos;

                    //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) PROMOTOR";

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

                    //strTO = aCorreoPromotor[j];
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                    ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                }

                for (int j = 0; j < aCorreoResponsable.Length; j++)
                {
                    if (aCorreoResponsable[j] == "") continue;

                    string[] aID = Regex.Split(aCorreoResponsable[j], "/");

                    //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) RESPONSABLE";

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
                    ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                    if (resultadoBusqueda.Count == 0)
                    {
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                        ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                    }
                }

                if (sCorreoCoordinador == "" || sCorreoCoordinador == "0//")
                {
                    for (int j = 0; j < aCorreoCoordinador.Length; j++)
                    {
                        if (aCorreoCoordinador[j] == "") continue;

                        string[] aID = Regex.Split(aCorreoCoordinador[j], "/");
                        //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) COORDINADORES DEL AREA";

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
                        ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                        if (resultadoBusqueda.Count == 0)
                        {
                            Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                            ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                        }
                    }
                }
                else
                {
                    string[] aID = Regex.Split(sCorreoCoordinador, "/");
                    //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) COORDINADOR SELECCIONADO";

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

                    //strTO = aCorreoPromotor[j];
                    ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                    if (resultadoBusqueda.Count == 0)
                    {
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                        ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                    }

                    //}
                }

                // FIN DE CORREO POR TRAMITACION   
            }

            if ((byEstado != byEstadoAnt) && (byEstado > 1)) // modificación de estado a partir de la tramitación 
            {
                // CORREO POR MODIFICACIÓN DE DATOS

                string strMensaje = "";
                string strAsunto = "";
                string strTO;

                // NOTIFICACIÓN AL SOLICITANTE ( CAMBIO DE ESTADO : A PDTE DE ACLARACIÓN )

                if (byEstado == 2 && byEstadoAnt == 1)
                {
                    string[] aID = Regex.Split(hdnCorreoSolicitante.Text, "/");

                    strMensaje = sNomCoordinador + @" requiere solicitud de aclaración.<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Solicitud de aclaración:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sSolicAcla.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Aclaración:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sAclaraResu.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Observaciones:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sObservaciones.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";

                    strMensaje += @"</table>";
                    //strMensaje += strDatos + @"<LABEL class='TITULO'>Causa:</LABEL>&nbsp;&nbsp;" + txtCausa.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Intervenciones:</LABEL>&nbsp;&nbsp;" + txtIntervencion.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Consideraciones:</LABEL>&nbsp;&nbsp;" + txtConsideracion.Text.Replace(((char)10).ToString(), "<br>");

                    strAsunto = "Pendiente de aclaración";
                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al Notificador ( CAMBIO DE ESTADO : A RECHAZADA )";


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

                // NOTIFICACIÓN AL COORDINADOR ( CAMBIO DE ESTADO : A ACLARACIÓN RESUELTA )

                if (byEstado == 13 && byEstadoAnt == 2)
                {
                    string[] aID = Regex.Split(sCorreoCoordinador, "/");

                    strMensaje = sNomSolicitante + @" ha contestado a la petición de aclaración.<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Solicitud de aclaración:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sSolicAcla.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Aclaración:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sAclaraResu.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Observaciones:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sObservaciones.Replace(((char)10).ToString(), "<br>");
                    strMensaje += @"<br><br></td></tr>";

                    strAsunto = "Aclaración resuelta";

                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al COORDINADOR, ( CAMBIO DE ESTADO : A PDTE DE RESOLUCIÓN )";


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

                // NOTIFICACIÓN AL SOLICITANTE ( CAMBIO DE ESTADO : A RECHAZADA  )

                if (byEstado == 4 && byEstadoAnt != 4)
                {
                    string[] aID = Regex.Split(hdnCorreoSolicitante.Text, "/");

                    strMensaje = @"Orden <LABEL class='TITULO'>'RECHAZADA'</LABEL> por " + sNomCoordinador + ".<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='90%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Motivo:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sMotivo.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>");
                    strMensaje += @"</td></tr>";
                    strMensaje += @"</table>";

                    strAsunto = "Rechazo";
                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al Notificador ( CAMBIO DE ESTADO : A RECHAZADA )";


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

                // NOTIFICACIÓN AL SOLICITANTE ( CAMBIO DE ESTADO : A PDTE DE ACEPTACION DE PROPUESTA )

                if (byEstado == 7 && byEstadoAnt != 7)
                {
                    string[] aID = Regex.Split(hdnCorreoSolicitante.Text, "/");

                    strMensaje = sNomCoordinador + @" requiere aceptación de propuesta.<BR><BR><BR><BR>";
                    
                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"</table>";
                    //strMensaje += strDatos + @"<LABEL class='TITULO'>Causa:</LABEL>&nbsp;&nbsp;" + txtCausa.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Intervenciones:</LABEL>&nbsp;&nbsp;" + txtIntervencion.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Consideraciones:</LABEL>&nbsp;&nbsp;" + txtConsideracion.Text.Replace(((char)10).ToString(), "<br>");

                    strAsunto = "Aceptación de propuesta";
                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al Notificador ( CAMBIO DE ESTADO : A RECHAZADA )";


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

                // NOTIFICACIÓN AL COORDINADOR ( CAMBIO DE ESTADO : A PDTE DE RESOLUCIÓN )

                if (byEstado == 6 && byEstadoAnt == 7)
                {
                    string[] aID = Regex.Split(sCorreoCoordinador, "/");

                    strMensaje = sNomSolicitante + @" ha aceptado la propuesta.<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='90%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"</table>";
                    //strMensaje += strDatos + @"<LABEL class='TITULO'>Causa:</LABEL>&nbsp;&nbsp;" + txtCausa.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Intervenciones:</LABEL>&nbsp;&nbsp;" + txtIntervencion.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Consideraciones:</LABEL>&nbsp;&nbsp;" + txtConsideracion.Text.Replace(((char)10).ToString(), "<br>");

                    strAsunto = "Propuesta aceptada";

                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al COORDINADOR, ( CAMBIO DE ESTADO : A PDTE DE RESOLUCIÓN )";


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

                // NOTIFICACIÓN AL SOLICITANTE ( CAMBIO DE ESTADO : A RESUELTA )

                if (byEstado == 9 && byEstadoAnt != 9)   
                {
                    string[] aID = Regex.Split(hdnCorreoSolicitante.Text, "/");

                    strMensaje = @"Petición resuelta. Se requiere aprobación.<BR><BR><BR><BR>";

                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"</table>";
                    //strMensaje += strDatos + @"<LABEL class='TITULO'>Causa:</LABEL>&nbsp;&nbsp;" + txtCausa.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Intervenciones:</LABEL>&nbsp;&nbsp;" + txtIntervencion.Text.Replace(((char)10).ToString(), "<br>") + @"<BR><BR><LABEL class='TITULO'>Consideraciones:</LABEL>&nbsp;&nbsp;" + txtConsideracion.Text.Replace(((char)10).ToString(), "<br>");

                    strAsunto = "Pendiente de aprobar";

                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al Notificador";


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

                // NOTIFICACIÓN AL RESPONSABLE / PROMOTOR / COORDINADOR CUANDO 
                // EL SOLICITANTE APRUEBA O RECHAZA UNA ORDEN O ALGUIEN ANULA UNA ORDEN

                //if (((byEstado == 10 || byEstado == 11) && byEstadoAnt == 9) || (byEstado == 12 && byEstadoAnt != 12))
                if ((byEstado == 10 || byEstado == 11) && byEstadoAnt == 9)
                {
                    ListDto<Integrante> ListaCorreo = new ListDto<Integrante>();

                    if (byEstado == 10)
                    {
                        strAsunto = "No aprobada";
                        strMensaje = sNomSolicitante + @" no ha dado su aprobación.<BR><BR><BR><BR>";
                    }
                    else
                    {
                        strAsunto = "Aprobación";
                        strMensaje = sNomSolicitante + @" ha dado su aprobación.<BR><BR><BR><BR>";
                    }

                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";

                    if (byEstado == 10)
                    {
                        strMensaje += @"</td></tr>";
                        strMensaje += @"<tr><td valign='top'>";
                        strMensaje += @"<LABEL class='TITULO'>Motivo:</LABEL>";
                        strMensaje += @"</td><td valign='top'>";
                        strMensaje += sMotivo.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>");
                        strMensaje += @"</td></tr>";
                    }

                    strMensaje += @"</table>";

                    // PROMOTOR

                    if (hdnEnviarCorreoPromotor.Text == "1")
                    {
                        string[] aID = Regex.Split(hdnCorreoPromotor.Text, "/");

                        //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) PROMOTOR POR APROBACIÓN / RECHAZO / ANULACION DE LA ORDEN";

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

                        //strTO = aCorreoPromotor[j];
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                        ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                    }

                    // RESPONSABLES

                    string[] aCorreoResponsable = Regex.Split(this.hdnCorreoResponsables.Text, ",");

                    for (int j = 0; j < aCorreoResponsable.Length; j++)
                    {
                        if (aCorreoResponsable[j] == "") continue;
                        string[] aID = Regex.Split(aCorreoResponsable[j], "/");

                        //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] +" ) AVISO A LOS RESPONSABLES POR APROBACIÓN / RECHAZO / ANULACION DE LA ORDEN";

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

                        ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                        if (resultadoBusqueda.Count == 0)
                        {
                            Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                            ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                        }
                    }
	
                    // AL COORDINADOR

                    if (sCorreoCoordinador != "")
                    {
                        string[] aID = Regex.Split(sCorreoCoordinador, "/");

                        //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) AVISO AL COORDINADOR POR APROBACIÓN / RECHAZO / ANULACION DE LA ORDEN";

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

                        ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                        if (resultadoBusqueda.Count == 0)
                        {
                            Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                            ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                        }
                    }
                }

                // AL SOLICITANTE Por anulación

                if (byEstado == 12 && byEstadoAnt != 12)
                {
                    string[] aID = Regex.Split(hdnCorreoSolicitante.Text, "/");

                    strMensaje = @"Anulación realizada por " + Session["NOMBRE"].ToString() + @".<BR><BR><BR><BR>";
                    strAsunto = "Anulación";

                    strMensaje += @"<table id='tblContenido' width='95%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                    strMensaje += @"<tr><td width='15%' valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += Request.QueryString["AREA"].ToString() + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Orden:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += strIDDefici + "-" + sDenominacion + "<br><br>";
                    strMensaje += @"</td></tr>";
                    strMensaje += @"<tr><td valign='top'>";
                    strMensaje += @"<LABEL class='TITULO'>Motivo:</LABEL>";
                    strMensaje += @"</td><td valign='top'>";
                    strMensaje += sMotivo.Replace(((char)10).ToString().Replace((char)34, (char)39), "<br>");
                    strMensaje += @"</td></tr>";
                    strMensaje += @"</table>";

                    //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de estado de la Orden: Notificamos al Notificador";


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

                // NOTIFICACIÓN A LOS ESPECIALISTAS AL PASAR A ESTADO EN RESOLUCION

                if (byEstado == 8 && (byEstadoAnt < 8 || byEstadoAnt == 10 || byEstadoAnt == 13))
                {
                    // ESPECIALISTAS

                    int iIDFICEPI = 0;
                    string sCorreoEspe = "";
                    strAsunto = "Asignación a tarea";
                    dr = null;
                    dr = Deficiencias.EspecialistasEnTareas(null, int.Parse(hdnIDDefi.Text));
                    hdnCorreoEspecialistas.Text = "";
                    while (dr.Read())
                    {
                        if (int.Parse(dr["T001_IDFICEPI"].ToString()) != iIDFICEPI)
                        {
                            if (iIDFICEPI != 0)
                            {
                                strMensaje += @"</table>";

                                string[] aID = Regex.Split(sCorreoEspe, "/");                               
                                //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) AVISO A LOS ESPECIALISTAS POR PASO A RESOLUCION";
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
                            // cabecera tabla
                            strMensaje = @"<p>" + sNomCoordinador + " le ha asignado como <LABEL class='TITULO'>ESPECIALISTA</LABEL>.</p><BR><BR><BR><BR>";
                            strMensaje += @"<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                            strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Área:</LABEL></td>";
                            strMensaje += @"<td width='85%' valign='top'>" + Request.QueryString["AREA"].ToString() + "<BR><BR></td><tr>";
                            strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Orden:</LABEL></td>";
                            strMensaje += @"<td width='85%' valign='top'>" + sDenominacion + "<BR><BR></td><tr>";
                            strMensaje += @"<tr><td width='15%' valign='top'><LABEL class='TITULO'>Tarea:</LABEL></td>";
                            strMensaje += @"<td width='85%' valign='top'>" + dr["T072_IDTAREA"].ToString() + "-" + dr["T072_DENOMINACION"].ToString() + "</td><tr>";
                            sCorreoEspe = dr["T001_IDFICEPI"].ToString() + "/" + dr["T001_CODRED"].ToString();
                            iIDFICEPI = int.Parse(dr["T001_IDFICEPI"].ToString());
                        }
                        else
                        {
                            // detalle
                            strMensaje += @"<tr><td width='15%' valign='top'>&nbsp;</td>";
                            strMensaje += @"<td width='85%' valign='top'>" + dr["T072_IDTAREA"].ToString() + "-" + dr["T072_DENOMINACION"].ToString() + "</td><tr>";
                        }
                    }
                    if (iIDFICEPI != 0)
                    {
                        strMensaje += @"</table>";
                        string[] aID = Regex.Split(sCorreoEspe, "/");
                        //strAsunto = " Usuario ( " + aID[0] + "/" + aID[1] + " ) AVISO A LOS ESPECIALISTAS POR PASO A RESOLUCION";

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
                    dr.Close();
                    dr.Dispose();
                }
            }

            // NOTIFICACIÓN AL COORDINADOR ( NUEVO Y AL ANTIGUO)

            if ((sCorreoCoordinador != "") && (sCoordinadorOld.ToUpper() != sCorreoCoordinador.ToUpper() && sCoordinadorOld != "") && (byEstado != 0))
            {
                string strMensaje = "";
                string strAsunto = "";
                string strTO;

                // NUEVO COORDINADOR

                string[] aID = Regex.Split(sCorreoCoordinador, "/");
                string[] aID2 = Regex.Split(sCoordinadorOld, "/");
                string[] aCorreoResponsable = Regex.Split(this.hdnCorreoResponsables.Text, ",");
                ListDto<Integrante> ListaCorreo = new ListDto<Integrante>();

                strAsunto = "Cambio de coordinador";
                //strAsunto = "Usuario ( " + aID[0] + "/" + aID[1] + " ) Cambio de coordinador en la Orden: Notificamos al Cordinador-nuevo";

                strMensaje = "<table id='tblContenido' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
                strMensaje += "<tr><td>";
                strMensaje += Session["NOMBRE"] + " ha realizado un cambio de coordinador.<BR><BR><BR><BR>";
                strMensaje += @"<LABEL class='TITULO'>Antiguo Coordinador:</LABEL>&nbsp;&nbsp;" + Nom_Apellidos(aID2[2]) + @"<br><br>";
                strMensaje += @"<LABEL class='TITULO'>Nuevo Coordinador:</LABEL>&nbsp;&nbsp;" + Nom_Apellidos(aID[2]) + @"<br><br>";
                strMensaje += @"<LABEL class='TITULO'>Área:</LABEL>&nbsp;&nbsp;" + Request.QueryString["AREA"].ToString() + @"<br><br>"; ;
                strMensaje += @"<LABEL class='TITULO'>Nº de Orden:</LABEL>&nbsp;&nbsp;" + strIDDefici + @"<br><br>"; ;
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
                ListDto<Integrante> resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID[0]));

                if (resultadoBusqueda.Count == 0)
                {
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                    ListaCorreo.Add(new Integrante(int.Parse(aID[0])));
                }

                // ANTIGUO COORDINADOR

                //strAsunto = "Usuario ( " + aID2[0] + "/" + aID2[1] + " ) Cambio de coordinador en la Orden: Notificamos al Cordinador-viejo";

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

                resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID2[0]));

                if (resultadoBusqueda.Count == 0)
                {
                    Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                    ListaCorreo.Add(new Integrante(int.Parse(aID2[0])));
                }

                for (int j = 0; j < aCorreoResponsable.Length; j++)
                {
                    if (aCorreoResponsable[j] == "") continue;

                    string[] aID3 = Regex.Split(aCorreoResponsable[j], "/");
                    //strMensaje = strCabecera + strDatos;

                    //strAsunto = " Usuario ( " + aID3[0] + "/" + aID3[1] + " ) RESPONSABLE";

                    if (aID3[1].Trim() == "")
                    {
                        strMensaje = " La aplicación GESTAR ha intentado enviar correo al usuario con código FICEPI " + aID3[0] + " y no ha podido. EL motivo es que no tiene asignado código de red. Por favor, rogamos se corrija esta situación. ";
                        strTO = "IntranetCau"; //caso de una persona que no tenga cod_red
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 13);
                        strTO = "EDA";
                    }
                    else
                    {
                        strTO = aID3[1];
                    }
                    resultadoBusqueda = ListaCorreo.FindAll(new SearchListDtoArg("ID", aID3[0]));

                    if (resultadoBusqueda.Count == 0)
                    {
                        Correo.Enviar(strAsunto, strMensaje, strTO, "", "", "", 2);
                        ListaCorreo.Add(new Integrante(int.Parse(aID3[0])));
                    }
                }                     
            }
            // FIN CORREO POR MODIFICACIÓN DE DATOS

            return sResul;
        }
        private string BorrarTareas(string sTareas)
        {
            string sResul;
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

            try
            {
                string[] aTareas = Regex.Split(sTareas, ",");

                foreach (string oTarea in aTareas)
                {
                    TAREA.Delete(tr, int.Parse(oTarea));
                }
            }
            catch (System.Exception objError)
            {
                sResul = Errores.mostrarError("Error al borrar la tarea.", objError);
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr, oConn);
                return "N@@" + sResul;
            }

            try
            {
                GESTAR.Capa_Negocio.Conexion.CommitTransaccion(tr);

                sResul = "";
            }
            catch (Exception ex)
            {
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                sResul = "N@@" + Errores.mostrarError("Error al borrar la tarea ( commit )", ex);
            }
            finally
            {
                GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
            }

            return sResul;
        }			
        private string ObtenerDocumentosArea(string sIdArea)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

                SqlDataReader dr = DOCAREA.Catalogo( int.Parse(sIdArea));

                sbuilder.Append("<table id='tblDocumentosArea' style='width: 850px; text-align:left'>");
                sbuilder.Append("<colgroup><col style='width:305px;' /><col width='200px' /><col width='210px' /><col width='135px' /></colgroup>");
                int i = 0;
                while (dr.Read())
                {
                    if (((bool)dr["t083_privado"]) && (int.Parse(Session["IDFICEPI"].ToString()) != int.Parse(dr["t083_autor"].ToString())) && (hdnAdmin.Text != "A")) continue;

                    //if (i % 2 == 0) sbuilder.Append("<tr class=FA ");
                    //else sbuilder.Append("<tr class=FB ");

                    sbuilder.Append("tr style='height:18px' id='" + dr["t083_iddocut"].ToString() + "' onclick='mm(event);' sTipo='A' sAutor='" + dr["t083_autor"].ToString() + "'>");

                    sbuilder.Append("<td style='padding-left:5px;'");

                    //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
                    if ((dr["t083_autor"].ToString() == Session["IDFICEPI"].ToString())
                        || (!(bool)dr["t083_privado"] && !(bool)dr["t083_modolectura"])
                        || (hdnAdmin.Text == "A")
                       )
                        sbuilder.Append("ondblclick=\"mDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\" ");

                    sbuilder.Append(" title=\"'" + dr["t083_descripcion"].ToString() + "\" ><nobr class='NBR' style='width:280px;text-overflow:ellipsis;overflow:hidden'>" + dr["t083_descripcion"].ToString() + "</nobr></td>");

                    if (dr["t083_nombrearchivo"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        //sbuilder.Append("<td><img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.sTipo, this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + dr["t083_nombrearchivo"].ToString() + "\">&nbsp;<nobr class='NBR' style='width:195px;text-overflow:ellipsis;overflow:hidden' title=\"" + dr["t083_nombrearchivo"].ToString() + "\">" + dr["t083_nombrearchivo"].ToString() + "</nobr></td>");
                        sbuilder.Append("<td>");
                        ////Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                        if ((!(bool)dr["t083_privado"]) ||
                             ((bool)dr["t083_privado"] && (dr["t083_autor"].ToString() == Session["IDFICEPI"].ToString() || hdnAdmin.Text == "A"))
                            )
                            sbuilder.Append("<img src=\"../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + dr["t083_nombrearchivo"].ToString() + "\">");

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
                return "N@@" + Errores.mostrarError("Error al obtener los documentos relacionados con el área", ex);
            }
        }
        private string Abrir(int sID)
        {
            string sResul = "";

            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                sResul = "N@@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }

            try
            {
                Deficiencias.Abrir
                        (
                        tr,
                        sID,
                        int.Parse(Session["IDFICEPI"].ToString())
                        );

                try
                {
                    Conexion.CommitTransaccion(tr);
                    sResul = "OK@@";
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    sResul = "N@@" + Errores.mostrarError("Error al grabar los datos ( commit )", ex);
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }

            }
            catch (System.Exception objError)
            {
                sResul = Errores.mostrarError("Error al abrir la DEFICIENCIA.", objError);
                Conexion.CerrarTransaccion(tr);
                return "N@@" + sResul;
            }
            return sResul;
        }			    
        //private string ObtenerTareas(string sId, byte nOrden, byte nAscDesc, byte bRtdo, string sFechaInicio, string sFechaFin)
        private string ObtenerTareas(string sId, byte nOrden, byte nAscDesc)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

                //DateTime? dFechaInicio = null;
                //if (sFechaInicio != "") dFechaInicio = DateTime.Parse(sFechaInicio);

                //DateTime? dFechaFin = null;
                //if (sFechaFin != "") dFechaFin = DateTime.Parse(sFechaFin);

                //if (sFechaFin == "" && sFechaInicio != "") dFechaFin = DateTime.Parse("01/01/2079");

                //SqlDataReader dr = TAREA.Catalogo(int.Parse(sId), nOrden, nAscDesc, bRtdo, dFechaInicio, dFechaFin);
                SqlDataReader dr;
                if (
                    (hdnEsTecnico.Text == "true") &&  
                    (hdnAdmin.Text != "A") &&
                    (hdnEsPromotor_o_Responsable.Text != "true") &&
                    (hdnEsCoordinador.Text != "true") 
                    )
                    dr = TAREA.Catalogo(int.Parse(sId), nOrden, nAscDesc,int.Parse(Session["IDFICEPI"].ToString()));
                else
                    dr = TAREA.Catalogo(int.Parse(sId), nOrden, nAscDesc, 0);

                sbuilder.Append("<table id='tblCatalogoTarea' style='width: 830px;'>");
                sbuilder.Append("<colgroup><col style='width:125px;' /><col style='width:435px' /><col style='width:125px' /><col style='width:115px' /></colgroup>");

                int i = 0;
                while (dr.Read())
                {
                    sbuilder.Append("<tr id='" + dr["ID"].ToString() + "' ");

                    //if (i % 2 == 0)
                    //    sbuilder.Append("class='FA' ");
                    //else
                    //    sbuilder.Append("class='FB' ");
                    
                    i++;
                    sbuilder.Append(" onclick=mm(event); ");
                    sbuilder.Append(" ondblclick=this.className='FS';Det_Tarea(this); ");

                    sbuilder.Append(" style='cursor: pointer;height:16px'>");
                    sbuilder.Append("<td  style='padding-left:5px;' align='center'>" + dr["ID"].ToString() + "</td>");
                    sbuilder.Append("<td>" + dr["DENOMINACION"].ToString() + "</td>");
                    sbuilder.Append("<td>" + dr["RESULTADO"].ToString() + "</td>");
                    sbuilder.Append("<td align='center'>" + dr["T072_AVANCE"].ToString() + "</td>");
                    sbuilder.Append("</tr>" + (char)13);
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
        private string ObtenerDocumentos(string sId)
        {
            try
            {
                System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

                SqlDataReader dr = DOCDEFICIENCIA.Catalogo( int.Parse(sId));

                sbuilder.Append("<table id='tblDocumentos' style='width: 850px; text-align:left'>");
                sbuilder.Append("<colgroup><col style='width:305px;'/><col width='200px' /><col width='210px' /><col width='135px' /></colgroup>");
                int i = 0;
                while (dr.Read())
                {
                    if (((bool)dr["t084_privado"]) && (int.Parse(Session["IDFICEPI"].ToString()) != int.Parse(dr["t084_autor"].ToString())) && (hdnAdmin.Text != "A")) continue;

                    //if (i % 2 == 0) sbuilder.Append("<tr class=FA ");
                    //else sbuilder.Append("<tr class=FB ");

                    sbuilder.Append("<tr style='height:18px' id='" + dr["t084_iddocut"].ToString() + "' onclick='mm(event);' sTipo='D' sAutor='" + dr["t084_autor"].ToString() + "'>");

                    sbuilder.Append("<td style='padding-left:5px;'");

                    //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
                    if ((dr["t084_autor"].ToString() == Session["IDFICEPI"].ToString())
                        || (!(bool)dr["t084_privado"] && !(bool)dr["t084_modolectura"])
                        || (hdnAdmin.Text == "A")
                       )
                        sbuilder.Append("ondblclick=\"mDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\" ");

                    sbuilder.Append(" title=\"'" + dr["t084_descripcion"].ToString() + "\" ><nobr class='NBR' style='width:280px;text-overflow:ellipsis;overflow:hidden'>" + dr["t084_descripcion"].ToString() + "</nobr></td>");

                    if (dr["t084_nombrearchivo"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        sbuilder.Append("<td>");
                        ////Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                        if ((!(bool)dr["t084_privado"]) ||
                             ((bool)dr["t084_privado"] && (dr["t084_autor"].ToString() == Session["IDFICEPI"].ToString() || hdnAdmin.Text == "A"))
                            )
                            sbuilder.Append("<img src=\"../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + dr["t084_nombrearchivo"].ToString() + "\">");

                        sbuilder.Append("&nbsp;<nobr class='NBR' style='width:175px;text-overflow:ellipsis;overflow:hidden' title=\"" + dr["t084_nombrearchivo"].ToString() + "\">" + dr["t084_nombrearchivo"].ToString() + "</nobr></td>");
                    }

                    if (dr["t084_weblink"].ToString() == "")
                        sbuilder.Append("<td></td>");
                    else
                    {
                        string sHTTP = "";
                        if (dr["t084_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                        sbuilder.Append("<td title='" + dr["t084_weblink"].ToString() + "'><a href='" + sHTTP + dr["t084_weblink"].ToString() + "'><nobr class='NBR' style='width:205px;text-overflow:ellipsis;overflow:hidden'>" + dr["t084_weblink"].ToString() + "</nobr></a></td>");
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
                    DOCDEFICIENCIA.Delete(tr, int.Parse(oDoc));
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
    }
}
