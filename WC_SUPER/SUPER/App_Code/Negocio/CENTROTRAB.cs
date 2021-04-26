using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class CENTROTRAB
	{
		#region Metodos
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Muestra los centros de trabajo con sus canales de distribución asociados
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_CENTROTRAB_CD", aParam);
        }
        public static SqlDataReader Obtener()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_CENTROTRAB_CAT", aParam);
        }
		#endregion


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza el canal de distribución de la tabla T009_CENTROTRAB.
        /// </summary>
         /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int T009_IDCENTRAB, string T009_CDSAP)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T009_IDCENTRAB", SqlDbType.Int, 4);
            aParam[0].Value = T009_IDCENTRAB;
            aParam[1] = new SqlParameter("@T009_CDSAP", SqlDbType.VarChar, 2);
            aParam[1].Value = T009_CDSAP;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_MANTCENTRO_U_CD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MANTCENTRO_U_CD", aParam);
        }
	}
}
