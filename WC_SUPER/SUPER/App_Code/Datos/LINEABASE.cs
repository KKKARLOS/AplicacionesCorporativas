using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    public partial class LINEABASE
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T685_LINEABASE.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	28/02/2012 9:58:56
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insertar(SqlTransaction tr, string t685_denominacion, int t314_idusuario, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                ParametroSql.add("@t685_denominacion", SqlDbType.Text, 50, t685_denominacion),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_LINEABASE_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_LINEABASE_INS", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T685_LINEABASE a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	28/02/2012 9:58:56
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Eliminar(SqlTransaction tr, int t685_idlineabase)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_LINEABASE_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_LINEABASE_D", aParam);
        }

        public static SqlDataReader CatalogoByPSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_LINEABASE_CATBYPSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LINEABASE_CATBYPSN", aParam);
        }

        public static DataSet ObtenerDatosValorGanado(SqlTransaction tr, int t685_idlineabase, int t325_anomes_referencia, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase),
                ParametroSql.add("@t325_anomes_referencia", SqlDbType.Int, 4, t325_anomes_referencia),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASE_GETDATOS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASE_GETDATOS", aParam);
        }
        public static DataSet ObtenerDatosCreacionLineaBAse(SqlTransaction tr, int t305_idproyectosubnodo, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASECREACION_GETDATOS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASECREACION_GETDATOS", aParam);
        }

        public static int ObtenerUltimaLineaBase(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_LINEABASE_ULTIMABYPSN", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_LINEABASE_ULTIMABYPSN", aParam));
        }

        public static SqlDataReader ObtenerMeses(SqlTransaction tr, int t685_idlineabase)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_LINEABASE_GETMESES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LINEABASE_GETMESES", aParam);
        }

        public static Capa_Negocio.LINEABASE Obtener(SqlTransaction tr, int t685_idlineabase)
        {
            Capa_Negocio.LINEABASE o = new Capa_Negocio.LINEABASE();

            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase)
            };

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_LINEABASE_SEL", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LINEABASE_SEL", aParam);

            if (dr.Read())
            {
                if (dr["t685_idlineabase"] != DBNull.Value)
                    o.t685_idlineabase = int.Parse(dr["t685_idlineabase"].ToString());
                if (dr["t685_denominacion"] != DBNull.Value)
                    o.t685_denominacion = (string)dr["t685_denominacion"];
                if (dr["t685_fecha"] != DBNull.Value)
                    o.t685_fecha = (DateTime)dr["t685_fecha"];
                if (dr["t314_idusuario_promotor"] != DBNull.Value)
                    o.t314_idusuario_promotor = int.Parse(dr["t314_idusuario_promotor"].ToString());
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["Promotor"] != DBNull.Value)
                    o.Promotor = (string)dr["Promotor"];
                if (dr["numero_lb"] != DBNull.Value)
                    o.numero_lb = int.Parse(dr["numero_lb"].ToString());
                if (dr["count_lb"] != DBNull.Value)
                    o.count_lb = int.Parse(dr["count_lb"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de LINEABASE"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static DataSet ObtenerDesgloseLB(SqlTransaction tr, int t685_idlineabase, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASE_GETDESGLOSE", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASE_GETDESGLOSE", aParam);
        }

        public static DataSet ObtenerDesgloseLB_XLS(SqlTransaction tr, int t685_idlineabase, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASE_GETDESGLOSE_XLS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASE_GETDESGLOSE_XLS", aParam);
        }
        public static SqlDataReader ObtenerProyectosConLineaBase(int t314_idusuario,
                string t301_estado,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sModulo,
                bool bAdministrador
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado),
                ParametroSql.add("@t301_categoria", SqlDbType.Char, 1, t301_categoria),
                ParametroSql.add("@t305_cualidad", SqlDbType.Char, 1, t305_cualidad),
                ParametroSql.add("@sClientes", SqlDbType.VarChar, 8000, sClientes),
                ParametroSql.add("@sResponsables", SqlDbType.VarChar, 8000, sResponsables),
                ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas),
                ParametroSql.add("@sHorizontal", SqlDbType.VarChar, 8000, sHorizontal),
                ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura),
                ParametroSql.add("@sSectores", SqlDbType.VarChar, 8000, sSectores),
                ParametroSql.add("@sSegmentos", SqlDbType.VarChar, 8000, sSegmentos),
                ParametroSql.add("@bComparacionLogica", SqlDbType.Bit, 1, bComparacionLogica),
                ParametroSql.add("@sCNP", SqlDbType.VarChar, 8000, sCNP),
                ParametroSql.add("@sCSN1P", SqlDbType.VarChar, 8000, sCSN1P),
                ParametroSql.add("@sCSN2P", SqlDbType.VarChar, 8000, sCSN2P),
                ParametroSql.add("@sCSN3P", SqlDbType.VarChar, 8000, sCSN3P),
                ParametroSql.add("@sCSN4P", SqlDbType.VarChar, 8000, sCSN4P),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@sModulo", SqlDbType.Char, 3, sModulo),
                ParametroSql.add("@bAdministrador", SqlDbType.Bit, 1, bAdministrador)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_LINEABASE", aParam);
        }
        /// <summary>
        /// Falta agilizar el procedimiento SUP_GETPROYECTOS_LINEABASE_CTE
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t301_estado"></param>
        /// <param name="t301_categoria"></param>
        /// <param name="t305_cualidad"></param>
        /// <param name="sClientes"></param>
        /// <param name="sResponsables"></param>
        /// <param name="sNaturalezas"></param>
        /// <param name="sHorizontal"></param>
        /// <param name="sModeloContrato"></param>
        /// <param name="sContrato"></param>
        /// <param name="sIDEstructura"></param>
        /// <param name="sSectores"></param>
        /// <param name="sSegmentos"></param>
        /// <param name="bComparacionLogica"></param>
        /// <param name="sCNP"></param>
        /// <param name="sCSN1P"></param>
        /// <param name="sCSN2P"></param>
        /// <param name="sCSN3P"></param>
        /// <param name="sCSN4P"></param>
        /// <param name="sPSN"></param>
        /// <param name="sModulo"></param>
        /// <param name="bAdministrador"></param>
        /// <param name="anomes"></param>
        /// <param name="moneda"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerProyectosConLineaBaseAvan(int t314_idusuario,
                string t301_estado,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,
                string sResponsables,
                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,
                string sIDEstructura,
                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sModulo,
                bool bAdministrador,
                int anomes,
                string moneda
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado),
                ParametroSql.add("@t301_categoria", SqlDbType.Char, 1, t301_categoria),
                ParametroSql.add("@t305_cualidad", SqlDbType.Char, 1, t305_cualidad),
                ParametroSql.add("@sClientes", SqlDbType.VarChar, 8000, sClientes),
                ParametroSql.add("@sResponsables", SqlDbType.VarChar, 8000, sResponsables),
                ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas),
                ParametroSql.add("@sHorizontal", SqlDbType.VarChar, 8000, sHorizontal),
                ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura),
                ParametroSql.add("@sSectores", SqlDbType.VarChar, 8000, sSectores),
                ParametroSql.add("@sSegmentos", SqlDbType.VarChar, 8000, sSegmentos),
                ParametroSql.add("@bComparacionLogica", SqlDbType.Bit, 1, bComparacionLogica),
                ParametroSql.add("@sCNP", SqlDbType.VarChar, 8000, sCNP),
                ParametroSql.add("@sCSN1P", SqlDbType.VarChar, 8000, sCSN1P),
                ParametroSql.add("@sCSN2P", SqlDbType.VarChar, 8000, sCSN2P),
                ParametroSql.add("@sCSN3P", SqlDbType.VarChar, 8000, sCSN3P),
                ParametroSql.add("@sCSN4P", SqlDbType.VarChar, 8000, sCSN4P),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@sModulo", SqlDbType.Char, 3, sModulo),
                ParametroSql.add("@bAdministrador", SqlDbType.Bit, 1, bAdministrador),
                ParametroSql.add("@t325_anomes_referencia", SqlDbType.Int, 4, anomes),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, moneda)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_LINEABASE_AVAN", aParam);
        }

        public static DataSet ObtenerDS(SqlTransaction tr, int t685_idlineabase)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t685_idlineabase", SqlDbType.Int, 4, t685_idlineabase)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_LINEABASE_SEL", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_LINEABASE_SEL", aParam);
        }

        #endregion
    }

    public partial class CONSISTENCIALB
    {
        #region Metodos

        public static SqlDataReader ObtenerAnalisis(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_LINEABASE_GETANALISIS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_LINEABASE_GETANALISIS", aParam);
        }

        #endregion
    }
}
