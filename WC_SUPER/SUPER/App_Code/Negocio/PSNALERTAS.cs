using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PSNALERTAS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T830_PSNALERTAS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	23/07/2012 12:36:46	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PSNALERTAS
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private byte _t820_idalerta;
		public byte t820_idalerta
		{
			get {return _t820_idalerta;}
			set { _t820_idalerta = value ;}
		}

		private bool _t830_habilitada;
		public bool t830_habilitada
		{
			get {return _t830_habilitada;}
			set { _t830_habilitada = value ;}
		}

		private int? _t830_inistandby;
		public int? t830_inistandby
		{
			get {return _t830_inistandby;}
			set { _t830_inistandby = value ;}
		}

		private int? _t830_finstandby;
		public int? t830_finstandby
		{
			get {return _t830_finstandby;}
			set { _t830_finstandby = value ;}
		}

		private string _t830_txtseguimiento;
		public string t830_txtseguimiento
		{
			get {return _t830_txtseguimiento;}
			set { _t830_txtseguimiento = value ;}
		}
		#endregion

		#region Constructor

		public PSNALERTAS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static DataSet ObtenerCatalogoByDialogo(int t831_iddialogoalerta, int t314_idusuario)
        {
            return SUPER.Capa_Datos.PSNALERTAS.CatalogoByDialogo(t831_iddialogoalerta, t314_idusuario);
        }

        public static void EstablecerAlertaEstructura(int nNivel, int nCodigo, byte nAlerta, bool bHabilitada)
        {
            SUPER.Capa_Datos.PSNALERTAS.EstablecerAlertaEstructura(null, nNivel, nCodigo, nAlerta, bHabilitada);
        }

        public static void TrasladarAlertaEstructura(byte nOpcion, byte nNivel, int nCodigo)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                SUPER.Capa_Datos.PSNALERTAS.TrasladarAlertaEstructura(tr, nOpcion, nNivel, nCodigo);
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al trasladar las alertas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    throw (new Exception(sResul));
                }
            }
        }

        public static void EstablecerAlertaDetalleProyecto(string sDatosAlertas)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                string[] aDatosAlertas = Regex.Split(sDatosAlertas, "{sepreg}");
                foreach (string sDatos in aDatosAlertas)
                {
                    if (sDatos == "") continue;
                    string[] aValores = Regex.Split(sDatos, "{sep}");
                    ///aValores[0] = sPSN
                    ///aValores[1] = id alerta 
                    ///aValores[2] = activado
                    ///aValores[3] = inicio standby
                    ///aValores[4] = fin standby
                    ///aValores[5] = seguimiento
                    SUPER.Capa_Datos.PSNALERTAS.EstablecerAlertaDetalleProyecto(tr, int.Parse(aValores[0]),
                        byte.Parse(aValores[1]),
                        (aValores[2] == "1") ? true : false,
                        (aValores[3] == "") ? null : (int?)int.Parse(aValores[3]),
                        (aValores[4] == "") ? null : (int?)int.Parse(aValores[4]),
                        Utilidades.unescape(aValores[5])
                        );
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar las alertas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    throw (new Exception(sResul));
                }
            }

            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EstablecerAlertaProyectosubnodo(string sDatosAlertas)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                string[] aDatosAlertas = Regex.Split(sDatosAlertas, "{sep}");
                foreach (string sDatos in aDatosAlertas)
                {
                    if (sDatos == "") continue;
                    SUPER.Capa_Datos.PSNALERTAS.EstablecerAlertaProyectosubnodo(tr, sDatos);
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar las alertas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    throw (new Exception(sResul));
                }
            }
        }

        public static string Catalogo(int t305_idproyectosubnodo, bool bLectura)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' style='width:670px;' cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
			        <col style='width:30px;' />
	                <col style='width:320px;' />
	                <col style='width:60px;' />
	                <col style='width:100px;' />
	                <col style='width:120px;' />
                    <col style='width:40px;' />
			    </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.PSNALERTAS.Catalogo(null, t305_idproyectosubnodo);

            while (dr.Read())
            {
                sb.Append("<tr bd='' id='" + dr["t820_idalerta"].ToString() + "' ");
                sb.Append("a="+ (((bool)dr["t830_habilitada"])?"1":"0") +" ");
                sb.Append("inistandby='" + dr["t830_inistandby"].ToString() + "' ");
                sb.Append("finstandby='" + dr["t830_finstandby"].ToString() + "' ");
                sb.Append("seg=\"" + Utilidades.escape(dr["t830_txtseguimiento"].ToString()) + "\" ");
                sb.Append(" style='height:20px;text-align:center;'>");
                sb.Append("<td style='text-align:right;padding-right:5px'>" + dr["t820_idalerta"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;text-align:left;'><nobr class='NBR W310' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:left;'></td>");
                sb.Append("<td style='text-align:left;'></td>");
                sb.Append("<td>");
                if (!bLectura)
                {
                    sb.Append("<input type='checkbox' class='check' " + ((dr["t830_txtseguimiento"].ToString() != "") ? "checked" : "") + " onclick='setSeguimiento(event)' style='cursor:pointer;' />");
                    sb.Append("<img src='../../../../images/imgSeguimiento.png' onclick='ModificarSeguimiento(this)' style='cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px;visibility:" + ((dr["t830_txtseguimiento"].ToString() != "") ? "visible" : "hidden") + "; width:16px; height:16px;' />");
                }
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static string ObtenerAlertasMiGestion(
                    Nullable<int> t305_idproyectosubnodo,
                    string t301_estado,
                    Nullable<int> t303_idnodo,
                    Nullable<int> t302_idcliente,
                    Nullable<int> t001_idficepi_interlocutor,
                    Nullable<bool> t830_habilitada,
                    Nullable<byte> t820_idalerta,
                    Nullable<int> t314_idusuario_gestor,
                    bool bStandBy,
                    bool bSeguimiento,
                    Nullable<int> t314_idusuario_responsable,
                    Nullable<byte> t821_idgrupoalerta
            )
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append(@"<table id='tblDatos' style='width: 960px; cursor:pointer;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
	                        <col style='width:20px;' />
	                        <col style='width:20px;' />
	                        <col style='width:60px;' />
	                        <col style='width:260px;' />
	                        <col style='width:250px;' />
	                        <col style='width:50px;' />
	                        <col style='width:100px;' />
	                        <col style='width:120px;' />
	                        <col style='width:50px;' />
	                        <col style='width:30px;' />
                        </colgroup>");

            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.PSNALERTAS.ObtenerAlertasMiGestion(null, t305_idproyectosubnodo,
                    t301_estado,
                    t303_idnodo,
                    t302_idcliente,
                    t001_idficepi_interlocutor,
                    t830_habilitada,
                    t820_idalerta,
                    t314_idusuario_gestor,
                    bStandBy,
                    bSeguimiento,
                    t314_idusuario_responsable,
                    t821_idgrupoalerta
                    );
            bool bExcede = false;
            int i = 0;
            while (dr.Read())
            {
                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
                sb.Append("<tr bd='' ");
                sb.Append("id='" + i.ToString() + "' ");
                //Datos de proyectos
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");

                sb.Append("idAlerta='" + dr["t820_idalerta"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                //string sTooltip = "<label style='width:70px;'>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString();
               
                sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
                //Datos de alertas 
                sb.Append("habilitada=" + (((bool)dr["t830_habilitada"]) ? "1" : "0") + " ");
                sb.Append("inistandby='" + dr["t830_inistandby"].ToString() + "' ");
                sb.Append("finstandby='" + dr["t830_finstandby"].ToString() + "' ");
                sb.Append("seg=\"" + Utilidades.escape(dr["t830_txtseguimiento"].ToString()) + "\" ");
                sb.Append("haydialog=" + (((bool)dr["bHayDialogos"]) ? "1" : "0") + " ");

                sb.Append(">");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                //sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td onmouseout='hideTTE();' onmouseover=\"showTTE(this.parentNode.getAttribute('tooltip'))\">" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t820_denominacion"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:left;'></td>");
                sb.Append("<td style='text-align:left;'></td>");
                sb.Append("<td>");
                //if (dr["t301_estado"].ToString() == "A")
                //{
                //    sb.Append("<input type='checkbox' class='check' " + ((dr["t830_txtseguimiento"].ToString() != "") ? "checked" : "") + " onclick='setSeguimiento()' style='cursor:pointer;' />");
                //    sb.Append("<img src='../../../../images/imgSeguimiento.png' onclick='ModificarSeguimiento(this)' style='cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px;visibility:" + ((dr["t830_txtseguimiento"].ToString() != "") ? "visible" : "hidden") + "; width:16px; height:16px;' />");
                //}
                sb.Append("</td>");
                sb.Append("<td></td>");

                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            if (bExcede) return "EXCEDE";
            else return sb.ToString();
        }
        /// <summary>
        /// Obtiene el motivo del último dialogo cerrado, con motivo de cierre asociados al proyecto y tipo de alerta que se pasa por parámetro
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <param name="t820_idalerta"></param>
        /// <returns></returns>
        public static string MotivoCierreDefecto(SqlTransaction tr, int t305_idproyectosubnodo, int t820_idalerta)
        {
            string sIdMotivo = "";

            SqlDataReader dr = SUPER.Capa_Datos.PSNALERTAS.MotivoCierreDefecto(null, t305_idproyectosubnodo, t820_idalerta);
            if (dr.Read())
                sIdMotivo = dr["t840_idmotivo"].ToString();

            return sIdMotivo;
        }
        #endregion
    }
}
