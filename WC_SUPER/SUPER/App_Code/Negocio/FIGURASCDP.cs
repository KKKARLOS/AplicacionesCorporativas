using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASCDP
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T477_FIGURASCDP
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/06/2009 12:50:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASCDP
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(int @t476_idcnp)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = @t476_idcnp;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASCDP_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t476_idcnp, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = t476_idcnp;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCDP_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCDP_DEL", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T477_FIGURASCDP a traves de la primary key.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t476_idcnp, int t314_idusuario, string t477_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = t476_idcnp;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t477_figura", SqlDbType.Char, 1);
            aParam[2].Value = t477_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCDP_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCDP_D", aParam);
        }
        #endregion
    }
}
