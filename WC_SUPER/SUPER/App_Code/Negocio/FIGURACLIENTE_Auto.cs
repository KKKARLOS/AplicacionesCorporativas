using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURACLIENTE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T401_FIGURACLIENTE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:30	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURACLIENTE
	{
		#region Propiedades y Atributos

		private int _t302_idcliente;
		public int t302_idcliente
		{
			get {return _t302_idcliente;}
			set { _t302_idcliente = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t401_figura;
		public string t401_figura
		{
			get {return _t401_figura;}
			set { _t401_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURACLIENTE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T401_FIGURACLIENTE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t302_idcliente , int t314_idusuario , string t401_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t401_figura", SqlDbType.Text, 1);
			aParam[2].Value = t401_figura;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_FIGURACLIENTE_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACLIENTE_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T401_FIGURACLIENTE.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t302_idcliente, int t314_idusuario, string t401_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t401_figura", SqlDbType.Text, 1);
			aParam[2].Value = t401_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURACLIENTE_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACLIENTE_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T401_FIGURACLIENTE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t302_idcliente, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURACLIENTE_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACLIENTE_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T401_FIGURACLIENTE,
		/// y devuelve una instancia u objeto del tipo FIGURACLIENTE
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURACLIENTE Select(SqlTransaction tr, int t302_idcliente, int t314_idusuario) 
		{
			FIGURACLIENTE o = new FIGURACLIENTE();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURACLIENTE_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURACLIENTE_S", aParam);

			if (dr.Read())
			{
				if (dr["t302_idcliente"] != DBNull.Value)
					o.t302_idcliente = int.Parse(dr["t302_idcliente"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t401_figura"] != DBNull.Value)
					o.t401_figura = (string)dr["t401_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURACLIENTE"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T401_FIGURACLIENTE.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t302_idcliente, Nullable<int> t314_idusuario, string t401_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t401_figura", SqlDbType.Text, 1);
			aParam[2].Value = t401_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACLIENTE_C", aParam);
		}

		#endregion
	}
}
