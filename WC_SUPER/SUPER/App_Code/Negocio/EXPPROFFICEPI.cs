using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
//Para el ArrayList

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EXPPROFFICEPI
    /// </summary>
    public partial class EXPPROFFICEPI
    {
        #region Propiedades y Atributos
        private int _t812_idexpprofficepi;
        public int t812_idexpprofficepi
        {
            get { return _t812_idexpprofficepi; }
            set { _t812_idexpprofficepi = value; }
        }

        private string _t812_visiblecv;
        public string t812_visiblecv
        {
            get { return _t812_visiblecv; }
            set { _t812_visiblecv = value; }
        }

        private DateTime? _t812_finicio;
        public DateTime? t812_finicio
        {
            get { return _t812_finicio; }
            set { _t812_finicio = value; }
        }

        private DateTime? _t812_ffin;
        public DateTime? t812_ffin
        {
            get { return _t812_ffin; }
            set { _t812_ffin = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t808_idexpprof;
        public int t808_idexpprof
        {
            get { return _t808_idexpprof; }
            set { _t808_idexpprof = value; }
        }

        private int? _t001_idficepi_validador;
        public int? t001_idficepi_validador
        {
            get { return _t001_idficepi_validador; }
            set { _t001_idficepi_validador = value; }
        }

        private int? _t819_idplantillacvt;
        public int? t819_idplantillacvt
        {
            get { return _t819_idplantillacvt; }
            set { _t819_idplantillacvt = value; }
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

        /*
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

        private int? _t811_idcuenta_ori;
        public int? t811_idcuenta_ori
        {
            get { return _t811_idcuenta_ori; }
            set { _t811_idcuenta_ori = value; }
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
        */
        #endregion

        #region Constructor

        public EXPPROFFICEPI(SqlTransaction tr, int t812_idexpprofficepi)
        {
            this.t812_idexpprofficepi = t812_idexpprofficepi;
            if (t812_idexpprofficepi != -1)
            {
                SUPER.DAL.EXPPROFFICEPI oExp = SUPER.DAL.EXPPROFFICEPI.Select(tr, t812_idexpprofficepi);
                this.t812_visiblecv = oExp.t812_visiblecv;
                this.t812_finicio = oExp.t812_finicio;
                this.t812_ffin = oExp.t812_ffin;
                this.t001_idficepi = oExp.t001_idficepi;
                this.t808_idexpprof = oExp.t808_idexpprof;
                this.t001_idficepi_validador = oExp.t001_idficepi_validador;
                this.t819_idplantillacvt = oExp.t819_idplantillacvt;
                this._idValidador = oExp.idValidador;
                this._denValidador = oExp.denValidador;
            }
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
        public static string GrabarPerfil(string strDatosGen, string strDatosET, string sEsMiCV)
        {
            string sResul = "";
            int idExpProf=-1, idExpProfFicepi=-1, idExpFicepiPerfil=-1;
            DateTime? dtIni=null;
            DateTime? dtFin=null;
            int? idValidador = null;

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
                string[] aVal = Regex.Split(strDatosGen, "##");
                idExpProf = int.Parse(aVal[0]);
                idExpProfFicepi = int.Parse(aVal[1]);
                idExpFicepiPerfil = int.Parse(aVal[2]);
                if (aVal[5] != "")
                    dtIni = Convert.ToDateTime(aVal[5]);
                else
                    dtIni = null;
                #region profesional
                int? idPlantilla = null;
                if (aVal[6] != "")
                    dtFin = Convert.ToDateTime(aVal[6]);
                else
                    dtFin = null;
                if (idExpProfFicepi==-1)
                {
                    if (aVal[4] == "")
                        idExpProfFicepi=SUPER.BLL.EXPPROFFICEPI.Insert(tr, null, dtIni, dtFin, int.Parse(aVal[3]), idExpProf, idValidador, idPlantilla);
                    else
                        idExpProfFicepi = SUPER.BLL.EXPPROFFICEPI.Insert(tr, aVal[4], dtIni, dtFin, int.Parse(aVal[3]), idExpProf, idValidador, idPlantilla);
                }
                #endregion
                #region Perfil
                if (aVal[14] == "")
                    aVal[14] = "0";

                if (idExpFicepiPerfil == -1)//Vamos a grabar un perfil nuevo
                {
                    idExpFicepiPerfil = SUPER.DAL.EXPFICEPIPERFIL.Insert(tr, dtIni, dtFin, aVal[7], aVal[8], aVal[9], aVal[10], DateTime.Now,
                                                                         int.Parse(aVal[11]), idExpProfFicepi,
                                                                         int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString()),
                                                                         (HttpContext.Current.Session["PROFESIONAL_CVEXCLUSION"].ToString() != "0") ? (int?)int.Parse(HttpContext.Current.Session["PROFESIONAL_CVEXCLUSION"].ToString()) : null,
                                                                         (HttpContext.Current.Session["RESPONSABLE_CVEXCLUSION"].ToString() != "0") ? (int?)int.Parse(HttpContext.Current.Session["RESPONSABLE_CVEXCLUSION"].ToString()) : null,
                                                                         short.Parse(aVal[13]), int.Parse(aVal[14]));

                    //Insertar en perfil si tuviera plantilla asociada
                    //if aVal[14] != ""
                }
                else
                {
                    int iRows = SUPER.DAL.EXPFICEPIPERFIL.Update(tr, idExpFicepiPerfil, dtIni, dtFin, aVal[7], aVal[8], aVal[9], aVal[10],
                                                     DateTime.Now, int.Parse(aVal[11]),
                                                     int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString()),
                                                     aVal[12],
                                                     (HttpContext.Current.Session["PROFESIONAL_CVEXCLUSION"].ToString() != "0") ? (int?)int.Parse(HttpContext.Current.Session["PROFESIONAL_CVEXCLUSION"].ToString()) : null,
                                                                         (HttpContext.Current.Session["RESPONSABLE_CVEXCLUSION"].ToString() != "0") ? (int?)int.Parse(HttpContext.Current.Session["RESPONSABLE_CVEXCLUSION"].ToString()) : null,
                                                     short.Parse(aVal[13]));
                    if (iRows == 0)
                    {
                        throw (new Exception("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha modificado el estado del perfil, por lo que la acción no ha podido ser realizada."));
                    }
                }
                #endregion
                #region Actualizar fechas en la T812_EXPPROFFICEPI
                //Si la experiencia no está ligada a proyecto, su inicio y fin viene marcado por los perfiles
                //tomando el mínimo de los inicios y el máximo de los finales
                if (aVal[15] == "N")
                {
                    DateTime? dFechaPerfilMin = DAL.EXPPROFFICEPI.getFechaMinPerfiles(tr, idExpProfFicepi);
                    DateTime? dFechaPerfilMax = DAL.EXPPROFFICEPI.getFechaMaxPerfiles(tr, idExpProfFicepi);
                    //if (dFechaPerfilMax.ToString() != "")
                    //{
                    //    if (dFechaPerfilMax.ToString().Substring(0, 10) == "31/12/2070")
                    //        dFechaPerfilMax = null;
                    //}

                    DAL.EXPPROFFICEPI.UpdateFechas(tr, idExpProfFicepi, dFechaPerfilMin, dFechaPerfilMax);
                }
                #endregion
                #region Entornos tecnológicos
                string[] aClase = Regex.Split(strDatosET, "///");
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
                            SUPER.DAL.EXPFICEPIENTORNO.Insert(tr, idExpFicepiPerfil, int.Parse(aValores[1]));
                            break;
                        case "D":
                            //if (aValores[2] != "")
                            SUPER.DAL.EXPFICEPIENTORNO.Delete(tr, idExpFicepiPerfil, int.Parse(aValores[1]));
                            break;
                    }
                }
                #endregion
                sResul = "OK@#@" + idExpProfFicepi.ToString() + "@#@" + idExpFicepiPerfil.ToString();
                //No hace falta, se hace por trigger
                //if (sEsMiCV == "S" && (aVal[9] == "O" || aVal[9] == "P")) 
                //    DAL.Curriculum.ActualizadoCV(tr, int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()));

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

        #endregion

        #region Metodos
        /// <summary>
        /// Inserta en T812_EXPPROFFICEPI. Si se está asignando un código de plantilla, el resto de perfiles se marcan como no visibles
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t812_visiblecv"></param>
        /// <param name="t812_finicio"></param>
        /// <param name="t812_ffin"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t808_idexpprof"></param>
        /// <param name="t001_idficepi_validador"></param>
        /// <param name="t819_idplantillacvt"></param>
        /// <returns></returns>
        public static int Insert(SqlTransaction tr, string t812_visiblecv, Nullable<DateTime> t812_finicio, Nullable<DateTime> t812_ffin,
                             int t001_idficepi, int t808_idexpprof, Nullable<int> t001_idficepi_validador, Nullable<int> t819_idplantillacvt)
        {
            return SUPER.DAL.EXPPROFFICEPI.Insert(tr, t812_visiblecv, t812_finicio, t812_ffin, t001_idficepi,
                                                   t808_idexpprof, t001_idficepi_validador, t819_idplantillacvt);
        }
        /// <summary>
        /// Actualiza una experiencia profesional. Si se pone como no visible en el Currículum se limpian (por trigger) los campos de petición de borrado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t812_idexpprofficepi"></param>
        /// <param name="t812_visiblecv"></param>
        /// <param name="t812_finicio"></param>
        /// <param name="t812_ffin"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t808_idexpprof"></param>
        /// <param name="t001_idficepi_validador"></param>
        /// <param name="t819_idplantillacvt"></param>
        /// <returns></returns>
        public static int Update(SqlTransaction tr, int t812_idexpprofficepi, string t812_visiblecv, Nullable<DateTime> t812_finicio, Nullable<DateTime> t812_ffin,
                             int t001_idficepi, int t808_idexpprof, Nullable<int> t001_idficepi_validador, Nullable<int> t819_idplantillacvt)
        {
            //Si tiene perfiles asociados, la plantilla debe ser null (y, lógicamente, no deben borrarse)
            if (t819_idplantillacvt == -1)
                t819_idplantillacvt = null;

            //01-16(Lacalle): Si el profesional tuviera perfiles no se borran
            //if (t819_idplantillacvt != null)//&& t812_visiblecv != "N"
             //   BorrarPerfiles(tr, t812_idexpprofficepi);

            int iRes = SUPER.DAL.EXPPROFFICEPI.Update(tr, t812_idexpprofficepi, t812_visiblecv, t812_finicio, t812_ffin, t001_idficepi,
                                                   t808_idexpprof, t001_idficepi_validador, t819_idplantillacvt);
            //if (t812_visiblecv=="N")
            //    SUPER.DAL.EXPPROFFICEPI.QuitarPeticionBorrado(tr, t812_idexpprofficepi);
            return iRes;
        }
        public static string getProfesionales(SqlTransaction tr, int t808_idexpprof, bool bModoLectura)
        {
            StringBuilder sb = new StringBuilder();
            string sCV = "", sAux = "", sVisibleCV = "";
            //Si un profesional tiene asignadas mas de 10 jornadas económicas, no se podrá asignar su CV como pendiente
            double dEsfuerzoenjor = 0;

            sb.Append("<table id='tblProf'");
            if (bModoLectura)
                sb.Append(" class='texto' style='width:915px;table-layout:fixed;' cellspacing='0' border='0'>");
            else
                sb.Append(" class='texto MANO' style='width:915px;table-layout:fixed;' cellspacing='0' border='0' mantenimiento='1'>");

            sb.Append("<colgroup>");
            sb.Append(" <col style='width:15px;' />");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:130px;' />");
            sb.Append(" <col style='width:0px;' />");
            sb.Append("</colgroup>");

            List<IB.SUPER.APP.Models.ExpProfFicepi> lstProf = SUPER.DAL.EXPPROFFICEPI.getProfesionales(null, t808_idexpprof);

            //Compruebo si hay algún profesional en la experiencia con Plantilla
            bool bHayProfConPlantilla = false;
            foreach (IB.SUPER.APP.Models.ExpProfFicepi oProf in lstProf)
            {
                if (oProf.idPlantilla != null)
                {
                    bHayProfConPlantilla = true;
                    break;
                }
            }
            //while (dr.Read())
            foreach (IB.SUPER.APP.Models.ExpProfFicepi oProf in lstProf)
            {
                if (oProf.esfuerzoJornadas != null)
                    dEsfuerzoenjor = (double)oProf.esfuerzoJornadas;
                else
                    dEsfuerzoenjor = 0;

                //sb.Append("<tr id='" + dr["t812_idexpprofficepi"].ToString() + "' bd='" + dr["BD"].ToString() + "' ");
                sb.Append("<tr id='" + oProf.t812_idexpprofficepi.ToString() + "' ");
                if (oProf.t812_idexpprofficepi.ToString() == "-1")
                    sb.Append("bd='I' ");
                else
                    sb.Append("bd='U' ");
                sb.Append("idF='" + oProf.t001_idficepi.ToString() + "' ");
                sb.Append("tipo='" + oProf.tipo + "' ");
                sb.Append("sexo='" + oProf.sexo + "'");
                sb.Append("plant='" + oProf.idPlantilla.ToString() + "' ");
                sb.Append("plantNew='" + oProf.idPlantilla.ToString() + "' ");
                sb.Append("valid='" + oProf.t001_idficepi_validador.ToString() + "' ");
                sb.Append("validNew='" + oProf.t001_idficepi_validador.ToString() + "' ");
                sb.Append("esf='" + dEsfuerzoenjor.ToString().Replace(",", ".") + "' ");
                sb.Append("esfJ='" + Math.Truncate(decimal.Parse(dEsfuerzoenjor.ToString())).ToString() + "' ");
                sVisibleCV = oProf.visibleCV.ToString().Trim();
                switch (sVisibleCV)
                {
                    case "S":
                        sb.Append("cv='S' ");
                        sCV = "Sí";
                        break;
                    case "N":
                        sb.Append("cv='N' ");
                        sCV = "No";
                        break;
                    default:
                        if (t808_idexpprof == -1)
                        {//SI LA EXPERIENCIA NO EXISTE
                            if (dEsfuerzoenjor >= 10)
                            {
                                sCV = "Sí";
                                sb.Append("cv='S' ");
                            }
                            else
                            {
                                sCV = "Pdte.";
                                sb.Append("cv='P' ");
                            }
                        }
                        else
                        {
                            if (bHayProfConPlantilla)
                            {
                                if (dEsfuerzoenjor >= 10)
                                {
                                    sCV = "";//SIN VALOR
                                    sb.Append("cv='' ");
                                }
                                else
                                {
                                    sCV = "Pdte.";
                                    sb.Append("cv='P' ");
                                }
                            }
                            else
                            {
                                if (dEsfuerzoenjor >= 10)
                                {
                                    sCV = "Sí";
                                    sb.Append("cv='S' ");
                                }
                                else
                                {
                                    sCV = "Pdte.";
                                    sb.Append("cv='P' ");
                                }
                            }
                        }
                        break;
                }


                sb.Append("style='height:22px'>");
                sb.Append("<td></td><td></td>");

                sb.Append("<td><nobr class='NBR W230'>" + oProf.profesional + "</nobr></td>");

                sAux = oProf.anomesPrimerConsumo.ToString();
                if (sAux != "")
                    sAux = SUPER.Capa_Negocio.Fechas.AnnomesAFechaDescCorta(int.Parse(sAux));
                sb.Append("<td>" + sAux + "</td>");
                sAux = oProf.anomesUltimoConsumo.ToString();
                if (sAux != "")
                    sAux = SUPER.Capa_Negocio.Fechas.AnnomesAFechaDescCorta(int.Parse(sAux));
                sb.Append("<td>" + sAux + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + dEsfuerzoenjor.ToString("N") + "</td>");

                sb.Append("<td>" + ((oProf.finicio == null) ? "" : ((DateTime)oProf.finicio).ToShortDateString()) + "</td>");
                sb.Append("<td>" + ((oProf.ffin == null) ? "" : ((DateTime)oProf.ffin).ToShortDateString()) + "</td>");
                sb.Append("<td>" + sCV + "</td>");
                if (oProf.perfil != "")
                {
                    if (bModoLectura)
                    {
                        sb.Append("<td><nobr style='vertical-align:top; padding-left:2px;' class='NBR W110' onmouseover='TTip(event)'>" + oProf.perfil + "</nobr></td>");
                    }
                    else
                    {
                        sb.Append("<td style='vertical-align: middle'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'>" + oProf.perfil + "</nobr></td>");
                    }
                }
                else
                {
                    sb.Append("<td style='vertical-align: middle'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'></nobr></td>");
                }
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string getProfesionales(SqlTransaction tr, int t301_idproyecto, int t808_idexpprof, bool bModoLectura)
        {
            StringBuilder sb = new StringBuilder();
            string sCV = "", sAux="", sVisibleCV="";
            //Si un profesional tiene asignadas mas de 10 jornadas económicas, no se podrá asignar su CV como pendiente
            double dEsfuerzoenjor = 0;

            sb.Append("<table id='tblProf'");
            if (bModoLectura)
                sb.Append(" class='texto' style='width:915px;table-layout:fixed;' cellspacing='0' border='0'>");
            else
                sb.Append(" class='texto MANO' style='width:915px;table-layout:fixed;' cellspacing='0' border='0' mantenimiento='1'>");

            sb.Append("<colgroup>");
            sb.Append(" <col style='width:15px;' />");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:80px;' />");
            sb.Append(" <col style='width:130px;' />");
            sb.Append(" <col style='width:0px;' />");
            sb.Append("</colgroup>");

            //SqlDataReader dr = SUPER.DAL.EXPPROFFICEPI.getProfesionales(null, t301_idproyecto, t808_idexpprof);
            List<IB.SUPER.APP.Models.ExpProfFicepi> lstProf = SUPER.DAL.EXPPROFFICEPI.getProfesionales(null, t301_idproyecto, t808_idexpprof);

            //Compruebo si hay algún profesional en la experiencia con Plantilla
            bool bHayProfConPlantilla = false;
            foreach (IB.SUPER.APP.Models.ExpProfFicepi oProf in lstProf)
            {
                if (oProf.idPlantilla != null)
                {
                    bHayProfConPlantilla = true;
                    break;
                }
            }
            //while (dr.Read())
            foreach (IB.SUPER.APP.Models.ExpProfFicepi oProf in lstProf)
            {
                if (oProf.esfuerzoJornadas != null)
                    dEsfuerzoenjor = (double)oProf.esfuerzoJornadas;
                else
                    dEsfuerzoenjor = 0;

                //sb.Append("<tr id='" + dr["t812_idexpprofficepi"].ToString() + "' bd='" + dr["BD"].ToString() + "' ");
                sb.Append("<tr id='" + oProf.t812_idexpprofficepi.ToString() + "' ");
                if (oProf.t812_idexpprofficepi.ToString() == "-1")
                    sb.Append("bd='I' ");
                else
                    sb.Append("bd='U' ");
                sb.Append("idF='" + oProf.t001_idficepi.ToString() + "' ");
                sb.Append("tipo='" + oProf.tipo + "' ");
                sb.Append("sexo='" + oProf.sexo + "'");
                sb.Append("plant='" + oProf.idPlantilla.ToString() + "' ");
                sb.Append("plantNew='" + oProf.idPlantilla.ToString() + "' ");
                sb.Append("valid='" + oProf.t001_idficepi_validador.ToString() + "' ");
                sb.Append("validNew='" + oProf.t001_idficepi_validador.ToString() + "' ");
                sb.Append("esf='" + dEsfuerzoenjor.ToString().Replace(",", ".") + "' ");
                sb.Append("esfJ='" + Math.Truncate(decimal.Parse(dEsfuerzoenjor.ToString())).ToString()+ "' ");
                //if (dr["t812_visiblecv"] == DBNull.Value)
                //{
                //if (dr["t812_idexpprofficepi"].ToString() == "-1")
                //    sb.Append("cv='' ");//SIN VALOR
                //else
                //{
                //    if (dEsfuerzoenjor > 10)
                //        sb.Append("cv='' ");//SIN VALOR
                //    else
                //        sb.Append("cv='P' ");//Pendiente
                //}
                //}
                //else
                //{
                sVisibleCV = oProf.visibleCV.ToString().Trim();
                switch (sVisibleCV)
                {
                    case "S":
                        sb.Append("cv='S' ");
                        sCV = "Sí";
                        break;
                    case "N":
                        sb.Append("cv='N' ");
                        sCV = "No";
                        break;
                    //case "P":
                    default:
                        if (t808_idexpprof==-1)
                        {//SI LA EXPERIENCIA NO EXISTE
                            if (dEsfuerzoenjor >= 10)
                            {
                                sCV = "Sí";
                                sb.Append("cv='S' ");
                            }
                            else
                            {
                                sCV = "Pdte.";
                                sb.Append("cv='P' ");
                            }
                        }
                        else
                        {
                            if (bHayProfConPlantilla)
                            {
                                if (dEsfuerzoenjor >= 10)
                                {
                                    sCV = "";//SIN VALOR
                                    sb.Append("cv='' ");
                                }
                                else
                                {
                                    sCV = "Pdte.";
                                    sb.Append("cv='P' ");
                                }
                            }
                            else
                            {
                                if (dEsfuerzoenjor >= 10)
                                {
                                    sCV = "Sí";
                                    sb.Append("cv='S' ");
                                }
                                else
                                {
                                    sCV = "Pdte.";
                                    sb.Append("cv='P' ");
                                }
                            }
                        }
                        break;
                }
                //}

                //sb.Append("primerConsumo='" + ((dr["dPrimerConsumo"] == DBNull.Value) ? "" : ((DateTime)dr["dPrimerConsumo"]).ToShortDateString()) + "' ");
                //sb.Append("ultimoConsumo='" + ((dr["dUltimoConsumo"] == DBNull.Value) ? "" : ((DateTime)dr["dUltimoConsumo"]).ToShortDateString())  + "' ");
                //sb.Append("esfuerzoenjor='" + double.Parse(dr["esfuerzoenjor"].ToString()).ToString("N") + "' ");


                sb.Append("style='height:22px'>");
                sb.Append("<td></td><td></td>");

                sb.Append("<td><nobr class='NBR W230'>" + oProf.profesional + "</nobr></td>");

                sAux = oProf.anomesPrimerConsumo.ToString();
                if (sAux != "")
                    sAux = SUPER.Capa_Negocio.Fechas.AnnomesAFechaDescCorta(int.Parse(sAux));
                sb.Append("<td>" + sAux + "</td>");
                sAux = oProf.anomesUltimoConsumo.ToString();
                if (sAux != "")
                    sAux = SUPER.Capa_Negocio.Fechas.AnnomesAFechaDescCorta(int.Parse(sAux));
                sb.Append("<td>" + sAux + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + dEsfuerzoenjor.ToString("N") + "</td>");

                //sb.Append("<td>" + ((DateTime)dr["t812_finicio"]).ToShortDateString() + "</td>");
                sb.Append("<td>" + ((oProf.finicio == null) ? "" : ((DateTime)oProf.finicio).ToShortDateString()) + "</td>");
                sb.Append("<td>" + ((oProf.ffin == null) ? "" : ((DateTime)oProf.ffin).ToShortDateString()) + "</td>");
                //if (dr["t812_visiblecv"] == DBNull.Value)
                //{
                //    if (dr["t812_idexpprofficepi"].ToString() == "-1")
                //        sCV = "";//SIN VALOR
                //    else
                //    {
                //        if (dEsfuerzoenjor > 10)
                //            sCV = "";//SIN VALOR
                //        else
                //            sCV = "Pdte.";
                //    }
                //}
                //else
                //{
                //}
                sb.Append("<td>"+sCV+"</td>");
                if (oProf.perfil != "")
                {
                    if (bModoLectura)
                    {
                        //sb.Append("<td><img style='width:16px; background:no-repeat;'><nobr style='vertical-align:top; padding-left:2px;' class='NBR W110' onmouseover='TTip(event)'>" + dr["t819_denominacion"].ToString() + "</nobr></td>");
                        sb.Append("<td><nobr style='vertical-align:top; padding-left:2px;' class='NBR W110' onmouseover='TTip(event)'>" + oProf.perfil + "</nobr></td>");
                    }
                    else
                    {
                        //sb.Append("<td style='vertical-align: middle'><img style='width:16px;background:no-repeat;'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'>" + dr["t819_denominacion"].ToString() + "</nobr></td>");
                        sb.Append("<td style='vertical-align: middle'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'>" + oProf.perfil + "</nobr></td>");
                    }
                }
                else
                {
                    //sb.Append("<td style='vertical-align: middle'><img style='width:16px;background:no-repeat;'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'></nobr></td>");
                    sb.Append("<td style='vertical-align: middle'><nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'></nobr></td>");
                }
                //12-15(Lacalle): Se elimina el concepto validador de esta pantalla
                //if (bModoLectura)
                //    sb.Append("<td><nobr class='NBR' style='width:115px;' onmouseover='TTip(event)'>" + dr["denValidador"].ToString() + "</nobr></td>");
                //else
                //    sb.Append("<td><nobr class='NBR' style='width:115px;' onmouseover='TTip(event)'>" + dr["denValidador"].ToString() + "</nobr></td>");
                sb.Append("<td></td>");


                sb.Append("</tr>");
            }
            
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void PedirBorrado(int t812_idexpprofficepi, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo, bool t812_borradoPlantilla)
        {
            //string sRes = "OK@#@";
            //try
            //{
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
                SUPER.DAL.EXPPROFFICEPI.PedirBorrado(null, t812_idexpprofficepi, t001_idficepi_petbor, sMotivo, t812_borradoPlantilla);
                //03/09/2013 De momento no enviamos correo
                //SUPER.Capa_Negocio.Correo.EnviarPetBorrado(sDatosCorreo, sMotivo);
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
            //}
            //catch (Exception ex)
            //{
            //    sRes = "ERROR@#@" + ex.Message;
            //}
            //return sRes;
        }
        /// <summary>
        /// Obtiene las experiencias de profesionales que han solicitado su borrado
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static string GetExperienciasBorrar(int t314_idusuario)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sTooltip = "";

            sb.Append("<table id='tblDatos' class='MANO' style='width:980px;' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");//Tipo de profesional
            sb.Append("<col style='width:210px' />");//Profesional
            sb.Append("<col style='width:210px;' />");//Experiencia profesional
            sb.Append("<col style='width:120px' />");//Perfil
            sb.Append("<col style='width:220px' />");//Motivo
            sb.Append("<col style='width:50px' />");//Aceptar
            sb.Append("<col style='width:45px' />");//No aceptar
            sb.Append("<col style='width:45px' />");//Reasignar 
            sb.Append("<col style='width:70px' />");//F.Limite 

            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.EXPPROFFICEPI.CatalogoPdtesBorrar(null, t314_idusuario);
            while (dr.Read())
            {
                sTooltip = "<label style=width:100px>Profesional:</label>" + dr["Profesional"].ToString();
                sTooltip += "<br><label style=width:100px>Empresa:</label>" + dr["EMPRESA"].ToString();
                sTooltip += "<br><label style=width:100px>" + SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO) + ":</label>" + dr["NODO"].ToString();
                sTooltip += "<br><label style=width:100px>Evaluador:</label>" + dr["SUPERVISOR"].ToString();

                sb.Append("<tr id='" + dr["t812_idexpprofficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("red='" + dr["t001_codred"].ToString() + "' ");
                sb.Append("idproyecto='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("expprof='" + dr["t808_idexpprof"].ToString() + "' ");
                //sb.Append("proy='" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + SUPER.Capa_Negocio.Utilidades.escape(dr["t301_denominacion"].ToString()) + "' ");
                sb.Append("denCli='" + dr["CLIENTE"].ToString() + "' ");
                sb.Append("idPlOri='" + dr["t819_idplantillacvt"].ToString() + "' ");//Id de la plantilla del perfil origen
                sb.Append("idPlDest='' ");//Id de la plantilla del perfil destino (para cuando se quiere reasignar)
                //if ((bool)dr["t812_borradoPlantilla"])
                //    sb.Append("bP='S' ");//Es una petición de borrado de perfil cuyo origen es una plantilla
                //else
                //    sb.Append("bP='N' ");
                if (dr["t819_idplantillacvt"].ToString()=="")
                    sb.Append("bP='N' ");
                else
                    sb.Append("bP='S' ");
                sb.Append("motivo='' >");

                sb.Append("<td style=\"border-right:none\"></td>");
                sb.Append("<td style='text-align:left;border-left: none;' class='MA'><nobr class='NBR W200' ");
                sb.Append("onmouseover=\'showTTE(\"" + SUPER.Capa_Negocio.Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\",null,null,350)\' onMouseout=\"hideTTE()\" ");
                sb.Append(">" + dr["PROFESIONAL"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)' class='MA'><nobr class='NBR W200'>" + dr["t808_denominacion"].ToString() + "</nobr></td>");
                //Si es una petición de borrado de perfil, muestro su denominación
                if ((bool)dr["t812_borradoPlantilla"])
                    sb.Append("<td onmouseover=\'showTTE(\"" + SUPER.Capa_Negocio.Utilidades.escape(dr["T035_DESCRIPCION"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;")) + "\",null,null,350)\' onMouseout=\"hideTTE()\" class='MA'><nobr class='NBR W110'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td onmouseover=\'showTTE(\"" + SUPER.Capa_Negocio.Utilidades.escape(dr["t812_motivo_petbor"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;")) + "\",null,null,350)\' onMouseout=\"hideTTE()\" class='MA'><nobr class='NBR W210'>" + dr["t812_motivo_petbor"].ToString() + "</nobr></td>");
                sb.Append("<td></td>");//Aceptar
                sb.Append("<td></td>");//No aceptar
                sb.Append("<td></td>");//Reasignar perfil de plantilla
                //if (HttpContext.Current.User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                //{ 
                //    if (dr["t970_flimite"] != DBNull.Value) // Fecha Límite
                //        sb.Append("<td align='left'>" + DateTime.Parse(dr["t970_flimite"].ToString()).ToShortDateString() + "</td>");
                //    else sb.Append("<td></td>");
                //}
                //else sb.Append("<td></td>");
                sb.Append("<td></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();

        }
        public static void BorrarExperiencias(string strDatos)
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
                string[] aExperiencias = Regex.Split(strDatos, "@fila@");
                foreach (string sExperiencia in aExperiencias)
                {
                    if (sExperiencia != "")
                    {
                        string[] aArgs = Regex.Split(sExperiencia, "@dato@");
                        switch (aArgs[0])
                        {
                            case "N"://Elimina la petición de borrado y envia correo con el motivo
                                SUPER.DAL.EXPPROFFICEPI.QuitarPeticionBorrado(tr, int.Parse(aArgs[1]));
                                SUPER.Capa_Negocio.Correo.EnviarPetDenegada(aArgs[6], aArgs[2], aArgs[5], 
                                                                            SUPER.Capa_Negocio.Utilidades.unescape(aArgs[3]), 
                                                                            SUPER.Capa_Negocio.Utilidades.unescape(aArgs[4]),
                                                                            SUPER.Capa_Negocio.Utilidades.unescape(aArgs[7]));
                                break;
                            case "B":
                                SUPER.DAL.EXPPROFFICEPI.QuitarPeticionBorrado(tr, int.Parse(aArgs[1]));
                                if (aArgs[2] == "S")
                                { //Es una petición de borrado de perfil cuyo origen es una plantilla
                                    //Pone la experiencia como que tiene que aparecer en el CV y quita la plantilla 
                                    SUPER.DAL.EXPPROFFICEPI.SetVisibilidad(tr, int.Parse(aArgs[1]), "S", null);
                                }
                                else
                                {
                                    //Pone la experiencia como que no tiene que aparecer en el CV y quita la plantilla 
                                    SUPER.DAL.EXPPROFFICEPI.SetVisibilidad(tr, int.Parse(aArgs[1]), "N", null);
                                }
                                break;
                            case "R"://Reasigna la plantilla del perfil, los campos de petición de borrado se limpian por trigger
                                SUPER.DAL.EXPPROFFICEPI.QuitarPeticionBorrado(tr, int.Parse(aArgs[1]));
                                SUPER.DAL.EXPPROFFICEPI.SetVisibilidad(tr, int.Parse(aArgs[1]), "S", int.Parse(aArgs[2]));
                                break;
                        }
                    }
                }
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
        /// <summary>
        /// marca como no visibles  todas las experiencias de un profesional en un proyecto salvo la indicada en el 3º parámetro
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <param name="t808_idexpprof"></param>
        /// <param name="t812_idexpprofficepi"></param>
        public static void BorrarPerfiles(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SUPER.DAL.EXPPROFFICEPI.BorrarPerfiles(tr, t812_idexpprofficepi);
        }
        /// <summary>
        /// Actualiza la fecha de realización en la tabla T970_EXPPROFFICEPI_TPL para los casos 2,3,4
        /// </summary>
        /// <param name="idExpProf"></param>
        /// <param name="sCasos"></param>
        /// <returns></returns>
        /// 
        public static string RevisadoPerfilExper(int idExpprofficepi)
        {
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            string sResul = "";
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
                SqlDataReader dr = SUPER.DAL.EXPPROFFICEPI.RealizarValidacion(tr, idExpprofficepi);
                if (dr.Read())
                {
                    if (int.Parse(dr["perfiles"].ToString()) > 0)
                    {
                        dr.Close();
                        dr.Dispose();

                        SUPER.DAL.EXPPROFFICEPI.ActualizarFRealizTareasPlazo(tr, idExpprofficepi);
                        SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                        sResul = "OK@#@";
                    }
                    else
                    {
                        dr.Close();
                        dr.Dispose();

                        sResul = "OK@#@NOVALIDADO";
                    }
                }
                else
                {
                    dr.Close();
                    dr.Dispose();

                    sResul = "OK@#@NOVALIDADO";
                }
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
    }
    #endregion
}