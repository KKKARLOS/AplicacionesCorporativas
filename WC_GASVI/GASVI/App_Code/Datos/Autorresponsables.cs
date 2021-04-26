using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
    /// Class	 : AUTORRESPONSABLE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T443_AUTORRESPONSABLEGV
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	21/06/2011 10:02:40	
	/// </history>
	/// -----------------------------------------------------------------------------
    public partial class AUTORRESPONSABLE
	{
		#region Metodos

        public static SqlDataReader ObtenerIntegrantes()
		{
			SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GVT_AUTORRESPONSABLEGV_CAT", aParam);
		}

        public static SqlDataReader CatalogoPersonas(string t001_apellido1, string t001_apellido2, string t001_nombre, string excluidos, bool todos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre);
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@excluidos", SqlDbType.VarChar, 8000, excluidos);
            aParam[i++] = ParametroSql.add("@todos", SqlDbType.Bit, 1, todos);

            return SqlHelper.ExecuteSqlDataReader("GVT_FICEPI_SEL", aParam);

        }

        public static void InsertAutorresponsable(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_AUTORRESPONSABLEGV_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_AUTORRESPONSABLEGV_INS", aParam);
        }

        public static void DeleteAutorresponsable(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_AUTORRESPONSABLEGV_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_AUTORRESPONSABLEGV_DEL", aParam);
        }

		#endregion
	}
}
