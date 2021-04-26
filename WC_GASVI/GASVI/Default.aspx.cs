using System;
using System.Text.RegularExpressions;
using GASVI.BLL;

namespace GASVI
{
	public partial class Default : System.Web.UI.Page
	{
        public int intAcceso = 0;
		public string sPerfil	= "";
		public string strEnlace	= "";
		public string strMsg	= "";
        public string strCaducidad = "";
        public bool bEntrar = false;
        private bool bError = false;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Request.QueryString["in"] != null)
            {
                int nCountVariables = Session.Count;
                for (int i = nCountVariables - 1; i >= 0; i--)
                {
                    if (Session.Keys[i].Length < 5 || Session.Keys[i].Substring(0, 4) == "GVT_")
                        Session[i] = null;
                }
            }
            
            AccesoAplicaciones objAccApli = null;
            #region Control de acceso a la aplicación
            try
            {
                string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");
                if (sUrlAux[1].ToUpper() != "GASVI") Session["GVT_strServer"] = "/";
                else Session["GVT_strServer"] = "/GASVI/";

                objAccApli = AccesoAplicaciones.Comprobar();

                if (!objAccApli.Estado)
                {
                    Session["GVT_MOTIVO"] = objAccApli.Motivo;
                    //Response.Redirect("Mantenimiento.aspx", true);
                }
            }
            catch (Exception ex)
            {
                bError = true;
                strMsg += Errores.mostrarError("Error al comprobar el acceso a la aplicación:", ex);
            }
            #endregion

            #region Control de identificación del usuario
            if (!bError)
            {
                try{
                    Session["GVT_HAYNOVEDADES"] = "0";
                    Session["GVT_HAYAVISOS"] = "0";

                    //Captura del usuario de red.
                    if (Request.QueryString["scr"] != null)
                    {
                        Session["GVT_IDRED"] = Utilidades.decodpar(Request.QueryString["scr"].ToString());
                    }
                    else
                    {
                        string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
                        Array.Reverse(aIdRed);
                        //aIdRed[0] = "MAPAASSU";//Susana Parra Ash
                        //aIdRed[0] = "DOECARKA";
                        //aIdRed[0] = "DOREZUJO";
                        //aIdRed[0] = "DOIZALVI";
                        //aIdRed[0] = "DOGACLOI";//Oiane Garcia Clavijo
                        //aIdRed[0] = "DOBAALMA";//Manuel Baraza
                        //aIdRed[0] = "MAINVEPA";//Paloma Indarte
                        //aIdRed[0] = "BIZAERAI";
                        //aIdRed[0] = "CHOTMUGO";//Gorka Otamendi
                        //aIdRed[0] = "DOIPGAJM";//JM Iparraguirre
                        Session["GVT_IDRED"] = aIdRed[0];
                    }

                    Profesional oProf = Profesional.Obtener(Session["GVT_IDRED"].ToString());
                    //if (oProf.bIdentificado)
                    //{
                        if (oProf.nFilasFicepi == 0){
                            strMsg = "Acceso denegado. Usuario no autorizado.";
                        }else if (oProf.nFilasFicepi > 1){
                            strMsg = "Acceso denegado. Existe más de un profesional con el mismo código de red. Contacte con el CAU.";
                        }else{
                            Session["GVT_IDFICEPI"] = oProf.t001_idficepi;
                            Session["GVT_USUARIOSUPER"] = oProf.t314_idusuario;
                            Session["GVT_PROFESIONAL"] = oProf.Nombre;
                            Session["GVT_NUEVOGASVI"] = oProf.bNuevoGasvi;
                            Session["GVT_MULTIUSUARIO"] = oProf.bMultiUsuario;
                            //Session["GVT_ADMIN"] = oProf.bAdministrador; //Utilizaremos el Rol "A".
                            Session["GVT_IDEMPRESADEFECTO"] = oProf.t313_idempresa_defecto;
                            Session["GVT_EMPRESADEFECTO"] = oProf.Empresa_defecto;
                            Session["GVT_EMPRESA"] = oProf.Empresa;
                            Session["GVT_IDEMPRESA"] = oProf.t313_idempresa;
                            Session["GVT_SEXO"] = oProf.Sexo;
                            Session["GVT_CCIBERPER"] = oProf.nCCIberper;

                            #region Valores que se cargan una única vez al entrar
                            if (Session["GVT_IDFICEPI_ENTRADA"] == null)
                            {
                                Session["GVT_IDFICEPI_ENTRADA"] = oProf.t001_idficepi;
                                Session["GVT_IDRED_ENTRADA"] = Session["GVT_IDRED"];
                                Session["GVT_PROFESIONAL_ENTRADA"] = oProf.Nombre;
                                Session["GVT_ADMIN_ENTRADA"] = oProf.bAdministrador;
                                Session["GVT_MONEDADEFECTO"] = oProf.t422_idmoneda;
                                Session["GVT_AVISOCAMBIOESTADO"] = oProf.t001_avisocambioes;
                                Session["GVT_MOTIVODEFECTO"] = (oProf.t423_idmotivo.HasValue) ? oProf.t423_idmotivo : null;

                                if (FICEPIAVISOS.VerSiHay((int)Session["GVT_IDFICEPI_ENTRADA"])) 
                                    Session["GVT_HAYAVISOS"] = "1";
                            }
                            #endregion

                            bEntrar = true;
                            strEnlace = Session["GVT_strServer"] + "Capa_Presentacion/Inicio/default.aspx";
                            Profesional.CargarRoles();


                            #region estado aplicacion
                            if (!objAccApli.Estado){
                                if (!User.IsInRole("A"))
                                {
                                    Session["GVT_IDRED"] = null;
                                    Session["GVT_IDFICEPI"] = null;
                                    Session["GVT_USUARIOSUPER"] = null;
                                    Session["GVT_PROFESIONAL"] = null;
                                    Session["GVT_NUEVOGASVI"] = null;
                                    Session["GVT_MULTIUSUARIO"] = null;
                                    Session["GVT_EMPRESA"] = null;
                                    Session["GVT_IDEMPRESA"] = null;
                                    Session["GVT_SEXO"] = null;
                                    Session["GVT_CCIBERPER"] = null;
                                    Session["GVT_IDFICEPI_ENTRADA"] = null;
                                    Session["GVT_IDRED_ENTRADA"] = null;
                                    Session["GVT_PROFESIONAL_ENTRADA"] = null;
                                    Session["GVT_ADMIN_ENTRADA"] = null;
                                    Session["GVT_MONEDADEFECTO"] = null;
                                    Session["GVT_AVISOCAMBIOESTADO"] = null;
                                    Session["GVT_MOTIVODEFECTO"] = null;
                                    
                                    try
                                    {
                                        Response.Redirect("Mantenimiento.aspx", true);
                                    }
                                    catch (System.Threading.ThreadAbortException) { }

                                }
                                else
                                {
                                    strMsg += "El acceso a GASVI se encuentra bloqueado temporalmente.|n|n";
                                    strMsg += "Motivo:|n|n";
                                    strMsg += Session["GVT_MOTIVO"].ToString().Replace(((char)13).ToString() + ((char)10).ToString(), "|n") + "|n|n|n";
                                    if (Session["GVT_SEXO"].ToString() == "V") strMsg += "Se le autoriza el acceso por ser administrador.";
                                    else strMsg += "Se le autoriza el acceso por ser administradora.";
                                }
                            }
                            #endregion
                        }
                    //}
                    //else
                    //{
                    //    strMsg = "No se han podido obtener los datos del usuario '" + Session["GVT_IDRED"].ToString() + "'";
                    //}
                }
                catch (System.Threading.ThreadAbortException) { }
                catch (Exception ex)
                {
                    bError = true;
                    strMsg += Errores.mostrarError("Error al obtener los datos del profesional.", ex);
                }
            }
            #endregion
		}
	}
}
