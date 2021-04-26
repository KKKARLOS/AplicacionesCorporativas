using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : CR2I
	/// Class	 : WEBEX_FICEPI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T140_WEBEX_FICEPI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/05/2009 13:41:08	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class WEBEX_FICEPI
	{
		#region Propiedades y Atributos

		private int _T139_IDRESERVA;
		public int T139_IDRESERVA
		{
			get {return _T139_IDRESERVA;}
			set { _T139_IDRESERVA = value ;}
		}

		private int _T001_IDFICEPI;
		public int T001_IDFICEPI
		{
			get {return _T001_IDFICEPI;}
			set { _T001_IDFICEPI = value ;}
		}

		private string _T140_FIGURA;
		public string T140_FIGURA
		{
			get {return _T140_FIGURA;}
			set { _T140_FIGURA = value ;}
		}
		#endregion

		#region Constructor

		public WEBEX_FICEPI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T140_WEBEX_FICEPI.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	11/05/2009 13:41:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insertar(SqlTransaction tr, int T139_IDRESERVA , int T001_IDFICEPI , string T140_FIGURA)
		{
			if (tr == null)
				 SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, "CR2_WEBEX_FICEPI_I", T139_IDRESERVA , T001_IDFICEPI , T140_FIGURA);
			else
                 SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_WEBEX_FICEPI_I", T139_IDRESERVA, T001_IDFICEPI, T140_FIGURA);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T140_WEBEX_FICEPI en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	11/05/2009 13:41:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectBy_IDRESERVA(SqlTransaction tr, int T139_IDRESERVA, string sAccion) 
		{
			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "CR2_WEBEX_FICEPI_SBy_IDRESERVA", sAccion, T139_IDRESERVA);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_WEBEX_FICEPI_SBy_IDRESERVA", sAccion, T139_IDRESERVA);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla T140_WEBEX_FICEPI en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [sqladmin]	11/05/2009 13:41:08
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void DeleteByT139_IDRESERVA(SqlTransaction tr, int T139_IDRESERVA)
		{
			if (tr == null)
                SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, "CR2_WEBEX_FICEPI_DByT139_IDRESERVA", T139_IDRESERVA);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_WEBEX_FICEPI_DByT139_IDRESERVA", T139_IDRESERVA);
		}

        public static bool AsisteAWebex(int nReserva, int nIdFicepi)
        {
            object oResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                "CR2_ASISTEWEBEX", nReserva, nIdFicepi);

            return (Convert.ToInt32(oResul) > 0)? true:false;
        }

		#endregion
	}
}
