using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;

namespace SUPER.DAL
{
    /// <summary>
    /// Maneja las solicitudes de creación de certificados y exámenes que los profesionales dirijen a RRHH
    /// </summary>
    public class SOLICITUD
    {
        public SOLICITUD()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static int Insertar(SqlTransaction tr, string t696_tipo, string t696_nombre, string t696_observaciones, int t001_idficepi_solic,
                                   Nullable<int> t582_idcertificado, string t697_usuticks, string t696_tipopeticion)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t696_tipo", SqlDbType.Char, 1, t696_tipo),
                ParametroSql.add("@t696_nombre", SqlDbType.VarChar, 200, t696_nombre),
                ParametroSql.add("@t696_observaciones", SqlDbType.Text, 2147483647, t696_observaciones),
                ParametroSql.add("@t001_idficepi_solic", SqlDbType.Int, 4, t001_idficepi_solic),
                ParametroSql.add("@t582_idcertificado", SqlDbType.Int, 4, t582_idcertificado),
                ParametroSql.add("@t697_usuticks", SqlDbType.VarChar, 50, t697_usuticks),
                ParametroSql.add("@t696_tipopeticion", SqlDbType.Char, 1, t696_tipopeticion)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_SOLICITUD_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_SOLICITUD_I", aParam));
        }
        public static void Resolver(SqlTransaction tr, int t696_id, string t696_motivo, int t001_idficepi_res, string t696_tipores)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t696_id", SqlDbType.Int, 4, t696_id),
                ParametroSql.add("@t001_idficepi_res", SqlDbType.Int, 4, t001_idficepi_res),
                ParametroSql.add("@t696_tipores", SqlDbType.Char, 1, t696_tipores),
                ParametroSql.add("@t696_motivo", SqlDbType.Text, 2147483647, t696_motivo)
            };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_SOLICITUD_RESOLVER", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_SOLICITUD_RESOLVER", aParam);
        }
    }
}