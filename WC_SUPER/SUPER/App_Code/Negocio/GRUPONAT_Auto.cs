using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : GRUPONAT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T321_GRUPONAT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class GRUPONAT
	{
		#region Propiedades y Atributos

		private int _t321_idgruponat;
		public int t321_idgruponat
		{
			get {return _t321_idgruponat;}
			set { _t321_idgruponat = value ;}
		}

		private string _t321_denominacion;
		public string t321_denominacion
		{
			get {return _t321_denominacion;}
			set { _t321_denominacion = value ;}
		}

		private byte _t320_idtipologiaproy;
		public byte t320_idtipologiaproy
		{
			get {return _t320_idtipologiaproy;}
			set { _t320_idtipologiaproy = value ;}
		}

		private int _t321_orden;
		public int t321_orden
		{
			get {return _t321_orden;}
			set { _t321_orden = value ;}
		}

		private bool _t321_estado;
		public bool t321_estado
		{
			get {return _t321_estado;}
			set { _t321_estado = value ;}
		}
		#endregion

		#region Constructor

		public GRUPONAT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T321_GRUPONAT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2009 10:40:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t321_denominacion , byte t320_idtipologiaproy , int t321_orden , bool t321_estado)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t321_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t321_denominacion;
			aParam[1] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[1].Value = t320_idtipologiaproy;
			aParam[2] = new SqlParameter("@t321_orden", SqlDbType.Int, 4);
			aParam[2].Value = t321_orden;
			aParam[3] = new SqlParameter("@t321_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t321_estado;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GRUPONAT_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GRUPONAT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T321_GRUPONAT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2009 10:40:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t321_idgruponat, string t321_denominacion, byte t320_idtipologiaproy, int t321_orden, bool t321_estado)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[0].Value = t321_idgruponat;
			aParam[1] = new SqlParameter("@t321_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t321_denominacion;
			aParam[2] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[2].Value = t320_idtipologiaproy;
			aParam[3] = new SqlParameter("@t321_orden", SqlDbType.Int, 4);
			aParam[3].Value = t321_orden;
			aParam[4] = new SqlParameter("@t321_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t321_estado;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_GRUPONAT_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GRUPONAT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T321_GRUPONAT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2009 10:40:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t321_idgruponat)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[0].Value = t321_idgruponat;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_GRUPONAT_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GRUPONAT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T321_GRUPONAT,
		/// y devuelve una instancia u objeto del tipo GRUPONAT
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2009 10:40:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static GRUPONAT Select(SqlTransaction tr, int t321_idgruponat) 
		{
			GRUPONAT o = new GRUPONAT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[0].Value = t321_idgruponat;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_GRUPONAT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GRUPONAT_S", aParam);

			if (dr.Read())
			{
				if (dr["t321_idgruponat"] != DBNull.Value)
					o.t321_idgruponat = int.Parse(dr["t321_idgruponat"].ToString());
				if (dr["t321_denominacion"] != DBNull.Value)
					o.t321_denominacion = (string)dr["t321_denominacion"];
				if (dr["t320_idtipologiaproy"] != DBNull.Value)
					o.t320_idtipologiaproy = byte.Parse(dr["t320_idtipologiaproy"].ToString());
				if (dr["t321_orden"] != DBNull.Value)
					o.t321_orden = int.Parse(dr["t321_orden"].ToString());
				if (dr["t321_estado"] != DBNull.Value)
					o.t321_estado = (bool)dr["t321_estado"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de GRUPONAT"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T321_GRUPONAT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2009 10:40:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t321_idgruponat, string t321_denominacion, Nullable<byte> t320_idtipologiaproy, Nullable<int> t321_orden, Nullable<bool> t321_estado, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[0].Value = t321_idgruponat;
			aParam[1] = new SqlParameter("@t321_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t321_denominacion;
			aParam[2] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[2].Value = t320_idtipologiaproy;
			aParam[3] = new SqlParameter("@t321_orden", SqlDbType.Int, 4);
			aParam[3].Value = t321_orden;
			aParam[4] = new SqlParameter("@t321_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t321_estado;

			aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[5].Value = nOrden;
			aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[6].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_GRUPONAT_C", aParam);
		}

		#endregion

	}
}
