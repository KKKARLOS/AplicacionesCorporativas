using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AGRUPACIONORDEN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T622_AGRUPACIONORDEN
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/10/2010 12:21:28	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AGRUPACIONORDEN
	{
		#region Propiedades y Atributos

		private int _t622_idagrupacion;
		public int t622_idagrupacion
		{
			get {return _t622_idagrupacion;}
			set { _t622_idagrupacion = value ;}
		}

		private string _t622_denominacion;
		public string t622_denominacion
		{
			get {return _t622_denominacion;}
			set { _t622_denominacion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}
		#endregion

		#region Constructor

		public AGRUPACIONORDEN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T622_AGRUPACIONORDEN.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:21:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t622_denominacion , int t314_idusuario_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t622_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t622_denominacion;
			aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AGRUPACIONORDEN_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AGRUPACIONORDEN_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T622_AGRUPACIONORDEN.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:21:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t622_idagrupacion, string t622_denominacion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[0].Value = t622_idagrupacion;
			aParam[1] = new SqlParameter("@t622_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t622_denominacion;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AGRUPACIONORDEN_UPD", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGRUPACIONORDEN_UPD", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T622_AGRUPACIONORDEN a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:21:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t622_idagrupacion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[0].Value = t622_idagrupacion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AGRUPACIONORDEN_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGRUPACIONORDEN_D", aParam);
		}

		#endregion
	}
}
