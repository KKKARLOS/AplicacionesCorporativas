using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FICHEROSMANIOBRA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T447_FICHEROSMANIOBRA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/10/2008 11:41:01	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FICHEROSMANIOBRA
	{
		#region Propiedades y Atributos

		private byte _t447_idtipo;
		public byte t447_idtipo
		{
			get {return _t447_idtipo;}
			set { _t447_idtipo = value ;}
		}

		private string _t447_denominacion;
		public string t447_denominacion
		{
			get {return _t447_denominacion;}
			set { _t447_denominacion = value ;}
		}

		private byte[] _t447_fichero;
		public byte[] t447_fichero
		{
			get {return _t447_fichero;}
			set { _t447_fichero = value ;}
		}
		#endregion

		#region Constructor

		public FICHEROSMANIOBRA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T447_FICHEROSMANIOBRA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	03/10/2008 11:41:01
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t447_denominacion , byte[] t447_fichero)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t447_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t447_denominacion;
			aParam[1] = new SqlParameter("@t447_fichero", SqlDbType.Binary, 2147483647);
			aParam[1].Value = t447_fichero;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FICHEROSMANIOBRA_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FICHEROSMANIOBRA_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T447_FICHEROSMANIOBRA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/10/2008 11:41:01
		/// </history>
		/// -----------------------------------------------------------------------------
		//public static int Update(SqlTransaction tr, byte t447_idtipo, string t447_denominacion, Nullable<byte[]> t447_fichero)
        public static int Update(SqlTransaction tr, byte t447_idtipo, string t447_denominacion, byte[] t447_fichero)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t447_idtipo", SqlDbType.TinyInt, 1);
			aParam[0].Value = t447_idtipo;
			aParam[1] = new SqlParameter("@t447_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t447_denominacion;
			aParam[2] = new SqlParameter("@t447_fichero", SqlDbType.Binary, 2147483647);
			aParam[2].Value = t447_fichero;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FICHEROSMANIOBRA_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICHEROSMANIOBRA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T447_FICHEROSMANIOBRA,
		/// y devuelve una instancia u objeto del tipo FICHEROSMANIOBRA
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	03/10/2008 11:41:01
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FICHEROSMANIOBRA Select(SqlTransaction tr, byte t447_idtipo) 
		{
			FICHEROSMANIOBRA o = new FICHEROSMANIOBRA();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t447_idtipo", SqlDbType.TinyInt, 1);
			aParam[0].Value = t447_idtipo;

			SqlDataReader dr;
			if (tr == null) 
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FICHEROSMANIOBRA_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FICHEROSMANIOBRA_S", aParam);
            
			if (dr.Read())
			{
				if (dr["t447_idtipo"] != DBNull.Value)
					o.t447_idtipo = byte.Parse(dr["t447_idtipo"].ToString());
				if (dr["t447_denominacion"] != DBNull.Value)
					o.t447_denominacion = (string)dr["t447_denominacion"];
				if (dr["t447_fichero"] != DBNull.Value)
					o.t447_fichero = (byte[])dr["t447_fichero"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FICHEROSMANIOBRA"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
