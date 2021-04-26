using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T676_FIGURAPSN_SUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:31	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SUBNODO
	{
        public static SqlDataReader CatalogoFiguras(int t304_idsubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_SUBNODO_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t304_idsubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SUBNODO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SUBNODO_DEL", aParam);
        }
    }
}
