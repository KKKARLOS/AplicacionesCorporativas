using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PARAMETRIZACIONSUPER
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T725_PARAMETRIZACIONSUPER
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	08/11/2011 11:36:45	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PARAMETRIZACIONSUPER
    {
        #region Propiedades y Atributos

        private decimal _t725_toleranciapasohistorico;
        public decimal t725_toleranciapasohistorico
        {
            get { return _t725_toleranciapasohistorico; }
            set { _t725_toleranciapasohistorico = value; }
        }

        private byte _t725_mesespasohistorico;
        public byte t725_mesespasohistorico
        {
            get { return _t725_mesespasohistorico; }
            set { _t725_mesespasohistorico = value; }
        }

        private bool _t725_auditgeneral;
        public bool t725_auditgeneral
        {
            get { return _t725_auditgeneral; }
            set { _t725_auditgeneral = value; }
        }

        private bool _t725_accesoauditoria;
        public bool t725_accesoauditoria
        {
            get { return _t725_accesoauditoria; }
            set { _t725_accesoauditoria = value; }
        }

        private int _t725_umciap_externos;
        public int t725_umciap_externos
        {
            get { return _t725_umciap_externos; }
            set { _t725_umciap_externos = value; }
        }

        private byte _t725_modotipocambioBCE;
        public byte t725_modotipocambioBCE
        {
            get { return _t725_modotipocambioBCE; }
            set { _t725_modotipocambioBCE = value; }
        }

        private decimal _t725_produccionCVT;
        public decimal t725_produccionCVT
        {
            get { return _t725_produccionCVT; }
            set { _t725_produccionCVT = value; }
        }
        private bool _t725_alertasproy_activas;
        public bool t725_alertasproy_activas
        {
            get { return _t725_alertasproy_activas; }
            set { _t725_alertasproy_activas = value; }
        }
        private bool _t725_correoCIAactivo;
        public bool t725_correoCIAactivo
        {
            get { return _t725_correoCIAactivo; }
            set { _t725_correoCIAactivo = value; }
        }

        private int _t725_ejecutaalertas;
        public int t725_ejecutaalertas
        {
            get { return _t725_ejecutaalertas; }
            set { _t725_ejecutaalertas = value; }
        }
        private bool _t725_foraneos;
        public bool t725_foraneos
        {
            get { return _t725_foraneos; }
            set { _t725_foraneos = value; }
        }
        private int _t725_ultcierreempresa_ECO;
        public int t725_ultcierreempresa_ECO
        {
            get { return _t725_ultcierreempresa_ECO; }
            set { _t725_ultcierreempresa_ECO = value; }
        }

        private DateTime? _t855_prevcierreeco;
        public DateTime? t855_prevcierreeco
        {
            get { return _t855_prevcierreeco; }
            set { _t855_prevcierreeco = value; }
        }
        private int _t725_ultcierreempresa_IAP;
        public int t725_ultcierreempresa_IAP
        {
            get { return _t725_ultcierreempresa_IAP; }
            set { _t725_ultcierreempresa_IAP = value; }
        }
        private int _t725_diasavisovencim;
        public int t725_diasavisovencim
        {
            get { return _t725_diasavisovencim; }
            set { _t725_diasavisovencim = value; }
        }
        private DateTime? _t725_fproceso_act_masi;
        public DateTime? t725_fproceso_act_masi
        {
            get { return _t725_fproceso_act_masi; }
            set { _t725_fproceso_act_masi = value; }
        }
        private short _t725_ndias_act_masi;
        public short t725_ndias_act_masi
        {
            get { return _t725_ndias_act_masi; }
            set { _t725_ndias_act_masi = value; }
        }
        private short _t725_ndias_envi_validar;
        public short t725_ndias_envi_validar
        {
            get { return _t725_ndias_envi_validar; }
            set { _t725_ndias_envi_validar = value; }
        }
        private short _t725_ndias_validar_reg;
        public short t725_ndias_validar_reg
        {
            get { return _t725_ndias_validar_reg; }
            set { _t725_ndias_validar_reg = value; }
        }
        private short _t725_ndias_cualifi_proy;
        public short t725_ndias_cualifi_proy
        {
            get { return _t725_ndias_cualifi_proy; }
            set { _t725_ndias_cualifi_proy = value; }
        }
        private short _t725_ndias_alta_exp;
        public short t725_ndias_alta_exp
        {
            get { return _t725_ndias_alta_exp; }
            set { _t725_ndias_alta_exp = value; }
        }
        private short _t725_ndias_peticion_bor;
        public short t725_ndias_peticion_bor
        {
            get { return _t725_ndias_peticion_bor; }
            set { _t725_ndias_peticion_bor = value; }
        }
        private short _t725_ndias_tar_ven_noven;
        public short t725_ndias_tar_ven_noven
        {
            get { return _t725_ndias_tar_ven_noven; }
            set { _t725_ndias_tar_ven_noven = value; }
        }
        private short _t725_ndias_tar_ven_mieq;
        public short t725_ndias_tar_ven_mieq
        {
            get { return _t725_ndias_tar_ven_mieq; }
            set { _t725_ndias_tar_ven_mieq = value; }
        }
        private DateTime? _t725_fultenvio_tar_ven_noven;
        public DateTime? t725_fultenvio_tar_ven_noven
        {
            get { return _t725_fultenvio_tar_ven_noven; }
            set { _t725_fultenvio_tar_ven_noven = value; }
        }
        private DateTime? _t725_fultenvio_tar_ven_mieq;
        public DateTime? t725_fultenvio_tar_ven_mieq
        {
            get { return _t725_fultenvio_tar_ven_mieq; }
            set { _t725_fultenvio_tar_ven_mieq = value; }
        }
        #endregion

        #region Constructor

        public PARAMETRIZACIONSUPER()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza Inserta un registro de la tabla T725_PARAMETRIZACIONSUPER.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/10/2008 11:41:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Update(SqlTransaction tr, decimal t725_toleranciapasohistorico, byte t725_mesespasohistorico,
                                bool t725_accesoauditoria, bool t725_auditgeneral, decimal t725_produccionCVT,
                                bool t725_alertasproy_activas, bool bMailCIA, bool t725_foraneos, int t725_diasavisovencim)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t725_toleranciapasohistorico", SqlDbType.Money, 8, t725_toleranciapasohistorico),
                ParametroSql.add("@t725_mesespasohistorico", SqlDbType.TinyInt, 1, t725_mesespasohistorico),
                ParametroSql.add("@t725_accesoauditoria", SqlDbType.Bit, 1, t725_accesoauditoria),
                ParametroSql.add("@t725_auditgeneral", SqlDbType.Bit, 1, t725_auditgeneral),
                ParametroSql.add("@t725_produccionCVT", SqlDbType.Money, 8, t725_produccionCVT),
                ParametroSql.add("@t725_alertasproy_activas", SqlDbType.Bit, 1, t725_alertasproy_activas),
                ParametroSql.add("@t725_correoCIAactivo ", SqlDbType.Bit, 1, bMailCIA),
                ParametroSql.add("@t725_foraneos", SqlDbType.Bit, 1, t725_foraneos),
                ParametroSql.add("@t725_diasavisovencim", SqlDbType.Int, 4, t725_diasavisovencim)                
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_U", aParam);
        }
        public static void Update(Nullable<int> t725_ejecutaalertas, Nullable<int> t001_idficepi_ejecutaalertas)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t725_ejecutaalertas", SqlDbType.Int, 4, t725_ejecutaalertas),
                ParametroSql.add("@t001_idficepi_ejecutaalertas", SqlDbType.Int, 4, t001_idficepi_ejecutaalertas)
            };

            SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_U_GEN_DIALOG", aParam);
        }
        public static void UpdateCierreEmpresaECO(SqlTransaction tr, int t725_ultcierreempresa_ECO)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t725_ultcierreempresa_ECO", SqlDbType.Int, 4, t725_ultcierreempresa_ECO)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_U_UMC", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_U_UMC", aParam);

        }
        public static int UpdateCierreEmpresaIAP(SqlTransaction tr, int t725_ultcierreempresa_IAP)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t725_ultcierreempresa_IAP", SqlDbType.Int, 4, t725_ultcierreempresa_IAP)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_CEIAP_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_CEIAP_U", aParam);

        }
        /// <summary>
        /// Actualiza la lista de figuras de proyecto asignables a un forastero (lista de elementos separada por comas)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="sFiguras"></param>
        /// <returns></returns>
        public static int UpdateFigProyForaneo(SqlTransaction tr, string t725_figurasforaneos)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t725_figurasforaneos", SqlDbType.VarChar, 50, t725_figurasforaneos)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_U_FIGFOR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_U_FIGFOR", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T725_PARAMETRIZACIONSUPER,
        /// y devuelve una instancia u objeto del tipo T725_PARAMETRIZACIONSUPER
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/10/2008 11:41:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static PARAMETRIZACIONSUPER Select(SqlTransaction tr)
        {
            PARAMETRIZACIONSUPER o = new PARAMETRIZACIONSUPER();

            SqlParameter[] aParam = new SqlParameter[0];

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PARAMETRIZACIONSUPER_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_S", aParam);

            if (dr.Read())
            {
                if (dr["t725_toleranciapasohistorico"] != DBNull.Value)
                    o.t725_toleranciapasohistorico = decimal.Parse(dr["t725_toleranciapasohistorico"].ToString());
                if (dr["t725_mesespasohistorico"] != DBNull.Value)
                    o.t725_mesespasohistorico = byte.Parse(dr["t725_mesespasohistorico"].ToString());
                if (dr["t725_auditgeneral"] != DBNull.Value)
                    o.t725_auditgeneral = bool.Parse(dr["t725_auditgeneral"].ToString());
                if (dr["t725_accesoauditoria"] != DBNull.Value)
                    o.t725_accesoauditoria = bool.Parse(dr["t725_accesoauditoria"].ToString());
                if (dr["t725_umciap_externos"] != DBNull.Value)
                    o.t725_umciap_externos = int.Parse(dr["t725_umciap_externos"].ToString());
                if (dr["t725_modotipocambioBCE"] != DBNull.Value)
                    o.t725_modotipocambioBCE = byte.Parse(dr["t725_modotipocambioBCE"].ToString());
                if (dr["t725_produccionCVT"] != DBNull.Value)
                    o.t725_produccionCVT = decimal.Parse(dr["t725_produccionCVT"].ToString());//t725_correoCIAactivo
                if (dr["t725_alertasproy_activas"] != DBNull.Value)
                    o.t725_alertasproy_activas = bool.Parse(dr["t725_alertasproy_activas"].ToString());
                if (dr["t725_correoCIAactivo"] != DBNull.Value)
                    o.t725_correoCIAactivo = bool.Parse(dr["t725_correoCIAactivo"].ToString());
                if (dr["t725_ejecutaalertas"] != DBNull.Value)
                    o.t725_ejecutaalertas = int.Parse(dr["t725_ejecutaalertas"].ToString());
                if (dr["t725_foraneos"] != DBNull.Value)
                    o.t725_foraneos = bool.Parse(dr["t725_foraneos"].ToString());
                if (dr["t725_ultcierreempresa_ECO"] != DBNull.Value)
                    o.t725_ultcierreempresa_ECO = int.Parse(dr["t725_ultcierreempresa_ECO"].ToString());
                if (dr["t855_prevcierreeco"] != DBNull.Value)
                    o.t855_prevcierreeco = (DateTime)dr["t855_prevcierreeco"];
                if (dr["t725_ultcierreempresa_IAP"] != DBNull.Value)
                    o.t725_ultcierreempresa_IAP = int.Parse(dr["t725_ultcierreempresa_IAP"].ToString());
                if (dr["t725_diasavisovencim"] != DBNull.Value)
                    o.t725_diasavisovencim = int.Parse(dr["t725_diasavisovencim"].ToString());
                if (dr["t725_fproceso_act_masi"] != DBNull.Value)
                    o.t725_fproceso_act_masi = (DateTime)dr["t725_fproceso_act_masi"];
                if (dr["t725_ndias_act_masi"] != DBNull.Value)
                    o.t725_ndias_act_masi = short.Parse(dr["t725_ndias_act_masi"].ToString());
                if (dr["t725_ndias_envi_validar"] != DBNull.Value)
                    o.t725_ndias_envi_validar = short.Parse(dr["t725_ndias_envi_validar"].ToString());
                if (dr["t725_ndias_validar_reg"] != DBNull.Value)
                    o.t725_ndias_validar_reg = short.Parse(dr["t725_ndias_validar_reg"].ToString());
                if (dr["t725_ndias_cualifi_proy"] != DBNull.Value)
                    o.t725_ndias_cualifi_proy = short.Parse(dr["t725_ndias_cualifi_proy"].ToString());
                if (dr["t725_ndias_alta_exp"] != DBNull.Value)
                    o.t725_ndias_alta_exp = short.Parse(dr["t725_ndias_alta_exp"].ToString());
                if (dr["t725_ndias_peticion_bor"] != DBNull.Value)
                    o.t725_ndias_peticion_bor = short.Parse(dr["t725_ndias_peticion_bor"].ToString());
                if (dr["t725_ndias_tar_ven_noven"] != DBNull.Value)
                    o.t725_ndias_tar_ven_noven = short.Parse(dr["t725_ndias_tar_ven_noven"].ToString());
                if (dr["t725_ndias_tar_ven_mieq"] != DBNull.Value)
                    o.t725_ndias_tar_ven_mieq = short.Parse(dr["t725_ndias_tar_ven_mieq"].ToString());
                if (dr["t725_fultenvio_tar_ven_noven"] != DBNull.Value)
                    o.t725_fultenvio_tar_ven_noven = (DateTime)dr["t725_fultenvio_tar_ven_noven"];
                if (dr["t725_fultenvio_tar_ven_mieq"] != DBNull.Value)
                    o.t725_fultenvio_tar_ven_mieq = (DateTime)dr["t725_fultenvio_tar_ven_mieq"];
            }
            else
            {
                o.t725_toleranciapasohistorico = 0;
                o.t725_mesespasohistorico = 0;
                o.t725_produccionCVT = 0;
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        #endregion

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza datos de un registro de la tabla T725_PARAMETRIZACIONSUPER.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/10/2008 11:41:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Update(SqlTransaction tr, Nullable<DateTime> t725_fproceso_act_masi, short t725_ndias_act_masi,
                                short t725_ndias_envi_validar, short t725_ndias_validar_reg, short t725_ndias_cualifi_proy,
                                short t725_ndias_alta_exp, short t725_ndias_peticion_bor, short t725_ndias_tar_ven_noven,
                                short t725_ndias_tar_ven_mieq, Nullable<DateTime> t725_fultenvio_tar_ven_noven, Nullable<DateTime> t725_fultenvio_tar_ven_mieq
                                )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t725_fproceso_act_masi", SqlDbType.SmallDateTime, 4, t725_fproceso_act_masi),
                ParametroSql.add("@t725_ndias_act_masi", SqlDbType.SmallInt, 2, t725_ndias_act_masi),
                ParametroSql.add("@t725_ndias_envi_validar", SqlDbType.SmallInt, 2, t725_ndias_envi_validar),
                ParametroSql.add("@t725_ndias_validar_reg", SqlDbType.SmallInt, 2, t725_ndias_validar_reg),
                ParametroSql.add("@t725_ndias_cualifi_proy", SqlDbType.SmallInt, 2, t725_ndias_cualifi_proy),
                ParametroSql.add("@t725_ndias_alta_exp", SqlDbType.SmallInt, 2, t725_ndias_alta_exp),
                ParametroSql.add("@t725_ndias_peticion_bor", SqlDbType.SmallInt, 2, t725_ndias_peticion_bor),
                ParametroSql.add("@t725_ndias_tar_ven_noven", SqlDbType.SmallInt, 2, t725_ndias_tar_ven_noven),
                ParametroSql.add("@t725_ndias_tar_ven_mieq", SqlDbType.SmallInt, 2, t725_ndias_tar_ven_mieq),
                ParametroSql.add("@t725_fultenvio_tar_ven_noven", SqlDbType.SmallDateTime, 4, t725_fultenvio_tar_ven_noven),
                ParametroSql.add("@t725_fultenvio_tar_ven_mieq", SqlDbType.SmallDateTime, 4, t725_fultenvio_tar_ven_mieq),        
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_CVT_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_CVT_U", aParam);
        }
        public static string Grabar(string sParametrizacion)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);

                string sError = Errores.mostrarError("Error al abrir la conexión", ex);
                string[] aError = Regex.Split(sError, "@#@");
                throw new Exception(Utilidades.escape(aError[0]), ex);
            }
            #endregion

            try
            {
                if (sParametrizacion != "")
                {

                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aDatos = Regex.Split(sParametrizacion, "#sCad#");

                    PARAMETRIZACIONSUPER.Update(tr,
                                                (aDatos[0] == "") ? null : (DateTime?)DateTime.Parse(aDatos[0]),
                                                (aDatos[1] == "") ? short.Parse("0") : short.Parse(aDatos[1]),
                                                (aDatos[2] == "") ? short.Parse("0") : short.Parse(aDatos[2]),
                                                (aDatos[3] == "") ? short.Parse("0") : short.Parse(aDatos[3]),
                                                (aDatos[4] == "") ? short.Parse("0") : short.Parse(aDatos[4]),
                                                (aDatos[5] == "") ? short.Parse("0") : short.Parse(aDatos[5]),
                                                (aDatos[6] == "") ? short.Parse("0") : short.Parse(aDatos[6]),
                                                (aDatos[7] == "") ? short.Parse("0") : short.Parse(aDatos[7]),
                                                (aDatos[8] == "") ? short.Parse("0") : short.Parse(aDatos[8]),
                                                (aDatos[9] == "") ? null : (DateTime?)DateTime.Parse(aDatos[9]),
                                                (aDatos[10] == "") ? null : (DateTime?)DateTime.Parse(aDatos[10])
                                                );

                }

                Conexion.CommitTransaccion(tr);
                sResul = "";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar la parametrización.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sResul;
        }
    }
}
