using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class MODOFACTSN2
    {
        #region Metodos

        public static SqlDataReader ObtenerActivosMasActual(int t324_idmodofact, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
            aParam[0].Value = t324_idmodofact;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_MODOFACTSN2_AMA", aParam);
        }

        #endregion
    }
}
