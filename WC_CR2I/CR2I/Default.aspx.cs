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
using System.Text.RegularExpressions;
using System.Configuration;
using CR2I.Capa_Negocio;
namespace CR2I
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
		public string strEnlace	= "";
		public string strMsg	= "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //char ch = '€';
            //int charCode = Convert.ToInt32(ch);
            //string binaryCharCode = Convert.ToString(136, 2);
            //string a = "";

            #region Control del estado de acceso a la aplicación
            bool bError = false;
			try
			{
				SqlDataReader dr = null;
			
				int intAcceso;
				if (Request.QueryString["ACCESO"]!=null)
					intAcceso = int.Parse(Request.QueryString["ACCESO"].ToString());
				else
					intAcceso = 0;

				if (intAcceso==0)
				{
					AccesoAplicaciones objAccesoAplicaciones = new AccesoAplicaciones();
					dr = objAccesoAplicaciones.Leer();

					Session["CR2I_MOTIVO"] = "";
					Session["CR2I_ACCESO"] = false;

					if (dr.Read()) 
					{
						Session["CR2I_MOTIVO"]=(string)dr["T000_MOTIVO"];
						Session["CR2I_ACCESO"]= (bool)dr["T000_ESTADO"];
					}
                    dr.Close();
                    dr.Dispose();
                    
                    if (!(bool)Session["CR2I_ACCESO"]) Response.Redirect("mantenimiento.aspx");
				}
			}
			catch(Exception ex)
			{
				bError = true;
				strMsg += Errores.mostrarError("Error al comprobar el acceso a la aplicación:",ex);
            }
            #endregion

            #region Control de identificación del usuario
            if (!bError)
			{
				string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");
                if (sUrlAux[1].ToUpper() != "CR2I") Session["strServer"] = "/";
                else Session["strServer"] = "/CR2I/";

                if (Request.ServerVariables["SERVER_NAME"].ToLower().IndexOf("wwwsec") > -1)
                {
                    Session["strOrigenAcceso"] = "Externo";
                }
                else
                {
                    Session["strOrigenAcceso"] = "Interno";
                }

				//Captura del usuario de red.
				string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
				Array.Reverse(aIdRed);

                //aIdRed[0]="DOALPABE";//Beñat Alonso Paulos
                //aIdRed[0]="DOGABEJO";
                //aIdRed[0] = "MAPESISC";//Soledad perez Sixto
                Session["CR2I_IDRED"] = aIdRed[0];

				try
				{
					if (Session["CR2I_IDRED"].ToString() != "")
					{
						Recurso objUsuario = new Recurso();
						SqlDataReader dr = null;

						dr = objUsuario.ObtenerUsuario(Session["CR2I_IDRED"].ToString());
			
						if (dr.Read()) 
						{
							Session["CR2I_CIP"]		= dr["T001_IDCIP"].ToString();
							Session["CR2I_IDFICEPI"]	= int.Parse(dr["T001_IDFICEPI"].ToString());
							Session["CR2I_NOMBRE"]	= dr["T001_NOMBRE"].ToString();
							Session["CR2I_APELLIDO1"]= dr["T001_APELLIDO1"].ToString();
							Session["CR2I_APELLIDO2"]= dr["T001_APELLIDO2"].ToString();
							Session["CR2I_EMAIL"]	= dr["T001_EMAIL"].ToString().Trim();
							Session["CR2I_OFICINA"]	= int.Parse(dr["T010_IDOFICINA"].ToString());
							Session["CR2I_PERFIL"]	= dr["T001_PERFILCR2I"].ToString().Trim();
							Session["CR2I_RESSALA"]	= dr["T001_RESSALA"].ToString().Trim();
							Session["CR2I_RESVIDEO"]= dr["T001_RESVIDEO"].ToString().Trim();
                            Session["RESWEBEX"] = dr["T001_RESWEBEX"].ToString().Trim();
                            Session["RESWIFI"] = dr["T001_RESWIFI"].ToString().Trim();

							//Recoger parámetro que se podría enviar desde diferentes partes
							//de la intranet.

							if (Session["CR2I_PERFIL"].ToString() == "")
							{
								strMsg	= "Usuario no autorizado";
							}
							else
							{
								string nOpcion = Request.QueryString["nOpcion"];
								if (nOpcion == null)
								{
									strEnlace = Session["strServer"]+"Capa_Presentacion/Default.aspx";
								}
								else
								{
									switch (nOpcion.ToString())
									{
										case "1":
											if (Session["CR2I_RESSALA"].ToString() == "") strMsg	= "Acceso no autorizado al módulo de salas de reunión.";
											else strEnlace = Session["strServer"]+"Capa_Presentacion/Salas/ConsultaOfi/Default.aspx";;
											break;
										case "2":
											if (Session["CR2I_RESSALA"].ToString() == "") strMsg	= "Acceso no autorizado al módulo de salas de reunión.";
											else strEnlace = Session["strServer"]+"Capa_Presentacion/Salas/ConsultaSal/Default.aspx";;
											break;
										case "3":
											if (Session["CR2I_RESVIDEO"].ToString() == "") strMsg = "Acceso no autorizado al módulo de videoconferencias.";
											else strEnlace = Session["strServer"]+"Capa_Presentacion/Video/Consulta/Default.aspx";;
											break;
									}
								}
							}
						}
						else
						{
							Session["CR2I_CIP"]		= "";
							Session["CR2I_NOMBRE"]	= "";
							Session["CR2I_APELLIDO1"]= "";
							Session["CR2I_APELLIDO2"]= "";
							strMsg = "No se han podido obtener los datos del usuario: '"+ Session["CR2I_IDRED"].ToString() +"'";
						}

                        dr.Close();
                        dr.Dispose();
                    }
					else
					{
						strMsg = "Usuario de windows no identificado";
					}
				}
				catch(Exception ex)
				{
					strMsg += Errores.mostrarError("Error al obtener los datos del usuario:",ex);
				}
            }
            #endregion

            //int a, b, c;
            //a = 0;
            //b = 10;
            //c = b / a;
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
