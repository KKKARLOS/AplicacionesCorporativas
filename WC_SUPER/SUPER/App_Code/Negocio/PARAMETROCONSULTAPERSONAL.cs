using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class PARAMETROCONSULTAPERSONAL
    {
        #region Metodos

        public static SqlDataReader SelectByIdconsulta(SqlTransaction tr, int t472_idconsulta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
            aParam[0].Value = t472_idconsulta;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PARAMETROCONSULTAPERSONAL_SByIdconsulta", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_SByIdconsulta", aParam);
        }
        public static SqlDataReader Catalogo(SqlTransaction tr, bool bActivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bActivos", SqlDbType.Bit, 1);
            aParam[0].Value = bActivos;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PARAMETROCONSULTAPERSONAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_CAT", aParam);
        }

        #endregion
    }
}
