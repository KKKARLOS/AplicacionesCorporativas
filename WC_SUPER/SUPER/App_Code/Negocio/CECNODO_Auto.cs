using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CECNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T381_CECNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	21/08/2009 13:13:00	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CECNODO
	{
		#region Propiedades y Atributos

		private int _t345_idcec;
		public int t345_idcec
		{
			get {return _t345_idcec;}
			set { _t345_idcec = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private bool _t381_obligatorio;
		public bool t381_obligatorio
		{
			get {return _t381_obligatorio;}
			set { _t381_obligatorio = value ;}
		}
		#endregion

		#region Constructor

		public CECNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T381_CECNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	21/08/2009 13:13:00
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t345_idcec , int t303_idnodo , bool t381_obligatorio)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
			aParam[0].Value = t345_idcec;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t381_obligatorio", SqlDbType.Bit, 1);
			aParam[2].Value = t381_obligatorio;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CECNODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECNODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T381_CECNODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	21/08/2009 13:13:00
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t345_idcec, int t303_idnodo, bool t381_obligatorio)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
			aParam[0].Value = t345_idcec;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t381_obligatorio", SqlDbType.Bit, 1);
			aParam[2].Value = t381_obligatorio;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CECNODO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECNODO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T381_CECNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	21/08/2009 13:13:00
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t345_idcec, int t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
			aParam[0].Value = t345_idcec;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CECNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECNODO_D", aParam);
		}

		#endregion
	}
}
