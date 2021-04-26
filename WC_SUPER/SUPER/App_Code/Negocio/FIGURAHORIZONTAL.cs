using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class FIGURAHORIZONTAL
    {
        #region Metodos
        public static SqlDataReader CatalogoFiguras(Nullable<int> t307_idhorizontal)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
            aParam[0].Value = t307_idhorizontal;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAHORIZONTAL_CF", aParam);
        }
        #endregion
    }
}
