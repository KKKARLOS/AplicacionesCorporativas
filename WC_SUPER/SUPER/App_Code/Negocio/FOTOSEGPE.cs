using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : FOTOSEGPE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T373_FOTOSEGPE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/05/2008 12:40:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FOTOSEGPE
	{
		#region Propiedades y Atributos

		#endregion

		#region Constructores

		#endregion

		#region Metodos
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T373_FOTOSEGPE.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	15/01/2009 9:17:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t373_idfotope, Nullable<DateTime> t373_fecha, Nullable<int> t373_anomes, 
                                             Nullable<int> t305_idproyectosubnodo, Nullable<int> t314_idusuario, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t373_idfotope", SqlDbType.Int, 4);
            aParam[0].Value = t373_idfotope;
            aParam[1] = new SqlParameter("@t373_fecha", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t373_fecha;
            aParam[2] = new SqlParameter("@t373_anomes", SqlDbType.Int, 4);
            aParam[2].Value = t373_anomes;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[3].Value = t305_idproyectosubnodo;
            aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[4].Value = t314_idusuario;

            aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[5].Value = nOrden;
            aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[6].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOSEGPE_C2", aParam);
        }
         
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los totales acumulados de un proyecto económico de una foto de seguimiento de proyecto
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoPE(int T373_idFotoPE, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T373_idFotoPE", SqlDbType.Int, 4, T373_idFotoPE);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOSEGPE_CATA", aParam);
        }

		#endregion
	}
}
