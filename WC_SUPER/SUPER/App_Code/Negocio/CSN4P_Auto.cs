using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CSN4P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T491_CSN4P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/10/2009 13:57:05	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CSN4P
	{
		#region Propiedades y Atributos

		private int _t491_idcsn4p;
		public int t491_idcsn4p
		{
			get {return _t491_idcsn4p;}
			set { _t491_idcsn4p = value ;}
		}

		private string _t491_denominacion;
		public string t491_denominacion
		{
			get {return _t491_denominacion;}
			set { _t491_denominacion = value ;}
		}

		private int _t394_idsupernodo4;
		public int t394_idsupernodo4
		{
			get {return _t394_idsupernodo4;}
			set { _t394_idsupernodo4 = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private bool _t491_activo;
		public bool t491_activo
		{
			get {return _t491_activo;}
			set { _t491_activo = value ;}
		}

		private byte _t491_orden;
		public byte t491_orden
		{
			get {return _t491_orden;}
			set { _t491_orden = value ;}
		}
		#endregion

		#region Constructor

		public CSN4P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T491_CSN4P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t491_denominacion , int t394_idsupernodo4 , int t314_idusuario_responsable , bool t491_activo , byte t491_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t491_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t491_denominacion;
			aParam[1] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[1].Value = t394_idsupernodo4;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t491_activo", SqlDbType.Bit, 1);
			aParam[3].Value = t491_activo;
			aParam[4] = new SqlParameter("@t491_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t491_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CSN4P_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CSN4P_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T491_CSN4P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t491_idcsn4p, string t491_denominacion, int t394_idsupernodo4, int t314_idusuario_responsable, bool t491_activo, byte t491_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[0].Value = t491_idcsn4p;
			aParam[1] = new SqlParameter("@t491_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t491_denominacion;
			aParam[2] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[2].Value = t394_idsupernodo4;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t491_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t491_activo;
			aParam[5] = new SqlParameter("@t491_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t491_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN4P_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN4P_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T491_CSN4P a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t491_idcsn4p)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[0].Value = t491_idcsn4p;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN4P_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN4P_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T491_CSN4P,
		/// y devuelve una instancia u objeto del tipo CSN4P
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CSN4P Select(SqlTransaction tr, int t491_idcsn4p) 
		{
			CSN4P o = new CSN4P();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[0].Value = t491_idcsn4p;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CSN4P_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CSN4P_S", aParam);

			if (dr.Read())
			{
				if (dr["t491_idcsn4p"] != DBNull.Value)
					o.t491_idcsn4p = int.Parse(dr["t491_idcsn4p"].ToString());
				if (dr["t491_denominacion"] != DBNull.Value)
					o.t491_denominacion = (string)dr["t491_denominacion"];
				if (dr["t394_idsupernodo4"] != DBNull.Value)
					o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t491_activo"] != DBNull.Value)
					o.t491_activo = (bool)dr["t491_activo"];
				if (dr["t491_orden"] != DBNull.Value)
					o.t491_orden = byte.Parse(dr["t491_orden"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CSN4P"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T491_CSN4P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t491_idcsn4p, string t491_denominacion, Nullable<int> t394_idsupernodo4, Nullable<int> t314_idusuario_responsable, Nullable<bool> t491_activo, Nullable<byte> t491_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[0].Value = t491_idcsn4p;
			aParam[1] = new SqlParameter("@t491_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t491_denominacion;
			aParam[2] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[2].Value = t394_idsupernodo4;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t491_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t491_activo;
			aParam[5] = new SqlParameter("@t491_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t491_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CSN4P_C", aParam);
		}

		#endregion
	}
}
