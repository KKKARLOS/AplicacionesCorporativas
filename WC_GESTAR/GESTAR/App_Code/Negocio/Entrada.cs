using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : ENTRADA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T074_ENTRADA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	03/08/2007 13:34:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ENTRADA
    {

        #region Propiedades y Atributos

  
        #endregion


        #region Constructores

        public ENTRADA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T074_ENTRADA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/08/2007 13:34:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string T074_DENOMINACION, Nullable<short> T075_ORIGEN, int T042_IDAREA, string T074_COMUNICANTE, string T074_MEDIO, string T074_ORGANIZACION, string T074_DESCRIPCION, Nullable<int> T074_ANALISTA, Nullable<DateTime> T074_FECHAANAL, string T074_NOTAS, Nullable<int> T074_CREADOR, short T074_ORDEN)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@T074_DENOMINACION", SqlDbType.Text, 100);
            aParam[0].Value = T074_DENOMINACION;
            aParam[1] = new SqlParameter("@T075_ORIGEN", SqlDbType.SmallInt, 2);
            aParam[1].Value = T075_ORIGEN;
            aParam[2] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[2].Value = T042_IDAREA;
            aParam[3] = new SqlParameter("@T074_COMUNICANTE", SqlDbType.Text, 63);
            aParam[3].Value = T074_COMUNICANTE;
            aParam[4] = new SqlParameter("@T074_MEDIO", SqlDbType.Text, 100);
            aParam[4].Value = T074_MEDIO;
            aParam[5] = new SqlParameter("@T074_ORGANIZACION", SqlDbType.Text, 100);
            aParam[5].Value = T074_ORGANIZACION;
            aParam[6] = new SqlParameter("@T074_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[6].Value = T074_DESCRIPCION;
            aParam[7] = new SqlParameter("@T074_ANALISTA", SqlDbType.Int, 4);
            aParam[7].Value = T074_ANALISTA;
            aParam[8] = new SqlParameter("@T074_FECHAANAL", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = T074_FECHAANAL;
            aParam[9] = new SqlParameter("@T074_NOTAS", SqlDbType.Text, 2147483647);
            aParam[9].Value = T074_NOTAS;
            aParam[10] = new SqlParameter("@T074_CREADOR", SqlDbType.Int, 4);
            aParam[10].Value = T074_CREADOR;
            aParam[11] = new SqlParameter("@T074_ORDEN", SqlDbType.SmallInt, 2);
            aParam[11].Value = T074_ORDEN;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_ENTRADA_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_ENTRADA_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T074_ENTRADA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/08/2007 13:34:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short T074_IDENTRADA, string T074_DENOMINACION, Nullable<short> T075_ORIGEN, int T042_IDAREA, string T074_COMUNICANTE, string T074_MEDIO, string T074_ORGANIZACION, string T074_DESCRIPCION, Nullable<int> T074_ANALISTA, Nullable<DateTime> T074_FECHAANAL, string T074_NOTAS, Nullable<int> T074_CREADOR, short T074_ORDEN)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@T074_IDENTRADA", SqlDbType.SmallInt, 2);
            aParam[0].Value = T074_IDENTRADA;
            aParam[1] = new SqlParameter("@T074_DENOMINACION", SqlDbType.Text, 100);
            aParam[1].Value = T074_DENOMINACION;
            aParam[2] = new SqlParameter("@T075_ORIGEN", SqlDbType.SmallInt, 2);
            aParam[2].Value = T075_ORIGEN;
            aParam[3] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[3].Value = T042_IDAREA;
            aParam[4] = new SqlParameter("@T074_COMUNICANTE", SqlDbType.Text, 63);
            aParam[4].Value = T074_COMUNICANTE;
            aParam[5] = new SqlParameter("@T074_MEDIO", SqlDbType.Text, 100);
            aParam[5].Value = T074_MEDIO;
            aParam[6] = new SqlParameter("@T074_ORGANIZACION", SqlDbType.Text, 100);
            aParam[6].Value = T074_ORGANIZACION;
            aParam[7] = new SqlParameter("@T074_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[7].Value = T074_DESCRIPCION;
            aParam[8] = new SqlParameter("@T074_ANALISTA", SqlDbType.Int, 4);
            aParam[8].Value = T074_ANALISTA;
            aParam[9] = new SqlParameter("@T074_FECHAANAL", SqlDbType.SmallDateTime, 4);
            aParam[9].Value = T074_FECHAANAL;
            aParam[10] = new SqlParameter("@T074_NOTAS", SqlDbType.Text, 2147483647);
            aParam[10].Value = T074_NOTAS;
            aParam[11] = new SqlParameter("@T074_CREADOR", SqlDbType.Int, 4);
            aParam[11].Value = T074_CREADOR;
            aParam[12] = new SqlParameter("@T074_ORDEN", SqlDbType.SmallInt, 2);
            aParam[12].Value = T074_ORDEN;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_ENTRADA_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_ENTRADA_U", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T074_ENTRADA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/08/2007 13:34:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, short T074_IDENTRADA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T074_IDENTRADA", SqlDbType.SmallInt, 2);
            aParam[0].Value = T074_IDENTRADA;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_ENTRADA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_ENTRADA_D", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T074_ENTRADA,
        /// y devuelve una instancia u objeto del tipo ENTRADA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/08/2007 13:34:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select(SqlTransaction tr, short T074_IDENTRADA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T074_IDENTRADA", SqlDbType.SmallInt, 2);
            aParam[0].Value = T074_IDENTRADA;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_ENTRADA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_ENTRADA_S", aParam);

            return dr;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T074_ENTRADA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/08/2007 13:34:06
        /// </history>
        /// -----------------------------------------------------------------------------        
        public static SqlDataReader Catalogo(Nullable<int> T042_IDAREA, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[0].Value = T042_IDAREA;
            aParam[1] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[1].Value = nOrden;
            aParam[2] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[2].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_ENTRADA_C_OLD", aParam);

            return dr;
        }
       /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T077_ALCANCE.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<short> t074_identrada, Nullable<int> t042_idarea, string t074_denominacion, Nullable<short> t074_orden, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t074_identrada", SqlDbType.Int, 4);
            aParam[0].Value = t074_identrada;
            aParam[1] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[1].Value = t042_idarea;
            aParam[2] = new SqlParameter("@t074_denominacion", SqlDbType.VarChar, 100);
            aParam[2].Value = t074_denominacion;
            aParam[3] = new SqlParameter("@t074_orden", SqlDbType.SmallInt, 2);
            aParam[3].Value = t074_orden;
            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_ENTRADA_C", aParam);

            return dr;
        }

        #endregion
    }
}
