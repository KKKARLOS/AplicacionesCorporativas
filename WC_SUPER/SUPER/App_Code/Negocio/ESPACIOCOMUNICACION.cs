using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : ESPACIOCOMUNICACION
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T639_ESPACIOCOMUNICACION
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	26/02/2008 11:08:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ESPACIOCOMUNICACION
    {
        #region Metodos


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T639_ESPACIOCOMUNICACION.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	05/01/2011 13:04:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t301_idproyecto, Nullable<DateTime> t639_fechacom, int t314_idusuario, bool t639_consumo, 
                                 bool t639_produccion, bool t639_facturacion, bool t639_otros, string t639_descripcion, 
                                 bool t639_vigenciaproyecto, Nullable<int> t639_vigenciadesde, Nullable<int> t639_vigenciahasta,
                                 string t639_observaciones, string t658_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t639_fechacom", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t639_fechacom;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;
            aParam[3] = new SqlParameter("@t639_consumo", SqlDbType.Bit, 1);
            aParam[3].Value = t639_consumo;
            aParam[4] = new SqlParameter("@t639_produccion", SqlDbType.Bit, 1);
            aParam[4].Value = t639_produccion;
            aParam[5] = new SqlParameter("@t639_facturacion", SqlDbType.Bit, 1);
            aParam[5].Value = t639_facturacion;
            aParam[6] = new SqlParameter("@t639_otros", SqlDbType.Bit, 1);
            aParam[6].Value = t639_otros;
            aParam[7] = new SqlParameter("@t639_descripcion", SqlDbType.Text, 2147483647);
            aParam[7].Value = t639_descripcion;
            aParam[8] = new SqlParameter("@t639_vigenciaproyecto", SqlDbType.Bit, 1);
            aParam[8].Value = t639_vigenciaproyecto;
            aParam[9] = new SqlParameter("@t639_vigenciadesde", SqlDbType.Int, 4);
            aParam[9].Value = t639_vigenciadesde;
            aParam[10] = new SqlParameter("@t639_vigenciahasta", SqlDbType.Int, 4);
            aParam[10].Value = t639_vigenciahasta;
            aParam[11] = new SqlParameter("@t639_observaciones", SqlDbType.Text, 2147483647);
            aParam[11].Value = t639_observaciones;
            aParam[12] = new SqlParameter("@t658_usuticks", SqlDbType.VarChar, 50);
            aParam[12].Value = t658_usuticks;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESPACIOCOMUNICACION_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESPACIOCOMUNICACION_INS", aParam));
        }
        public static int Update(SqlTransaction tr, int t639_idcomunicacion, string t639_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
            aParam[0].Value = t639_idcomunicacion;
            aParam[1] = new SqlParameter("@t639_observaciones", SqlDbType.Text, 2147483647);
            aParam[1].Value = t639_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ESPACIOCOMUNICACION_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOCOMUNICACION_U2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T639_ESPACIOCOMUNICACION.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/01/2011 13:04:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ESPACIOCOMUNICACION_C2", aParam);
        }

        #endregion
    }
}
