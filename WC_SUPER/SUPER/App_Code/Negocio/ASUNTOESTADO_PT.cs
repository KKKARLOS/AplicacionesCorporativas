using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTOESTADO_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T416_ASUNTOESTADO_PT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 17:24:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTOESTADO_PT
	{
		#region Propiedades y Atributos

		private int _t409_idasunto;
		public int t409_idasunto
		{
			get {return _t409_idasunto;}
			set { _t409_idasunto = value ;}
		}

		private byte _t416_codestado;
		public byte t416_codestado
		{
			get {return _t416_codestado;}
			set { _t416_codestado = value ;}
		}

		private DateTime _t416_fecha;
		public DateTime t416_fecha
		{
			get {return _t416_fecha;}
			set { _t416_fecha = value ;}
		}

		private int _t416_idautor;
		public int t416_idautor
		{
			get {return _t416_idautor;}
			set { _t416_idautor = value ;}
		}

		private int _t416_idestado;
		public int t416_idestado
		{
			get {return _t416_idestado;}
			set { _t416_idestado = value ;}
		}
		#endregion

		#region Constructores

		public ASUNTOESTADO_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T416_ASUNTOESTADO_PT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t409_idasunto, byte t416_codestado, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;
			aParam[1] = new SqlParameter("@t416_codestado", SqlDbType.TinyInt, 1);
			aParam[1].Value = t416_codestado;
			aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTOESTADO_PT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTOESTADO_PT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T416_ASUNTOESTADO_PT en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt409_idasunto(SqlTransaction tr, int t409_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ASUNTOESTADO_PT_SByt409_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTOESTADO_PT_SByt409_idasunto", aParam);
		}

		#endregion
	}
}
