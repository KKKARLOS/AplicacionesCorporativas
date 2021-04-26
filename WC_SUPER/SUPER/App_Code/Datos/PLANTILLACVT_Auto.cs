using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PLANTILLACVT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T819_PLANTILLACVT
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	13/08/2012 11:34:31	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PLANTILLACVT
    {

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T819_PLANTILLACVT.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 11:34:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t819_denominacion, string t819_funcion, string t819_observa, int t808_idexpprof,
                                 Nullable<int> t035_idcodperfil, short t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_denominacion", SqlDbType.VarChar, 50, t819_denominacion);
            aParam[i++] = ParametroSql.add("@t819_funcion", SqlDbType.Text, 2147483647, t819_funcion);
            aParam[i++] = ParametroSql.add("@t819_observa", SqlDbType.Text, 2147483647, t819_observa);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 2, t020_idcodidioma);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_PLANTILLACVT_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_PLANTILLACVT_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T819_PLANTILLACVT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 11:34:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t819_idplantillacvt, string t819_denominacion, string t819_funcion, 
                                 string t819_observa, int t808_idexpprof, Nullable<int> t035_idcodperfil, short t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            aParam[i++] = ParametroSql.add("@t819_denominacion", SqlDbType.VarChar, 50, t819_denominacion);
            aParam[i++] = ParametroSql.add("@t819_funcion", SqlDbType.Text, 2147483647, t819_funcion);
            aParam[i++] = ParametroSql.add("@t819_observa", SqlDbType.Text, 2147483647, t819_observa);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 2, t020_idcodidioma);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_PLANTILLACVT_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PLANTILLACVT_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T819_PLANTILLACVT a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 11:34:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_PLANTILLACVT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PLANTILLACVT_D", aParam);
        }

        #endregion
    }
}
