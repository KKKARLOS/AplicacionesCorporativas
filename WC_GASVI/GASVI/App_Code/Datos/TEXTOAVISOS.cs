using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// <summary>
	/// Descripción breve de Idiomas.
	/// </summary>
	public partial class TEXTOAVISOS
    {
        #region Métodos

        public static SqlDataReader ObtenerAvisos()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GVT_AVISOS_INI", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T774_TEXTOAVISOSGASVI.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	14/12/2011 18:00:40
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t774_denominacion, string t774_titulo, string t774_texto, bool t774_borrable, Nullable<DateTime> t774_fiv, Nullable<DateTime> t774_ffv)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t774_denominacion", SqlDbType.Text, 50, t774_denominacion);
            aParam[i++] = ParametroSql.add("@t774_titulo", SqlDbType.Text, 50, t774_titulo);
            aParam[i++] = ParametroSql.add("@t774_texto", SqlDbType.Text, 2147483647, t774_texto);
            aParam[i++] = ParametroSql.add("@t774_borrable", SqlDbType.Bit, 1, t774_borrable);
            aParam[i++] = ParametroSql.add("@t774_fiv", SqlDbType.SmallDateTime, 4, t774_fiv);
            aParam[i++] = ParametroSql.add("@t774_ffv", SqlDbType.SmallDateTime, 4, t774_ffv);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_TEXTOAVISOS_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_TEXTOAVISOS_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T774_TEXTOAVISOSGASVI.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	14/12/2011 18:00:40
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t774_idaviso, string t774_denominacion, string t774_titulo, string t774_texto, bool t774_borrable, Nullable<DateTime> t774_fiv, Nullable<DateTime> t774_ffv)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t774_idaviso", SqlDbType.Int, 4, t774_idaviso);
            aParam[i++] = ParametroSql.add("@t774_denominacion", SqlDbType.Text, 50, t774_denominacion);
            aParam[i++] = ParametroSql.add("@t774_titulo", SqlDbType.Text, 50, t774_titulo);
            aParam[i++] = ParametroSql.add("@t774_texto", SqlDbType.Text, 2147483647, t774_texto);
            aParam[i++] = ParametroSql.add("@t774_borrable", SqlDbType.Bit, 1, t774_borrable);
            aParam[i++] = ParametroSql.add("@t774_fiv", SqlDbType.SmallDateTime, 4, t774_fiv);
            aParam[i++] = ParametroSql.add("@t774_ffv", SqlDbType.SmallDateTime, 4, t774_ffv);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_TEXTOAVISOS_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_TEXTOAVISOS_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T774_TEXTOAVISOSGASVI a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	14/12/2011 18:00:40
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t774_idaviso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t774_idaviso", SqlDbType.Int, 4, t774_idaviso);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_TEXTOAVISOS_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_TEXTOAVISOS_D", aParam);
        }

        #endregion
    }
}
