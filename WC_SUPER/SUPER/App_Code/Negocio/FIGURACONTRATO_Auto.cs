using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURACONTRATO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T311_FIGURACONTRATO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURACONTRATO
	{
		#region Propiedades y Atributos

		private int _t306_idcontrato;
		public int t306_idcontrato
		{
			get {return _t306_idcontrato;}
			set { _t306_idcontrato = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t311_figura;
		public string t311_figura
		{
			get {return _t311_figura;}
			set { _t311_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURACONTRATO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T311_FIGURACONTRATO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t306_idcontrato , int t314_idusuario , string t311_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t311_figura", SqlDbType.Text, 1);
			aParam[2].Value = t311_figura;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_FIGURACONTRATO_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACONTRATO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T311_FIGURACONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t306_idcontrato, int t314_idusuario, string t311_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t311_figura", SqlDbType.Text, 1);
			aParam[2].Value = t311_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURACONTRATO_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACONTRATO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T311_FIGURACONTRATO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t306_idcontrato, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURACONTRATO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURACONTRATO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T311_FIGURACONTRATO,
		/// y devuelve una instancia u objeto del tipo FIGURACONTRATO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURACONTRATO Select(SqlTransaction tr, int t306_idcontrato, int t314_idusuario) 
		{
			FIGURACONTRATO o = new FIGURACONTRATO();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURACONTRATO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURACONTRATO_S", aParam);

			if (dr.Read())
			{
				if (dr["t306_idcontrato"] != DBNull.Value)
					o.t306_idcontrato = int.Parse(dr["t306_idcontrato"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t311_figura"] != DBNull.Value)
					o.t311_figura = (string)dr["t311_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURACONTRATO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T311_FIGURACONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t306_idcontrato, Nullable<int> t314_idusuario, string t311_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t311_figura", SqlDbType.Text, 1);
			aParam[2].Value = t311_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACONTRATO_C", aParam);
		}

		#endregion
	}
}
