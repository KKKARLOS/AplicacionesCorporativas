using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CSN1P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T485_CSN1P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/10/2009 13:57:05	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CSN1P
	{
		#region Propiedades y Atributos

		private int _t485_idcsn1p;
		public int t485_idcsn1p
		{
			get {return _t485_idcsn1p;}
			set { _t485_idcsn1p = value ;}
		}

		private string _t485_denominacion;
		public string t485_denominacion
		{
			get {return _t485_denominacion;}
			set { _t485_denominacion = value ;}
		}

		private int _t391_idsupernodo1;
		public int t391_idsupernodo1
		{
			get {return _t391_idsupernodo1;}
			set { _t391_idsupernodo1 = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private bool _t485_activo;
		public bool t485_activo
		{
			get {return _t485_activo;}
			set { _t485_activo = value ;}
		}

		private byte _t485_orden;
		public byte t485_orden
		{
			get {return _t485_orden;}
			set { _t485_orden = value ;}
		}
		#endregion

		#region Constructor

		public CSN1P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T485_CSN1P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t485_denominacion , int t391_idsupernodo1 , int t314_idusuario_responsable , bool t485_activo , byte t485_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t485_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t485_denominacion;
			aParam[1] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[1].Value = t391_idsupernodo1;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t485_activo", SqlDbType.Bit, 1);
			aParam[3].Value = t485_activo;
			aParam[4] = new SqlParameter("@t485_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t485_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CSN1P_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CSN1P_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T485_CSN1P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t485_idcsn1p, string t485_denominacion, int t391_idsupernodo1, int t314_idusuario_responsable, bool t485_activo, byte t485_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[0].Value = t485_idcsn1p;
			aParam[1] = new SqlParameter("@t485_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t485_denominacion;
			aParam[2] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[2].Value = t391_idsupernodo1;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t485_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t485_activo;
			aParam[5] = new SqlParameter("@t485_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t485_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN1P_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN1P_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T485_CSN1P a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t485_idcsn1p)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[0].Value = t485_idcsn1p;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN1P_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN1P_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T485_CSN1P,
		/// y devuelve una instancia u objeto del tipo CSN1P
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CSN1P Select(SqlTransaction tr, int t485_idcsn1p) 
		{
			CSN1P o = new CSN1P();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[0].Value = t485_idcsn1p;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CSN1P_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CSN1P_S", aParam);

			if (dr.Read())
			{
				if (dr["t485_idcsn1p"] != DBNull.Value)
					o.t485_idcsn1p = int.Parse(dr["t485_idcsn1p"].ToString());
				if (dr["t485_denominacion"] != DBNull.Value)
					o.t485_denominacion = (string)dr["t485_denominacion"];
				if (dr["t391_idsupernodo1"] != DBNull.Value)
					o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t485_activo"] != DBNull.Value)
					o.t485_activo = (bool)dr["t485_activo"];
				if (dr["t485_orden"] != DBNull.Value)
					o.t485_orden = byte.Parse(dr["t485_orden"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CSN1P"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T485_CSN1P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t485_idcsn1p, string t485_denominacion, Nullable<int> t391_idsupernodo1, Nullable<int> t314_idusuario_responsable, Nullable<bool> t485_activo, Nullable<byte> t485_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[0].Value = t485_idcsn1p;
			aParam[1] = new SqlParameter("@t485_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t485_denominacion;
			aParam[2] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[2].Value = t391_idsupernodo1;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t485_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t485_activo;
			aParam[5] = new SqlParameter("@t485_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t485_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CSN1P_C", aParam);
		}

		#endregion
	}
}
