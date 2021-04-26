using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//para gestion de roles
using System.Web.Security;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de Recurso.
    /// </summary>
    public class Recurso
    {
        #region Atributos Y Propiedades

        private int _IdFicepi;
        public int IdFicepi
        {
            get { return _IdFicepi; }
            set { _IdFicepi = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private int? _idNodo;
        public int? idNodo
        {
            get { return _idNodo; }
            set { _idNodo = value; }
        }
        private int? _IdCalendario;
        public int? IdCalendario
        {
            get { return _IdCalendario; }
            set { _IdCalendario = value; }
        }
        private int _CodCentro;
        public int CodCentro
        {
            get { return _CodCentro; }
            set { _CodCentro = value; }
        }
        private int _CodEmpresa;
        public int CodEmpresa
        {
            get { return _CodEmpresa; }
            set { _CodEmpresa = value; }
        }
        private int? _UMCIAP;
        public int? UMCIAP
        {
            get { return _UMCIAP; }
            set { _UMCIAP = value; }
        }

        private decimal _CosteHora;
        public decimal CosteHora
        {
            get { return _CosteHora; }
            set { _CosteHora = value; }
        }
        private decimal _CosteJornada;
        public decimal CosteJornada
        {
            get { return _CosteJornada; }
            set { _CosteJornada = value; }
        }
        private double _nHorasJorRed;
        public double nHorasJorRed
        {
            get { return _nHorasJorRed; }
            set { _nHorasJorRed = value; }
        }

        private bool _JornadaReducida;
        public bool JornadaReducida
        {
            get { return _JornadaReducida; }
            set { _JornadaReducida = value; }
        }
        private bool _ControlHuecos;
        public bool ControlHuecos
        {
            get { return _ControlHuecos; }
            set { _ControlHuecos = value; }
        }

        private string _Nif;
        public string Nif
        {
            get { return _Nif; }
            set { _Nif = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        private string _Apellido1;
        public string Apellido1
        {
            get { return _Apellido1; }
            set { _Apellido1 = value; }
        }
        private string _Apellido2;
        public string Apellido2
        {
            get { return _Apellido2; }
            set { _Apellido2 = value; }
        }
        private string _DesNodo;
        public string DesNodo
        {
            get { return _DesNodo; }
            set { _DesNodo = value; }
        }
        private string _DesCentro;
        public string DesCentro
        {
            get { return _DesCentro; }
            set { _DesCentro = value; }
        }
        private string _DesCalendario;
        public string DesCalendario
        {
            get { return _DesCalendario; }
            set { _DesCalendario = value; }
        }
        private string _sCodRed;
        public string sCodRed
        {
            get { return _sCodRed; }
            set { _sCodRed = value; }
        }

        private string _AdminPC;
        public string AdminPC
        {
            get { return _AdminPC; }
            set { _AdminPC = value; }
        }

        private bool _esAdminPC;
        public bool esAdminPC
        {
            get { return _esAdminPC; }
            set { _esAdminPC = value; }
        }

        private bool _esAdminCVT;
        public bool esAdminCVT
        {
            get { return _esAdminCVT; }
            set { _esAdminCVT = value; }
        }

        private bool _esDIS;
        public bool esDIS
        {
            get { return _esDIS; }
            set { _esDIS = value; }
        }

        private string _AdminPer;
        public string AdminPer
        {
            get { return _AdminPer; }
            set { _AdminPer = value; }
        }

        private string _AdminCVT;
        public string AdminCVT
        {
            get { return _AdminCVT; }
            set { _AdminCVT = value; }
        }

        private string _sSexo;
        public string sSexo
        {
            get { return _sSexo; }
            set { _sSexo = value; }
        }
        private string _sTipoRecurso;
        public string sTipoRecurso
        {
            get { return _sTipoRecurso; }
            set { _sTipoRecurso = value; }
        }

        private DateTime? _FecUltImputacion;
        public DateTime? FecUltImputacion
        {
            get { return _FecUltImputacion; }
            set { _FecUltImputacion = value; }
        }
        private DateTime _FecAlta;
        public DateTime FecAlta
        {
            get { return _FecAlta; }
            set { _FecAlta = value; }
        }
        private DateTime? _FecBaja;
        public DateTime? FecBaja
        {
            get { return _FecBaja; }
            set { _FecBaja = value; }
        }
        private DateTime? _FecDesdeJorRed;
        public DateTime? FecDesdeJorRed
        {
            get { return _FecDesdeJorRed; }
            set { _FecDesdeJorRed = value; }
        }
        private DateTime? _FecHastaJorRed;
        public DateTime? FecHastaJorRed
        {
            get { return _FecHastaJorRed; }
            set { _FecHastaJorRed = value; }
        }

        //private bool _bMultiplesUsuarios;
        //public bool bMultiplesUsuarios
        //{
        //    get { return _bMultiplesUsuarios; }
        //    set { _bMultiplesUsuarios = value; }
        //}
        private string _sSemanaLaboral;
        public string sSemanaLaboral
        {
            get { return _sSemanaLaboral; }
            set { _sSemanaLaboral = value; }
        }
        private bool _CRP;
        public bool CRP
        {
            get { return _CRP; }
            set { _CRP = value; }
        }
        private bool _bAccesohabilitado;
        public bool bAccesohabilitado
        {
            get { return _bAccesohabilitado; }
            set { _bAccesohabilitado = value; }
        }

        private bool _bDiamanteMovil;
        public bool bDiamanteMovil
        {
            get { return _bDiamanteMovil; }
            set { _bDiamanteMovil = value; }
        }

        private bool _t314_carrusel1024;
        public bool t314_carrusel1024
        {
            get { return _t314_carrusel1024; }
            set { _t314_carrusel1024 = value; }
        }
        private bool _t314_avance1024;
        public bool t314_avance1024
        {
            get { return _t314_avance1024; }
            set { _t314_avance1024 = value; }
        }
        private bool _t314_resumen1024;
        public bool t314_resumen1024
        {
            get { return _t314_resumen1024; }
            set { _t314_resumen1024 = value; }
        }
        private bool _t314_datosres1024;
        public bool t314_datosres1024
        {
            get { return _t314_datosres1024; }
            set { _t314_datosres1024 = value; }
        }
        private bool _t314_fichaeco1024;
        public bool t314_fichaeco1024
        {
            get { return _t314_fichaeco1024; }
            set { _t314_fichaeco1024 = value; }
        }
        private bool _t314_segrenta1024;
        public bool t314_segrenta1024
        {
            get { return _t314_segrenta1024; }
            set { _t314_segrenta1024 = value; }
        }
        private bool _t314_avantec1024;
        public bool t314_avantec1024
        {
            get { return _t314_avantec1024; }
            set { _t314_avantec1024 = value; }
        }
        private bool _t314_estruct1024;
        public bool t314_estruct1024
        {
            get { return _t314_estruct1024; }
            set { _t314_estruct1024 = value; }
        }
        private bool _t314_fotopst1024;
        public bool t314_fotopst1024
        {
            get { return _t314_fotopst1024; }
            set { _t314_fotopst1024 = value; }
        }
        private bool _t314_plant1024;
        public bool t314_plant1024
        {
            get { return _t314_plant1024; }
            set { _t314_plant1024 = value; }
        }
        private bool _t314_const1024;
        public bool t314_const1024
        {
            get { return _t314_const1024; }
            set { _t314_const1024 = value; }
        }

        private bool _t314_iapfact1024;
        public bool t314_iapfact1024
        {
            get { return _t314_iapfact1024; }
            set { _t314_iapfact1024 = value; }
        }
        private bool _t314_iapdiario1024;
        public bool t314_iapdiario1024
        {
            get { return _t314_iapdiario1024; }
            set { _t314_iapdiario1024 = value; }
        }
        private bool _t314_cuadromando1024;
        public bool t314_cuadromando1024
        {
            get { return _t314_cuadromando1024; }
            set { _t314_cuadromando1024 = value; }
        }
        private bool _t314_recibirmails;
        public bool t314_recibirmails
        {
            get { return _t314_recibirmails; }
            set { _t314_recibirmails = value; }
        }
        private bool _t314_defectoperiodificacion;
        public bool t314_defectoperiodificacion
        {
            get { return _t314_defectoperiodificacion; }
            set { _t314_defectoperiodificacion = value; }
        }
        private bool _t314_multiventana;
        public bool t314_multiventana
        {
            get { return _t314_multiventana; }
            set { _t314_multiventana = value; }
        }
		
        private int _t314_nsegmb;
        public int t314_nsegmb
        {
            get { return _t314_nsegmb; }
            set { _t314_nsegmb = value; }
        }
        private byte[] _t001_foto;
        public byte[] t001_foto
        {
            get { return _t001_foto; }
            set { _t001_foto = value; }
        }
        private string _T010_CODWEATHER;
        public string T010_CODWEATHER
        {
            get { return _T010_CODWEATHER; }
            set { _T010_CODWEATHER = value; }
        }
        private string _T010_NOMWEATHER;
        public string T010_NOMWEATHER
        {
            get { return _T010_NOMWEATHER; }
            set { _T010_NOMWEATHER = value; }
        }

        private byte _t314_importaciongasvi;
        public byte t314_importaciongasvi
        {
            get { return _t314_importaciongasvi; }
            set { _t314_importaciongasvi = value; }
        }

        private string _t001_botonfecha;
        public string t001_botonfecha
        {
            get { return _t001_botonfecha; }
            set { _t001_botonfecha = value; }
        }

        private string _t422_idmoneda_vdc;
        public string t422_idmoneda_vdc
        {
            get { return _t422_idmoneda_vdc; }
            set { _t422_idmoneda_vdc = value; }
        }

        private string _t422_denominacionimportes_vdc;
        public string t422_denominacionimportes_vdc
        {
            get { return _t422_denominacionimportes_vdc; }
            set { _t422_denominacionimportes_vdc = value; }
        }
        private string _t422_idmoneda_vdp;
        public string t422_idmoneda_vdp
        {
            get { return _t422_idmoneda_vdp; }
            set { _t422_idmoneda_vdp = value; }
        }

        private string _t422_denominacionimportes_vdp;
        public string t422_denominacionimportes_vdp
        {
            get { return _t422_denominacionimportes_vdp; }
            set { _t422_denominacionimportes_vdp = value; }
        }
        private bool _t314_nuevogasvi;
        public bool t314_nuevogasvi
        {
            get { return _t314_nuevogasvi; }
            set { _t314_nuevogasvi = value; }
        }

        private bool _t314_calculoonline;
        public bool t314_calculoonline
        {
            get { return _t314_calculoonline; }
            set { _t314_calculoonline = value; }
        }

        private bool _t314_cargaestructura;
        public bool t314_cargaestructura
        {
            get { return _t314_cargaestructura; }
            set { _t314_cargaestructura = value; }
        }

        private bool _CVFinalizado;
        public bool CVFinalizado
        {
            get { return _CVFinalizado; }
            set { _CVFinalizado = value; }
        }

        private int? _Responsable_CvExclusion;
        public int? Responsable_CvExclusion
        {
            get { return _Responsable_CvExclusion; }
            set { _Responsable_CvExclusion = value; }
        }
        private int? _Profesional_CvExclusion;
        public int? Profesional_CvExclusion
        {
            get { return _Profesional_CvExclusion; }
            set { _Profesional_CvExclusion = value; }
        }
        private bool _baja;
        public bool baja
        {
            get { return _baja; }
            set { _baja = value; }
        }
        #endregion

        public Recurso()
        {
           // bMultiplesUsuarios = false;
            this.esAdminPC = false;
            this.esAdminCVT = false;
            this.esDIS = false;
            this.AdminPC = "";
            this.AdminCVT = "";
            this.AdminPer = "";
        }
        public bool ObtenerRecurso(string IDRED, int? t314_idusuario)
        {
            bool bIdentificado = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIDRED", SqlDbType.VarChar, 15);
            aParam[0].Value = IDRED;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_LOGIN", aParam);

            if (dr.Read())
            {
                bIdentificado = true;
                this.IdFicepi = int.Parse(dr["T001_IDFICEPI"].ToString());
                this.esDIS = Recurso.bPerteneceDIS(null, this.IdFicepi);

                this.IdUsuario = int.Parse(dr["t314_idusuario"].ToString());
                this.Nif = dr["t001_cip"].ToString();
                this.Nombre = dr["Nombre"].ToString();
                this.Apellido1 = dr["APELLIDO1"].ToString();
                this.Apellido2 = dr["APELLIDO2"].ToString();
                if (dr["t303_idnodo"] != DBNull.Value)
                    this.idNodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_ultcierreIAP"] != DBNull.Value)
                    this.UMCIAP = int.Parse(dr["t303_ultcierreIAP"].ToString());
                this.DesNodo = dr["t303_denominacion"].ToString();
                this.AdminCVT = dr["t399_figura_cvt"].ToString();
                this.AdminPer = dr["t399_figura_per"].ToString();
                this.AdminPC = dr["t399_figura"].ToString();

                if (this.AdminPC == "S") this.AdminPC = "SA";

                this.esAdminPC = false;
                if (this.AdminPC != "")
                    this.esAdminPC = true;
                this.esAdminCVT = false;
                if (this.AdminCVT != "")
                    this.esAdminCVT = true;

                this.JornadaReducida = (bool)dr["t314_jornadareducida"];
                this.ControlHuecos = (bool)dr["t314_controlhuecos"];
                if (dr["IdCalendario"] != DBNull.Value)
                    this.IdCalendario = int.Parse(dr["IdCalendario"].ToString());
                this.DesCalendario = dr["desCalendario"].ToString();
                if (dr["T009_IDCENTRAB"] != DBNull.Value)
                    this.CodCentro = int.Parse(dr["T009_IDCENTRAB"].ToString());
                this.DesCentro = dr["T009_DESCENTRAB"].ToString();
                this.FecAlta = DateTime.Parse(dr["t314_falta"].ToString());
                if (dr["t314_fbaja"] != DBNull.Value)
                    this.FecBaja = DateTime.Parse(dr["t314_fbaja"].ToString());
                //La fecha de última imputación será la mayor entre la última imputación y el último mes cerrado del nodo
                //if (dr["fUltImputacion"] != DBNull.Value)
                //    this.FecUltImputacion = DateTime.Parse(dr["fUltImputacion"].ToString());
                string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, this.IdUsuario);
                if (sFecUltImputac != "")
                    this.FecUltImputacion = DateTime.Parse(sFecUltImputac);

                this.nHorasJorRed = double.Parse(dr["t314_horasjor_red"].ToString());
                if (dr["t314_fdesde_red"] != DBNull.Value)
                    this.FecDesdeJorRed = DateTime.Parse(dr["t314_fdesde_red"].ToString());
                if (dr["t314_fhasta_red"] != DBNull.Value)
                    this.FecHastaJorRed = DateTime.Parse(dr["t314_fhasta_red"].ToString());
                this.sSemanaLaboral = dr["t066_semlabL"].ToString() + "," + dr["t066_semlabM"].ToString() + "," + dr["t066_semlabX"].ToString() + "," + dr["t066_semlabJ"].ToString() + "," + dr["t066_semlabV"].ToString() + "," + dr["t066_semlabS"].ToString() + "," + dr["t066_semlabD"].ToString();
                this.sCodRed = dr["t001_codred"].ToString();
                this.sSexo = dr["t001_sexo"].ToString();
                this.CRP = (bool)dr["t314_crp"];
                this.bAccesohabilitado = (bool)dr["t314_accesohabilitado"];
                this.bDiamanteMovil = (bool)dr["t314_diamante"];
                this.sTipoRecurso = dr["tipo"].ToString();
                this.t314_nsegmb = int.Parse(dr["t314_nsegmb"].ToString());
                this.T010_CODWEATHER = dr["T010_CODWEATHER"].ToString();
                this.T010_NOMWEATHER = dr["T010_NOMWEATHER"].ToString();
                this.t314_carrusel1024 = (bool)dr["t314_carrusel1024"];
                this.t314_avance1024 = (bool)dr["t314_avance1024"];
                this.t314_resumen1024 = (bool)dr["t314_resumen1024"];
                this.t314_datosres1024 = (bool)dr["t314_datosres1024"];
                this.t314_fichaeco1024 = (bool)dr["t314_fichaeco1024"];
                this.t314_segrenta1024 = (bool)dr["t314_segrenta1024"];
                this.t314_avantec1024 = (bool)dr["t314_avantec1024"];
                this.t314_estruct1024 = (bool)dr["t314_estruct1024"];
                this.t314_fotopst1024 = (bool)dr["t314_fotopst1024"];
                this.t314_plant1024 = (bool)dr["t314_plant1024"];
                this.t314_const1024 = (bool)dr["t314_const1024"];
                this.t314_iapfact1024 = (bool)dr["t314_iapfact1024"];
                this.t314_iapdiario1024 = (bool)dr["t314_iapdiario1024"];
                this.t314_cuadromando1024 = (bool)dr["t314_cuadromando1024"];
                this.t314_importaciongasvi = (byte)dr["t314_importaciongasvi"];
                this.t314_recibirmails = (bool)dr["t314_recibirmails"];
                this.t314_defectoperiodificacion = (bool)dr["t314_defectoperiodificacion"];
                this.t314_multiventana = (bool)dr["t314_multiventana"];
                this.t001_botonfecha = dr["t001_botonfecha"].ToString();
                this.t422_idmoneda_vdc = dr["t422_idmoneda_vdc"].ToString();
                this.t422_denominacionimportes_vdc = dr["t422_denominacionimportes_vdc"].ToString();
                if (dr["t422_idmoneda_vdp"] != DBNull.Value)
                    this.t422_idmoneda_vdp = dr["t422_idmoneda_vdp"].ToString();
                if (dr["t422_denominacionimportes_vdp"] != DBNull.Value)
                    this.t422_denominacionimportes_vdp = dr["t422_denominacionimportes_vdp"].ToString();
                this.t314_nuevogasvi = (bool)dr["t314_nuevogasvi"];
                this.t314_calculoonline = (bool)dr["t314_calculoonline"];
                this.t314_cargaestructura = (bool)dr["t314_cargaestructura"];
                this.CVFinalizado = (bool)dr["CVFinalizado"];
                this.Responsable_CvExclusion = (dr["RESPONSABLE_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["RESPONSABLE_CVEXCLUSION"].ToString()) : 0;
                this.Profesional_CvExclusion = (dr["PROFESIONAL_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["PROFESIONAL_CVEXCLUSION"].ToString()) : 0;
            }

            //if (dr.Read()) //si hubiera una segunda fila en el sqldatareader, es que tiene más de un usuario activo
            //{
            //    bMultiplesUsuarios = true;
            //}
            dr.Close();
            dr.Dispose();

            this.t001_foto = Recurso.ObtenerFoto(this.IdFicepi);

            return bIdentificado;
        }
        public bool ObtenerRecursoBaja(string IDRED, int? t314_idusuario)
        {
            bool bIdentificado = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIDRED", SqlDbType.VarChar, 15);
            aParam[0].Value = IDRED;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_LOGIN_BAJA", aParam);

            if (dr.Read())
            {
                bIdentificado = true;
                this.IdFicepi = int.Parse(dr["T001_IDFICEPI"].ToString());
                this.esDIS = Recurso.bPerteneceDIS(null, this.IdFicepi);
                this.IdUsuario = int.Parse(dr["t314_idusuario"].ToString());
                this.Nombre = dr["Nombre"].ToString();
                this.Apellido1 = dr["APELLIDO1"].ToString();
                this.Apellido2 = dr["APELLIDO2"].ToString();
                if (dr["t303_idnodo"] != DBNull.Value)
                    this.idNodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t303_ultcierreIAP"] != DBNull.Value)
                    this.UMCIAP = int.Parse(dr["t303_ultcierreIAP"].ToString());
                this.DesNodo = dr["t303_denominacion"].ToString();
                this.AdminPC = "";
                this.AdminPer = "";
                this.AdminCVT = dr["t399_figura_cvt"].ToString();
                if (dr["t399_figura"].ToString() == "P")
                    this.AdminPer = dr["t399_figura"].ToString();
                else
                {
                    this.AdminPC = dr["t399_figura"].ToString();
                    if (this.AdminPC == "S") this.AdminPC = "SA";

                }
                this.esAdminPC = false;
                if (this.AdminPC != "")
                    this.esAdminPC = true;
                this.esAdminCVT = false;
                if (this.AdminCVT != "")
                    this.esAdminCVT = true;

                this.JornadaReducida = (bool)dr["t314_jornadareducida"];
                this.ControlHuecos = (bool)dr["t314_controlhuecos"];
                if (dr["IdCalendario"] != DBNull.Value)
                    this.IdCalendario = int.Parse(dr["IdCalendario"].ToString());
                this.DesCalendario = dr["desCalendario"].ToString();
                if (dr["T009_IDCENTRAB"] != DBNull.Value)
                    this.CodCentro = int.Parse(dr["T009_IDCENTRAB"].ToString());
                this.DesCentro = dr["T009_DESCENTRAB"].ToString();
                this.FecAlta = DateTime.Parse(dr["t314_falta"].ToString());
                if (dr["t314_fbaja"] != DBNull.Value)
                    this.FecBaja = DateTime.Parse(dr["t314_fbaja"].ToString());
                //La fecha de última imputación será la mayor entre la última imputación y el último mes cerrado del nodo
                //if (dr["fUltImputacion"] != DBNull.Value)
                //    this.FecUltImputacion = DateTime.Parse(dr["fUltImputacion"].ToString());
                string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, this.IdUsuario);
                if (sFecUltImputac != "")
                    this.FecUltImputacion = DateTime.Parse(sFecUltImputac);

                this.nHorasJorRed = double.Parse(dr["t314_horasjor_red"].ToString());
                if (dr["t314_fdesde_red"] != DBNull.Value)
                    this.FecDesdeJorRed = DateTime.Parse(dr["t314_fdesde_red"].ToString());
                if (dr["t314_fhasta_red"] != DBNull.Value)
                    this.FecHastaJorRed = DateTime.Parse(dr["t314_fhasta_red"].ToString());
                this.sSemanaLaboral = dr["t066_semlabL"].ToString() + "," + dr["t066_semlabM"].ToString() + "," + dr["t066_semlabX"].ToString() + "," + dr["t066_semlabJ"].ToString() + "," + dr["t066_semlabV"].ToString() + "," + dr["t066_semlabS"].ToString() + "," + dr["t066_semlabD"].ToString();
                this.sCodRed = dr["t001_codred"].ToString();
                this.sSexo = dr["t001_sexo"].ToString();
                this.CRP = (bool)dr["t314_crp"];
                this.bAccesohabilitado = (bool)dr["t314_accesohabilitado"];
                this.bDiamanteMovil = (bool)dr["t314_diamante"];
                this.sTipoRecurso = dr["tipo"].ToString();
                this.t314_nsegmb = int.Parse(dr["t314_nsegmb"].ToString());
                this.T010_CODWEATHER = dr["T010_CODWEATHER"].ToString();
                this.T010_NOMWEATHER = dr["T010_NOMWEATHER"].ToString();
                this.t314_carrusel1024 = (bool)dr["t314_carrusel1024"];
                this.t314_avance1024 = (bool)dr["t314_avance1024"];
                this.t314_resumen1024 = (bool)dr["t314_resumen1024"];
                this.t314_datosres1024 = (bool)dr["t314_datosres1024"];
                this.t314_fichaeco1024 = (bool)dr["t314_fichaeco1024"];
                this.t314_segrenta1024 = (bool)dr["t314_segrenta1024"];
                this.t314_avantec1024 = (bool)dr["t314_avantec1024"];
                this.t314_estruct1024 = (bool)dr["t314_estruct1024"];
                this.t314_fotopst1024 = (bool)dr["t314_fotopst1024"];
                this.t314_plant1024 = (bool)dr["t314_plant1024"];
                this.t314_const1024 = (bool)dr["t314_const1024"];
                this.t314_iapfact1024 = (bool)dr["t314_iapfact1024"];
                this.t314_iapdiario1024 = (bool)dr["t314_iapdiario1024"];
                this.t314_cuadromando1024 = (bool)dr["t314_cuadromando1024"];
                this.t314_importaciongasvi = (byte)dr["t314_importaciongasvi"];
                this.t314_recibirmails = (bool)dr["t314_recibirmails"];
                this.t314_defectoperiodificacion = (bool)dr["t314_defectoperiodificacion"];
                this.t314_multiventana = (bool)dr["t314_multiventana"];
                this.t422_idmoneda_vdc = dr["t422_idmoneda_VDC"].ToString();
                this.t422_denominacionimportes_vdc = dr["t422_denominacionimportes_vdc"].ToString();
                if (dr["t422_idmoneda_vdp"] != DBNull.Value)
                    this.t422_idmoneda_vdp = dr["t422_idmoneda_VDP"].ToString();
                if (dr["t422_denominacionimportes_vdp"] != DBNull.Value)
                    this.t422_denominacionimportes_vdp = dr["t422_denominacionimportes_vdp"].ToString();
                this.t314_nuevogasvi = (bool)dr["t314_nuevogasvi"];
                this.t314_calculoonline = (bool)dr["t314_calculoonline"];
                this.t314_cargaestructura = (bool)dr["t314_cargaestructura"];
                //this.CVFinalizado = (bool)dr["CVFinalizado"];
                //this.Responsable_CvExclusion = (dr["RESPONSABLE_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["RESPONSABLE_CVEXCLUSION"].ToString()) : 0;
                //this.Profesional_CvExclusion = (dr["PROFESIONAL_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["PROFESIONAL_CVEXCLUSION"].ToString()) : 0;

            }
            //if (dr.Read()) //si hubiera una segunda fila en el sqldatareader, es que tiene más de un usuario activo
            //{
            //    bMultiplesUsuarios = true;
            //}
            dr.Close();
            dr.Dispose();

            this.t001_foto = Recurso.ObtenerFoto(this.IdFicepi);

            return bIdentificado;
        }
        public bool ObtenerRecursoFICEPI(string IDRED)
        {
            bool bIdentificado = false;

            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sIDRED", SqlDbType.VarChar, 15, IDRED);

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_LOGINFICEPI", aParam);

            if (dr.Read())
            {
                bIdentificado = true;
                this.IdFicepi = int.Parse(dr["T001_IDFICEPI"].ToString());
                this.esDIS = Recurso.bPerteneceDIS(null, this.IdFicepi);
                this.Nombre = dr["Nombre"].ToString();
                this.Apellido1 = dr["APELLIDO1"].ToString();
                this.Apellido2 = dr["APELLIDO2"].ToString();

                this.AdminCVT = dr["t399_figura_cvt"].ToString();
                this.esAdminCVT = false;
                if (this.AdminCVT != "")
                    this.esAdminCVT = true;

                this.sCodRed = dr["t001_codred"].ToString();
                this.sSexo = dr["t001_sexo"].ToString();
                this.sTipoRecurso = dr["tipo"].ToString();
                this.T010_CODWEATHER = dr["T010_CODWEATHER"].ToString();
                this.T010_NOMWEATHER = dr["T010_NOMWEATHER"].ToString();
                this.t001_botonfecha = dr["t001_botonfecha"].ToString();
                this.CVFinalizado = (bool)dr["CVFinalizado"];
                this.Responsable_CvExclusion = (dr["RESPONSABLE_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["RESPONSABLE_CVEXCLUSION"].ToString()) : 0;
                this.Profesional_CvExclusion = (dr["PROFESIONAL_CVEXCLUSION"].ToString() != "") ? (int?)int.Parse(dr["PROFESIONAL_CVEXCLUSION"].ToString()) : 0;
            }

            dr.Close();
            dr.Dispose();

            this.t001_foto = Recurso.ObtenerFoto(this.IdFicepi);

            return bIdentificado;
        }

        public static byte[] ObtenerFoto(int t001_idficepi)
        {
            byte[] oFoto = null;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GETFOTO", aParam);

            if (dr.Read())
            {
                if (dr["t001_foto"] != DBNull.Value)
                    oFoto = (byte[])dr["t001_foto"];
            }

            dr.Close();
            dr.Dispose();

            return oFoto;
        }

        public static void CargarRoles(string sUsuario, string sFicepi, string sAdmin, string sAdminPer, string sAdminCVT)
        {
            #region Creación dinámica de roles
            //Roles Administrador y especiales
            if (!Roles.RoleExists("A")) Roles.CreateRole("A");
            if (!Roles.RoleExists("PER")) Roles.CreateRole("PER");
            if (!Roles.RoleExists("REC")) Roles.CreateRole("REC");
            if (!Roles.RoleExists("MISCONS")) Roles.CreateRole("MISCONS");
            if (!Roles.RoleExists("USUMULT")) Roles.CreateRole("USUMULT");
            if (!Roles.RoleExists("DIS")) Roles.CreateRole("DIS");
            if (!Roles.RoleExists("USA")) Roles.CreateRole("USA");
            if (!Roles.RoleExists("T")) Roles.CreateRole("T");
            //Rol para pruebas de IAP30
            if (!Roles.RoleExists("PPIAP30")) Roles.CreateRole("PPIAP30");

            //Supernodo 4
            if (!Roles.RoleExists("RSN4")) Roles.CreateRole("RSN4");
            if (!Roles.RoleExists("DSN4")) Roles.CreateRole("DSN4");
            if (!Roles.RoleExists("GSN4")) Roles.CreateRole("GSN4");
            if (!Roles.RoleExists("ISN4")) Roles.CreateRole("ISN4");
            if (!Roles.RoleExists("CSN4")) Roles.CreateRole("CSN4");
            if (!Roles.RoleExists("SSN4")) Roles.CreateRole("SSN4");
            if (!Roles.RoleExists("VSN4")) Roles.CreateRole("VSN4");
            //Supernodo 3  
            if (!Roles.RoleExists("RSN3")) Roles.CreateRole("RSN3");
            if (!Roles.RoleExists("DSN3")) Roles.CreateRole("DSN3");
            if (!Roles.RoleExists("GSN3")) Roles.CreateRole("GSN3");
            if (!Roles.RoleExists("ISN3")) Roles.CreateRole("ISN3");
            if (!Roles.RoleExists("CSN3")) Roles.CreateRole("CSN3");
            if (!Roles.RoleExists("SSN3")) Roles.CreateRole("SSN3");
            if (!Roles.RoleExists("VSN3")) Roles.CreateRole("VSN3");
            //Supernodo 2
            if (!Roles.RoleExists("RSN2")) Roles.CreateRole("RSN2");
            if (!Roles.RoleExists("DSN2")) Roles.CreateRole("DSN2");
            if (!Roles.RoleExists("GSN2")) Roles.CreateRole("GSN2");
            if (!Roles.RoleExists("ISN2")) Roles.CreateRole("ISN2");
            if (!Roles.RoleExists("CSN2")) Roles.CreateRole("CSN2");
            if (!Roles.RoleExists("SSN2")) Roles.CreateRole("SSN2");
            if (!Roles.RoleExists("VSN2")) Roles.CreateRole("VSN2");
            //Supernodo 1
            if (!Roles.RoleExists("RSN1")) Roles.CreateRole("RSN1");
            if (!Roles.RoleExists("DSN1")) Roles.CreateRole("DSN1");
            if (!Roles.RoleExists("GSN1")) Roles.CreateRole("GSN1");
            if (!Roles.RoleExists("ISN1")) Roles.CreateRole("ISN1");
            if (!Roles.RoleExists("CSN1")) Roles.CreateRole("CSN1");
            if (!Roles.RoleExists("SSN1")) Roles.CreateRole("SSN1");
            if (!Roles.RoleExists("VSN1")) Roles.CreateRole("VSN1");
            //Nodo
            if (!Roles.RoleExists("RN")) Roles.CreateRole("RN");
            if (!Roles.RoleExists("DN")) Roles.CreateRole("DN");
            if (!Roles.RoleExists("CN")) Roles.CreateRole("CN");
            if (!Roles.RoleExists("GN")) Roles.CreateRole("GN");
            if (!Roles.RoleExists("IN")) Roles.CreateRole("IN");
            if (!Roles.RoleExists("SN")) Roles.CreateRole("SN");
            if (!Roles.RoleExists("PN")) Roles.CreateRole("PN");
            if (!Roles.RoleExists("VN")) Roles.CreateRole("VN");
            if (!Roles.RoleExists("OT")) Roles.CreateRole("OT");
            if (!Roles.RoleExists("RG")) Roles.CreateRole("RG");
            //Subodo
            if (!Roles.RoleExists("RSB")) Roles.CreateRole("RSB");
            if (!Roles.RoleExists("DSB")) Roles.CreateRole("DSB");
            if (!Roles.RoleExists("GSB")) Roles.CreateRole("GSB");
            if (!Roles.RoleExists("ISB")) Roles.CreateRole("ISB");
            if (!Roles.RoleExists("SSB")) Roles.CreateRole("SSB");
            //Proyecto
            if (!Roles.RoleExists("RP")) Roles.CreateRole("RP");
            if (!Roles.RoleExists("DP")) Roles.CreateRole("DP");
            if (!Roles.RoleExists("CP")) Roles.CreateRole("CP");
            if (!Roles.RoleExists("IP")) Roles.CreateRole("IP");
            if (!Roles.RoleExists("JP")) Roles.CreateRole("JP");
            if (!Roles.RoleExists("MP")) Roles.CreateRole("MP");
            if (!Roles.RoleExists("SP")) Roles.CreateRole("SP");
            if (!Roles.RoleExists("BP")) Roles.CreateRole("BP");
            if (!Roles.RoleExists("K")) Roles.CreateRole("K");
            //Contrato
            if (!Roles.RoleExists("RC")) Roles.CreateRole("RC");
            if (!Roles.RoleExists("DC")) Roles.CreateRole("DC");
            if (!Roles.RoleExists("IC")) Roles.CreateRole("IC");
            //Horizontal
            if (!Roles.RoleExists("RH")) Roles.CreateRole("RH");
            if (!Roles.RoleExists("DH")) Roles.CreateRole("DH");
            if (!Roles.RoleExists("IH")) Roles.CreateRole("IH");
            //Cliente
            if (!Roles.RoleExists("RL")) Roles.CreateRole("RL");
            if (!Roles.RoleExists("DL")) Roles.CreateRole("DL");
            if (!Roles.RoleExists("IL")) Roles.CreateRole("IL");
            //Cualificador Qn
            if (!Roles.RoleExists("RQN")) Roles.CreateRole("RQN");
            if (!Roles.RoleExists("DQN")) Roles.CreateRole("DQN");
            if (!Roles.RoleExists("IQN")) Roles.CreateRole("IQN");
            //Cualificador Q1
            if (!Roles.RoleExists("RQ1")) Roles.CreateRole("RQ1");
            if (!Roles.RoleExists("DQ1")) Roles.CreateRole("DQ1");
            if (!Roles.RoleExists("IQ1")) Roles.CreateRole("IQ1");
            //Cualificador Q2
            if (!Roles.RoleExists("RQ2")) Roles.CreateRole("RQ2");
            if (!Roles.RoleExists("DQ2")) Roles.CreateRole("DQ2");
            if (!Roles.RoleExists("IQ2")) Roles.CreateRole("IQ2");
            //Cualificador Q3
            if (!Roles.RoleExists("RQ3")) Roles.CreateRole("RQ3");
            if (!Roles.RoleExists("DQ3")) Roles.CreateRole("DQ3");
            if (!Roles.RoleExists("IQ3")) Roles.CreateRole("IQ3");
            //Cualificador Q4
            if (!Roles.RoleExists("RQ4")) Roles.CreateRole("RQ4");
            if (!Roles.RoleExists("DQ4")) Roles.CreateRole("DQ4");
            if (!Roles.RoleExists("IQ4")) Roles.CreateRole("IQ4");

            //Consulta de CVs -> CVT 
            if (!Roles.RoleExists("ACV")) Roles.CreateRole("ACV");
            if (!Roles.RoleExists("ECV")) Roles.CreateRole("ECV");
            if (!Roles.RoleExists("CCV")) Roles.CreateRole("CCV");
            if (!Roles.RoleExists("UCV")) Roles.CreateRole("UCV");

            //GESTOR DE ALERTAS
            if (!Roles.RoleExists("GA")) Roles.CreateRole("GA");

            //Evaluador
            if (!Roles.RoleExists("EVAL")) Roles.CreateRole("EVAL");

            //Comercial SIC
            if (!Roles.RoleExists("COMS")) Roles.CreateRole("COMS");
            //Visualizador de sus facturas.
            if (!Roles.RoleExists("VSF")) Roles.CreateRole("VSF");
            //Responsable Organización Comercial
            if (!Roles.RoleExists("ROC")) Roles.CreateRole("ROC");
            //Responsable Línea Oferta
            if (!Roles.RoleExists("RLO")) Roles.CreateRole("RLO");
            //Responsable Asset
            if (!Roles.RoleExists("RAS")) Roles.CreateRole("RAS");

            //Roles PREVENTA
            #region PREVENTA
            // Figura Responsable a nivel de área preventa 
            if (!Roles.RoleExists("RAPREV")) Roles.CreateRole("RAPREV");
            // Figura Responsable a nivel de subárea preventa
            if (!Roles.RoleExists("RSAPREV")) Roles.CreateRole("RSAPREV");
            //Figura (Delegado, Colaborador, Invitado) a nivel de área preventa
            if (!Roles.RoleExists("DAPREV")) Roles.CreateRole("DAPREV");
            if (!Roles.RoleExists("CAPREV")) Roles.CreateRole("CAPREV");
            if (!Roles.RoleExists("IAPREV")) Roles.CreateRole("IAPREV");
            //Figura (Delegado, Colaborador, Posible líder) a nivel de subárea preventa
            if (!Roles.RoleExists("DSAPREV")) Roles.CreateRole("DSAPREV");
            if (!Roles.RoleExists("CSAPREV")) Roles.CreateRole("CSAPREV");
            if (!Roles.RoleExists("LSAPREV")) Roles.CreateRole("LSAPREV");
            //Líder de acción preventa
            if (!Roles.RoleExists("LACCPREV")) Roles.CreateRole("LACCPREV");
            //Participante en tarea preventa
            if (!Roles.RoleExists("PTARPREV")) Roles.CreateRole("PTARPREV");

            #endregion

            //TODO Test SIC Desarrollo
            if (!Roles.RoleExists("SPM")) Roles.CreateRole("SPM");
            
            #endregion

            //Se borran los roles que pudiera tener el usuario.
            foreach (string Rol in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
            {
                //if (Rol == "REC" && HttpContext.Current.Session["IDFICEPI_ENTRADA"] != null) continue;
                if (HttpContext.Current.User.IsInRole(Rol)) Roles.RemoveUserFromRole(HttpContext.Current.User.Identity.Name, Rol);
            }
            //Roles.RemoveUserFromRoles(HttpContext.Current.User.Identity.Name.ToString(), Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name));
            //Obtengo las diferentes figuras del usuario y genero los roles 
            //Si es administrador o superadministrador no me molesto en consultar la base de datos
            #region Asignar Roles
            //try {
                if (sUsuario != "")
                {
                    if (!HttpContext.Current.User.IsInRole("T"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "T");
                }
                if (sAdmin != "")
                {
                    if (!HttpContext.Current.User.IsInRole("A"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "A");
                }
                if (sAdminPer != "")
                {
                    if (!HttpContext.Current.User.IsInRole("PER"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "PER");
                }
                if (sAdminCVT != "")
                {
                    if (!HttpContext.Current.User.IsInRole("ACV"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "ACV");
                }

                if ((bool)HttpContext.Current.Session["MULTIUSUARIO"]){
                    if (!HttpContext.Current.User.IsInRole("USUMULT"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "USUMULT");
                }

                if (Recurso.bPerteneceDIS(null, (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"]) && !HttpContext.Current.User.IsInRole("DIS"))
                    Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "DIS");
                //En principio solo permitimos reconexión a administradores de producción y de CVT (+ DIS)
                if (HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "A" ||
                    HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "SA" ||
                    HttpContext.Current.Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() == "A")
                {
                    if (!HttpContext.Current.User.IsInRole("REC"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "REC");
                }

                SqlParameter[] aParam = new SqlParameter[2];
			    int i = 0;
			    aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, (sUsuario=="")?0:int.Parse(sUsuario));
                aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, int.Parse(sFicepi));
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURAS", aParam);
                while (dr.Read())
                {
                    if (!HttpContext.Current.User.IsInRole(dr["figura"].ToString()))
                    {
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, dr["figura"].ToString());
                    }
                }
                dr.Close();
                dr.Dispose();
            //}
            //catch { }
            #endregion
        }
        public static void CargarRolesForaneo(string sNif, string sUsuario, string sFicepi, string sAdmin, string sAdminPer, string sAdminCVT)
        {
            string sUserWindows = HttpContext.Current.User.Identity.Name.ToString();
            //Obtengo las diferentes figuras del usuario y genero los roles 
            //Si es administrador o superadministrador no me molesto en consultar la base de datos
            #region Roles especiales
            try
            {
                #region Creación dinámica de roles
                //Roles Administrador y especiales
                if (!Roles.RoleExists("A")) Roles.CreateRole("A");
                if (!Roles.RoleExists("PER")) Roles.CreateRole("PER");
                if (!Roles.RoleExists("REC")) Roles.CreateRole("REC");
                if (!Roles.RoleExists("MISCONS")) Roles.CreateRole("MISCONS");
                if (!Roles.RoleExists("USUMULT")) Roles.CreateRole("USUMULT");
                if (!Roles.RoleExists("DIS")) Roles.CreateRole("DIS");
                if (!Roles.RoleExists("USA")) Roles.CreateRole("USA");
                if (!Roles.RoleExists("T")) Roles.CreateRole("T");
                //Rol para pruebas de IAP30
                if (!Roles.RoleExists("PPIAP30")) Roles.CreateRole("PPIAP30");

                //Supernodo 4
                if (!Roles.RoleExists("RSN4")) Roles.CreateRole("RSN4");
                if (!Roles.RoleExists("DSN4")) Roles.CreateRole("DSN4");
                if (!Roles.RoleExists("GSN4")) Roles.CreateRole("GSN4");
                if (!Roles.RoleExists("ISN4")) Roles.CreateRole("ISN4");
                if (!Roles.RoleExists("CSN4")) Roles.CreateRole("CSN4");
                if (!Roles.RoleExists("SSN4")) Roles.CreateRole("SSN4");
                if (!Roles.RoleExists("VSN4")) Roles.CreateRole("VSN4");
                //Supernodo 3  
                if (!Roles.RoleExists("RSN3")) Roles.CreateRole("RSN3");
                if (!Roles.RoleExists("DSN3")) Roles.CreateRole("DSN3");
                if (!Roles.RoleExists("GSN3")) Roles.CreateRole("GSN3");
                if (!Roles.RoleExists("ISN3")) Roles.CreateRole("ISN3");
                if (!Roles.RoleExists("CSN3")) Roles.CreateRole("CSN3");
                if (!Roles.RoleExists("SSN3")) Roles.CreateRole("SSN3");
                if (!Roles.RoleExists("VSN3")) Roles.CreateRole("VSN3");
                //Supernodo 2
                if (!Roles.RoleExists("RSN2")) Roles.CreateRole("RSN2");
                if (!Roles.RoleExists("DSN2")) Roles.CreateRole("DSN2");
                if (!Roles.RoleExists("GSN2")) Roles.CreateRole("GSN2");
                if (!Roles.RoleExists("ISN2")) Roles.CreateRole("ISN2");
                if (!Roles.RoleExists("CSN2")) Roles.CreateRole("CSN2");
                if (!Roles.RoleExists("SSN2")) Roles.CreateRole("SSN2");
                if (!Roles.RoleExists("VSN2")) Roles.CreateRole("VSN2");
                //Supernodo 1
                if (!Roles.RoleExists("RSN1")) Roles.CreateRole("RSN1");
                if (!Roles.RoleExists("DSN1")) Roles.CreateRole("DSN1");
                if (!Roles.RoleExists("GSN1")) Roles.CreateRole("GSN1");
                if (!Roles.RoleExists("ISN1")) Roles.CreateRole("ISN1");
                if (!Roles.RoleExists("CSN1")) Roles.CreateRole("CSN1");
                if (!Roles.RoleExists("SSN1")) Roles.CreateRole("SSN1");
                if (!Roles.RoleExists("VSN2")) Roles.CreateRole("VSN1");
                //Nodo
                if (!Roles.RoleExists("RN")) Roles.CreateRole("RN");
                if (!Roles.RoleExists("DN")) Roles.CreateRole("DN");
                if (!Roles.RoleExists("CN")) Roles.CreateRole("CN");
                if (!Roles.RoleExists("GN")) Roles.CreateRole("GN");
                if (!Roles.RoleExists("IN")) Roles.CreateRole("IN");
                if (!Roles.RoleExists("SN")) Roles.CreateRole("SN");
                if (!Roles.RoleExists("PN")) Roles.CreateRole("PN");
                if (!Roles.RoleExists("VN")) Roles.CreateRole("VN");
                if (!Roles.RoleExists("OT")) Roles.CreateRole("OT");
                if (!Roles.RoleExists("RG")) Roles.CreateRole("RG");
                //Subodo
                if (!Roles.RoleExists("RSB")) Roles.CreateRole("RSB");
                if (!Roles.RoleExists("DSB")) Roles.CreateRole("DSB");
                if (!Roles.RoleExists("GSB")) Roles.CreateRole("GSB");
                if (!Roles.RoleExists("ISB")) Roles.CreateRole("ISB");
                if (!Roles.RoleExists("SSB")) Roles.CreateRole("SSB");
                //Proyecto
                if (!Roles.RoleExists("RP")) Roles.CreateRole("RP");
                if (!Roles.RoleExists("DP")) Roles.CreateRole("DP");
                if (!Roles.RoleExists("CP")) Roles.CreateRole("CP");
                if (!Roles.RoleExists("IP")) Roles.CreateRole("IP");
                if (!Roles.RoleExists("JP")) Roles.CreateRole("JP");
                if (!Roles.RoleExists("MP")) Roles.CreateRole("MP");
                if (!Roles.RoleExists("SP")) Roles.CreateRole("SP");
                if (!Roles.RoleExists("BP")) Roles.CreateRole("BP");
                if (!Roles.RoleExists("K")) Roles.CreateRole("K");
                //Contrato
                if (!Roles.RoleExists("RC")) Roles.CreateRole("RC");
                if (!Roles.RoleExists("DC")) Roles.CreateRole("DC");
                if (!Roles.RoleExists("IC")) Roles.CreateRole("IC");
                //Horizontal
                if (!Roles.RoleExists("RH")) Roles.CreateRole("RH");
                if (!Roles.RoleExists("DH")) Roles.CreateRole("DH");
                if (!Roles.RoleExists("IH")) Roles.CreateRole("IH");
                //Cliente
                if (!Roles.RoleExists("RL")) Roles.CreateRole("RL");
                if (!Roles.RoleExists("DL")) Roles.CreateRole("DL");
                if (!Roles.RoleExists("IL")) Roles.CreateRole("IL");
                //Cualificador Qn
                if (!Roles.RoleExists("RQN")) Roles.CreateRole("RQN");
                if (!Roles.RoleExists("DQN")) Roles.CreateRole("DQN");
                if (!Roles.RoleExists("IQN")) Roles.CreateRole("IQN");
                //Cualificador Q1
                if (!Roles.RoleExists("RQ1")) Roles.CreateRole("RQ1");
                if (!Roles.RoleExists("DQ1")) Roles.CreateRole("DQ1");
                if (!Roles.RoleExists("IQ1")) Roles.CreateRole("IQ1");
                //Cualificador Q2
                if (!Roles.RoleExists("RQ2")) Roles.CreateRole("RQ2");
                if (!Roles.RoleExists("DQ2")) Roles.CreateRole("DQ2");
                if (!Roles.RoleExists("IQ2")) Roles.CreateRole("IQ2");
                //Cualificador Q3
                if (!Roles.RoleExists("RQ3")) Roles.CreateRole("RQ3");
                if (!Roles.RoleExists("DQ3")) Roles.CreateRole("DQ3");
                if (!Roles.RoleExists("IQ3")) Roles.CreateRole("IQ3");
                //Cualificador Q4
                if (!Roles.RoleExists("RQ4")) Roles.CreateRole("RQ4");
                if (!Roles.RoleExists("DQ4")) Roles.CreateRole("DQ4");
                if (!Roles.RoleExists("IQ4")) Roles.CreateRole("IQ4");

                //Consulta de CVs -> CVT 
                if (!Roles.RoleExists("ACV")) Roles.CreateRole("ACV");
                if (!Roles.RoleExists("ECV")) Roles.CreateRole("ECV");
                if (!Roles.RoleExists("CCV")) Roles.CreateRole("CCV");

                //GESTOR DE ALERTAS
                if (!Roles.RoleExists("GA")) Roles.CreateRole("GA");
                #endregion
                //Se borran los roles que pudiera tener el usuario.
                foreach (string Rol in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
                {
                    if (HttpContext.Current.User.IsInRole(Rol))
                        Roles.RemoveUserFromRole(HttpContext.Current.User.Identity.Name.ToString(), Rol);
                }
                if (sUsuario != "")
                {
                    if (!HttpContext.Current.User.IsInRole("T"))
                        Roles.AddUserToRole(sUserWindows, "T");
                }
                if (sAdmin != "")
                {
                    if (!HttpContext.Current.User.IsInRole("A"))
                        Roles.AddUserToRole(sUserWindows, "A");
                }
                if (sAdminPer != "")
                {
                    if (!HttpContext.Current.User.IsInRole("PER"))
                        Roles.AddUserToRole(sUserWindows, "PER");
                }
                if (sAdminCVT != "")
                {
                    if (!HttpContext.Current.User.IsInRole("ACV"))
                        Roles.AddUserToRole(sUserWindows, "ACV");
                }

                if ((bool)HttpContext.Current.Session["MULTIUSUARIO"])
                {
                    if (!HttpContext.Current.User.IsInRole("USUMULT"))
                        Roles.AddUserToRole(sUserWindows, "USUMULT");
                }

                if (Recurso.bPerteneceDIS(null, (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"]) && !HttpContext.Current.User.IsInRole("DIS"))
                    Roles.AddUserToRole(sUserWindows, "DIS");

                //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                //{
                //    if (!HttpContext.Current.User.IsInRole("REC"))
                //        Roles.AddUserToRole(sUserWindows, "REC");
                //}
                //En principio solo permitimos reconexión a administradores de producción y de CVT (+ DIS)
                if (HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "A" ||
                    HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "SA" ||
                    HttpContext.Current.Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() == "A")
                {
                    if (!HttpContext.Current.User.IsInRole("REC"))
                        Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "REC");
                }


                SqlParameter[] aParam = new SqlParameter[2];
                int i = 0;
                aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, (sUsuario == "") ? 0 : int.Parse(sUsuario));
                aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, int.Parse(sFicepi));
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURAS", aParam);
                while (dr.Read())
                {
                    if (!HttpContext.Current.User.IsInRole(dr["figura"].ToString()))
                    {
                        Roles.AddUserToRole(sUserWindows, dr["figura"].ToString());
                    }
                }
                dr.Close();
                dr.Dispose();
            }
            catch //(Exception e)
            {
                //miLog.put("Error al cargar los roles de un foraneo." + e.Message);
            }
            #endregion
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los datos de un recurso de la tabla T314_USUARIO
        /// y devuelve una instancia u objeto del tipo Recurso
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static Recurso Obtener(int Num_Empleado)
        {
            Recurso o = new Recurso();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
            aParam[0].Value = Num_Empleado;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_RECURSO_S", aParam);

            if (dr.Read())
            {
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.IdUsuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["cod_empresa"] != DBNull.Value)
                    o.CodEmpresa = int.Parse(dr["cod_empresa"].ToString());
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.idNodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t314_costehora"] != DBNull.Value)
                    o.CosteHora = decimal.Parse(dr["t314_costeHora"].ToString());
                if (dr["t314_costejornada"] != DBNull.Value)
                    o.CosteJornada = decimal.Parse(dr["t314_costejornada"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato del recurso"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static Recurso GetDatos(int idFicepi)
        {
            Recurso o = new Recurso();
            SqlParameter[] aParam = new SqlParameter[]{
				ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi)
            };
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_S3", aParam);
            if (dr.Read())
            {
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.IdUsuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t314_crp"] != DBNull.Value)
                    o.CRP = (bool)dr["t314_crp"];
                if (dr["t001_sexo"] != DBNull.Value)
                    o.sSexo = dr["t001_sexo"].ToString();
                
                if (dr["t399_figura"].ToString() != "")
                {
                    switch (dr["t399_figura"].ToString())
                    {
                        case "A":
                        case "S":
                            o.esAdminPC =true;
                            o.AdminPC = dr["t399_figura"].ToString();
                            break;
                        case "P":
                            o.AdminPer = dr["t399_figura"].ToString();
                            break;
                    }
                }
                o.Nombre = dr["Profesional"].ToString();
                o.DesNodo = dr["DesNodo"].ToString();
                if (dr["baja"].ToString() == "1")
                    o.baja = true;
                else
                    o.baja = false;
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato del recurso"));
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader ObtenerUsuarios(string sCodRed)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIDred", SqlDbType.VarChar, 15);
            aParam[0].Value = sCodRed;

            return SqlHelper.ExecuteSqlDataReader("SUP_SELECCIONUSUARIO", aParam);
        }
        public static int ObtenerCountUsuarios(string sCodRed)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sCodRed", SqlDbType.VarChar, 15);
            aParam[0].Value = sCodRed;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIOCOUNT", aParam));
        }
        public static SqlDataReader ObtenerCRsAcceso(int NumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            return SqlHelper.ExecuteSqlDataReader("PSP_CRUSUARIO", aParam);
        }

        /*public static SqlDataReader ObtenerRelacionTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.VarChar, 2);
            aParam[1] = new SqlParameter("@sValor1", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@sValor2", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@sValor3", SqlDbType.VarChar, 50);

            aParam[0].Value = strOpcion;
            aParam[1].Value = strValor1;
            aParam[2].Value = strValor2;
            aParam[3].Value = strValor3;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREATECNICOS", aParam);
        }
        */
        public static SqlDataReader GetUsuariosPSN(string strOpcion, string strValor1, string strValor2, string strValor3,
                                                string sCodUne, string idPSN, string sCualidad, string sIdTarea)
        {
            SqlDataReader dr;
            //Miro si el proyectosubnodo permite asignar técnicos desde PST que no estén asociados al proyecto
            bool bAdmite = PROYECTOSUBNODO.GetAdmiteRecursoPST(null, int.Parse(idPSN));
            if (bAdmite)
                dr=ObtenerRelacionProfesionalesTarifa(strOpcion, strValor1, strValor2, strValor3, sCodUne, idPSN, sCualidad, sIdTarea, true);
            else
                dr=GetUsersPSN(strOpcion, strValor1, strValor2, strValor3, sCodUne, idPSN, sCualidad, sIdTarea);
            return dr;
        }
        public static SqlDataReader ObtenerRelacionProfesionalesTarifa(string strOpcion, string strValor1, string strValor2, string strValor3,
                                                           string sCodUne, string t305_idProyectoSubnodo, string sCualidad, string sIdTarea, bool bSoloActivos)
        {//Obtiene la relación de técnicos según parametro
            //Si strOpcion=N es una busqueda por nombre
            //Si strOpcion=C es una busqueda por CR
            //Si strOpcion=G es una busqueda por Grupo Funcional
            //Si strOpcion=P es una busqueda por recursos asociados al proyecto económico
            //Si strOpcion=T es una busqueda por recursos asociados al proyecto técnico
            switch (strOpcion)
            {
                case "N":
                    SqlParameter[] aParam1 = new SqlParameter[9];
                    aParam1[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
                    aParam1[0].Value = strValor1;
                    aParam1[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
                    aParam1[1].Value = strValor2;
                    aParam1[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                    aParam1[2].Value = strValor3;
                    aParam1[3] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    if (sCodUne == "") aParam1[3].Value = DBNull.Value;
                    else aParam1[3].Value = Convert.ToInt32(sCodUne);
                    aParam1[4] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam1[4].Value = DBNull.Value;
                    else aParam1[4].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam1[5] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam1[5].Value = sCualidad;
                    aParam1[6] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam1[6].Value = DBNull.Value;
                    else aParam1[6].Value = Convert.ToInt32(sIdTarea);
                    aParam1[7] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam1[7].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    aParam1[8] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam1[8].Value = bSoloActivos;

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_NOMBRE", aParam1);

                case "C":
                    SqlParameter[] aParam2 = new SqlParameter[3];
                    aParam2[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    //aParam2[0].Value = Convert.ToInt32(sCodUne);
                    aParam2[0].Value = Convert.ToInt32(strValor1);
                    aParam2[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam2[1].Value = DBNull.Value;
                    else aParam2[1].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam2[2] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam2[2].Value = DBNull.Value;
                    else aParam2[2].Value = Convert.ToInt32(sIdTarea);

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_NODO", aParam2);

                case "G":
                    SqlParameter[] aParam3 = new SqlParameter[6];
                    aParam3[0] = new SqlParameter("@nGF", SqlDbType.Int, 4);
                    aParam3[0].Value = Convert.ToInt32(strValor1);
                    aParam3[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam3[1].Value = Convert.ToInt32(sCodUne);
                    aParam3[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam3[2].Value = DBNull.Value;
                    else aParam3[2].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam3[3] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam3[3].Value = sCualidad;
                    aParam3[4] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam3[4].Value = DBNull.Value;
                    else aParam3[4].Value = Convert.ToInt32(sIdTarea);
                    aParam3[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam3[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_GF", aParam3);

                case "P":
                    SqlParameter[] aParam4 = new SqlParameter[3];
                    aParam4[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam4[0].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam4[1] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam4[1].Value = DBNull.Value;
                    else aParam4[1].Value = Convert.ToInt32(sIdTarea);
                    aParam4[2] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam4[2].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_PSN", aParam4);

                case "T":
                    SqlParameter[] aParam5 = new SqlParameter[2];
                    aParam5[0] = new SqlParameter("@nPT", SqlDbType.Int, 4);
                    aParam5[0].Value = Convert.ToInt32(strValor1);
                    aParam5[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam5[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_PT", aParam5);

                case "O":
                    SqlParameter[] aParam6 = new SqlParameter[2];
                    aParam6[0] = new SqlParameter("@idOficina", SqlDbType.Int, 4);
                    if (strValor1 == "") aParam6[0].Value = DBNull.Value;
                    else aParam6[0].Value = Convert.ToInt32(strValor1);

                    aParam6[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam6[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_OFICINA", aParam6);

            }

            throw (new Exception("Se ha pasado un parámetro de búsqueda incorrecto '" + strOpcion +"'"));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Es similar a ObtenerRelacionProfesionalesTarifa pero solo saca usuarios que ya estén asignados al Proyecto Económico
        /// Se usa en las pantallas de asignación de técnicos a items de la estructura técnica (PT, F, A, T y masiva) cuando
        /// el proyecto económico no permite asignar desde la parte técnica a recursos que no estén asignados al proyecto
        /// Obtiene los datos de los profesionales en función de los filtros establecidos para
        /// su asignación al proyecto económico.
        /// Si strOpcion=N es una busqueda por nombre
        /// Si strOpcion=C es una busqueda por CR
        /// Si strOpcion=G es una busqueda por Grupo Funcional
        /// Si strOpcion=P es una busqueda por recursos asociados al proyecto económico
        /// Si strOpcion=T es una busqueda por recursos asociados al proyecto técnico
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetUsersPSN(string strOpcion, string strValor1, string strValor2, string strValor3,
                                                string sCodUne, string t305_idProyectoSubnodo, string sCualidad, string sIdTarea)
        {
            switch (strOpcion)
            {
                case "N":
                    SqlParameter[] aParam1 = new SqlParameter[8];
                    aParam1[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
                    aParam1[0].Value = strValor1;
                    aParam1[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
                    aParam1[1].Value = strValor2;
                    aParam1[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                    aParam1[2].Value = strValor3;
                    aParam1[3] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    if (sCodUne == "") aParam1[3].Value = DBNull.Value;
                    else aParam1[3].Value = Convert.ToInt32(sCodUne);
                    aParam1[4] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam1[4].Value = DBNull.Value;
                    else aParam1[4].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam1[5] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam1[5].Value = sCualidad;
                    aParam1[6] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam1[6].Value = DBNull.Value;
                    else aParam1[6].Value = Convert.ToInt32(sIdTarea);
                    aParam1[7] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam1[7].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_NOMBRE2", aParam1);

                case "C":
                    SqlParameter[] aParam2 = new SqlParameter[3];
                    aParam2[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam2[0].Value = Convert.ToInt32(sCodUne);
                    aParam2[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam2[1].Value = DBNull.Value;
                    else aParam2[1].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam2[2] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam2[2].Value = DBNull.Value;
                    else aParam2[2].Value = Convert.ToInt32(sIdTarea);

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_NODO2", aParam2);

                case "G":
                    SqlParameter[] aParam3 = new SqlParameter[6];
                    aParam3[0] = new SqlParameter("@nGF", SqlDbType.Int, 4);
                    aParam3[0].Value = Convert.ToInt32(strValor1);
                    aParam3[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam3[1].Value = Convert.ToInt32(sCodUne);
                    aParam3[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    if (t305_idProyectoSubnodo == "") aParam3[2].Value = DBNull.Value;
                    else aParam3[2].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam3[3] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam3[3].Value = sCualidad;
                    aParam3[4] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam3[4].Value = DBNull.Value;
                    else aParam3[4].Value = Convert.ToInt32(sIdTarea);
                    aParam3[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam3[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_GF2", aParam3);

                case "P":
                    SqlParameter[] aParam4 = new SqlParameter[3];
                    aParam4[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam4[0].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam4[1] = new SqlParameter("@idTarea", SqlDbType.Int, 4);
                    if (sIdTarea == "") aParam4[1].Value = DBNull.Value;
                    else aParam4[1].Value = Convert.ToInt32(sIdTarea);
                    aParam4[2] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam4[2].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_PSN", aParam4);

                case "T":
                    SqlParameter[] aParam5 = new SqlParameter[2];
                    aParam5[0] = new SqlParameter("@nPT", SqlDbType.Int, 4);
                    aParam5[0].Value = Convert.ToInt32(strValor1);
                    aParam5[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam5[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_TARIFA_PT", aParam5);
            }

            throw (new Exception("Se ha pasado un parámetro de búsqueda incorrecto '" + strOpcion + "'"));
        }

        public static SqlDataReader Catalogo(string sApellido1, string sApellido2, string sNombre, bool bForaneos)
        {//Obtención del catalogo de personas susceptibles de ser integrantes
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAP1", SqlDbType.VarChar, 50);
            aParam[0].Value = sApellido1;
            aParam[1] = new SqlParameter("@sAP2", SqlDbType.VarChar, 50);
            aParam[1].Value = sApellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[3].Value = bForaneos;
            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL", aParam);
        }
        public static SqlDataReader CatalogoPerfil(string strOpcion, string sApellido1, string sApellido2, string sNombre)
        {//Obtención del catalogo de personas susceptibles de ser integrantes
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.VarChar, 50);
            aParam[0].Value = strOpcion;
            aParam[1] = new SqlParameter("@sValor1", SqlDbType.VarChar, 50);
            aParam[1].Value = sApellido1;
            aParam[2] = new SqlParameter("@sValor2", SqlDbType.VarChar, 50);
            aParam[2].Value = sApellido2;
            aParam[3] = new SqlParameter("@sValor3", SqlDbType.VarChar, 50);
            aParam[3].Value = sNombre;
            aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[4].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOS_PERFIL", aParam);
        }
        /// <summary>
        /// Obtiene los usuarios SUPER que tienen alguna de las figuras indicada en la tabla que se pasa como parametro
        /// La tabla tiene 2 campos nNivel y sFigura que pueden tener los siguiente valores
        /// nNivel: 1->SNN4, 2->SNN3 (negocio), 3->SNN2 (linea), 4->SNN1 (unidad), 5-> NODO, 6->SUBNODO, 7->PROYECTO
        ///         8->CONTRATO, 9->HORIZONTAL, 10->CLIENTE, 11->OT, 12->Resp. GF, 13->Resp.Qn, 14->Resp.Q1, 15->Resp.Q2
        /// sFigura: D->Delegado, C->Colaborador, I->Invitado, R->Responsable, OT->Miembro de Oficina Técnica, G->Gestor
        ///          RG->Responsable Grupo Funcional, P->RIA, T->SAA, L->SAT, M->RTPE, K->RTPT, S->Asistente, B->Bitacórico
        ///          J->Jefe
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="dtFiguras"></param>
        /// <returns></returns>
        public static SqlDataReader GetUsuariosPorFigura(SqlTransaction tr, DataTable dtFiguras)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@TFIG", SqlDbType.Structured, dtFiguras)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_FIGURAS", 300, aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROFESIONALES_FIGURAS", 300, aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los datos de los profesionales en función de los filtros establecidos para
        /// su asignación al proyecto económico.
        ///Si strOpcion=N es una busqueda por nombre
        ///Si strOpcion=C es una busqueda por CR
        ///Si strOpcion=G es una busqueda por Grupo Funcional
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader ObtenerRelacionProfesionalesCoste(string strOpcion, string strValor1, string strValor2, string strValor3, 
                                                        int nCodUne, string sModeloCoste, string sCualidad, int t305_idproyectosubnodo,
                                                        int nNodoPSN)
        {
            bool bPermitirForaneos = (bool)HttpContext.Current.Session["FORANEOS"];
            switch (strOpcion)
            {
                case "N":
                    SqlParameter[] aParam1 = new SqlParameter[9];
                    aParam1[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
                    aParam1[0].Value = strValor1;
                    aParam1[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
                    aParam1[1].Value = strValor2;
                    aParam1[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                    aParam1[2].Value = strValor3;
                    aParam1[3] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam1[3].Value = nCodUne;
                    aParam1[4] = new SqlParameter("@sModeloCoste", SqlDbType.Char, 1);
                    aParam1[4].Value = sModeloCoste;
                    aParam1[5] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam1[5].Value = sCualidad;
                    aParam1[6] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam1[6].Value = t305_idproyectosubnodo;
                    aParam1[7] = new SqlParameter("@nNodoPSN", SqlDbType.Int, 4);
                    aParam1[7].Value = nNodoPSN;
                    aParam1[8] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam1[8].Value = bPermitirForaneos;
                    
                    return SqlHelper.ExecuteSqlDataReader("SUP_PROF_ASIG_NOMBRE", aParam1);

                case "C":
                    SqlParameter[] aParam2 = new SqlParameter[4];
                    aParam2[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam2[0].Value = nCodUne;
                    aParam2[1] = new SqlParameter("@sModeloCoste", SqlDbType.Char, 1);
                    aParam2[1].Value = sModeloCoste;
                    aParam2[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam2[2].Value = t305_idproyectosubnodo;
                    aParam2[3] = new SqlParameter("@nNodoPSN", SqlDbType.Int, 4);
                    aParam2[3].Value = nNodoPSN;

                    return SqlHelper.ExecuteSqlDataReader("SUP_PROF_ASIG_NODO", aParam2);

                case "G":
                    SqlParameter[] aParam3 = new SqlParameter[7];
                    aParam3[0] = new SqlParameter("@nGF", SqlDbType.Int, 4);
                    aParam3[0].Value = Convert.ToInt32(strValor1);
                    aParam3[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam3[1].Value = nCodUne;
                    aParam3[2] = new SqlParameter("@sModeloCoste", SqlDbType.Char, 1);
                    aParam3[2].Value = sModeloCoste;
                    aParam3[3] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
                    aParam3[3].Value = sCualidad;
                    aParam3[4] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam3[4].Value = t305_idproyectosubnodo;
                    aParam3[5] = new SqlParameter("@nNodoPSN", SqlDbType.Int, 4);
                    aParam3[5].Value = nNodoPSN;
                    aParam3[6] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                    aParam3[6].Value = bPermitirForaneos;

                    return SqlHelper.ExecuteSqlDataReader("SUP_PROF_ASIG_GF", aParam3);
            }

            throw (new Exception("Se ha pasado un parámetro de búsqueda incorrecto '" + strOpcion + "'"));
        }

        public static SqlDataReader GetProfesionales(string strOpcion, string strValor1, string strValor2, string strValor3,
                                                     string t305_idProyectoSubnodo, string sTiposProfesional, bool bSoloActivos)
        {//Obtiene la relación de técnicos según parametro
            //Si strOpcion=N es una busqueda por nombre
            //Si strOpcion=C es una busqueda por CR
            //Si strOpcion=G es una busqueda por Grupo Funcional
            //Si strOpcion=P es una busqueda por recursos asociados al proyecto económico
            //Si strOpcion=O es una busqueda por oficina
            switch (strOpcion)
            {
                case "N":
                    SqlParameter[] aParam1 = new SqlParameter[5];
                    aParam1[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
                    aParam1[0].Value = strValor1;
                    aParam1[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
                    aParam1[1].Value = strValor2;
                    aParam1[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                    aParam1[2].Value = strValor3;

                    aParam1[3] = new SqlParameter("@TiposProfesional", SqlDbType.VarChar, 50);
                    aParam1[3].Value = sTiposProfesional;

                    aParam1[4] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam1[4].Value = bSoloActivos;

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_NOMBRE_CAT", aParam1);

                case "C":
                    SqlParameter[] aParam2 = new SqlParameter[3];
                    aParam2[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    aParam2[0].Value = Convert.ToInt32(strValor1);

                    aParam2[1] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam2[1].Value = bSoloActivos;

                    aParam2[2] = new SqlParameter("@TiposProfesional", SqlDbType.VarChar, 50);
                    aParam2[2].Value = sTiposProfesional;


                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_NODO_CAT", aParam2);

                case "G":
                    SqlParameter[] aParam3 = new SqlParameter[3];
                    aParam3[0] = new SqlParameter("@nGF", SqlDbType.Int, 4);
                    aParam3[0].Value = Convert.ToInt32(strValor1);

                    //aParam3[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
                    //aParam3[1].Value = Convert.ToInt32(sCodUne);

                    aParam3[1] = new SqlParameter("@TiposProfesional", SqlDbType.VarChar, 50);
                    aParam3[1].Value = sTiposProfesional;

                    aParam3[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam3[2].Value = bSoloActivos;


                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_GF_CAT", aParam3);

                case "P":
                    SqlParameter[] aParam4 = new SqlParameter[3];
                    aParam4[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
                    aParam4[0].Value = Convert.ToInt32(t305_idProyectoSubnodo);
                    aParam4[1] = new SqlParameter("@TiposProfesional", SqlDbType.VarChar, 50);
                    aParam4[1].Value = sTiposProfesional;
                    aParam4[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam4[2].Value = bSoloActivos;

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_PSN_CAT", aParam4);

                case "O":
                    SqlParameter[] aParam6 = new SqlParameter[3];
                    aParam6[0] = new SqlParameter("@idOficina", SqlDbType.Int, 4);
                    if (strValor1 == "") aParam6[0].Value = DBNull.Value;
                    else aParam6[0].Value = Convert.ToInt32(strValor1);

                    aParam6[1] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                    aParam6[1].Value = bSoloActivos;

                    aParam6[2] = new SqlParameter("@TiposProfesional", SqlDbType.VarChar, 50);
                    aParam6[2].Value = sTiposProfesional;

                    return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_OFICINA_CAT", aParam6);

            }

            throw (new Exception("Se ha pasado un parámetro de búsqueda incorrecto '" + strOpcion + "'"));
        }

        public static int ActualizarCalendario(SqlTransaction tr, int num_empleado, int t066_idcal)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
            aParam[1].Value = t066_idcal;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_RECURSOCAL_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECURSOCAL_U", aParam);
        }
        //public static SqlDataReader ObtenerProfesionales(string sOrigen, string sAp1, string sAp2, string sNombre)
        public static SqlDataReader ObtenerProfesionales(string sAp1, string sAp2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            //aParam[0] = new SqlParameter("@sOrigen", SqlDbType.Char, 1);
            aParam[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);

            //aParam[0].Value = sOrigen;
            aParam[0].Value = sAp1;
            aParam[1].Value = sAp2;
            aParam[2].Value = sNombre;
            aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES", aParam);
        }

        public static SqlDataReader ObtenerProfesionalesReconexion(string sAp1, string sAp2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            //aParam[0] = new SqlParameter("@sOrigen", SqlDbType.Char, 1);
            aParam[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);

            //aParam[0].Value = sOrigen;
            aParam[0].Value = sAp1;
            aParam[1].Value = sAp2;
            aParam[2].Value = sNombre;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_RECONEXION", aParam);
        }

        public static SqlDataReader CatalogoTareas(SqlTransaction tr, int nIdRecurso, string sTipoItem, int nUserActual, bool bSoloActivos)
        {
            string sProcAlm = "SUP_RECURSO_TAREAS";
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = nIdRecurso;
            aParam[1] = new SqlParameter("@nUserActual", SqlDbType.Int, 4);
            aParam[1].Value = nUserActual;
            aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
            aParam[2].Value = bSoloActivos;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                switch (sTipoItem)
                {
                    case "T":
                        sProcAlm = "SUP_RECURSO_TAREAS_ADMIN";
                        break;
                    case "A":
                        sProcAlm = "SUP_RECURSO_ACTIV_ADMIN";
                        break;
                    case "F":
                        sProcAlm = "SUP_RECURSO_FASE_ADMIN";
                        break;
                    case "P":
                        sProcAlm = "SUP_RECURSO_PT_ADMIN";
                        break;
                    case "E":
                        sProcAlm = "SUP_RECURSO_PE_ADMIN";
                        break;
                    case "E2":
                        sProcAlm = "SUP_RECURSO_PE2_ADMIN";
                        break;
                }
            }
            else
            {
                switch (sTipoItem)
                {
                    case "T":
                        sProcAlm = "SUP_RECURSO_TAREAS";
                        break;
                    case "A":
                        sProcAlm = "SUP_RECURSO_ACTIV";
                        break;
                    case "F":
                        sProcAlm = "SUP_RECURSO_FASE";
                        break;
                    case "P":
                        sProcAlm = "SUP_RECURSO_PT";
                        break;
                    case "E":
                        sProcAlm = "SUP_RECURSO_PE";
                        break;
                    case "E2":
                        sProcAlm = "SUP_RECURSO_PE2";
                        break;
                }
            }
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);

            return dr;
        }

        public static bool bEsRGF(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            int nResul = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_RGF", aParam));

            if (nResul > 0) return true;
            else return false;
        }
        public static string CodigoRed(int NumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@iEmpleado", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            return (string)SqlHelper.ExecuteScalar("SUP_CODRED2", aParam);
        }

        #region Métodos para calcular el grado de ocupación
        public static DataSet OcupacionProfesional(int intNumEmpleado, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = intNumEmpleado;

            return SqlHelper.ExecuteDataset("SUP_OCUPACION_PROFESIONAL", aParam);
        }
        public static DataSet OcupacionProfesionalNodo(int iCR, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = iCR;

            return SqlHelper.ExecuteDataset("SUP_OCUPACION_NODO", aParam);
        }
        public static DataSet OcupacionProfesionalGF(int iGF, DateTime dtDesde, DateTime dtHasta)//int iCR, 
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            //aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@cod_gf", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            //aParam[2].Value = iCR;
            aParam[2].Value = iGF;

            return SqlHelper.ExecuteDataset("SUP_OCUPACION_GF", aParam);
        }
        public static DataSet OcupacionProfesionalPE(int nPSN, DateTime dtDesde, DateTime dtHasta)//int iCR, 
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);//t305_idproyectosubnodo

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = nPSN;

            return SqlHelper.ExecuteDataset("SUP_OCUPACION_PE", aParam);
        }
        public static DataSet OcupacionProfesionalFU(int iFuncion, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            //aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@cod_fu", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            //aParam[2].Value = iCR;
            aParam[2].Value = iFuncion;

            return SqlHelper.ExecuteDataset("SUP_OCUPACION_FU", aParam);
        }

        public static SqlDataReader OcupacionProfesional2(int intNumEmpleado, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = intNumEmpleado;

            return SqlHelper.ExecuteSqlDataReader("SUP_OCUPACION_PROFESIONAL2", aParam);
        }
        public static SqlDataReader OcupacionProfesionalNodo2(int iCR, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = iCR;

            return SqlHelper.ExecuteSqlDataReader("SUP_OCUPACION_NODO2", aParam);
        }
        public static SqlDataReader OcupacionProfesionalGF2(int iGF, DateTime dtDesde, DateTime dtHasta)//int iCR, 
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            //aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@cod_gf", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            //aParam[2].Value = iCR;
            aParam[2].Value = iGF;

            return SqlHelper.ExecuteSqlDataReader("SUP_OCUPACION_GF2", aParam);
        }
        public static SqlDataReader OcupacionProfesionalPE2(int nPSN, DateTime dtDesde, DateTime dtHasta)//int iCR, 
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@nPSN", SqlDbType.Int, 4);//t305_idproyectosubnodo

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            aParam[2].Value = nPSN;

            return SqlHelper.ExecuteSqlDataReader("SUP_OCUPACION_PE2", aParam);
        }
        public static SqlDataReader OcupacionProfesionalFU2(int iFuncion, DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime, 8);
            aParam[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime, 8);
            //aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@cod_fu", SqlDbType.Int, 4);

            aParam[0].Value = dtDesde;
            aParam[1].Value = dtHasta;
            //aParam[2].Value = iCR;
            aParam[2].Value = iFuncion;

            return SqlHelper.ExecuteSqlDataReader("SUP_OCUPACION_FU2", aParam);
        }
        #endregion
        #region Métodos para calcular el calendario de ocupación
        public static DataSet OcupacionProfesionalCalendario(int intNumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = intNumEmpleado;
            
            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_PROFESIONAL", aParam);
        }
        public static DataSet OcupacionProfesionalCalendarioNodo(int iCR)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = iCR;

            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_NODO", aParam);
        }
        public static DataSet OcupacionProfesionalCalendarioGF(int iGF)//int iCR, 
        {
            SqlParameter[] aParam = new SqlParameter[1];
            //aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@cod_gf", SqlDbType.Int, 4);

            //aParam[0].Value = iCR;
            aParam[0].Value = iGF;

            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_GF", aParam);
        }
        public static DataSet OcupacionProfesionalCalendarioPE(int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_PE", aParam);
        }
        public static DataSet OcupacionProfesionalCalendarioFU(int iFuncion)//int t303_idnodo, 
        {
            SqlParameter[] aParam = new SqlParameter[1];
            //aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@cod_fu", SqlDbType.Int, 4);

            //aParam[0].Value = t303_idnodo;
            aParam[0].Value = iFuncion;

            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_FU", aParam);
        }
        /*
                public static SqlDataReader OcupacionProfesionalCalendario2(int intNumEmpleado)
                {
                    SqlParameter[] aParam = new SqlParameter[1];
                    aParam[0] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
                    aParam[0].Value = intNumEmpleado;
                    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_OCUP_CAL_PROFESIONAL2", aParam);
                    return dr;
                }
                public static SqlDataReader OcupacionProfesionalCalendarioCR2(int iCR)
                {
                    SqlParameter[] aParam = new SqlParameter[1];
                    aParam[0] = new SqlParameter("@cod_une", SqlDbType.Int, 4);

                    aParam[0].Value = iCR;

                    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_OCUP_CAL_CR2", aParam);
                    return dr;
                }
                public static SqlDataReader OcupacionProfesionalCalendarioGF2(int iCR, int iGF)
                {
                    SqlParameter[] aParam = new SqlParameter[2];
                    aParam[0] = new SqlParameter("@cod_une", SqlDbType.Int, 4);
                    aParam[1] = new SqlParameter("@cod_gf", SqlDbType.Int, 4);

                    aParam[0].Value = iCR;
                    aParam[1].Value = iGF;

                    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_OCUP_CAL_GF2", aParam);
                    return dr;
                }
                public static SqlDataReader OcupacionProfesionalCalendarioPE2(int iCR, int iPE)
                {
                    SqlParameter[] aParam = new SqlParameter[2];
                    aParam[0] = new SqlParameter("@cod_une", SqlDbType.Int, 4);
                    aParam[1] = new SqlParameter("@cod_pe", SqlDbType.Int, 4);

                    aParam[0].Value = iCR;
                    aParam[1].Value = iPE;

                    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_OCUP_CAL_PE2", aParam);
                    return dr;
                }
                public static SqlDataReader OcupacionProfesionalCalendarioFU2(int iCR, int iFuncion)
                {
                    SqlParameter[] aParam = new SqlParameter[2];
                    aParam[0] = new SqlParameter("@cod_une", SqlDbType.Int, 4);
                    aParam[1] = new SqlParameter("@cod_fu", SqlDbType.Int, 4);

                    aParam[0].Value = iCR;
                    aParam[1].Value = iFuncion;

                    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_OCUP_CAL_FU2", aParam);
                    return dr;
                }
         */
        //Métodos para calcular el desglose de tareas de ocupación
        public static DataSet OcupacionProfesionalTarea(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            return SqlHelper.ExecuteDataset("SUP_OCUP_CAL_PROF_TAREA", aParam);
        }

        #endregion

        public static SqlDataReader ObtenerUsuariosConectados(string sCODRED)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sCODRED", SqlDbType.VarChar, 4000);
            aParam[0].Value = sCODRED;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_CODRED", aParam);
        }

        public static int SetBotonCalendario(SqlTransaction tr, int idFicepi, string sBoton)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = idFicepi;
            aParam[1] = new SqlParameter("@t001_botonfecha", SqlDbType.Text, 1);
            aParam[1].Value = sBoton;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("FIC_BOTONFECHA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "FIC_BOTONFECHA_U", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene si un recurso está asignado a un proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool Asignado(int NumEmpleado, int nPSN)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;

            int nResul = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_RECURSO_PSN", aParam));

            if (nResul > 0) bRes = true;
            else bRes = false;

            return bRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene si un recurso es asignable a un proyecto desde PST
        /// Lo usamos para la asignación de recursos desde la importación de OpenProj
        /// Devuelve OK si el recurso se puede asignar o una indicación de porqué no se puede asignar
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static string Asignable(int NumEmpleado, int nPSN, bool bPermiteRecursoPST)
        {
            string sRes = "OK";
            string sCodRed = "";
            if (bPermiteRecursoPST)
            {
                sCodRed=CodigoRed(NumEmpleado);
                if (sCodRed == null)
                    sRes = "El empleado " + NumEmpleado.ToString() + " no se puede asignar al proyecto. Profesional no encontrado.\n";
                else
                {
                    if (sCodRed == "")
                        sRes = "El empleado " + NumEmpleado.ToString() + " no se puede asignar al proyecto. Profesional no encontrado.\n";

                    else
                    {
                        string sCualidad = PROYECTOSUBNODO.GetCualidad(null,nPSN);
                        if (sCualidad != "C" && sCualidad != "P")
                            sRes = "El empleado " + NumEmpleado.ToString() + " no se puede asignar. La cualidad del proyecto no lo permite.\n";
                        else
                        {
                            USUARIO oUser = USUARIO.Select(null,NumEmpleado);
                            int iNodoUser = 1;
                            if (oUser.t303_idnodo != null)
                                iNodoUser=(int)oUser.t303_idnodo;
                            int iNodoPSN = PROYECTOSUBNODO.GetNodo(null, nPSN);
                            if (sCualidad == "P" && iNodoUser != iNodoPSN)
                                sRes = "El empleado " + NumEmpleado.ToString() + " no se puede asignar. No pertenece al CR del proyecto.\n";
                            else
                            {
                                bool bEsRepicable = PROYECTO.EsReplicableByPSN(null, nPSN);
                                if (!bEsRepicable && iNodoUser != iNodoPSN)
                                    sRes = "El empleado " + NumEmpleado.ToString() + " no se puede asignar. No pertenece al CR del proyecto (proyecto no replicable).\n";
                            }
                        }

                    }
                }
            }
            else
            {
                if (!Asignado(NumEmpleado, nPSN))
                    sRes="El empleado " +NumEmpleado.ToString() +" no se puede asignar. El proyecto no permite asignar recursos desde PST.\n";
            }

            return sRes;
        }

        public static bool bPerteneceDIS(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
        }
        /// <summary>
        /// Dado un código de usuario devuleve su código de red ,si lo tiene, y sino la dirección email indicada en T001_FICEPI
        /// </summary>
        /// <param name="NumEmpleado"></param>
        /// <returns></returns>
        public static string GetDireccionMail(int NumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@iEmpleado", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            return (string)SqlHelper.ExecuteScalar("SUP_GET_MAIL", aParam);
        }
        //public static string obtenerCodRed(int idficepi)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@idficepi", SqlDbType.Int, 4);
        //    aParam[0].Value = idficepi;

        //    return (string)SqlHelper.ExecuteScalar("SUP_GET_CODRED", aParam);
        //}
    }
}
