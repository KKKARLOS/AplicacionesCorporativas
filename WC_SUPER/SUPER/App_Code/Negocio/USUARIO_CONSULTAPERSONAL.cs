using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class USUARIO_CONSULTAPERSONAL
    {
        #region Metodos

        public static SqlDataReader ObtenerCatalogo(SqlTransaction tr, Nullable<int> t314_idusuario, bool t473_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t473_estado", SqlDbType.Bit, 1);
            aParam[1].Value = t473_estado;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_CONSULTAPERSONAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIO_CONSULTAPERSONAL_CAT", aParam);
        }

        public static int GetNumConsultas(int t314_idusuario, bool t472_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t472_estado", SqlDbType.Int, 4);
            aParam[1].Value = t472_estado;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_CONSULTAPERSONAL_COUNT", aParam));
        }

        #endregion
    }
}
