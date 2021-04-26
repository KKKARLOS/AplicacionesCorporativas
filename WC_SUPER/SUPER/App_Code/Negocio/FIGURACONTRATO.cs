using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class FIGURACONTRATO
    {
        #region Metodos
        public static SqlDataReader CatalogoFiguras(Nullable<int> t306_idcontrato)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACONTRATO_CF", aParam);
        }
        #endregion
    }
}
