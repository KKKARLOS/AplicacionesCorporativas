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
/// Summary description for ActividadPsp
/// </summary>
namespace SUPER.Capa_Negocio
{

    public partial class ACTIVIDADPSP
    {
        #region Atributos y Propiedades complementarios

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

        private string _t334_desfase;
        public string t334_desfase
        {
            get { return _t334_desfase; }
            set { _t334_desfase = value; }
        }
        private int _t335_idactividad;
        public int t335_idactividad
        {
            get { return _t335_idactividad; }
            set { _t335_idactividad = value; }
        }

        private string _t335_desactividad;
        public string t335_desactividad
        {
            get { return _t335_desactividad; }
            set { _t335_desactividad = value; }
        }

        private string _t335_observaciones;
        public string t335_observaciones
        {
            get { return _t335_observaciones; }
            set { _t335_observaciones = value; }
        }

        private int _t334_idfase;
        public int t334_idfase
        {
            get { return _t334_idfase; }
            set { _t334_idfase = value; }
        }

        private short _t335_orden;
        public short t335_orden
        {
            get { return _t335_orden; }
            set { _t335_orden = value; }
        }

        private string _t335_desactividadlong;
        public string t335_desactividadlong
        {
            get { return _t335_desactividadlong; }
            set { _t335_desactividadlong = value; }
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
        private bool _heredaCR;
        public bool heredaCR
        {
            get { return _heredaCR; }
            set { _heredaCR = value; }
        }
        private bool _heredaPE;
        public bool heredaPE
        {
            get { return _heredaPE; }
            set { _heredaPE = value; }
        }
        private bool _t335_heredanodo;
        public bool t335_heredanodo
        {
            get { return _t335_heredanodo; }
            set { _t335_heredanodo = value; }
        }
        private bool _t335_heredaproyeco;
        public bool t335_heredaproyeco
        {
            get { return _t335_heredaproyeco; }
            set { _t335_heredaproyeco = value; }
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

        private double _nPresupuesto;
        public double nPresupuesto
        {
            get { return _nPresupuesto; }
            set { _nPresupuesto = value; }
        }

        private double _t335_avance;
        public double t335_avance
        {
            get { return _t335_avance; }
            set { _t335_avance = value; }
        }

        private bool _t335_avanceauto;
        public bool t335_avanceauto
        {
            get { return _t335_avanceauto; }
            set { _t335_avanceauto = value; }
        }

        #endregion

        #region Metodos Básicos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla t335_ACTIVIDADPSP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	07/11/2006 16:18:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t335_desactividad, Nullable<int> t334_idfase, short t335_orden,
                                string t335_desactividadlong, bool bHeredaNodo, bool bHeredaPE, Nullable<int> t346_idpst)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t335_desactividad", SqlDbType.Text, 100);
            aParam[0].Value = t335_desactividad;
            aParam[1] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[1].Value = t334_idfase;
            aParam[2] = new SqlParameter("@t335_orden", SqlDbType.SmallInt, 2);
            aParam[2].Value = t335_orden;
            aParam[3] = new SqlParameter("@t335_desactividadlong", SqlDbType.Text, 2147483647);
            aParam[3].Value = t335_desactividadlong;
            aParam[4] = new SqlParameter("@t335_heredanodo", SqlDbType.Bit, 1);
            aParam[4].Value = bHeredaNodo;
            aParam[5] = new SqlParameter("@t335_heredaproyeco", SqlDbType.Bit, 1);
            aParam[5].Value = bHeredaPE;
            aParam[6] = new SqlParameter("@t346_idpst", SqlDbType.Int, 1);
            aParam[6].Value = t346_idpst;            

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACTIVIDADPSP_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACTIVIDADPSP_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla t335_ACTIVIDADPSP
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	07/11/2006 16:18:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t335_idactividad, string t335_desactividad, Nullable<int> t334_idfase,
                                 short t335_orden, string t335_desactividadlong, bool bHeredaNodo, bool bHeredaPE, Nullable<int> t346_idpst, decimal t335_presupuesto, decimal t335_avance, bool bAvanceAuto, string t335_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = t335_idactividad;
            aParam[1] = new SqlParameter("@t335_desactividad", SqlDbType.Text, 100);
            aParam[1].Value = t335_desactividad;
            aParam[2] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[2].Value = t334_idfase;
            aParam[3] = new SqlParameter("@t335_orden", SqlDbType.SmallInt, 2);
            aParam[3].Value = t335_orden;
            aParam[4] = new SqlParameter("@t335_desactividadlong", SqlDbType.Text, 2147483647);
            aParam[4].Value = t335_desactividadlong;
            aParam[5] = new SqlParameter("@t335_heredanodo", SqlDbType.Bit, 1);
            aParam[5].Value = bHeredaNodo;
            aParam[6] = new SqlParameter("@t335_heredaproyeco", SqlDbType.Bit, 1);
            aParam[6].Value = bHeredaPE;
            aParam[7] = new SqlParameter("@t346_idpst", SqlDbType.Int, 1);
            aParam[7].Value = t346_idpst;
            aParam[8] = new SqlParameter("@t335_presupuesto", SqlDbType.Money, 8);
            aParam[8].Value = t335_presupuesto;
            aParam[9] = new SqlParameter("@t335_avance", SqlDbType.Float, 8);
            aParam[9].Value = t335_avance;
            aParam[10] = new SqlParameter("t335_avanceauto", SqlDbType.Bit, 1);
            aParam[10].Value = bAvanceAuto;
            aParam[11] = new SqlParameter("t335_observaciones", SqlDbType.Text, 2147483647);
            aParam[11].Value = t335_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACTIVIDADPSP_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDADPSP_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla t335_ACTIVIDADPSP a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	07/11/2006 16:18:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t335_idactividad)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = t335_idactividad;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACTIVIDADPSP_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDADPSP_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla t335_ACTIVIDADPSP,
        /// y devuelve una instancia u objeto del tipo ACTIVIDADPSP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	07/11/2006 16:18:23
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static ACTIVIDADPSP Select(SqlTransaction tr, int t335_idactividad)
        //{
        //    ACTIVIDADPSP o = new ACTIVIDADPSP();

        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
        //    aParam[0].Value = t335_idactividad;

        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADPSP_S", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACTIVIDADPSP_S", aParam);

        //    if (dr.Read())
        //    {
        //        if (dr["t335_idactividad"] != DBNull.Value)
        //            o.t335_idactividad = (int)dr["t335_idactividad"];
        //        if (dr["t335_desactividad"] != DBNull.Value)
        //            o.t335_desactividad = (string)dr["t335_desactividad"];
        //        if (dr["t334_idfase"] != DBNull.Value)
        //            o.t334_idfase = (int)dr["t334_idfase"];
        //        if (dr["t335_orden"] != DBNull.Value)
        //            o.t335_orden = short.Parse(dr["t335_orden"].ToString());
        //        if (dr["t335_desactividadlong"] != DBNull.Value)
        //            o.t335_desactividadlong = (string)dr["t335_desactividadlong"];
        //        if (dr["t335_heredanodo"] != DBNull.Value)
        //            o.t335_heredanodo = (bool)dr["t335_heredanodo"];
        //        if (dr["t335_heredaproyeco"] != DBNull.Value)
        //            o.t335_heredaproyeco = (bool)dr["t335_heredaproyeco"];
        //    }
        //    else
        //    {
        //        throw (new NullReferenceException("No se ha obtenido ningun dato de ACTIVIDADPSP"));
        //    }

        //    dr.Close();
        //    dr.Dispose();

        //    return o;
        //}

        #endregion

        #region	Métodos públicos
        public static ACTIVIDADPSP Obtener(int t335_idactividad)
        {
            ACTIVIDADPSP o = new ACTIVIDADPSP();
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = t335_idactividad;
            //Obtengo los datos de la fase
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADPSP_S", aParam);
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
                if (dr["t335_idactividad"] != DBNull.Value)
                    o.t335_idactividad = (int)dr["t335_idactividad"];
                if (dr["t335_desactividad"] != DBNull.Value)
                    o.t335_desactividad = (string)dr["t335_desactividad"];
                if (dr["t335_observaciones"] != DBNull.Value)
                    o.t335_observaciones = (string)dr["t335_observaciones"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["num_proyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["num_proyecto"];
                if (dr["nom_proyecto"] != DBNull.Value)
                    o.nom_proyecto = (string)dr["nom_proyecto"];
                if (dr["t335_orden"] != DBNull.Value)
                    o.t335_orden = short.Parse(dr["t335_orden"].ToString());
                else o.t335_orden = 0;
                if (dr["t335_desactividadlong"] != DBNull.Value)
                    o.t335_desactividadlong = (string)dr["t335_desactividadlong"];
                if (dr["t335_heredanodo"] != DBNull.Value)
                    o.t335_heredanodo = (bool)dr["t335_heredanodo"];
                //Lo puede estar heredando bien del PT bien de la Fase
                o.heredaCR = false;
                if (dr["t331_heredanodo"] != DBNull.Value)
                    o.heredaCR = (bool)dr["t331_heredanodo"];
                if (!o.heredaCR)
                {
                    if (dr["t334_heredanodo"] != DBNull.Value)
                        o.heredaCR = (bool)dr["t334_heredanodo"];
                }
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t335_heredaproyeco"] != DBNull.Value)
                    o.t335_heredaproyeco = (bool)dr["t335_heredaproyeco"];
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];
                //Lo puede estar heredando bien del PT bien de la Fase
                o.heredaPE = false;
                if (dr["t331_heredaproyeco"] != DBNull.Value)
                    o.heredaPE = (bool)dr["t331_heredaproyeco"];
                if (!o.heredaPE)
                {
                    if (dr["t334_heredaproyeco"] != DBNull.Value)
                        o.heredaPE = (bool)dr["t334_heredaproyeco"];
                }

                if ((dr["F_t346_idpst"] != DBNull.Value) || (dr["PT_t346_idpst"] != DBNull.Value))
                    o.bOTCHeredada = true;
                else
                    o.bOTCHeredada = false;
                if (dr["t346_idpst"] == DBNull.Value)
                {//Si no tiene PST a nivel de actividad, miro si tiene a nivel de Fase o PT
                    //if (dr["F_t346_idpst"] != DBNull.Value)
                    //{
                    //    o.t346_idpst = (int)dr["F_t346_idpst"];
                    //    if (dr["F_t346_codpst"] != DBNull.Value)
                    //        o.t346_codpst = (string)dr["F_t346_codpst"];
                    //    if (dr["F_t346_despst"] != DBNull.Value)
                    //        o.t346_despst = (string)dr["F_t346_despst"];
                    //}
                    //else
                    //{
                    //    if (dr["PT_t346_idpst"] != DBNull.Value)
                    //        o.t346_idpst = (int)dr["PT_t346_idpst"];
                    //    if (dr["PT_t346_codpst"] != DBNull.Value)
                    //        o.t346_codpst = (string)dr["PT_t346_codpst"];
                    //    if (dr["PT_t346_despst"] != DBNull.Value)
                    //        o.t346_despst = (string)dr["PT_t346_despst"];
                    //}
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
                if (dr["t335_presupuesto"] != DBNull.Value)
                    o.nPresupuesto = double.Parse(dr["t335_presupuesto"].ToString());
                else o.nPresupuesto = 0;
                if (dr["t335_avance"] != DBNull.Value)
                    o.t335_avance = double.Parse(dr["t335_avance"].ToString());
                if (dr["t335_avanceauto"] != DBNull.Value)
                    o.t335_avanceauto = (bool)dr["t335_avanceauto"];

                //Obtengo los datos de LAS TAREAS referentes a la fase
                dr.Close();
                SqlParameter[] aParam1 = new SqlParameter[1];
                aParam1[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
                aParam1[0].Value = t335_idactividad;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDAD_TAREAS", aParam1);
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
                    if (dr["nPresupuestoT"] != DBNull.Value)
                        o.nPresupuestoT = double.Parse(dr["nPresupuestoT"].ToString());
                }
                //Obtengo los datos del IAP referentes a la fase
                dr.Close();
                SqlParameter[] aParam2 = new SqlParameter[1];
                aParam2[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
                aParam2[0].Value = t335_idactividad;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADIAPS", aParam2);
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
            else { throw (new NullReferenceException("No se ha obtenido ningun dato de Actividad")); }
            return o;

        }
        public static SqlDataReader CatalogoRecursos(int nIdActividad, bool bMostrarBajas)
        {//Obtine los recursos asociados al Proyecto técnico
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[0].Value = nIdActividad;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADRECURSOCATA", aParam);
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

            return SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADTECNICOS", aParam);
        }
         * */
        public static SqlDataReader CatalogoTareas(int nIdActividad)
        {//Obtiene las tareas asociadas a la fase y sus consumos
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = nIdActividad;

            return SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDAD_TAREA_S", aParam);
        }
        public static void AsignarTareas(SqlTransaction tr, int iCodActividad, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                         int iTarifa, string sIndicaciones, bool bNotifExceso)
        {//Asigna recursos a todas las tareas de una fase
            int iCodTarea, iNumAsig;
            string oRec;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = iCodActividad;
            //Recorro todas las tareas de la fase
            //SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREASUP_SByt335_idactividad", aParam);
            SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S4", aParam);
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
                    oRec += Utilidades.escape(dr2["t332_destarea"].ToString()) + "##";
                    oRec += dr2["num_proyecto"].ToString() + "##" + Utilidades.escape(dr2["nom_proyecto"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t331_despt"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t334_desfase"].ToString()) + "##" + Utilidades.escape(dr2["t335_desactividad"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t346_codpst"].ToString()) + "##" + Utilidades.escape(dr2["t346_despst"].ToString()) + "##";
                    oRec += Utilidades.escape(dr2["t332_otl"].ToString()) + "##" + Utilidades.escape(dr2["t332_incidencia"].ToString()) + "##";

                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, null, dtFip.ToString(), dtFfp.ToString(), sIndicaciones, dr2["t332_mensaje"].ToString());
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al asignar técnicos a tareas.", ex);
            //}
        }
        public static void AsignarTareas2(SqlTransaction tr, int iCodActividad, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de una fase que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);

            aParam[0].Value = iCodActividad;
            //Recorro todas las tareas del Proyecto Técnico
            //SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREASUP_SByt335_idactividad", aParam);
            SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S4", aParam);
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
        /// <summary>
        /// Obtiene las tareas vivas de una Actividad
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t335_idactividad"></param>
        /// <param name="t314_idusuario">Restringe las tareas a las asignadas al profesional</param>
        /// <param name="bAsignadas">Indica que hay que restringir las tareas al profesional</param>
        /// <param name="bSoloActivas">Restringe las tareas a las que su asociación al profesional este activa</param>
        /// <returns></returns>
        public static List<SUPER.Capa_Negocio.TAREAPSP> GetTareasVivas(SqlTransaction tr, int t335_idactividad, int t314_idusuario,
                                                   bool bAsignadas, bool bSoloActivas)
        {
            SqlDataReader dr;
            if (bAsignadas)
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t335_idactividad", SqlDbType.Int, 4, t335_idactividad),
                    ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                    ParametroSql.add("@bSoloActivas", SqlDbType.Bit, 1, bSoloActivas)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAS_PROFESIONAL_ByActividad", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAS_PROFESIONAL_ByActividad", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t335_idactividad", SqlDbType.Int, 4, t335_idactividad)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S4", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_S4", aParam);
            }
            List<SUPER.Capa_Negocio.TAREAPSP> aTareas = new List<SUPER.Capa_Negocio.TAREAPSP>();
            SUPER.Capa_Negocio.TAREAPSP oTarea;

            while (dr.Read())
            {
                oTarea = new SUPER.Capa_Negocio.TAREAPSP();
                oTarea.t332_idtarea = int.Parse(dr["t332_idtarea"].ToString());
                oTarea.t332_notif_prof = (bool)dr["t332_notif_prof"];
                oTarea.t332_destarea = dr["t332_destarea"].ToString();
                oTarea.num_proyecto = int.Parse(dr["num_proyecto"].ToString());
                oTarea.nom_proyecto = dr["nom_proyecto"].ToString();
                oTarea.t331_despt = dr["t331_despt"].ToString();
                oTarea.t334_desfase = dr["t334_desfase"].ToString();
                oTarea.t335_desactividad = dr["t335_desactividad"].ToString();
                oTarea.t346_codpst = dr["t346_codpst"].ToString();
                oTarea.t346_despst = dr["t346_despst"].ToString();
                oTarea.t332_otl = dr["t332_otl"].ToString();
                oTarea.t332_incidencia = dr["t332_incidencia"].ToString();
                oTarea.t332_mensaje = dr["t332_mensaje"].ToString();

                aTareas.Add(oTarea);
            }
            dr.Close();
            dr.Dispose();

            return aTareas;
        }
        public static void AsignarTareasProfesional(SqlTransaction tr, bool bSoloAsignadas, bool bSoloActivas, int iRecursoOrigen, 
                                          int iCodActividad, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de una fase que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //try
            //{
            //Recorro todas las tareas del Proyecto Técnico
            List<SUPER.Capa_Negocio.TAREAPSP> oLista = SUPER.Capa_Negocio.ACTIVIDADPSP.GetTareasVivas(tr, iCodActividad, iRecursoOrigen, bSoloAsignadas, bSoloActivas);
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
        public static void EstadoRecursos(SqlTransaction tr, int iCodActividad, int iCodRecurso, string sEstado)
        {//Cambia el estado a todos las tareas/recurso de la fase 
            int iEstado;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);

            aParam[0].Value = iCodActividad;
            aParam[1].Value = iCodRecurso;
            if (sEstado == "A") iEstado = 1;
            else iEstado = 0;
            aParam[2].Value = iEstado;

            int nResul = 0;
            if (tr == null)
                nResul = SqlHelper.ExecuteNonQuery("SUP_ACTIVIDADRECURSO_ESTADO", aParam);
            else
                nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDADRECURSO_ESTADO", aParam);
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al desasignar técnicos a tareas.", ex);
            //}
        }

        /// <summary>
        /// 
        /// Obtiene las tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Actividad
        /// </summary>
        /// 
        public static SqlDataReader CatalogoTareasRecurso(int nIdActividad, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = nIdActividad;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            return SqlHelper.ExecuteSqlDataReader("SUP_ASIGTAREA_A", aParam);
        }

        /// <summary>
        /// 
        /// Obtiene el número de tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Actividad
        /// </summary>
        /// 
        public static int NumeroTareasRecurso(SqlTransaction tr, int nIdActividad, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = nIdActividad;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASIGTAREACOUNT_A", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASIGTAREACOUNT_A", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla actividades.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> cod_actividad, string nom_actividad, int cod_pt, Nullable<int> cod_fase, byte nOrden, byte nAscDesc, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@cod_actividad", SqlDbType.Int, 4);
            aParam[0].Value = cod_actividad;
            aParam[1] = new SqlParameter("@nom_actividad", SqlDbType.Text, 50);
            aParam[1].Value = nom_actividad;
            aParam[2] = new SqlParameter("@cod_pt", SqlDbType.Int, 4);
            aParam[2].Value = cod_pt;
            aParam[3] = new SqlParameter("@cod_fase", SqlDbType.Int, 4);
            aParam[3].Value = cod_fase;

            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;
            aParam[6] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[6].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDAD_C", aParam);
        }
        /// <summary>
        /// Calcula el estado de una actividad en base al estado de sus tareas
        ///     Si hay alguna activa (t332_estado=1) -> estado 0 (En curso)
        ///     Sino -> Estado 1 (Completada)
        /// </summary>
        /// <param name="t335_idactividad"></param>
        /// <returns></returns>
        public static int GetEstado(SqlTransaction tr, int t335_idactividad)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t335_idactividad", SqlDbType.Int, 4, t335_idactividad)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACTIVIDAD_ESTADO", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACTIVIDAD_ESTADO", aParam));
        }
        #endregion
    }
}