using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de PTCampoFecha
    /// </summary>
    public partial class PTCampoFecha
    {
        #region Propiedades y Atributos

        private int _t331_idpt;
        public int t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        private string _t290_idcampo;
        public string t290_idcampo
        {
            get { return _t290_idcampo; }
            set { _t290_idcampo = value; }
        }
        private DateTime? _t728_valor;
        public DateTime? t728_valor
        {
            get { return _t728_valor; }
            set { _t728_valor = value; }
        }
        #endregion
        #region Constructores
        public PTCampoFecha()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T728_PTCAMPOFECHA 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t331_idpt, int t290_idcampo, Nullable<DateTime> t728_valor, string t291_tipodato)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t728_valor", SqlDbType.DateTime, 3);
            aParam[2].Value = t728_valor;
            aParam[3] = new SqlParameter("@t291_tipodato", SqlDbType.Char, 1);
            aParam[3].Value = t291_tipodato;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PTCAMPOFECHA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PTCAMPOFECHA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T728_PTCAMPOFECHA 
        /// </summary>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t331_idpt, int t290_idcampo, Nullable<DateTime> t728_valor, string t291_tipodato)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t728_valor", SqlDbType.DateTime, 3);
            aParam[2].Value = t728_valor;
            aParam[3] = new SqlParameter("@t291_tipodato", SqlDbType.Char, 1);
            aParam[3].Value = t291_tipodato;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PTCAMPOFECHA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PTCAMPOFECHA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T728_PTCAMPOFECHA  a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t331_idpt, int t290_idcampo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PTCAMPOFECHA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PTCAMPOFECHA_D", aParam);
        }

        #endregion
    }
}