using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de AMBITO
    /// </summary>
    public class AMBITO
    {
       #region Metodos

        public static SqlDataReader Arbol()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_AMBITO_ZONA_CAT", aParam);
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_AMBITO_C", aParam);

        }
        public static SqlDataReader Zonas(Nullable<int> t481_idambito)
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t481_idambito", SqlDbType.Int, 4);
            aParam[0].Value = t481_idambito;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_ZONASAMBITO_C", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T481_AMBITO
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t481_denominacion, string t481_codigoexterno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t481_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t481_denominacion;
            aParam[1] = new SqlParameter("@t481_codigoexterno", SqlDbType.Text, 15);
            aParam[1].Value = t481_codigoexterno;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalar("SUP_AMBITO_I", aParam));
            else
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AMBITO_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T481_AMBITO
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t481_idambito, string t481_denominacion, string t481_codigoexterno)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t481_idambito", SqlDbType.Int, 4);
            aParam[0].Value = t481_idambito;
            aParam[1] = new SqlParameter("@t481_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t481_denominacion;
            aParam[2] = new SqlParameter("@t481_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t481_codigoexterno;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_AMBITO_U", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AMBITO_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T481_AMBITO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t481_idambito)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t481_idambito", SqlDbType.Int, 4);
            aParam[0].Value = t481_idambito;

            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_AMBITO_D", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AMBITO_D", aParam);
        }        

		#endregion
    }
}