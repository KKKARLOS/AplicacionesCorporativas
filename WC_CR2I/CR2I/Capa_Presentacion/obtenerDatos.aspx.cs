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
using CR2I.Capa_Datos;
using CR2I.Capa_Negocio;
using System.Text;
using Microsoft.JScript;

namespace CR2I.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerDatos.
	/// </summary>
	public partial class obtenerDatos : System.Web.UI.Page
	{
		protected string strResultado;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				string sInicial;

				switch (int.Parse(Request.QueryString["intOpcion"].ToString()))
				{
					case 1:  //Buscar un recurso en el abecedario
						sInicial	= Request.QueryString["strInicial"];
						strResultado = obtenerRecurso(sInicial);
						break;
					case 2:  //Buscar un recurso en el abecedario para añadirlo como asistente
                        string sAp1 = Request.QueryString["sAp1"];
                        string sAp2 = Request.QueryString["sAp2"];
                        string sNombre = Request.QueryString["sNombre"];
                        strResultado = obtenerAsistente(GlobalObject.unescape(sAp1), GlobalObject.unescape(sAp2), GlobalObject.unescape(sNombre));
						break;
					case 3:  //Para mostrar los perfiles de CR2I de los usuarios
						sInicial	= Request.QueryString["strInicial"];
						strResultado = obtenerPerfilesCR2I(sInicial);
						break;
					case 4:  //Para los perfiles de CR2I de un usuario
						string strCIP	= Request.QueryString["strCIP"];
						string sCR2I	= Request.QueryString["sCR2I"];
						string sReunion	= Request.QueryString["sReunion"];
						string sVideo	= Request.QueryString["sVideo"];
						string sWebex	= Request.QueryString["sWebex"];
						string sWifi	= Request.QueryString["sWifi"];
                        strResultado = actualizarPerfilesCR2I(strCIP, sCR2I, sReunion, sVideo, sWebex, sWifi);
						break;
					case 5:  //Buscar un recurso en el abecedario para añadirlo como asistente con IDFICEPI
                        string sAp1F = Request.QueryString["sAp1"];
                        string sAp2F = Request.QueryString["sAp2"];
                        string sNombreF = Request.QueryString["sNombre"];
                        strResultado = obtenerAsistenteFICEPI(GlobalObject.unescape(sAp1F), GlobalObject.unescape(sAp2F), GlobalObject.unescape(sNombreF));
						break;
                    case 6:  //Buscar reservas WIFI
                        string sSoloActivas = Request.QueryString["sSoloActivas"];
                        strResultado = obtenerReservasWIFI(sSoloActivas);
                        break;
                }

				Response.Write(strResultado);
			}
		}

		public string obtenerRecurso(string sInicial)
		{

			SqlConnection oConn = Datos.abrirConexion;
			SqlCommand cmd = new SqlCommand("FIC_PROFESIONAL",oConn);
			cmd.CommandType=CommandType.StoredProcedure;
			
			SqlCommandBuilder.DeriveParameters(cmd);  
			cmd.Parameters[1].Value = sInicial;

			StringBuilder strBuilder = new StringBuilder();

			try
			{
				SqlDataReader dr;  
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		
				int i = 0;
				strBuilder.Append("<table id='tblOpciones' name='tblOpciones' style='width:396px;text-align:left;' border='0' cellspacing='0' cellpadding='0'>");
				while (dr.Read()) 
				{
					if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
					else  strBuilder.Append("<tr class=FB ");
					i++;
                    strBuilder.Append("id='" + dr["T001_CIP"].ToString() + "' idFicepi='" + dr["T001_IDFICEPI"].ToString() + "' onClick='marcarUnaFila(this.id,this.rowIndex)' onDblClick='aceptarClick(this.rowIndex)' style='cursor:pointer;height:16px'><td style='padding-left:5px'><label class=texto id='lbl" + dr["T001_CIP"].ToString() + "' style='width:380px;text-overflow:ellipsis;overflow:hidden'");
			
					if (dr["DESCRIPCION"].ToString().Length > 80)
					{
						strBuilder.Append(" title='"+ dr["DESCRIPCION"].ToString() +"'");
					}
			
					strBuilder.Append("><NOBR>"+ dr["DESCRIPCION"] +"</NOBR></label></td></tr>");
				}			
				strBuilder.Append("</table>");
                dr.Close();
                dr.Dispose();
            }
			catch
			{
				strBuilder.Length = 0;
				strBuilder.Append("Error");
			}
			//Response.Write(strTabla)*/
			return strBuilder.ToString();
		}

        public string obtenerAsistente(string sAp1, string sAp2, string sNombre)
		{
			SqlConnection oConn = Datos.abrirConexion;
			SqlCommand cmd = new SqlCommand("FIC_PROFESIONAL",oConn);
			cmd.CommandType=CommandType.StoredProcedure;
			
			SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Parameters[1].Value = sAp1;
            cmd.Parameters[2].Value = sAp2;
            cmd.Parameters[3].Value = sNombre;

			StringBuilder strBuilder = new StringBuilder();

			try
			{
				SqlDataReader dr;  
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		
				int i = 0;
				strBuilder.Append("<table id='tblOpciones' name='tblOpciones' style='text-align:left; width:320px' border='0' cellspacing='0' cellpadding='0'>");
				while (dr.Read()) 
				{
					if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
					else  strBuilder.Append("<tr class=FB ");
					i++;
                    //strBuilder.Append("id='" + dr["T001_CIP"].ToString() + "' onClick='marcarEstaFila(this,false)' onDblClick='convocar(this.id,this.children[0].textContent);marcarEstaFila(this,false);' style='cursor:pointer;height:16px'><td style='padding-left:5px'><label class=texto id='lbl" + dr["T001_CIP"].ToString() + "' style='width:315px;text-overflow:ellipsis;overflow:hidden'");
                    strBuilder.Append("id='" + dr["T001_CIP"].ToString() + "' onClick='marcarEstaFila(this,false)' onDblClick='convocar2(this);marcarEstaFila(this,false);' style='cursor:pointer;height:16px'><td style='padding-left:5px'><label class=texto id='lbl" + dr["T001_CIP"].ToString() + "' style='width:315px;text-overflow:ellipsis;overflow:hidden'");
			
					if (dr["DESCRIPCION"].ToString().Length > 80)
					{
						strBuilder.Append(" title='"+ dr["DESCRIPCION"].ToString() +"'");
					}
			
					strBuilder.Append("><NOBR>"+ dr["DESCRIPCION"] +"</NOBR></label></td></tr>");
				}			
				strBuilder.Append("</table>");
                dr.Close();
                dr.Dispose();
            }
			catch
			{
				strBuilder.Length = 0;
				strBuilder.Append("Error");
			}
			//Response.Write(strTabla)*/
			return strBuilder.ToString();
		}

        public string obtenerAsistenteFICEPI(string sAp1, string sAp2, string sNombre)
        {
            SqlConnection oConn = Datos.abrirConexion;
            SqlCommand cmd = new SqlCommand("FIC_PROFESIONAL", oConn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Parameters[1].Value = sAp1;
            cmd.Parameters[2].Value = sAp2;
            cmd.Parameters[3].Value = sNombre;

            StringBuilder strBuilder = new StringBuilder();

            try
            {
                SqlDataReader dr;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                int i = 0;
                strBuilder.Append("<table id='tblOpciones' name='tblOpciones'style='text-align:left; width:320px;' border='0' cellspacing='0' cellpadding='0'>");
                while (dr.Read())
                {
                    if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
                    else strBuilder.Append("<tr class=FB ");
                    i++;
                    strBuilder.Append("id='" + dr["T001_IDFICEPI"].ToString() + "' onClick='marcarEstaFila(this,false)' onDblClick='convocar2(this);marcarEstaFila(this,false);' style='cursor:pointer;height:16px'><td style='padding-left:5px'><label class=texto id='lbl" + dr["T001_IDFICEPI"].ToString() + "' style='width:315px;text-overflow:ellipsis;overflow:hidden'");

                    if (dr["DESCRIPCION"].ToString().Length > 80)
                    {
                        strBuilder.Append(" title='" + dr["DESCRIPCION"].ToString() + "'");
                    }

                    strBuilder.Append("><NOBR>" + dr["DESCRIPCION"] + "</NOBR></label></td></tr>");
                }
                strBuilder.Append("</table>");
                dr.Close();
                dr.Dispose();
            }
            catch
            {
                strBuilder.Length = 0;
                strBuilder.Append("Error");
            }
            //Response.Write(strTabla)*/
            return strBuilder.ToString();
        }

		public string obtenerPerfilesCR2I(string sInicial)
		{
			StringBuilder strBuilder = new StringBuilder();

			try
			{
				Perfil objPer = new Perfil();
                SqlDataReader dr = objPer.Obtener(sInicial);
		
				int i = 0;
				strBuilder.Append("<table id='tblOpciones' class='MA' name='tblOpciones' style='text-align:left; width:800px' border='0' cellspacing='0' cellpadding='0'>");
                strBuilder.Append("<colgroup><col style='width:350px;'/><col style='width:90px;'/><col style='width:90px;'/><col style='width:90px;'/><col style='width:90px;'/><col style='width:90px;'/></colgroup>");
				while (dr.Read()) 
				{
                    if (i == 0)
                    {
                        strBuilder.Append("<tr class=FA id='" + dr["CODIGO"].ToString() + "'");
                        strBuilder.Append(" onDblClick='mostrarDetalle(this.id)' style='height:16px'>");
                        strBuilder.Append("<td style='padding-left:5px;'>" + dr["DESCRIPCION"] + "</td>");
                        strBuilder.Append("<td>" + dr["T001_PERFILCR2I"] + "</td>");
                        strBuilder.Append("<td>" + dr["T001_RESSALA"] + "</td>");
                        strBuilder.Append("<td>" + dr["T001_RESVIDEO"] + "</td>");
                        strBuilder.Append("<td>" + dr["T001_RESWEBEX"] + "</td>");
                        strBuilder.Append("<td>" + dr["T001_RESWIFI"] + "</td>");
                        strBuilder.Append("</tr>");
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            strBuilder.Append("<tr class=FA id='" + dr["CODIGO"].ToString() + "'");
                            strBuilder.Append(" onDblClick='mostrarDetalle(this.id)' style='height:16px'>");
                            strBuilder.Append("<td style='padding-left:5px;'>" + dr["DESCRIPCION"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_PERFILCR2I"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESSALA"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESVIDEO"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESWEBEX"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESWIFI"] + "</td>");
                            strBuilder.Append("</tr>");
                        }
                        else //strBuilder.Append("<tr class=FB id='" + dr["CODIGO"].ToString() + "' onDblClick='mostrarDetalle(this.id)' style='height:16px'><td style='padding-left:5px;'>" + dr["DESCRIPCION"] + "</td><td>" + dr["T001_PERFILCR2I"] + "</td><td>" + dr["T001_RESSALA"] + "</td><td>" + dr["T001_RESVIDEO"] + "</td></tr>");
                        {
                            strBuilder.Append("<tr class=FB id='" + dr["CODIGO"].ToString() + "'");
                            strBuilder.Append(" onDblClick='mostrarDetalle(this.id)' style='height:16px'>");
                            strBuilder.Append("<td style='padding-left:5px;'>" + dr["DESCRIPCION"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_PERFILCR2I"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESSALA"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESVIDEO"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESWEBEX"] + "</td>");
                            strBuilder.Append("<td>" + dr["T001_RESWIFI"] + "</td>");
                            strBuilder.Append("</tr>");
                        }
                    }
					i++;
				}			
				strBuilder.Append("</table>");
                dr.Close();
                dr.Dispose();
            }
			catch
			{
				strBuilder.Length = 0;
				strBuilder.Append("Error");
			}
			//Response.Write(strTabla)*/
			return strBuilder.ToString();
		}

		public string actualizarPerfilesCR2I(string strCIP, string sCR2I, string sReunion, string sVideo, string sWebex, string sWifi)
		{
			string strReturn = "OK";

			try
			{
				Perfil objPer = new Perfil();
				objPer.sCIP = strCIP;
				objPer.sPerCR2I = sCR2I;
				objPer.sPerReunion = sReunion;
				objPer.sPerVideo = sVideo;
                objPer.sPerWebex = sWebex;
                objPer.sPerWifi = sWifi;
				int nResult = objPer.Actualizar();
			}
			catch
			{
				strReturn = "Error";
			}
			//Response.Write(strTabla)
			return strReturn;	

		}

        public string obtenerReservasWIFI(string sSoloActivas)
        {
            string sFecAux = "";

            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 850px; table-layout:fixed; HEIGHT: 17px;cursor: url(../../../images/imgManoAzul2.cur)' cellspacing='0' border='0'>");
                sb.Append("    <colgroup>");
                sb.Append("        <col style='width:295px;padding-left:3px;' />");
                sb.Append("        <col style='width:295px' />");
                sb.Append("        <col style='width:100px' />");
                sb.Append("        <col style='width:100px' />");
                sb.Append("        <col style='width:60px' />");
                sb.Append("    </colgroup>");

                sb.Append("<tbody>");

                string sColor = "";
                SqlDataReader dr = WIFI.CatalogoWifi((int)Session["CR2I_IDFICEPI"], (sSoloActivas == "1") ? true : false);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t085_idreserva"].ToString() + "' ");
                    sb.Append(" style=\"height:16px; cursor:url(../../../images/imgManoAzul2.cur),pointer\" onclick='msse(this)' ondblclick='mdwifi(this.id)'>");

                    sb.Append("<td><nobr class='NBR W290'>" + dr["Solicitante"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W290'>" + dr["t085_interesado"].ToString() + "</nobr></td>");

                    sFecAux = dr["t085_fechoraini"].ToString().Substring(0, 16);
                    if (sFecAux.Substring(15, 1) == ":")
                        sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                    sb.Append("<td>" + sFecAux + "</td>");

                    sFecAux = dr["t085_fechorafin"].ToString().Substring(0, 16);
                    if (sFecAux.Substring(15, 1) == ":")
                        sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                    sb.Append("<td>" + sFecAux + "</td>");

                    switch (dr["t085_estado"].ToString())
                    {
                        case "1": sColor = "Orange"; break;
                        case "2": sColor = "Green"; break;
                        case "3": sColor = "Gray"; break;
                        case "4": sColor = "Red"; break;
                    }
                    sb.Append("<td style='Color:" + sColor + "'>" + dr["des_estado"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener el catálogo de reservas wifi", ex);
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
	}
}
