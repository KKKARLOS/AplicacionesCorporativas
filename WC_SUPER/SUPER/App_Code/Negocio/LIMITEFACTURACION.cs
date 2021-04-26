using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class LIMITEFACTURACION
    {
        public static DateTime Obtener(SqlTransaction tr, int t637_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t637_anomes", SqlDbType.Int, 4);
            aParam[0].Value = t637_anomes;

            if (tr == null)
                return (DateTime)SqlHelper.ExecuteScalar("SUP_LIMITEFACTURACION_O", aParam);
            else
                return (DateTime)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_LIMITEFACTURACION_O", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T637_LIMITEFACTURACION.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	09/12/2010 9:30:45
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader LimitesAnno(int anno)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@anno", SqlDbType.Int, 4);
            aParam[0].Value = anno;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_LIMITEFACTURACION_CA", aParam);
        }

    }
}