using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for AGRUPACIONORDEN
    /// </summary>
    public partial class AGRUPACIONORDEN
    {
        public static SqlDataReader ObtenerCatalogo(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_AGRUPACIONONDEN_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_AGRUPACIONONDEN_CAT", aParam);
        }

    }
}