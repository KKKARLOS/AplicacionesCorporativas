using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : VGNOENVIAR
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T672_VGNOENVIAR que contiene los profesionales a los que no queremos que les
    /// llegue correo en el proceso de envío de los correos de Visibilidad del Gasto
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	24/06/2011 10:17:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class VGNOENVIAR
    {
        #region Propiedades y Atributos

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        #endregion

        #region Constructor

        public VGNOENVIAR()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T672_VGNOENVIAR.
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("PVG_VGNOENVIAR_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "PVG_VGNOENVIAR_I", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T672_VGNOENVIAR a traves de la primary key.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("PVG_VGNOENVIAR_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "PVG_VGNOENVIAR_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T672_VGNOENVIAR.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("PVG_VGNOENVIAR_CAT", aParam);
        }

        #endregion
    }
}
