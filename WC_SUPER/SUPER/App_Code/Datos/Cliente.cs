using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de Cliente
    /// </summary>
    public class CLIENTE
    {
        #region Metodos


        public static SqlDataReader ObtenerClientesAvisosExcepciones(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_AVISOS_EXCP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLIENTE_AVISOS_EXCP_C", aParam);
        }
        public static SqlDataReader ObtenerClientes(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t302_denominacion", SqlDbType.Text, 100);
            aParam[0].Value = t302_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
            aParam[2].Value = bSoloActivos;
            aParam[3] = new SqlParameter("@bInternos", SqlDbType.Bit, 1);
            aParam[3].Value = bInternos;
            aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[4].Value = t314_idusuario;
            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre", aParam);
        }
        public static void Update(SqlTransaction tr, int t302_idcliente, Nullable<bool> t302_noalertas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t302_noalertas", SqlDbType.Bit, 1, t302_noalertas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CLIENTE_NOALERTA", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CLIENTE_NOALERTA", aParam);
        }
        #endregion
    }
}

