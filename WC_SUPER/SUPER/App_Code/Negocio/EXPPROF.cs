using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EXPPROF
    /// </summary>
    public partial class EXPPROF
    {
        #region Propiedades y Atributos

        private int _t808_idexpprof;
        public int t808_idexpprof
        {
            get { return _t808_idexpprof; }
            set { _t808_idexpprof = value; }
        }

        private string _t808_denominacion;
        public string t808_denominacion
        {
            get { return _t808_denominacion; }
            set { _t808_denominacion = value; }
        }

        private string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _Dentro;
        public string Dentro
        {
            get { return _Dentro; }
            set { _Dentro = value; }
        }

        private string _t808_descripcion;
        public string t808_descripcion
        {
            get { return _t808_descripcion; }
            set { _t808_descripcion = value; }
        }

        private bool _t808_enibermatica;
        public bool t808_enibermatica
        {
            get { return _t808_enibermatica; }
            set { _t808_enibermatica = value; }
        }

        private bool _tieneproyecto;
        public bool bTieneProyecto
        {
            get { return _tieneproyecto; }
            set { _tieneproyecto = value; }
        }

        private int? _t811_idcuenta_ori;
        public int? t811_idcuenta_ori
        {
            get { return _t811_idcuenta_ori; }
            set { _t811_idcuenta_ori = value; }
        }

        private int? _idSegmento_ori;
        public int? idSegmento_ori
        {
            get { return _idSegmento_ori; }
            set { _idSegmento_ori = value; }
        }

        private string _ACS;
        public string ACS
        {
            get { return _ACS; }
            set { _ACS = value; }
        }

        private string _ACT;
        public string ACT
        {
            get { return _ACT; }
            set { _ACT = value; }
        }

        private string _idACS;
        public string idACS
        {
            get { return _idACS; }
            set { _idACS = value; }
        }

        private string _idACT;
        public string idACT
        {
            get { return _idACT; }
            set { _idACT = value; }
        }

        private int? _idCliente;
        public int? idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private string _Cliente;
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private string _Segmento_ori;
        public string Segmento_ori
        {
            get { return _Segmento_ori; }
            set { _Segmento_ori = value; }
        }

        private string _Segmento;
        public string Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }


        private int? _idSector_ori;
        public int? idSector_ori
        {
            get { return _idSector_ori; }
            set { _idSector_ori = value; }
        }

        private string _Sector;
        public string Sector
        {
            get { return _Sector; }
            set { _Sector = value; }
        }

        private string _Sector_ori;
        public string Sector_ori
        {
            get { return _Sector_ori; }
            set { _Sector_ori = value; }
        }

        private int? _idEmpresaC;
        public int? idEmpresaC
        {
            get { return _idEmpresaC; }
            set { _idEmpresaC = value; }
        }

        private string _EmpresaC;
        public string EmpresaC
        {
            get { return _EmpresaC; }
            set { _EmpresaC = value; }
        }

        private int? _idClienteP;
        public int? idClienteP
        {
            get { return _idClienteP; }
            set { _idClienteP = value; }
        }

        private int _idSegmentoCliente;
        public int idSegmentoCliente
        {
            get { return _idSegmentoCliente; }
            set { _idSegmentoCliente = value; }
        }

        private int _idSectorCliente;
        public int idSectorCliente
        {
            get { return _idSectorCliente; }
            set { _idSectorCliente = value; }
        }

        private int _idSegmentoEmpresaC;
        public int idSegmentoEmpresaC
        {
            get { return _idSegmentoEmpresaC; }
            set { _idSegmentoEmpresaC = value; }
        }

        private int _idSectorEmpresaC;
        public int idSectorEmpresaC
        {
            get { return _idSectorEmpresaC; }
            set { _idSectorEmpresaC = value; }
        }

        private int _idSegmentoP;
        public int idSegmentoP
        {
            get { return _idSegmentoP; }
            set { _idSegmentoP = value; }
        }

        private int _idSectorP;
        public int idSectorP
        {
            get { return _idSectorP; }
            set { _idSectorP = value; }
        }

        private string _ClienteP;
        public string ClienteP
        {
            get { return _ClienteP; }
            set { _ClienteP = value; }
        }

        private int? _idSegmento_des;
        public int? idSegmento_des
        {
            get { return _idSegmento_des; }
            set { _idSegmento_des = value; }
        }

        private string _Segmento_des;
        public string Segmento_des
        {
            get { return _Segmento_des; }
            set { _Segmento_des = value; }
        }

        private int? _idSector_des;
        public int? idSector_des
        {
            get { return _idSector_des; }
            set { _idSector_des = value; }
        }

        private string _Sector_des;
        public string Sector_des
        {
            get { return _Sector_des; }
            set { _Sector_des = value; }
        }

        private int? _t811_idcuenta_para;
        public int? t811_idcuenta_para
        {
            get { return _t811_idcuenta_para; }
            set { _t811_idcuenta_para = value; }
        }

        private int? _t302_idcliente;
        public int? t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _denProyecto;
        public string denProyecto
        {
            get { return _denProyecto; }
            set { _denProyecto = value; }
        }
        private string _ctaOrigen;
        public string ctaOrigen
        {
            get { return _ctaOrigen; }
            set { _ctaOrigen = value; }
        }
        private string _ctaDestino;
        public string ctaDestino
        {
            get { return _ctaDestino; }
            set { _ctaDestino = value; }
        }
        private string _t302_denominacion;
        public string t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }
        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }
        private int _t302_idcliente_proyecto;
        public int t302_idcliente_proyecto
        {
            get { return _t302_idcliente_proyecto; }
            set { _t302_idcliente_proyecto = value; }
        }

        //Validador
        private int? _idValidador;
        public int? idValidador
        {
            get { return _idValidador; }
            set { _idValidador = value; }
        }
        private string _denValidador;
        public string denValidador
        {
            get { return _denValidador; }
            set { _denValidador = value; }
        }


        #endregion

        #region Metodos

        public static EXPPROF DatosExpProf(SqlTransaction tr, int t808_idexpprof)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosExpProf(tr, t808_idexpprof);
            if (dr.Read())
            {
                if (dr["t808_denominacion"] != DBNull.Value)
                    o.t808_denominacion = dr["t808_denominacion"].ToString();
                if (dr["t808_descripcion"] != DBNull.Value)
                    o.t808_descripcion = dr["t808_descripcion"].ToString();
                if (dr["t302_idcliente"] != DBNull.Value)
                    o.t302_idcliente = (int)dr["t302_idcliente"];
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = dr["t302_denominacion"].ToString();

                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["t808_idexpprof"];
                if (dr["t808_enibermatica"] != DBNull.Value)
                    o.t808_enibermatica = (bool)dr["t808_enibermatica"];
                if (dr["bTienProy"] != DBNull.Value)
                {
                    //o.bTieneProyecto = (bool)dr["bTienProy"];
                    if (dr["bTienProy"].ToString() == "1")
                        o.bTieneProyecto = true;
                    else
                        o.bTieneProyecto = false;
                }
                if (dr["idSegmentoOri"] != DBNull.Value)
                    o.idSegmento_ori = (int)dr["idSegmentoOri"];
                //Validador. Se toma el de de la experiencia del profesional, si no hay se toma su evaluador progress
                //if (dr["idValidador"] != DBNull.Value)
                //{
                //    o.idValidador = (int)dr["idValidador"];
                //    o.denValidador = dr["denValidador"].ToString();
                //}
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;
        }
        /// <summary>
        /// Obtiene además los datos del validador de la experiencia
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t808_idexpprof"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static EXPPROF DatosExpProf(SqlTransaction tr, int t808_idexpprof, int t001_idficepi)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosExpProf(tr, t808_idexpprof, t001_idficepi);
            if (dr.Read())
            {
                if (dr["t808_denominacion"] != DBNull.Value)
                    o.t808_denominacion = dr["t808_denominacion"].ToString();
                if (dr["t808_descripcion"] != DBNull.Value)
                    o.t808_descripcion = dr["t808_descripcion"].ToString();
                if (dr["t302_idcliente"] != DBNull.Value)
                    o.t302_idcliente = (int)dr["t302_idcliente"];
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = dr["t302_denominacion"].ToString();

                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["t808_idexpprof"];
                if (dr["t808_enibermatica"] != DBNull.Value)
                    o.t808_enibermatica = (bool)dr["t808_enibermatica"];
                if (dr["bTienProy"] != DBNull.Value)
                {
                    //o.bTieneProyecto = (bool)dr["bTienProy"];
                    if (dr["bTienProy"].ToString() == "1")
                        o.bTieneProyecto = true;
                    else
                        o.bTieneProyecto = false;
                }
                if (dr["idSegmentoOri"] != DBNull.Value)
                    o.idSegmento_ori = (int)dr["idSegmentoOri"];
                //Validador. Se toma el de de la experiencia del profesional, si no hay se toma su evaluador progress
                if (dr["idValidador"] != DBNull.Value)
                {
                    o.idValidador = (int)dr["idValidador"];
                    o.denValidador = dr["denValidador"].ToString();
                }
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;
        }

        /// <summary>
        /// Datos de una experiencia profesional fuera de Ibermática
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t808_idexpprof"></param>
        /// <returns></returns>
        public static EXPPROF DatosExpProfFuera(SqlTransaction tr, int t808_idexpprof)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosExpProfFuera(tr, t808_idexpprof);
            if (dr.Read())
            {
                if (dr["t808_denominacion"] != DBNull.Value)
                    o.t808_denominacion = dr["t808_denominacion"].ToString();
                if (dr["t808_descripcion"] != DBNull.Value)
                    o.t808_descripcion = dr["t808_descripcion"].ToString();
                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["t808_idexpprof"];
                if (dr["t808_enibermatica"] != DBNull.Value)
                    o.t808_enibermatica = (bool)dr["t808_enibermatica"];

                if (dr["t811_idcuenta_ori"] != DBNull.Value)
                    o.t811_idcuenta_ori = (int)dr["t811_idcuenta_ori"];
                if (dr["ctaOrigen"] != DBNull.Value)
                    o.ctaOrigen = dr["ctaOrigen"].ToString();
                if (dr["idSegmentoOrigen"] != DBNull.Value)
                    o.idSegmento_ori = (int)dr["idSegmentoOrigen"];
                if (dr["idSectorOrigen"] != DBNull.Value)
                    o.idSector_ori = (int)dr["idSectorOrigen"];

                if (dr["t811_idcuenta_para"] != DBNull.Value)
                    o.t811_idcuenta_para = (int)dr["t811_idcuenta_para"];
                if (dr["ctaDestino"] != DBNull.Value)
                    o.ctaDestino = dr["ctaDestino"].ToString();
                if (dr["idSegmentoDestino"] != DBNull.Value)
                    o.idSegmento_des = (int)dr["idSegmentoDestino"];
                if (dr["idSectorDestino"] != DBNull.Value)
                    o.idSector_des = (int)dr["idSectorDestino"];

            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static EXPPROF DatosExpProfFuera(SqlTransaction tr, int t808_idexpprof, int t001_idficepi)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosExpProfFuera(tr, t808_idexpprof, t001_idficepi);
            if (dr.Read())
            {
                if (dr["t808_denominacion"] != DBNull.Value)
                    o.t808_denominacion = dr["t808_denominacion"].ToString();
                if (dr["t808_descripcion"] != DBNull.Value)
                    o.t808_descripcion = dr["t808_descripcion"].ToString();
                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["t808_idexpprof"];
                if (dr["t808_enibermatica"] != DBNull.Value)
                    o.t808_enibermatica = (bool)dr["t808_enibermatica"];

                if (dr["t811_idcuenta_ori"] != DBNull.Value)
                    o.t811_idcuenta_ori = (int)dr["t811_idcuenta_ori"];
                if (dr["ctaOrigen"] != DBNull.Value)
                    o.ctaOrigen = dr["ctaOrigen"].ToString();
                if (dr["idSegmentoOrigen"] != DBNull.Value)
                    o.idSegmento_ori = (int)dr["idSegmentoOrigen"];
                if (dr["idSectorOrigen"] != DBNull.Value)
                    o.idSector_ori = (int)dr["idSectorOrigen"];

                if (dr["t811_idcuenta_para"] != DBNull.Value)
                    o.t811_idcuenta_para = (int)dr["t811_idcuenta_para"];
                if (dr["ctaDestino"] != DBNull.Value)
                    o.ctaDestino = dr["ctaDestino"].ToString();
                if (dr["idSegmentoDestino"] != DBNull.Value)
                    o.idSegmento_des = (int)dr["idSegmentoDestino"];
                if (dr["idSectorDestino"] != DBNull.Value)
                    o.idSector_des = (int)dr["idSectorDestino"];
                //Validador. Se toma el de de la experiencia del profesional, si no hay se toma su evaluador progress
                if (dr["idValidador"] != DBNull.Value)
                {
                    o.idValidador = (int)dr["idValidador"];
                    o.denValidador = dr["denValidador"].ToString();
                }
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static EXPPROF Datos(SqlTransaction tr, int t301_idproyecto)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.Datos(tr, t301_idproyecto);
            if (dr.Read())
            {

                if (dr["T808_DESCRIPCION"] != DBNull.Value)
                    o.t808_descripcion = dr["T808_DESCRIPCION"].ToString();
                if (dr["denProyecto"] != DBNull.Value)
                    o.denProyecto = dr["denProyecto"].ToString();
                if (dr["T302_IDCLIENTE_PROYECTO"] != DBNull.Value)
                    o.t302_idcliente_proyecto = (int)dr["T302_IDCLIENTE_PROYECTO"];
       
                if (dr["T302_DENOMINACION"] != DBNull.Value)
                    o.t302_denominacion = dr["T302_DENOMINACION"].ToString();

                if (dr["T808_IDEXPPROF"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["T808_IDEXPPROF"];

            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static EXPPROF DatosProy(SqlTransaction tr, int t301_idproyecto)
        {
            EXPPROF o = new EXPPROF();
            o.t808_idexpprof = -1;
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosProy(tr, t301_idproyecto);
            if (dr.Read())
            {
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.denProyecto = dr["t301_denominacion"].ToString();
                if (dr["t302_idcliente_proyecto"] != DBNull.Value)
                    o.t302_idcliente_proyecto = (int)dr["t302_idcliente_proyecto"];
                else
                    o.t302_idcliente_proyecto = -1;
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        /// <summary>
        /// Graba experiencia profesional asociada a proyecto
        /// </summary>
        /// <param name="idProyNew"></param>
        /// <param name="idEP"></param>
        /// <param name="idProy"></param>
        /// <param name="strDatosGen"></param>
        /// <param name="strDatosACT"></param>
        /// <param name="strDatosACS"></param>
        /// <param name="strProf"></param>
        /// <returns></returns>
        public static string Grabar(int idProyNew, int idEP, int idProy, string strDatosGen, string strDatosACT, string strDatosACS, string strProf)
        {
            string sIdInsertados = "";
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                #region Datos generales
                int? ctaOri = null, ctaDes = null, idCli = null, idEmp = null;
                bool bEnIb = false;
                string[] aGen = Regex.Split(strDatosGen, "##");
                if (aGen[2] == "S") bEnIb = true;
                if (aGen[3] != "-1") ctaOri = int.Parse(aGen[3]);
                if (aGen[4] != "-1") ctaDes = int.Parse(aGen[4]);
                if (aGen[5] != "-1") idCli = int.Parse(aGen[5]);
                if (aGen[6] != "-1") idEmp = int.Parse(aGen[6]);

                if (idEP == -1)//Vamos a grabar una experiencia profesional nueva
                {
                    idEP = SUPER.DAL.EXPPROF.Insert(tr, aGen[0], aGen[1], bEnIb, ctaOri, ctaDes, idCli, idEmp);
                    if (idProy != -1)
                        SUPER.DAL.EXPPROFPROYECTO.Insert(tr, idEP, idProy);
                }
                else
                {
                    if (idProyNew != -1)//Estamos asociando una experiencia profesional existente a otro proyecto
                        SUPER.DAL.EXPPROFPROYECTO.Insert(tr, idEP, idProy);

                    SUPER.DAL.EXPPROF.Update(tr, idEP, aGen[0], aGen[1], bEnIb, ctaOri, ctaDes, idCli, idEmp);
                }
                #endregion
                #region Areas de conocimiento tecnológico
                string[] aClase = Regex.Split(strDatosACT, "///");
                foreach (string oClase in aClase)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    switch (aValores[0])
                    {
                        //case "U":
                        //EXPPROFACT.Update(tr, idEP, int.Parse(aValores[1]));
                            //break;
                        case "I":
                            SUPER.DAL.EXPPROFACT.Insert(tr, idEP, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACT.Delete(tr, idEP, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                #region Areas de conocimiento sectorial
                string[] aACS = Regex.Split(strDatosACS, "///");
                foreach (string oACS in aACS)
                {
                    if (oACS == "") continue;
                    string[] aValores = Regex.Split(oACS, "##");
                    switch (aValores[0])
                    {
                        //case "U":
                        //EXPPROFACT.Update(tr, idEP, int.Parse(aValores[1]));
                        //break;
                        case "I":
                            SUPER.DAL.EXPPROFACS.Insert(tr, idEP, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACS.Delete(tr, idEP, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                #region profesionales
                DateTime dtIni;
                DateTime? dtFin=null;
                int idExpprofficepi;
                int? idPlantilla = null;
                int? idValidador = null;
                string sProfesional = "", sConsumoIAP="";
                bool bHayCVsinValor = false;
                decimal dConsumoIAP = 0;
                // AUNQUE NO HAYA CAMBIO EN EL APARTADO PROFESIONALES SI DAMOS A GRABAR PORQUE LA VISUALIZACIÓN DE LOS
                // DATOS QUE OBTENEMOS DEL PROCEDIMIENTO SUP_CVT_EXPROFFICEPI_C2 PUEDE SER DIFERENTE A LO QUE HAY EN BASE DE DATOS (T812_FFIN)
                string[] aProf = Regex.Split(strProf, "///");
                foreach (string oProf in aProf)
                {

                    if (oProf == "") continue;
                    string[] aVal = Regex.Split(oProf, "##");
                    idExpprofficepi = int.Parse(aVal[1]);
                    dtIni = Convert.ToDateTime(aVal[3]);
                    if (aVal[4] != "")
                        dtFin = Convert.ToDateTime(aVal[4]);
                    else
                        dtFin = null;

                    if (aVal[5] != "S" && aVal[5] != "N")
                    {
                        //sConsumoIAP = aVal[9].Replace(".", ",");
                        if (aVal[9] != "")
                        {
                            //dConsumoIAP = Math.Truncate(decimal.Parse(sConsumoIAP));
                            dConsumoIAP = decimal.Parse(aVal[9]);
                            if (dConsumoIAP >= 10)
                                bHayCVsinValor = true;
                        }
                    }
                    if (aVal[6] != "")
                        idPlantilla = int.Parse(aVal[6]);
                    else
                        idPlantilla = null;
                    if (aVal[7] != "")
                        idValidador = int.Parse(aVal[7]);
                    else
                        idValidador = null;
                    sProfesional = aVal[8];
                    switch (aVal[0])
                    {
                        case "I":
                            sIdInsertados += (SUPER.BLL.EXPPROFFICEPI.Insert(tr, aVal[5], dtIni, dtFin, int.Parse(aVal[2]), idEP, idValidador, idPlantilla)).ToString() + "#dato#";
                            break;
                        case "U":
                            //Hay que controlar que la fecha de inicio de experiencia no sea posterior a la
                            //fecha de inicio mínima de los perfiles del profesional en la experiencia.
                            DateTime? dFechaPerfilMin = DAL.EXPPROFFICEPI.getFechaMinPerfiles(tr, idExpprofficepi);
                            if (dFechaPerfilMin < dtIni)
                            {
                                throw new Exception("La fecha de inicio de asociación a la experiencia profesional de '" + sProfesional + "' debe ser igual o anterior a la fecha mínima de inicio en los perfiles (" + ((DateTime)dFechaPerfilMin).ToShortDateString() + ").");
                            }

                            //Hay que controlar que la fecha de fin de experiencia no sea anterior a la
                            //fecha de fin máxima de los perfiles del profesional en la experiencia.
                            if (dtFin != null)
                            {
                                DateTime? dFechaPerfilMax = DAL.EXPPROFFICEPI.getFechaMaxPerfiles(tr, idExpprofficepi);
                                //if (dFechaPerfilMax.ToString() != "")
                                //{
                                //    if (dFechaPerfilMax.ToString().Substring(0, 10) == "31/12/2070")
                                //        dFechaPerfilMax = null;
                                //}
                                if (dFechaPerfilMax != null && dFechaPerfilMax > dtFin)
                                {
                                    throw new Exception("La fecha de fin de asociación a la experiencia profesional de '" + sProfesional + "' debe ser igual o posterior a la fecha máxima de fin en los perfiles (" + ((DateTime)dFechaPerfilMax).ToShortDateString() + ").");
                                }
                            }
                            SUPER.BLL.EXPPROFFICEPI.Update(tr, idExpprofficepi, aVal[5], dtIni, dtFin, int.Parse(aVal[2]), idEP, idValidador, idPlantilla);
                            break;
                    }
                }
                #endregion

                //Si todos los profesionales con 10 jornadas o mas tienen indicado si su CV es visible o no, quito el proyecto de la lista de pendientes de cualificar
                //y pongo a NULL T301.t301_fpuestacualif_cvt
                if (!bHayCVsinValor)
                    SUPER.DAL.EXPPROF.BorrarPdteCualificar(tr, idEP);

                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return idEP.ToString() + "#item#" + sIdInsertados;
        }

        /// <summary>
        /// Graba una experiencia profesional en Ibermatica no asociada a proyecto
        /// </summary>
        /// <param name="idExpProf"></param>
        /// <param name="strDatosGen">T808_EXPPROF</param>
        /// <param name="strDatosACT">T817_EXPPROFACT</param>
        /// <param name="strDatosACS">T816_EXPPROFACS</param>
        /// <param name="strDatosPERF">T813_EXPFICEPIPERFIL</param>
        /// <returns></returns>
        public static string GrabarEnIb(int idExpProf, string strDatosGen, string strDatosACT, string strDatosACS, string strDatosPERF, int idProfesional) //, string sEsMiCV)
        {
            string sResul = "";
            int t812_expprofficepi=0;
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                #region Experiencia profesional
                //int? ctaOri = null, ctaDes = null, idCli = null, idEmp = null;
                int? idCli = null;
                string[] aGen = Regex.Split(strDatosGen, "##");
                if (aGen[2] != "-1") idCli = int.Parse(aGen[2]);

                if (idExpProf == -1)
                {   
                    //Vamos a grabar una experiencia profesional nueva
                    //Y su Experiencia profesional ficepi
                    idExpProf = SUPER.DAL.EXPPROF.Insert(tr, aGen[0], aGen[1], true, null, null, idCli, null);
                    t812_expprofficepi = SUPER.DAL.EXPPROFFICEPI.Insert(tr, "S", null, null, idProfesional, idExpProf, int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()));

                }
                else
                    SUPER.DAL.EXPPROF.Update(tr, idExpProf, aGen[0], aGen[1], true, null, null, idCli, null);
                #endregion
                #region Areas de conocimiento tecnológico
                string[] aClase = Regex.Split(strDatosACT, "///");
                foreach (string oClase in aClase)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    switch (aValores[0])
                    {
                        //case "U":
                        //EXPPROFACT.Update(tr, idEP, int.Parse(aValores[1]));
                        //break;
                        case "I":
                            SUPER.DAL.EXPPROFACT.Insert(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACT.Delete(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                #region Areas de conocimiento sectorial
                string[] aACS = Regex.Split(strDatosACS, "///");
                foreach (string oACS in aACS)
                {
                    if (oACS == "") continue;
                    string[] aValores = Regex.Split(oACS, "##");
                    switch (aValores[0])
                    {
                        //case "U":
                        //EXPPROFACT.Update(tr, idEP, int.Parse(aValores[1]));
                        //break;
                        case "I":
                            SUPER.DAL.EXPPROFACS.Insert(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACS.Delete(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                #region Perfiles
                string[] aPERF = Regex.Split(strDatosPERF, "///");
                foreach (string oClase in aPERF)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    switch (aValores[0])
                    {
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPFICEPIPERFIL.Delete(tr, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion

                //if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

                sResul = "OK@#@" + idExpProf.ToString() + "@#@" + t812_expprofficepi;

                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        /// <summary>
        /// Graba una experiencia profesional fuera de Ibermatica 
        /// </summary>
        /// <param name="idExpProf"></param>
        /// <param name="idExpProfFicepi"></param>
        /// <param name="idExpFicepiPerfil"></param>
        /// <param name="idFicepi">Profesional al que se le están modificando datos</param>
        /// <param name="idFicepiAct">Profesional que está actualizando los datos</param>
        /// <param name="strDatosGen">T808_EXPPROF</param>
        /// <param name="strDatosACT">T817_EXPPROFACT</param>
        /// <param name="strDatosACS">T816_EXPPROFACS</param>
        /// <param name="strEPF">T812_EXPPROFFICEPI</param>
        /// <param name="strEFP">T813_EXPFICEPIPERFIL</param>
        /// <param name="strET">T815_EXPFICEPIENTORNO</param>
        /// <returns></returns>
        public static string GrabarFueraIb(int idExpProf, string strDatosGen, string strDatosACT, string strDatosACS, string strDatosPERF, int t001_idficepi)
        {
            string sResul = "";
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                #region Experiencia profesional
                int? idCtaOri = null;
                int? idCtaDes = null;
                int t812_expprofficepi = 0;
                string[] aGen = Regex.Split(strDatosGen, "##");
                
                //Empresa Contratante (cuenta origen)
                //Si es negativo sabemos que es CuentaCVT
                //Si es positivo sabemos que es cliente y por lo tanto hay que grabarlo en CuentasCVT
                int auxE = (aGen[2] == "null") ? 0 : int.Parse(aGen[2]);
                //if (aGen[2] != "null" && auxE < 0)
                if (auxE < 0)
                {
                    idCtaOri = int.Parse(aGen[2]);
                    //Es posible que un Encargado de CV grabe una cuentaCVT sin sector/segmento por lo que si llegamos aqui,
                    //por si acaso comprobamos si la cuenta tiene sector y sino se lo ponemos
                    int idCtaOriAux = (int)idCtaOri * -1;
                    int iSegmento = SUPER.BLL.CuentasCVT.GetSegmento(tr, idCtaOriAux);
                    if (iSegmento==-1)
                    {
                        if (aGen[4] != "")
                            SUPER.DAL.CuentasCVT.UpdateSegmento(tr, idCtaOriAux, int.Parse(aGen[4]));
                    }
                }
                else //if (aGen[2] == "null" || auxE > 0)
                {//La empresa no existe, la doy de alta
                    if (auxE > 0)
                    {//Existe como cliente pero no como cuenta asi que la doy de alta
                        idCtaOri = SUPER.BLL.CuentasCVT.Insert(tr, aGen[6], (aGen[4] == "") ? null : (int?)int.Parse(aGen[4]), 1, auxE);
                    }
                    else
                    {//No tiene codigo de cuenta ni de cliente. Antes de insertar compruebo si ya existe como cuentaCVT
                        int idCtaOriAux = SUPER.BLL.CuentasCVT.ObtenerPorNombre(tr, aGen[6]);
                        if (idCtaOriAux == 0)
                        {//Sino existe cuenta, hay que insertarla
                            idCtaOri = SUPER.BLL.CuentasCVT.Insert(tr, aGen[6], (aGen[4] == "") ? null : (int?)int.Parse(aGen[4]), 0, null);
                        }
                        else//Le asigno la cuenta encontrada
                            idCtaOri = idCtaOriAux;
                    }
                }
                //Cliente (cuenta destino)
                //Si es negativo sabemos que es CuentaCVT
                //Si es positivo sabemos que es cliente y por lo tanto hay que grabarlo en CuentasCVT
                int auxC = (aGen[3] == "null") ? 0 : int.Parse(aGen[3]);
                if (auxC < 0)
                {
                    idCtaDes = int.Parse(aGen[3]);
                    //Es posible que un Encargado de CV grabe una cuentaCVT sin sector/segmento por lo que si llegamos aqui,
                    //por si acaso comprobamos si la cuenta tiene sector y sino se lo ponemos
                    int idCtaDesAux = (int)idCtaDes * -1;
                    int iSegmento = SUPER.BLL.CuentasCVT.GetSegmento(tr, idCtaDesAux);
                    if (iSegmento == -1)
                    {
                        if (aGen[5] != "")
                            SUPER.DAL.CuentasCVT.UpdateSegmento(tr, idCtaDesAux, int.Parse(aGen[5]));
                    }
                }
                else //if (aGen[3] == "null" || auxC > 0)
                {
                    //Compruebo que no la haya dado de alta como empresa contratante (origen)
                    if (aGen[7] != "")
                    {
                        if ((aGen[6] == aGen[7]))//&& (aGen[4] == aGen[5])
                        {
                            idCtaDes = idCtaOri;
                        }
                        else//La empresa no existe, la doy de alta
                        {
                            if (auxC > 0)
                            {//Existe como cliente pero no como cuenta asi que la doy de alta
                                idCtaDes = SUPER.BLL.CuentasCVT.Insert(tr, aGen[7], (aGen[5] == "") ? null : (int?)int.Parse(aGen[5]), 1, auxC);
                            }
                            else
                            {//No tiene codigo de cuenta ni de cliente. Antes de insertar compruebo si ya existe como cuentaCVT
                                int idCtaDesAux = SUPER.BLL.CuentasCVT.ObtenerPorNombre(tr, aGen[7]);
                                if (idCtaDesAux == 0)
                                {//Sino existe cuenta, hay que insertarla
                                    idCtaDes = SUPER.BLL.CuentasCVT.Insert(tr, aGen[7], (aGen[5] == "") ? null : (int?)int.Parse(aGen[5]), 0, null);
                                }
                                else//Le asigno la cuenta encontrada
                                    idCtaDes = idCtaDesAux;
                            }
                        }
                    }
                }

                if (idExpProf == -1)
                {
                    //Vamos a grabar una experiencia profesional nueva
                    //Y su experiencia ficepi
                    idExpProf = SUPER.DAL.EXPPROF.Insert(tr, aGen[0], aGen[1], false, idCtaOri, idCtaDes, null, null);
                    t812_expprofficepi = SUPER.DAL.EXPPROFFICEPI.Insert(tr, "S", null, null, t001_idficepi, idExpProf, int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()));
                }
                else
                    SUPER.DAL.EXPPROF.Update(tr, idExpProf, aGen[0], aGen[1], false, idCtaOri, idCtaDes, null, null);
                #endregion

                #region Areas de conocimiento tecnológico
                string[] aClase = Regex.Split(strDatosACT, "///");
                foreach (string oClase in aClase)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    switch (aValores[0])
                    {
                        case "I":
                            SUPER.DAL.EXPPROFACT.Insert(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACT.Delete(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                #region Areas de conocimiento sectorial
                string[] aACS = Regex.Split(strDatosACS, "///");
                foreach (string oACS in aACS)
                {
                    if (oACS == "") continue;
                    string[] aValores = Regex.Split(oACS, "##");
                    switch (aValores[0])
                    {
                        case "I":
                            SUPER.DAL.EXPPROFACS.Insert(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPPROFACS.Delete(tr, idExpProf, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion

                #region Perfiles
                string[] aPERF = Regex.Split(strDatosPERF, "///");
                foreach (string oClase in aPERF)
                {
                    if (oClase == "") continue;
                    string[] aValores = Regex.Split(oClase, "##");
                    switch (aValores[0])
                    {
                        case "D":
                            SUPER.DAL.EXPFICEPIPERFIL.Delete(tr, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion

                sResul = "OK@#@" + idExpProf.ToString() + "@#@" + t812_expprofficepi + "@#@" + idCtaOri.ToString() + "@#@" + idCtaDes.ToString();

                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        public static string MiCVExpProfEnIbermatica(int idFicepi)//, bool bGestorCV)
        {
            SqlDataReader dr = SUPER.DAL.EXPPROF.CatalogoEnIbermaticaByProfesional(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblExpProfIber' style='width:930px;' class='texto MA'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:30px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:340px;' />");
            sb.Append("<col style='width:330px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("</colgroup>");
            while (dr.Read())
            {//mdExpIber(idExpProf, idExpProfFicepi, idExpFicepiPerfil, estado, capaPerfiles)
                //Los estados borrador solo los ve el propio autor
                //if (dr["ESTADO"].ToString() != "B" || bMiCV)
                //{
                    sb.Append("<tr id='" + dr["t808_idexpprof"].ToString() + "'");
                    sb.Append(" idPF='" + dr["t812_idexpprofficepi"].ToString() + "'");
                    sb.Append(" est='" + dr["ESTADO"].ToString() + "'");
                    sb.Append(" tipo='" + dr["TIPO"].ToString() + "'");
                    sb.Append(" pr='" + dr["T301_IDPROYECTO"].ToString() + "'");
                    sb.Append(" style='height:20px;' ");
                    sb.Append(" onclick='ms(this);' ondblclick='mdExpIber(this);'>");
                    if (dr["t812_fPeticionBorrado"].ToString() != "")
                        sb.Append("<td><img src='../../../images/imgPetBorrado.png' title='Pdte de eliminar' /></td>");
                    else
                        sb.Append("<td>" + GetImagen(dr["ESTADO"].ToString()) + "</td>");
                    sb.Append("<td colspan='3'><nobr class='NBR' style='width:430px;' onmouseover='TTip(event)' >" + dr["t808_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR' onmouseover='TTip(event)' style='width:320px;'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                    string finicio = (dr["T812_FINICIO"].ToString() != "") ? DateTime.Parse(dr["T812_FINICIO"].ToString()).ToShortDateString() : "";
                    sb.Append("<td>" + finicio + "</td>");
                    string ffin = (dr["T812_FFIN"].ToString() != "") ? DateTime.Parse(dr["T812_FFIN"].ToString()).ToShortDateString() : "";
                    sb.Append("<td>" + ffin + "</td>");
                    sb.Append("</tr>");
                //}
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        private static string GetImagen(string sEstado)
        {
            string sRes = "";
            switch (sEstado)
            {
                //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                //case ("O"):
                //case ("P"):
                //    sRes = "<img src='../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />";
                //    break;
                //case ("R"):
                //    sRes = "<img src='../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />";
                //    break;
                case ("X"):
                case ("Y"):
                    sRes = "<img src='../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />";
                    break;
                case ("S"):
                case ("T"):
                    sRes = "<img src='../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />";
                    break;
                case ("B"):
                    sRes = "<img src='../../../images/imgBorrador.png' title='Datos en borrador' />";
                    break;
                case ("V"):
                    sRes = "<img src='../../../images/imgFN.gif' title='' />";
                    break;
                default:
                    sRes ="<img src='../../../images/imgFN.gif' title='' />";
                    break;
            }

            return sRes;
        }

        public static string MiCVExpProfFueraIbermatica(int idFicepi)
        {
            SqlDataReader dr = SUPER.DAL.EXPPROF.CatalogoFueraIbermaticaByProfesional(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblExpProfNoIber' style='width:930px;' class='MA'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:30px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:340px;' />");
            sb.Append("<col style='width:330px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                //Los estados borrador solo los ve el propio autor
                //if (dr["ESTADO"].ToString() != "B" || bMiCV)
                //{
                    sb.Append("<tr id='" + dr["T808_IDEXPPROF"].ToString() + "'");
                    sb.Append(" est='" + dr["ESTADO"].ToString() + "'");
                    sb.Append(" idPF='" + dr["T812_IDEXPPROFFICEPI"].ToString() + "'");
                    sb.Append(" style='height:20px;' onclick='mm(event);' ondblclick='mdExpNoIber(this);'>");
                    sb.Append("<td>" + GetImagen(dr["ESTADO"].ToString()) + "</td>");
                    sb.Append("<td colspan='3'><nobr class='NBR' style='width:430px;' onmouseover='TTip(event)' >" + dr["T808_DENOMINACION"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR' onmouseover='TTip(event)' style='width:320px;'>" + dr["ctaOrigen"].ToString() + "</nobr></td>");
                    string finicio = (dr["T812_FINICIO"].ToString() != "") ? DateTime.Parse(dr["T812_FINICIO"].ToString()).ToShortDateString() : "";
                    sb.Append("<td>" + finicio + "</td>");
                    string ffin = (dr["T812_FFIN"].ToString() != "") ? DateTime.Parse(dr["T812_FFIN"].ToString()).ToShortDateString() : "";
                    sb.Append("<td>" + ffin + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</tr>");
                //}
            }

            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        #region Borrado
        /// <summary>
        /// Comprueba si existe algun registro en T812_EXPPROFFICEPI correspondiente a la experiencia profesional y asociada
        /// a otro profesional
        /// </summary>
        public static bool Borrable(string sIdExpProf, int idFicepi)
        {
            bool bBorrable = true;
            string[] aReg = Regex.Split(sIdExpProf, "##");
            foreach (string oReg in aReg)
            {
                if (oReg == "") continue;
                if (SUPER.DAL.EXPPROFFICEPI.bHayOtroProfesional(null,int.Parse(oReg), idFicepi))
                {
                    bBorrable = false;
                    break;
                }
            }
            return bBorrable;
        }
        public static string Borrar(string sIdExpProf, int idFicepi, int IdficepiEntrada)
        {
            string sRes = "OK@#@";
            try
            {
                if (Borrable(sIdExpProf, idFicepi))
                {
                    #region Inicio Transacción
                    SqlConnection oConn;
                    SqlTransaction tr;
                    try
                    {
                        oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                        tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                    }
                    catch (Exception ex)
                    {
                        throw (new Exception("Error al abrir la conexion", ex));
                    }

                    #endregion
                    try
                    {
                        string[] aReg = Regex.Split(sIdExpProf, "##");
                        foreach (string oReg in aReg)
                        {
                            if (oReg == "") continue;
                            //primero borro los registro en la T812_EXPPROFFICEPI para permitir el borrado de la T808_EXPPROF
                            //ya que no hay delete cascada
                            SUPER.DAL.EXPPROFFICEPI.Borrar(tr, int.Parse(oReg), idFicepi);
                            SUPER.DAL.EXPPROF.Delete(tr, int.Parse(oReg));
                        }

                        if (idFicepi == IdficepiEntrada)
                            SUPER.DAL.Curriculum.ActualizadoCV(tr, idFicepi);

                        SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                    }
                    catch (Exception ex)
                    {
                        SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                        throw ex;
                    }
                    finally
                    {
                        SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                    }
                }
                else
                    sRes = "ERROR@#@Denegado!\n\nExisten otros profesionales asociados a la experiencia profesional";
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }
        #endregion

        public static string MiCVExpProfEnIbermaticaHTML(int idFicepi, int bControl, string nombrecuenta, Nullable<int> idcuenta, Nullable<int> t483_idsector, Nullable<int> t035_codperfile, string let036_idcodentorno)
        {
            SqlDataReader dr = DAL.EXPPROF.MiCVExpProfEnIbermaticaHTML(null, idFicepi, bControl, nombrecuenta, idcuenta, t483_idsector, t035_codperfile, let036_idcodentorno);
            StringBuilder sbCabecera = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbN = new StringBuilder();
            StringBuilder sbFinal = new StringBuilder();
            int codExp = 0;
            //int codExpN = 0;
            int i = 0;
            //int iN = 0;
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            if (dr.HasRows)
            {
                sbCabecera.Append("<table style='margin-left:50px; margin-top:25px; width:400px;' cellpadding='1' cellspacing='0' border='0'>");
                sbCabecera.Append("<tr><td>");
                sbCabecera.Append("<label id='lblExpIber' class='titulo2'>Experiencia en Ibermatica</label>");
                sbCabecera.Append("</td></tr>");
                sbCabecera.Append("</table>");
            }
            while (dr.Read())
            {
                 #region Exp Ligada a proyecto
                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";

                if (i == 0 || codExp != int.Parse(dr["T808_IDEXPPROF"].ToString()))
                {
                    codExp = int.Parse(dr["T808_IDEXPPROF"].ToString());
                    sb.Append("<table style='margin-left:70px; margin-top:15px; width:580px;' cellpadding='1' cellspacing='0' border='0' >");
                    sb.Append("<colgroup>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append("</colgroup>");
                    //Separador (Siempre que haya mas de una experiencia)
                    if (i != 0)
                    {
                        sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");
                        sb.Append("<tr style='height:5px;'><td colspan='2'></td></tr>");
                    }
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDenominacion' class='label' style='color:#336699;'>Experiencia:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='Denominacion' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T808_DENOMINACION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDescripcion' class='label' style='color:#336699;'>Descripción:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='Descripcion' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T808_DESCRIPCION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblACS' class='label' style='color:#336699;'>ACS:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='ACS' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["ACS"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblACT' class='label' style='color:#336699;'>ACT:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='ACT' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["ACT"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblCliente' class='label' style='color:#336699;'>Cliente:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<nobr id='Cliente' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["CLIENTE"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    //string DsecSegO = "";
                    //DsecSegO = (dr["Sector"].ToString() != "") ? "Sector" + ((dr["Segmento"].ToString() != "") ? "-" + "Segmento" : "") : "Segmento";

                    sb.Append("<label id='lblSecSegO' class='label' style='color:#336699;'>Sector-Segmento:</label>");
                    sb.Append("</td><td colspan='5'>");

                    string secSegO = "";
                    secSegO = (dr["Sector"].ToString() != "") ? dr["Sector"].ToString() + ((dr["Segmento"].ToString() != "") ? "-" + dr["Segmento"].ToString() : "") : dr["Segmento"].ToString();

                    sb.Append("<nobr id='SecSegO' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + secSegO + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='FInicio' class='label' style='" + sColor + "'>" + dr["T812_FINICIO"].ToString() + "</label>");
                    sb.Append("</td><td align='center'>");
                    sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T812_FFIN"].ToString() + "</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='lblNMes' class='label' style='color:#336699;'>Nº meses:</label>");
                    sb.Append("</td><td>");
                    string nmes = (dr["NMESES"].ToString() == "0") ? "0" : dr["NMESES"].ToString();
                    sb.Append("<label id='NMes' class='label' style='" + sColor + "'>" + nmes + "</label>");
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                }
                sb.Append("<table style='margin-left:85px; margin-top:15px; width:565px; text-align:left;' cellpadding='0' cellspacing='0' border='0' >");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:85px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:80px;'/>");
                sb.Append("</colgroup>");
                sb.Append("<tr><td colspan='6' style='border-bottom: 1px dotted #336699;'></td></tr>");
                sb.Append("<tr style='height:4px;'><td colspan='6'></td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblPerfil' class='label' style='color:#336699;'>Perfil:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Perfil' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntornoP' class='label' style='color:#336699;'>Ent.Tec\\Fun.:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='EntornoP' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["ENTORNO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFuncion' class='label' style='color:#336699;'>Fun. realizadas:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Funcion' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["T813_Funcion"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FInicio' class='label' style='"+sColor+" margin-left:0px;'>" + dr["T813_FINICIO"].ToString() + "</label>");
                sb.Append("</td><td align='center'>");
                sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T813_FFIN"].ToString() + "</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='lblNMesP' class='label' style='color:#336699;'>Nº meses:</label>");
                sb.Append("</td><td>");
                string nmesP = (dr["NMESESP"].ToString() == "0") ? "0" : dr["NMESESP"].ToString();
                sb.Append("<label id='NMesP' class='label' style='" + sColor + "'>" + nmesP + "</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                i++;

                #endregion
            }
            if (sb.ToString() != "")
            {
                sbFinal.Append(sbCabecera.ToString());
                sbFinal.Append(sb.ToString());
            }
            dr.Close();
            dr.Dispose();
            return sbFinal.ToString();
        }

        public static string MiCVExpProfFueraIbermaticaHTML(int idFicepi, int bControl, string nombrecuenta, Nullable<int> idcuenta, Nullable<int> t483_idsector, Nullable<int> t035_codperfile, string let036_idcodentorno)
        {
            SqlDataReader dr = DAL.EXPPROF.MiCVExpProfFueraIbermaticaHTML(null, idFicepi, bControl, nombrecuenta, idcuenta, t483_idsector, t035_codperfile, let036_idcodentorno);
            StringBuilder sb = new StringBuilder();
            int codExp = 0;
            int i = 0;
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            if (dr.HasRows)
            {
                sb.Append("<table style='margin-left:50px; margin-top:25px; width:400px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblExpFueraIber' class='titulo2'>Experiencia fuera de Ibermatica</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
            }
            while (dr.Read())
            {
                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";
                if (i == 0 || codExp != int.Parse(dr["T808_IDEXPPROF"].ToString()))
                {
                    codExp = int.Parse(dr["T808_IDEXPPROF"].ToString());
                    sb.Append("<table style='margin-left:70px; margin-top:15px; width:580px;' cellpadding='1' cellspacing='0' border='0' >");
                    sb.Append("<colgroup>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append("</colgroup>");
                    //Separador (Siempre que haya mas de una experiencia)
                    if (i != 0)
                    {
                        sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");
                        sb.Append("<tr style='height:5px;'><td colspan='2'></td></tr>");
                    }
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDenominacion' class='label' style='color:#336699;'>Experiencia:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='Denominacion' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T808_DENOMINACION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDescripcion' class='label' style='color:#336699;'>Descripción:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='Descripcion' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T808_DESCRIPCION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblACS' class='label' style='color:#336699;'>ACS:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='ACS' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["ACS"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblACT' class='label' style='color:#336699;'>ACT:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='ACT' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["ACT"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblEOrigen' class='label' style='color:#336699;'>Eª contratante:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='EOrigen' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T811_DENOMINACION_ORI"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    //string DsecSegO = "";
                    //DsecSegO = (dr["SectorEmpresa_ORI"].ToString() != "") ? "Sector" + ((dr["SegmentoEmpresa_ORI"].ToString() != "") ? "-" + "Segmento" : "") : "Segmento";

                    sb.Append("<label id='lblSecSegO' class='label' style='color:#336699;'>Sector-Segmento:</label>");
                    sb.Append("</td><td colspan='5'>");

                    string secSegO = "";
                    secSegO = (dr["SectorEmpresa_ORI"].ToString() != "") ? dr["SectorEmpresa_ORI"].ToString() + ((dr["SegmentoEmpresa_ORI"].ToString()!="")?"-" + dr["SegmentoEmpresa_ORI"].ToString():"") : dr["SegmentoEmpresa_ORI"].ToString();

                    sb.Append("<nobr id='SecSegO' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + secSegO + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblCliente' class='label' style='color:#336699;'>Cliente:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<nobr id='Cliente' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["CLIENTE"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    //string DsecSegC = "";
                    //DsecSegC = (dr["SectorCliente"].ToString() != "") ? "Sector" + ((dr["SegmentoCliente"].ToString() != "") ? "-" + "Segmento" : "") : "Segmento";

                    sb.Append("<label id='lblSecSegC' class='label' style='color:#336699;'>Sector-Segmento:</label>");
                    sb.Append("</td><td colspan='5'>");

                    string secSegC = "";
                    secSegC = (dr["SectorCliente"].ToString() != "") ? dr["SectorCliente"].ToString() + ((dr["SegmentoCliente"].ToString()!="")?"-" + dr["SegmentoCliente"].ToString():"") : dr["SegmentoCliente"].ToString();

                    sb.Append("<nobr id='SecSegC' class='NBR W450 label' onmouseover='TTip(event)' style='" + sColor + "'>" + secSegC + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='FInicio' class='label' style='" + sColor + "'>" + dr["T812_FINICIO"].ToString() + "</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T812_FFIN"].ToString() + "</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='lblNMes' class='label' style='color:#336699;'>Nº meses:</label>");
                    sb.Append("</td><td>");
                    string nmes = (dr["NMESES"].ToString() == "0") ? "0" : dr["NMESES"].ToString();
                    sb.Append("<label id='NMes' class='label' style='" + sColor + "'>" + nmes + "</label>");
                    sb.Append("</td></tr>");

                    sb.Append("</table>");
                }
                sb.Append("<table style='margin-left:85px; margin-top:15px; width:565px;' cellpadding='1' cellspacing='0' border='0' >");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:85px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:80px;'/>");
                sb.Append("</colgroup>");
                sb.Append("<tr><td colspan='6' style='border-bottom: 1px dotted #336699;'></td></tr>");
                sb.Append("<tr style='height:4px;'><td colspan='6'></td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblPerfil' class='label W120' style='color:#336699;'>Perfil:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Perfil' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntornoP' class='label W120' style='color:#336699;'>En.Tec\\Fun.:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='EntornoP' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["ENTORNO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFuncion' class='label' style='color:#336699;'>Fun. realizadas:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Funcion' class='NBR W450 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:0px;'>" + dr["T813_Funcion"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FInicio' class='label' style='"+sColor+" margin-left:0px;'>" + dr["T813_FINICIO"].ToString() + "</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T813_FFIN"].ToString() + "</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='lblNMesP' class='label' style='color:#336699;'>Nº meses:</label>");
                sb.Append("</td><td>");
                string nmesP = (dr["NMESESP"].ToString() == "0") ? "0" : dr["NMESESP"].ToString();
                sb.Append("<label id='NMesP' class='label' style='" + sColor + "'>" + nmesP + "</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }


        public static string CatalogoExpProf(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:900px;' >");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:280px;' />");
            sb.Append(" <col style='width:280px;' />");
            sb.Append(" <col style='width:280px;' />");
            sb.Append(" <col style='width:60px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = DAL.EXPPROF.CatalogoExpProf(null, t001_idficepi);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T808_IDEXPPROF"].ToString() + "' idf='" + dr["T001_IDFICEPI"].ToString() + "' epf='" + dr["T812_IDEXPPROFFICEPI"].ToString() + "' efp='" + dr["T813_IDEXPFICEPIPERFIL"].ToString() + "' style='height:20px;' class='MANO' onclick='md(this);'>");
                sb.Append("<td><nobr class='NBR W275' onmouseover='TTip(event);'>" + dr["PROFESIONAL"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275' onmouseover='TTip(event);'>" + dr["T808_DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275' onmouseover='TTip(event);'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
                sb.Append("<td>" + DateTime.Parse(dr["T838_FECHA"].ToString()).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }


        public static EXPPROF DatosExpProfDetPerfil(SqlTransaction tr, int t808_idexpprof)
        {
            EXPPROF o = new EXPPROF();
            SqlDataReader dr = SUPER.DAL.EXPPROF.DatosExpProfDetPerfil(tr, t808_idexpprof);
            if (dr.Read())
            {
                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = (int)dr["t808_idexpprof"];
                if (dr["t808_denominacion"] != DBNull.Value)
                    o.t808_denominacion = dr["t808_denominacion"].ToString();
                if (dr["t808_descripcion"] != DBNull.Value)
                    o.t808_descripcion = dr["t808_descripcion"].ToString();

                //Exp IBER
                if (dr["idCliente"] != DBNull.Value)
                    o.idCliente = (int)dr["idCliente"];
                if (dr["Cliente"] != DBNull.Value)
                    o.Cliente = dr["Cliente"].ToString();
                if (dr["idSegmentoCliente"] != DBNull.Value)
                    o.idSegmentoCliente = (int)dr["idSegmentoCliente"];
                if (dr["SegmentoCliente"] != DBNull.Value)
                    o.Segmento = dr["SegmentoCliente"].ToString();
                if (dr["idSectorCliente"] != DBNull.Value)
                    o.idSectorCliente = (int)dr["idSectorCliente"];
                if (dr["SectorCliente"] != DBNull.Value)
                    o.Sector = dr["SectorCliente"].ToString();

                //Exp FUERA(Empresa Contratante)
                if (dr["idEmpresaC"] != DBNull.Value)
                    o.idEmpresaC = (int)dr["idEmpresaC"];
                if (dr["EmpresaC"] != DBNull.Value)
                    o.EmpresaC = dr["EmpresaC"].ToString();
                if (dr["idSegmentoEmpresaC"] != DBNull.Value)
                    o.idSegmentoEmpresaC = (int)dr["idSegmentoEmpresaC"];
                if (dr["SegmentoEmpresaC"] != DBNull.Value)
                    o.Segmento_ori = dr["SegmentoEmpresaC"].ToString();
                if (dr["idSectorEmpresaC"] != DBNull.Value)
                    o.idSectorEmpresaC = (int)dr["idSectorEmpresaC"];
                if (dr["SectorEmpresaC"] != DBNull.Value)
                    o.Sector_ori = dr["SectorEmpresaC"].ToString();

                if (dr["idClienteP"] != DBNull.Value)
                    o.idClienteP = (int)dr["idClienteP"];
                if (dr["ClienteP"] != DBNull.Value)
                    o.ClienteP = dr["ClienteP"].ToString();
                if (dr["idSegmentoP"] != DBNull.Value)
                    o.idSegmentoP = (int)dr["idSegmentoP"];
                if (dr["SegmentoP"] != DBNull.Value)
                    o.Segmento_des = dr["SegmentoP"].ToString();
                if (dr["idSectorP"] != DBNull.Value)
                    o.idSectorP = (int)dr["idSectorP"];
                if (dr["SectorP"] != DBNull.Value)
                    o.Sector_des = dr["SectorP"].ToString();

                if (dr["ACONSECT"] != DBNull.Value)
                    o.ACS = dr["ACONSECT"].ToString();
                if (dr["ACONTECNO"] != DBNull.Value)
                    o.ACT = dr["ACONTECNO"].ToString();

                if (dr["idACONSECT"] != DBNull.Value)
                    o.idACS = dr["idACONSECT"].ToString();
                if (dr["idACONTECNO"] != DBNull.Value)
                    o.idACT = dr["idACONTECNO"].ToString();

                if (dr["Tipo"] != DBNull.Value)
                    o.Tipo = dr["Tipo"].ToString();
                if (dr["Dentro"] != DBNull.Value)
                    o.Dentro = dr["Dentro"].ToString();
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t808_idexpprof = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;
        }


        public static string Update(string strDatosEP)
        {
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                int? idCli = null, idEC = null, idCliP = null;
                string[] aDat = Regex.Split(strDatosEP, "##");

                #region Datos Exp Prof + CuentasCVT

                if (aDat[3] == "I")
                {
                    if (aDat[4] != "" && aDat[4] != "null")
                        idCli = int.Parse(aDat[4]);
                }
                else
                {
                    //Empresa Contratante
                    //Si es negativo sabemos que es CuentaCVT
                    //Si es positivo sabemos que es cliente y por lo tanto hay que grabarlo en CuentasCVT
                    int auxE = (aDat[5] == "null") ? 0 : int.Parse(aDat[5]);
                    if (aDat[5]!="null" && auxE < 0)
                        idEC = int.Parse(aDat[5]);
                    else if (aDat[5] == "null" || auxE > 0)
                    {//La empresa no existe, la doy de alta
                        if (aDat[7] != "")
                        {
                            if (aDat[8] != "")
                            {
                                idEC = SUPER.BLL.CuentasCVT.Insert(tr, aDat[7], int.Parse(aDat[8]));
                            }
                        }
                    }

                    //Cliente
                    //Si es negativo sabemos que es CuentaCVT
                    //Si es positivo sabemos que es cliente y por lo tanto hay que grabarlo en CuentasCVT
                    int auxC = (aDat[6] == "null") ? 0 : int.Parse(aDat[6]);
                    if (aDat[6] != "null" && auxC < 0)
                        idCliP = int.Parse(aDat[6]);
                    else if (aDat[6] == "null" || auxC > 0)
                    {
                        if (aDat[9] != "")
                        {
                            //Comprueba que no la haya dado de alta como empresa contratante (origen)
                            if ((aDat[7] == aDat[9]) && (aDat[8] == aDat[10]))
                                idCliP = idEC;
                            else//La empresa no existe, la doy de alta
                            {
                                if (aDat[9] != "" && aDat[10] != "")
                                    idCliP = SUPER.BLL.CuentasCVT.Insert(tr, aDat[9], int.Parse(aDat[10]));
                            }
                        }
                    }
                }
                SUPER.DAL.EXPPROF.Update(tr, int.Parse(aDat[0]), aDat[12], aDat[11], (aDat[3]=="I")? true : false, idEC, idCliP, idCli,null);
                
                #endregion

                #region Areas de conocimiento tecnológico

                if (aDat[1]!=""){
                    SUPER.BLL.EXPPROFACS.InsertEP(tr, int.Parse(aDat[0]), aDat[1]);
                }

                #endregion

                #region Areas de conocimiento sectorial

                if (aDat[2] != "")
                {
                    SUPER.BLL.EXPPROFACT.InsertEP(tr, int.Parse(aDat[0]), aDat[2]);
                }

                #endregion
                

                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return "";//idEP.ToString() + "#item#" + sIdInsertados;
        }

        public static void Reasignar(SqlTransaction tr, int idOrigen, int idDestino, int idSegmento, string sDen)
        {
            //Si el destino es negativo es que es un cliente y no hay cuentaCVT. En este caso hay que crear una nueva cuenta y reasignar
            //al nuevo código obtenido
            if (idDestino < 0)
            {
                idDestino = SUPER.BLL.CuentasCVT.Insert(tr, sDen, idSegmento, 1, idDestino*(-1));
            }
            SUPER.DAL.EXPPROF.Reasignar(tr, idOrigen, idDestino);
        }

        public static string ObtenerExperienciasProyectos(string sDenExperiencia, int t314_idusuario, string t301_categoria, string t301_estado, string t305_cualidad, string sClientes, string sResponsables,
                                                        string sIDEstructura, bool bComparacionLogica, string sPSN, bool bAdministrador)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MA' style='width:1170px;' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:285px' />");//Denominación experiencia
            sb.Append("<col style='width:65px' />");//Id Proyecto
            sb.Append("<col style='width:20px' />");//ESTADO DEL PROYECTO
            sb.Append("<col style='width:200px' />");//Proyecto
            sb.Append("<col style='width:200px' />");//Cliente
            sb.Append("<col style='width:200px' />");//CR
            sb.Append("<col style='width:200px' />");//Responsable
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.DAL.EXPPROF.ObtenerExperienciasProyectos(sDenExperiencia, t314_idusuario, t301_estado, t301_categoria, t305_cualidad,
                                                                            sClientes, sResponsables, sIDEstructura, bComparacionLogica, sPSN, bAdministrador);

            while (dr.Read())
            {
                
                sb.Append("<tr style='height:20px' id=" + dr["t305_idproyectosubnodo"].ToString() + "/" + dr["t808_idexpprof"].ToString());
                sb.Append(" estado='" + dr["t301_estado"].ToString() + "'");
                sb.Append(" desc='" + dr["t808_descripcion"].ToString() + "'");
                sb.Append(" acs='" + dr["acs"].ToString() + "'");
                sb.Append(" act='" + dr["act"].ToString() + "'");
                sb.Append(">");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W280'>" + dr["t808_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W200' style='noWrap:true;'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W200' style='noWrap:true;'>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W200' style='noWrap:true;'>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W200' style='noWrap:true;'>" + dr["responsable_proyecto"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        #endregion
    }
}