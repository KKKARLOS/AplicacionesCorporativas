using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIO
	/// 
	/// -----------------------------------------------------------------------------
	public partial class USUARIO
	{
		#region Metodos

        public static SqlDataReader ObtenerAccionesPendientes(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCIONESPENDIENTES_TEMP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCIONESPENDIENTES_TEMP", aParam);
        }

        public static SqlDataReader ObtenerAccionesPendientesV2(SqlTransaction tr, Nullable<int> t314_idusuario, Nullable<int> t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t001_idficepi_cvt", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCIONESPENDIENTES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCIONESPENDIENTES", aParam);
        }
        /// <summary>
        /// Actualiza la password de un usuario para el acceso a servicios de recuperación de información
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t314_password"></param>
        /// <returns></returns>
        public static void GrabarPasswServicio(int t314_idusuario, string t314_password)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t314_password", SqlDbType.VarChar, 15, t314_password)
            };
            SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_PASSW", aParam);
        }
        public static SqlDataReader GetDatos(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_S", aParam);
        }
        /// <summary>
        /// Borra un registro de la tabla T439_CORREOS
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t439_tipo"></param>
        /// <param name="t439_accion"></param>
        /// <param name="?"></param>
        public static void BorrarCorreo(SqlTransaction tr, int t314_idusuario, byte t439_tipo, bool t439_accion, int t439_clave1)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t439_tipo", SqlDbType.TinyInt, 1, t439_tipo),
                ParametroSql.add("@t439_accion", SqlDbType.Bit, 1, t439_accion),
                ParametroSql.add("@t439_clave1", SqlDbType.Int, 4, t439_clave1)
            };
            if (tr==null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIO_CORREO_D", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_CORREO_D", aParam);
        }

        /// <summary>
        /// Obtiene un catálogo con tres registros
        ///     IAP y fecha mímima de consumos IAP
        ///     ECO y último día del mínimo mes de consumos económicos
        ///     PRY y fecha mínima de asiganción a proyectos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static SqlDataReader GetMinimoFechaAlta(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_MIN_ALTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIO_MIN_ALTA", aParam);
        }
        #endregion
	}
}
