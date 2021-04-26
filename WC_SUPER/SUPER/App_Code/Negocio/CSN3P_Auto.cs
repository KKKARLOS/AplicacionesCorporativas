using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CSN3P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T489_CSN3P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/10/2009 13:57:05	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CSN3P
	{
		#region Propiedades y Atributos

		private int _t489_idcsn3p;
		public int t489_idcsn3p
		{
			get {return _t489_idcsn3p;}
			set { _t489_idcsn3p = value ;}
		}

		private string _t489_denominacion;
		public string t489_denominacion
		{
			get {return _t489_denominacion;}
			set { _t489_denominacion = value ;}
		}

		private int _t393_idsupernodo3;
		public int t393_idsupernodo3
		{
			get {return _t393_idsupernodo3;}
			set { _t393_idsupernodo3 = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private bool _t489_activo;
		public bool t489_activo
		{
			get {return _t489_activo;}
			set { _t489_activo = value ;}
		}

		private byte _t489_orden;
		public byte t489_orden
		{
			get {return _t489_orden;}
			set { _t489_orden = value ;}
		}
		#endregion

		#region Constructor

		public CSN3P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T489_CSN3P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t489_denominacion , int t393_idsupernodo3 , int t314_idusuario_responsable , bool t489_activo , byte t489_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t489_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t489_denominacion;
			aParam[1] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[1].Value = t393_idsupernodo3;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t489_activo", SqlDbType.Bit, 1);
			aParam[3].Value = t489_activo;
			aParam[4] = new SqlParameter("@t489_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t489_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CSN3P_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CSN3P_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T489_CSN3P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t489_idcsn3p, string t489_denominacion, int t393_idsupernodo3, int t314_idusuario_responsable, bool t489_activo, byte t489_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[0].Value = t489_idcsn3p;
			aParam[1] = new SqlParameter("@t489_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t489_denominacion;
			aParam[2] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[2].Value = t393_idsupernodo3;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t489_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t489_activo;
			aParam[5] = new SqlParameter("@t489_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t489_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN3P_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN3P_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T489_CSN3P a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t489_idcsn3p)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[0].Value = t489_idcsn3p;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN3P_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN3P_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T489_CSN3P,
		/// y devuelve una instancia u objeto del tipo CSN3P
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CSN3P Select(SqlTransaction tr, int t489_idcsn3p) 
		{
			CSN3P o = new CSN3P();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[0].Value = t489_idcsn3p;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CSN3P_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CSN3P_S", aParam);

			if (dr.Read())
			{
				if (dr["t489_idcsn3p"] != DBNull.Value)
					o.t489_idcsn3p = int.Parse(dr["t489_idcsn3p"].ToString());
				if (dr["t489_denominacion"] != DBNull.Value)
					o.t489_denominacion = (string)dr["t489_denominacion"];
				if (dr["t393_idsupernodo3"] != DBNull.Value)
					o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t489_activo"] != DBNull.Value)
					o.t489_activo = (bool)dr["t489_activo"];
				if (dr["t489_orden"] != DBNull.Value)
					o.t489_orden = byte.Parse(dr["t489_orden"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CSN3P"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T489_CSN3P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t489_idcsn3p, string t489_denominacion, Nullable<int> t393_idsupernodo3, Nullable<int> t314_idusuario_responsable, Nullable<bool> t489_activo, Nullable<byte> t489_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[0].Value = t489_idcsn3p;
			aParam[1] = new SqlParameter("@t489_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t489_denominacion;
			aParam[2] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[2].Value = t393_idsupernodo3;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t489_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t489_activo;
			aParam[5] = new SqlParameter("@t489_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t489_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CSN3P_C", aParam);
		}

		#endregion
	}
}
