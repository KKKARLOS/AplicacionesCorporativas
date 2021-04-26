using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de FamiliaPerfil
    /// </summary>
    public class FamiliaPerfil
    {
        public FamiliaPerfil()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Familia
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[] { };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAPERFIL_CAT", aParam);
        }
        public static SqlDataReader CatalogoProfesional(int t001_idficepi, string sSoloPrivadas)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@SoloPrivadas", SqlDbType.Char, 1, sSoloPrivadas)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAPERFIL_PROFESIONAL_CAT", aParam);
        }
        public static int Modificar(SqlTransaction tr, int t859_idfamperfil, string t859_denominacion, int t001_idficepi, bool t859_publica)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil),
                ParametroSql.add("@t859_denominacion", SqlDbType.VarChar, 50, t859_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t859_publica", SqlDbType.Bit, 1, t859_publica)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_U", aParam);
        }
        public static int CambiarDenominacion(SqlTransaction tr, int t859_idfamperfil, string t859_denominacion, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil),
                ParametroSql.add("@t859_denominacion", SqlDbType.VarChar, 50, t859_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_U2", aParam);
        }
        public static int Insertar(SqlTransaction tr, string t859_denominacion, int t001_idficepi, bool t859_publica)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_denominacion", SqlDbType.VarChar, 50, t859_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t859_publica", SqlDbType.Bit, 1, t859_publica)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FAMILIAPERFIL_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FAMILIAPERFIL_I", aParam));
        }
        public static int Borrar(SqlTransaction tr, int t859_idfamperfil)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_D", aParam);
        }
        public static int Publicar(SqlTransaction tr, int t859_idfamperfil, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_PUBLICAR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_PUBLICAR", aParam);
        }
        public static void CopiarPerfiles(SqlTransaction tr, int idFamOrigen, int idFamDestino)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@idFamOrigen", SqlDbType.Int, 4, idFamOrigen),
                ParametroSql.add("@idFamDestino", SqlDbType.Int, 4, idFamDestino)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_COPIAR_PERFILES", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_COPIAR_PERFILES", aParam);
        }
        public static SqlDataReader SelectDenyAutor(SqlTransaction tr, string t859_denominacion, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_denominacion", SqlDbType.VarChar, 50, t859_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAPERFIL_S1", aParam);
        }
        #endregion

        #region Perfil en la familia
        public static SqlDataReader CatalogoPerfil(int t859_idfamperfil)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAPERFIL_PERFIL_CAT", aParam);
        }
        public static void InsertarPerfil(SqlTransaction tr, int t859_idfamperfil, int t035_idcodperfil)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil),
                ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_PERFIL_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_PERFIL_I", aParam);
        }
        public static int BorrarPerfil(SqlTransaction tr, int t859_idfamperfil, int t035_idcodperfil)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t859_idfamperfil", SqlDbType.Int, 4, t859_idfamperfil),
                ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAPERFIL_PERFIL_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAPERFIL_PERFIL_D", aParam);
        }
        #endregion
    }
}