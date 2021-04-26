using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class PSNALERTAS
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T830_PSNALERTAS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	23/07/2012 12:36:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t305_idproyectosubnodo, byte t820_idalerta, bool t830_habilitada, Nullable<int> t830_inistandby, Nullable<int> t830_finstandby, string t830_txtseguimiento)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t830_habilitada", SqlDbType.Bit, 1, t830_habilitada);
            aParam[i++] = ParametroSql.add("@t830_inistandby", SqlDbType.Int, 4, t830_inistandby);
            aParam[i++] = ParametroSql.add("@t830_finstandby", SqlDbType.Int, 4, t830_finstandby);
            aParam[i++] = ParametroSql.add("@t830_txtseguimiento", SqlDbType.Text, 2147483647, t830_txtseguimiento);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PSNALERTAS_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PSNALERTAS_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T830_PSNALERTAS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	23/07/2012 12:36:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataSet CatalogoByDialogo(int t831_iddialogoalerta, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bAdmin", SqlDbType.Bit, 1, SUPER.Capa_Negocio.Utilidades.EsAdminProduccion());

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteDataset("SUP_PSNALERTAS_CATByDialogo", aParam);
        }

        public static void EstablecerAlertaEstructura(SqlTransaction tr, int nNivel, int nCodigo, byte nAlerta, bool bHabilitada)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nNivel", SqlDbType.Int, 4, nNivel);
            aParam[i++] = ParametroSql.add("@nCodigo", SqlDbType.Int, 4, nCodigo);
            aParam[i++] = ParametroSql.add("@nAlerta", SqlDbType.TinyInt, 1, nAlerta);
            aParam[i++] = ParametroSql.add("@bHabilitada", SqlDbType.Bit, 2, bHabilitada);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETALERTA_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETALERTA_UPD", aParam);
        }
        public static void TrasladarAlertaEstructura(SqlTransaction tr, byte nOpcion, byte nNivel, int nCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nOpcion", SqlDbType.TinyInt, 1, nOpcion);
            aParam[i++] = ParametroSql.add("@nNivel", SqlDbType.TinyInt, 1, nNivel);
            aParam[i++] = ParametroSql.add("@nCodigo", SqlDbType.Int, 4, nCodigo);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETALERTA_ESTRUCTURA", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETALERTA_ESTRUCTURA", aParam);
        }
        public static void EstablecerAlertaDetalleProyecto(SqlTransaction tr, int t305_idproyectosubnodo, byte t820_idalerta,
            bool bActivar, Nullable<int> t830_inistandby, Nullable<int> t830_finstandby, string t830_txtseguimiento)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@bActivar", SqlDbType.Bit, 1, bActivar);
            aParam[i++] = ParametroSql.add("@t830_inistandby", SqlDbType.Int, 4, t830_inistandby);
            aParam[i++] = ParametroSql.add("@t830_finstandby", SqlDbType.Int, 4, t830_finstandby);
            aParam[i++] = ParametroSql.add("@t830_txtseguimiento", SqlDbType.Text, 16, t830_txtseguimiento);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETALERTA_PROYECTO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETALERTA_PROYECTO", aParam);
        }
        public static void EstablecerAlertaProyectosubnodo(SqlTransaction tr, string sDatosAlertas)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sDatosAlertas", SqlDbType.VarChar, 8000, sDatosAlertas);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETALERTA_PSN", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETALERTA_PSN", aParam);
        }
        
        public static SqlDataReader Catalogo(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PSNALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSNALERTAS_CAT", aParam);
        }

        public static SqlDataReader ObtenerAlertasMiGestion(
                    SqlTransaction tr,
                    Nullable<int> t305_idproyectosubnodo,
                    string t301_estado,
                    Nullable<int> t303_idnodo,
                    Nullable<int> t302_idcliente,
                    Nullable<int> t001_idficepi_interlocutor,
                    Nullable<bool> t830_habilitada,
                    Nullable<byte> t820_idalerta,
                    Nullable<int> t314_idusuario_gestor,
                    bool bStandBy,
                    bool bSeguimiento,
                    Nullable<int> t314_idusuario_responsable,
                    Nullable<byte> t821_idgrupoalerta
                    )
        {
            SqlParameter[] aParam = new SqlParameter[12];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t001_idficepi_interlocutor", SqlDbType.Int, 4, t001_idficepi_interlocutor);
            aParam[i++] = ParametroSql.add("@t830_habilitada", SqlDbType.Bit, 1, t830_habilitada);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario_gestor", SqlDbType.Int, 4, t314_idusuario_gestor);
            aParam[i++] = ParametroSql.add("@bStandBy", SqlDbType.Bit, 1, bStandBy);
            aParam[i++] = ParametroSql.add("@bSeguimiento", SqlDbType.Bit, 1, bSeguimiento);
            aParam[i++] = ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable);
            aParam[i++] = ParametroSql.add("@t821_idgrupoalerta", SqlDbType.TinyInt, 1, t821_idgrupoalerta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PSNALERTAS_CATGESTION", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSNALERTAS_CATGESTION", aParam);
        }
        /// <summary>
        /// Obtiene el motivo del último dialogo cerrado, con motivo de cierre asociados al proyecto y tipo de alerta que se pasa por parámetro
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <param name="t820_idalerta"></param>
        /// <returns></returns>
        public static SqlDataReader MotivoCierreDefecto(SqlTransaction tr, int t305_idproyectosubnodo, int t820_idalerta)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_MOTIVODEFECTO_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_MOTIVODEFECTO_S", aParam);
        }

    }

}