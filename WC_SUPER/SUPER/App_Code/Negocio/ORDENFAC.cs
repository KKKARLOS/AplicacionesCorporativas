using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
	public partial class ORDENFAC
    {
        #region Propiedades y Atributos complementarios

        private string _t302_denominacion_respago;
        public string t302_denominacion_respago
        {
            get { return _t302_denominacion_respago; }
            set { _t302_denominacion_respago = value; }
        }
        private string _t302_denominacion_destfact;
        public string t302_denominacion_destfact
        {
            get { return _t302_denominacion_destfact; }
            set { _t302_denominacion_destfact = value; }
        }
        /*private string _NifSolicitante;
        public string NifSolicitante
        {
            get { return _NifSolicitante; }
            set { _NifSolicitante = value; }
        }*/
        private string _NifRespPago;
        public string NifRespPago
        {
            get { return _NifRespPago; }
            set { _NifRespPago = value; }
        }
        private string _NifDestFra;
        public string NifDestFra
        {
            get { return _NifDestFra; }
            set { _NifDestFra = value; }
        }

        private string _t622_denominacion;
        public string t622_denominacion
        {
            get { return _t622_denominacion; }
            set { _t622_denominacion = value; }
        }

        private string _des_estado;
        public string des_estado
        {
            get { return _des_estado; }
            set { _des_estado = value; }
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

        private string _t302_codigoexterno;
        public string t302_codigoexterno
        {
            get { return _t302_codigoexterno; }
            set { _t302_codigoexterno = value; }
        }
        public bool t302_efactur { get; set; }

        #endregion

        #region Metodos
        public static int Insert(SqlTransaction tr, int t305_idproyectosubnodo, Nullable<int> t302_idcliente_solici, 
                                 Nullable<int> t302_idcliente_respago, Nullable<int> t302_idcliente_destfact, 
                                 string t610_condicionpago, string t610_viapago, string t610_refcliente, 
                                 Nullable<DateTime> t610_fprevemifact, string t610_moneda, Nullable<int> t622_idagrupacion, 
                                 string t610_observacionespool, string t610_comentario, string t610_dvsap, 
                                 string t621_idovsap, float t610_dto_porcen, decimal t610_dto_importe, 
                                 Nullable<DateTime> t610_fdiferida, int t314_idusuario_respcomercial, string t624_usuticks, 
                                 bool t610_ivaincluido, string t610_textocabecera, int t314_idusuario, string t610_infotramit)
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t302_idcliente_solici", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente_solici;
            aParam[2] = new SqlParameter("@t302_idcliente_respago", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente_respago;
            aParam[3] = new SqlParameter("@t302_idcliente_destfact", SqlDbType.Int, 4);
            aParam[3].Value = t302_idcliente_destfact;
            aParam[4] = new SqlParameter("@t610_condicionpago", SqlDbType.Text, 4);
            aParam[4].Value = t610_condicionpago;
            aParam[5] = new SqlParameter("@t610_viapago", SqlDbType.Text, 1);
            aParam[5].Value = t610_viapago;
            aParam[6] = new SqlParameter("@t610_refcliente", SqlDbType.Text, 35);
            aParam[6].Value = t610_refcliente;
            aParam[7] = new SqlParameter("@t610_fprevemifact", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = t610_fprevemifact;
            aParam[8] = new SqlParameter("@t610_moneda", SqlDbType.Text, 5);
            aParam[8].Value = t610_moneda;
            aParam[9] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
            aParam[9].Value = t622_idagrupacion;
            aParam[10] = new SqlParameter("@t610_observacionespool", SqlDbType.Text, 2147483647);
            aParam[10].Value = t610_observacionespool;
            aParam[11] = new SqlParameter("@t610_comentario", SqlDbType.Text, 2147483647);
            aParam[11].Value = t610_comentario;
            aParam[12] = new SqlParameter("@t610_dvsap", SqlDbType.Text, 10);
            aParam[12].Value = t610_dvsap;
            aParam[13] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
            aParam[13].Value = t621_idovsap;
            aParam[14] = new SqlParameter("@t610_dto_porcen", SqlDbType.Real, 4);
            aParam[14].Value = t610_dto_porcen;
            aParam[15] = new SqlParameter("@t610_dto_importe", SqlDbType.Money, 8);
            aParam[15].Value = t610_dto_importe;
            aParam[16] = new SqlParameter("@t610_fdiferida", SqlDbType.SmallDateTime, 4);
            aParam[16].Value = t610_fdiferida;
            aParam[17] = new SqlParameter("@t314_idusuario_respcomercial", SqlDbType.Int, 4);
            aParam[17].Value = t314_idusuario_respcomercial;
            aParam[18] = new SqlParameter("@t624_usuticks", SqlDbType.VarChar, 50);
            aParam[18].Value = t624_usuticks;
            aParam[19] = new SqlParameter("@t610_ivaincluido", SqlDbType.Bit, 1);
            aParam[19].Value = t610_ivaincluido;
            aParam[20] = new SqlParameter("@t610_textocabecera", SqlDbType.Text, 2147483647);
            aParam[20].Value = t610_textocabecera;
            aParam[21] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[21].Value = t314_idusuario;
            aParam[22] = new SqlParameter("@t610_infotramit", SqlDbType.Text, 150);
            aParam[22].Value = t610_infotramit;
            
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ORDENFAC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ORDENFAC_INS", aParam));
        }
        public static int Update(SqlTransaction tr, int t610_idordenfac, int t305_idproyectosubnodo, 
                                 Nullable<int> t302_idcliente_solici, Nullable<int> t302_idcliente_respago, 
                                Nullable<int> t302_idcliente_destfact, string t610_condicionpago, string t610_viapago, 
                                string t610_refcliente, Nullable<DateTime> t610_fprevemifact, string t610_moneda, 
                                Nullable<int> t622_idagrupacion, string t610_observacionespool, string t610_comentario, 
                                string t610_dvsap, string t621_idovsap, float t610_dto_porcen, decimal t610_dto_importe, 
                                Nullable<DateTime> t610_fdiferida, int t314_idusuario_respcomercial, bool t610_ivaincluido,
                                string t610_textocabecera, string t610_infotramit)//, int t314_idusuario
        {
            SqlParameter[] aParam = new SqlParameter[22];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@t302_idcliente_solici", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente_solici;
            aParam[3] = new SqlParameter("@t302_idcliente_respago", SqlDbType.Int, 4);
            aParam[3].Value = t302_idcliente_respago;
            aParam[4] = new SqlParameter("@t302_idcliente_destfact", SqlDbType.Int, 4);
            aParam[4].Value = t302_idcliente_destfact;
            aParam[5] = new SqlParameter("@t610_condicionpago", SqlDbType.Text, 4);
            aParam[5].Value = t610_condicionpago;
            aParam[6] = new SqlParameter("@t610_viapago", SqlDbType.Text, 1);
            aParam[6].Value = t610_viapago;
            aParam[7] = new SqlParameter("@t610_refcliente", SqlDbType.Text, 35);
            aParam[7].Value = t610_refcliente;
            aParam[8] = new SqlParameter("@t610_fprevemifact", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = t610_fprevemifact;
            aParam[9] = new SqlParameter("@t610_moneda", SqlDbType.Text, 5);
            aParam[9].Value = t610_moneda;
            aParam[10] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
            aParam[10].Value = t622_idagrupacion;
            aParam[11] = new SqlParameter("@t610_observacionespool", SqlDbType.Text, 2147483647);
            aParam[11].Value = t610_observacionespool;
            aParam[12] = new SqlParameter("@t610_comentario", SqlDbType.Text, 2147483647);
            aParam[12].Value = t610_comentario;
            aParam[13] = new SqlParameter("@t610_dvsap", SqlDbType.Text, 10);
            aParam[13].Value = t610_dvsap;
            aParam[14] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
            aParam[14].Value = t621_idovsap;
            aParam[15] = new SqlParameter("@t610_dto_porcen", SqlDbType.Real, 4);
            aParam[15].Value = t610_dto_porcen;
            aParam[16] = new SqlParameter("@t610_dto_importe", SqlDbType.Money, 8);
            aParam[16].Value = t610_dto_importe;
            aParam[17] = new SqlParameter("@t610_fdiferida", SqlDbType.SmallDateTime, 4);
            aParam[17].Value = t610_fdiferida;
            aParam[18] = new SqlParameter("@t314_idusuario_respcomercial", SqlDbType.Int, 4);
            aParam[18].Value = t314_idusuario_respcomercial;
            aParam[19] = new SqlParameter("@t610_ivaincluido", SqlDbType.Bit, 1);
            aParam[19].Value = t610_ivaincluido;
            aParam[20] = new SqlParameter("@t610_textocabecera", SqlDbType.Text, 2147483647);
            aParam[20].Value = t610_textocabecera;
            //aParam[21] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[21].Value = t314_idusuario;
            aParam[21] = new SqlParameter("@t610_infotramit", SqlDbType.Text, 150);
            aParam[21].Value = t610_infotramit;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ORDENFAC_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORDENFAC_UPD", aParam);
        }
        
        public static int UpdateEstado(SqlTransaction tr, int t610_idordenfac, string t610_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;
            aParam[1] = new SqlParameter("@t610_estado", SqlDbType.Char, 1);
            aParam[1].Value = t610_estado;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ORDENFAC_UPDEST", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORDENFAC_UPDEST", aParam);
        }

        public static int UpdateProySubnodo(SqlTransaction tr, int t610_idordenfac, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("[SUP_ORDENFAC_UPD_PROY]", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "[SUP_ORDENFAC_UPD_PROY]", aParam);
        }

        public static ORDENFAC Select(SqlTransaction tr, int t610_idordenfac)
        {
            ORDENFAC o = new ORDENFAC();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[1].Value = t610_idordenfac;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_ORDENFAC_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDENFAC_O", aParam);

            if (dr.Read())
            {
                if (dr["t610_idordenfac"] != DBNull.Value)
                    o.t610_idordenfac = int.Parse(dr["t610_idordenfac"].ToString());
                if (dr["t610_fcreacion"] != DBNull.Value)
                    o.t610_fcreacion = (DateTime)dr["t610_fcreacion"];
                if (dr["t610_estado"] != DBNull.Value)
                    o.t610_estado = (string)dr["t610_estado"];
                if (dr["des_estado"] != DBNull.Value)
                    o.des_estado = (string)dr["des_estado"];
                if (dr["t610_ftramitada"] != DBNull.Value)
                    o.t610_ftramitada = (DateTime)dr["t610_ftramitada"];
                if (dr["t610_ftraspasada"] != DBNull.Value)
                    o.t610_ftraspasada = (DateTime)dr["t610_ftraspasada"];
                if (dr["t610_fenviada"] != DBNull.Value)
                    o.t610_fenviada = (DateTime)dr["t610_fenviada"];
                if (dr["t610_frecogida"] != DBNull.Value)
                    o.t610_frecogida = (DateTime)dr["t610_frecogida"];
                if (dr["t610_semaforo"] != DBNull.Value)
                    o.t610_semaforo = (bool)dr["t610_semaforo"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t302_idcliente_solici"] != DBNull.Value)
                    o.t302_idcliente_solici = int.Parse(dr["t302_idcliente_solici"].ToString());
                if (dr["t302_idcliente_respago"] != DBNull.Value)
                    o.t302_idcliente_respago = int.Parse(dr["t302_idcliente_respago"].ToString());
                if (dr["t302_idcliente_destfact"] != DBNull.Value)
                    o.t302_idcliente_destfact = int.Parse(dr["t302_idcliente_destfact"].ToString());
                if (dr["t610_condicionpago"] != DBNull.Value)
                    o.t610_condicionpago = (string)dr["t610_condicionpago"];
                if (dr["t610_viapago"] != DBNull.Value)
                    o.t610_viapago = (string)dr["t610_viapago"];
                if (dr["t610_refcliente"] != DBNull.Value)
                    o.t610_refcliente = (string)dr["t610_refcliente"];
                if (dr["t610_fprevemifact"] != DBNull.Value)
                    o.t610_fprevemifact = (DateTime)dr["t610_fprevemifact"];
                if (dr["t610_moneda"] != DBNull.Value)
                    o.t610_moneda = (string)dr["t610_moneda"];
                if (dr["t622_idagrupacion"] != DBNull.Value)
                    o.t622_idagrupacion = int.Parse(dr["t622_idagrupacion"].ToString());
                if (dr["t610_observacionespool"] != DBNull.Value)
                    o.t610_observacionespool = (string)dr["t610_observacionespool"];
                if (dr["t610_comentario"] != DBNull.Value)
                    o.t610_comentario = (string)dr["t610_comentario"];
                if (dr["t610_dvsap"] != DBNull.Value)
                    o.t610_dvsap = (string)dr["t610_dvsap"];
                //if (dr["t302_denominacion_solici"] != DBNull.Value)
                //    o.t302_denominacion_solici = (string)dr["t302_denominacion_solici"];
                if (dr["t302_denominacion_respago"] != DBNull.Value)
                    o.t302_denominacion_respago = (string)dr["t302_denominacion_respago"];
                if (dr["t302_denominacion_destfact"] != DBNull.Value)
                    o.t302_denominacion_destfact = (string)dr["t302_denominacion_destfact"];
                //if (dr["NifSolicitante"] != DBNull.Value)
                //    o.NifSolicitante = (string)dr["NifSolicitante"];
                if (dr["NifRespPago"] != DBNull.Value)
                    o.NifRespPago = (string)dr["NifRespPago"];
                if (dr["NifDestFra"] != DBNull.Value)
                    o.NifDestFra = (string)dr["NifDestFra"];
                if (dr["t621_idovsap"] != DBNull.Value)
                    o.t621_idovsap = (string)dr["t621_idovsap"];
                if (dr["t610_dto_porcen"] != DBNull.Value)
                    o.t610_dto_porcen = float.Parse(dr["t610_dto_porcen"].ToString());
                if (dr["t610_dto_importe"] != DBNull.Value)
                    o.t610_dto_importe = decimal.Parse(dr["t610_dto_importe"].ToString());
                if (dr["t610_fdiferida"] != DBNull.Value)
                    o.t610_fdiferida = (DateTime)dr["t610_fdiferida"];
                if (dr["t622_denominacion"] != DBNull.Value)
                    o.t622_denominacion = (string)dr["t622_denominacion"];
                if (dr["t314_idusuario_respcomercial"] != DBNull.Value)
                    o.t314_idusuario_respcomercial = int.Parse(dr["t314_idusuario_respcomercial"].ToString());
                o.denComercial= (string)dr["denComercial"];
                if (dr["direccion"] != DBNull.Value)
                    o.direccion = (string)dr["direccion"];
                if (dr["t610_ivaincluido"] != DBNull.Value)
                    o.t610_ivaincluido = (bool)dr["t610_ivaincluido"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = (int)dr["t301_idproyecto"];
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t610_textocabecera"] != DBNull.Value)
                    o.t610_textocabecera = (string)dr["t610_textocabecera"];
                if (dr["t610_observacionesplan"] != DBNull.Value)
                    o.t610_observacionesplan = (string)dr["t610_observacionesplan"];
                if (dr["t302_codigoexterno"] != DBNull.Value)
                    o.t302_codigoexterno = (string)dr["t302_codigoexterno"].ToString();
                if (dr["t302_efactur"] != DBNull.Value)
                    o.t302_efactur = (bool)dr["t302_efactur"];
                if (dr["t610_infotramit"] != DBNull.Value)
                    o.t610_infotramit = (string)dr["t610_infotramit"];
            } 
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de ORDENFAC"));
            }

            dr.Close();
            dr.Dispose();

            o.numDocs = SUPER.DAL.ORDENFAC.NumDocs(tr, t610_idordenfac);

            return o;
        }

		public static SqlDataReader CatalogoByPSN(SqlTransaction tr, int t305_idproyectosubnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

			aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[1].Value = t305_idproyectosubnodo;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ORDFAC_CATPSN", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDFAC_CATPSN", aParam);
		}
        public static SqlDataReader CatalogoByPSNyFechas(SqlTransaction tr, int t305_idproyectosubnodo,
                                    Nullable<DateTime> dtFecIni, Nullable<DateTime> dtFecFin)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@dtFecIni", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtFecIni;
            aParam[3] = new SqlParameter("@dtFecFin", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dtFecFin;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ORDFAC_PSN_FEC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDFAC_PSN_FEC_CAT", aParam);
        }

        public static SqlDataReader Catalogo(SqlTransaction tr, int t314_idusuario, string sEstados, Nullable<int> nAnnomes, Nullable<int> t305_idproyectosubnodo, Nullable<int> t302_idcliente, bool bMostrarPropias)
		{
			SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@sEstados", SqlDbType.VarChar, 20);
			aParam[2].Value = sEstados;
			aParam[3] = new SqlParameter("@nAnnomes", SqlDbType.Int, 4);
			aParam[3].Value = nAnnomes;
			aParam[4] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[4].Value = t305_idproyectosubnodo;
			aParam[5] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[5].Value = t302_idcliente;
            aParam[6] = new SqlParameter("@bMostrarPropias", SqlDbType.Bit, 1);
            aParam[6].Value = bMostrarPropias;
            
			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ORDFAC_CAT", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDFAC_CAT", aParam);
		}
        public static SqlDataReader CatalogoADM(SqlTransaction tr, string sEstados, Nullable<int> nAnnomes, Nullable<int> t305_idproyectosubnodo, Nullable<int> t302_idcliente, Nullable<int> t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@entorno", SqlDbType.Char, 1, System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper());
            aParam[i++] = ParametroSql.add("@sEstados", SqlDbType.VarChar, 20, sEstados);
            aParam[i++] = ParametroSql.add("@nAnnomes", SqlDbType.Int, 4, nAnnomes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ORDFAC_CAT_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ORDFAC_CAT_ADM", aParam);
        }


        public static int NroPosiciones(SqlTransaction tr, int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ORDENFAC_NPOSICIONES", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ORDENFAC_NPOSICIONES", aParam));
        }
        public static int Recuperar(SqlTransaction tr, int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ORDENFAC_REC", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORDENFAC_REC", aParam);
        }
        public static int Duplicar(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, string sAccDoc)
        {
            int iNewOrdenFac = -1;
            bool bSuperAdm = SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion();

            iNewOrdenFac = SUPER.DAL.ORDENFAC.Duplicar(tr, t610_idordenfac, t314_idusuario, bSuperAdm);
            switch (sAccDoc)
            {
                case "M":
                    SUPER.DAL.ORDENFAC.MantenerDocs(tr, t610_idordenfac, t314_idusuario, iNewOrdenFac);
                    break;
                case "G":
                    DuplicarDocs(tr, t610_idordenfac, t314_idusuario, iNewOrdenFac);
                    break;
            }

            return iNewOrdenFac;
        }
        /// <summary>
        /// Recorre la lista de documentos asociados a la orden de facturación
        /// Genera un copia en el Content-Server y crea un nuevo registro de documento con el nuevo identificador
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="iNewOrdenFac"></param>
        public static void DuplicarDocs(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, int iNewOrdenFac)
        {
            long idContentServer=-1;
            //Obtengo la lista de documentos asociados a la orden de facturación origen
            List<DOCUOF> Lista = SUPER.Capa_Negocio.DOCUOF.Lista(tr, t610_idordenfac);
            foreach (DOCUOF oDoc in Lista)
            {
                //Inserto el archivo en Atenea
                idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(oDoc.t624_nombrearchivo, oDoc.t624_archivo);
                //Inserto un registro en la tabal de documentos de orden de facturación con referencia al nuevo documento en Atenea
                SUPER.DAL.DOCUOF.Insert(tr, iNewOrdenFac, oDoc.t624_descripcion, oDoc.t624_nombrearchivo, oDoc.t624_archivo, idContentServer, t314_idusuario);
            }
        }
        /// <summary>
        /// Copia los documento de una plantilla a una orden de facturación
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t629_idplantillaof"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="t610_idordenfac"></param>
        public static void CopiarDocsPLANTaOF(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, int t610_idordenfac)
        {
            long idContentServer = -1;
            //Obtengo la lista de documentos asociados a la orden de facturación origen
            List<PLANTILLADOCUOF> Lista = SUPER.Capa_Negocio.PLANTILLADOCUOF.Lista(tr, t629_idplantillaof);
            foreach (PLANTILLADOCUOF oDoc in Lista)
            {
                //Inserto el archivo en Atenea
                idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(oDoc.t631_nombrearchivo, oDoc.t631_archivo);
                //Inserto un registro en la tabal de documentos de PLANTILLA orden de facturación con referencia al nuevo documento en Atenea
                SUPER.DAL.DOCUOF.Insert(tr, t610_idordenfac, oDoc.t631_descripcion, oDoc.t631_nombrearchivo, oDoc.t631_archivo, idContentServer, t314_idusuario);
            }
        }

        public static int CrearDesdePlantilla(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, string sAccDoc)
        {
            int iNewOrdenFac = -1;
            bool bSuperAdm = SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion();

            iNewOrdenFac = SUPER.DAL.ORDENFAC.CrearDesdePlantilla(tr, t629_idplantillaof, t314_idusuario, bSuperAdm);
            switch (sAccDoc)
            {
                case "M":
                    SUPER.DAL.ORDENFAC.MantenerDocs(tr, t629_idplantillaof, t314_idusuario, iNewOrdenFac);
                    break;
                case "G":
                    CopiarDocsPLANTaOF(tr, t629_idplantillaof, t314_idusuario, iNewOrdenFac);
                    break;
            }

            return iNewOrdenFac;
        }

        public static SqlDataReader Previsualizar(int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[1].Value = t610_idordenfac;

            return SqlHelper.ExecuteSqlDataReader("SUP_ORDENFAC_PREV_O", aParam);
        }

        public static bool HayDocs(SqlTransaction tr, string slOrdenes)
        {
            return SUPER.DAL.ORDENFAC.HayDocs(tr, slOrdenes);
        }
        public static int NumDocs(SqlTransaction tr, int t610_idordenfac)
        {
            return SUPER.DAL.ORDENFAC.NumDocs(tr, t610_idordenfac);
        }
        #endregion
    }
}
