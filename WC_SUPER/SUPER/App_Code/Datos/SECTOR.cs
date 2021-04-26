using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de SECTOR
    /// </summary>
    public class SECTOR
    {
       #region Metodos
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_SECTORES_C", aParam);

        }
        public static SqlDataReader Arbol()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_SECTOR_SEGMENTO_CAT", aParam);            
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T483_SECTOR.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t483_denominacion, string t483_codigoexterno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t483_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t483_denominacion;
            aParam[1] = new SqlParameter("@t483_codigoexterno", SqlDbType.Text, 15);
            aParam[1].Value = t483_codigoexterno;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalar("SUP_SECTOR_I", aParam));
            else
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SECTOR_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T483_SECTOR.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t483_idsector, string t483_denominacion, string t483_codigoexterno)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[0].Value = t483_idsector;
            aParam[1] = new SqlParameter("@t483_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t483_denominacion;
            aParam[2] = new SqlParameter("@t483_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t483_codigoexterno;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_SECTOR_U", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SECTOR_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T483_SECTOR a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t483_idsector)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[0].Value = t483_idsector;

            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_SECTOR_D", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SECTOR_D", aParam);
        }
        public static SqlDataReader Segmentos(Nullable<int> t483_idsector)
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[0].Value = t483_idsector;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTOSSECTOR_C", aParam);
        }    
		#endregion
    }
}