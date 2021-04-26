using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de PTCampoImporte
    /// </summary>
    public class PTCampoImporte
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
        private decimal? _t727_valor;
        public decimal? t727_valor
        {
            get { return _t727_valor; }
            set { _t727_valor = value; }
        }
        #endregion
        #region Constructores
        public PTCampoImporte()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T727_PTCAMPOIMPORTE
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t331_idpt, int t290_idcampo, Nullable<decimal> t727_valor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t727_valor", SqlDbType.Money, 8);
            aParam[2].Value = t727_valor;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PTCAMPOIMPORTE_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PTCAMPOIMPORTE_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T727_PTCAMPOIMPORTE
        /// </summary>
        /// <history>
        /// 	Creado por [IBERMATICA]	29/04/2015 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t331_idpt, int t290_idcampo, Nullable<decimal> t727_valor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = t290_idcampo;
            aParam[2] = new SqlParameter("@t727_valor", SqlDbType.Money, 8);
            aParam[2].Value = t727_valor;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PTCAMPOIMPORTE_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PTCAMPOIMPORTE_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T727_PTCAMPOIMPORTE a traves de la primary key.
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
                return SqlHelper.ExecuteNonQuery("SUP_PTCAMPOIMPORTE_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PTCAMPOIMPORTE_D", aParam);
        }

        #endregion
    }
}