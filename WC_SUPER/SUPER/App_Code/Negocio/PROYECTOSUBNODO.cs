using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PROYECTOSUBNODO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T305_PROYECTOSUBNODO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	03/03/2008 11:19:00	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PROYECTOSUBNODO
    {
        #region Propiedades y Atributos Complementarios

        private int _t303_idnodo;
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private int _t303_ultcierreeco;
        public int t303_ultcierreeco
        {
            get { return _t303_ultcierreeco; }
            set { _t303_ultcierreeco = value; }
        }

        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        private string _t304_denominacion;
        public string t304_denominacion
        {
            get { return _t304_denominacion; }
            set { _t304_denominacion = value; }
        }

        private string _des_responsable;
        public string des_responsable
        {
            get { return _des_responsable; }
            set { _des_responsable = value; }
        }

        private string _ext_responsable;
        public string ext_responsable
        {
            get { return _ext_responsable; }
            set { _ext_responsable = value; }
        }

        private int _fecInicioReal;
        public int fecInicioReal
        {
            get { return _fecInicioReal; }
            set { _fecInicioReal = value; }
        }

        private int _fecFinReal;
        public int fecFinReal
        {
            get { return _fecFinReal; }
            set { _fecFinReal = value; }
        }

        private double _nProducidoReal;
        public double nProducidoReal
        {
            get { return _nProducidoReal; }
            set { _nProducidoReal = value; }
        }

        private bool _t320_facturable;
        public bool t320_facturable
        {
            get { return _t320_facturable; }
            set { _t320_facturable = value; }
        }

        private int _mesesCerrados;
        public int mesesCerrados
        {
            get { return _mesesCerrados; }
            set { _mesesCerrados = value; }
        }

        private string _des_visador;
        public string des_visador
        {
            get { return _des_visador; }
            set { _des_visador = value; }
        }

        private string _des_visadorcv;
        public string des_visadorcv
        {
            get { return _des_visadorcv; }
            set { _des_visadorcv = value; }
        }

        private string _des_interlocutor;
        public string des_interlocutor
        {
            get { return _des_interlocutor; }
            set { _des_interlocutor = value; }
        }

        private string _t391_denominacion;
        public string t391_denominacion
        {
            get { return _t391_denominacion; }
            set { _t391_denominacion = value; }
        }
        private string _t392_denominacion;
        public string t392_denominacion
        {
            get { return _t392_denominacion; }
            set { _t392_denominacion = value; }
        }
        private string _t393_denominacion;
        public string t393_denominacion
        {
            get { return _t393_denominacion; }
            set { _t393_denominacion = value; }
        }
        private string _t394_denominacion;
        public string t394_denominacion
        {
            get { return _t394_denominacion; }
            set { _t394_denominacion = value; }
        }
        private string _t301_categoria;
        public string t301_categoria
        {
            get { return _t301_categoria; }
            set { _t301_categoria = value; }
        }
        private string _t422_denominacion;
        public string t422_denominacion
        {
            get { return _t422_denominacion; }
            set { _t422_denominacion = value; }
        }
        private string _t422_denominacionimportes;
        public string t422_denominacionimportes
        {
            get { return _t422_denominacionimportes; }
            set { _t422_denominacionimportes = value; }
        }

        private string _profesional_diceno_cvt;
        public string profesional_diceno_cvt
        {
            get { return _profesional_diceno_cvt; }
            set { _profesional_diceno_cvt = value; }
        }

        private DateTime? _t301_fechano_cvt;
        public DateTime? t301_fechano_cvt
        {
            get { return _t301_fechano_cvt; }
            set { _t301_fechano_cvt = value; }
        }

        private string _t301_motivono_cvt;
        public string t301_motivono_cvt
        {
            get { return _t301_motivono_cvt; }
            set { _t301_motivono_cvt = value; }
        }

        public int t001_idficepi_responsable { get; set; }

        public string des_interlocalertasocfa { get; set; }
        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T305_PROYECTOSUBNODO junto con datos complementarios
        /// como el IDNodo y las descripciones de las foreng keys,
        /// y devuelve una instancia u objeto del tipo PROYECTOSUBNODO
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static PROYECTOSUBNODO Obtener(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            PROYECTOSUBNODO o = new PROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSUBNODO_SD", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTOSUBNODO_SD", aParam);

            if (dr.Read())
            {
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t304_idsubnodo"] != DBNull.Value)
                    o.t304_idsubnodo = int.Parse(dr["t304_idsubnodo"].ToString());
                if (dr["t320_facturable"] != DBNull.Value)
                    o.t320_facturable = (bool)dr["t320_facturable"];
                if (dr["t305_finalizado"] != DBNull.Value)
                    o.t305_finalizado = (bool)dr["t305_finalizado"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["t305_heredanodo"] != DBNull.Value)
                    o.t305_heredanodo = (bool)dr["t305_heredanodo"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t304_denominacion"] != DBNull.Value)
                    o.t304_denominacion = (string)dr["t304_denominacion"];
                if (dr["t303_ultcierreeco"] != DBNull.Value)
                    o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi_responsable = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["Responsable"] != DBNull.Value)
                    o.des_responsable = (string)dr["Responsable"];
                if (dr["t305_seudonimo"] != DBNull.Value)
                    o.t305_seudonimo = (string)dr["t305_seudonimo"];
                if (dr["t305_accesobitacora_iap"] != DBNull.Value)
                    o.t305_accesobitacora_iap = (string)dr["t305_accesobitacora_iap"];
                if (dr["t305_accesobitacora_pst"] != DBNull.Value)
                    o.t305_accesobitacora_pst = (string)dr["t305_accesobitacora_pst"];
                if (dr["t305_imputablegasvi"] != DBNull.Value)
                    o.t305_imputablegasvi = (bool)dr["t305_imputablegasvi"];
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisoresponsablepst"] != DBNull.Value)
                    o.t305_avisoresponsablepst = (bool)dr["t305_avisoresponsablepst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t305_avisofigura"] != DBNull.Value)
                    o.t305_avisofigura = (bool)dr["t305_avisofigura"];
                if (dr["t305_modificaciones"] != DBNull.Value)
                    o.t305_modificaciones = (string)dr["t305_modificaciones"];
                if (dr["t305_observaciones"] != DBNull.Value)
                    o.t305_observaciones = (string)dr["t305_observaciones"];
                if (dr["mesesCerrados"] != DBNull.Value)
                    o.mesesCerrados = int.Parse(dr["mesesCerrados"].ToString());
                if (dr["t001_ficepi_visador"] != DBNull.Value)
                    o.t001_ficepi_visador = int.Parse(dr["t001_ficepi_visador"].ToString());
                if (dr["Visador"] != DBNull.Value)
                    o.des_visador = (string)dr["Visador"];
                if (dr["t305_supervisor_visador"] != DBNull.Value)
                    o.t305_supervisor_visador = (bool)dr["t305_supervisor_visador"];
                if (dr["t476_idcnp"] != DBNull.Value)
                    o.t476_idcnp = int.Parse(dr["t476_idcnp"].ToString());
                if (dr["t485_idcsn1p"] != DBNull.Value)
                    o.t485_idcsn1p = int.Parse(dr["t485_idcsn1p"].ToString());
                if (dr["t487_idcsn2p"] != DBNull.Value)
                    o.t487_idcsn2p = int.Parse(dr["t487_idcsn2p"].ToString());
                if (dr["t489_idcsn3p"] != DBNull.Value)
                    o.t489_idcsn3p = int.Parse(dr["t489_idcsn3p"].ToString());
                if (dr["t491_idcsn4p"] != DBNull.Value)
                    o.t491_idcsn4p = int.Parse(dr["t491_idcsn4p"].ToString());
                if (dr["t305_observacionesadm"] != DBNull.Value)
                    o.t305_observacionesadm = (string)dr["t305_observacionesadm"];
                if (dr["t305_importaciongasvi"] != DBNull.Value)
                    o.t305_importaciongasvi = (byte)dr["t305_importaciongasvi"];
                if (dr["t305_observacionesadm"] != DBNull.Value)
                    o.t305_observacionesadm = (string)dr["t305_observacionesadm"];
                if (dr["t305_importaciongasvi"] != DBNull.Value)
                    o.t305_importaciongasvi = (byte)dr["t305_importaciongasvi"];
                if (dr["t391_denominacion"] != DBNull.Value)
                    o.t391_denominacion = (string)dr["t391_denominacion"];
                if (dr["t392_denominacion"] != DBNull.Value)
                    o.t392_denominacion = (string)dr["t392_denominacion"];
                if (dr["t393_denominacion"] != DBNull.Value)
                    o.t393_denominacion = (string)dr["t393_denominacion"];
                if (dr["t394_denominacion"] != DBNull.Value)
                    o.t394_denominacion = (string)dr["t394_denominacion"];
                if (dr["t301_categoria"] != DBNull.Value)
                    o.t301_categoria = (string)dr["t301_categoria"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t422_denominacionimportes"] != DBNull.Value)
                    o.t422_denominacionimportes = (string)dr["t422_denominacionimportes"];
                if (dr["t305_opd"] != DBNull.Value)
                    o.t305_opd = (bool)dr["t305_opd"];
                if (dr["t001_idficepi_visadorcv"] != DBNull.Value)
                    o.t001_idficepi_visadorcv = int.Parse(dr["t001_idficepi_visadorcv"].ToString());
                if (dr["Visadorcv"] != DBNull.Value)
                    o.des_visadorcv = (string)dr["VisadorCV"];
                if (dr["t001_idficepi_interlocutor"] != DBNull.Value)
                    o.t001_idficepi_interlocutor = int.Parse(dr["t001_idficepi_interlocutor"].ToString());
                if (dr["Interlocutor"] != DBNull.Value)
                    o.des_interlocutor = (string)dr["Interlocutor"];
                if (dr["PROFESIONAL_DICENO_CVT"] != DBNull.Value)
                    o.profesional_diceno_cvt = (string)dr["PROFESIONAL_DICENO_CVT"];
                if (dr["t301_fechano_cvt"] != DBNull.Value)
                    o.t301_fechano_cvt = (DateTime)dr["t301_fechano_cvt"];
                if (dr["t301_motivono_cvt"] != DBNull.Value)
                    o.t301_motivono_cvt = (string)dr["t301_motivono_cvt"];

                if (dr["t001_idficepi_interlocalertasocfa"] != DBNull.Value)
                    o.t001_idficepi_interlocalertasocfa = int.Parse(dr["t001_idficepi_interlocalertasocfa"].ToString());
                if (dr["des_interlocalertasocfa"] != DBNull.Value)
                    o.des_interlocalertasocfa = (string)dr["des_interlocalertasocfa"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PROYECTOSUBNODO"));
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
        public static PROYECTOSUBNODO ObtenerConProduccion(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            PROYECTOSUBNODO o = PROYECTOSUBNODO.Obtener(tr, t305_idproyectosubnodo);
            /*PROYECTOSUBNODO o = new PROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSUBNODO_SD", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTOSUBNODO_SD", aParam);

            if (dr.Read())
            {
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t304_idsubnodo"] != DBNull.Value)
                    o.t304_idsubnodo = int.Parse(dr["t304_idsubnodo"].ToString());
                if (dr["t320_facturable"] != DBNull.Value)
                    o.t320_facturable = (bool)dr["t320_facturable"];
                if (dr["t305_finalizado"] != DBNull.Value)
                    o.t305_finalizado = (bool)dr["t305_finalizado"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["t305_heredanodo"] != DBNull.Value)
                    o.t305_heredanodo = (bool)dr["t305_heredanodo"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t304_denominacion"] != DBNull.Value)
                    o.t304_denominacion = (string)dr["t304_denominacion"];
                if (dr["t303_ultcierreeco"] != DBNull.Value)
                    o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["Responsable"] != DBNull.Value)
                    o.des_responsable = (string)dr["Responsable"];
                if (dr["t305_seudonimo"] != DBNull.Value)
                    o.t305_seudonimo = (string)dr["t305_seudonimo"];
                if (dr["t305_accesobitacora_iap"] != DBNull.Value)
                    o.t305_accesobitacora_iap = (string)dr["t305_accesobitacora_iap"];
                if (dr["t305_accesobitacora_pst"] != DBNull.Value)
                    o.t305_accesobitacora_pst = (string)dr["t305_accesobitacora_pst"];
                if (dr["t305_imputablegasvi"] != DBNull.Value)
                    o.t305_imputablegasvi = (bool)dr["t305_imputablegasvi"];
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisoresponsablepst"] != DBNull.Value)
                    o.t305_avisoresponsablepst = (bool)dr["t305_avisoresponsablepst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t305_avisofigura"] != DBNull.Value)
                    o.t305_avisofigura = (bool)dr["t305_avisofigura"];
                if (dr["t305_modificaciones"] != DBNull.Value)
                    o.t305_modificaciones = (string)dr["t305_modificaciones"];
                if (dr["t305_observaciones"] != DBNull.Value)
                    o.t305_observaciones = (string)dr["t305_observaciones"];
                if (dr["mesesCerrados"] != DBNull.Value)
                    o.mesesCerrados = int.Parse(dr["mesesCerrados"].ToString());
                if (dr["t001_ficepi_visador"] != DBNull.Value)
                    o.t001_ficepi_visador = int.Parse(dr["t001_ficepi_visador"].ToString());
                if (dr["Visador"] != DBNull.Value)
                    o.des_visador = (string)dr["Visador"];
                if (dr["t305_supervisor_visador"] != DBNull.Value)
                    o.t305_supervisor_visador = (bool)dr["t305_supervisor_visador"];
                if (dr["t476_idcnp"] != DBNull.Value)
                    o.t476_idcnp = int.Parse(dr["t476_idcnp"].ToString());
                if (dr["t485_idcsn1p"] != DBNull.Value)
                    o.t485_idcsn1p = int.Parse(dr["t485_idcsn1p"].ToString());
                if (dr["t487_idcsn2p"] != DBNull.Value)
                    o.t487_idcsn2p = int.Parse(dr["t487_idcsn2p"].ToString());
                if (dr["t489_idcsn3p"] != DBNull.Value)
                    o.t489_idcsn3p = int.Parse(dr["t489_idcsn3p"].ToString());
                if (dr["t491_idcsn4p"] != DBNull.Value)
                    o.t491_idcsn4p = int.Parse(dr["t491_idcsn4p"].ToString());
                if (dr["t305_observacionesadm"] != DBNull.Value)
                    o.t305_observacionesadm = (string)dr["t305_observacionesadm"];
                if (dr["t305_importaciongasvi"] != DBNull.Value)
                    o.t305_importaciongasvi = (byte)dr["t305_importaciongasvi"];

                if (dr["t391_denominacion"] != DBNull.Value)
                    o.t391_denominacion = (string)dr["t391_denominacion"];
                if (dr["t392_denominacion"] != DBNull.Value)
                    o.t392_denominacion = (string)dr["t392_denominacion"];
                if (dr["t393_denominacion"] != DBNull.Value)
                    o.t393_denominacion = (string)dr["t393_denominacion"];
                if (dr["t394_denominacion"] != DBNull.Value)
                    o.t394_denominacion = (string)dr["t394_denominacion"];
                if (dr["t301_categoria"] != DBNull.Value)
                    o.t301_categoria = (string)dr["t301_categoria"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PROYECTOSUBNODO"));
            }
            dr.Close();
            dr.Dispose();
            */
            SqlParameter[] aParam1 = new SqlParameter[1];
            aParam1[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam1[0].Value = t305_idproyectosubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_GETREALIZADOPSN", aParam1);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETREALIZADOPSN", aParam1);
            if (dr.Read())
            {
                if (dr["Inicio"] != DBNull.Value)
                    o.fecInicioReal = int.Parse(dr["Inicio"].ToString());
                if (dr["Fin"] != DBNull.Value)
                    o.fecFinReal = int.Parse(dr["Fin"].ToString());
                if (dr["Producido"] != DBNull.Value)
                    o.nProducidoReal = double.Parse(dr["Producido"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
        public static PROYECTOSUBNODO ObtenerContratante(SqlTransaction tr, int t301_idproyecto)
        {
            PROYECTOSUBNODO o = new PROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSUBNODO_SCON", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTOSUBNODO_SCON", aParam);

            if (dr.Read())
            {
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t304_idsubnodo"] != DBNull.Value)
                    o.t304_idsubnodo = int.Parse(dr["t304_idsubnodo"].ToString());
                if (dr["t320_facturable"] != DBNull.Value)
                    o.t320_facturable = (bool)dr["t320_facturable"];
                if (dr["t305_finalizado"] != DBNull.Value)
                    o.t305_finalizado = (bool)dr["t305_finalizado"];
                if (dr["t305_cualidad"] != DBNull.Value)
                    o.t305_cualidad = (string)dr["t305_cualidad"];
                if (dr["t305_heredanodo"] != DBNull.Value)
                    o.t305_heredanodo = (bool)dr["t305_heredanodo"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_denominacion"] != DBNull.Value)
                    o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t304_denominacion"] != DBNull.Value)
                    o.t304_denominacion = (string)dr["t304_denominacion"];
                if (dr["t303_ultcierreeco"] != DBNull.Value)
                    o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.des_responsable = (string)dr["responsable"];
                if (dr["t305_seudonimo"] != DBNull.Value)
                    o.t305_seudonimo = (string)dr["t305_seudonimo"];
                if (dr["t305_accesobitacora_iap"] != DBNull.Value)
                    o.t305_accesobitacora_iap = (string)dr["t305_accesobitacora_iap"];
                if (dr["t305_accesobitacora_pst"] != DBNull.Value)
                    o.t305_accesobitacora_pst = (string)dr["t305_accesobitacora_pst"];
                if (dr["t305_imputablegasvi"] != DBNull.Value)
                    o.t305_imputablegasvi = (bool)dr["t305_imputablegasvi"];
                if (dr["t305_admiterecursospst"] != DBNull.Value)
                    o.t305_admiterecursospst = (bool)dr["t305_admiterecursospst"];
                if (dr["t305_avisoresponsablepst"] != DBNull.Value)
                    o.t305_avisoresponsablepst = (bool)dr["t305_avisoresponsablepst"];
                if (dr["t305_avisorecursopst"] != DBNull.Value)
                    o.t305_avisorecursopst = (bool)dr["t305_avisorecursopst"];
                if (dr["t305_avisofigura"] != DBNull.Value)
                    o.t305_avisofigura = (bool)dr["t305_avisofigura"];
                if (dr["t305_modificaciones"] != DBNull.Value)
                    o.t305_modificaciones = (string)dr["t305_modificaciones"];
                if (dr["t305_observaciones"] != DBNull.Value)
                    o.t305_observaciones = (string)dr["t305_observaciones"];
                if (dr["T001_EXTTEL"] != DBNull.Value)
                    o.ext_responsable = (string)dr["T001_EXTTEL"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PROYECTOSUBNODO"));
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        /// <summary>
        /// Dada una lista de proyectos, obtiene la lista de códigos de subnodo correspondientes
        /// </summary>        
        /// <param name="slProyecto"></param>
        /// <returns></returns>
        public static string ObtenerProyectosSubnodo(string slProyecto)
        {

            string sResp = "";

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ObtenerProyectosSubnodo(slProyecto);

            while (dr.Read())
            {
                sResp += dr["t305_idproyectosubnodo"].ToString() + ",";
            }

            dr.Close();
            dr.Dispose();

            return sResp;
            
        }



        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el tipo de coste que se aplica al proyecto H->por hora, J->por jornada
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static string GetTipoCoste(SqlTransaction tr, int nIdPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPE", SqlDbType.Int, 4);
            aParam[0].Value = nIdPE;

            if (tr == null)
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_PROYECTO_TIPOCOSTE", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTO_TIPOCOSTE", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene si permite asignar recursos desde la parte técnica
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool GetAdmiteRecursoPST(SqlTransaction tr, int nIdPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPE", SqlDbType.Int, 4);
            aParam[0].Value = nIdPE;

            if (tr == null)
                return Convert.ToBoolean(SqlHelper.ExecuteScalar("SUP_PROYECTO_ADMITERECURSOPST", aParam));
            else
                return Convert.ToBoolean(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTO_ADMITERECURSOPST", aParam));
        }
        public static void AsignarTareasProfesional(SqlTransaction tr, bool bSoloAsignadas, bool bSoloActivas, 
                                          int iRecursoOrigen, int iCodPSN, 
                                          int iCodRecurso, DateTime? dtFip, DateTime? dtFfp,
                                          int iTarifa, string sIndicaciones, bool bNotifExceso,
                                          bool bAdmiteRecursoPST, int IdNodo, int iUltCierreEco)
        {//Asigna recursos a todas las tareas de un ProyectoSubnodo que no lo tuvieran ya
            int iCodTarea;
            int? nIdTarif;
            string oRec;
            bool bRecursoAsignado, bNotifProf;
            //try
            //{
            List<SUPER.Capa_Negocio.TAREAPSP> oLista = SUPER.DAL.PROYECTOSUBNODO.GetTareasVivas(tr, iCodPSN, iRecursoOrigen, bSoloAsignadas, bSoloActivas);
            foreach(SUPER.Capa_Negocio.TAREAPSP oTarea in oLista)
            {
                iCodTarea = oTarea.t332_idtarea;
                bNotifProf = oTarea.t332_notif_prof;
                if (iTarifa == -1)
                    nIdTarif = null;
                else
                    nIdTarif = iTarifa;

                bRecursoAsignado = TareaRecurso.InsertarTEC(tr, iCodTarea, iCodRecurso, null, null, null, dtFip, dtFfp, nIdTarif, 1, "",
                                                            sIndicaciones, bNotifExceso, bAdmiteRecursoPST, iCodPSN, IdNodo, iUltCierreEco);

                if (bRecursoAsignado && bNotifProf)
                {//SOLO ENVIAMOS CORREO SI EL RECURSO NO ESTABA ASOCIADO A LA TAREA
                    oRec = "##" + iCodTarea.ToString() + "##" + iCodRecurso.ToString() + "################";
                    oRec += oTarea.t332_destarea + "##";
                    oRec += oTarea.num_proyecto.ToString() + "##" + oTarea.nom_proyecto + "##";
                    oRec += oTarea.t331_despt + "##";
                    oRec += oTarea.t334_desfase + "##" + oTarea.t335_desactividad + "##";
                    oRec += oTarea.t346_codpst + "##" + oTarea.t346_despst + "##";
                    oRec += oTarea.t332_otl + "##" + oTarea.t332_incidencia + "##";

                    TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, null, dtFip.ToString(), dtFfp.ToString(), sIndicaciones, oTarea.t332_mensaje);
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al asignar técnicos a tareas.", ex);
            //}
        }

        public static bool EsNecesarioReplicar(int nUsuario, Nullable<int> nProyecto, bool bAdministrador)
        {
            bool bResp = false;

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nProyecto", SqlDbType.Int, 4);
            aParam[1].Value = nProyecto;
            aParam[2] = new SqlParameter("@bAdministrador", SqlDbType.Bit, 1);
            aParam[2].Value = bAdministrador;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICANODO", aParam);

            if (dr.Read())
            {
                bResp = true;
            }

            dr.Close();
            dr.Dispose();

            return bResp;
        }
        public static SqlDataReader ObtenerProyectosAReplicar(int nUsuario, Nullable<int> nProyecto, bool bAdministrador)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nProyecto", SqlDbType.Int, 4);
            aParam[1].Value = nProyecto;
            aParam[2] = new SqlParameter("@bAdministrador", SqlDbType.Bit, 1);
            aParam[2].Value = bAdministrador;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICAPROY", aParam);
        }
        /// <summary>
        /// Dada una lista de proyectos, obtiene un catálogo de los que necesitan replica
        /// </summary>
        /// <param name="nUsuario"></param>
        /// <param name="nProyecto"></param>
        /// <param name="bAdministrador"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerProyectosAReplicar(int nUsuario, bool bAdministrador, string slProyecto)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@usuario", SqlDbType.Int, 4, nUsuario),
                ParametroSql.add("@bAdministrador", SqlDbType.Bit, 1, bAdministrador),
                ParametroSql.add("@tbl_psn", SqlDbType.Structured, SUPER.Capa_Negocio.Utilidades.GetDataTableFromListCod(slProyecto,",")),
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICA_LISTAPROY", aParam);
        }

        public static SqlDataReader ObtenerNodosDeProyectosAReplicar(int nUsuario, Nullable<int> nProyecto, bool bAdministrador)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nProyecto", SqlDbType.Int, 4);
            aParam[1].Value = nProyecto;
            aParam[2] = new SqlParameter("@bAdministrador", SqlDbType.Bit, 1);
            aParam[2].Value = bAdministrador;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICANODO", aParam);
        }
        public static DataSet ObtenerSubnodosParaReplicar(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_SUBNODOREPLICA", aParam);
        }

        public static bool EsNecesarioReplicarUSA(int nUsuario, bool bSAA)
        {
            bool bResp = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@bSAA", SqlDbType.Bit, 1);
            aParam[1].Value = bSAA;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICANODO_USA", aParam);

            if (dr.Read())
            {
                bResp = true;
            }

            dr.Close();
            dr.Dispose();

            return bResp;
        }
        public static SqlDataReader ObtenerProyectosAReplicarUSA(int nUsuario, bool bSAA)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@bSAA", SqlDbType.Bit, 1);
            aParam[1].Value = bSAA;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICAPROY_USA", aParam);
        }
        public static SqlDataReader ObtenerNodosDeProyectosAReplicarUSA(int nUsuario, bool bSAA)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@bSAA", SqlDbType.Bit, 1);
            aParam[1].Value = bSAA;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETREPLICANODO_USA", aParam);
        }
        public static SqlDataReader ObtenerProyectosACerrarUSA(int nUsuario, bool bSAA)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@bSAA", SqlDbType.Bit, 1);
            aParam[1].Value = bSAA;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETCIERREPROY_USA", 600, aParam);
        }

        public static SqlDataReader ObtenerProyectosACerrar(int nUsuario, Nullable<int> nPSN, bool bAdministrador)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;
            aParam[2] = new SqlParameter("@bAdmin", SqlDbType.Bit, 1);
            aParam[2].Value = bAdministrador;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETCIERREPROY", 600, aParam);
        }
        /// <summary>
        /// Dada una lista de proyectos, obtiene un catálogo de los que necesitan cierre
        /// </summary>
        /// <param name="nUsuario"></param>
        /// <param name="bAdministrador"></param>
        /// <param name="slProyecto"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerProyectosACerrar(string slProyecto)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@tbl_psn", SqlDbType.Structured, SUPER.Capa_Negocio.Utilidades.GetDataTableFromListCod(slProyecto,",")),
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_GETCIERRE_LISTAPROY", 600, aParam);
        }

        public static SqlDataReader ObtenerProyectosACerrarADM(int t325_anomes, string sResponsables, string sSubnodos, 
                                                                string sPSN, bool bComparacionLogica)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[0].Value = t325_anomes;
            aParam[1] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[1].Value = sResponsables;
            aParam[2] = new SqlParameter("@sSubnodos", SqlDbType.VarChar, 8000);
            aParam[2].Value = sSubnodos;
            aParam[3] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[3].Value = sPSN;
            aParam[4] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[4].Value = bComparacionLogica;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETCIERREPROY_ADM", 600, aParam);
        }
        public static SqlDataReader ObtenerProyectosNoCerrados(int nUsuario, int AnnoMes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@usuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = AnnoMes;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYNOCERRADOS", 300, aParam);
        }
        public static SqlDataReader GetGestionados(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_GESTIONADOS", aParam);
        }
        public static SqlDataReader ObtenerReplicasDeProyecto(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            //return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETREPLICASPROYECTO", aParam);
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETREPLICASPROYECTO", aParam);
        }
        public static DataSet ObtenerMesesAGenerar(SqlTransaction tr, int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@anomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;

            return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETMESESAGENERARPSN", aParam);
        }
        public static int BorrarGeneracionesAnteriores(SqlTransaction tr, int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@anomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_BORRAGENANT", aParam);
        }
        public static bool EsNecesarioGenerarAReplicado(SqlTransaction tr, int nSegMesProy, int nNodoReplica)
        {
            bool bResp = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nSegMesProy", SqlDbType.Int, 4);
            aParam[0].Value = nSegMesProy;
            aParam[1] = new SqlParameter("@nNodoReplica", SqlDbType.Int, 4);
            aParam[1].Value = nNodoReplica;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GETNECESIDADGENMES", aParam);

            if (dr.Read())
            {
                bResp = true;
            }

            dr.Close();
            dr.Dispose();

            return bResp;
        }
        public static string ObtenerProduccionPAC()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sTitle = "Producción de proyectos asociados al contrato";
            bool sw = false;

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 580px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:100px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ObtenerProduccionPAC(null,
                int.Parse(HttpContext.Current.Session["ID_PROYECTOSUBNODO"].ToString()),
                int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()),
                SUPER.Capa_Negocio.Utilidades.EsAdminProduccion(),
                (HttpContext.Current.Session["MONEDA_VDP"] != null) ? HttpContext.Current.Session["MONEDA_VDP"].ToString() : HttpContext.Current.Session["MONEDA_PROYECTOSUBNODO"].ToString());

            while (dr.Read())
            {
                if (!sw)
                {
                    sTitle = "Producción de proyectos de categoría " + ((dr["t301_categoria"].ToString() == "P") ? "producto" : "servicio") + " asociados al contrato";
                }
                sb.Append("<tr id=\"" + dr["t305_idproyectosubnodo"].ToString() + "\" ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("vision='" + dr["vision"].ToString() + "' ");
                sb.Append("ML='" + dr["modo_lectura"].ToString() + "' ");
                sb.Append("moneda='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;' >" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                string sTooltip = "<label class=W70>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label class=W70>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39);
                if (Utilidades.EstructuraActiva("SN4"))
                    sTooltip += "<br><label class=W70>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label>" + dr["t394_denominacion"].ToString().Replace((char)34, (char)39);
                if (Utilidades.EstructuraActiva("SN3"))
                    sTooltip += "<br><label class=W70>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label>" + dr["t393_denominacion"].ToString().Replace((char)34, (char)39);
                if (Utilidades.EstructuraActiva("SN2"))
                    sTooltip += "<br><label class=W70>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label>" + dr["t392_denominacion"].ToString().Replace((char)34, (char)39);
                if (Utilidades.EstructuraActiva("SN1"))
                    sTooltip += "<br><label class=W70>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label>" + dr["t391_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label class=W70>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label class=W70>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39);

                sb.Append("<td><nobr class='NBR W350' ");
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\")\' onMouseout=\"hideTTE()\" ");
                sb.Append(">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + double.Parse(dr["TPPAC"].ToString()).ToString("N") + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString() + "@#@" + sTitle;
        }
        public static int ObtenerPrimerMesAbierto(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GETPRIMERMESABIERTO", aParam));

        }
        public static int ObtenerUltimoMes(SqlTransaction tr, int nPSN)
        {
            int nResul = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETULTIMOMES", aParam);
            if (dr.Read())
            {
                nResul = int.Parse(dr["t325_anomes"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return nResul;
        }
        public static void BorrarPTByPSN(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PST_DBy_PSN", aParam);
        }

        public static int InsertGeneral(SqlTransaction tr, int t301_idproyecto, int t304_idsubnodo, bool t305_finalizado, string t305_cualidad,
                    bool t305_heredanodo, int t314_idusuario_responsable, string t305_seudonimo, string t305_accesobitacora_iap,
                    string t305_accesobitacora_pst, bool t305_imputablegasvi, bool t305_admiterecursospst, bool t305_avisoresponsablepst,
                    bool t305_avisorecursopst, bool t305_avisofigura, Nullable<int> t001_ficepi_visador, bool t305_supervisor_visador,
                    Nullable<int> t476_idcnp, Nullable<int> t485_idcsn1p, Nullable<int> t487_idcsn2p, Nullable<int> t489_idcsn3p,
                    Nullable<int> t491_idcsn4p, byte t305_importaciongasvi, string t422_idmoneda, bool t305_opd, Nullable<int> t001_idficepi_visadorcv,
                    Nullable<int> t001_idficepi_interlocutor)
        {
            SqlParameter[] aParam = new SqlParameter[26];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t304_idsubnodo;
            aParam[2] = new SqlParameter("@t305_finalizado", SqlDbType.Bit, 1);
            aParam[2].Value = t305_finalizado;
            aParam[3] = new SqlParameter("@t305_cualidad", SqlDbType.Text, 1);
            aParam[3].Value = t305_cualidad;
            aParam[4] = new SqlParameter("@t305_heredanodo", SqlDbType.Bit, 1);
            aParam[4].Value = t305_heredanodo;
            aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_responsable;
            aParam[6] = new SqlParameter("@t305_seudonimo", SqlDbType.Text, 70);
            aParam[6].Value = t305_seudonimo;
            aParam[7] = new SqlParameter("@t305_accesobitacora_iap", SqlDbType.Text, 1);
            aParam[7].Value = t305_accesobitacora_iap;
            aParam[8] = new SqlParameter("@t305_accesobitacora_pst", SqlDbType.Text, 1);
            aParam[8].Value = t305_accesobitacora_pst;
            aParam[9] = new SqlParameter("@t305_imputablegasvi", SqlDbType.Bit, 1);
            aParam[9].Value = t305_imputablegasvi;
            aParam[10] = new SqlParameter("@t305_admiterecursospst", SqlDbType.Bit, 1);
            aParam[10].Value = t305_admiterecursospst;
            aParam[11] = new SqlParameter("@t305_avisoresponsablepst", SqlDbType.Bit, 1);
            aParam[11].Value = t305_avisoresponsablepst;
            aParam[12] = new SqlParameter("@t305_avisorecursopst", SqlDbType.Bit, 1);
            aParam[12].Value = t305_avisorecursopst;
            aParam[13] = new SqlParameter("@t305_avisofigura", SqlDbType.Bit, 1);
            aParam[13].Value = t305_avisofigura;
            aParam[14] = new SqlParameter("@t001_ficepi_visador", SqlDbType.Int, 4);
            aParam[14].Value = t001_ficepi_visador;
            aParam[15] = new SqlParameter("@t305_supervisor_visador", SqlDbType.Bit, 1);
            aParam[15].Value = t305_supervisor_visador;
            aParam[16] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[16].Value = t476_idcnp;
            aParam[17] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[17].Value = t485_idcsn1p;
            aParam[18] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[18].Value = t487_idcsn2p;
            aParam[19] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[19].Value = t489_idcsn3p;
            aParam[20] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[20].Value = t491_idcsn4p;
            aParam[21] = new SqlParameter("@t305_importaciongasvi", SqlDbType.TinyInt, 1);
            aParam[21].Value = t305_importaciongasvi;
            aParam[22] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[22].Value = t422_idmoneda;
            aParam[23] = new SqlParameter("@t305_opd", SqlDbType.Bit, 1);
            aParam[23].Value = t305_opd;
            aParam[24] = new SqlParameter("@t001_idficepi_visadorcv", SqlDbType.Int, 4);
            aParam[24].Value = t001_idficepi_visadorcv;
            aParam[25] = new SqlParameter("@t001_idficepi_interlocutor", SqlDbType.Int, 4);
            aParam[25].Value = t001_idficepi_interlocutor;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROYECTOSUBNODO_IGEN", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOSUBNODO_IGEN", aParam));
        }

        public static int UpdateGeneral(SqlTransaction tr, int t305_idproyectosubnodo, int t301_idproyecto, int t304_idsubnodo,
                    bool t305_finalizado, string t305_cualidad, bool t305_heredanodo, int t314_idusuario_responsable,
                    string t305_seudonimo, string t305_accesobitacora_iap, string t305_accesobitacora_pst,
                    bool t305_imputablegasvi, bool t305_admiterecursospst, bool t305_avisoresponsablepst,
                    bool t305_avisorecursopst, bool t305_avisofigura, Nullable<int> t001_ficepi_visador,
                    bool t305_supervisor_visador, Nullable<int> t476_idcnp, Nullable<int> t485_idcsn1p,
                    Nullable<int> t487_idcsn2p, Nullable<int> t489_idcsn3p, Nullable<int> t491_idcsn4p,
                    byte t305_importaciongasvi, string t422_idmoneda, bool t305_opd, Nullable<int> t001_idficepi_visadorcv,
                    Nullable<int> t001_idficepi_interlocutor, Nullable<int> t001_idficepi_interlocalertasocfa)
        {
            SqlParameter[] aParam = new SqlParameter[28];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[1].Value = t301_idproyecto;
            aParam[2] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[2].Value = t304_idsubnodo;
            aParam[3] = new SqlParameter("@t305_finalizado", SqlDbType.Bit, 1);
            aParam[3].Value = t305_finalizado;
            aParam[4] = new SqlParameter("@t305_cualidad", SqlDbType.Text, 1);
            aParam[4].Value = t305_cualidad;
            aParam[5] = new SqlParameter("@t305_heredanodo", SqlDbType.Bit, 1);
            aParam[5].Value = t305_heredanodo;
            aParam[6] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[6].Value = t314_idusuario_responsable;
            aParam[7] = new SqlParameter("@t305_seudonimo", SqlDbType.Text, 70);
            aParam[7].Value = t305_seudonimo;
            aParam[8] = new SqlParameter("@t305_accesobitacora_iap", SqlDbType.Text, 1);
            aParam[8].Value = t305_accesobitacora_iap;
            aParam[9] = new SqlParameter("@t305_accesobitacora_pst", SqlDbType.Text, 1);
            aParam[9].Value = t305_accesobitacora_pst;
            aParam[10] = new SqlParameter("@t305_imputablegasvi", SqlDbType.Bit, 1);
            aParam[10].Value = t305_imputablegasvi;
            aParam[11] = new SqlParameter("@t305_admiterecursospst", SqlDbType.Bit, 1);
            aParam[11].Value = t305_admiterecursospst;
            aParam[12] = new SqlParameter("@t305_avisoresponsablepst", SqlDbType.Bit, 1);
            aParam[12].Value = t305_avisoresponsablepst;
            aParam[13] = new SqlParameter("@t305_avisorecursopst", SqlDbType.Bit, 1);
            aParam[13].Value = t305_avisorecursopst;
            aParam[14] = new SqlParameter("@t305_avisofigura", SqlDbType.Bit, 1);
            aParam[14].Value = t305_avisofigura;
            aParam[15] = new SqlParameter("@t001_ficepi_visador", SqlDbType.Int, 4);
            aParam[15].Value = t001_ficepi_visador;
            aParam[16] = new SqlParameter("@t305_supervisor_visador", SqlDbType.Bit, 1);
            aParam[16].Value = t305_supervisor_visador;
            aParam[17] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[17].Value = t476_idcnp;
            aParam[18] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[18].Value = t485_idcsn1p;
            aParam[19] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[19].Value = t487_idcsn2p;
            aParam[20] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[20].Value = t489_idcsn3p;
            aParam[21] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[21].Value = t491_idcsn4p;
            aParam[22] = new SqlParameter("@t305_importaciongasvi", SqlDbType.TinyInt, 1);
            aParam[22].Value = t305_importaciongasvi;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;
            aParam[24] = new SqlParameter("@t305_opd", SqlDbType.Bit, 1);
            aParam[24].Value = t305_opd;
            aParam[25] = new SqlParameter("@t001_idficepi_visadorcv", SqlDbType.Int, 4);
            aParam[25].Value = t001_idficepi_visadorcv; 
            aParam[26] = new SqlParameter("@t001_idficepi_interlocutor", SqlDbType.Int, 4);
            aParam[26].Value = t001_idficepi_interlocutor;
            aParam[27] = new SqlParameter("@t001_idficepi_interlocalertasocfa", SqlDbType.Int, 4);
            aParam[27].Value = t001_idficepi_interlocalertasocfa;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTOSUBNODO_UGEN", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_UGEN", aParam);
        }

        public static int UpdateAnotaciones(SqlTransaction tr, int nPSN, string sModificaciones, string sObservaciones, string sObservacionesadm)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@t305_modificaciones", SqlDbType.Text, 2147483647);
            aParam[1].Value = sModificaciones;
            aParam[2] = new SqlParameter("@t305_observaciones", SqlDbType.Text, 2147483647);
            aParam[2].Value = sObservaciones;
            aParam[3] = new SqlParameter("@t305_observacionesadm", SqlDbType.Text, 2147483647);
            aParam[3].Value = sObservacionesadm;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_UANOT", aParam);
        }
        public static int UpdateReasignacion(SqlTransaction tr, int nPSN, int t314_idusuario_responsable, int t304_idsubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_responsable;
            aParam[2] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[2].Value = t304_idsubnodo;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_UREASIG", aParam);
        }
        public static int UpdateReasignacion(SqlTransaction tr, int nPSN, int t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_responsable;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_RES_UREASIG", aParam);
        }
        public static SqlDataReader ObtenerAvanceEconomico(int t314_idusuario, int nDesde, int nHasta, Nullable<int> nNivelEstructura,
                                                           string t301_estado, string t301_categoria, string t305_cualidad, string sClientes,
                                                           string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                                           string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                                           bool bComparacionLogica, string sCNP, string sCSN1P, string sCSN2P,
                                                           string sCSN3P, string sCSN4P, string sPSN, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[24];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@nNivelEstructura", SqlDbType.TinyInt, 2, nNivelEstructura);
            aParam[i++] = ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado);
            aParam[i++] = ParametroSql.add("@t301_categoria", SqlDbType.Char, 1, t301_categoria);
            aParam[i++] = ParametroSql.add("@t305_cualidad", SqlDbType.Char, 1, t305_cualidad);
            aParam[i++] = ParametroSql.add("@sClientes", SqlDbType.VarChar, 8000, sClientes);
            aParam[i++] = ParametroSql.add("@sResponsables", SqlDbType.VarChar, 8000, sResponsables);
            aParam[i++] = ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas);
            aParam[i++] = ParametroSql.add("@sHorizontal", SqlDbType.VarChar, 8000, sHorizontal);
            aParam[i++] = ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato);
            aParam[i++] = ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato);
            aParam[i++] = ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura);
            aParam[i++] = ParametroSql.add("@sSectores", SqlDbType.VarChar, 8000, sSectores);
            aParam[i++] = ParametroSql.add("@sSegmentos", SqlDbType.VarChar, 8000, sSegmentos);
            aParam[i++] = ParametroSql.add("@bComparacionLogica", SqlDbType.Bit, 1, bComparacionLogica);
            aParam[i++] = ParametroSql.add("@sCNP", SqlDbType.VarChar, 8000, sCNP);
            aParam[i++] = ParametroSql.add("@sCSN1P", SqlDbType.VarChar, 8000, sCSN1P);
            aParam[i++] = ParametroSql.add("@sCSN2P", SqlDbType.VarChar, 8000, sCSN2P);
            aParam[i++] = ParametroSql.add("@sCSN3P", SqlDbType.VarChar, 8000, sCSN3P);
            aParam[i++] = ParametroSql.add("@sCSN4P", SqlDbType.VarChar, 8000, sCSN4P);
            aParam[i++] = ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_GETAVANCEECO_ADMIN" : "SUP_GETAVANCEECO_7AM", aParam);//SUP_GETAVANCEECO_ADMIN_7AM
            else
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_GETAVANCEECO" : "SUP_GETAVANCEECO_7AM", aParam);
        }

        public static SqlDataReader ObtenerAvancePSN(int t305_idproyectosubnodo, int nAnomes, int nUsuario, string t422_idmoneda, bool bRTPT, string sNivelPresupuesto)
        {
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nPSN", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nAnomes", SqlDbType.Int, 4, nAnomes);
            aParam[i++] = ParametroSql.add("@nUsuario", SqlDbType.Int, 4, nUsuario);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@bRTPT", SqlDbType.Bit, 1, bRTPT); 

          
            switch (sNivelPresupuesto)
            {
                case "P":
                    //dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_PT", aParam);
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PSN_PT", aParam);
                    break;
                case "F":
                    //dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_F", aParam);
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PSN_F", aParam);
                    break;
                case "A":
                    //dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_A", aParam);
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PSN_A", aParam);
                    break;
                case "T":
                    //dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_T", aParam);
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PSN_T", aParam);
                    break;
            }

            return dr;

            //return SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PSN", aParam);
        }
        public static SqlDataReader ObtenerAvancePT(int t305_idproyectosubnodo, int t331_idpt, int nAnomes, byte nCerradas, string t422_idmoneda, string sNivelPresupuesto)
        {
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nPSN", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nPT", SqlDbType.Int, 4, t331_idpt);
            aParam[i++] = ParametroSql.add("@nAnomes", SqlDbType.Int, 4, nAnomes);
            aParam[i++] = ParametroSql.add("@bCerradas", SqlDbType.Bit, 1, nCerradas);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            switch (sNivelPresupuesto)
            {
                case "P":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PT_PT", aParam);
                    break;
                case "F":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PT_F", aParam);
                    break;
                case "A":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PT_A", aParam);
                    break;
                case "T":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PT_T", aParam);
                    break;
            }
            return dr;
            //return SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_PT", aParam);
        }
        public static SqlDataReader ObtenerAvanceTarea(int t332_idtarea, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nT", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCE_T", aParam);
        }

        public static SqlDataReader ObtenerAvanceTecnico(int t314_idusuario, int nDesde, int nHasta, Nullable<int> nNivelEstructura,
                                                           string t301_estado, string t301_categoria, string t305_cualidad, string sClientes,
                                                           string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                                           string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                                           bool bComparacionLogica, string sCNP, string sCSN1P, string sCSN2P,
                                                           string sCSN3P, string sCSN4P, string sPSN, string t422_idmoneda)
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
            aParam[4] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[4].Value = t301_estado;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
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
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCEPST_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_GETAVANCEPST", aParam);
        }

        public static SqlDataReader ObtenerResumenEconomicoProyecto(int t314_idusuario,
                int nDesde,
                int nHasta,
                Nullable<int> nNivelEstructura,
                string t301_estado,
                string t301_categoria,
                string t305_cualidad,
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
                string sPSN,
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
            aParam[4] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[4].Value = t301_estado;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
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
            aParam[22] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[22].Value = sPSN;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_RESUMENECOPROY_ADMIN" : "SUP_RESUMENECOPROY_7AM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader(((bool)HttpContext.Current.Session["CALCULOONLINE"]) ? "SUP_RESUMENECOPROY" : "SUP_RESUMENECOPROY_7AM", aParam);
        }

        public static int NumTareasAcumuladoSuperiorPrevision(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_COMPACUMPREV", aParam));
        }
        public static int ObtenerUltCierreEcoNodoPSN(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ULTCIERREECO_NODOPSN", aParam));
        }
        public static int GetNodo(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            if (tr != null)
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GET_NODO_PSN", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GET_NODO_PSN", aParam));
        }

        public static bool ExisteProyectoSubNodo(SqlTransaction tr, int t301_idproyecto, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTEPROYECTOSUBNODO", 120, aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTEPROYECTOSUBNODO", 120, aParam)) == 0) ? false : true;

        }
        public static SqlDataReader ObtenerProyectosCambioEstructura(string sOpcion, string sValor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
            aParam[0].Value = sOpcion;
            aParam[1] = new SqlParameter("@sValor", SqlDbType.VarChar, 8000);
            aParam[1].Value = sValor;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURA_PROYECTO_CAT", aParam);
        }
        public static SqlDataReader ObtenerProyectosCambioEstructuraParesDatos(string sParesDatos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sParesDatos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sParesDatos;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAPSN_LISTA_CAT", aParam);
        }
        public static SqlDataReader ObtenerProyectosReasignacion(string sOpcion, Nullable<int> t303_idnodo, string sValor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
            aParam[0].Value = sOpcion;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@sValor", SqlDbType.VarChar, 8000);
            aParam[2].Value = sValor;

            return SqlHelper.ExecuteSqlDataReader("SUP_REASIGNACION_PROYECTO_CAT", aParam);
        }
        /// <summary>
        /// Dada una lista de t305_idproyectosubnodos devuelve sus datos
        /// </summary>
        /// <param name="sOpcion"></param>
        /// <param name="t303_idnodo"></param>
        /// <param name="sValor"></param>
        /// <returns></returns>
        public static SqlDataReader GetPSNReasignar(string sValor)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sValor", SqlDbType.VarChar, 8000);
            aParam[0].Value = sValor;

            return SqlHelper.ExecuteSqlDataReader("SUP_PSN_LISTA_CAT", aParam);
        }

        public static SqlDataReader ObtenerProyectosReasignacionMultiSubnodo( string sIdSubNodo, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIdSubNodo", SqlDbType.VarChar, 8000);
            aParam[0].Value = sIdSubNodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_REASIGNACION_SUBNODO_MULTI_PROYECTO_CAT", aParam);
        }

        public static SqlDataReader ObtenerProyectosUSA(int nIdUser, bool bSAT, bool bSAA, int nIdUserSol)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdUser", SqlDbType.Int, 4);
            aParam[0].Value = nIdUser;
            aParam[1] = new SqlParameter("@bSAT", SqlDbType.Bit, 1);
            aParam[1].Value = bSAT;
            aParam[2] = new SqlParameter("@bSAA", SqlDbType.Bit, 1);
            aParam[2].Value = bSAA;
            aParam[3] = new SqlParameter("@nIdUserSol", SqlDbType.Int, 4);
            aParam[3].Value = nIdUserSol;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_USA_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_USA", aParam);
        }

        public static SqlDataReader ObtenerProyectosRelacionUSA(int t314_idusuario, bool bSAT, bool bSAA, bool bExternalizable, bool bExternalizado)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bSAT", SqlDbType.Bit, 1, bSAT);
            aParam[i++] = ParametroSql.add("@bSAA", SqlDbType.Bit, 1, bSAA);
            aParam[i++] = ParametroSql.add("@bExternalizable", SqlDbType.Bit, 1, bExternalizable);
            aParam[i++] = ParametroSql.add("@bExternalizado", SqlDbType.Bit, 1, bExternalizado);

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_RELACIONUSA", aParam);
        }
        public static SqlDataReader ObtenerInformeUSAExcel(string sProyectos, int anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sProyectos", SqlDbType.VarChar, 8000, sProyectos);
            aParam[i++] = ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes);

            return SqlHelper.ExecuteSqlDataReader("SUP_INFORMEMENSUALUSA_EXCEL", aParam);
        }

        public static SqlDataReader ObtenerReplicasCambioEstructura(int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSUBNODOREPLICA_SBy_Contratante", aParam);
        }
        public static SqlDataReader ObtenerContratantesCambioEstructura(int t306_idcontrato, string tipo_arrastre)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@tipo_arrastre", SqlDbType.Char, 1);
            aParam[1].Value = tipo_arrastre;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSUBNODOCONTRATANTE_SBy_Contrato", aParam);
        }
        public static DataSet ObtenerContratantesCambioEstructuraDS(SqlTransaction tr, int t306_idcontrato, string tipo_arrastre)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@tipo_arrastre", SqlDbType.Char, 1);
            aParam[1].Value = tipo_arrastre;

            return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_PROYECTOSUBNODOCONTRATANTE_SBy_Contrato", aParam);
        }
        public static DataSet ObtenerSegMesGastosFinancierosDS(SqlTransaction tr, int nAnomes, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sMesValor", SqlDbType.Int, 4);
            aParam[0].Value = nAnomes;
            aParam[1] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[1].Value = sNodos;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GASTOSFINANCIEROSCALCULO_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GASTOSFINANCIEROSCALCULO_CAT", aParam);
        }

        public static bool ExistePIG(SqlTransaction tr, int t303_idnodo, int t323_idnaturaleza, short t301_annoPIG)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
            aParam[1].Value = @t323_idnaturaleza;
            aParam[2] = new SqlParameter("@t301_annoPIG", SqlDbType.SmallInt, 2);
            aParam[2].Value = t301_annoPIG;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NUMPSNPIG_SBy_Nodo", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NUMPSNPIG_SBy_Nodo", aParam)) == 0) ? false : true;
        }

        public static SqlDataReader ObtenerCatalogoPeriodificacion(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_PERIODIFICACION_CAT", aParam);
        }

        public static SqlDataReader ObtenerProyectosConMesesAbiertos(int t303_idnodo, int t303_ultcierreeco)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t303_ultcierreeco", SqlDbType.Int, 4);
            aParam[1].Value = t303_ultcierreeco;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MESA_NODO", aParam);
        }

        public static SqlDataReader ObtenerProyectosConObraEnCurso(int t303_idnodo, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[0].Value = nAnno;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_OBRAENCURSO_NODO", aParam);
        }
        public static SqlDataReader ObtenerProyectosConObraEnCursoNodoDotacion(int t303_idnodo, int nAnnoMes, int nMeses)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nAnnoMes", SqlDbType.Int, 4);
            aParam[0].Value = nAnnoMes;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@nMeses", SqlDbType.Int, 4);
            aParam[2].Value = nMeses;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_OBRAENCURSO_NODO_DOTACION", aParam);
        }
        public static DataSet ObtenerProyectosObraEnCurso(SqlTransaction tr, int nAnno, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[0].Value = nAnno;
            aParam[1] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[1].Value = sNodos;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GETPROYECTOS_OBRAENCURSO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETPROYECTOS_OBRAENCURSO", aParam);
        }
        public static DataSet ObtenerProyectosObraEnCursoDotacion(SqlTransaction tr, int nAnnoMes, int nMeses, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nAnnoMes", SqlDbType.Int, 4);
            aParam[0].Value = nAnnoMes;
            aParam[1] = new SqlParameter("@nMeses", SqlDbType.Int, 4);
            aParam[1].Value = nMeses;
            aParam[2] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[2].Value = sNodos;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GETPROYECTOS_OBRAENCURSO_DOTACION", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETPROYECTOS_OBRAENCURSO_DOTACION", aParam);
        }
        public static void EliminarObraEnCurso(SqlTransaction tr, int nAnno, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[0].Value = nAnno;
            aParam[1] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[1].Value = sNodos;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_OBRAENCURSO_DEL", 120, aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OBRAENCURSO_DEL", 120, aParam);
        }
        public static void EliminarObraEnCursoDotacion(SqlTransaction tr, int nAnnoMes, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnnoMes", SqlDbType.Int, 4);
            aParam[0].Value = nAnnoMes;
            aParam[1] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[1].Value = sNodos;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_OBRAENCURSO_DOTACION_DEL", 120, aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OBRAENCURSO_DOTACION_DEL", 120, aParam);
        }
        public static bool TieneConsumos(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTO_TIENE_CONSUMOS", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTO_TIENE_CONSUMOS", aParam);

            if (dr.Read())
            {
                bRes = true;
            }
            dr.Close();
            dr.Dispose();

            return bRes;
        }
        public static bool TieneApuntesGasvi(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTO_TIENE_GASVI", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTO_TIENE_GASVI", aParam);

            if (dr.Read())
            {
                bRes = true;
            }
            dr.Close();
            dr.Dispose();

            return bRes;
        }
        /// <summary>
        /// Comprueba si un proyectosubnodo es modificable en función del perfil del recurso que lo está accediendo
        /// </summary>
        public static string getAcceso(SqlTransaction tr, int nIdPSN, int iUser)
        {
            string sRes = "N";
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nIdPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                sRes = "W";
            else
            {
                object obj;
                if (tr == null)
                    obj = SqlHelper.ExecuteScalar("SUP_PERMISO_PE", aParam);
                else
                    obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERMISO_PE", aParam);
                if (obj != null)
                {
                    if ((bool)obj) sRes = "R";
                    else sRes = "W";
                }
            }
            return sRes;
        }
        /// <summary>
        /// Obtiene el estado del proyecto que engloba el proyectosubodo que se le pasa como parametro
        /// </summary>
        public static string getEstado(SqlTransaction tr, int nIdPSN)
        {
            string sRes = "C";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdPSN;
            object obj;
            if (tr == null)
                obj = SqlHelper.ExecuteScalar("SUP_PROYECTOSUBNODO_ESTADO", aParam);
            else
                obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOSUBNODO_ESTADO", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        public static SqlDataReader ObtenerUsuariosConVision(string sPSN, string sUsuarios)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[0].Value = sPSN;
            aParam[1] = new SqlParameter("@sUsuarios", SqlDbType.VarChar, 8000);
            aParam[1].Value = sUsuarios;

            return SqlHelper.ExecuteSqlDataReader("SUP_VISIONPSN_USUARIOS", aParam);
        }
        /// <summary>
        /// El nivel nos indica si la lista es de codigos de subnodo, nodo, SNN1, SNN2, SNN3 o SNN4
        /// Devuelve la lista de ProyectosSubnodo que cuelgan de la lista indicada
        /// </summary>
        public static SqlDataReader Proyectos_Estructura(string sNivel, string sListaCodigos)
        {
            string sProcAlm = "";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sCodigos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sListaCodigos;
            switch (sNivel)
            {
                case "1":
                    sProcAlm = "SUP_GETPROYECTOSxSUBNODO";
                    break;
                case "2":
                    sProcAlm = "SUP_GETPROYECTOSxNODO";
                    break;
                case "3":
                    sProcAlm = "SUP_GETPROYECTOSxSNN1";
                    break;
                case "4":
                    sProcAlm = "SUP_GETPROYECTOSxSNN2";
                    break;
                case "5":
                    sProcAlm = "SUP_GETPROYECTOSxSNN3";
                    break;
                case "6":
                    sProcAlm = "SUP_GETPROYECTOSxSNN4";
                    break;
            }

            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }

        public static void Duplicar(SqlTransaction tr, int iPSN_Origen, int iPSN_Destino, int iPE_Origen, int iPE_Destino,
                                    bool bBitacoraPT, bool bBitacoraTA, bool bHitos, string sEstadosTarea, int t314_idusuario, string sAccDoc, bool bCopiaAE)
        {
            string sError = "";
            //Nos indican que la bitácora de PE no se debe copiar (dejamos el código por si acaso)
            bool bBitacoraPE = false;
            switch (sAccDoc)
            {
                case "N"://No copia documentos
                    SUPER.DAL.PROYECTOSUBNODO.Duplicar(tr, iPSN_Origen, iPSN_Destino, iPE_Origen, iPE_Destino,
                                                       bBitacoraPT, bBitacoraTA, bHitos, sEstadosTarea, t314_idusuario, false, bCopiaAE);
                    break;
                case "M"://Mantiene el mismo id de Atenea para los nuevos registros de documentos
                    SUPER.DAL.PROYECTOSUBNODO.Duplicar(tr, iPSN_Origen, iPSN_Destino, iPE_Origen, iPE_Destino,
                                                       bBitacoraPT, bBitacoraTA, bHitos, sEstadosTarea, t314_idusuario, true, bCopiaAE);
                    break;
                case "G"://Genera una nueva copia del documento en Atenea
                    try
                    {
                        #region Duplicar estructura con copia de documentos
                        //Colección con el Id Atenea original y el Id de la tabla de documento
                        Dictionary<long, int> dictOri = new Dictionary<long, int>();
                        //Colección con el Id Atenea original y el Id de Atenea Destino
                        Dictionary<long, long> dictDest = new Dictionary<long, long>();

                        //1º genera toda la estructura nueva
                        SUPER.DAL.PROYECTOSUBNODO.Duplicar(tr, iPSN_Origen, iPSN_Destino, iPE_Origen, iPE_Destino,
                                                           bBitacoraPT, bBitacoraTA, bHitos, sEstadosTarea, t314_idusuario, true, bCopiaAE);
                        //2º revisa todos los elementos que tengan documento (t2_iddocumento!=null) y obtiene
                        //un nuevo identificador con el que se updateará el registro
                        long idContentServer;
                        #region Proyecto Económico
                        dictOri.Clear();
                        dictDest.Clear();
                        //Obtengo la lista de documentos asociados al nuevo PE (solo cojo registros con t2_iddocumento!=null)
                        SqlDataReader dr1 = SUPER.DAL.DocuPE.Catalogo(tr, iPSN_Destino);
                        while (dr1.Read())
                        {
                            dictOri.Add(long.Parse(dr1["t2_iddocumento"].ToString()), int.Parse(dr1["t368_iddocupe"].ToString()));
                        }
                        dr1.Close();
                        dr1.Dispose();
                        //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                        long[] aIdDocOri1 = new long[dictOri.Count];
                        int i = 0;
                        foreach (KeyValuePair<long, int> item in dictOri)
                        {
                            aIdDocOri1[i++] = item.Key;
                        }
                        if (i > 0)
                        {
                            //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                            dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri1);

                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                idContentServer = dictDest[item.Key];
                                if (idContentServer > 0)
                                {
                                    //Updateo la tabla con la referencia al nuevo documento en Atenea
                                    SUPER.DAL.DocuPE.UpdateIdDoc(tr, item.Value, idContentServer);
                                }
                                else
                                {
                                    sError = "El servicio de copia del documento de Proyecto Económico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                    throw (new NullReferenceException(sError));
                                }
                            }
                        }
                        #endregion
                        #region Proyecto Técnico
                        dictOri.Clear();
                        dictDest.Clear();
                        //Obtengo la lista de documentos asociados a los nuevos PT (solo cojo registros con t2_iddocumento!=null)
                        SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ListaPTconDoc(tr, iPSN_Destino);
                        while (dr.Read())
                        {
                            dictOri.Add(long.Parse(dr["t2_iddocumento"].ToString()), int.Parse(dr["t362_iddocupt"].ToString()));
                        }
                        dr.Close();
                        dr.Dispose();
                        //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                        long[] aIdDocOri2 = new long[dictOri.Count];
                        i = 0;
                        foreach (KeyValuePair<long, int> item in dictOri)
                        {
                            aIdDocOri2[i++] = item.Key;
                        }
                        //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                        if (i > 0)
                        {
                            dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri2);

                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                idContentServer = dictDest[item.Key];
                                if (idContentServer > 0)
                                {
                                    //Updateo la tabla con la referencia al nuevo documento en Atenea
                                    SUPER.Capa_Negocio.DOCUPT.UpdateIdDoc(tr, item.Value, idContentServer);
                                }
                                else
                                {
                                    sError = "El servicio de copia del documento de Proyecto Técnico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                    throw (new NullReferenceException(sError));
                                }
                            }
                        }
                        #endregion
                        #region Fase
                        dictOri.Clear();
                        dictDest.Clear();
                        //Obtengo la lista de documentos asociados a las nuevas Fases (solo cojo registros con t2_iddocumento!=null)
                        SqlDataReader dr2 = SUPER.DAL.DocuF.ListaPSN(tr, iPSN_Destino);
                        while (dr2.Read())
                        {
                            dictOri.Add(long.Parse(dr2["t2_iddocumento"].ToString()), int.Parse(dr2["t364_iddocuf"].ToString()));
                        }
                        dr2.Close();
                        dr2.Dispose();
                        //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                        long[] aIdDocOri3 = new long[dictOri.Count];
                        i = 0;
                        foreach (KeyValuePair<long, int> item in dictOri)
                        {
                            aIdDocOri3[i++] = item.Key;
                        }
                        if (i > 0)
                        {
                            //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                            dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri3);

                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                idContentServer = dictDest[item.Key];
                                if (idContentServer > 0)
                                {
                                    //Updateo la tabla con la referencia al nuevo documento en Atenea
                                    SUPER.DAL.DocuF.UpdateIdDoc(tr, item.Value, idContentServer);
                                }
                                else
                                {
                                    sError = "El servicio de copia del documento de Fase " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                    throw (new NullReferenceException(sError));
                                }
                            }
                        }
                        #endregion
                        #region Actividad
                        dictOri.Clear();
                        dictDest.Clear();
                        //Obtengo la lista de documentos asociados a las nuevas Actividades (solo cojo registros con t2_iddocumento!=null)
                        SqlDataReader dr3 = SUPER.DAL.DocuA.ListaPSN(tr, iPSN_Destino);
                        while (dr3.Read())
                        {
                            dictOri.Add(long.Parse(dr3["t2_iddocumento"].ToString()), int.Parse(dr3["t365_iddocua"].ToString()));
                        }
                        dr3.Close();
                        dr3.Dispose();
                        //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                        long[] aIdDocOri4 = new long[dictOri.Count];
                        i = 0;
                        foreach (KeyValuePair<long, int> item in dictOri)
                        {
                            aIdDocOri4[i++] = item.Key;
                        }
                        if (i > 0)
                        {
                            //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                            dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri4);

                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                idContentServer = dictDest[item.Key];
                                if (idContentServer > 0)
                                {
                                    //Updateo la tabla con la referencia al nuevo documento en Atenea
                                    SUPER.DAL.DocuA.UpdateIdDoc(tr, item.Value, idContentServer);
                                }
                                else
                                {
                                    sError = "El servicio de copia del documento de Actividad " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                    throw (new NullReferenceException(sError));
                                }
                            }
                        }
                        #endregion
                        #region Tarea
                        dictOri.Clear();
                        dictDest.Clear();
                        //Obtengo la lista de documentos asociados a las nuevas Tareas (solo cojo registros con t2_iddocumento!=null)
                        SqlDataReader dr4 = SUPER.DAL.DocuT.ListaPSN(tr, iPSN_Destino);
                        while (dr4.Read())
                        {
                            dictOri.Add(long.Parse(dr4["t2_iddocumento"].ToString()), int.Parse(dr4["t363_iddocut"].ToString()));
                        }
                        dr4.Close();
                        dr4.Dispose();
                        //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                        long[] aIdDocOri5 = new long[dictOri.Count];
                        i = 0;
                        foreach (KeyValuePair<long, int> item in dictOri)
                        {
                            aIdDocOri5[i++] = item.Key;
                        }
                        if (i > 0)
                        {
                            //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                            dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri5);

                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                idContentServer = dictDest[item.Key];
                                if (idContentServer > 0)
                                {
                                    //Updateo la tabla con la referencia al nuevo documento en Atenea
                                    SUPER.DAL.DocuT.UpdateIdDoc(tr, item.Value, idContentServer);
                                }
                                else
                                {
                                    sError = "El servicio de copia del documento de Tarea " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                    throw (new NullReferenceException(sError));
                                }
                            }
                        }
                        #endregion
                        #region Hito de tarea
                        if (bHitos)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            //Obtengo la lista de documentos asociados a los nuevos Hitos (solo cojo registros con t2_iddocumento!=null)
                            SqlDataReader dr5 = SUPER.DAL.DocuH.ListaPSN(tr, iPSN_Destino);
                            while (dr5.Read())
                            {
                                dictOri.Add(long.Parse(dr5["t2_iddocumento"].ToString()), int.Parse(dr5["t366_iddocuh"].ToString()));
                            }
                            dr5.Close();
                            dr5.Dispose();
                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri6 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri6[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri6);

                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocuH.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Hito de Tarea " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Hitos especiales
                        if (bHitos)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            //Obtengo la lista de documentos asociados a lo nuevos Hitos especiales (solo cojo registros con t2_iddocumento!=null)
                            SqlDataReader dr6 = SUPER.DAL.DocuHE.ListaPSN(tr, iPSN_Destino);
                            while (dr6.Read())
                            {
                                dictOri.Add(long.Parse(dr6["t2_iddocumento"].ToString()), int.Parse(dr6["t367_iddocuhe"].ToString()));
                            }
                            dr6.Close();
                            dr6.Dispose();
                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri7 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri7[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                //La llamada al servicio devuelve una lista de pares de valores con el Id original y el Id Nuevo
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri7);

                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocuHE.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Hito especial " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Proyecto Económico. Asunto de bitácora
                        if (bBitacoraPE)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            SqlDataReader dr7 = SUPER.DAL.DocAsuPE.ListaPSN(tr, iPSN_Destino);
                            while (dr7.Read())
                            {
                                dictOri.Add(long.Parse(dr7["t2_iddocumento"].ToString()), int.Parse(dr7["t386_iddocasu"].ToString()));
                            }
                            dr7.Close();
                            dr7.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri8 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri8[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri8);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocAsuPE.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Asunto de Proyecto Económico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Proyecto Económico. Acción de bitácora
                        if (bBitacoraPE)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            SqlDataReader dr8 = SUPER.DAL.DocAccPE.ListaPSN(tr, iPSN_Destino);
                            while (dr8.Read())
                            {
                                dictOri.Add(long.Parse(dr8["t2_iddocumento"].ToString()), int.Parse(dr8["t387_iddocacc"].ToString()));
                            }
                            dr.Close();
                            dr.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri9 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri9[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri9);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocAccPE.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Acción de Proyecto Económico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Proyecto Técnico. Asuntos de Bitácora
                        if (bBitacoraPT)
                        {
                            //Obtengo la lista de documentos asociados a los nuevos Asuntos de Bitácora de PT (solo cojo registros con t2_iddocumento!=null)
                            dictOri.Clear();
                            dictDest.Clear();
                            dr = SUPER.DAL.PROYECTOSUBNODO.ListaAsuntoPTconDoc(tr, iPSN_Destino);
                            while (dr.Read())
                            {
                                dictOri.Add(long.Parse(dr["t2_iddocumento"].ToString()), int.Parse(dr["t411_iddocasu"].ToString()));
                            }
                            dr.Close();
                            dr.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri10 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri10[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri10);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.Capa_Negocio.DOCASU_PT.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Asunto de Proyecto Técnico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Proyecto Técnico. Acción de bitácora
                        if (bBitacoraPT)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            SqlDataReader dr9 = SUPER.DAL.DocAccPT.ListaPSN(tr, iPSN_Destino);
                            while (dr9.Read())
                            {
                                dictOri.Add(long.Parse(dr9["t2_iddocumento"].ToString()), int.Parse(dr9["t412_iddocacc"].ToString()));
                            }
                            dr9.Close();
                            dr9.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri11 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri11[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri11);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocAccPT.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Acción de Proyecto Técnico " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Tarea. Asunto de bitácora
                        if (bBitacoraTA)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            SqlDataReader dr10 = SUPER.DAL.DocAsuT.ListaPSN(tr, iPSN_Destino);
                            while (dr10.Read())
                            {
                                dictOri.Add(long.Parse(dr10["t2_iddocumento"].ToString()), int.Parse(dr10["t602_iddocasu"].ToString()));
                            }
                            dr10.Close();
                            dr10.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri12 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri12[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri12);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocAsuT.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Asunto de Tarea " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Tarea. Acción de bitácora
                        if (bBitacoraTA)
                        {
                            dictOri.Clear();
                            dictDest.Clear();
                            SqlDataReader dr11 = SUPER.DAL.DocAccT.ListaPSN(tr, iPSN_Destino);
                            while (dr11.Read())
                            {
                                dictOri.Add(long.Parse(dr11["t2_iddocumento"].ToString()), int.Parse(dr11["t603_iddocacc"].ToString()));
                            }
                            dr11.Close();
                            dr11.Dispose();

                            //Creo un array para llamar a Conserva con la lista de índices de documentos a copiar
                            long[] aIdDocOri13 = new long[dictOri.Count];
                            i = 0;
                            foreach (KeyValuePair<long, int> item in dictOri)
                            {
                                aIdDocOri13[i++] = item.Key;
                            }
                            if (i > 0)
                            {
                                dictDest = IB.Conserva.ConservaHelper.CopiarDocumentos(aIdDocOri13);
                                foreach (KeyValuePair<long, int> item in dictOri)
                                {
                                    //Con el Id de Atenea Original obtengo el Id de Atenea destino 
                                    idContentServer = dictDest[item.Key];
                                    if (idContentServer > 0)
                                    {
                                        //Updateo la tabla con la referencia al nuevo documento en Atenea
                                        SUPER.DAL.DocAccT.UpdateIdDoc(tr, item.Value, idContentServer);
                                    }
                                    else
                                    {
                                        sError = "El servicio de copia del documento de Bitácora de Acción de Tarea " + item.Value.ToString() + " en el Content-Server devuelve -1";
                                        throw (new NullReferenceException(sError));
                                    }
                                }
                            }
                        }
                        #endregion

                        #endregion
                    }
                    catch (ConservaException cex)
                    {
                        sError += "Error código=" + cex.ErrorCode + " descripción=" + cex.Message;
                        if (cex.InnerException != null)
                        {
                            if (cex.InnerException.GetType().Name == "ConservaException")
                            {
                                ConservaException icex = (ConservaException)cex.InnerException;
                                sError += "<br />InnerException: código=" + icex.ErrorCode + " descripción=" + icex.Message;
                            }
                            else
                                sError += "<br />InnerException: descripción=" + cex.InnerException.Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        if (ex.InnerException != null)
                            sError += "<br />InnerException: descripción=" + ex.InnerException.Message;
                    }
                    finally
                    {
                        if (sError != "")
                            throw (new NullReferenceException(sError));
                    }
                    break;
            }
        }
        public static SqlDataReader FigurasModoEscritura(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYFIGUMODOESC", aParam);
        }
        public static SqlDataReader FigurasModoProduccion(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYFIGUPRODU", aParam);
        }

        public static SqlDataReader FigurasModoProduccion2(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYFIGUPRODU2", aParam);
        }

        /// <summary>
        /// Devuelve una consulta de las vacaciones de los profesionales internos asociados al proyectosubnodo
        /// </summary>
        public static SqlDataReader Vacaciones(int nIdPSN, string sTipo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@idPSN", SqlDbType.Int, 4);
            aParam[0].Value = nIdPSN;
            if (sTipo == "P")
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_VACATAS_PROF", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_VACATAS_MES", aParam);
        }


        public static void setPerfilesDefectoATareas(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETPERFILDEFECTO_TAREAS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETPERFILDEFECTO_TAREAS", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene si las tareas del proyecto son facturables por defecto
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool GetFacturable(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            if (tr == null)
                return Convert.ToBoolean(SqlHelper.ExecuteScalar("SUP_PROYECTOSUBNODO_FACTURABLE", aParam));
            else
                return Convert.ToBoolean(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOSUBNODO_FACTURABLE", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la cualidad del proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static string GetCualidad(SqlTransaction tr, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            if (tr == null)
                return (string)(SqlHelper.ExecuteScalar("SUP_PROYECTOSUBNODO_S2", aParam));
            else
                return (string)(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOSUBNODO_S2", aParam));
        }

        public static DataSet ObtenerProyectosAlertas(SqlTransaction tr,
            Nullable<int> t301_idproyecto,
	        string t301_denominacion,
	        string sTipoBusqueda,
	        Nullable<int> t303_idnodo,
	        Nullable<int> t302_idliente,
	        Nullable<int> t306_idcontrato,
	        Nullable<int> t314_idusuario_responsable,
	        Nullable<int> t307_idhorizontal,
            string sEstado)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
            aParam[i++] = ParametroSql.add("@t301_denominacion", SqlDbType.VarChar, 70, t301_denominacion);
            aParam[i++] = ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t302_idliente", SqlDbType.Int, 4, t302_idliente);
            aParam[i++] = ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato);
            aParam[i++] = ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable);
            aParam[i++] = ParametroSql.add("@t307_idhorizontal", SqlDbType.Int, 4, t307_idhorizontal);
            aParam[i++] = ParametroSql.add("@t301_estado", SqlDbType.Char, 1, sEstado);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GETPROYECTOS_ALERTAS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETPROYECTOS_ALERTAS", aParam);
        }

        public static string ObtenerProyectosACualificarCVT(int t314_idusuario)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sTooltip = "";

            sb.Append("<table id='tblDatos' class='MANO' style='width:960px;' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");//Producto o Servicio
            sb.Append("<col style='width:20px' />");//Estado
            sb.Append("<col style='width:65px;' />");//Nº de proyecto
            sb.Append("<col style='width:300px' />");//Denominación de proyecto
            sb.Append("<col style='width:225px' />");//Cliente
            sb.Append("<col style='width:225px' />");//Centro de responsabilidad
            sb.Append("<col style='width:50px' />");//Check e imagen para el motivo
            sb.Append("<col style='width:55px' />");//Fecha límite de finalización de la tarea
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.PROYECTOSUBNODO.CatalogoPendienteCualificarCVT(null, t314_idusuario);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idproyecto='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("expprof='" + dr["t808_idexpprof"].ToString() + "' ");
                sb.Append("habCualificable='" + dr["CUALIFICABLE"].ToString() + "' ");
                sb.Append("motivo='' >");

                sb.Append("<td style=\"border-right:none\"></td>");
                sb.Append("<td style=\"border-right:none\"></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;' class='MA'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td style='text-align:left;border-left: none;' class='MA'><nobr class='NBR W300' ");
                sTooltip = "<label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN4")) sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label>" + dr["t394_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN3")) sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label>" + dr["t393_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN2")) sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label>" + dr["t392_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN1")) sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label>" + dr["t391_denominacion"].ToString();
                sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString();

                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\",null,null,350)\' onMouseout=\"hideTTE()\" ");
                sb.Append(">" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)' class='MA'><nobr class='NBR W220'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)' class='MA'><nobr class='NBR W200'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td align='center'></td>");
                if (HttpContext.Current.User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                {   if (dr["t969_flimite"] != DBNull.Value)
                        sb.Append("<td>" + DateTime.Parse(dr["t969_flimite"].ToString()).ToShortDateString() + "</td>");
                    else sb.Append("<td></td>");
                }
                else sb.Append("<td></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();

        }

        public static string ObtenerInterlocutores(int t305_idproyectosubnodo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width: 500px;' cellpadding='0' cellspacing='0' border='0'>");

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ObtenerInterlocutores(null, t305_idproyectosubnodo);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("onclick='ms(this)' ");
                sb.Append("ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ObtenerInterlocutoresOCyFA()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width: 500px;' cellpadding='0' cellspacing='0' border='0'>");

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ObtenerInterlocutoresOCyFA(null);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("onclick='ms(this)' ");
                sb.Append("ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string PruebaDatosTablaDinamica()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("{ 'datos': [");

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamica(null);
            bool bPrimero = true;
            while (dr.Read())
            {
                if (bPrimero)
                {
                    bPrimero = false;
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append(@"{
	                        'idproyectosubnodo': " + dr["t305_idproyectosubnodo"].ToString() + @",
	                        'idnodo': " + dr["t303_idnodo"].ToString() + @",
	                        'desnodo': " + (char)34 + Utilidades.escape(dr["t303_denominacion"].ToString()) + (char)34 + @",
	                        'idproyecto': " + dr["t301_idproyecto"].ToString() + @",
	                        'desproyecto': " + (char)34 + Utilidades.escape(dr["t301_denominacion"].ToString()) + (char)34 + @",
	                        'idcliente': " + dr["t302_idcliente"].ToString() + @",
	                        'descliente': " + (char)34 + Utilidades.escape(dr["t302_denominacion"].ToString()) + (char)34 + @",
	                        'idresponsableproyecto': " + dr["t314_idusuario_responsable"].ToString() + @",
	                        'desresponsableproyecto': " + (char)34 + Utilidades.escape(dr["ResponsableProyecto"].ToString()) + (char)34 + @",
	                        'cualidad': " + (char)34 + dr["t305_cualidad"].ToString() + (char)34 + @",
	                        'idnaturaleza': " + dr["t323_idnaturaleza"].ToString() + @",
	                        'desnaturaleza': " + (char)34 + Utilidades.escape(dr["t323_denominacion"].ToString()) + (char)34 + @",
	                        'anomes': " + dr["t325_anomes"].ToString() + @",
	                        'importe': " + dr["importe"].ToString().Replace(",", ".") + @"
                            }");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("]}");

            return sb.ToString();
        }

        public static string PruebaDatosTablaDinamica2()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int t325_anomes_min = 0, t325_anomes_max = 0;

            sb.Append("{ 'datos': [");

            SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamica2(null);
            bool bPrimero = true;
            while (dr.Read())
            {
                if (bPrimero)
                {
                    bPrimero = false;
                    t325_anomes_min = (int)dr["t325_anomes"];
                    t325_anomes_max = (int)dr["t325_anomes"];
                }
                else
                {
                    sb.Append(",");
                    if ((int)dr["t325_anomes"] < t325_anomes_min) t325_anomes_min = (int)dr["t325_anomes"];
                    if ((int)dr["t325_anomes"] > t325_anomes_max) t325_anomes_max = (int)dr["t325_anomes"];
                }
                sb.Append(@"{
	                        'idproyectosubnodo': " + dr["t305_idproyectosubnodo"].ToString() + @",
	                        'anomes': " + dr["t325_anomes"].ToString() + @",
	                        'idnodo': " + dr["t303_idnodo"].ToString() + @",
	                        'desnodo': " + (char)34 + Utilidades.escape(dr["t303_denominacion"].ToString()) + (char)34 + @",
	                        'idproyecto': " + dr["t301_idproyecto"].ToString() + @",
	                        'desproyecto': " + (char)34 + Utilidades.escape(((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString()) + (char)34 + @",
	                        'idcliente': " + dr["t302_idcliente"].ToString() + @",
	                        'descliente': " + (char)34 + Utilidades.escape(dr["t302_denominacion"].ToString()) + (char)34 + @",
	                        'idresponsableproyecto': " + dr["t314_idusuario_responsable"].ToString() + @",
	                        'desresponsableproyecto': " + (char)34 + Utilidades.escape(dr["ResponsableProyecto"].ToString()) + (char)34 + @",
	                        'cualidad': " + (char)34 + dr["t305_cualidad"].ToString() + (char)34 + @",
	                        'idnaturaleza': " + dr["t323_idnaturaleza"].ToString() + @",
	                        'desnaturaleza': " + (char)34 + Utilidades.escape(dr["t323_denominacion"].ToString()) + (char)34 + @",
                            'Ingresos_Netos': " + dr["Ingresos_Netos"].ToString().Replace(",", ".") + @",
                            'Margen': " + dr["Margen"].ToString().Replace(",", ".") + @",
                            'Obra_en_curso': " + dr["Obra_en_curso"].ToString().Replace(",", ".") + @",
                            'Saldo_de_Clientes': " + dr["Saldo_de_Clientes"].ToString().Replace(",", ".") + @",
                            'Total_Cobros': " + dr["Total_Cobros"].ToString().Replace(",", ".") + @",
                            'Total_Gastos': " + dr["Total_Gastos"].ToString().Replace(",", ".") + @",
                            'Total_Ingresos': " + dr["Total_Ingresos"].ToString().Replace(",", ".") + @",
                            'Volumen_de_Negocio': " + dr["Volumen_de_Negocio"].ToString().Replace(",", ".") + @",
                            'Otros_consumos': " + dr["Otros_consumos"].ToString().Replace(",", ".") + @",
                            'Consumo_recursos': " + dr["Consumo_recursos"].ToString().Replace(",", ".") + @"
                            }");
            }
            dr.Close();
            dr.Dispose();
            sb.Append(@"],
                'anomes_min': " + t325_anomes_min + @",
                'anomes_max': " + t325_anomes_max + @"
            }");

            return sb.ToString();
        }

        public static DataSet PruebaDatosTablaDinamicaServidor()
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int t325_anomes_min = 0, t325_anomes_max = 0;

            //sb.Append("{ 'datos': [");

            DataSet ds = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamicaDS(null);
            ds.Tables[0].TableName = "Consulta";

            bool bPrimero = true;
            foreach (DataRow oFila in ds.Tables["Consulta"].Rows)
            {
                if (bPrimero)
                {
                    bPrimero = false;
                    t325_anomes_min = (int)oFila["t325_anomes"];
                    t325_anomes_max = (int)oFila["t325_anomes"];
                }
                else
                {
                    //sb.Append(",");
                    if ((int)oFila["t325_anomes"] < t325_anomes_min) t325_anomes_min = (int)oFila["t325_anomes"];
                    if ((int)oFila["t325_anomes"] > t325_anomes_max) t325_anomes_max = (int)oFila["t325_anomes"];
                }
                //                sb.Append(@"{
                //	                        'idproyectosubnodo': " + dr["t305_idproyectosubnodo"].ToString() + @",
                //	                        'anomes': " + dr["t325_anomes"].ToString() + @",
                //	                        'idnodo': " + dr["t303_idnodo"].ToString() + @",
                //	                        'desnodo': " + (char)34 + Utilidades.escape(dr["t303_denominacion"].ToString()) + (char)34 + @",
                //	                        'idproyecto': " + dr["t301_idproyecto"].ToString() + @",
                //	                        'desproyecto': " + (char)34 + Utilidades.escape(((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString()) + (char)34 + @",
                //	                        'idcliente': " + dr["t302_idcliente"].ToString() + @",
                //	                        'descliente': " + (char)34 + Utilidades.escape(dr["t302_denominacion"].ToString()) + (char)34 + @",
                //	                        'idresponsableproyecto': " + dr["t314_idusuario_responsable"].ToString() + @",
                //	                        'desresponsableproyecto': " + (char)34 + Utilidades.escape(dr["ResponsableProyecto"].ToString()) + (char)34 + @",
                //	                        'cualidad': " + (char)34 + dr["t305_cualidad"].ToString() + (char)34 + @",
                //	                        'idnaturaleza': " + dr["t323_idnaturaleza"].ToString() + @",
                //	                        'desnaturaleza': " + (char)34 + Utilidades.escape(dr["t323_denominacion"].ToString()) + (char)34 + @",
                //                            'Ingresos_Netos': " + dr["Ingresos_Netos"].ToString().Replace(",", ".") + @",
                //                            'Margen': " + dr["Margen"].ToString().Replace(",", ".") + @",
                //                            'Obra_en_curso': " + dr["Obra_en_curso"].ToString().Replace(",", ".") + @",
                //                            'Saldo_de_Clientes': " + dr["Saldo_de_Clientes"].ToString().Replace(",", ".") + @",
                //                            'Total_Cobros': " + dr["Total_Cobros"].ToString().Replace(",", ".") + @",
                //                            'Total_Gastos': " + dr["Total_Gastos"].ToString().Replace(",", ".") + @",
                //                            'Total_Ingresos': " + dr["Total_Ingresos"].ToString().Replace(",", ".") + @",
                //                            'Volumen_de_Negocio': " + dr["Volumen_de_Negocio"].ToString().Replace(",", ".") + @",
                //                            'Otros_consumos': " + dr["Otros_consumos"].ToString().Replace(",", ".") + @",
                //                            'Consumo_recursos': " + dr["Consumo_recursos"].ToString().Replace(",", ".") + @"
                //                            }");
            }

            #region Creación de Tablas auxiliares
            /* Creo la tabla de meses */
            DataTable dtMeses = ds.Tables.Add("Meses");
            DataColumn dcMes = dtMeses.Columns.Add("mes", typeof(Int32));
            dtMeses.PrimaryKey = new DataColumn[] { dcMes };

            while (t325_anomes_min <= t325_anomes_max)
            {
                DataRow dr = dtMeses.NewRow();
                dr[0] = t325_anomes_min;
                dtMeses.Rows.Add(dr);

                t325_anomes_min = Fechas.AddAnnomes(t325_anomes_min, 1);
            }


            /* Creo la tabla de Agrupaciones */
            DataTable dtAgrupacion = ds.Tables.Add("Agrupaciones");
            DataColumn dcAgrupacion = dtAgrupacion.Columns.Add("Agrupacion", typeof(string));
            dtAgrupacion.PrimaryKey = new DataColumn[] { dcAgrupacion };

            /* Creo la tabla de Visualizaciones */
            DataTable dtVisualizacion = ds.Tables.Add("Visualizaciones");
            DataColumn dcVisualizacion = dtVisualizacion.Columns.Add("Visualizacion", typeof(string));
            dtVisualizacion.PrimaryKey = new DataColumn[] { dcVisualizacion };

            /* Creo la tabla de Datos */
            DataTable dtDato = ds.Tables.Add("Datos");
            DataColumn dcDato = dtDato.Columns.Add("Dato", typeof(string));
            dtDato.PrimaryKey = new DataColumn[] { dcDato };



            #endregion

            return ds;
        }

        //public static DataSet PruebaDatosTablaDinamicaServidorV3()
        //{
        //    DataSet ds = SUPER.DAL.PROYECTOSUBNODO.PruebaDatosTablaDinamicaDSV3(null);
        //    ds.Tables[0].TableName = "Consulta";

        //    #region Creación de Tablas auxiliares
        //    /* Creo la tabla de Agrupaciones */
        //    DataTable dtAgrupacion = ds.Tables.Add("Agrupaciones");
        //    DataColumn dcAgrupacion = dtAgrupacion.Columns.Add("Agrupacion", typeof(string));
        //    dtAgrupacion.PrimaryKey = new DataColumn[] { dcAgrupacion };

        //    /* Creo la tabla de Visualizaciones */
        //    DataTable dtVisualizacion = ds.Tables.Add("Visualizaciones");
        //    DataColumn dcVisualizacion = dtVisualizacion.Columns.Add("Visualizacion", typeof(string));
        //    dtVisualizacion.PrimaryKey = new DataColumn[] { dcVisualizacion };

        //    /* Creo la tabla de Datos */
        //    DataTable dtDato = ds.Tables.Add("Datos");
        //    DataColumn dcDato = dtDato.Columns.Add("Dato", typeof(string));
        //    dtDato.PrimaryKey = new DataColumn[] { dcDato };

        //    #endregion

        //    return ds;
        //}

        public static void ProyectoNoCualificar(int t314_idusuario, string strDatos)
        {
            string[] aProyectos = Regex.Split(strDatos, "@proyecto@");
            foreach (string sProyecto in aProyectos)
            {
                if (sProyecto != "")
                {
                    string[] aArgs = Regex.Split(sProyecto, "@dato@");
                    DAL.PROYECTOSUBNODO.ProyectoNoCualificar(null, t314_idusuario, int.Parse(aArgs[0]), Utilidades.unescape(aArgs[1]));
                }
            }

        }
        /// <summary>
        /// Mira si un usuario (en base a sus permisos) tiene acceso en modo escritura a un proyecto
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <returns></returns>
        public static bool AccesoEscritura(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            bool bRes = false;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                bRes = true;
            else
            {
                SqlDataReader dr = SUPER.DAL.PROYECTOSUBNODO.ProyectoVision(tr, t314_idusuario, t305_idproyectosubnodo);
                if (dr.Read())
                {
                    if (int.Parse(dr["modo_lectura"].ToString()) == 0)
                        bRes = true;
                }
            }
            return bRes;
        }


        public static DIALOGOALERTAS CountDialogosAbiertos(int t305_idproyectosubnodo, int t001_idficepi)
        {
            return DIALOGOALERTAS.CountDialogosAbiertos(t305_idproyectosubnodo, t001_idficepi);
        }
        #endregion
        public static DataSet Disponibilidad(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            return SqlHelper.ExecuteDataset("SUP_CONS_DISPONIBILIDAD", aParam);
        }
        public static void UpdateMoneda(int t305_idproyectosubnodo, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            SqlHelper.ExecuteNonQuery("SUP_PROYECTOSUBNODO_UMONEDA", aParam);
        }

        public static void UpdateNivelPresupuesto(SqlTransaction tr, int t305_idproyectosubnodo, string t305_nivelpresupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t305_nivelpresupuesto", SqlDbType.Char, 1, t305_nivelpresupuesto);
            if (tr == null)
               SqlHelper.ExecuteReturn("SUP_PROYECTOSUBNODO_UNIVELPRESUPUESTO", aParam);
            else
               SqlHelper.ExecuteReturnTransaccion(tr, "SUP_PROYECTOSUBNODO_UNIVELPRESUPUESTO", aParam);
            
        }

        public static void UpdatePresupAcumTodosPT(SqlTransaction tr, int t305_idproyectosubnodo, string nivelPresupuestacionAnt)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nivelPresupuestacionAnt", SqlDbType.Char, 1, nivelPresupuestacionAnt);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_PT_U_PRESUPACUM", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_PT_U_PRESUPACUM", aParam);

        }

        public static void UpdatePresupAcumTodasFases(SqlTransaction tr, int t305_idproyectosubnodo, string nivelPresupuestacionAnt)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nivelPresupuestacionAnt", SqlDbType.Char, 1, nivelPresupuestacionAnt);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_FASE_U_PRESUPACUM", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_FASE_U_PRESUPACUM", aParam);

        }

        public static void UpdatePresupAcumTodasActividades(SqlTransaction tr, int t305_idproyectosubnodo, string nivelPresupuestacionAnt)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@nivelPresupuestacionAnt", SqlDbType.Char, 1, nivelPresupuestacionAnt);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_ACTIVIDAD_U_PRESUPACUM", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_ACTIVIDAD_U_PRESUPACUM", aParam);

        }

        public static void UpdatePresupuestoTodosPT(SqlTransaction tr, int t305_idproyectosubnodo, decimal presupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t331_presupuesto", SqlDbType.Money, 8, presupuesto);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_PT_U_PRESUPUESTO", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_PT_U_PRESUPUESTO", aParam);

        }

        public static void UpdatePresupuestoTodasFases(SqlTransaction tr, int t305_idproyectosubnodo, decimal presupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t334_presupuesto", SqlDbType.Money, 8, presupuesto);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_FASE_U_PRESUPUESTO", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_FASE_U_PRESUPUESTO", aParam);

        }

        public static void UpdatePresupuestoTodasActividades(SqlTransaction tr, int t305_idproyectosubnodo, decimal presupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t335_presupuesto", SqlDbType.Money, 8, presupuesto);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_ACTIVIDAD_U_PRESUPUESTO", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_ACTIVIDAD_U_PRESUPUESTO", aParam);

        }

        public static void UpdatePresupuestoTodasTareas(SqlTransaction tr, int t305_idproyectosubnodo, decimal presupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t332_presupuesto", SqlDbType.Money, 8, presupuesto);
            if (tr == null)
                SqlHelper.ExecuteReturn("SUP_TAREA_U_PRESUPUESTO", aParam);
            else
                SqlHelper.ExecuteReturnTransaccion(tr, "SUP_TAREA_U_PRESUPUESTO", aParam);

        }



        public static int ObtenerSubnodoDestinoReplica(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo)
            };
            object oRes;
            if (tr == null)
                oRes = SqlHelper.ExecuteReturn("SUP_OBTENERSUBNODODESTINOREPLICA", aParam);
            else
                oRes = SqlHelper.ExecuteReturnTransaccion(tr, "SUP_OBTENERSUBNODODESTINOREPLICA", aParam);

            return Convert.ToInt32(oRes);
        }
        
        public static string ObtenerTareasSubnodoSinActividadoFase(int t305_idproyectosubnodo, string nivelPresupuestacionN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
           
            if(nivelPresupuestacionN == "A")
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_TAREAS_SIN_ACTIVIDAD", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_TAREAS_SIN_FASE", aParam));
        }


        public static string ObtenerActividadesSubnodoSinFase(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            return Convert.ToString(SqlHelper.ExecuteScalar("SUP_ACTIVIDADES_SIN_FASE", aParam));            
        }
    }
}
