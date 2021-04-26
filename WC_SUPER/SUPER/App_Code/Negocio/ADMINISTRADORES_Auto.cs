using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ADMINISTRADORES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T399_ADMINISTRADORES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	15/02/2010 16:17:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ADMINISTRADORES
	{
		#region Propiedades y Atributos

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private string _t399_figura;
		public string t399_figura
		{
			get {return _t399_figura;}
			set { _t399_figura = value ;}
		}
		#endregion

		#region Constructor

		public ADMINISTRADORES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T399_ADMINISTRADORES.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t001_idficepi , string t399_figura)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;
			aParam[1] = new SqlParameter("@t399_figura", SqlDbType.Text, 1);
			aParam[1].Value = t399_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_ADMINISTRADORES_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ADMINISTRADORES_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T399_ADMINISTRADORES.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t001_idficepi, string t399_figura)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;
			aParam[1] = new SqlParameter("@t399_figura", SqlDbType.Text, 1);
			aParam[1].Value = t399_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ADMINISTRADORES_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ADMINISTRADORES_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T399_ADMINISTRADORES a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ADMINISTRADORES_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ADMINISTRADORES_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T399_ADMINISTRADORES.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo()
		{
			SqlParameter[] aParam = new SqlParameter[0];
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_ADMINISTRADORES_CAT", aParam);
		}

		#endregion
	}
}
