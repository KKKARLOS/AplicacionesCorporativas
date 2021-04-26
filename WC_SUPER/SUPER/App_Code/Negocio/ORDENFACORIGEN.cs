using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class ORDENFACORIGEN
    {
        #region Propiedades y Atributos complementarios

        private string _des_condicionpago;
        public string des_condicionpago
        {
            get { return _des_condicionpago; }
            set { _des_condicionpago = value; }
        }

        private string _des_viapago;
        public string des_viapago
        {
            get { return _des_viapago; }
            set { _des_viapago = value; }
        }

        private string _des_moneda;
        public string des_moneda
        {
            get { return _des_moneda; }
            set { _des_moneda = value; }
        }

        private string _NifSolicitante;
        public string NifSolicitante
        {
            get { return _NifSolicitante; }
            set { _NifSolicitante = value; }
        }

        //private string _t302_denominacion_solici;
        //public string t302_denominacion_solici
        //{
        //    get { return _t302_denominacion_solici; }
        //    set { _t302_denominacion_solici = value; }
        //}

        private string _NifRespPago;
        public string NifRespPago
        {
            get { return _NifRespPago; }
            set { _NifRespPago = value; }
        }

        private string _t302_denominacion_respago;
        public string t302_denominacion_respago
        {
            get { return _t302_denominacion_respago; }
            set { _t302_denominacion_respago = value; }
        }

        private string _NifDestFra;
        public string NifDestFra
        {
            get { return _NifDestFra; }
            set { _NifDestFra = value; }
        }

        private string _t302_denominacion_destfact;
        public string t302_denominacion_destfact
        {
            get { return _t302_denominacion_destfact; }
            set { _t302_denominacion_destfact = value; }
        }

        private string _des_ovsap;
        public string des_ovsap
        {
            get { return _des_ovsap; }
            set { _des_ovsap = value; }
        }

        private string _t622_denominacion;
        public string t622_denominacion
        {
            get { return _t622_denominacion; }
            set { _t622_denominacion = value; }
        }
        private string _direccion;
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }
        private string _t301_denominacion;
        public string t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }

        private string _t610_textocabecera;
        public string t610_textocabecera
        {
            get { return _t610_textocabecera; }
            set { _t610_textocabecera = value; }
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T619_ORDENFACORIGEN,
        /// y devuelve una instancia u objeto del tipo ORDENFACORIGEN
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/11/2010 12:09:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static ORDENFACORIGEN Select(SqlTransaction tr, int t619_idordenfac)
        {
            ORDENFACORIGEN o = new ORDENFACORIGEN();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t619_idordenfac", SqlDbType.Int, 4);
            aParam[1].Value = t619_idordenfac;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_ORDENFACORIGEN_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDENFACORIGEN_O", aParam);

            if (dr.Read())
            {
                if (dr["t619_idordenfac"] != DBNull.Value)
                    o.t619_idordenfac = int.Parse(dr["t619_idordenfac"].ToString());
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t302_idcliente_solici"] != DBNull.Value)
                    o.t302_idcliente_solici = int.Parse(dr["t302_idcliente_solici"].ToString());
                if (dr["t302_idcliente_respago"] != DBNull.Value)
                    o.t302_idcliente_respago = int.Parse(dr["t302_idcliente_respago"].ToString());
                if (dr["t302_idcliente_destfact"] != DBNull.Value)
                    o.t302_idcliente_destfact = int.Parse(dr["t302_idcliente_destfact"].ToString());
                if (dr["t619_condicionpago"] != DBNull.Value)
                    o.t619_condicionpago = (string)dr["t619_condicionpago"];
                if (dr["t619_viapago"] != DBNull.Value)
                    o.t619_viapago = (string)dr["t619_viapago"];
                if (dr["t619_refcliente"] != DBNull.Value)
                    o.t619_refcliente = (string)dr["t619_refcliente"];
                if (dr["t619_fprevemifact"] != DBNull.Value)
                    o.t619_fprevemifact = (DateTime)dr["t619_fprevemifact"];
                if (dr["t619_moneda"] != DBNull.Value)
                    o.t619_moneda = (string)dr["t619_moneda"];
                if (dr["t619_idagrupacion"] != DBNull.Value)
                    o.t619_idagrupacion = int.Parse(dr["t619_idagrupacion"].ToString());
                if (dr["t619_observacionespool"] != DBNull.Value)
                    o.t619_observacionespool = (string)dr["t619_observacionespool"];
                if (dr["t619_comentario"] != DBNull.Value)
                    o.t619_comentario = (string)dr["t619_comentario"];
                if (dr["t621_idovsap"] != DBNull.Value)
                    o.t621_idovsap = (string)dr["t621_idovsap"];
                if (dr["t619_dto_porcen"] != DBNull.Value)
                    o.t619_dto_porcen = float.Parse(dr["t619_dto_porcen"].ToString());
                if (dr["t619_dto_importe"] != DBNull.Value)
                    o.t619_dto_importe = decimal.Parse(dr["t619_dto_importe"].ToString());
                if (dr["t619_fdiferida"] != DBNull.Value)
                    o.t619_fdiferida = (DateTime)dr["t619_fdiferida"];
                if (dr["des_condicionpago"] != DBNull.Value)
                    o.des_condicionpago = (string)dr["des_condicionpago"];
                if (dr["des_viapago"] != DBNull.Value)
                    o.des_viapago = (string)dr["des_viapago"];
                if (dr["des_moneda"] != DBNull.Value)
                    o.des_moneda = (string)dr["des_moneda"];
                //if (dr["NifSolicitante"] != DBNull.Value)
                //    o.NifSolicitante = (string)dr["NifSolicitante"];
                //if (dr["t302_denominacion_solici"] != DBNull.Value)
                //    o.t302_denominacion_solici = (string)dr["t302_denominacion_solici"];
                if (dr["NifRespPago"] != DBNull.Value)
                    o.NifRespPago = (string)dr["NifRespPago"];
                if (dr["t302_denominacion_respago"] != DBNull.Value)
                    o.t302_denominacion_respago = (string)dr["t302_denominacion_respago"];
                if (dr["NifDestFra"] != DBNull.Value)
                    o.NifDestFra = (string)dr["NifDestFra"];
                if (dr["t302_denominacion_destfact"] != DBNull.Value)
                    o.t302_denominacion_destfact = (string)dr["t302_denominacion_destfact"];
                if (dr["des_ovsap"] != DBNull.Value)
                    o.des_ovsap = (string)dr["des_ovsap"];
                if (dr["t622_denominacion"] != DBNull.Value)
                    o.t622_denominacion = (string)dr["t622_denominacion"];
                if (dr["t314_idusuario_respcomercial"] != DBNull.Value)
                    o.t314_idusuario_respcomercial = int.Parse(dr["t314_idusuario_respcomercial"].ToString());
                if (dr["direccion"] != DBNull.Value)
                    o.direccion = (string)dr["direccion"];
                if (dr["t619_ivaincluido"] != DBNull.Value)
                    o.t619_ivaincluido = (bool)dr["t619_ivaincluido"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = (int)dr["t301_idproyecto"];
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t610_textocabecera"] != DBNull.Value)
                    o.t610_textocabecera = (string)dr["t610_textocabecera"];
                if (dr["t619_infotramit"] != DBNull.Value)
                    o.t619_infotramit = (string)dr["t619_infotramit"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de la orden original"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        #endregion
    }
}
