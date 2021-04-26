using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : DATOECO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T376_DATOECO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	17/04/2008 13:01:53	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DATOECO
    {
        #region Propiedades y Atributos

        private int _t376_iddatoeco;
        public int t376_iddatoeco
        {
            get { return _t376_iddatoeco; }
            set { _t376_iddatoeco = value; }
        }

        private int _t325_idsegmesproy;
        public int t325_idsegmesproy
        {
            get { return _t325_idsegmesproy; }
            set { _t325_idsegmesproy = value; }
        }

        private int _t329_idclaseeco;
        public int t329_idclaseeco
        {
            get { return _t329_idclaseeco; }
            set { _t329_idclaseeco = value; }
        }

        private string _t376_motivo;
        public string t376_motivo
        {
            get { return _t376_motivo; }
            set { _t376_motivo = value; }
        }

        private decimal _t376_importe;
        public decimal t376_importe
        {
            get { return _t376_importe; }
            set { _t376_importe = value; }
        }

        private int _t303_idnodo_destino;
        public int t303_idnodo_destino
        {
            get { return _t303_idnodo_destino; }
            set { _t303_idnodo_destino = value; }
        }

        private int _t315_idproveedor;
        public int t315_idproveedor
        {
            get { return _t315_idproveedor; }
            set { _t315_idproveedor = value; }
        }

        private DateTime _t376_fecha;
        public DateTime t376_fecha
        {
            get { return _t376_fecha; }
            set { _t376_fecha = value; }
        }

        private string _t376_seriefactura;
        public string t376_seriefactura
        {
            get { return _t376_seriefactura; }
            set { _t376_seriefactura = value; }
        }

        private int _t376_numerofactura;
        public int t376_numerofactura
        {
            get { return _t376_numerofactura; }
            set { _t376_numerofactura = value; }
        }

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

        private int _t376_iddatoeco_rel;
        public int t376_iddatoeco_rel
        {
            get { return _t376_iddatoeco_rel; }
            set { _t376_iddatoeco_rel = value; }
        }
        #endregion

        #region Constructores

        public DATOECO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static int Insert(SqlTransaction tr, int t325_idsegmesproy, int t329_idclaseeco, string t376_motivo, decimal t376_importe,
                                 Nullable<int> t303_idnodo_destino, Nullable<int> t315_idproveedor, Nullable<byte> t446_idproceso, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[1].Value = t329_idclaseeco;
            aParam[2] = new SqlParameter("@t376_motivo", SqlDbType.VarChar, 50);
            aParam[2].Value = (t376_motivo.Length > 50) ? t376_motivo.Substring(0, 50) : t376_motivo;
            aParam[3] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[3].Value = t376_importe;
            aParam[4] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo_destino;
            aParam[5] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[5].Value = t315_idproveedor;
            aParam[6] = new SqlParameter("@t446_idproceso", SqlDbType.TinyInt, 1);
            aParam[6].Value = t446_idproceso;
            aParam[7] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[7].Value = t422_idmoneda;
            
            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_OFICH", aParam));
        }

        public static int Insert(SqlTransaction tr, int t325_idsegmesproy, int t329_idclaseeco, string t376_motivo, decimal t376_importe,
                         Nullable<int> t303_idnodo_destino, Nullable<int> t315_idproveedor, Nullable<byte> t446_idproceso)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[1].Value = t329_idclaseeco;
            aParam[2] = new SqlParameter("@t376_motivo", SqlDbType.VarChar, 50);
            aParam[2].Value = (t376_motivo.Length > 50) ? t376_motivo.Substring(0, 50) : t376_motivo;
            aParam[3] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[3].Value = t376_importe;
            aParam[4] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo_destino;
            aParam[5] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[5].Value = t315_idproveedor;
            aParam[6] = new SqlParameter("@t446_idproceso", SqlDbType.TinyInt, 1);
            aParam[6].Value = t446_idproceso;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_ISEG", aParam));
        }
        public static int InsertFactura(SqlTransaction tr, int t325_idsegmesproy, int t329_idclaseeco, string t376_motivo, decimal t376_importe,
                                 Nullable<int> t303_idnodo_destino, Nullable<int> t315_idproveedor, Nullable<DateTime> t376_fecha,
                                 string t376_seriefactura, Nullable<int> t376_numerofactura, Nullable<int> t313_idempresa,
                                 Nullable<int> t302_idcliente, Nullable<byte> iProc, string t376_refcliente)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[1].Value = t329_idclaseeco;
            aParam[2] = new SqlParameter("@t376_motivo", SqlDbType.Text, 50);
            aParam[2].Value = t376_motivo;
            aParam[3] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[3].Value = t376_importe;
            aParam[4] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo_destino;
            aParam[5] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[5].Value = t315_idproveedor;
            aParam[6] = new SqlParameter("@t376_fecha", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t376_fecha;
            aParam[7] = new SqlParameter("@t376_seriefactura", SqlDbType.Text, 5);
            aParam[7].Value = t376_seriefactura;
            aParam[8] = new SqlParameter("@t376_numerofactura", SqlDbType.Int, 4);
            aParam[8].Value = t376_numerofactura;
            aParam[9] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[9].Value = t313_idempresa;
            aParam[10] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[10].Value = t302_idcliente;
            aParam[11] = new SqlParameter("@t446_idproceso", SqlDbType.TinyInt, 1);
            aParam[11].Value = iProc;
            aParam[12] = new SqlParameter("@t376_refcliente", SqlDbType.VarChar, 20);
            if (t376_refcliente.Trim() == "") aParam[12].Value = null;
            else aParam[12].Value = t376_refcliente;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_I_FACT", aParam));
        }
        public static int InsertCobro(SqlTransaction tr, int t325_idsegmesproy, decimal t376_importe, Nullable<DateTime> t376_fecha,
                                        int t376_iddatoeco_rel, string t376_seriefactura, int t376_numerofactura)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[1].Value = t376_importe;
            aParam[2] = new SqlParameter("@t376_fecha", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t376_fecha;
            aParam[3] = new SqlParameter("@t376_iddatoeco_rel", SqlDbType.Int, 4);
            aParam[3].Value = t376_iddatoeco_rel;
            aParam[4] = new SqlParameter("@t376_seriefactura", SqlDbType.Text, 5);
            aParam[4].Value = t376_seriefactura;
            aParam[5] = new SqlParameter("@t376_numerofactura", SqlDbType.Int, 4);
            aParam[5].Value = t376_numerofactura;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_I_COB", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T376_DATOECO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	17/04/2008 13:01:53
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t376_iddatoeco, int t325_idsegmesproy, int t329_idclaseeco, string t376_motivo, decimal t376_importe, Nullable<int> t303_idnodo_destino, Nullable<int> t315_idproveedor)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t376_iddatoeco", SqlDbType.Int, 4);
            aParam[0].Value = t376_iddatoeco;
            aParam[1] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[1].Value = t325_idsegmesproy;
            aParam[2] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[2].Value = t329_idclaseeco;
            aParam[3] = new SqlParameter("@t376_motivo", SqlDbType.Text, 50);
            aParam[3].Value = t376_motivo;
            aParam[4] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[4].Value = t376_importe;
            aParam[5] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo_destino;
            aParam[6] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[6].Value = t315_idproveedor;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DATOECO_USEG", aParam);
        }
        public static int UpdateDecalaje(SqlTransaction tr, int t376_iddatoeco, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t376_iddatoeco", SqlDbType.Int, 4);
            aParam[0].Value = t376_iddatoeco;
            aParam[1] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[1].Value = t325_idsegmesproy;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DATOECO_UDEC", aParam);
        }
        public static int UpdateAvanceTecnico(SqlTransaction tr, int t376_iddatoeco, decimal t376_importe)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t376_iddatoeco", SqlDbType.Int, 4);
            aParam[0].Value = t376_iddatoeco;
            aParam[1] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[1].Value = t376_importe;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DATOECO_UAVT", aParam);
        }
        public static int UpdateImporte(SqlTransaction tr, int t376_iddatoeco, decimal t376_importe)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t376_iddatoeco", SqlDbType.Int, 4);
            aParam[0].Value = t376_iddatoeco;
            aParam[1] = new SqlParameter("@t376_importe", SqlDbType.Money, 8);
            aParam[1].Value = t376_importe;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DATOECO_UAVT", aParam);
        }

        public static int Delete(SqlTransaction tr, int t376_iddatoeco)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t376_iddatoeco", SqlDbType.Int, 4, t376_iddatoeco);

            if (tr==null)
                return SqlHelper.ExecuteNonQuery("SUP_DATOECO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DATOECO_D", aParam);
        }

        public static SqlDataReader Catalogo(int t325_idsegmesproy, int nG, Nullable<int> nS, Nullable<int> nC, Nullable<int> nCL, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@nG", SqlDbType.Int, 4, nG);
            aParam[i++] = ParametroSql.add("@nS", SqlDbType.Int, 4, nS);
            aParam[i++] = ParametroSql.add("@nC", SqlDbType.Int, 4, nC);
            aParam[i++] = ParametroSql.add("@nCL", SqlDbType.Int, 4, nCL);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader((t422_idmoneda != null) ? "SUP_DATOECO_CAT" : "SUP_DATOECO_CAT_MP", aParam);
        }

        public static SqlDataReader CatalogoProduccionTransferencia(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_PRODTRANSFERENCIA_REP", aParam);
        }
        public static SqlDataReader ObtenerOrdenesFacturacionBorradoDecalaje(int t325_annomes, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[0].Value = t325_annomes;
            aParam[1] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[1].Value = sNodos;

            return SqlHelper.ExecuteSqlDataReader("SUP_ORDENESFACTURACION_CAT", aParam);
        }
        public static int ObtenerOrdenesFacturacionMes(int t325_annomes)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[0].Value = t325_annomes;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ORDENESFACTURACION_MES", aParam));
        }

        public static int ExisteDatoEco(SqlTransaction tr, int t325_idsegmesproy, int t329_idclaseeco)
        {
            int nDatoEco = 0;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[1].Value = t329_idclaseeco;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_EXISTEDATOECO", aParam);

            if (dr.Read())
            {
                nDatoEco = (int)dr["t376_iddatoeco"];
            }

            dr.Close();
            dr.Dispose();

            return nDatoEco;
        }
        public static int ExisteCobro(SqlTransaction tr, int nAnoMes, int t329_iddatoeco_fact)
        {
            int nDatoEco = 0;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnoMes", SqlDbType.Int, 4);
            aParam[0].Value = nAnoMes;
            aParam[1] = new SqlParameter("@t329_iddatoeco_fact", SqlDbType.Int, 4);
            aParam[1].Value = t329_iddatoeco_fact;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_EXISTE_COBRO", aParam);

            if (dr.Read())
            {
                nDatoEco = (int)dr["t376_iddatoeco"];
            }

            dr.Close();
            dr.Dispose();

            return nDatoEco;
        }
        //Funciones para el informe
        public static SqlDataReader ObtenerCombosInf(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;
            aParam[4] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[4].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            return SqlHelper.ExecuteSqlDataReader("SUP_INF_DATOECO_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerCombosInfAdm(int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[0].Value = nDesde;
            aParam[1] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[1].Value = nHasta;
            aParam[2] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[2].Value = nFilasMax;
            aParam[3] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[3].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            return SqlHelper.ExecuteSqlDataReader("SUP_INF_DATOECO_CRITERIOS_ADM", aParam);
        }
        public static SqlDataReader ObtenerInf(
                    int nOpcion,
                    int t314_idusuario,
                    int nDesde,
                    int nHasta,
                    Nullable<int> nNivelEstructura,
                    string t301_categoria,
                    string t305_cualidad,
                    string sProyectos,
                    string sClientes,
                    string sClientesFact,
                    string sResponsables,
                    string sNaturalezas,
                    string sHorizontal,
                    string sModeloContrato,
                    string sContrato,
                    string sIDEstructura,
                    string sSectores,
                    string sSegmentos,
                    bool bComparacionLogica,
                    string sCNP,
                    string sCSN1P,
                    string sCSN2P,
                    string sCSN3P,
                    string sCSN4P,
                    string sClasesEco,
                    string sCR,
                    string sProveedores,
                    string sSociedades, 
                    string sPaises,
                    string sProvincias,
                    string sSectorCG, 
                    string sSectorCF, 
                    string sSegmentoCG, 
                    string sSegmentoCF,
                    string sPaisCG, 
                    string sPaisCF, 
                    string sProvinciaCG, 
                    string sProvinciaCF,
                    string t422_idmoneda
               )
        {
            SqlParameter[] aParam = null;
            //if (nOpcion==2)
            //    aParam = new SqlParameter[24];
            //else
            //    aParam = new SqlParameter[26];

            switch (nOpcion)
            {
                case 1: aParam = new SqlParameter[27]; break;
                case 2: aParam = new SqlParameter[27]; break;
                case 3:
                case 4:
                    aParam = new SqlParameter[37];
                    break;
            }

            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[6].Value = sProyectos;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sClientesFact", SqlDbType.VarChar, 8000);
            aParam[22].Value = sClientesFact;
            aParam[23] = new SqlParameter("@sClasesEco", SqlDbType.VarChar, 8000);
            aParam[23].Value = sClasesEco;

            switch (nOpcion)
            {
                case 1://Consumos
                    aParam[24] = new SqlParameter("@sCR", SqlDbType.VarChar, 8000);
                    aParam[24].Value = sCR;
                    aParam[25] = new SqlParameter("@sProveedores", SqlDbType.VarChar, 8000);
                    aParam[25].Value = sProveedores;
                    aParam[26] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
                    aParam[26].Value = t422_idmoneda;

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_CONSUMOS_ADMIN" : "SUP_INF_CONSUMOS_7AM", 3600, aParam);
                    else
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_CONSUMOS_USU" : "SUP_INF_CONSUMOS_7AM", 3600, aParam);
                case 2://Producción
                    aParam[24] = new SqlParameter("@sPaises", SqlDbType.VarChar, 8000);
                    aParam[24].Value = sPaises;

                    aParam[25] = new SqlParameter("@sProvincias", SqlDbType.VarChar, 8000);
                    aParam[25].Value = sProvincias;

                    aParam[26] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
                    aParam[26].Value = t422_idmoneda;
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_PRODUCCION_ADMIN" : "SUP_INF_PRODUCCION_7AM", 3600, aParam);
                    else
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_PRODUCCION_USU" : "SUP_INF_PRODUCCION_7AM", 3600, aParam);
                case 3://Ingresos
                case 4://Cobros
                default:
                    aParam[24] = new SqlParameter("@sSociedades", SqlDbType.VarChar, 8000);
                    aParam[24].Value = sSociedades;

                    aParam[25] = new SqlParameter("@sPaises", SqlDbType.VarChar, 8000);
                    aParam[25].Value = sPaises;

                    aParam[26] = new SqlParameter("@sProvincias", SqlDbType.VarChar, 8000);
                    aParam[26].Value = sProvincias;


                    aParam[27] = new SqlParameter("@entorno", SqlDbType.Char, 1);
                    aParam[27].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
                    aParam[28] = new SqlParameter("@bSectorCG", SqlDbType.Bit, 1);
                    aParam[28].Value = (sSectorCG == "1") ? true : false;
                    aParam[29] = new SqlParameter("@bSectorCF", SqlDbType.Bit, 1);
                    aParam[29].Value = (sSectorCF == "1") ? true : false;
                    aParam[30] = new SqlParameter("@bSegmentoCG", SqlDbType.Bit, 1);
                    aParam[30].Value = (sSegmentoCG == "1") ? true : false;
                    aParam[31] = new SqlParameter("@bSegmentoCF", SqlDbType.Bit, 1);
                    aParam[31].Value = (sSegmentoCF == "1") ? true : false;

                    aParam[32] = new SqlParameter("@bPaisCG", SqlDbType.Bit, 1);
                    aParam[32].Value = (sPaisCG == "1") ? true : false;
                    aParam[33] = new SqlParameter("@bPaisCF", SqlDbType.Bit, 1);
                    aParam[33].Value = (sPaisCF == "1") ? true : false;
                    aParam[34] = new SqlParameter("@bProvinciaCG", SqlDbType.Bit, 1);
                    aParam[34].Value = (sProvinciaCG == "1") ? true : false;
                    aParam[35] = new SqlParameter("@bProvinciaCF", SqlDbType.Bit, 1);
                    aParam[35].Value = (sProvinciaCF == "1") ? true : false;

                    aParam[36] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
                    aParam[36].Value = t422_idmoneda;

                        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_FACT_ADMIN" : "SUP_INF_FACT_7AM", 3600, aParam);
                    else
                        return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_INF_FACT_USU" : "SUP_INF_FACT_7AM", 3600, aParam);
            }
        }

        public static SqlDataReader ObtenerProduccionPorProfesional(                    
                    int t314_idusuario,
                    int nDesde,
                    int nHasta,
                    Nullable<int> nNivelEstructura,
                    string t301_categoria,
                    string t305_cualidad,
                    string sProyectos,
                    string sClientes,
                    string sResponsables,
                    string sNaturalezas,
                    string sHorizontal,
                    string sModeloContrato,
                    string sContrato,
                    string sIDEstructura,
                    string sSectores,
                    string sSegmentos,
                    bool bComparacionLogica,
                    string sCNP,
                    string sCSN1P,
                    string sCSN2P,
                    string sCSN3P,
                    string sCSN4P,
                    string sCR,
                    string sPaises,
                    string sProvincias,
                    string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[24]; 

            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[6].Value = sProyectos;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCNP;
            aParam[18] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN1P;
            aParam[19] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN2P;
            aParam[20] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN3P;
            aParam[21] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN4P;
            aParam[22] = new SqlParameter("@sCR", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCR;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_PRODUCCION_PORPROFESIONAL_ADMIN", 3600, aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_PRODUCCION_PORPROFESIONAL_USU", 3600, aParam);

        }

        public static DataSet ObtenerConsumosGASVI(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            
            return SqlHelper.ExecuteDataset("SUP_CONSUMOSGASVI_PSN", aParam);
        }
        public static int InsertConsumoGasvi(SqlTransaction tr, int t325_idsegmesproy,
                                    int t420_idreferencia,
                                    string t376_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t376_motivo", SqlDbType.Text, 50, t376_motivo);

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_I_GASVI", aParam));
        }

        public static decimal ObtenerPasoAProduccion(SqlTransaction tr, int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;

            if (tr==null)
                return Convert.ToDecimal(SqlHelper.ExecuteScalar("SUP_GETAVANCE_PASOPROD", aParam));
            else
                return Convert.ToDecimal(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GETAVANCE_PASOPROD", aParam));
        }
        /// <summary>
        /// Obtiene la diferencia entre el importe producido en PST y el valor acumulado en PGE en 
        /// "Producción-Otra producción-Avance tecnico PST" hasta el mes anterior
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="nPSN"></param>
        /// <param name="nAnomes"></param>
        /// <returns></returns>
        public static decimal GetProduccionAvanceTecnico(SqlTransaction tr,int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@nPSN", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@nAnomes", SqlDbType.Int, 4, nAnomes)
            };
            if (tr == null)
                return Convert.ToDecimal(SqlHelper.ExecuteScalar("SUP_GETAVANCE_PASOPROD_AVTEC", aParam));
            else
                return Convert.ToDecimal(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GETAVANCE_PASOPROD_AVTEC", aParam));
        }
        public static decimal GetImporte(SqlTransaction tr, int idClase, int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t329_idclaseeco", SqlDbType.Int, 4, idClase),
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@t325_anomes", SqlDbType.Int, 4, nAnomes)
            };
            if (tr == null)
                return Convert.ToDecimal(SqlHelper.ExecuteScalar("SUP_DATOECO_IMPORTE_S", aParam));
            else
                return Convert.ToDecimal(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECO_IMPORTE_S", aParam));
        }

        #endregion
    }
}
