using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : EXPPROFACS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T816_EXPPROFACS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	30/07/2012 14:15:52	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXPPROFACS
	{
		#region Propiedades y Atributos

		private int _t808_idexpprof;
		public int t808_idexpprof
		{
			get {return _t808_idexpprof;}
			set { _t808_idexpprof = value ;}
		}

		private int _t809_idaconsect;
		public int t809_idaconsect
		{
			get {return _t809_idaconsect;}
			set { _t809_idaconsect = value ;}
		}
		#endregion

		#region Constructor

		public EXPPROFACS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T816_EXPPROFACS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 14:15:52
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t808_idexpprof , int t809_idaconsect)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t809_idaconsect", SqlDbType.Int, 4, t809_idaconsect);

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFACS_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFACS_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T816_EXPPROFACS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 14:15:52
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t808_idexpprof, int t809_idaconsect)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t809_idaconsect", SqlDbType.Int, 4, t809_idaconsect);

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFACS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFACS_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T816_EXPPROFACS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 14:15:52
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo(Nullable<int> t808_idexpprof, Nullable<int> t809_idaconsect, byte nOrden, byte nAscDesc)
        //{
        //    SqlParameter[] aParam = new SqlParameter[4];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
        //    aParam[i++] = ParametroSql.add("@t809_idaconsect", SqlDbType.Int, 4, t809_idaconsect);

        //    aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
        //    aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFACS_C", aParam);
        //}

		#endregion
	}
}
