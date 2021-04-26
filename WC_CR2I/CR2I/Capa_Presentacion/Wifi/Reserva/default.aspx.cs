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
//using Microsoft.Web.UI.WebControls;
using EO.Web;
using System.Text.RegularExpressions;
using CR2I.Capa_Negocio;
using CR2I.Capa_Datos;
using RJS.Web.WebControl;
using System.IO;
//PARA ACCESO AL LDAP
using System.Collections.Specialized;
using System.DirectoryServices;

namespace CR2I.Capa_Presentacion.Wifi.Reserva
{
    /// <summary>
    /// Descripción breve de _Default.
    /// </summary>

    public partial class Default : System.Web.UI.Page
    {
        //protected Toolbar Botonera = new Toolbar();
        public string sErrores = "";
        protected string sIDReserva;
        protected string bNuevo;
        protected string sLectura;
        protected string strInicial;
        protected string strHora;
        protected string strSalas;
        protected string nRecurso;
        public string strMsg;
        public string strLocation;
        public string sResultadoGrabacion = "";
        public string sEsInsert = "false", bEsAnular = "false";

        protected int nIDReserva;

        protected System.Web.UI.WebControls.Image Image1;

        protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset1;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset2;

        protected RecursoFisico objRF;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["CR2I_IDRED"] == null)
            {
                try { Response.Redirect("~/SesionCaducada.aspx", true); }
                catch (System.Threading.ThreadAbortException) { }
            }
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
            // All webpages displaying an ASP.NET menu control must inherit this class.
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsCallback)
            {
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.TituloPagina = "Detalle de conexión wifi";
                Master.Comportamiento = 9;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/convocados.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                Utilidades.SetEventosFecha(this.txtFechaIni);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                this.txtSolicitante.Attributes.Add("readonly", "readonly");
                this.txtUsuario.Attributes.Add("readonly", "readonly");
                this.txtPwd.Attributes.Add("readonly", "readonly");

                bNuevo = "";
                strInicial = "";
                sLectura = "false";
                strMsg = "";
                strLocation = "";

                string sHoy = System.DateTime.Today.ToShortDateString();

                //bool bCargando = false;
                //if (Request.QueryString["I"] != null)
                //{
                //    if (Request.QueryString["I"] == "1")
                //        bCargando = true;
                //}
                if (!Page.IsPostBack)//|| bCargando
                {
                    //bNuevo = Request.Form["ctl00$CPHC$hdnNuevo"];
                    if (Request.QueryString["hdnNuevo"] != null)
                        bNuevo = Utilidades.decodpar(Request.QueryString["hdnNuevo"].ToString());//Request.Form["ctl00$CPHC$hdnNuevo"];
                    if (bNuevo == "True")
                    {
                        //Botonera.Items[2].Enabled = false;
                        this.txtSolicitante.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                        this.txtCIP.Text = Session["CR2I_CIP"].ToString();
                        //this.txtInteresado.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                        this.txtFechaIni.Text = DateTime.Today.ToShortDateString();
                        this.txtFechaFin.Text = DateTime.Today.ToShortDateString();
                        if (DateTime.Now.Minute < 30)
                            this.cboHoraIni.SelectedValue = (DateTime.Now.Hour).ToString() + ":00";
                        else
                            this.cboHoraIni.SelectedValue = (DateTime.Now.Hour).ToString() + ":30";

                        //                    this.cboHoraIni.SelectedValue = "7:00";// Request.Form["ctl00$CPHC$hdnHora"].ToString().Trim();
                        if (this.cboHoraIni.SelectedIndex < 22) this.cboHoraFin.SelectedValue = "18:00";//this.cboHoraIni.SelectedIndex + 1;
                        else this.cboHoraFin.SelectedIndex = this.cboHoraIni.SelectedIndex + 1;
                    }
                    else
                    {
                        //Botonera.Items[4].Enabled = false;
                        //Botonera.Items[6].Enabled = true;
                        //Botonera.Items[8].Enabled = true;
                        if (Request.QueryString["hdnReserva"] != null)
                            sIDReserva = Utilidades.decodpar(Request.QueryString["hdnReserva"].ToString());//Request.Form["ctl00$CPHC$hdnReserva"];
                        if (sIDReserva != null)
                        {
                            hdnIDReserva.Text = sIDReserva;
                            WIFI oWifi = WIFI.Obtener(null, int.Parse(hdnIDReserva.Text));

                            this.txtSolicitante.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                            //this.txtCIP.Text = Session["CR2I_CIP"].ToString();
                            this.txtInteresado.Text = oWifi.t085_interesado;
                            this.txtFechaIni.Text = oWifi.t085_fechoraini.ToShortDateString();
                            this.txtFechaFin.Text = oWifi.t085_fechorafin.ToShortDateString();
                            this.cboHoraIni.SelectedValue = oWifi.t085_fechoraini.ToShortTimeString();
                            this.cboHoraFin.SelectedValue = oWifi.t085_fechorafin.ToShortTimeString();
                            this.txtEmpresa.Text = oWifi.t085_empresa;
                            this.txtObservaciones.Text = oWifi.t085_observaciones;
                            this.txtUsuario.Text = oWifi.t085_usuwifi;
                            this.txtPwd.Text = oWifi.t085_pwdwifi;
                            this.hdnEstado.Text = oWifi.t085_estado.ToString();
                            //fstConexion.Style.Add("visibility","visible");

                            if (oWifi.t085_estado == 3 || oWifi.t085_estado == 4)
                            {
                                //Botonera.Items[0].Enabled = false;
                                //if (oWifi.t085_estado == 4)
                                //    Botonera.Items[2].Enabled = false;
                                //System.Web.UI.Control Area = this.FindControl("AreaTrabajo");
                                //ModoLectura.Poner(Area.Controls);
                                sLectura = "true";
                                System.Web.UI.Control Area = this.AreaTrabajo.FindControl("tblFiltros");
                                if (Area != null)
                                    ModoLectura.Poner(Area.Controls);
                                else
                                    ModoLectura.Poner(this.Controls);
                            }
                        }
                    }
                }//Fin de Postback
                //CargarDatos();

            }
        }// Fin de Load

        //private void CargarBotonera()
        //{
        //    CBotonera objBot = new CBotonera();
        //    objBot.CargarBotonera(Botonera, this.Comportamiento, Session["strServer"].ToString());
        //    this.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
        //    this.BarraBotones.Controls.Add(Botonera);
        //}

        //private void Botonera_Click(object sender, System.EventArgs e)
        //{
        //    ToolbarItem btn = (ToolbarItem)sender;
        //    string sMsg = "Clicked on object '" + sender.ToString() +" / " + Botonera.Items.FlatIndexOf(btn).ToString();

        //    //Response.Write(sMsg);
        //    //			switch (Botonera.Items.FlatIndexOf(btn))
        //    switch (btn.ID.ToLower())
        //    {
        //        case "procesar":
        //            Grabar();
        //            break;
        //        case "anular":
        //            Eliminar();
        //            break;
        //        case "cancelar":
        //            Cancelar();
        //            break;
        //        case "regresar":
        //            Cancelar();
        //            break;
        //    }
        //}
        public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
                //case "regresar":
                //    //string sUrl = HistorialNavegacion.Leer();
                //    try
                //    {
                //        Response.Redirect("../../Default.aspx");
                //    }
                //    catch (System.Threading.ThreadAbortException) { }
                //    break;
                case "procesar":
                    Grabar();
                    break;
                case "anular":
                    Eliminar();
                    break;
                case "cancelar":
                    Cancelar();
                    break;
                case "regresar":
                    Cancelar();
                    break;
            }
        }

        private void Grabar()
        {
            bool bErrorControlado = false;
            ArrayList aListCorreo = new ArrayList();
            string sAsunto = "";
            string sTexto = "";
            string sTO = "";
            string strFecIniOld = "";
            string strFecFinOld = "";
            string strInteresadoOld = "";
            string strEmpresaOld = "";

            string sUsuario = "", sPassword = "";

            if (this.hdnIDReserva.Text != "")
            {
                //Si se trata de una reserva existente, se obtienen sus datos
                //para luego comunicar las modificaciones realizadas.
                WIFI oWifi = WIFI.Obtener(null, int.Parse(this.hdnIDReserva.Text));
                strFecIniOld = oWifi.t085_fechoraini.ToString();
                strFecFinOld = oWifi.t085_fechorafin.ToString();
                strInteresadoOld = oWifi.t085_interesado;
                strEmpresaOld = oWifi.t085_empresa;

                if (strFecIniOld.Length == 19) strFecIniOld = strFecIniOld.Substring(0, 16);
                else strFecIniOld = strFecIniOld.Substring(0, 15);
                if (strFecFinOld.Length == 19) strFecFinOld = strFecFinOld.Substring(0, 16);
                else strFecFinOld = strFecFinOld.Substring(0, 15);
            }

            SqlConnection oConn = Conexion.Abrir();
            SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

            DateTime dInicio = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue);
            DateTime dFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue);
            DateTime dNow = DateTime.Now;

            try
            {
                if (this.hdnIDReserva.Text == "")  //insert
                {
                    #region Código Insert
                    sEsInsert = "true";
                    string sTicks = DateTime.Now.Ticks.ToString();
                    //string sTicksReducida = sTicks.Substring(10, 8);
                    //sUsuario = "IB" + EncodeTo64(sTicksReducida).Substring(0, 6).ToUpper();

                    //sPassword = EncodeTo64((int.Parse(sTicksReducida) + ((int)Session["CR2I_IDFICEPI"] * int.Parse(sTicksReducida))).ToString());
                    //sPassword = sPassword.Substring(sPassword.Length - 10, 8);

                    sPassword = sTicks.Substring(sTicks.Length - 4, 4);
                    //sUsuario = "ib" + sTicksReducida;
                    //sPassword = (int.Parse(sTicks.Substring(0, 8)) * (int)Session["CR2I_IDFICEPI"]).ToString().Substring(0, 8);
                    //sPassword = (long.Parse(sTicks.Substring(sTicks.Length - 8, 8)) * long.Parse(Session["CR2I_IDFICEPI"].ToString())).ToString();
                    //sPassword = sPassword.Substring(sPassword.Length-8, 8);


                    //Datos de la reserva
                    byte nEstado = 1;
                    if (dInicio < dNow && dFin > dNow)
                        nEstado = 2;
                    int nResul = WIFI.Insert(tr,
                                        (int)Session["CR2I_IDFICEPI"],
                                        txtInteresado.Text,
                                        txtEmpresa.Text,
                                        dInicio,
                                        dFin,
                                        txtObservaciones.Text,
                                        nEstado,
                                        sPassword);
                    sUsuario = "ib" + nResul.ToString().Substring(nResul.ToString().Length - 4, 4);
                    txtUsuario.Text = sUsuario;
                    txtPwd.Text = sPassword;

                    try
                    {
                        if (dInicio < dNow && dFin > dNow)
                        {//hay que crear la reserva directamente en el LDAP 
                            DirectoryEntry de = new DirectoryEntry("LDAP://172.20.254.150:389/ou=people,dc=visitas,dc=ib",
                                                    "cn=vadmin,dc=visitas,dc=ib",
                                                    "PruebaLDAP",
                                                    AuthenticationTypes.FastBind);
                            DirectoryEntries entries = de.Children;
                            DirectoryEntry oUser = entries.Add("cn=" + sUsuario, "inetOrgPerson");

                            //oUser.Properties["dn"].Add("cn=" + sUsuario + ",ou=people,dc=visitas,dc=ib");
                            oUser.Properties["objectClass"].Add("inetOrgPerson");
                            oUser.Properties["cn"].Add(sUsuario);
                            oUser.Properties["sn"].Add(sUsuario);
                            oUser.Properties["uid"].Add(sUsuario);
                            oUser.Properties["userpassword"].Add(sPassword);
                            oUser.Properties["ou"].Add("Visitas");

                            oUser.CommitChanges();

                            //DirectoryEntry oUserDelete = entries.Find("cn=" + sUsuario, "inetOrgPerson");
                            //entries.Remove(oUserDelete);
                            //generar error
                            //DirectoryEntry oUserDeletex = entries.Find("cn=x" + sUsuario, "inetOrgPerson");
                        }

                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        //string s = "";
                        //No existe o no se ha encontrado el usuario
                    }
                    catch (Exception ex)
                    {
                        sErrores = "Error : " + ex.Message;
                    }

                    hdnIDReserva.Text = nResul.ToString();

                    sTO = Session["CR2I_IDRED"].ToString();

                    sAsunto = "Reserva WIFI";

                    string sFecIni = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue).ToString();
                    if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                    else sFecIni = sFecIni.Substring(0, 15);
                    string sFecFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue).ToString();
                    if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                    else sFecFin = sFecFin.Substring(0, 15);

                    sTexto = "<p style='font-size:12px'>" + this.txtSolicitante.Text + @"
							 ha solicitado una reserva WIFI para <b>" + this.txtInteresado.Text + @"</b><br /><br /><br />
							<span style='width:150px'><b>Inicio:</b></span> " + sFecIni + @"<br />
							<span style='width:150px'><b>Fin:</b></span> " + sFecFin + @"<br /><br />
							<span style='width:150px'><b>Usuario:</b></span> " + txtUsuario.Text + @"<br />
							<span style='width:150px'><b>Contraseña:</b></span> " + txtPwd.Text + @"<br /><br />
							<span style='width:150px'><b>Observaciones:</b></span> " + txtObservaciones.Text.Replace(((char)10).ToString(), "<br />") + @"<br /><br /><br /><br /></p>";

                    string[] aMail = { sAsunto, sTexto, sTO, "", "I", "" };
                    aListCorreo.Add(aMail);

                    #endregion
                }
                else  //update
                {
                    #region Código Update
                    //Datos de la reserva
                    WIFI oWifi = WIFI.Obtener(tr, int.Parse(hdnIDReserva.Text));

                    byte nEstado = oWifi.t085_estado;
                    if (dInicio < dNow && dFin > dNow)
                        nEstado = 2;
                    WIFI.Actualizar(tr, int.Parse(hdnIDReserva.Text),
                                        (int)Session["CR2I_IDFICEPI"],
                                        txtInteresado.Text,
                                        txtEmpresa.Text,
                                        dInicio,
                                        dFin,
                                        txtObservaciones.Text,
                                        nEstado,
                                        txtPwd.Text);

                    try
                    {
                        if (dInicio < dNow && dFin > dNow)
                        {
                            DirectoryEntry de = new DirectoryEntry("LDAP://172.20.254.150:389/ou=people,dc=visitas,dc=ib",
                                                    "cn=vadmin,dc=visitas,dc=ib",
                                                    "PruebaLDAP",
                                                    AuthenticationTypes.FastBind);
                            DirectoryEntries entries = de.Children;

                            //1º Borrar la reserva WIFI que pudiera existir.
                            try
                            {
                                DirectoryEntry oUserDelete = entries.Find("cn=" + txtUsuario.Text, "inetOrgPerson");
                                entries.Remove(oUserDelete);
                            }
                            catch (System.Runtime.InteropServices.COMException)
                            {
                                //string s = "";
                                //No existe o no se ha encontrado el usuario
                            }

                            //2º Hay que crear la reserva directamente en el LDAP 
                            DirectoryEntry oUser = entries.Add("cn=" + txtUsuario.Text, "inetOrgPerson");

                            //oUser.Properties["dn"].Add("cn=" + sUsuario + ",ou=people,dc=visitas,dc=ib");
                            oUser.Properties["objectClass"].Add("inetOrgPerson");
                            oUser.Properties["cn"].Add(txtUsuario.Text);
                            oUser.Properties["sn"].Add(txtUsuario.Text);
                            oUser.Properties["uid"].Add(txtUsuario.Text);
                            oUser.Properties["userpassword"].Add(txtPwd.Text);
                            oUser.Properties["ou"].Add("Visitas");

                            oUser.CommitChanges();
                        }

                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        //string s = "";
                        //No existe o no se ha encontrado el usuario
                    }
                    catch (Exception ex)
                    {
                        sErrores = "Error : " + ex.Message;
                    }

                    sTO = Session["CR2I_IDRED"].ToString();

                    sAsunto = "Modificación reserva WIFI.";

                    string sFecIni = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue).ToString();
                    if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                    else sFecIni = sFecIni.Substring(0, 15);
                    string sFecFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue).ToString();
                    if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                    else sFecFin = sFecFin.Substring(0, 15);

                    sTexto = @"<p style='font-size:12px'>La reserva WIFI <br /><br />
							<span style='width:150px'><b>Inicio:</b></span> " + strFecIniOld + @"<br />
							<span style='width:150px'><b>Fin:</b></span> " + strFecFinOld + @"<br /><br />
							<span style='width:150px'><b>Interesado:</b></span> " + strInteresadoOld + @"<br />
							<br />Ha sido modificada por " + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + @"  
							y se reservará <br /><br />
							<span style='width:150px'><b>Inicio:</b></span> " + sFecIni + @"<br />
							<span style='width:150px'><b>Fin:</b></span> " + sFecFin + @"<br /><br />
							<span style='width:150px'><b>Interesado:</b></span> " + txtInteresado.Text + @"<br /><br />
							<span style='width:150px'><b>Usuario:</b></span> " + txtUsuario.Text + @"<br />
							<span style='width:150px'><b>Contraseña:</b></span> " + txtPwd.Text + @"<br /><br /><br /><br /></p>";

                    string[] aMail = { sAsunto, sTexto, sTO, "", "I", "" };
                    aListCorreo.Add(aMail);

                    #endregion
                }

                Conexion.CommitTransaccion(tr);
                sResultadoGrabacion = "OK";
            }
            catch (Exception ex)
            {
                if (!bErrorControlado) sErrores += Errores.mostrarError("Error al realizar la reserva:", ex);
                else sErrores = ex.Message;
                Conexion.CerrarTransaccion(tr);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }

            try
            {
                Correo.EnviarCorreos(aListCorreo);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al enviar los mails de convocatoria:", ex);
            }
        }

        private void Eliminar()
        {
            bool bError = false;
            ArrayList aListCorreo = new ArrayList();
            string sAsunto = "";
            string sTexto = "";
            string sTO = "";

            SqlConnection oConn = Conexion.Abrir();
            SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

            try
            {
                bEsAnular = "true";
                WIFI oWifi = WIFI.Obtener(tr, int.Parse(hdnIDReserva.Text));

                sTO = Session["CR2I_IDRED"].ToString();
                sAsunto = "Anulación reserva WIFI.";

                string sFecIni = oWifi.t085_fechoraini.ToString();
                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                else sFecIni = sFecIni.Substring(0, 15);
                string sFecFin = oWifi.t085_fechorafin.ToString();
                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                else sFecFin = sFecFin.Substring(0, 15);

                //int nResul = WIFI.Eliminar(tr, int.Parse(hdnIDReserva.Text));
                WIFI.Actualizar(tr, int.Parse(hdnIDReserva.Text),
                                    oWifi.t001_idficepi,
                                    oWifi.t085_interesado,
                                    oWifi.t085_empresa,
                                    oWifi.t085_fechoraini,
                                    oWifi.t085_fechorafin,
                                    oWifi.t085_observaciones,
                                    4,
                                    oWifi.t085_pwdwifi);

                sTexto = @"<p style='font-size:12px'>La reserva WIFI para el interesado " + oWifi.t085_interesado + @"<br /><br />
								<br /><span style='width:150px'><b>Inicio:</b></span> " + oWifi.t085_fechoraini.ToString() + @"<br />
								<span style='width:150px'><b>Fin:</b></span> " + oWifi.t085_fechorafin.ToString() + @"
								<br /><br />Ha sido anulada por <b>" + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + @"
								</b>.<br /><br /><span style='width:170px'><b>Motivo de anulación:</b></span> " + this.hdnAnulacion.Text + @"<br /><br /><br /></p>";

                string[] aMail = { sAsunto, sTexto, sTO, "", "I", "" };
                aListCorreo.Add(aMail);

                Conexion.CommitTransaccion(tr);
                sResultadoGrabacion = "OK";
            }
            catch (Exception ex)
            {
                bError = true;
                this.sErrores = Errores.mostrarError("Error al avisar a los convocados de la anulación de la reseva", ex);
                Conexion.CerrarTransaccion(tr);
                return;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }

            try
            {
                Correo.EnviarCorreos(aListCorreo);
            }
            catch (Exception ex)
            {
                bError = true;
                sErrores += Errores.mostrarError("Error al enviar los mails de anulación de convocatoria:", ex);
            }

            if (bError == false) Cancelar();
        }

        private void Cancelar()
        {
            Response.Redirect("../Consulta/Default.aspx", true);
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

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;
        }
    }
}
