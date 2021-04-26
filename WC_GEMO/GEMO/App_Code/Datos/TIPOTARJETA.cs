using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : TARIFADATOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T711_TARIFADATOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TIPOTARJETA
	{
        #region Propiedades y Atributos

        private short _t712_idtarjeta;
        public short t712_idtarjeta
        {
            get { return _t712_idtarjeta; }
            set { _t712_idtarjeta = value; }
        }

        private string _t712_denominacion;
        public string t712_denominacion
        {
            get { return _t712_denominacion; }
            set { _t712_denominacion = value; }
        }
        #endregion

        #region Constructor

        public TIPOTARJETA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T712_TIPOTARJETA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t712_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t712_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t712_denominacion;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GEM_TIPOTARJETA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_TIPOTARJETA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T712_TIPOTARJETA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short t712_idtarjeta, string t712_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t712_idtarjeta", SqlDbType.SmallInt, 2);
            aParam[0].Value = t712_idtarjeta;
            aParam[1] = new SqlParameter("@t712_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t712_denominacion;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_TIPOTARJETA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_TIPOTARJETA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T712_TIPOTARJETA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:02:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, short t712_idtarjeta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t712_idtarjeta", SqlDbType.SmallInt, 2);
            aParam[0].Value = t712_idtarjeta;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_TIPOTARJETA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_TIPOTARJETA_D", aParam);
        }

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_TIPOTARJETA_C", aParam);
        }
		#endregion
	}
}
