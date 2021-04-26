using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for FasePsp
/// </summary>
/// 
namespace SUPER.Capa_Negocio
{

    public partial class FASEPSP
    {
        #region Atributos y Propiedades complementarios

        private int _t334_idfase;
        public int t334_idfase
        {
            get { return _t334_idfase; }
            set { _t334_idfase = value; }
        }

        private string _t334_desfase;
        public string t334_desfase
        {
            get { return _t334_desfase; }
            set { _t334_desfase = value; }
        }

        private short _t334_orden;
        public short t334_orden
        {
            get { return _t334_orden; }
            set { _t334_orden = value; }
        }

        private string _t334_desfaselong;
        public string t334_desfaselong
        {
            get { return _t334_desfaselong; }
            set { _t334_desfaselong = value; }
        }

        private string _t334_observaciones;
        public string t334_observaciones
        {
            get { return _t334_observaciones; }
            set { _t334_observaciones = value; }
        }

        private short _t303_idnodo;
        public short t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }
        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }
        private string _t305_cualidad;
        public string t305_cualidad
        {
            get { return _t305_cualidad; }
            set { _t305_cualidad = value; }
        }

        private int _num_proyecto;
        public int num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
        }

        private string _nom_proyecto;
        public string nom_proyecto
        {
            get { return _nom_proyecto; }
            set { _nom_proyecto = value; }
        }

        private int _t331_idpt;
        public int t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        private string _t331_despt;
        public string t331_despt
        {
            get { return _t331_despt; }
            set { _t331_despt = value; }
        }

        private string _sPromotor;
        public string sPromotor
        {
            get { return _sPromotor; }
            set { _sPromotor = value; }
        }

        private string _sModificador;
        public string sModificador
        {
            get { return _sModificador; }
            set { _sModificador = value; }
        }

        private DateTime _t332_fiv;
        public DateTime t332_fiv
        {
            get { return _t332_fiv; }
            set { _t332_fiv = value; }
        }

        private DateTime _t332_ffv;
        public DateTime t332_ffv
        {
            get { return _t332_ffv; }
            set { _t332_ffv = value; }
        }

        private DateTime _t332_fipl;
        public DateTime t332_fipl
        {
            get { return _t332_fipl; }
            set { _t332_fipl = value; }
        }

        private DateTime _t332_ffpl;
        public DateTime t332_ffpl
        {
            get { return _t332_ffpl; }
            set { _t332_ffpl = value; }
        }

        private double _t332_etpl;
        public double t332_etpl
        {
            get { return _t332_etpl; }
            set { _t332_etpl = value; }
        }

        private DateTime _t332_ffpr;
        public DateTime t332_ffpr
        {
            get { return _t332_ffpr; }
            set { _t332_ffpr = value; }
        }

        private double _t332_etpr;
        public double t332_etpr
        {
            get { return _t332_etpr; }
            set { _t332_etpr = value; }
        }
        private DateTime _dPrimerConsumo;
        public DateTime dPrimerConsumo
        {
            get { return _dPrimerConsumo; }
            set { _dPrimerConsumo = value; }
        }

        private DateTime _dUltimoConsumo;
        public DateTime dUltimoConsumo
        {
            get { return _dUltimoConsumo; }
            set { _dUltimoConsumo = value; }
        }

        private DateTime _dFinEstimado;
        public DateTime dFinEstimado
        {
            get { return _dFinEstimado; }
            set { _dFinEstimado = value; }
        }

        private double _nTotalEstimado;
        public double nTotalEstimado
        {
            get { return _nTotalEstimado; }
            set { _nTotalEstimado = value; }
        }

        private double _nConsumidoHoras;
        public double nConsumidoHoras
        {
            get { return _nConsumidoHoras; }
            set { _nConsumidoHoras = value; }
        }

        private double _nPendienteEstimado;
        public double nPendienteEstimado
        {
            get { return _nPendienteEstimado; }
            set { _nPendienteEstimado = value; }
        }

        private double _nConsumidoJornadas;
        public double nConsumidoJornadas
        {
            get { return _nConsumidoJornadas; }
            set { _nConsumidoJornadas = value; }
        }

        private double _nAvanceTeorico;
        public double nAvanceTeorico
        {
            get { return _nAvanceTeorico; }
            set { _nAvanceTeorico = value; }
        }
        private bool _t331_heredanodo;
        public bool t331_heredanodo
        {
            get { return _t331_heredanodo; }
            set { _t331_heredanodo = value; }
        }
        private bool _t331_heredaproyeco;
        public bool t331_heredaproyeco
        {
            get { return _t331_heredaproyeco; }
            set { _t331_heredaproyeco = value; }
        }
        private bool _t334_heredanodo;
        public bool t334_heredanodo
        {
            get { return _t334_heredanodo; }
            set { _t334_heredanodo = value; }
        }
        private bool _t334_heredaproyeco;
        public bool t334_heredaproyeco
        {
            get { return _t334_heredaproyeco; }
            set { _t334_heredaproyeco = value; }
        }
        private bool _t305_admiterecursospst;
        public bool t305_admiterecursospst
        {
            get { return _t305_admiterecursospst; }
            set { _t305_admiterecursospst = value; }
        }
        private bool _t305_avisorecursopst;
        public bool t305_avisorecursopst
        {
            get { return _t305_avisorecursopst; }
            set { _t305_avisorecursopst = value; }
        }
        //Estado del proyecto económico
        private string _t301_estado;
        public string t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }

        private int _cod_cliente;
        public int cod_cliente
        {
            get { return _cod_cliente; }
            set { _cod_cliente = value; }
        }
        private int _t346_idpst;
        public int t346_idpst
        {
            get { return _t346_idpst; }
            set { _t346_idpst = value; }
        }
        private string _t346_codpst;
        public string t346_codpst
        {
            get { return _t346_codpst; }
            set { _t346_codpst = value; }
        }
        private string _t346_despst;
        public string t346_despst
        {
            get { return _t346_despst; }
            set { _t346_despst = value; }
        }
        private bool _bOTCHeredada;
        public bool bOTCHeredada
        {
            get { return _bOTCHeredada; }
            set { _bOTCHeredada = value; }
        }

        private bool _t301_esreplicable;
        public bool t301_esreplicable
        {
            get { return _t301_esreplicable; }
            set { _t301_esreplicable = value; }
        }

        private string _t305_nivelpresupuesto;
        public string t305_nivelpresupuesto
        {
            get { return _t305_nivelpresupuesto; }
            set { _t305_nivelpresupuesto = value; }
        }

        private double _nPresupuestoT;
        public double nPresupuestoT
        {
            get { return _nPresupuestoT; }
            set { _nPresupuestoT = value; }
        }

        private double _nPresupuestoA;
        public double nPresupuestoA
        {
            get { return _nPresupuestoA; }
            set { _nPresupuestoA = value; }
        }

        private double _nPresupuesto;
        public double nPresupuesto
        {
            get { return _nPresupuesto; }
            set { _nPresupuesto = value; }
        }

        private double _t334_avance;
        public double t334_avance
        {
            get { return _t334_avance; }
            set { _t334_avance = value; }
        }

        private bool _t334_avanceauto;
        public bool t334_avanceauto
        {
            get { return _t334_avanceauto; }
            set { _t334_avanceauto = value; }
        }

        #endregion

        #region Metodos Básicos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla t334_FASEPSP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	21/11/2007 11:38:57
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t334_desfase, short t334_orden, string t334_desfaselong,
                                 bool bHeredaNodo, bool bHeredaPE, Nullable<int> t346_idpst)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t334_desfase", SqlDbType.Text, 100);
            aParam[0].Value = t334_desfase;
            aParam[1] = new SqlParameter("@t334_orden", SqlDbType.SmallInt, 2);
            aParam[1].Value = t334_orden;
            aParam[2] = new SqlParameter("@t334_desfaselong", SqlDbType.Text, 2147483647);
            aParam[2].Value = t334_desfaselong;
            aParam[3] = new SqlParameter("@t334_heredanodo", SqlDbType.Bit, 1);
            aParam[3].Value = bHeredaNodo;
            aParam[4] = new SqlParameter("@t334_heredaproyeco", SqlDbType.Bit, 1);
            aParam[4].Value = bHeredaPE;
            aParam[5] = new SqlParameter("@t346_idpst", SqlDbType.Int, 1);
            aParam[5].Value = t346_idpst;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FASESUP_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FASESUP_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla t334_FASEPSP
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	21/11/2007 11:38:57
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t334_idfase, string t334_desfase, short t334_orden, string t334_desfaselong,
                                 bool bHeredaNodo, bool bHeredaPE, Nullable<int> t346_idpst, decimal strPresupuesto, decimal fAvance, bool bAvanceAuto, string t334_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;
            aParam[1] = new SqlParameter("@t334_desfase", SqlDbType.Text, 100);
            aParam[1].Value = t334_desfase;
            aParam[2] = new SqlParameter("@t334_orden", SqlDbType.SmallInt, 2);
            aParam[2].Value = t334_orden;
            aParam[3] = new SqlParameter("@t334_desfaselong", SqlDbType.Text, 2147483647);
            aParam[3].Value = t334_desfaselong;
            aParam[4] = new SqlParameter("@t334_heredanodo", SqlDbType.Bit, 1);
            aParam[4].Value = bHeredaNodo;
            aParam[5] = new SqlParameter("@t334_heredaproyeco", SqlDbType.Bit, 1);
            aParam[5].Value = bHeredaPE;
            aParam[6] = new SqlParameter("@t346_idpst", SqlDbType.Int, 1);
            aParam[6].Value = t346_idpst;
            aParam[7] = new SqlParameter("@t334_presupuesto", SqlDbType.Money, 8);
            aParam[7].Value = strPresupuesto;
            aParam[8] = new SqlParameter("@t334_avance", SqlDbType.Float, 8);
            aParam[8].Value = fAvance;
            aParam[9] = new SqlParameter("@t334_avanceauto", SqlDbType.Bit, 1);
            aParam[9].Value = bAvanceAuto;
            aParam[10] = new SqlParameter("@t334_observaciones", SqlDbType.Text, 2147483647);
            aParam[10].Value = t334_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FASESUP_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASESUP_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla t334_FASEPSP a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	21/11/2007 11:38:57
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t334_idfase)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FASESUP_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASESUP_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla t334_FASEPSP,
        /// y devuelve una instancia u objeto del tipo FASEPSP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	21/11/2007 11:38:57
        /// </history>
        /// -----------------------------------------------------------------------------
        public static FASEPSP Select(SqlTransaction tr, int t334_idfase)
        {
            FASEPSP o = new FASEPSP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_FASESUP_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FASESUP_S", aParam);

            if (dr.Read())
            {
                if (dr["t334_idfase"] != DBNull.Value)
                    o.t334_idfase = (int)dr["t334_idfase"];
                if (dr["t334_desfase"] != DBNull.Value)
                    o.t334_desfase = (string)dr["t334_desfase"];
                if (dr["t334_orden"] != DBNull.Value)
                    o.t334_orden = short.Parse(dr["t334_orden"].ToString());
                if (dr["t334_desfaselong"] != DBNull.Value)
                    o.t334_desfaselong = (string)dr["t334_desfaselong"];
                if (dr["t334_heredanodo"] != DBNull.Value)
                    o.t334_heredanodo = (bool)dr["t334_heredanodo"];
                if (dr["t334_heredaproyeco"] != DBNull.Value)
                    o.t334_heredaproyeco = (bool)dr["t334_heredaproyeco"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de FASEPSP"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        #endregion

        #region	Métodos públicos
        public static FASEPSP Obtener(int t334_idfase)
        {
            FASEPSP o = new FASEPSP();
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;
            //Obtengo los datos de la fase
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_FASESUP_S", aParam);
            if (dr.Read())
            {
                if (dr["t331_idpt"] != DBNull.Value)
                    o.t331_idpt = (int)dr["t331_idpt"];
                if (dr["t331_despt"] != DBNull.Value)
                    o.t331_despt = (string)dr["t331_despt"];
                if (dr["t334_idfase"] != DBNull.Value)
                    o.t334_idfase = (int)dr["t334_idfase"];
                if (dr["t334_desfase"] != DBNull.Value)
                    o.t334_desfase = (string)dr["t334_desfase"];
                if (dr["t334_observaciones"] != DBNull.Value)
                    o.t334_observaciones = (string)dr["t334_observaciones"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["num_proyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["num_proyecto"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["nom_proyecto"] != DBNull.Value)
                    o.nom_proyecto = (string)dr["nom_proyecto"];
                if (dr["t334_orden"] != DBNull.Value)
                    o.t334_orden = short.Parse(dr["t334_orden"].ToString());
                else o.t334_orden = 0;
                if (dr["t334_desfaselong"] != DBNull.Value)
                    o.t334_desfaselong = (string)dr["t334_desfaselong"];
                if (dr["t334_heredanodo"] != DBNull.Value)
                    o.t334_heredanodo = (bool)dr["t334_heredanodo"];
                if (dr["t331_heredanodo"] != DBNull.Value)
                    o.t331_heredanodo = (bool)dr["t331_heredanodo"];
                if (dr["t334_heredaproyeco"] != DBNull.Value)
                    o.t334_heredaproyeco = (bool)dr["t334_heredaproyeco"];
                if (dr["t331_heredaproyeco"] != DBNull.Value)
                    o.t331_heredaproyeco = (bool)dr["t331_heredaproyeco"];
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];

                if (dr["PT_t346_idpst"] != DBNull.Value)
                    o.bOTCHeredada = true;
                else
                    o.bOTCHeredada = false;

                if (dr["t346_idpst"] == DBNull.Value)
                {
                    //if (dr["PT_t346_idpst"] != DBNull.Value)
                    //{
                    //    o.bOTCHeredada = true;
                    //    o.t346_idpst = (int)dr["PT_t346_idpst"];
                    //}
                    //if (dr["PT_t346_codpst"] != DBNull.Value)
                    //    o.t346_codpst = (string)dr["PT_t346_codpst"];
                    //if (dr["PT_t346_despst"] != DBNull.Value)
                    //    o.t346_despst = (string)dr["PT_t346_despst"];
                }
                else
                {
                    o.t346_idpst = (int)dr["t346_idpst"];
                    if (dr["t346_codpst"] != DBNull.Value)
                        o.t346_codpst = (string)dr["t346_codpst"];
                    if (dr["t346_despst"] != DBNull.Value)
                        o.t346_despst = (string)dr["t346_despst"];
                }

                if (dr["cod_cliente"] != DBNull.Value)
                    o.cod_cliente = (int)dr["cod_cliente"];
                if (dr["t301_esreplicable"] != DBNull.Value)
                    o.t301_esreplicable = (bool)dr["t301_esreplicable"];

                if (dr["t305_nivelpresupuesto"] != DBNull.Value)
                    o.t305_nivelpresupuesto = (string)dr["t305_nivelpresupuesto"];
                
                if (dr["t334_presupuesto"] != DBNull.Value)
                    o.nPresupuesto = double.Parse(dr["t334_presupuesto"].ToString());

                if (dr["t334_avance"] != DBNull.Value)
                    o.t334_avance = double.Parse(dr["t334_avance"].ToString());
                if (dr["t334_avanceauto"] != DBNull.Value)
                    o.t334_avanceauto = (bool)dr["t334_avanceauto"];

                //Obtengo los datos de LAS TAREAS referentes a la fase
                dr.Close();

                SqlParameter[] aParam1 = new SqlParameter[2];
                aParam1[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
                aParam1[0].Value = t334_idfase;
                aParam1[1] = new SqlParameter("@nivelpresupuesto", SqlDbType.Char, 1);
                aParam1[1].Value = o.t305_nivelpresupuesto;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_FASE_TAREAS", aParam1);
                if (dr.Read())
                {
                    if (dr["dVigIni"] != DBNull.Value)
                        o.t332_fiv = (DateTime)dr["dVigIni"];
                    if (dr["dVigFin"] != DBNull.Value)
                        o.t332_ffv = (DateTime)dr["dVigFin"];
                    if (dr["dPlanIni"] != DBNull.Value)
                        o.t332_fipl = (DateTime)dr["dPlanIni"];
                    if (dr["dPlanFin"] != DBNull.Value)
                        o.t332_ffpl = (DateTime)dr["dPlanFin"];
                    if (dr["nPlanEstimado"] != DBNull.Value)
                        o.t332_etpl = double.Parse(dr["nPlanEstimado"].ToString());
                    if (dr["dPrevFin"] != DBNull.Value)
                        o.t332_ffpr = (DateTime)dr["dPrevFin"];
                    if (dr["nPrevEstimado"] != DBNull.Value)
                        o.t332_etpr = double.Parse(dr["nPrevEstimado"].ToString());
                    if (dr["nPresupuesto"] != DBNull.Value)
                        o.nPresupuestoT = double.Parse(dr["nPresupuesto"].ToString());
                    
                }

                dr.Close();

                //En caso de queel nivel de presupuesto esté establecido para las activiades, se llama a otro procedure que realiza este calculo
                switch (o.t305_nivelpresupuesto.ToString())
                {
                    case "A":
                        SqlParameter[] aParam3 = new SqlParameter[1];
                        aParam3[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
                        aParam3[0].Value = t334_idfase;
                        dr = SqlHelper.ExecuteSqlDataReader("SUP_FASE_PRESUPACUM_ACTIVIDAD", aParam3);
                        if (dr.Read())
                        {
                            if (dr["nPresupuesto"] != DBNull.Value)
                                o.nPresupuesto = double.Parse(dr["nPresupuesto"].ToString());

                        }
                        //Obtengo la suma de los presupuestos de las actividades asociadas
                        dr.Close();
                        break;

                    case "T":

                        o.nPresupuesto = o.nPresupuestoT;
                        break;
                }

                //Obtengo los datos del IAP referentes a la fase                
                SqlParameter[] aParam2 = new SqlParameter[1];
                aParam2[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
                aParam2[0].Value = t334_idfase;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_FASEIAPS", aParam2);
                if (dr.Read())
                {
                    if (dr["dPrimerConsumo"] != DBNull.Value)
                        o.dPrimerConsumo = (DateTime)dr["dPrimerConsumo"];
                    if (dr["dUltimoConsumo"] != DBNull.Value)
                        o.dUltimoConsumo = (DateTime)dr["dUltimoConsumo"];
                    if (dr["dFinEstimado"] != DBNull.Value)
                        o.dFinEstimado = (DateTime)dr["dFinEstimado"];
                    if (dr["nTotalEstimado"] != DBNull.Value)
                        o.nTotalEstimado = double.Parse(dr["nTotalEstimado"].ToString());
                    if (dr["nConsumidoHoras"] != DBNull.Value)
                        o.nConsumidoHoras = double.Parse(dr["nConsumidoHoras"].ToString());
                    if (dr["nPendienteEstimado"] != DBNull.Value)
                        o.nPendienteEstimado = double.Parse(dr["nPendienteEstimado"].ToString());
                    if (dr["nConsumidoJornadas"] != DBNull.Value)
                        o.nConsumidoJornadas = double.Parse(dr["nConsumidoJornadas"].ToString());
                    if (dr["nAvanceTeorico"] != DBNull.Value)
                        o.nAvanceTeorico = double.Parse(dr["nAvanceTeorico"].ToString());
                    dr.Close();
                }
                dr.Dispose();
            }
            else { throw (new NullReferenceException("No se ha obtenido ningun dato de Fase")); }
            return o;

        }
        public static SqlDataReader CatalogoRecursos(int nIdFase, bool bMostrarBajas)
        {//Obtine los recursos asociados al Proyecto técnico
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[0].Value = nIdFase;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_FASERECURSOCATA", aParam);
        }
        /*public static SqlDataReader ObtenerRelacionTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3
                                                            , bool bRepPreCerr, Nullable<int> t303_idnodo)
        {//Obtiene la relación de técnicos según parametro
            //Si strOpcion=N es una busqueda por nombre
            //Si strOpcion=C es una busqueda por CR
            //Si strOpcion=G es una busqueda por Grupo Funcional
            //Si strOpcion=P es una busqueda por recursos asociados al proyecto económico
            //Si strOpcion=T es una busqueda por recursos asociados al proyecto técnico
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
            aParam[1] = new SqlParameter("@sValor1", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@sValor2", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@sValor3", SqlDbType.VarChar, 50);
            aParam[4] = new SqlParameter("@bRepPreCerr", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);

            aParam[0].Value = strOpcion;
            aParam[1].Value = strValor1;
            aParam[2].Value = strValor2;
            aParam[3].Value = strValor3;
            aParam[4].Value = bRepPreCerr;
            aParam[5].Value = t303_idnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_FASETECNICOS", aParam);
        }
        */
        public static SqlDataReader CatalogoTareas(SqlTransaction tr, int nIdFase)
        {//Obtiene las tareas asociadas a la fase y sus consumos
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = nIdFase;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FASE_TAREA_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FASE_TAREA_S", aParam);
        }
        public static SqlDataReader CatalogoTareasFase(int nIdFase)
        {//Obtiene las actividades y tareas asociadas a la fase y sus consumos
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = nIdFase;

            return SqlHelper.ExecuteSqlDataReader("SUP_FASE_TAREA_S2", aParam);
        }
        public static void AsignarTareas(SqlTransaction tr, int iCodFase, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                         int iTarifa, string sIndicaciones, bool bNotifExceso)
        {//Asigna recursos a todas las tareas de una fase
            int iCodTarea, iNumAsig;
            string oRec;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = iCodFase;
            //Recorro todas las tareas de la fase
            SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S3", aParam);
            while (dr2.Read())
            {
                iCodTarea = int.Parse(dr2["t332_idtarea"].ToString());
                if (iTarifa == -1)
                    iNumAsig = TareaRecurso.InsertarSNE(tr, iCodTarea, iCodRecurso, null, null, null, dtFip, dtFfp, null, 1, "", sIndicaciones, bNotifExceso);
                else
                    iNumAsig = TareaRecurso.InsertarSNE(tr, iCodTarea, iCodRecurso, null, null, null, dtFip, dtFfp, iTarifa, 1, "", sIndicaciones, bNotifExceso);
                if (iNumAsig != 0)
                {//SOLO ENVIAMOS CORREO SI EL RECURSO NO ESTABA ASOCIADO A LA TAREA
                    oRec = "##" + iCodTarea.ToString() + "##" + iCodRecurso.ToString() + "################";
                    oRec += dr2["t332_destarea"].ToString() + "##";
                    oRec += dr2["num_proyecto"].ToString() + "##" + dr2["nom_proyecto"].ToString() + "##";
                    oRec += dr2["t331_despt"].ToString() + "##";
                    oRec += dr2["t334_desfase"].ToString() + "##" + dr2["t335_desactividad"].ToString() + "##";
                    oRec += dr2["t346_codpst"].ToString() + "##" + dr2["t346_despst"].ToString() + "##";
                    oRec += dr2["t332_otl"].ToString() + "##" + dr2["t332_incidencia"].ToString() + "##";

                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, null, dtFip.ToString(), dtFfp.ToString(), sIndicaciones, dr2["t332_mensaje"].ToString());
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al asignar técnicos a tareas.", ex);
            //}
        }
        public static void AsignarTareas2(SqlTransaction tr, int iCodFase, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de una fase que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);

            aParam[0].Value = iCodFase;
            //Recorro todas las tareas del Proyecto Técnico
            //SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREASUP_SByt334_idfase", aParam);
            SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S3", aParam);
            while (dr2.Read())
            {
                iCodTarea = int.Parse(dr2["t332_idtarea"].ToString());
                bNotifProf = (bool)dr2["t332_notif_prof"];
                if (iTarifa == -1)
                    nIdTarif = null;
                else
                    nIdTarif = iTarifa;

                bRecursoAsignado = TareaRecurso.InsertarTEC(tr, iCodTarea, iCodRecurso, null, null, null, dtFip, dtFfp, nIdTarif, 1, "",
                                                            sIndicaciones, bNotifExceso, bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                if (bRecursoAsignado && bNotifProf)
                {//SOLO ENVIAMOS CORREO SI EL RECURSO NO ESTABA ASOCIADO A LA TAREA
                    oRec = "##" + iCodTarea.ToString() + "##" + iCodRecurso.ToString() + "################";
                    oRec += Utilidades.escape(dr2["t332_destarea"].ToString()) + "##";
                    oRec += dr2["num_proyecto"].ToString() + "##" + Utilidades.escape(dr2["nom_proyecto"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t331_despt"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t334_desfase"].ToString()) + "##" + Utilidades.escape(dr2["t335_desactividad"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t346_codpst"].ToString()) + "##" + Utilidades.escape(dr2["t346_despst"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t332_otl"].ToString()) + "##" + Utilidades.escape(dr2["t332_incidencia"].ToString()) + "##";

                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, null, dtFip.ToString(), dtFfp.ToString(),
                                                     sIndicaciones, Utilidades.escape(dr2["t332_mensaje"].ToString()));
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al asignar técnicos a tareas.", ex);
            //}
        }
        public static void AsignarTareasProfesional(SqlTransaction tr, bool bSoloAsignadas, bool bSoloActivas, int iRecursoOrigen, 
                                          int iCodFase, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de una fase que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //Recorro todas las tareas del Proyecto Técnico
            List<SUPER.Capa_Negocio.TAREAPSP> oLista = SUPER.DAL.Fase.GetTareasVivas(tr, iCodFase, iRecursoOrigen, bSoloAsignadas, bSoloActivas);
            foreach (SUPER.Capa_Negocio.TAREAPSP oTarea in oLista)
            {
                iCodTarea = oTarea.t332_idtarea;
                bNotifProf = oTarea.t332_notif_prof;
                if (iTarifa == -1)
                    nIdTarif = null;
                else
                    nIdTarif = iTarifa;

                bRecursoAsignado = TareaRecurso.InsertarTEC(tr, iCodTarea, iCodRecurso, null, null, null, dtFip, dtFfp, nIdTarif, 1, "",
                                                            sIndicaciones, bNotifExceso, bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                if (bRecursoAsignado && bNotifProf)
                {//SOLO ENVIAMOS CORREO SI EL RECURSO NO ESTABA ASOCIADO A LA TAREA
                    oRec = "##" + iCodTarea.ToString() + "##" + iCodRecurso.ToString() + "################";
                    oRec += oTarea.t332_destarea + "##";
                    oRec += oTarea.num_proyecto.ToString() + "##" + oTarea.nom_proyecto + "##";
                    oRec += oTarea.t331_despt + "##";
                    oRec += oTarea.t334_desfase + "##" + oTarea.t335_desactividad + "##";
                    oRec += oTarea.t346_codpst + "##" + oTarea.t346_despst + "##";
                    oRec += oTarea.t332_otl + "##" + oTarea.t332_incidencia + "##";

                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, null, dtFip.ToString(), dtFfp.ToString(),
                                                     sIndicaciones, Utilidades.escape(oTarea.t332_mensaje));
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al asignar técnicos a tareas.", ex);
            //}
        }
        public static void EstadoRecursos(SqlTransaction tr, int iCodFase, int iCodRecurso, string sEstado)
        {//Cambia el estado a todos las tareas/recurso de la fase 
            int iEstado;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);

            aParam[0].Value = iCodFase;
            aParam[1].Value = iCodRecurso;
            if (sEstado == "A") iEstado = 1;
            else iEstado = 0;
            aParam[2].Value = iEstado;

            int nResul = 0;
            if (tr == null)
                nResul = SqlHelper.ExecuteNonQuery("SUP_FASERECURSO_ESTADO", aParam);
            else
                nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASERECURSO_ESTADO", aParam);
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al desasignar técnicos a tareas.", ex);
            //}
        }

        /// <summary>
        /// 
        /// Obtiene las tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Fase
        /// </summary>
        /// 
        public static SqlDataReader CatalogoTareasRecurso(int iCodFase, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = iCodFase;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            return SqlHelper.ExecuteSqlDataReader("SUP_ASIGTAREA_F", aParam);
        }

        /// <summary>
        /// 
        /// Obtiene el número de tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Fase
        /// </summary>
        /// 
        public static int NumeroTareasRecurso(SqlTransaction tr, int iCodFase, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = iCodFase;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASIGTAREACOUNT_F", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASIGTAREACOUNT_F", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla fases.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> cod_fase, string nom_fase, int cod_pt, byte nOrden, byte nAscDesc, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@cod_fase", SqlDbType.Int, 4);
            aParam[0].Value = cod_fase;
            aParam[1] = new SqlParameter("@nom_fase", SqlDbType.Text, 50);
            aParam[1].Value = nom_fase;
            aParam[2] = new SqlParameter("@cod_pt", SqlDbType.Int, 4);
            aParam[2].Value = cod_pt;

            aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[3].Value = nOrden;
            aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[4].Value = nAscDesc;
            aParam[5] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[5].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FASE_C", aParam);
        }
        /// <summary>
        /// Calcula el estado de una fase en base al estado de sus tareas
        ///     Si hay alguna activa (t332_estado=1) -> estado 0 (En curso)
        ///     Sino -> Estado 1 (Completada)
        /// </summary>
        /// <param name="t334_idfase"></param>
        /// <returns></returns>
        public static int GetEstado(SqlTransaction tr, int t334_idfase)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t334_idfase", SqlDbType.Int, 4, t334_idfase)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FASE_ESTADO", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FASE_ESTADO", aParam));
        }
        #endregion
    }
}