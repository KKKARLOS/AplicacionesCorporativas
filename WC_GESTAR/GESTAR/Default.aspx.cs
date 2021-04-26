using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using GESTAR.Capa_Negocio;

namespace GESTAR
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
		public string sPerfil	= "";
		public string strEnlace	= "";
		public string strMsg	= "";
        public string strCaducidad = "";
        private bool bError = false;
        SqlDataReader dr = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            #region Control del estado de acceso a la aplicación
            try
            {
                string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");

                if (sUrlAux[1].ToUpper() != "GESTAR") Session["strServer"] = "/";
                else Session["strServer"] = "/GESTAR/";

			    int intAcceso;
			    if (Request.QueryString["ACCESO"]!=null)
				    intAcceso = int.Parse(Request.QueryString["ACCESO"].ToString());
			    else
				    intAcceso = 0;

			    if (intAcceso==0)
			    {
                    dr = AccesoAplicaciones.Leer();

				    Session["MOTIVO"] = "";
				    Session["ACCESO"] = false;

				    if (dr.Read()) 
				    {
					    Session["MOTIVO"]=(string)dr["T000_MOTIVO"];
					    Session["ACCESO"]= (bool)dr["T000_ESTADO"];
				    }

                    dr.Close();
                    dr.Dispose();
                    dr = null;

				    if (!(bool)Session["ACCESO"]) Response.Redirect("mantenimiento.aspx");
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
                    //Captura del usuario de red.
			        string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
			        Array.Reverse(aIdRed);
			        Session["IDRED"] = aIdRed[0];
                    //Session["IDRED"] = "DORAVIMA";//Mitxel Ranero
                    //Session["IDRED"] = "DOGAELIN";//Iñigo Garro
                    //Session["IDRED"] = "DORESAPA";//Pablo rezola
                    //Session["IDRED"] = "DONAIGYO";//Yolanda Nanclares
                    //Session["IDRED"] = "DOGAGOCO";//CORO GARIN
                    //Session["IDRED"] = "DOVESASI";//Silvia Vega
                    //Session["IDRED"] = "BICAZALA";//Laura Carrillo
                    //Session["IDRED"] = "DOASMOMI";//Mila Asenjo
                    //Session["IDRED"] = "MAMAMAJA";//Juan Antonio Martin Mayoral
                    //Session["IDRED"] = "DOIZALVI";//Victor Izaguirre
                    //Session["IDRED"] = "DOLAGAJM";//Josemi Lacalle
                    //Session["IDRED"] = "DOGACLOI";//Ohiane Garcia Clavijo
                    //Session["IDRED"] = "MAMAGOJE";//JESUS MARTIN GONZALEZ
                    //Session["IDRED"] = "MACUGOJA";//Juan Antonio Cuesta Gonzalez
                    //Session["IDRED"] = "DOERSAJU";//Juncal Errazquin
                    if ((Request.QueryString["CODRED"] != null) && 
                        (Session["IDRED"].Equals("DOPEOTCA") || 
                         Session["IDRED"].Equals("DOARHUMI") || 
                         Session["IDRED"].Equals("DOIZALVI"))) 
                        Session["IDRED"] = Request.QueryString["CODRED"].ToString();
        		
			        int intPerfil = 0;
        			
			        if (Request.QueryString["PERFIL"]!=null)
				        intPerfil = int.Parse(Request.QueryString["PERFIL"].ToString());

                    // A COMENTARIZAR AL PONER EN EXPLOTACION	
                    //			else
                    //intPerfil = 3;
                    // HASTA AQUI

                    //switch (intPerfil)
                    //{
                    //    //			    PROMOTOR			
                    //    case 1:
                    //        Session["IDRED"] = "DOREZUJO"; // 
                    //        break;
                    //    case 2:
                    //        //			SOLICITANTE
                    //        Session["IDRED"] = "DOGAGOCO"; //  
                    //        break;
                    //    case 3:
                    //        //			COORDINADOR
                    //        Session["IDRED"] = "DOTOFEAN"; //1568
                    //        break;
                    //    case 4:
                    //        //			RESPONSABLE
                    //        Session["IDRED"] = "DOIZALVI";// 1321
                    //        break;
                    //    case 5:
                    //        //			TECNICO
                    //        Session["IDRED"] = "DOPEOTCA";// 1353 DOLAGAJM //1202 //DOPEOTCA
                    //        break;
                    //}	
                    
                    //Session["IDRED"] = "BICAZALA"; //DOIZALVI DOPEOTCA DOLAGAJM
                    //Session["IDRED"] = "DOIZALVI"; //DOIZALVI DOPEOTCA "DOLAURTU"
                    //Session["IDRED"] = "BIGAGAFE"; //DOIZALVI DOPEOTCA "DOLAURTU"
                    //Session["IDRED"] = "DOGULESA";
                    //Session["IDRED"] = "DOARHUMI";
                    //Session["IDRED"] = "BIEXMOMC";
                    
                    //Session["IDRED"] = "DOGAGOCO";

                    //Session["IDRED"] = "DOLAGAJM";
                    //Session["IDRED"] = "DOIZALVI";
                    
                    //Session["IDRED"] = "DOTOFEAN";
			        dr = null;

                    dr = Recursos.ObtenerUsuario(Session["IDRED"].ToString(), 0);
        			
			        Session["COMPORTAMIENTO"] = 0;

			        if (dr.Read()) 
			        {
                        Session["CIP"] = dr["T001_CIP"].ToString();
                        Session["NOMBRE"] = dr["usuario"].ToString();
                        Session["IDFICEPI"] = dr["T001_IDFICEPI"].ToString();
                        Session["NOMBRE2"] = dr["usuario2"].ToString();
                        Session["ADMIN"] = dr["T001_PERFILGESTAR"].ToString(); // A=Administrador o B=Básico
                        Session["BTN_FECHA"] = dr["t001_botonfecha"].ToString();
                        Session["RESOLUCION"] = "";

                        if (Session["ADMIN"].ToString() == "")
                        {
                            strMsg = "Usuario no autorizado";
                        }

                        strEnlace = Session["strServer"] + "Capa_Presentacion/Areas/default.aspx";
                        GestionDeRoles();
                    }
			        else
			        {
                        strMsg = "No se han podido obtener los datos del usuario '" + Session["IDRED"].ToString() + "'";
			        }

			        dr.Close();
                    dr.Dispose();

                }
                catch (Exception ex)
                {
                    bError = true;
                    strMsg += Errores.mostrarError("Error al obtener los datos del usuario", ex);
                }
            }
            #endregion

		}

        private void GestionDeRoles()
        {
            #region Identificación de Roles (perfiles) del usuario

            //Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            //Esta comprobación podría no realizarse y manejar nosotros los roles
            //vía la pantalla de ASP.NET Configuration.
            // crea ROL A NIVEL DE APLICACIÓN

            if (!Roles.RoleExists("I")) Roles.CreateRole("I");
            if (!Roles.RoleExists("E")) Roles.CreateRole("E");

            //Se borran los roles que pudiera tener el usuario.
            //Response.Write(User.Identity.Name.ToString());
            
            foreach (string Rol in Roles.GetRolesForUser(User.Identity.Name))
            {
                if (User.IsInRole(Rol)) Roles.RemoveUserFromRole(User.Identity.Name.ToString(), Rol);
            }

            //Ahora se determinarían los perfiles que tiene el usuario para asignárselos.
            //Se realiza la validación que sea necesaria y si es correcta, se asigna el rol "I"

            dr = Areas.Lista(int.Parse(Session["IDFICEPI"].ToString()), Session["ADMIN"].ToString());
            if (dr.Read())
            {
                Roles.AddUserToRole(User.Identity.Name.ToString(), "I");
            }
            Session["ESPECIALIS"] = "N";
            dr = Areas.ListaEspe(int.Parse(Session["IDFICEPI"].ToString()), Session["ADMIN"].ToString());
           
            if (dr.Read())
            {
                Roles.AddUserToRole(User.Identity.Name.ToString(), "E");
                Session["ESPECIALIS"] = "S";
            }
           
            dr.Close();
            dr.Dispose();

            #endregion
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

	}

}
