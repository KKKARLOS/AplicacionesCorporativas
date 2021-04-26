using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SN3
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T679_FIGURAPSN_SN3
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:31	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SN3
	{
        public static SqlDataReader CatalogoFiguras(int t393_idsupernodo3)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_SN3_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t393_idsupernodo3, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN3_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN3_DEL", aParam);
        }
    }
}
