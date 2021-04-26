using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ACCESOMODULO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T434_ACCESOMODULO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/02/2010 7:29:42	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCESOMODULO
	{
		#region Propiedades y Atributos

		private string _t434_modulo;
		public string t434_modulo
		{
			get {return _t434_modulo;}
			set { _t434_modulo = value ;}
		}

		private bool _t434_acceso;
		public bool t434_acceso
		{
			get {return _t434_acceso;}
			set { _t434_acceso = value ;}
		}
		#endregion

		#region Constructor

		public ACCESOMODULO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T434_ACCESOMODULO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/02/2010 7:29:42
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, string t434_modulo, bool t434_acceso)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t434_modulo", SqlDbType.Text, 10);
			aParam[0].Value = t434_modulo;
			aParam[1] = new SqlParameter("@t434_acceso", SqlDbType.Bit, 1);
			aParam[1].Value = t434_acceso;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ACCESOMODULO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCESOMODULO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T434_ACCESOMODULO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/02/2010 7:29:42
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(string t434_modulo, Nullable<bool> t434_acceso, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t434_modulo", SqlDbType.Text, 10);
			aParam[0].Value = t434_modulo;
			aParam[1] = new SqlParameter("@t434_acceso", SqlDbType.Bit, 1);
			aParam[1].Value = t434_acceso;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_ACCESOMODULO_C", aParam);
		}

		#endregion
	}
}
