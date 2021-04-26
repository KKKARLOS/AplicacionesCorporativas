using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : TAREA_USUARIO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T047_TAREA_USUARIO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	05/09/2007 14:29:03	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class TAREA_USUARIO
    {
        #region Constructores

        public TAREA_USUARIO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T047_TAREA_USUARIO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	05/09/2007 14:29:03
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int T072_IDTAREA, int T001_IDFICEPI)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;
            aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[1].Value = T001_IDFICEPI;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_TAREA_USUARIO_I", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_TAREA_USUARIO_I", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T047_TAREA_USUARIO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/09/2007 14:29:03
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int T072_IDTAREA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_TAREA_USUARIO_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_TAREA_USUARIO_D", aParam);

            return returnValue;
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T047_TAREA_USUARIO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/09/2007 14:29:03
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> T072_IDTAREA, int T001_IDFICEPI, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;
            aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[1].Value = T001_IDFICEPI;
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[2].Value = nOrden;
            aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[3].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_TAREA_USUARIO_C", aParam);

            return dr;
        }

        #endregion
    }
}
