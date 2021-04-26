using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : USUSINAVISOVTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T856_NOAVISOVENCIMIENTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	15/02/2010 16:17:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUSINAVISOVTO
	{
		#region Propiedades y Atributos

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		#endregion

		#region Constructor

        public USUSINAVISOVTO()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static string obtenerUsuariosSinAvisoVto()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                SqlDataReader dr = SUPER.Capa_Datos.USUSINAVISOVTO.Catalogo();
                sb.Append("<TABLE id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
                sb.Append("<colgroup><col style='width:11px;' /><col style='width: 20px' /><col style='width: 419px;' /></colgroup>");
                sb.Append("<tbody>");
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' style='height:20px;' tipo='" + dr["tipo"].ToString() + "'");
                    sb.Append(" onmousedown='DD(event)' onclick='mm(event)'>");
                    sb.Append("<td style='padding-left:1px'><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");
                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPM.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEM.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFM.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:left;' ><nobr class='NBR' style='width:400px'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb.Append("</tr>" + (char)10);
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString(); ;
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales sin aviso de vencimiento.", ex);
            }
        }
        
        public static string Grabar(string strProfesionales)
        {
            string sResul = "", sElementosInsertados = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion
            try
            {
                #region Datos Profesionales
                if (strProfesionales != "")//No se ha modificado nada de la pestaña de Figuras
                {
                    string[] aProfesionales = Regex.Split(strProfesionales, "///");
                    foreach (string oProfesional in aProfesionales)
                    {
                        if (oProfesional == "") continue;
                        string[] aValores = Regex.Split(oProfesional, "##");
                        ///aValores[0] = bd
                        ///aValores[1] = idFicepi

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.Capa_Datos.USUSINAVISOVTO.Insert(tr, int.Parse(aValores[1]));
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;

                            case "D":
                                SUPER.Capa_Datos.USUSINAVISOVTO.Delete(tr, int.Parse(aValores[1]));
                                break;
                        }
                    }
                }

                #endregion

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del profesional", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = "OK@#@" + sElementosInsertados;
            return sResul;
        }

        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = USUARIO.GetProfAdm(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false, null);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH:450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;text-align:left' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");

                var sTooltip = "<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
                sTooltip += "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipProf=\"" + sTooltip + "\" ");
                        
                
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W430' style='noWrap:true;' ondblclick='addItem(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W430' style='noWrap:true;'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();

            return sResul;
        }

        #endregion
	}
}
