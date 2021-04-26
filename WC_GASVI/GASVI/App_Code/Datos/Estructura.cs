using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class Estructura
    {
        #region Métodos

        public static SqlDataReader ListaActiva()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ESTRUCTURA_AC", aParam);
        }

        public static SqlDataReader GetDatosEstructura()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_DEN_ESTRUCTURA_C", aParam);
        }

        public static SqlDataReader getEstructura(int bInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@bMostrarInactivos", SqlDbType.Bit, 1, bInactivos)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_ORGANIZATIVA", aParam);

        }

        public static SqlDataReader getEstructuraCenCos(int bInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@bMostrarInactivos", SqlDbType.Bit, 1, bInactivos)
            };

            return SqlHelper.ExecuteSqlDataReader("GVT_ESTRUORGA_CENCOS", aParam);

        }
        #endregion
    }
}
