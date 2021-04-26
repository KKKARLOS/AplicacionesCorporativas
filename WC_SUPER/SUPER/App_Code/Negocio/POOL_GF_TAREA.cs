using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : POOL_GF_TAREA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T408_POOL_GF_TAREA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	27/12/2007 9:49:54	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class POOL_GF_TAREA
    {

        #region Propiedades y Atributos

        private int _t332_idtarea;
        public int t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }

        private int _t342_idgrupro;
        public int t342_idgrupro
        {
            get { return _t342_idgrupro; }
            set { _t342_idgrupro = value; }
        }
        #endregion

        #region Constructores

        public POOL_GF_TAREA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T408_POOL_GF_TAREA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	27/12/2007 9:49:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t332_idtarea, int t342_idgrupro)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[1].Value = t342_idgrupro;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("SUP_POOL_GF_TAREA_I_SNE", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_TAREA_I_SNE", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T408_POOL_GF_TAREA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	27/12/2007 9:49:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t332_idtarea, int t342_idgrupro)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[1].Value = t342_idgrupro;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("SUP_POOL_GF_TAREA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_TAREA_D", aParam);

            return returnValue;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de los grupos funcionales de la UNE marcando los de la tabla T408_POOL_GF_TAREA.
        /// menos los GF que vengan heredados del PT, de la Fase (si existe) y de la Actividad (si existe)
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	27/12/2007 9:49:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t331_idpt, int t334_idfase, int t335_idactividad, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[1].Value = t334_idfase;
            aParam[2] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[2].Value = t335_idactividad;
            aParam[3] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[3].Value = t332_idtarea;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_POOL_GF_TAREA_C", aParam);

            return dr;
        }
        /// <summary>
        /// 
        /// Duplica los datos de los grupos funcionales del Pool de la tarea origen a la destino.
        /// </summary>
        public static void DuplicarPoolGF(SqlTransaction tr, int nIdTareaAnt, int nIdTareaAct)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTareaAnt", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdTareaAct", SqlDbType.Int, 4);


            aParam[0].Value = nIdTareaAnt;
            aParam[1].Value = nIdTareaAct;

            int nResul = 0;
            if (tr == null)
                nResul = SqlHelper.ExecuteNonQuery("SUP_TAREA_POOL_GF_DUP", aParam);
            else
                nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_POOL_GF_DUP", aParam);
        }

        #endregion
    }
}
