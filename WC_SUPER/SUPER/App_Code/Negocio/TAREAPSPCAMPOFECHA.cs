using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : TAREAPSPCAMPOFECHA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T293_TAREAPSPCAMPOFECHA
    /// </summary>
    /// <history>
    /// 	Creado por [IBERMATICA]	16/11/2007 11:32:29	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class TAREAPSPCAMPOFECHA
    {

        #region Propiedades y Atributos

        private int _t332_idtarea;
        public int t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }

        private string _t290_idcampo;
        public string t290_idcampo
        {
            get { return _t290_idcampo; }
            set { _t290_idcampo = value; }
        }
        private DateTime? _t293_valor;
        public DateTime? t293_valor
        {
            get { return _t293_valor; }
            set { _t293_valor = value; }
        }
        #endregion

        #region Constructores

        public TAREAPSPCAMPOFECHA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T293_TAREAPSPCAMPOFECHA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t332_idtarea, int t290_idcampo, Nullable<DateTime> t293_valor, string t291_tipodato)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t293_valor", SqlDbType.DateTime, 3);
            aParam[2].Value = t293_valor;
            aParam[3] = new SqlParameter("@t291_tipodato", SqlDbType.Char, 1);
            aParam[3].Value = t291_tipodato;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TAREAPSPCAMPOFECHA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREAPSPCAMPOFECHA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T293_TAREAPSPCAMPOFECHA
        /// </summary>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t332_idtarea, int t290_idcampo, Nullable<DateTime> t293_valor, string t291_tipodato)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t293_valor", SqlDbType.DateTime, 3);
            aParam[2].Value = t293_valor;
            aParam[3] = new SqlParameter("@t291_tipodato", SqlDbType.Char, 1);
            aParam[3].Value = t291_tipodato;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSPCAMPOFECHA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPCAMPOFECHA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T292_TAREA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t332_idtarea, int t290_idcampo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSPCAMPOFECHA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPCAMPOFECHA_D", aParam);
        }

        #endregion
    }
}
