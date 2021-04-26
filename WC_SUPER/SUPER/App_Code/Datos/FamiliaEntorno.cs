using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de FamiliaEntorno
    /// </summary>
    public class FamiliaEntorno
    {
        public FamiliaEntorno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Familia
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[] { };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAENTORNO_CAT", aParam);
        }
        public static SqlDataReader CatalogoProfesional(int t001_idficepi, string sSoloPrivadas)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@SoloPrivadas", SqlDbType.Char, 1, sSoloPrivadas)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAENTORNO_PROFESIONAL_CAT", aParam);
        }
        public static int Modificar(SqlTransaction tr, int t861_idfament, string t861_denominacion, int t001_idficepi, bool t861_publica)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament),
                ParametroSql.add("@t861_denominacion", SqlDbType.VarChar, 50, t861_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t861_publica", SqlDbType.Bit, 1, t861_publica)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_U", aParam);
        }
        public static int CambiarDenominacion(SqlTransaction tr, int t861_idfament, string t861_denominacion, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament),
                ParametroSql.add("@t861_denominacion", SqlDbType.VarChar, 50, t861_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_U2", aParam);
        }
        public static int Insertar(SqlTransaction tr, string t861_denominacion, int t001_idficepi, bool t861_publica)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_denominacion", SqlDbType.VarChar, 50, t861_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t861_publica", SqlDbType.Bit, 1, t861_publica)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FAMILIAENTORNO_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FAMILIAENTORNO_I", aParam));
        }
        public static int Borrar(SqlTransaction tr, int t861_idfament)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_D", aParam);
        }
        public static int Publicar(SqlTransaction tr, int t861_idfament, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_PUBLICAR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_PUBLICAR", aParam);
        }
        public static void CopiarEntornos(SqlTransaction tr, int idFamOrigen, int idFamDestino)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@idFamOrigen", SqlDbType.Int, 4, idFamOrigen),
                ParametroSql.add("@idFamDestino", SqlDbType.Int, 4, idFamDestino)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_COPIAR_ENTORNOS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_COPIAR_ENTORNOS", aParam);
        }
        public static SqlDataReader SelectDenyAutor(SqlTransaction tr, string t861_denominacion, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_denominacion", SqlDbType.VarChar, 50, t861_denominacion),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAENTORNO_S1", aParam);
        }
        #endregion

        #region Entorno en la familia
        public static SqlDataReader CatalogoEntorno(int t861_idfament)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FAMILIAENTORNO_ENTORNO_CAT", aParam);
        }
        public static void InsertarEntorno(SqlTransaction tr, int t861_idfament, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_ENTORNO_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_ENTORNO_I", aParam);
        }
        public static int BorrarEntorno(SqlTransaction tr, int t861_idfament, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t861_idfament", SqlDbType.Int, 4, t861_idfament),
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FAMILIAENTORNO_ENTORNO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FAMILIAENTORNO_ENTORNO_D", aParam);
        }
        #endregion
    }
}
