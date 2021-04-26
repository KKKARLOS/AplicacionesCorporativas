using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FIGURASCSN4P
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T492_FIGURASCSN4P
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/06/2009 12:50:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FIGURASCSN4P
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(int t491_idcsn4p)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[0].Value = t491_idcsn4p;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASCSN4P_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t491_idcsn4p, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[0].Value = t491_idcsn4p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN4P_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN4P_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t491_idcsn4p, int t314_idusuario, string t492_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[0].Value = t491_idcsn4p;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t492_figura", SqlDbType.Char, 1);
            aParam[2].Value = t492_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN4P_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN4P_D", aParam);
        }

        #endregion
    }
}
