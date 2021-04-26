using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : MODOFACTSN2
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T324_MODOFACTSN2
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/03/2010 16:29:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class MODOFACTSN2
	{
		#region Propiedades y Atributos

		private int _t324_idmodofact;
		public int t324_idmodofact
		{
			get {return _t324_idmodofact;}
			set { _t324_idmodofact = value ;}
		}

		private string _t324_denominacion;
		public string t324_denominacion
		{
			get {return _t324_denominacion;}
			set { _t324_denominacion = value ;}
		}

		private bool _t324_activo;
		public bool t324_activo
		{
			get {return _t324_activo;}
			set { _t324_activo = value ;}
		}

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}
		#endregion

		#region Constructor

		public MODOFACTSN2() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T324_MODOFACTSN2.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/03/2010 16:29:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t324_denominacion , bool t324_activo , int t392_idsupernodo2)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t324_denominacion", SqlDbType.Text, 20);
			aParam[0].Value = t324_denominacion;
			aParam[1] = new SqlParameter("@t324_activo", SqlDbType.Bit, 1);
			aParam[1].Value = t324_activo;
			aParam[2] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[2].Value = t392_idsupernodo2;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_MODOFACTSN2_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_MODOFACTSN2_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T324_MODOFACTSN2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/03/2010 16:29:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t324_idmodofact, string t324_denominacion, bool t324_activo, int t392_idsupernodo2)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
			aParam[0].Value = t324_idmodofact;
			aParam[1] = new SqlParameter("@t324_denominacion", SqlDbType.Text, 20);
			aParam[1].Value = t324_denominacion;
			aParam[2] = new SqlParameter("@t324_activo", SqlDbType.Bit, 1);
			aParam[2].Value = t324_activo;
			aParam[3] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[3].Value = t392_idsupernodo2;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_MODOFACTSN2_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MODOFACTSN2_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T324_MODOFACTSN2 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/03/2010 16:29:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t324_idmodofact)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
			aParam[0].Value = t324_idmodofact;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_MODOFACTSN2_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MODOFACTSN2_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T324_MODOFACTSN2,
		/// y devuelve una instancia u objeto del tipo MODOFACTSN2
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/03/2010 16:29:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static MODOFACTSN2 Select(SqlTransaction tr, int t324_idmodofact) 
		{
			MODOFACTSN2 o = new MODOFACTSN2();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
			aParam[0].Value = t324_idmodofact;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_MODOFACTSN2_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MODOFACTSN2_S", aParam);

			if (dr.Read())
			{
				if (dr["t324_idmodofact"] != DBNull.Value)
					o.t324_idmodofact = int.Parse(dr["t324_idmodofact"].ToString());
				if (dr["t324_denominacion"] != DBNull.Value)
					o.t324_denominacion = (string)dr["t324_denominacion"];
				if (dr["t324_activo"] != DBNull.Value)
					o.t324_activo = (bool)dr["t324_activo"];
				if (dr["t392_idsupernodo2"] != DBNull.Value)
					o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de MODOFACTSN2"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T324_MODOFACTSN2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/03/2010 16:29:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t324_idmodofact, string t324_denominacion, Nullable<bool> t324_activo, Nullable<int> t392_idsupernodo2, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
			aParam[0].Value = t324_idmodofact;
			aParam[1] = new SqlParameter("@t324_denominacion", SqlDbType.Text, 20);
			aParam[1].Value = t324_denominacion;
			aParam[2] = new SqlParameter("@t324_activo", SqlDbType.Bit, 1);
			aParam[2].Value = t324_activo;
			aParam[3] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[3].Value = t392_idsupernodo2;

			aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[4].Value = nOrden;
			aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[5].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_MODOFACTSN2_C", aParam);
		}

		#endregion
	}
}
