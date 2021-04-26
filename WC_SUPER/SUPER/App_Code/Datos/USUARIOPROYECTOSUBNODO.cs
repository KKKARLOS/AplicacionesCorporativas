using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public class USUARIOPROYECTOSUBNODO
    {
        public static SqlDataReader GetUsuariosCambioEstructura(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t305_idproyectosubnodo_replica)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo_contratante", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo_contratante;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo_replica", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo_replica;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURA_PROYECTO_12", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_12", aParam);
        }
    }
}