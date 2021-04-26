using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : PROCESO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T078_PROCESO
    /// </summary>
    /// <history>
    /// 	Creado por [DOPEOTCA]	23/05/2007 9:07:28	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PROCESO
    {

        #region Propiedades y Atributos
        #endregion

        #region Constructores

        public PROCESO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T078_PROCESO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t042_idarea, string t078_descripcion, short t078_orden)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[0].Value = t042_idarea;
            aParam[1] = new SqlParameter("@t078_descripcion", SqlDbType.VarChar, 50);
            aParam[1].Value = t078_descripcion;
            aParam[2] = new SqlParameter("@t078_orden", SqlDbType.SmallInt, 2);
            aParam[2].Value = t078_orden;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_PROCESO_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_PROCESO_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T078_PROCESO.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t078_idproceso, string t078_descripcion, short t078_orden)
        {

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t078_idproceso", SqlDbType.Int, 4);
            aParam[0].Value = t078_idproceso;
            aParam[1] = new SqlParameter("@t078_descripcion", SqlDbType.VarChar, 50);
            aParam[1].Value = t078_descripcion;
            aParam[2] = new SqlParameter("@t078_orden", SqlDbType.SmallInt, 250);
            aParam[2].Value = t078_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_PROCESO_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_PROCESO_U", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T078_PROCESO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t078_idproceso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t078_idproceso", SqlDbType.Int, 4);
            aParam[0].Value = t078_idproceso;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_PROCESO_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_PROCESO_D", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T078_PROCESO,
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select(SqlTransaction tr, int t078_idproceso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t078_idproceso", SqlDbType.Int, 4);
            aParam[0].Value = t078_idproceso;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_PROCESO_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_PROCESO_S", aParam);

            return dr;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T078_PROCESO.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t078_idproceso, Nullable<int> t042_idarea, string t078_descripcion, Nullable<short> t078_orden, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t078_idproceso", SqlDbType.Int, 4);
            aParam[0].Value = t078_idproceso;
            aParam[1] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[1].Value = t042_idarea;
            aParam[2] = new SqlParameter("@t078_descripcion", SqlDbType.VarChar, 50);
            aParam[2].Value = t078_descripcion;
            aParam[3] = new SqlParameter("@t078_orden", SqlDbType.SmallInt, 2);
            aParam[3].Value = t078_orden;
            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_PROCESO_C", aParam);

            return dr;
        }

        #endregion
    }
}
