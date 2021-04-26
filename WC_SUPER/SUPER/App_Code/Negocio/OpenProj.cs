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
//
using SUPER.Capa_Datos;

using System.Xml;
using System.Xml.XPath;

namespace SUPER.Capa_Negocio
{
    public class ItemsProyecto
    {
        #region Atributos privados
        private int _codPT;
        private int _codFase;
        private int _codActiv;
        private int _codTarea;
        private string _nombre;
        private string _descripcion;
        private string _tipo;
        private int _orden;
        //private DateTime _FIPL;
        private string _FIPL;
        private string _FFPL;
        private decimal _ETPL;
        private string _PRIMER_CONSUMO;
        private string _FFPR;
        private decimal _ETPR;
        //private decimal _AVANCE;
        private decimal _Consumido;
        private string _situacion;
        private bool _facturable;
        private int _margen;
        private bool _borrar;
        private Decimal _EsfuerzoHoras;
        #endregion

        #region Propiedades públicas

        public int codPT
        {
            get { return _codPT; }
            set { _codPT = value; }
        }
        public int codFase
        {
            get { return _codFase; }
            set { _codFase = value; }
        }
        public int codActiv
        {
            get { return _codActiv; }
            set { _codActiv = value; }
        }
        public int codTarea
        {
            get { return _codTarea; }
            set { _codTarea = value; }
        }
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public int orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        public string FIPL
        {
            get { return _FIPL; }
            set { _FIPL = value; }
        }
        public string FFPL
        {
            get { return _FFPL; }
            set { _FFPL = value; }
        }
        public decimal ETPL
        {
            get { return _ETPL; }
            set { _ETPL = value; }
        }
        public string PRIMER_CONSUMO
        {
            get { return _PRIMER_CONSUMO; }
            set { _PRIMER_CONSUMO = value; }
        }
        public string FFPR
        {
            get { return _FFPR; }
            set { _FFPR = value; }
        }
        public decimal ETPR
        {
            get { return _ETPR; }
            set { _ETPR = value; }
        }
        //public decimal AVANCE
        //{
        //    get { return _AVANCE; }
        //    set { _AVANCE = value; }
        //}
        public decimal Consumido
        {
            get { return _Consumido; }
            set { _Consumido = value; }
        }
        public string situacion
        {
            get { return _situacion; }
            set { _situacion = value; }
        }
        public bool facturable
        {
            get { return _facturable; }
            set { _facturable = value; }
        }
        public int margen
        {
            get { return _margen; }
            set { _margen = value; }
        }
        public bool borrar
        {
            get { return _borrar; }
            set { _borrar = value; }
        }
        public Decimal EsfuerzoHoras
        {
            get { return _EsfuerzoHoras; }
            set { _EsfuerzoHoras = value; }
        }

        #endregion

        public ItemsProyecto()
        {
            _codPT = 0;
            _codFase = 0;
            _codActiv = 0;
            _codTarea = 0;
            _nombre = "";
            _descripcion = "";
            _tipo = "";
            _orden = 0;
            _FIPL = "";
            _FFPL = "";
            _ETPL = 0;
            _PRIMER_CONSUMO = "";
            _FFPR = "";
            _ETPR = 0;
            //_AVANCE=0;
            _Consumido = 0;
            _situacion = "";
            _facturable = true;
            _margen = 0;
            _borrar = true;
        }
        public ItemsProyecto(int codPT, int codFase, int codActiv, int codTarea, string nombre, string descripcion, string tipo, int orden,
                             string FIPL, string FFPL, Decimal ETPL, string PRIMER_CONSUMO, string ULTIMO_CONSUMO, string FFPR,
                             Decimal ETPR, Decimal Consumido, string situacion, bool facturable, int margen)
        {
            //05/10/    2011 Victor dice que si una tarea no está planificada hay que ponerle un defecto para que salga en la exportación
            if (tipo == "HF")
            {
                ETPL = 0;
                ETPR = 0;
                Consumido = 0;
                PRIMER_CONSUMO = "";
                ULTIMO_CONSUMO = "";
            }
            else
            {
                if (FIPL == "")
                {
                    if (PRIMER_CONSUMO == "")
                        FIPL = DateTime.Now.ToShortDateString();
                    else
                        FIPL = PRIMER_CONSUMO;
                }
                if (FFPL == "")
                {
                    if (ULTIMO_CONSUMO == "")
                        FFPL = FIPL;
                    else
                        FFPL = ULTIMO_CONSUMO;
                }
                if (FFPR == "")
                {
                    if (ULTIMO_CONSUMO == "")
                        FFPR = FFPL;
                    else
                        FFPR = ULTIMO_CONSUMO;
                }

                if (ETPL == 0)
                {
                    if (Consumido == 0)
                        ETPL = 8;
                    else
                        ETPL = Consumido;
                }
                if (ETPR == 0)
                {
                    ETPR = ETPL;
                }
            }
            _codPT = codPT;
            _codFase = codFase;
            _codActiv = codActiv;
            _codTarea = codTarea;
            _nombre = nombre;
            _descripcion = descripcion;
            _tipo = tipo;
            _orden = orden;
            _FIPL = FIPL;
            _FFPL = FFPL;
            _ETPL = ETPL;
            //Sino es una tarea ponemos el FIPL porque sino puede ocurrir que haya una tarea planificada sin consumo
            //y no le va a dejar poner a esa tarea su FIPL si es menor que el primer consumo del resto de tareas de ese PT, F o A
            if (tipo == "T")
                _PRIMER_CONSUMO = (PRIMER_CONSUMO == "") ? FIPL : PRIMER_CONSUMO;
            else
                _PRIMER_CONSUMO = FIPL;

            _FFPR = (FFPR == "") ? FFPL : FFPR;
            //Si la previsión es mejor que la planificación hacemos FFPR=FFPL porque sino el OpenProj no lo gestiona bien
            if (OpenProj.flDuracionDias(FFPL, FFPR) <= 0)
                _FFPR = FFPL;
            //Si no hay previsión de esfuerzo ponemos el consumido
            _ETPR = (ETPR == 0) ? Consumido : ETPR;
            //Si lo consumido es mayor que lo previsto, ponemos como previsto el consumido (OpenProj no permite avances superiores al 100%)
            if (Consumido > ETPR)
                _ETPR = Consumido;
            //_AVANCE = (AVANCE > 1) ? 1 : AVANCE;//OpenProj no permite avances superiores al 100%
            _Consumido = Consumido;
            _situacion = situacion;
            _facturable = facturable;
            _margen = margen;
            _borrar = true;
            //_EsfuerzoHoras = (ETPR == 0) ? ETPL : ETPR;
            _EsfuerzoHoras = (_ETPR == 0) ? ETPL : _ETPR;
        }
    }
    public class OpenProj
    {
        #region Propiedades y Atributos

        private byte[] _t681_fichXML;
        public byte[] t681_fichXML
        {
            get { return _t681_fichXML; }
            set { _t681_fichXML = value; }
        }

        private string _t681_denominacion;
        public string t681_denominacion
        {
            get { return _t681_denominacion; }
            set { _t681_denominacion = value; }
        }

        private int _t681_idplant;
        public int t681_idplant
        {
            get { return _t681_idplant; }
            set { _t681_idplant = value; }
        }
        #endregion

        public OpenProj()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Obtención de datos
        //public static OpenProj Select(SqlTransaction tr, int t681_idplant)
        //{
        //    OpenProj o = new OpenProj();

        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t681_idplant", SqlDbType.Int, 4);
        //    aParam[0].Value = t681_idplant;

        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUXML_S2", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUXML_S2", aParam);

        //    if (dr.Read())
        //    {
        //        o.t681_idplant = t681_idplant;
        //        if (dr["t681_denominacion"] != DBNull.Value)
        //            o.t681_denominacion = (string)dr["t681_denominacion"];
        //        //if (dr["t681_fichXML"] != DBNull.Value)
        //        //    o.t681_fichXML = (byte[])dr["t681_fichXML"];
        //    }
        //    else
        //    {
        //        throw (new NullReferenceException("No se ha obtenido ningun dato de OpenProj"));
        //    }

        //    dr.Close();
        //    dr.Dispose();

        //    return o;
        //}

        //<summary>
        //Obtiene la estructura completa de un proyectosubnodo excepto las tareas anuladas y los hitos
        //La ordenación es descendente porque la usamos para recorrer una hashtable y al hacer el ForEach
        //empieza a recuperar por el último insertado
        //Lo utilizamos para exportar la estructura de un proyecto a Open Project
        //</summary>
        public static SqlDataReader GetEstructura(SqlTransaction tr, int iPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            aParam[0].Value = iPSN;
            if (tr != null)
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESTRUCTURA_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_ESTRUCTURA_PSN", aParam);
        }
        //<summary>
        //Obtiene la estructura completa de un proyectosubnodo excepto las tareas anuladas y los hitos
        //Solo para aquellas tareas de los PTs de los que el usuario sea RTPT
        //La ordenación es descendente porque la usamos para recorrer una hashtable y al hacer el ForEach
        //empieza a recuperar por el último insertado
        //Lo utilizamos para exportar la estructura de un proyecto a Open Project
        //</summary>
        public static SqlDataReader GetEstructura(SqlTransaction tr, int iPSN, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            aParam[0].Value = iPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int);
            aParam[1].Value = t314_idusuario;
            if (tr != null)
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESTRUCTURA_PSN2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_ESTRUCTURA_PSN2", aParam);
        }
        //<summary>
        //Obtiene las cadenas que forman la plantilla de documento XML para un proyecto vacío en OpenProj
        //Lo utilizamos para exportar la estructura de un proyecto a Open Project
        //</summary>
        public static SqlDataReader GetPlantilla(SqlTransaction tr, int t681_idPlant)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t681_idPlant", SqlDbType.Int);
            aParam[0].Value = t681_idPlant;
            if (tr != null)
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLAOPENPROJ_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAOPENPROJ_S", aParam);
        }
        public static byte[] GetXML(SqlTransaction tr, byte[] documento)
        {
            SqlDataReader dr;
            byte[] aA =null;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@documento", SqlDbType.Binary, 2147483647);
            aParam[0].Value = documento;
            if (tr != null)
                dr= SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUXML_S", aParam);
            else
                dr= SqlHelper.ExecuteSqlDataReader("SUP_DOCUXML_S", aParam);
            if (dr.Read())
            {
                aA=(byte[])dr["documento"];
            }
            dr.Close();
            dr.Dispose();
            return aA;
        }
        //public static int UpdateXML(SqlTransaction tr, int t681_idplant, byte[] t681_fichXML, string t681_denominacion)
        //{
        //    SqlParameter[] aParam = new SqlParameter[3];
        //    aParam[0] = new SqlParameter("@t681_idplant", SqlDbType.Int, 4);
        //    aParam[0].Value = t681_idplant;
        //    aParam[1] = new SqlParameter("@t681_fichXML", SqlDbType.Binary, 2147483647);
        //    aParam[1].Value = t681_fichXML;
        //    aParam[2] = new SqlParameter("@t681_denominacion", SqlDbType.Text, 255);
        //    aParam[2].Value = t681_denominacion;

        //    // Ejecuta la query y devuelve el numero de registros modificados.
        //    int returnValue;
        //    if (tr == null)
        //        returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCUXML_U", aParam);
        //    else
        //        returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUXML_U", aParam);

        //    return returnValue;
        //}

        //<summary>
        //Obtiene los profesionales asociados a las tareas del PSN
        //Lo utilizamos para exportar la estructura de un proyecto a Open Project
        //</summary>
        public static SqlDataReader GetProfesionales(SqlTransaction tr, int iPSN, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int);
            aParam[0].Value = iPSN;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;
            if (tr != null)
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSN_PROFESIONALES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PSN_PROFESIONALES", aParam);
        }
        //<summary>
        //Obtiene los profesionales asociados a una tarea
        //Lo utilizamos para exportar la estructura de un proyecto a Open Project
        //</summary>
        public static SqlDataReader GetProfesionalesTarea(SqlTransaction tr, int iTarea, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int);
            aParam[0].Value = iTarea;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;
            if (tr != null)
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_PROFESIONALES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREA_PROFESIONALES", aParam);
        }

        /// <summary>
        /// Comprueba si un proyecto económico tiene tareas cuya fecha de fin planificada es superior a la fecha de fin prevista
        /// </summary>
        public static bool bTareasParaAvisar(SqlTransaction tr, int iNumProy)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = iNumProy;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTETAREAAVISABLE_PSN", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTETAREAAVISABLE_PSN", aParam));

            if (nResul > 0) bRes = true;

            return bRes;
        }
        /// <summary>
        /// Devuelve las tareas de un PSN cuya fecha de fin planificada es superior a la fecha de fin prevista
        /// </summary>
        public static string GetTareasPlanMayorPrevisto(SqlTransaction tr, int iPSN)
        {
            string sRes = "";
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int);
            aParam[0].Value = iPSN;
            if (tr != null)
                dr= SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_FFPL_MAYOR_FFPR", aParam);
            else
                dr= SqlHelper.ExecuteSqlDataReader("SUP_TAREA_FFPL_MAYOR_FFPR", aParam);
            while (dr.Read())
            {
                sRes += int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###");
                sRes += "/#/" + dr["t332_destarea"].ToString() + "<reg>";
            }
            dr.Close();
            dr.Dispose();

            return sRes;
        }
        /// <summary>
        /// Devuelve las tareas de un PSN cuya fecha de fin planificada es superior a la fecha de fin prevista
        /// Solo para aquellas tareas de los PTs de los que el usuario sea RTPT
        /// </summary>
        public static string GetTareasPlanMayorPrevisto(SqlTransaction tr, int iPSN, int t314_idusuario)
        {
            string sRes = "";
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int);
            aParam[0].Value = iPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int);
            aParam[1].Value = t314_idusuario;
            if (tr != null)
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_FFPL_MAYOR_FFPR2", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_FFPL_MAYOR_FFPR2", aParam);
            while (dr.Read())
            {
                sRes += int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###");
                sRes += "/#/" + dr["t332_destarea"].ToString() + "<reg>";
            }
            dr.Close();
            dr.Dispose();

            return sRes;
        }

        #endregion

        #region Metodos para updatear desde OpenProj
        public static void Modificar(SqlTransaction tr, int iCodUne, int iNumProy, string sTipo, string sDesc,
                                    int iPT, int iFase, int iActiv, int iTarea, int iHito, int iMargen, int iOrden,
                                    string sFIniPL, string sFfinPl, decimal fDuracion, string sDenominacion)
        {
            //byte iEstado;
            sDesc = Utilidades.unescape(sDesc);
            sDenominacion = Utilidades.unescape(sDenominacion);
            switch (sTipo)
            {
                case "P":
                    //if (sSituacion == "") iEstado = 1;
                    //else iEstado = byte.Parse(sSituacion);
                    ProyTec.Modificar(tr, iPT, sDesc, iOrden, null, sDenominacion);
                    break;
                case "F": ModificarFase(tr, iFase, sDesc, iOrden, sDenominacion); break;
                case "A": ModificarActividad(tr, iActiv, iFase, sDesc, iOrden, sDenominacion); break;
                case "T":
                    int iUsuario = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                    //if (sSituacion == "") iEstado = 1;
                    //else iEstado = byte.Parse(sSituacion);
                    ModificarTarea(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFIniPL, sFfinPl, fDuracion, iUsuario, sDenominacion);
                    break;
                //case "HT":
                //    ModificarHito(tr, iHito, sDesc, iOrden, iMargen);
                //    break;
                case "HF":
                    ModificarHitoPE(tr, iHito, sDesc, iOrden, sFIniPL, sDenominacion);
                    break;
                //case "HM":
                //    ModificarHito(tr, iHito, sDesc, iOrden, iMargen);
                //    break;
            }
        }
        /// <summary>
        /// Modifica los datos básicos de la fase
        /// Se le llama desde la importación de OpenProj
        /// </summary>
        private static void ModificarFase(SqlTransaction tr, int iFase, string sDesc, int iOrden, string t334_desfaselong)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@t334_desfaselong", SqlDbType.Text, 2147483647);
            aParam[0].Value = iFase;
            aParam[1].Value = sDesc;
            aParam[2].Value = iOrden;
            aParam[3].Value = t334_desfaselong;
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASE_OP_U", aParam);
        }
        /// <summary>
        /// Modifica los datos básicos de la actividad
        /// Se le llama desde la importación de OpenProj
        /// </summary>
        private static void ModificarActividad(SqlTransaction tr, int iActiv, int iFase, string sDesc, int iOrden, string t335_desactividadlong)
        {//Si iFase = -1 es una actividad sin fase (se controla en el procedimiento almacenado)
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nIdActiv", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@t335_desactividadlong", SqlDbType.Text, 2147483647);

            aParam[0].Value = iActiv;
            aParam[1].Value = iFase;
            aParam[2].Value = sDesc;
            aParam[3].Value = iOrden;
            aParam[4].Value = t335_desactividadlong;
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDAD_OP_U", aParam);
        }
        /// <summary>
        /// Modifica los datos básicos de la tarea
        /// Se le llama desde la importación de OpenProj
        /// </summary>
        public static int ModificarTarea(SqlTransaction tr, int nIdTarea, string sDesTarea, int nIdPT, Nullable<int> nIdActividad, int nOrden,
                                    string sFiniPl, string sFfinPl, decimal fDuracion, int nIdUltmodif, string t332_destarealong)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesTarea", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@dFiniPl", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFfinPl", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@dFFPR", SqlDbType.SmallDateTime, 4);
            aParam[7] = new SqlParameter("@rEtpl", SqlDbType.Float, 8);
            aParam[8] = new SqlParameter("@rETPR", SqlDbType.Float, 8);
            aParam[9] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[10] = new SqlParameter("@nIdultmodif", SqlDbType.Int, 4);
            aParam[11] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = sDesTarea;
            aParam[2].Value = nIdPT;
            //if (nIdActividad == -1) aParam[3].Value = null;
            //else aParam[3].Value = nIdActividad;
            aParam[3].Value = nIdActividad;
            if (sFiniPl == "") aParam[4].Value = null;
            else aParam[4].Value = DateTime.Parse(sFiniPl);
            if (sFfinPl == "") aParam[5].Value = null;
            else aParam[5].Value = DateTime.Parse(sFfinPl);
            //FFPR
            if (sFfinPl == "") aParam[6].Value = null;
            else aParam[6].Value = DateTime.Parse(sFfinPl);
            //ETPL
            aParam[7].Value = (float)fDuracion;
            //ETPR
            aParam[8].Value = (float)fDuracion;

            aParam[9].Value = nOrden;
            aParam[10].Value = nIdUltmodif;
            aParam[11].Value = t332_destarealong;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_OP_U", aParam);
        }
        /// <summary>
        /// Modifica los datos básicos del hito de fecha
        /// Se le llama desde la importación de OpenProj
        /// </summary>
        private static void ModificarHitoPE(SqlTransaction tr, int iHito, string sDesc, int iOrden, string sFecha, string t352_deshitolong)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesHito", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@dFecha", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@t352_deshitolong", SqlDbType.Text, 2147483647);
            aParam[0].Value = iHito;
            aParam[1].Value = sDesc;
            //DateTime dFecha= DateTime.Parse("01/01/1900");
            //if (sFecha != "") dFecha = DateTime.Parse(sFecha);
            aParam[2].Value = DateTime.Parse(sFecha).ToShortDateString();
            aParam[3].Value = iOrden;
            aParam[4].Value = t352_deshitolong;
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOPE_OP_U", aParam);
        }

        #endregion

        #region métodos para insertar en BBDD desde OpenProj
        public static string Insertar(SqlTransaction tr, int iCodUne, int iNumProy, int it305_IdProy,
                               string sTipo, string sDesc, int iPT, int iFase, int iActiv, int iMargen, int iOrden,
                               string sFIniPL, string sFfinPl, decimal fETPL, string sFFPR, decimal fETPR, string sFIniV, string sFfinV,
                               decimal fPresup, bool bFacturable, bool bObligaEst, bool bAvanceAutomatico, string sSituacion, string sObs, decimal fAvance)
        {
            int nResul = 0;
            byte iEstado;
            bool bEstadoTarea = false;
            string sAviso = "";
            sDesc = Utilidades.unescape(sDesc);
            sObs = Utilidades.unescape(sObs);
            switch (sTipo)
            {
                case "P":
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    bEstadoTarea = ProyTec.bFaltanValoresAE(tr, (short)iCodUne, null);
                    if (bEstadoTarea) iEstado = 2;
                    else iEstado = 1;
                    //if (bEstadoTarea) sAviso = "Se han insertado proyectos técnicos que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que el proyecto técnico no tiene valores asignados";
                    nResul = ProyTec.Insert(tr, sDesc, it305_IdProy, iEstado, bObligaEst, (short)iOrden, null, sObs, false, false, "X", fPresup, fAvance, bAvanceAutomatico, "");
                    break;
                case "F": nResul = EstrProy.InsertarFase(tr, sDesc, iOrden, sObs, fPresup, fAvance, bAvanceAutomatico); break;
                case "A": nResul = EstrProy.InsertarActividad(tr, iFase, sDesc, iOrden, sObs, fPresup, fAvance, bAvanceAutomatico); break;
                case "T":
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, null);
                    if (bEstadoTarea) iEstado = 2;
                    else iEstado = 1;
                    //if (bEstadoTarea) sAviso = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                    nResul = InsertarTarea(tr, sDesc, iPT, iActiv, iOrden, sFIniPL, sFfinPl, fETPL, sFFPR, fETPR, sFIniV, sFfinV, fPresup,
                                      iEstado, bFacturable, bAvanceAutomatico, sObs);
                    break;
                case "HM":
                case "HT":
                    nResul = EstrProy.InsertarHito(tr, sDesc, iMargen, iOrden, it305_IdProy);
                    break;
                case "HF":
                    //nResul = InsertarHitoPE(tr, iCodUne, iNumProy, sDesc, sFIniPL, iOrden);
                    nResul = EstrProy.InsertarHitoPE(tr, it305_IdProy, sDesc, sFIniPL, iOrden, sObs);
                    break;
            }
            return nResul.ToString() + "##" + sAviso;
        }
        //Para insertar la tarea desde la pantalla de importación de Openproj
        public static int InsertarTarea(SqlTransaction tr, string sDesTarea, int nIdPT, Nullable<int> nIdActividad, int nOrden,
                                   string sFiniPl, string sFfinPl, decimal fETPL, string sFFPR, decimal fETPR, string sFiniV, string sFfinV,
                                   decimal fPresupuesto, byte iEstado, bool bFacturable, bool bAvanceAutomatico, string sObs)
        {
            SqlParameter[] aParam = new SqlParameter[18];
            aParam[0] = new SqlParameter("@sDesTarea", SqlDbType.VarChar, 100);
            aParam[1] = new SqlParameter("@nIdPT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nIdActividad", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nIdPromotor", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@dFalta", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFiniPl", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@dFfinPl", SqlDbType.SmallDateTime, 4);
            aParam[7] = new SqlParameter("@rEtpl", SqlDbType.Float, 8);
            aParam[8] = new SqlParameter("@rPresupuesto", SqlDbType.Real, 4);
            aParam[9] = new SqlParameter("@dFiniV", SqlDbType.SmallDateTime, 4);
            aParam[10] = new SqlParameter("@dFfinV", SqlDbType.SmallDateTime, 4);
            aParam[11] = new SqlParameter("@nOrden", SqlDbType.SmallInt, 2);
            aParam[12] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
            aParam[13] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);
            aParam[14] = new SqlParameter("@t332_avanceauto", SqlDbType.Bit, 1);
            aParam[15] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);
            aParam[16] = new SqlParameter("@rEtpr", SqlDbType.Float, 8);
            aParam[17] = new SqlParameter("@dFfinPr", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = sDesTarea;
            aParam[1].Value = nIdPT;
            if (nIdActividad == -1) aParam[2].Value = null;
            else aParam[2].Value = nIdActividad;
            //aParam[2].Value = nIdActividad;
            aParam[3].Value = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            aParam[4].Value = DateTime.Now;
            if (sFiniPl == "") aParam[5].Value = null;
            else aParam[5].Value = DateTime.Parse(sFiniPl);
            if (sFfinPl == "") aParam[6].Value = null;
            else aParam[6].Value = DateTime.Parse(sFfinPl);
            aParam[7].Value = (double)fETPL;
            aParam[8].Value = fPresupuesto;
            if (sFiniV == "") aParam[9].Value = null;
            else aParam[9].Value = DateTime.Parse(sFiniV);
            if (sFfinV == "") aParam[10].Value = null;
            else aParam[10].Value = DateTime.Parse(sFfinV);
            aParam[11].Value = nOrden;
            aParam[12].Value = iEstado;
            aParam[13].Value = bFacturable;
            aParam[14].Value = bAvanceAutomatico;
            aParam[15].Value = sObs;
            aParam[16].Value = (double)fETPR;
            if (sFFPR == "") aParam[17].Value = null;
            else aParam[17].Value = DateTime.Parse(sFFPR);

            return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREAPSP_OP_I", aParam));
        }
        #endregion

        #region Métodos para crear el contenido XML
        /// <summary>
        /// Genera un nodo XML para añadir al nodo Task
        /// En este nodo se indicará para el atributo Numero1 el código de ítem que le correponde en SUPER
        /// </summary>
        private static XmlElement CrearExtendedAttributeXml(XmlDocument doc, string sUID, string sFieldID, string sValue)
        {
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement ExtendedAttribute = doc.CreateElement("ExtendedAttribute", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            ExtendedAttribute.AppendChild(UID);

            System.Xml.XmlElement FieldID = doc.CreateElement("FieldID", en);
            FieldID.InnerText = sFieldID;
            ExtendedAttribute.AppendChild(FieldID);

            System.Xml.XmlElement Value = doc.CreateElement("Value", en);
            Value.InnerText = sValue;
            ExtendedAttribute.AppendChild(Value);

            return ExtendedAttribute;
        }

        public static XmlElement CrearResourceXml(XmlDocument doc, string sUID, string sID, string sInitials, string sName,
                            string sMaxUnits, string sAccrueAt, string sStandardRate, string sStandardRateFormat,
                            string sCost, string sOvertimeRate, string sCostPerUse)
        {
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement Resource = doc.CreateElement("Resource", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Resource.AppendChild(UID);

            System.Xml.XmlElement ID = doc.CreateElement("ID", en);
            ID.InnerText = sID;
            Resource.AppendChild(ID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Resource.AppendChild(Name);

            System.Xml.XmlElement Initials = doc.CreateElement("Initials", en);
            Initials.InnerText = sInitials;
            Resource.AppendChild(Initials);

            System.Xml.XmlElement MaxUnits = doc.CreateElement("MaxUnits", en);
            MaxUnits.InnerText = sMaxUnits;
            Resource.AppendChild(MaxUnits);

            System.Xml.XmlElement AccrueAt = doc.CreateElement("AccrueAt", en);
            AccrueAt.InnerText = sAccrueAt;
            Resource.AppendChild(AccrueAt);

            System.Xml.XmlElement StandardRate = doc.CreateElement("StandardRate", en);
            StandardRate.InnerText = sStandardRate;
            Resource.AppendChild(StandardRate);

            System.Xml.XmlElement StandardRateFormat = doc.CreateElement("StandardRateFormat", en);
            StandardRateFormat.InnerText = sStandardRateFormat;
            Resource.AppendChild(StandardRateFormat);

            System.Xml.XmlElement Cost = doc.CreateElement("Cost", en);
            Cost.InnerText = sCost;
            Resource.AppendChild(Cost);

            System.Xml.XmlElement OvertimeRate = doc.CreateElement("OvertimeRate", en);
            OvertimeRate.InnerText = sOvertimeRate;
            Resource.AppendChild(OvertimeRate);

            System.Xml.XmlElement CostPerUse = doc.CreateElement("CostPerUse", en);
            CostPerUse.InnerText = sCostPerUse;
            Resource.AppendChild(CostPerUse);

            System.Xml.XmlElement CalendarUID = doc.CreateElement("CalendarUID", en);
            CalendarUID.InnerText = fgGetCalendario();//"2";//Es el ID del calendario en el que se trabaja todos los días y 8 horas al día
            Resource.AppendChild(CalendarUID);

            return Resource;
        }

        public static XmlElement CrearCalendarioUsuarioXml(XmlDocument doc, string sUID, string sName, string sBaseUID)
        {
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement Calendar = doc.CreateElement("Calendar", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Calendar.AppendChild(UID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Calendar.AppendChild(Name);

            System.Xml.XmlElement BaseCalendarUID = doc.CreateElement("BaseCalendarUID", en);
            BaseCalendarUID.InnerText = sBaseUID;
            Calendar.AppendChild(BaseCalendarUID);

            return Calendar;
        }

        public static XmlElement CrearLineaBaseTareaXml(XmlDocument doc, string sNumber, string sStart, string sFinish, string sWork)
        {
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement Baseline = doc.CreateElement("Baseline", en);

            System.Xml.XmlElement Number = doc.CreateElement("Number", en);
            Number.InnerText = sNumber;
            Baseline.AppendChild(Number);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            Baseline.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            Baseline.AppendChild(Finish);

            System.Xml.XmlElement Work = doc.CreateElement("Work", en);
            Work.InnerText = sWork;
            Baseline.AppendChild(Work);

            return Baseline;
        }

        public static XmlElement CrearTareaXml(XmlDocument doc, string sUID, string sID, string sName, string sObs, string sWBS, string sOutlineLevel,
                                         string sPriority, string sStart, string sFinish, string sDuration, string sDurationFormat,
                                         string sIsSubproject, string sFixedCostAccrual, string sPdte, decimal Consumido,
                                         string sFIPL, string sFFPL, decimal dETPL, decimal dETPR, decimal dParticipacion, bool bLinBase)
        {
            //bool bLinBase = false;
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement Task = doc.CreateElement("Task", en);

            if (sWBS == "HF")
            {
                sFinish = sStart;
                sDuration = "PT0H0M0S";
                sPdte = "PT0H0M0S";
                dETPL = 0;
                dETPR = 0;
                sFFPL = sFIPL;
                dParticipacion = 1;
                Consumido = 0;
            }
            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            //El código del item se lo paso en el campo Numero1, porque el valor de este campo lo reescribe OpenProj al grabar
            //UID.InnerText = sUID;
            UID.InnerText = sID;
            Task.AppendChild(UID);

            System.Xml.XmlElement ID = doc.CreateElement("ID", en);
            ID.InnerText = sID;
            Task.AppendChild(ID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Task.AppendChild(Name);

            //Creo que es el que indica que la tarea sea de "Duración fijada" (para que tenga un trabajo(esfuerzo) diferente en horas a la duración)
            System.Xml.XmlElement Type = doc.CreateElement("Type", en);
            Type.InnerText = "1";
            Task.AppendChild(Type);

            System.Xml.XmlElement WBS = doc.CreateElement("WBS", en);
            WBS.InnerText = sWBS;
            Task.AppendChild(WBS);

            System.Xml.XmlElement OutlineLevel = doc.CreateElement("OutlineLevel", en);
            OutlineLevel.InnerText = sOutlineLevel;
            Task.AppendChild(OutlineLevel);

            System.Xml.XmlElement Priority = doc.CreateElement("Priority", en);
            Priority.InnerText = sPriority;
            Task.AppendChild(Priority);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            Task.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            Task.AppendChild(Finish);

            System.Xml.XmlElement Duration = doc.CreateElement("Duration", en);
            Duration.InnerText = sDuration;
            Task.AppendChild(Duration);

            System.Xml.XmlElement DurationFormat = doc.CreateElement("DurationFormat", en);
            DurationFormat.InnerText = sDurationFormat;
            Task.AppendChild(DurationFormat);

            //Esfuerzo de la tarea
            System.Xml.XmlElement miWork = doc.CreateElement("Work", en);
            if (dETPR != 0)
                miWork.InnerText = flDuracionOpenProj(dETPR, "", "");
            else
                miWork.InnerText = flDuracionOpenProj(dETPL, "", "");
            Task.AppendChild(miWork);
            //para indicarle que es un Hito
            if (sWBS == "HF")
            {
                System.Xml.XmlElement Milestone = doc.CreateElement("Milestone", en);
                Milestone.InnerText = "1";
                Task.AppendChild(Milestone);
            }
            //para indicarle el grado de avance de la tarea
            //if (sPdte != "" && sPdte != "PT0H0M0S")
            if (Consumido != 0 && dParticipacion != 0)
            {
                //System.Xml.XmlElement Work = doc.CreateElement("Work", en);
                //Work.InnerText = "PT0H0M0S";
                //Task.AppendChild(Work);
                System.Xml.XmlElement Stop = doc.CreateElement("Stop", en);
                if (dETPR <= Consumido)//Para que aparezca la marca de tarea finalizada
                    Stop.InnerText = sFinish;
                else
                {
                    //Segundos consumidos
                    int nSegConsumidos = (int)(Consumido * 3600);
                    //Segundos que se pueden asignar cada día
                    int nSegundosDia = (int)Math.Round(8 * dParticipacion * 3600);
                    //Stop.InnerText = flFechaAvance(Consumido, sStart, dParticipacion);
                    Stop.InnerText = flFechaAvance(sStart, nSegConsumidos, nSegundosDia);
                }
                Task.AppendChild(Stop);
            }

            System.Xml.XmlElement Critical = doc.CreateElement("Critical", en);
            Critical.InnerText = "1";
            Task.AppendChild(Critical);

            System.Xml.XmlElement IsSubproject = doc.CreateElement("IsSubproject", en);
            IsSubproject.InnerText = sIsSubproject;
            Task.AppendChild(IsSubproject);

            System.Xml.XmlElement FixedCostAccrual = doc.CreateElement("FixedCostAccrual", en);
            FixedCostAccrual.InnerText = sFixedCostAccrual;
            Task.AppendChild(FixedCostAccrual);

            //para indicarle el grado de avance de la tarea
            if (Consumido != 0)
            {
                System.Xml.XmlElement RemainingDuration = doc.CreateElement("RemainingDuration", en);
                //03/11/2011
                //decimal dPendiente = (dETPR - Consumido) / (8 * dParticipacion);
                decimal dPendiente = dETPR - Consumido;
                if (dPendiente < 0) dPendiente = 0;
                RemainingDuration.InnerText = flDuracionOpenProj(dPendiente, "", "");
                Task.AppendChild(RemainingDuration);
            }

            //Para indicarle que la tarea empiece en una fecha diferente a la de inicio del proyetco
            System.Xml.XmlElement ConstraintType = doc.CreateElement("ConstraintType", en);
            //ConstraintType.InnerText = "4";//No iniciar antes que
            ConstraintType.InnerText = "2";//Debe empezar ya
            Task.AppendChild(ConstraintType);

            //Para indicarle que coja el calendario en el que todos los días son laborables
            System.Xml.XmlElement CalendarUID = doc.CreateElement("CalendarUID", en);
            CalendarUID.InnerText = fgGetCalendario();//"2";
            Task.AppendChild(CalendarUID);

            //para indicarle la fecha de inicio de la tarea
            System.Xml.XmlElement ConstraintDate = doc.CreateElement("ConstraintDate", en);
            ConstraintDate.InnerText = sStart;
            Task.AppendChild(ConstraintDate);

            //Prueba
            System.Xml.XmlElement IgnoreResourceCalendar = doc.CreateElement("IgnoreResourceCalendar", en);
            IgnoreResourceCalendar.InnerText = "1";
            Task.AppendChild(IgnoreResourceCalendar);

            //para indicarle la descripción de la tarea
            if (sObs.Trim() != "")
            {
                System.Xml.XmlElement Notes = doc.CreateElement("Notes", en);
                Notes.InnerText = sObs;
                Task.AppendChild(Notes);
            }
            //Creo un nodo nuevo donde guardar el código del item (por si luego quiero importar)
            Task.AppendChild(CrearExtendedAttributeXml(doc, "1", "188743767", sUID));

            //Miro si es necesario guardar linea base
            //if (sWBS == "T")
            //{
            string sStartIni = "", sFinishIni = "";
            sStartIni = flGetFechaOpenProj(sFIPL, "IJ");
            sFinishIni = flGetFechaOpenProj(sFFPL, "FJ");
            //if (sStartIni != sStart) bLinBase = true;
            //else
            //{
            //    if (sFinishIni != sFinish) bLinBase = true;
            //    else
            //    {
            //        if (dETPL != dETPR) bLinBase = true;
            //    }
            //}
            if (bLinBase)
            {
                Task.AppendChild(CrearLineaBaseTareaXml(doc, "0", sStartIni, sFinishIni, flDuracionOpenProj(dETPL, "", "")));
            }
            //}

            return Task;
        }

        private static XmlElement CrearTimephasedDataXml(XmlDocument doc, string sType, string sUID, string sStart, string sFinish,
                                                  string sUnit, string sValue)
        {
            string en = "http://schemas.microsoft.com/project";
            System.Xml.XmlElement TimephasedData = doc.CreateElement("TimephasedData", en);

            System.Xml.XmlElement Type = doc.CreateElement("Type", en);
            Type.InnerText = sType;
            TimephasedData.AppendChild(Type);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            TimephasedData.AppendChild(UID);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            TimephasedData.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            TimephasedData.AppendChild(Finish);

            System.Xml.XmlElement Unit = doc.CreateElement("Unit", en);
            Unit.InnerText = sUnit;
            TimephasedData.AppendChild(Unit);

            System.Xml.XmlElement Value = doc.CreateElement("Value", en);
            Value.InnerText = sValue;
            TimephasedData.AppendChild(Value);

            return TimephasedData;
        }

        public static XmlElement CrearAssignmentXml(XmlDocument doc, ItemsProyecto oItem, string sUID, string sResourceUID, decimal dEsfuerzo,
                                              decimal dParticipacion, decimal dParticipacionBase, string sPdte, bool bConLineBase, int nDifDias)
        {
            int nSegTotal = (int)(dEsfuerzo * 3600);
            //Tiempo consumido (se reparte en registros Tipo 2)
            int nSegConsumidos = (int)(oItem.Consumido * 3600);
            //El tiempo que va desde lo consumido hasta el total (se reparte en registros Tipo 1)
            int nSegPrevistos = nSegTotal - nSegConsumidos;
            //Segundos que ya he asignado
            int nSegAsignados = 0;
            //Segundos que se pueden asignar cada día
            int nSegundosDia = (int)Math.Floor(8 * dParticipacion * 3600);
            //int nSegundosDia = (int)Math.Round(8 * dParticipacion * 3600);
            //Segundos que añado a la parte consumida 
            int nSegConsResto = 0;
            bool bContinuar = true;
            string sHorasDia = "PT0H0M0S";
            string sHorasResto = "PT0H0M0S";

            string en = "http://schemas.microsoft.com/project"; 
            string sWork = flDuracionOpenProj(dEsfuerzo, "", "");
            string sUnits = dParticipacion.ToString().Replace(",", ".");

            //Lo que se puede asignar cada dia en formato Openproj
            sHorasDia = flGetHorasDia(nSegundosDia);

            System.Xml.XmlElement Assignment = doc.CreateElement("Assignment", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Assignment.AppendChild(UID);

            System.Xml.XmlElement TaskUID = doc.CreateElement("TaskUID", en);
            TaskUID.InnerText = oItem.orden.ToString();
            Assignment.AppendChild(TaskUID);

            System.Xml.XmlElement ResourceUID = doc.CreateElement("ResourceUID", en);
            ResourceUID.InnerText = sResourceUID;
            Assignment.AppendChild(ResourceUID);

            System.Xml.XmlElement RemainingWork = doc.CreateElement("RemainingWork", en);
            if (sPdte != "")
                RemainingWork.InnerText = sPdte;
            else
                RemainingWork.InnerText = "PT0H0M0S";//sWork;
            Assignment.AppendChild(RemainingWork);

            System.Xml.XmlElement Units = doc.CreateElement("Units", en);
            Units.InnerText = sUnits;// 1;
            Assignment.AppendChild(Units);

            System.Xml.XmlElement Work = doc.CreateElement("Work", en);
            Work.InnerText = sWork;
            Assignment.AppendChild(Work);

            // esto debería crear día a día en función del rango de fechas. Ojo con el comienzo es un día antes que el comienzo real
            if (oItem.tipo!="HF" && oItem.PRIMER_CONSUMO != "" && oItem.FFPR != "")
            {
                DateTime dtAux = DateTime.Parse(oItem.PRIMER_CONSUMO);
                DateTime dtIni = DateTime.Parse(oItem.PRIMER_CONSUMO);
                DateTime dtFin = DateTime.Parse(oItem.FFPR);
                //metemos dias correspondientes al grado de avance y luego los restantes hasta el final son líneas del estado actual
                #region Grado de avance Type=2
                if (oItem.Consumido != 0 && nSegundosDia != 0)
                {
                    if (nSegConsumidos > nSegundosDia)
                    {
                        while (bContinuar)
                        {
                            if ((nSegAsignados + nSegundosDia) <= nSegConsumidos)
                            {
                                if (dtAux == dtFin) //if (dtAux == dtFin.AddDays(-1))
                                {//Si estoy en el último día, meto los segundos restantes
                                    bContinuar = false;
                                    if (nSegAsignados < nSegConsumidos)
                                    {
                                        nSegConsResto = nSegConsumidos - nSegAsignados;
                                        sHorasResto = flGetHorasDia(nSegConsResto);
                                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                            flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"), "3", sHorasResto));
                                        nSegAsignados = nSegConsumidos;
                                    }
                                }
                                else
                                {//Asigno un día y continuo el bucle

                                    Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                                        flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                        flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"), "3", sHorasDia));
                                    nSegAsignados += nSegundosDia;
                                    dtAux = dtAux.AddDays(1);
                                }
                            }
                            else
                                bContinuar = false;
                        }
                        if (nSegAsignados < nSegConsumidos)
                            {
                                nSegConsResto = nSegConsumidos - nSegAsignados;
                                sHorasResto = flGetHorasDia(nSegConsResto);
                                Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                    flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"), "3", sHorasResto));
                                nSegAsignados = nSegConsumidos;
                                dtAux = dtAux.AddDays(1);
                            }
                    }
                    else
                    {
                        sHorasResto = flGetHorasDia(nSegConsumidos);
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                            flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"), "3", sHorasResto));
                        nSegAsignados = nSegConsumidos;
                        dtAux = dtAux.AddDays(1);
                    }
                    //dtIni = dtAux;
                }
                #endregion
                #region Estado actual del proyecto Type=1
                //Si lo presupuestado y lo consumido son iguales no ponemos registros Tipo 1 puesto que todos los días irán con Tipo 2
                //nSegPrevistos = nSegTotal - nSegConsumidos
                if (nSegPrevistos > 0)
                {
                    nSegAsignados = 0;
                    //if (Fechas.DateDiff("day", dtIni, dtFin) == 0)
                    //    Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                    //                            flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                    //                            flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sWork));
                    //else
                    //{
                        dtAux = dtAux.AddDays(-1);
                        if (nSegPrevistos > nSegundosDia)
                        {
                            bContinuar = true;
                            while (bContinuar)
                            {
                                if ((nSegAsignados +  nSegundosDia) <= nSegPrevistos)
                                {
                                    if (dtAux == dtFin.AddDays(-1))
                                    {//Si estoy en el último día, meto los segundos restantes
                                        bContinuar = false;
                                        if (nSegAsignados < nSegPrevistos)
                                        {
                                            nSegConsResto = nSegPrevistos - nSegAsignados;
                                            sHorasResto = flGetHorasDia(nSegConsResto);
                                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
                                                                flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "FD"), "3", sHorasResto));
                                            nSegAsignados = nSegPrevistos;
                                        }
                                    }
                                    else
                                        {//Asigno un día y continuo el bucle
                                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
                                                                flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "FD"), "3", sHorasDia));
                                            nSegAsignados += nSegundosDia;
                                            dtAux = dtAux.AddDays(1);
                                        }
                                }
                                else
                                {
                                    bContinuar = false;
                                    if (nSegAsignados < nSegPrevistos)
                                    {
                                        nSegConsResto = nSegPrevistos - nSegAsignados;
                                        sHorasResto = flGetHorasDia(nSegConsResto);
                                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
                                                            flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "FD"), "3", sHorasResto));
                                        nSegAsignados = nSegPrevistos;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sHorasResto = flGetHorasDia(nSegPrevistos);
                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
                                                flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "FD"), "3", sHorasResto));
                            nSegAsignados = nSegPrevistos;
                        }
                    //}
                }
                #endregion
                #region Línea base. Type=4
                if (bConLineBase)
                {
                    bool bAsignadoPrimerElem = false;
                    sWork = flDuracionOpenProj(oItem.ETPL, "", "");
                    sUnits = dParticipacionBase.ToString().Replace(",", ".");

                    dtAux = DateTime.Parse(oItem.FIPL);
                    dtIni = DateTime.Parse(oItem.FIPL);
                    dtFin = DateTime.Parse(oItem.FFPL);
                    nDifDias = Fechas.DateDiff("day", dtIni, dtFin); //* 24;
                    if (nDifDias <= 0)
                    {
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sWork));
                    }
                    else
                    {
                        //Openproj no tiene en cuenta las asignaciones de línea base en sábados y domingos (supongo que porque para sus cálculos
                        //internos toma el calendario estandar) a pesar de que está indicado el calendario 24x7
                        //Por ello es necesario hacer el reparto entre los días laborables
                        //int nCont = 0;
                        int iNumDiasLaborables = flNumLaborables(dtIni, dtFin);
                        if (iNumDiasLaborables == 0) iNumDiasLaborables = 1;
                        nSegPrevistos = (int)(oItem.ETPL * 3600);
                        nSegundosDia = (int)Math.Round((decimal)nSegPrevistos / iNumDiasLaborables);
                        nSegAsignados = 0;
                        #region prueba 1
                        //dtAux = dtAux.AddDays(-1);
                        /*
                        if (nSegPrevistos > nSegundosDia)
                        {
                            bContinuar = true;
                            while (bContinuar)
                            {
                                if (!flEsLaborable(dtAux))
                                {
                                    dtAux = dtAux.AddDays(1);
                                }
                                else
                                {
                                    if ((nSegAsignados + nSegundosDia) <= nSegPrevistos)
                                    {
                                        if (dtAux >= dtFin)//if (dtAux == dtFin.AddDays(-1))
                                        {//Si estoy en el último día, meto los segundos restantes
                                            bContinuar = false;
                                            if (nSegAsignados < nSegPrevistos)
                                            {
                                                nSegConsResto = nSegPrevistos - nSegAsignados;
                                                sHorasResto = flGetHorasDia(nSegConsResto);
                                                Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sHorasResto));
                                            }
                                        }
                                        else
                                        {//Asigno un día y continuo el bucle
                                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sHorasDia));
                                            nSegAsignados += nSegundosDia;
                                            dtAux = dtAux.AddDays(1);
                                            nCont++;
                                        }
                                    }
                                    else
                                    {
                                        bContinuar = false;
                                        if (nSegAsignados < nSegPrevistos)
                                        {
                                            nSegConsResto = nSegPrevistos - nSegAsignados;
                                            sHorasResto = flGetHorasDia(nSegConsResto);
                                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sHorasResto));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            sHorasResto = flGetHorasDia(nSegPrevistos);
                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sHorasResto));
                        }
                         * */
                        #endregion

                        #region prueba 2
                        //nDifDias++;
                        bAsignadoPrimerElem = false;
                        //Si se puede, reparto una hora para cada día y el resto al primer día
                        if (oItem.ETPL >= (decimal)iNumDiasLaborables)
                        {
                            #region reparto de horas
                            for (int i = 0; i <= nDifDias; i++)
                            {
                                if (flEsLaborable(dtAux))
                                {
                                    if (!bAsignadoPrimerElem)
                                    {
                                        decimal dHoras = oItem.ETPL - (decimal)iNumDiasLaborables + 1;
                                        sHorasDia = flHorasDia(dHoras, 1);
                                        bAsignadoPrimerElem = true;
                                    }
                                    else
                                        sHorasDia = "PT1H0M0S";
                                    //Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                    //                        flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                    //                        flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"),
                                    //                        "3", sHorasDia));
                                    Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "IB"),
                                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "FB"),
                                                            "3", sHorasDia));
                                }
                                else
                                {//Si el último día es festivo, le pongo 1 seg para que la fecha de fin quede OK
                                    if (i == nDifDias)
                                    {
                                        sHorasDia = "PT0H0M1S";
                                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IB"),
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FB"),
                                                                "3", sHorasDia));
                                    }
                                }
                                dtAux = dtAux.AddDays(1);
                            }
                            #endregion
                        }
                        else//Hay menos horas que dias
                        {
                            //Si se puede, reparto un minuto para cada día laborable y el resto al primer día
                            decimal dMinutos = oItem.ETPL * 60;
                            if ((int)System.Math.Floor(dMinutos) >= iNumDiasLaborables)
                            {
                                #region reparto de minutos
                                for (int i = 0; i <= nDifDias; i++)
                                {
                                    if (flEsLaborable(dtAux))
                                    {
                                        if (!bAsignadoPrimerElem)
                                        {
                                            //Calculo el nº de minutos total
                                            decimal dMinu = oItem.ETPL * 60;
                                            //Le resto los usados en el intervalo
                                            dMinu = dMinu - iNumDiasLaborables + 1;

                                            sHorasDia = flHorasDia(dMinu / 60, 1);
                                            bAsignadoPrimerElem = true;
                                        }
                                        else
                                            sHorasDia = "PT0H1M0S";
                                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IB"),
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FB"),
                                                                "3", sHorasDia));
                                    }
                                    else
                                    {
                                        if (i == nDifDias)
                                        {
                                            sHorasDia = "PT0H0M1S";
                                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "IB"),
                                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "FB"),
                                                                    "3", sHorasDia));
                                        }
                                    }
                                    dtAux = dtAux.AddDays(1);
                                }
                                #endregion
                            }
                            else//Hay menos minutos que dias
                            {
                                #region reparto de segundos
                                //1 segundo para cada día y el resto al primer día
                                for (int i = 0; i <= nDifDias; i++)
                                {
                                    if (flEsLaborable(dtAux))
                                    {
                                        if (!bAsignadoPrimerElem)
                                        {
                                            //Calculo el nº de segundos total
                                            decimal dSegundos = oItem.ETPL * 3600;
                                            //Le resto los usados en el intervalo
                                            dSegundos = dSegundos - iNumDiasLaborables + 1;
                                            sHorasDia = flHorasDia(dSegundos / 3600, 1);
                                            bAsignadoPrimerElem = true;
                                        }
                                        else
                                            sHorasDia = "PT0H0M1S";
                                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IB"),
                                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FB"),
                                                                "3", sHorasDia));
                                    }
                                    dtAux = dtAux.AddDays(1);
                                }
                                #endregion
                            }
                        }

                        #endregion
                    }
                }
                #endregion
            }
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-25T22:00:00", "2011-07-26T22:00:00", "3", "PT8H0M0S"));
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-26T22:00:00", "2011-07-27T22:00:00", "3", "PT8H0M0S"));
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-27T22:00:00", "2011-07-28T22:00:00", "3", "PT8H0M0S"));
            return Assignment;
        }
        public static XmlElement CrearAssignmentPadreXml(XmlDocument doc, ItemsProyecto oItem, string sUID, string sResourceUID)
        {
            string sHorasDia = "PT8H0M0S";
            string en = "http://schemas.microsoft.com/project";
            // esto debería crear día a día en función del rango de fechas. Ojo con el comienzo es un día antes que el comienzo real
            DateTime dtIni = DateTime.Parse(oItem.FIPL);
            DateTime dtAux = dtIni;
            DateTime dtFin = DateTime.Parse(oItem.FFPR);
            int nDias = Fechas.DateDiff("day", dtIni, dtFin)+1;
            string sWork = flDuracionOpenProj(nDias * 8, "", "");

            System.Xml.XmlElement Assignment = doc.CreateElement("Assignment", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Assignment.AppendChild(UID);

            System.Xml.XmlElement TaskUID = doc.CreateElement("TaskUID", en);
            TaskUID.InnerText = oItem.orden.ToString();
            Assignment.AppendChild(TaskUID);

            System.Xml.XmlElement ResourceUID = doc.CreateElement("ResourceUID", en);
            ResourceUID.InnerText = sResourceUID;
            Assignment.AppendChild(ResourceUID);

            System.Xml.XmlElement RemainingWork = doc.CreateElement("RemainingWork", en);
            RemainingWork.InnerText = sWork;
            Assignment.AppendChild(RemainingWork);

            System.Xml.XmlElement Units = doc.CreateElement("Units", en);
            Units.InnerText = "1";
            Assignment.AppendChild(Units);

            System.Xml.XmlElement Work = doc.CreateElement("Work", en);
            Work.InnerText = sWork;
            Assignment.AppendChild(Work);

            if (nDias == 1)
                Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                        flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                        flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sWork));
            else
            {
                dtAux = dtAux.AddDays(-1);
                for (int i=0; i< nDias; i++)
                {
                    Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                        flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
                                        flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "FD"), "3", sHorasDia));
                    dtAux = dtAux.AddDays(1);
                }
            }
            return Assignment;
        }
        #endregion

        #region Utilidades
        //Devuelve el código del calendario por defecto. En teoría debería ser 2
        //Pero en Openproj cuando se abre y se cierra un proyecto en formato XML, borra el calendario por defecto que
        //se le hubiera indicado y pone el calendario 1
        public static string fgGetCalendario()
        {
            return "2";
        }
        //Rellena con un cero por la izquierda si el nº que se pasa como parámetro tiene menos de dos dígitos
        public static string flRellenar(int nNum)
        {
            string sRes;
            if (nNum < 10)
                sRes = "0" + nNum.ToString();
            else
                sRes = nNum.ToString();
            return sRes;
        }

        //Devuelve una fecha en formato OpenProj
        public static string flGetFechaOpenProj(string sFecha, string sTipo)
        {
            //string sRes = "1970-01-01T01:00:00";
            DateTime dtHoy = DateTime.Now;
            string sRes = dtHoy.Year.ToString() + "-" + flRellenar(dtHoy.Month) + "-" + flRellenar(dtHoy.Day) + "T01:00:00";
            if (sFecha != "")
            {
                DateTime dtAux = DateTime.Parse(sFecha);
                //sRes = dtAux.Year.ToString() + "-" +
                //       (dtAux.Month < 10) ? "0" + dtAux.Month.ToString() : dtAux.Month.ToString() + "-" + 
                //       (dtAux.Day < 10) ? "0" + dtAux.Day.ToString() : dtAux.Day.ToString();
                sRes = dtAux.Year.ToString() + "-" + flRellenar(dtAux.Month) + "-" + flRellenar(dtAux.Day);
                switch (sTipo)
                {
                    case "ID"://Inicio día
                        //sRes += "T01:00:00";
                        sRes += "T08:00:00";
                        break;
                    case "FD"://Fin día
                        sRes += "T22:00:00";
                        //sRes += "T16:00:00";
                        break;
                    case "IJ"://Inicio jornada
                        sRes += "T08:00:00";
                        break;
                    case "FJ"://Fin jornada
                        sRes += "T16:00:00";
                        break;
                    case "IB"://Inicio jornada para líneas base
                        sRes += "T10:00:00";
                        break;
                    case "FB"://Fin jornada para líneas base
                        sRes += "T14:00:00";
                        break;
                    default://En tipo hemos pasado un nº de horas
                        decimal dHoras = decimal.Parse(sTipo);
                        int nHoras = (int)Math.Floor(dHoras);
                        int nMinutos = (int)(60 * (dHoras - nHoras));
                        string sHoras = (8 + nHoras).ToString();
                        if (sHoras.Length == 1) sHoras = "0" + sHoras;
                        string sMinutos = nMinutos.ToString();
                        if (sMinutos.Length == 1) sMinutos = "0" + sMinutos;
                        sRes += "T" + sHoras + ":" + sMinutos + ":00";
                        break;

                }
            }
            return sRes;
        }

        //En función del margen en SUPER, se obtiene el nivel de identación en OpenProj
        public static string flGetIdentacion(int iMargen)
        {
            string sRes = "1";
            switch (iMargen)
            {
                case 0:
                    sRes = "1";
                    break;
                case 20:
                    sRes = "2";
                    break;
                case 40:
                    sRes = "3";
                    break;
                case 60:
                    sRes = "4";
                    break;
            }
            return sRes;
        }

        //Dado el esfuerzo total previso y lo consumido, devuelve el pendiente de realizar
        public static string flPdteOpenProj(decimal dEtpr, decimal dConsumido)
        {
            string sRes = "";
            decimal dPdte = dEtpr - dConsumido, dMinutos = 0;
            if (dPdte > 0)
            {
                int nHoras = 0, nMinutos = 0;

                nHoras = (int)System.Math.Floor(dPdte);
                dMinutos = dPdte - nHoras;
                nMinutos = (int)System.Math.Floor(dMinutos * 60);

                sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M0S";
            }
            else
            {
                if (dPdte < 0)//se ha consumido mas de lo presupuestado -> ponemos cero como pendiente
                    sRes = "PT0H0M0S";
            }
            return sRes;
        }

        //Si hay Esfuerzo Total Previsto se toma ese valor. Sino la diferencia entre fecha primer consumo y fecha fin prevista
        //(si no hay fecha de fin prevista, se toma la fecha de fin planificada)
        public static int flDuracionDias(string sFechaIni, string sFechaFin)
        {
            int iRes = 0;
            if (sFechaIni != "" && sFechaFin != "")
            {
                DateTime dtIni = DateTime.Parse(sFechaIni);
                DateTime dtFin = DateTime.Parse(sFechaFin);
                iRes = (Fechas.DateDiff("day", dtIni, dtFin) + 1);
            }
            return iRes;
        }

        public static string flDuracionOpenProj(decimal dEtpr, string sFechaIni, string sFechaFin)
        {
            string sRes = "PT0H0M0S";
            if (dEtpr == 0)
            {
                if (sFechaIni != "" && sFechaFin != "")
                {
                    DateTime dtIni = DateTime.Parse(sFechaIni);
                    DateTime dtFin = DateTime.Parse(sFechaFin);
                    int nDifDias = (Fechas.DateDiff("day", dtIni, dtFin) + 1) * 8;
                    if (nDifDias >= 0)
                        sRes = "PT" + nDifDias.ToString() + "H0M0S";
                }
            }
            else
            {
                int nHoras = 0, nMinutos = 0;
                decimal dMinutos = 0;

                nHoras = (int)System.Math.Floor(dEtpr);
                dMinutos = dEtpr - nHoras;
                nMinutos = (int)System.Math.Floor(dMinutos * 60);

                sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M0S";
            }
            return sRes;
        }

        //Devuelve la fecha de Stop según la fecha de inicio de la tarea y las horas consumidas
        //(se supone que cada día son 8 horas)
        private static string flFechaAvance(decimal Consumido, string sFechaIni, decimal dParticipacion)
        {
            string sRes = "";
            if (sFechaIni != "")
            {
                if (Consumido == 0 || dParticipacion == 0)
                {
                    sRes = flGetFechaOpenProj(sFechaIni, "FD");
                }
                else
                {
                    int nDias = 0, nHoras = 0, nMinutos = 0;
                    decimal dMinutos = 0, dHorasDia = 8 * dParticipacion;
                    DateTime dtAux = DateTime.Parse(sFechaIni);
                    //En función de lo consumido y de lo que trabaja cada dia, calculo el nº de días de avance
                    nDias = (int)System.Math.Floor(Consumido / dHorasDia);
                    dtAux = dtAux.AddDays(nDias);
                    //Lo que queda será el avance en horas, minutos
                    nHoras = (int)System.Math.Floor(Consumido - (nDias * dHorasDia));
                    dtAux = dtAux.AddHours(nHoras);

                    dMinutos = Consumido - (nDias * dHorasDia) - nHoras;
                    nMinutos = (int)System.Math.Floor(dMinutos * 60);
                    dtAux = dtAux.AddMinutes(nMinutos);

                    sRes = dtAux.Year.ToString() + "-" + flRellenar(dtAux.Month) + "-" + flRellenar(dtAux.Day) + "T" +
                           flRellenar(dtAux.Hour) + ":" + flRellenar(dtAux.Minute) + ":00";
                }
            }
            return sRes;
        }
        //Devuelve la fecha de Stop según la fecha de inicio de la tarea, los segundos consumidos y los segundos que se trabaja diariamente
        private static string flFechaAvance(string sFechaIni, int nSegConsumidos, int nSegDia)
        {
            string sRes = "";
            if (sFechaIni != "")
            {
                if (nSegConsumidos == 0 || nSegDia == 0)
                {
                    sRes = flGetFechaOpenProj(sFechaIni, "FD");
                }
                else
                {
                    int nDias = 0, nHoras = 0, nMinutos = 0, nSegundos=0, nResto=0;
                    DateTime dtAux = DateTime.Parse(sFechaIni);
                    //En función de lo consumido y de lo que trabaja cada dia, calculo el nº de días de avance
                    nDias = (int)System.Math.Floor((decimal)nSegConsumidos / nSegDia);
                    //Lo que queda será el avance en horas, minutos
                    nResto = nSegConsumidos - (nSegDia * nDias);
                    if (nResto > 0)
                    {
                        nHoras = (int)System.Math.Floor((decimal)nResto / 3600);
                        nResto = nResto - (nHoras * 3600);
                        if (nResto > 0)
                        {
                            nMinutos= (int)System.Math.Floor((decimal)nResto / 60);
                            nResto = nResto - (nMinutos * 60);
                            if (nResto > 0)
                            {
                                nSegundos = nResto;
                            }
                        }
                    }
                    dtAux = dtAux.AddDays(nDias);
                    dtAux = dtAux.AddHours(nHoras);
                    dtAux = dtAux.AddMinutes(nMinutos);
                    dtAux = dtAux.AddSeconds(nSegundos);

                    sRes = dtAux.Year.ToString() + "-" + flRellenar(dtAux.Month) + "-" + flRellenar(dtAux.Day) + "T" +
                           flRellenar(dtAux.Hour) + ":" + flRellenar(dtAux.Minute) + ":" + flRellenar(dtAux.Second);
                }

            }
            return sRes;
        }

        //Dado un porcentaje (de participación de un usuario en una tarea)
        //Devuelve el tiempo que le debe dedicar (teniendo en cuenta que la jornada la tomamos como de 8 horas)
        private static string flPorcentajeDia(string sPorcentaje)
        {
            string sRes = "PT0H0M0S";
            if (sPorcentaje != "")
            {
                sPorcentaje = sPorcentaje.Replace(".", ",");
                decimal dPorcentaje = decimal.Parse(sPorcentaje);
                if (dPorcentaje == 1)
                {
                    sRes = "PT8H0M0S";
                }
                else
                {
                    if (dPorcentaje > 0)
                    {
                        decimal dSegundos = 8 * 60 * 60 * dPorcentaje;
                        int nHoras = (int)System.Math.Floor(dSegundos / (60 * 60));

                        decimal dAux = dSegundos - (nHoras * 60 * 60);
                        int nMinutos = (int)System.Math.Floor(dAux / 60);

                        dAux = dAux - (nMinutos * 60);
                        int nSegundos = (int)System.Math.Floor(dAux);
                        sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M" + nSegundos.ToString() + "S";

                    }
                }
            }
            return sRes;
        }

        //Dado un esfuerzo en horas y un nº de días devuelve el tiempo dedicado cada día en formato PTxHxMxS
        private static string flHorasDia(decimal dHorasTot, int nDias)
        {
            string sRes = "PT0H0M0S";
            if (nDias==0) return sRes;

            int nHoras = 0, nMinutos = 0, nSegundos = 0;
            decimal dHoras=0, dMinutos = 0, dSegundos = 0, dAux=0;

            //Calculo las horas que tocan para cada día
            dHoras = dHorasTot / nDias;
            nHoras = (int)System.Math.Floor(dHoras);
            //Calculo las horas que quedan sin repartir
            dAux = dHorasTot - (nHoras * nDias);
            //Las paso a minutos
            dAux = dAux * 60;
            //Calculo los minutos que tocan para cada día
            dMinutos = dAux / nDias;
            nMinutos = (int)System.Math.Floor(dMinutos);
            //Calculo los minutos que quedan sin repartir
            dAux = dAux - (nMinutos * nDias);
            //Los paso a segundos
            dAux = dAux * 60;
            //Calculo los segundos que tocan para cada día
            dSegundos = dAux / nDias;
            nSegundos = (int)System.Math.Floor(dSegundos);
            //Calculo los segundos que quedan sin repartir
            //dAux = dSegundos - (nSegundos);

            sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M" + nSegundos.ToString() + "S";
            return sRes;
        }
        //Dado unos segundos devuelve en formato PTxHxMxS
        private static string flGetHorasDia(int nSegTot)
        {
            string sRes = "PT0H0M0S";
            if (nSegTot == 0) return sRes;

            //Transformo a horas minutos y segundos 
            int nHR = (int)System.Math.Floor((decimal)nSegTot / 3600);
            nSegTot = nSegTot - (nHR * 3600);
            int nMR = (int)System.Math.Floor((decimal)nSegTot / 60);
            int nSR = nSegTot - (nMR * 60);

            sRes = "PT" + nHR.ToString() + "H" + nMR.ToString() + "M" + nSR.ToString() + "S";
            return sRes;
        }

        //Dado un intervalo de fechas devuelve el nº de días laborables (Lunes a Viernes)
        private static int flNumLaborables(DateTime dtIni, DateTime dtFin)
        {
            int iRes = 0;
            int nDifDias = Fechas.DateDiff("day", dtIni, dtFin);
            DateTime dtAux = dtIni;

            for (int i = 0; i <= nDifDias; i++)
            {
                if (flEsLaborable(dtAux))
                        iRes++;
                dtAux = dtAux.AddDays(1);
            }

            return iRes;
        }
        private static bool flEsLaborable(DateTime dtDia)
        {
            bool bRes = false;
            DayOfWeek objDiaSemana = dtDia.DayOfWeek;
            switch (objDiaSemana)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    {
                        bRes=true;
                        break;
                    }
            }
            return bRes;
        }

        //Dadas unas horas en formato Openproj y un nº de horas, las suma y devuelve en formato Openproj
        private static string flSumar(string sHorasDia, decimal dRestoHoras)
        {
            string sRes = sHorasDia, sAuxI = "", sAuxD = "";
            int nPos = 0, nHI = 0, nMI = 0, nSI = 0, nHR = 0, nMR = 0, nSR = 0;
            decimal dAux = 0;

            //Paso la cadena a horas minutos y segundos
            //Quito el PT inicial de la cadena
            sAuxI = sHorasDia.Substring(2);

            nPos = sAuxI.IndexOf("H");
            sAuxD = sAuxI.Substring(nPos + 1);
            sAuxI = sAuxI.Substring(0, nPos);
            nHI = int.Parse(sAuxI);

            sAuxI = sAuxD;
            nPos = sAuxI.IndexOf("M");
            sAuxD = sAuxI.Substring(nPos + 1);
            sAuxI = sAuxI.Substring(0, nPos);
            nMI = int.Parse(sAuxI);

            sAuxI = sAuxD;
            nPos = sAuxI.IndexOf("S");
            sAuxI = sAuxI.Substring(0, nPos);
            nSI = int.Parse(sAuxI);



            if (dRestoHoras > 0)
            {
                //Paso las horas a sumar a horas minutos y segundos
                nHR = (int)System.Math.Floor(dRestoHoras);
                //Paso el resto a minutos
                dAux = (dRestoHoras - (decimal)nHR) * 60;
                nMR = (int)System.Math.Floor(dAux);
                //Paso el resto a segundos
                dAux = (dAux - (decimal)nMR) * 60;
                nSR = (int)System.Math.Floor(dAux);
                dAux = dAux - (decimal)nSR;

                //Sumo ambos elementos
                nHR += nHI;
                nMR += nMI;
                nSR += nSI;
                //Si los segundos exceden de 60 los sumo a los minutos
                if (nSR > 60)
                {
                    nMI = (int)System.Math.Floor((decimal)nSR / 60);
                    nSR -= nMI * 60;
                    nMR += nMI;
                }
                //Si los minutos exceden de 60 los sumo a las horas
                if (nMR > 60)
                {
                    nHI = (int)System.Math.Floor((decimal)nMR / 60);
                    nMR -= nHI * 60;
                    nHR += nHI;
                }
            }
            else//Para cuando quiero restar
            {
                //Obtengo el total de segundos inicales
                int nSegIni = (nHI * 3600) + (nMI * 60) + nSI;
                //Obtengo el total de segundos a restar
                //int nSegRest = (int)System.Math.Floor(dRestoHoras * 3600 * -1);
                int nSegRest = (int)System.Math.Round((dRestoHoras * 3600 * -1), 0);

                //Transformo a horas minutos y segundos la diferencia entre ambos
                nSegIni -= nSegRest;
                nHR = (int)System.Math.Floor((decimal)nSegIni / 3600);
                nSegIni = nSegIni - (nHR * 3600);
                nMR = (int)System.Math.Floor((decimal)nSegIni / 60);
                nSR = nSegIni - (nMR * 60);
            }
            sRes = "PT" + nHR.ToString() + "H" + nMR.ToString() + "M" + nSR.ToString() + "S";

            return sRes;
        }
        //Dadas unas horas en formato Openproj y un nº de segundos, los suma y devuelve en formato Openproj
        private static string flSumarSegundos(string sHorasDia, decimal dRestoSegundos)
        {
            string sRes = sHorasDia, sAuxI = "", sAuxD = "";
            int nPos = 0, nHI = 0, nMI = 0, nSI = 0, nHR = 0, nMR = 0, nSR = 0;

            //Paso la cadena a horas minutos y segundos
            //Quito el PT inicial de la cadena
            sAuxI = sHorasDia.Substring(2);

            nPos = sAuxI.IndexOf("H");
            sAuxD = sAuxI.Substring(nPos + 1);
            sAuxI = sAuxI.Substring(0, nPos);
            nHI = int.Parse(sAuxI);

            sAuxI = sAuxD;
            nPos = sAuxI.IndexOf("M");
            sAuxD = sAuxI.Substring(nPos + 1);
            sAuxI = sAuxI.Substring(0, nPos);
            nMI = int.Parse(sAuxI);

            sAuxI = sAuxD;
            nPos = sAuxI.IndexOf("S");
            sAuxI = sAuxI.Substring(0, nPos);
            nSI = int.Parse(sAuxI);


            //Obtengo el total de segundos inicales
            decimal dSegIni = (nHI * 3600) + (nMI * 60) + nSI;
            dSegIni += dRestoSegundos;
            //Transformo a horas minutos y segundos 
            nHR = (int)System.Math.Floor(dSegIni / 3600);
            dSegIni = dSegIni - (nHR * 3600);
            nMR = (int)System.Math.Floor(dSegIni / 60);
            nSR = (int)Math.Round(dSegIni) - (nMR * 60);

            sRes = "PT" + nHR.ToString() + "H" + nMR.ToString() + "M" + nSR.ToString() + "S";

            return sRes;
        }
        #endregion
    }
}
