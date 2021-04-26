using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class FIGURACLIENTE
    {
        #region Metodos
        public static SqlDataReader CatalogoFiguras(Nullable<int> t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACLIENTE_CF", aParam);
        }
        /// <summary>
        /// Obtiene la lista de nodos de las figuras de invitados de cliente
        /// </summary>
        public static SqlDataReader CatalogoInvitados(int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACLIENTE_INVITADO_CF", aParam);
        }
        /// <summary>
        /// Obtiene la lista de nodos de las figuras de invitados de cliente
        /// </summary>
        public static SqlDataReader CatalogoNodos(int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURACLIENTE_NODO_CF", aParam);
        }
        #endregion
    }
}
