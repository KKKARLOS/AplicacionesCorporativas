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
//Para el List
//using System.Collections;
using System.Collections.Generic;

using SUPER.Capa_Datos;
namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for ProyTec
    /// </summary>
    public class ProyTec
    {
        #region Propiedades y Atributos

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

        private short _cod_une;
        public short cod_une
        {
            get { return _cod_une; }
            set { _cod_une = value; }
        }
        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }
        private int _num_proyecto;
        public int num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
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

        private string _nom_proyecto;
        public string nom_proyecto
        {
            get { return _nom_proyecto; }
            set { _nom_proyecto = value; }
        }
        //Estado del proyecto económico
        private string _t301_estado;
        public string t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }
        //Estado del proyecto técnico
        private byte _t331_estado;
        public byte t331_estado
        {
            get { return _t331_estado; }
            set { _t331_estado = value; }
        }

        private bool _t331_obligaest;
        public bool t331_obligaest
        {
            get { return _t331_obligaest; }
            set { _t331_obligaest = value; }
        }

        private short _t331_orden;
        public short t331_orden
        {
            get { return _t331_orden; }
            set { _t331_orden = value; }
        }

        private int _cod_cliente;
        public int cod_cliente
        {
            get { return _cod_cliente; }
            set { _cod_cliente = value; }
        }

        private string _nom_cliente;
        public string nom_cliente
        {
            get { return _nom_cliente; }
            set { _nom_cliente = value; }
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
        //private long _t332_etpr;
        //public long t332_etpr
        //{
        //    get { return _t332_etpr; }
        //    set { _t332_etpr = value; }
        //}

        private string _t331_desptlong;
        public string t331_desptlong
        {
            get { return _t331_desptlong; }
            set { _t331_desptlong = value; }
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

        private double _nPresupuestoA;
        public double nPresupuestoA
        {
            get { return _nPresupuestoA; }
            set { _nPresupuestoA = value; }
        }

        private double _nPresupuestoF;
        public double nPresupuestoF
        {
            get { return _nPresupuestoF; }
            set { _nPresupuestoF = value; }
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

        private string _t331_observaciones;
        public string t331_observaciones
        {
            get { return _t331_observaciones; }
            set { _t331_observaciones = value; }
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
        private string _t331_acceso_iap;
        public string t331_acceso_iap
        {
            get { return _t331_acceso_iap; }
            set { _t331_acceso_iap = value; }
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
        private string _t305_accesobitacora_pst;
        public string t305_accesobitacora_pst
        {
            get { return _t305_accesobitacora_pst; }
            set { _t305_accesobitacora_pst = value; }
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

        private double _t331_avance;
        public double t331_avance
        {
            get { return _t331_avance; }
            set { _t331_avance = value; }
        }

        private bool _t331_avanceauto;
        public bool t331_avanceauto
        {
            get { return _t331_avanceauto; }
            set { _t331_avanceauto = value; }
        }

        #endregion
        //Constructores 
        public ProyTec()
        {
            // TODO: Add constructor logic here
        }
        //Métodos Públicos
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla proyectos técnicos.
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo(Nullable<int> cod_pt, string nom_pt, short cod_une, Nullable<short> cod_pe, int iUser, 
        //                                     string sPerfil, byte nOrden, byte nAscDesc, string sTipoBusqueda)
        public static SqlDataReader Catalogo(Nullable<int> cod_pt, string nom_pt, Nullable<int> t305_id, int iUser, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@cod_pt", SqlDbType.Int, 4);
            aParam[0].Value = cod_pt;
            aParam[1] = new SqlParameter("@nom_pt", SqlDbType.Text, 50);
            aParam[1].Value = nom_pt;
            aParam[2] = new SqlParameter("@t305_id", SqlDbType.Int, 4);
            aParam[2].Value = t305_id;
            aParam[3] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
            aParam[3].Value = iUser;
            aParam[4] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[4].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                return SqlHelper.ExecuteSqlDataReader("SUP_PT_C_ADMIN", aParam);
            }
            else
            {
                if (t305_id == null)
                    return SqlHelper.ExecuteSqlDataReader("SUP_PT_C", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_PT_C2", aParam);
            }
        }
        public static ProyTec Obtener(int t331_idpt)
        {
            ProyTec o = new ProyTec();
            if (t331_idpt == -1) return o;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            //Obtengo los datos del Proyecto Técnico
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_PT_S", aParam);
            if (dr.Read())
            {
                if (dr["t331_idpt"] != DBNull.Value)
                    o.t331_idpt = (int)dr["t331_idpt"];
                if (dr["t331_despt"] != DBNull.Value)
                    o.t331_despt = (string)dr["t331_despt"];
                if (dr["cod_une"] != DBNull.Value)
                    o.cod_une = short.Parse(dr["cod_une"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["num_proyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["num_proyecto"];
                if (dr["nom_proyecto"] != DBNull.Value)
                    o.nom_proyecto = (string)dr["nom_proyecto"];
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];
                if (dr["t331_estado"] != DBNull.Value)
                    o.t331_estado = (byte)dr["t331_estado"];
                if (dr["t331_obligaest"] != DBNull.Value)
                    o.t331_obligaest = (bool)dr["t331_obligaest"];
                if (dr["t331_orden"] != DBNull.Value)
                    o.t331_orden = short.Parse(dr["t331_orden"].ToString());
                else o.t331_orden = 0;
                if (dr["t331_desptlong"] != DBNull.Value)
                    o.t331_desptlong = (string)dr["t331_desptlong"];
                if (dr["t346_idpst"] != DBNull.Value)
                    o.t346_idpst = (int)dr["t346_idpst"];
                if (dr["t346_codpst"] != DBNull.Value)
                    o.t346_codpst = (string)dr["t346_codpst"];
                if (dr["t346_despst"] != DBNull.Value)
                    o.t346_despst = (string)dr["t346_despst"];
                if (dr["cod_cliente"] != DBNull.Value)
                    o.cod_cliente = (int)dr["cod_cliente"];
                if (dr["nom_cliente"] != DBNull.Value)
                    o.nom_cliente = (string)dr["nom_cliente"];
                if (dr["t331_heredanodo"] != DBNull.Value)
                    o.t331_heredanodo = (bool)dr["t331_heredanodo"];
                if (dr["t331_heredaproyeco"] != DBNull.Value)
                    o.t331_heredaproyeco = (bool)dr["t331_heredaproyeco"];
                if (dr["t331_acceso_iap"] != DBNull.Value)
                    o.t331_acceso_iap = (string)dr["t331_acceso_iap"];
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t305_accesobitacora_pst"] != DBNull.Value)
                    o.t305_accesobitacora_pst = (string)dr["t305_accesobitacora_pst"];
                if (dr["t301_esreplicable"] != DBNull.Value)
                    o.t301_esreplicable = (bool)dr["t301_esreplicable"];
                if (dr["t305_nivelpresupuesto"] != DBNull.Value)
                    o.t305_nivelpresupuesto = (string)dr["t305_nivelpresupuesto"];
                if (dr["t331_presupuesto"] != DBNull.Value)
                    o.nPresupuesto = double.Parse(dr["t331_presupuesto"].ToString());
                if (dr["t331_avance"] != DBNull.Value)
                    o.t331_avance = double.Parse(dr["t331_avance"].ToString());
                if (dr["t331_avanceauto"] != DBNull.Value)
                    o.t331_avanceauto = (bool)(dr["t331_avanceauto"]);
                if (dr["t331_observaciones"] != DBNull.Value)
                    o.t331_observaciones = (string)dr["t331_observaciones"];
                //Obtengo los datos de LAS TAREAS referentes al Proyecto Técnico 
                dr.Close();
                SqlParameter[] aParam1 = new SqlParameter[1];
                aParam1[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
                aParam1[0].Value = t331_idpt;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PT_TAREAS", aParam1);
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

                //En caso de que el nivel de presupuesto esté establecido para las activiades o fases, se llama a otros procedures que realizan este calculo
                switch (o.t305_nivelpresupuesto.ToString())
                {
                    case "F":
                        SqlParameter[] aParam3 = new SqlParameter[1];
                        aParam3[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
                        aParam3[0].Value = t331_idpt;
                        dr = SqlHelper.ExecuteSqlDataReader("SUP_PT_PRESUPACUM_FASE", aParam3);
                        if (dr.Read())
                        {
                            if (dr["nPresupuesto"] != DBNull.Value)
                                o.nPresupuesto = double.Parse(dr["nPresupuesto"].ToString());

                        }
                        //Obtengo la suma de los presupuestos de las actividades asociadas
                        dr.Close();
                        break;

                    case "A":
                        SqlParameter[] aParam4 = new SqlParameter[1];
                        aParam4[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
                        aParam4[0].Value = t331_idpt;
                        dr = SqlHelper.ExecuteSqlDataReader("SUP_PT_PRESUPACUM_ACTIVIDAD", aParam4);
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

                //Obtengo los datos del IAP referentes al Proyecto Técnico
                SqlParameter[] aParam2 = new SqlParameter[1];
                aParam2[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
                aParam2[0].Value = t331_idpt;
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PTIAPS", aParam2);
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
            else { throw (new NullReferenceException("No se ha obtenido ningun dato de PT")); }
            return o;
        }
        /// <summary>
        /// 
        /// Graba los datos básicos del Proyecto Técnico,
        /// correspondientes a la tabla t331_PT.
        /// </summary>
        //public static int Insert(SqlTransaction tr, string t331_despt, short cod_une, int num_proyecto, byte t331_estado,
        //                         bool t331_obligaest, short t331_orden, Nullable<int> t346_idpst, string t331_desptlong)
        public static int Insert(SqlTransaction tr, string t331_despt, int t305_idproyectosubnodo, byte t331_estado,
                                 bool t331_obligaest, short t331_orden, Nullable<int> t346_idpst, string t331_desptlong,
                                 bool bHeredaNodo, bool bHeredaPE, string t331_acceso_iap, decimal fPresupuesto, decimal fAvance, bool bAvanceAuto, string t331_observaciones)
        {
            string sIdRecurso;
            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@t331_despt", SqlDbType.Text, 100);
            aParam[0].Value = t331_despt;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@t331_estado", SqlDbType.TinyInt, 1);
            aParam[2].Value = t331_estado;
            aParam[3] = new SqlParameter("@t331_obligaest", SqlDbType.Bit, 1);
            aParam[3].Value = t331_obligaest;
            aParam[4] = new SqlParameter("@t331_orden", SqlDbType.SmallInt, 2);
            aParam[4].Value = t331_orden;
            aParam[5] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
            aParam[5].Value = t346_idpst;
            aParam[6] = new SqlParameter("@t331_desptlong", SqlDbType.Text, 2147483647);
            aParam[6].Value = t331_desptlong;
            aParam[7] = new SqlParameter("@t331_heredanodo", SqlDbType.Bit, 1);
            aParam[7].Value = bHeredaNodo;
            aParam[8] = new SqlParameter("@t331_heredaproyeco", SqlDbType.Bit, 1);
            aParam[8].Value = bHeredaPE;
            aParam[9] = new SqlParameter("@t331_acceso_iap", SqlDbType.Text, 1);
            aParam[9].Value = t331_acceso_iap;
            aParam[10] = new SqlParameter("@t331_presupuesto", SqlDbType.Money, 8);
            aParam[10].Value = fPresupuesto;
            aParam[11] = new SqlParameter("@t331_avance", SqlDbType.Float, 8);
            aParam[11].Value = fAvance;
            aParam[12] = new SqlParameter("@t331_avanceauto", SqlDbType.Bit, 1);
            aParam[12].Value = bAvanceAuto;
            aParam[13] = new SqlParameter("@t331_observaciones", SqlDbType.Text, 2147483647);
            aParam[13].Value = t331_observaciones;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PT_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PT_I", aParam));
            //Despues de insertar el Proyecto Técnico asigno como responsable técnico por defecto al pool de RTPTs
            if (POOLRPT.HayPool(t305_idproyectosubnodo))
            {
                SqlDataReader rdr = POOLRPT.SelectByProyecto(t305_idproyectosubnodo);
                while (rdr.Read())
                {
                    sIdRecurso = rdr["t314_idusuario"].ToString();
                    RTPT.Insert(tr, returnValue, int.Parse(sIdRecurso));
                }
                rdr.Close();
                rdr.Dispose();
            }
            //else
            //    RTPT.Insert(tr, returnValue, int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()));

            return returnValue;
        }
        /// <summary>
        /// Modifica los datos básicos del Proyecto Técnico, correspondientes a la tabla t331_PT.
        /// Se le llama desde la pantalla de estructura técnica
        /// </summary>
        public static void Modificar(SqlTransaction tr, int nIdPT, string sDesPT, int nOrden, Nullable<byte> iEstado, decimal fPresupuesto, decimal fAvance, Nullable<byte> iAvanceAuto)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesPT", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[3] = new SqlParameter("@iEstado", SqlDbType.TinyInt, 1);
            aParam[4] = new SqlParameter("@nPresupuesto", SqlDbType.Money, 8);
            aParam[5] = new SqlParameter("@t331_avance", SqlDbType.Float, 8);
            aParam[6] = new SqlParameter("@t331_avanceauto", SqlDbType.Bit, 1);

            aParam[0].Value = nIdPT;
            aParam[1].Value = sDesPT;
            aParam[2].Value = ((bool)HttpContext.Current.Session["RTPT_PROYECTOSUBNODO"]) ? null : (int?)nOrden;
            aParam[3].Value = iEstado;
            aParam[4].Value = fPresupuesto;
            aParam[5].Value = fAvance;
            aParam[6].Value = iAvanceAuto;
            //object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "PSP_PTU", aParam);
            //return int.Parse(nResul.ToString());
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PTU_ESTR", aParam);
            return;
        }
        /// <summary>
        /// Modifica los datos básicos del Proyecto Técnico, correspondientes a la tabla t331_PT.
        /// Se le llama desde la importación de OpenProj
        /// </summary>
        public static void Modificar(SqlTransaction tr, int nIdPT, string sDesPT, int nOrden, Nullable<byte> iEstado, string t331_desptlong)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesPT", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[3] = new SqlParameter("@iEstado", SqlDbType.TinyInt, 1);
            aParam[4] = new SqlParameter("@t331_desptlong", SqlDbType.Text, 2147483647);


            aParam[0].Value = nIdPT;
            aParam[1].Value = sDesPT;
            aParam[2].Value = nOrden;
            aParam[3].Value = iEstado;
            aParam[4].Value = t331_desptlong;
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PT_OP_U", aParam);
            return;
        }
        public static int Update(SqlTransaction tr, int t331_idpt, string t331_despt, int t305_idproyectosubnodo, byte t331_estado,
                                 bool t331_obligaest, short t331_orden, Nullable<int> t346_idpst, string t331_desptlong,
                                 bool bHeredaNodo, bool bHeredaPE, string t331_acceso_iap, decimal t331_presupuesto, decimal t331_avance, bool bAvanceAuto, string t331_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t331_despt", SqlDbType.Text, 100);
            aParam[1].Value = t331_despt;
            aParam[2] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[2].Value = t305_idproyectosubnodo;
            aParam[3] = new SqlParameter("@t331_estado", SqlDbType.TinyInt, 1);
            aParam[3].Value = t331_estado;
            aParam[4] = new SqlParameter("@t331_obligaest", SqlDbType.Bit, 1);
            if (t331_obligaest) aParam[4].Value = 1;
            else aParam[4].Value = 0;
            aParam[5] = new SqlParameter("@t331_orden", SqlDbType.SmallInt, 2);
            aParam[5].Value = t331_orden;
            aParam[6] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
            aParam[6].Value = t346_idpst;
            aParam[7] = new SqlParameter("@t331_desptlong", SqlDbType.Text, 2147483647);
            aParam[7].Value = t331_desptlong;
            aParam[8] = new SqlParameter("@t331_heredanodo", SqlDbType.Bit, 1);
            aParam[8].Value = bHeredaNodo;
            aParam[9] = new SqlParameter("@t331_heredaproyeco", SqlDbType.Bit, 1);
            aParam[9].Value = bHeredaPE;
            aParam[10] = new SqlParameter("@t331_acceso_iap", SqlDbType.Char, 1);
            aParam[10].Value = t331_acceso_iap;
            aParam[11] = new SqlParameter("@t331_presupuesto", SqlDbType.Money, 8);
            aParam[11].Value = t331_presupuesto;
            aParam[12] = new SqlParameter("@t331_avance", SqlDbType.Float, 8);
            aParam[12].Value = t331_avance;
            aParam[13] = new SqlParameter("t331_avanceauto", SqlDbType.Bit, 1);
            aParam[13].Value = bAvanceAuto;
            aParam[14] = new SqlParameter("t331_observaciones", SqlDbType.Text, 2147483647);
            aParam[14].Value = t331_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PT_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PT_U", aParam);
        }
        public static int UpdateOrden(SqlTransaction tr, int t331_idpt, short t331_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t331_orden", SqlDbType.SmallInt, 2);
            aParam[1].Value = t331_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PT_U_ORDEN", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PT_U_ORDEN", aParam);
        }
        /// <summary>
        /// 
        /// Elimina los datos básicos del Proyecto Técnico,
        /// correspondientes a la tabla t331_PT.
        /// </summary>
        /// 
        public static int Eliminar(SqlTransaction tr, int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PT_D", aParam);
        }
        /// <summary>
        /// 
        /// Elimina las tareas del Proyecto Técnico
        /// </summary>
        /// 
        public static int EliminarContenido(SqlTransaction tr, int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PT_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PT_D2", aParam);
        }
        public static SqlDataReader CatalogoResponsables(int nUsuario)
        {//Obtine los responsables de los proyectos a los que tenemos accesp
            SqlParameter[] aParam = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[0];
                return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_RESPON_PROY_ADMIN_TEC", aParam);
            }
            else aParam = new SqlParameter[1];
            {
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_RESPON_PROY_USU_TEC", aParam);
            }
        }
        public static SqlDataReader CatalogoRecursos(int nIdPT, bool bMostrarBajas)
        {//Obtine los recursos asociados al Proyecto técnico
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_PTRECURSOCATA", aParam);
        }
        public static SqlDataReader CatalogoTareas(int nIdPT)
        {//Obtiene las tareas asociadas al Proyecto técnico y sus consumos
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_PT_TAREA_S", aParam);
        }
        public static SqlDataReader CatalogoTareasPT(int nIdPT)
        {//Obtiene las fases, actividades y tareas asociadas al Proyecto técnico y sus consumos
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_PT_TAREA_S2", aParam);
        }
        public static SqlDataReader CatalogoRTPTs(int nIdPT)
        {//Obtine los responsables técnicos asociados al Proyecto técnico
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_PTRTPTCATA", aParam);
        }
        //public static SqlDataReader ObtenerRelacionTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3
        //                                                    , bool bRepPreCerr, Nullable<int> t303_idnodo)
        //{//Obtiene la relación de técnicos según parametro
        //    //Si strOpcion=N es una busqueda por nombre
        //    //Si strOpcion=C es una busqueda por CR
        //    //Si strOpcion=G es una busqueda por Grupo Funcional
        //    //Si strOpcion=P es una busqueda por recursos asociados al proyecto económico
        //    //Si strOpcion=T es una busqueda por recursos asociados al proyecto técnico
        //    //bRepPreCerr -> Replicado a precio cerrado. En este caso solo se podrán obtener profesionales del CR
        //    SqlParameter[] aParam = new SqlParameter[6];
        //    aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
        //    aParam[1] = new SqlParameter("@sValor1", SqlDbType.VarChar, 50);
        //    aParam[2] = new SqlParameter("@sValor2", SqlDbType.VarChar, 50);
        //    aParam[3] = new SqlParameter("@sValor3", SqlDbType.VarChar, 50);
        //    aParam[4] = new SqlParameter("@bRepPreCerr", SqlDbType.Bit, 1);
        //    aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);

        //    aParam[0].Value = strOpcion;
        //    aParam[1].Value = strValor1;
        //    aParam[2].Value = strValor2;
        //    aParam[3].Value = strValor3;
        //    aParam[4].Value = bRepPreCerr;
        //    aParam[5].Value = t303_idnodo;

        //    return SqlHelper.ExecuteSqlDataReader("SUP_PTTECNICOS", aParam);
        //}


        /// <summary>
        /// Obtiene las tareas vivas de un PT
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t331_idpt"></param>
        /// <param name="t314_idusuario">Restringe las tareas a las asignadas al profesional</param>
        /// <param name="bAsignadas">Indica que hay que restringir las tareas al profesional</param>
        /// <param name="bSoloActivas">Restringe las tareas a las que su asociación al profesional este activa</param>
        /// <returns></returns>
        public static List<SUPER.Capa_Negocio.TAREAPSP> GetTareasVivas(SqlTransaction tr, int t331_idpt, int t314_idusuario,
                                                   bool bAsignadas, bool bSoloActivas)
        {
            SqlDataReader dr;
            if (bAsignadas)
            {
                SqlParameter[] aParam = new SqlParameter[]{
                    ParametroSql.add("@t331_idpt", SqlDbType.Int, 4, t331_idpt),
                    ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                    ParametroSql.add("@bSoloActivas", SqlDbType.Bit, 1, bSoloActivas)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAS_PROFESIONAL_ByPT", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAS_PROFESIONAL_ByPT", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[]{
                    ParametroSql.add("@t331_idpt", SqlDbType.Int, 4, t331_idpt)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_S2", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAPSP_S2", aParam);
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
                                          int iCodPT, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de un PT que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //try
            //{
            //Recorro todas las tareas del Proyecto Técnico
            List<SUPER.Capa_Negocio.TAREAPSP> oLista = SUPER.Capa_Negocio.ProyTec.GetTareasVivas(tr, iCodPT, iRecursoOrigen, bSoloAsignadas, bSoloActivas);
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
        public static void AsignarTareas2(SqlTransaction tr, int iCodPT, int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de un PT que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);

            aParam[0].Value = iCodPT;
            //Recorro todas las tareas del Proyecto Técnico
            //SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("PSP_TAREAPSP_SByt331_idpt", aParam);
            SqlDataReader dr2 = SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_S2", aParam);
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
        public static void DesAsignarTareas(SqlTransaction tr, int iCodPT, int iCodRecurso)
        {//Al desasignar recursos a un PT hay que quitarlos de todas sus tareas 
            //En principio este método no lo usamos
            int iCodTarea;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);

            aParam[0].Value = iCodPT;
            //Recorro todas las tareas del Proyecto Técnico
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_SByt331_idpt", aParam);
            while (dr.Read())
            {
                iCodTarea = int.Parse(dr["t332_idtarea"].ToString());
                TareaRecurso.Eliminar(tr, iCodTarea, iCodRecurso);
            }
            dr.Close();
            dr.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al desasignar técnicos a tareas.", ex);
            //}
        }
        public static void EstadoRecursos(SqlTransaction tr, int iCodPT, int iCodRecurso, string sEstado)
        {//Cambia el estado a todos las tareas/recurso del proyecto tecnico 
            //int iCodTarea, iEstado;
            //try
            //{
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);

            aParam[0].Value = iCodPT;
            aParam[1].Value = iCodRecurso;
            if (sEstado == "A") aParam[2].Value = 1;
            else aParam[2].Value = 0;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PTRECURSO_ESTADO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PTRECURSO_ESTADO", aParam);
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al desasignar técnicos a tareas.", ex);
            //}
        }

        public static bool bFaltanValoresAE(SqlTransaction tr, short t303_idnodo, Nullable<int> nIdPT)
        {
            //Si viene código de PT miramos si tiene valores en los atributos estadisticos obligatorios del CR
            //Si viene a nulo solo miramos si el CR tiene atributos estadísticos obligatorios
            bool bFaltanValores = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[1].Value = nIdPT;

            int returnValue;
            if (nIdPT == null)
            {
                //aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Text, 1);
                //aParam[1].Value = "P";
                if (tr == null)
                    //returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("PSP_AE_OBLIGATORIO2", aParam));
                    returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AE_OBLIGATORIO", aParam));
                else
                    //returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "PSP_AE_OBLIGATORIO2", aParam));
                    returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AE_OBLIGATORIO", aParam));

                if (returnValue > 0)
                    bFaltanValores = true;
            }
            else
            {
                if (tr == null)
                    returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AE_PT_OBLIGATORIO", aParam));
                else
                    returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AE_PT_OBLIGATORIO", aParam));

                if (returnValue > 0)
                    bFaltanValores = true;
            }
            return bFaltanValores;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta en la tabla T055_AEPTPSP sacando los valores de T071_AEITEMSPLANTILLA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [doarhumi]	26/04/2007 14:56:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarAE(SqlTransaction tr, int idElemento, int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@idElemento", SqlDbType.Int, 4);
            aParam[1].Value = idElemento;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_AE_PT_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_PT_I", aParam);
        }

        /// <summary>
        /// 
        /// Obtiene las tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Proyecto Técnico
        /// </summary>
        /// 
        public static SqlDataReader CatalogoTareasRecurso(int nIdPT, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            return SqlHelper.ExecuteSqlDataReader("SUP_ASIGTAREA_PT", aParam);
        }

        /// <summary>
        /// 
        /// Obtiene el número de tareas a las que está asignado el profesional
        /// con el check de estado a ON a nivel de Proyecto Técnico
        /// </summary>
        /// 
        public static int NumeroTareasRecurso(SqlTransaction tr, int nIdPT, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASIGTAREACOUNT_PT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASIGTAREACOUNT_PT", aParam));
        }
        /// <summary>
        /// 
        /// Comprueba si un proyecto técnico es de estimación obligatoria
        /// </summary>
        public static bool bObligaEst(SqlTransaction tr, int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            if (tr == null)
                return (bool)SqlHelper.ExecuteScalar("SUP_PT_OBLIGA", aParam);
            else
                return (bool)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PT_OBLIGA", aParam);
        }

        /// <summary>
        /// Comprueba si un proyecto técnico es modificable en función del perfil del recurso que la está accediendo
        /// </summary>
        public static string getAcceso(SqlTransaction tr, int nIdPT, int iUser)
        {
            string sRes = "N";
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                sRes = "W";
            else
            {
                object obj;
                if (tr == null)
                    obj = SqlHelper.ExecuteScalar("SUP_PERMISO_PT", aParam);
                else
                    obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERMISO_PT", aParam);
                if (obj != null)
                {
                    if ((bool)obj) sRes = "R";
                    else sRes = "W";
                }
            }
            return sRes;
        }
        /// <summary>
        /// 
        /// Obtiene un catalogo de PTs accesibles por el usuario y que tienen asociados datos en la Bitacora de PT (T409_ASUNTOPT)
        /// </summary>
        public static SqlDataReader CatalogoBitacora(Nullable<int> cod_pe, int iUser, string sNomPT, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = cod_pe;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            aParam[2] = new SqlParameter("@sNomPT", SqlDbType.Text, 50);
            aParam[2].Value = sNomPT;
            aParam[3] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[3].Value = sTipoBusqueda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                return SqlHelper.ExecuteSqlDataReader("SUP_PT_BIT_C_ADMIN", aParam);
            }
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PT_BIT_C", aParam);
        }
        public static int GetNodo(SqlTransaction tr, int nPT)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[0].Value = nPT;
            if (tr != null)
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GET_NODO_PT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GET_NODO_PT", aParam));
        }
        /// <summary>
        /// 
        /// Obtiene el mayor nº de orden de un PT dentro de un Proyectosubnodo
        /// De momento solo se usa para la importación de estructura técnica desde Openproj
        /// </summary>
        /// 
        public static int GetMaxOrden(SqlTransaction tr, int nIdPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdPSN;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_MAXORDEN_PT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_MAXORDEN_PT", aParam));
        }

        public static SqlDataReader CatalogoTareasConAE(int nIdPT, int idAE)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = idAE;

            return SqlHelper.ExecuteSqlDataReader("SUP_PT_AETAREA_CAT", aParam);
        }
        public static SqlDataReader CatalogoTareasConCampo(int nIdPT, int idCampo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = nIdPT;
            aParam[1] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[1].Value = idCampo;

            return SqlHelper.ExecuteSqlDataReader("SUP_PT_CAMPOTAREA_CAT", aParam);
        }

    }
}
