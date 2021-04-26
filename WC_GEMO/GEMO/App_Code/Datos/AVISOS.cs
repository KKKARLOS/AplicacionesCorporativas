using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : AVISOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T715_AVISOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AVISOS
	{
        #region Propiedades y Atributos

        private short _t715_idaviso;
        public short t715_idaviso
        {
            get { return _t715_idaviso; }
            set { _t715_idaviso = value; }
        }

        private string _t715_denominacion;
        public string t715_denominacion
        {
            get { return _t715_denominacion; }
            set { _t715_denominacion = value; }
        }
        #endregion

        #region Constructor

        public AVISOS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T715_AVISOS.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t715_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t715_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t715_denominacion;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GEM_AVISOS_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_AVISOS_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T715_AVISOS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short t715_idaviso, string t715_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t715_idaviso", SqlDbType.SmallInt, 2);
            aParam[0].Value = t715_idaviso;
            aParam[1] = new SqlParameter("@t715_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t715_denominacion;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_AVISOS_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_AVISOS_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T715_AVISOS a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, short t715_idaviso)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t715_idaviso", SqlDbType.SmallInt, 2);
            aParam[0].Value = t715_idaviso;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_AVISOS_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_AVISOS_D", aParam);
        }

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_AVISOS_C", aParam);
        }
		#endregion
	}
}
