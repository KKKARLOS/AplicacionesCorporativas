using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : INTERFACTSAP
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T445_INTERFACTSAP
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	02/10/2008 17:29:37	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class INTERFACTSAP
    {
        #region Propiedades y Atributos

        private int _t313_idempresa;
        public int t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private int _t302_idcliente;
        public int t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private int _t303_idnodo;
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private string _t445_grupo;
        public string t445_grupo
        {
            get { return _t445_grupo; }
            set { _t445_grupo = value; }
        }

        private string _t445_serie;
        public string t445_serie
        {
            get { return _t445_serie; }
            set { _t445_serie = value; }
        }

        private int _t445_numero;
        public int t445_numero
        {
            get { return _t445_numero; }
            set { _t445_numero = value; }
        }

        private DateTime _t445_fec_fact;
        public DateTime t445_fec_fact
        {
            get { return _t445_fec_fact; }
            set { _t445_fec_fact = value; }
        }

        private decimal _t445_imp_fact;
        public decimal t445_imp_fact
        {
            get { return _t445_imp_fact; }
            set { _t445_imp_fact = value; }
        }

        private string _t445_moneda;
        public string t445_moneda
        {
            get { return _t445_moneda; }
            set { _t445_moneda = value; }
        }

        private string _t445_descri;
        public string t445_descri
        {
            get { return _t445_descri; }
            set { _t445_descri = value; }
        }
        #endregion

        #region Constructor

        public INTERFACTSAP()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T445_INTERFACTSAP.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2008 17:29:37
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t313_idempresa, int t302_idcliente, int t305_idproyectosubnodo, 
                                    bool t445_grupo, string t445_serie, int t445_numero, DateTime t445_fec_fact, 
                                    decimal t445_imp_fact, string t445_moneda, string t445_descri, string t445_refcliente)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[0].Value = t313_idempresa;
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente;
            aParam[2] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[2].Value = t305_idproyectosubnodo;
            aParam[3] = new SqlParameter("@t445_grupo", SqlDbType.Bit, 1);
            aParam[3].Value = t445_grupo;
            aParam[4] = new SqlParameter("@t445_serie", SqlDbType.Text, 5);
            aParam[4].Value = t445_serie;
            aParam[5] = new SqlParameter("@t445_numero", SqlDbType.Int, 4);
            aParam[5].Value = t445_numero;
            aParam[6] = new SqlParameter("@t445_fec_fact", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t445_fec_fact;
            aParam[7] = new SqlParameter("@t445_imp_fact", SqlDbType.Money, 8);
            aParam[7].Value = t445_imp_fact;
            aParam[8] = new SqlParameter("@t445_moneda", SqlDbType.Text, 3);
            aParam[8].Value = t445_moneda;
            aParam[9] = new SqlParameter("@t445_descri", SqlDbType.Text, 40);
            aParam[9].Value = t445_descri;
            aParam[10] = new SqlParameter("@t445_refcliente", SqlDbType.Text, 20);
            aParam[10].Value = t445_refcliente;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_INTERFACTSAP_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_INTERFACTSAP_I", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T445_INTERFACTSAP a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/02/2009 11:58:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t445_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t445_id", SqlDbType.Int, 4);
            aParam[0].Value = t445_id;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_INTERFACTSAP_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_INTERFACTSAP_D", aParam);
        }


        #endregion
    }
}
