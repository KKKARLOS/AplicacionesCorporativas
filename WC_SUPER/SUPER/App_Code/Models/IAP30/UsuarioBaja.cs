using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class UsuarioBaja
    {

        /// <summary>
        /// Summary description for UsuarioBaja
        /// </summary>
		#region Private Variables
		private Int32 _t001_IDFICEPI;
		private Int32 _t314_idusuario;
		private String _NOMBRE;
		private String _APELLIDO1;
		private String _APELLIDO2;
		private Int16 _T009_IDCENTRAB;
		private String _T009_DESCENTRAB;
		private Int32 _t303_idnodo;
		private Int32 _t303_ultcierreIAP;
		private String _t303_denominacion;
		private String _t399_figura;
		private String _t399_figura_cvt;
		private DateTime _t314_falta;
		private DateTime _t314_fbaja;
		private Boolean _t314_jornadareducida;
		private Single _t314_horasjor_red;
		private DateTime _t314_fdesde_red;
		private DateTime _t314_fhasta_red;
		private Boolean _t314_controlhuecos;
		private DateTime _fUltImputacion;
		private Int32 _IdCalendario;
		private String _desCalendario;
		private Int32 _t066_semlabL;
		private Int32 _t066_semlabM;
		private Int32 _t066_semlabX;
		private Int32 _t066_semlabJ;
		private Int32 _t066_semlabV;
		private Int32 _t066_semlabS;
		private Int32 _t066_semlabD;
		private String _t001_codred;
		private String _t001_sexo;
		private Boolean _t314_crp;
		private Boolean _t314_accesohabilitado;
		private Boolean _t314_diamante;
		private String _tipo;
		private Byte _t314_nsegmb;
		private String _T010_CODWEATHER;
		private String _T010_NOMWEATHER;
		private Boolean _t314_carrusel1024;
		private Boolean _t314_avance1024;
		private Boolean _t314_resumen1024;
		private Boolean _t314_datosres1024;
		private Boolean _t314_fichaeco1024;
		private Boolean _t314_segrenta1024;
		private Boolean _t314_avantec1024;
		private Boolean _t314_estruct1024;
		private Boolean _t314_fotopst1024;
		private Boolean _t314_plant1024;
		private Boolean _t314_const1024;
		private Boolean _t314_iapfact1024;
		private Boolean _t314_iapdiario1024;
		private Boolean _t314_cuadromando1024;
		private Byte _t314_importaciongasvi;
		private Boolean _t314_recibirmails;
		private Boolean _t314_defectoperiodificacion;
		private Boolean _t314_multiventana;
		private String _t422_idmoneda_VDC;
		private String _t422_denominacionimportes_vdc;
		private String _t422_idmoneda_VDP;
		private String _t422_denominacionimportes_vdp;
		private String _t422_denominacionimportes;
		private Boolean _t314_nuevogasvi;
		private Boolean _t314_calculoonline;
		private Boolean _t314_cargaestructura;

		#endregion

		#region Public Properties
		public Int32 t001_IDFICEPI
		{
			get{return _t001_IDFICEPI;}
			set{_t001_IDFICEPI = value;}
		}

		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String NOMBRE
		{
			get{return _NOMBRE;}
			set{_NOMBRE = value;}
		}

		public String APELLIDO1
		{
			get{return _APELLIDO1;}
			set{_APELLIDO1 = value;}
		}

		public String APELLIDO2
		{
			get{return _APELLIDO2;}
			set{_APELLIDO2 = value;}
		}

		public Int16 T009_IDCENTRAB
		{
			get{return _T009_IDCENTRAB;}
			set{_T009_IDCENTRAB = value;}
		}

		public String T009_DESCENTRAB
		{
			get{return _T009_DESCENTRAB;}
			set{_T009_DESCENTRAB = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public Int32 t303_ultcierreIAP
		{
			get{return _t303_ultcierreIAP;}
			set{_t303_ultcierreIAP = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

		public String t399_figura
		{
			get{return _t399_figura;}
			set{_t399_figura = value;}
		}

		public String t399_figura_cvt
		{
			get{return _t399_figura_cvt;}
			set{_t399_figura_cvt = value;}
		}

		public DateTime t314_falta
		{
			get{return _t314_falta;}
			set{_t314_falta = value;}
		}

		public DateTime t314_fbaja
		{
			get{return _t314_fbaja;}
			set{_t314_fbaja = value;}
		}

		public Boolean t314_jornadareducida
		{
			get{return _t314_jornadareducida;}
			set{_t314_jornadareducida = value;}
		}

		public Single t314_horasjor_red
		{
			get{return _t314_horasjor_red;}
			set{_t314_horasjor_red = value;}
		}

		public DateTime t314_fdesde_red
		{
			get{return _t314_fdesde_red;}
			set{_t314_fdesde_red = value;}
		}

		public DateTime t314_fhasta_red
		{
			get{return _t314_fhasta_red;}
			set{_t314_fhasta_red = value;}
		}

		public Boolean t314_controlhuecos
		{
			get{return _t314_controlhuecos;}
			set{_t314_controlhuecos = value;}
		}

		public DateTime fUltImputacion
		{
			get{return _fUltImputacion;}
			set{_fUltImputacion = value;}
		}

		public Int32 IdCalendario
		{
			get{return _IdCalendario;}
			set{_IdCalendario = value;}
		}

		public String desCalendario
		{
			get{return _desCalendario;}
			set{_desCalendario = value;}
		}

		public Int32 t066_semlabL
		{
			get{return _t066_semlabL;}
			set{_t066_semlabL = value;}
		}

		public Int32 t066_semlabM
		{
			get{return _t066_semlabM;}
			set{_t066_semlabM = value;}
		}

		public Int32 t066_semlabX
		{
			get{return _t066_semlabX;}
			set{_t066_semlabX = value;}
		}

		public Int32 t066_semlabJ
		{
			get{return _t066_semlabJ;}
			set{_t066_semlabJ = value;}
		}

		public Int32 t066_semlabV
		{
			get{return _t066_semlabV;}
			set{_t066_semlabV = value;}
		}

		public Int32 t066_semlabS
		{
			get{return _t066_semlabS;}
			set{_t066_semlabS = value;}
		}

		public Int32 t066_semlabD
		{
			get{return _t066_semlabD;}
			set{_t066_semlabD = value;}
		}

		public String t001_codred
		{
			get{return _t001_codred;}
			set{_t001_codred = value;}
		}

		public String t001_sexo
		{
			get{return _t001_sexo;}
			set{_t001_sexo = value;}
		}

		public Boolean t314_crp
		{
			get{return _t314_crp;}
			set{_t314_crp = value;}
		}

		public Boolean t314_accesohabilitado
		{
			get{return _t314_accesohabilitado;}
			set{_t314_accesohabilitado = value;}
		}

		public Boolean t314_diamante
		{
			get{return _t314_diamante;}
			set{_t314_diamante = value;}
		}

		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}

		public Byte t314_nsegmb
		{
			get{return _t314_nsegmb;}
			set{_t314_nsegmb = value;}
		}

		public String T010_CODWEATHER
		{
			get{return _T010_CODWEATHER;}
			set{_T010_CODWEATHER = value;}
		}

		public String T010_NOMWEATHER
		{
			get{return _T010_NOMWEATHER;}
			set{_T010_NOMWEATHER = value;}
		}

		public Boolean t314_carrusel1024
		{
			get{return _t314_carrusel1024;}
			set{_t314_carrusel1024 = value;}
		}

		public Boolean t314_avance1024
		{
			get{return _t314_avance1024;}
			set{_t314_avance1024 = value;}
		}

		public Boolean t314_resumen1024
		{
			get{return _t314_resumen1024;}
			set{_t314_resumen1024 = value;}
		}

		public Boolean t314_datosres1024
		{
			get{return _t314_datosres1024;}
			set{_t314_datosres1024 = value;}
		}

		public Boolean t314_fichaeco1024
		{
			get{return _t314_fichaeco1024;}
			set{_t314_fichaeco1024 = value;}
		}

		public Boolean t314_segrenta1024
		{
			get{return _t314_segrenta1024;}
			set{_t314_segrenta1024 = value;}
		}

		public Boolean t314_avantec1024
		{
			get{return _t314_avantec1024;}
			set{_t314_avantec1024 = value;}
		}

		public Boolean t314_estruct1024
		{
			get{return _t314_estruct1024;}
			set{_t314_estruct1024 = value;}
		}

		public Boolean t314_fotopst1024
		{
			get{return _t314_fotopst1024;}
			set{_t314_fotopst1024 = value;}
		}

		public Boolean t314_plant1024
		{
			get{return _t314_plant1024;}
			set{_t314_plant1024 = value;}
		}

		public Boolean t314_const1024
		{
			get{return _t314_const1024;}
			set{_t314_const1024 = value;}
		}

		public Boolean t314_iapfact1024
		{
			get{return _t314_iapfact1024;}
			set{_t314_iapfact1024 = value;}
		}

		public Boolean t314_iapdiario1024
		{
			get{return _t314_iapdiario1024;}
			set{_t314_iapdiario1024 = value;}
		}

		public Boolean t314_cuadromando1024
		{
			get{return _t314_cuadromando1024;}
			set{_t314_cuadromando1024 = value;}
		}

		public Byte t314_importaciongasvi
		{
			get{return _t314_importaciongasvi;}
			set{_t314_importaciongasvi = value;}
		}

		public Boolean t314_recibirmails
		{
			get{return _t314_recibirmails;}
			set{_t314_recibirmails = value;}
		}

		public Boolean t314_defectoperiodificacion
		{
			get{return _t314_defectoperiodificacion;}
			set{_t314_defectoperiodificacion = value;}
		}

		public Boolean t314_multiventana
		{
			get{return _t314_multiventana;}
			set{_t314_multiventana = value;}
		}

		public String t422_idmoneda_VDC
		{
			get{return _t422_idmoneda_VDC;}
			set{_t422_idmoneda_VDC = value;}
		}

		public String t422_denominacionimportes_vdc
		{
			get{return _t422_denominacionimportes_vdc;}
			set{_t422_denominacionimportes_vdc = value;}
		}

		public String t422_idmoneda_VDP
		{
			get{return _t422_idmoneda_VDP;}
			set{_t422_idmoneda_VDP = value;}
		}

		public String t422_denominacionimportes_vdp
		{
			get{return _t422_denominacionimportes_vdp;}
			set{_t422_denominacionimportes_vdp = value;}
		}

		public String t422_denominacionimportes
		{
			get{return _t422_denominacionimportes;}
			set{_t422_denominacionimportes = value;}
		}

		public Boolean t314_nuevogasvi
		{
			get{return _t314_nuevogasvi;}
			set{_t314_nuevogasvi = value;}
		}

		public Boolean t314_calculoonline
		{
			get{return _t314_calculoonline;}
			set{_t314_calculoonline = value;}
		}

		public Boolean t314_cargaestructura
		{
			get{return _t314_cargaestructura;}
			set{_t314_cargaestructura = value;}
		}


        #endregion

	}
}
