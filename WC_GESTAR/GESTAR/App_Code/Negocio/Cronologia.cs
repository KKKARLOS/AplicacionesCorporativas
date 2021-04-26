using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GESTAR
	/// Class	 : CRONOLOGIA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T096_CRONOLOGIA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	20/09/2007 16:20:19	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CRONOLOGIA
	{

		#region Propiedades y Atributos

		#endregion


		#region Constructores

		public CRONOLOGIA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion


		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T096_CRONOLOGIA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	20/09/2007 16:20:19
		/// </history>
		/// -----------------------------------------------------------------------------

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T096_CRONOLOGIA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	20/09/2007 16:20:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> T044_IDDEFICIENCIA)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
			aParam[0].Value = T044_IDDEFICIENCIA;
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_CRONOLOGIA_C", aParam);

			return dr;
		}
        public static SqlDataReader Volcado(Nullable<int> T044_IDDEFICIENCIA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_CRONOLOGIA_V", aParam);

            return dr;
        }
		#endregion
	}
}
