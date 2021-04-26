using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using GEMO.DAL;

namespace GEMO.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : FIGURAS_PROFESI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T707_FIGURAS_PROFESI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/04/2011 16:27:27	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAS_PROFESI
	{
		#region Propiedades y Atributos

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private string _t707_figura;
		public string t707_figura
		{
			get {return _t707_figura;}
			set { _t707_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAS_PROFESI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
        public static string ObtenerIntegrantes()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbArray = new StringBuilder();
            sbArray.Append(" aFigIni = new Array();");
            int i = 0;

            SqlDataReader dr = GEMO.DAL.FIGURAS_PROFESI.Catalogo();

            sb.Append("<table id='tblFiguras2' class='MM' style='width:450px; background-image:url(../../../../Images/imgFT20.gif)' mantenimiento='1' >");
            sb.Append("<colgroup><col style='width:15px' /><col style='width:20px' /><col style='width:295px;' /><col style='width:100px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            int nIdFicepi = 0;
            string sCorreo = "";
            string sEnvios = "";
            bool bHayFilas = false;
            string sColor = "black";
            int iControl = 0;

            while (dr.Read())
            {
                bHayFilas = true;
                sbArray.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t001_idficepi"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");

                i++;
                sColor = "black";

                if ((int)dr["t001_idficepi"] != nIdFicepi)
                {
                    if (nIdFicepi != 0)
                    {
                        sb.Append("</ul></div></td>");

                        sEnvios = "<td><input type=checkbox id='chkCorreo' ";
                        if (iControl == 0) sEnvios = sEnvios + "disabled ";
                        if (sCorreo=="S") sEnvios = sEnvios + "checked ";
                        sEnvios = sEnvios + "class='check' runat=server onclick=mfa(this.parentElement.parentElement,'U') />";
                        sb.Append(sEnvios+ "</td>");
                       
                        sb.Append("</tr>");
                    }

                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' style='height:20px;color:" + sColor + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "'>");
                    //sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W290' style='noWrap:true;'>" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentElement.parentElement)'
                    //Figuras
                    sb.Append("<td><div style='height:20px;'><ul id='box-" + dr["t001_idficepi"].ToString() + "'>");
                    iControl = 0;
                    switch (dr["figura"].ToString().Trim())
                    {
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgControlador.gif' title='Controlador' /></li>"); iControl = 1; break;
                        case "F": sb.Append("<li id='F' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgFacturador.gif' title='Facturador' /></li>");  break;
                        //case "U": sb.Append("<li id='U' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInteresado.gif' title='Interesado' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgMedios.gif' title='Medios' /></li>"); break;
                        case "A": sb.Append("<li id='A' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgAdministrador.gif' title='Administrador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nIdFicepi = (int)dr["t001_idficepi"];
                    sCorreo = dr["t001_facturacion"].ToString();

                }
                else
                {
                    switch (dr["figura"].ToString().Trim())
                    {
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgControlador.gif' title='Controlador' /></li>"); iControl = 1; break;
                        case "F": sb.Append("<li id='F' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgFacturador.gif' title='Facturador' /></li>"); break;
                        //case "U": sb.Append("<li id='U' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInteresado.gif' title='Interesado' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgMedios.gif' title='Medios' /></li>"); break;
                        case "A": sb.Append("<li id='A' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgAdministrador.gif' title='Administrador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");

                sEnvios = "<td><input type=checkbox id='chkCorreo' class='check' runat=server ";
                if (iControl == 0) sEnvios = sEnvios + "disabled ";
                if (sCorreo == "S") sEnvios = sEnvios + "checked ";
                sEnvios = sEnvios + " onclick=mfa(this.parentElement.parentElement,'U') />";
                sb.Append(sEnvios + "</td>");

                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString() + "@#@" + sbArray.ToString();
        }

        public static void Grabar(string strFiguras)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                if (strFiguras != "") //No se ha modificado nada de la pestaña de Figuras
                {
                    string[] aUsuarios = Regex.Split(strFiguras, "///");
                    foreach (string oUsuario in aUsuarios)
                    {
                        if (oUsuario == "") continue;
                        string[] aFig = Regex.Split(oUsuario, "##");

                        ///aFig[0] = bd
                        ///aFig[1] = t001_idficepi
                        ///aFig[2] = Figuras
                        ///aFig[3] = Correo controladores
                        ///
                        if (aFig[0] == "D")
                            GEMO.DAL.FIGURAS_PROFESI.DeleteALL(tr, int.Parse(aFig[1]));
                        else
                        {
                            string[] aFiguras = Regex.Split(aFig[2], ",");
                            foreach (string oFigura in aFiguras)
                            {
                                if (oFigura == "") continue;
                                string[] aFig2 = Regex.Split(oFigura, "@");
                                ///aFig2[0] = bd
                                ///aFig2[1] = Figura
                                ///
                                if (aFig2[0] == "D")
                                    GEMO.DAL.FIGURAS_PROFESI.Delete(tr, int.Parse(aFig[1]), aFig2[1]);
                                else
                                    GEMO.DAL.FIGURAS_PROFESI.Insert(tr, int.Parse(aFig[1]), aFig2[1], aFig[3] );
                            }
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la lista de figuras.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul!="")
                    throw (new Exception(sResul));
            }
        }

		#endregion
	}
}
