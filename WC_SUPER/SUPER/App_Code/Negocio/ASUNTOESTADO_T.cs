using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTOESTADO_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T606_ASUNTOESTADO_T
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 17:24:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTOESTADO_T
	{
		#region Propiedades y Atributos

		private int _t600_idasunto;
		public int t600_idasunto
		{
			get {return _t600_idasunto;}
			set { _t600_idasunto = value ;}
		}

		private byte _t606_codestado;
		public byte t606_codestado
		{
			get {return _t606_codestado;}
			set { _t606_codestado = value ;}
		}

		private DateTime _t606_fecha;
		public DateTime t606_fecha
		{
			get {return _t606_fecha;}
			set { _t606_fecha = value ;}
		}

		private int _t606_idautor;
		public int t606_idautor
		{
			get {return _t606_idautor;}
			set { _t606_idautor = value ;}
		}

		private int _t606_idestado;
		public int t606_idestado
		{
			get {return _t606_idestado;}
			set { _t606_idestado = value ;}
		}
		#endregion

		#region Constructores

		public ASUNTOESTADO_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T606_ASUNTOESTADO_T.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t600_idasunto, byte t606_codestado, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;
			aParam[1] = new SqlParameter("@t606_codestado", SqlDbType.TinyInt, 1);
			aParam[1].Value = t606_codestado;
			aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTOESTADO_T_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTOESTADO_T_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T606_ASUNTOESTADO_T en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt600_idasunto(SqlTransaction tr, int t600_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ASUNTOESTADO_T_SByt600_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTOESTADO_T_SByt600_idasunto", aParam);
		}

		#endregion
	}
}
