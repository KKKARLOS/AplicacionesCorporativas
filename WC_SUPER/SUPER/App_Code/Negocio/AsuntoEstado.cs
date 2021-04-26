using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ASUNTOESTADO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t385_ASUNTOESTADO
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	16/11/2007 17:24:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTOESTADO
	{

		#region Propiedades y Atributos

		private int _t385_idasunto;
		public int t385_idasunto
		{
			get {return _t385_idasunto;}
			set { _t385_idasunto = value ;}
		}

		private byte _t385_codestado;
		public byte t385_codestado
		{
			get {return _t385_codestado;}
			set { _t385_codestado = value ;}
		}

		private DateTime _t385_fecha;
		public DateTime t385_fecha
		{
			get {return _t385_fecha;}
			set { _t385_fecha = value ;}
		}

		private int _t385_idautor;
		public int t385_idautor
		{
			get {return _t385_idautor;}
			set { _t385_idautor = value ;}
		}

		private int _t385_idestado;
		public int t385_idestado
		{
			get {return _t385_idestado;}
			set { _t385_idestado = value ;}
		}
		#endregion

		#region Constructores

		public ASUNTOESTADO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t385_ASUNTOESTADO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t382_idasunto , byte t385_codestado , int t385_idautor)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;
			aParam[1] = new SqlParameter("@t385_codestado", SqlDbType.TinyInt, 1);
			aParam[1].Value = t385_codestado;
			aParam[2] = new SqlParameter("@t385_idautor", SqlDbType.Int, 4);
			aParam[2].Value = t385_idautor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTOESTADO_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTOESTADO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t385_ASUNTOESTADO en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 17:24:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt382_idasunto(SqlTransaction tr, int t382_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ASUNTOESTADO_SByt382_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTOESTADO_SByt382_idasunto", aParam);
		}

		#endregion
	}
}
