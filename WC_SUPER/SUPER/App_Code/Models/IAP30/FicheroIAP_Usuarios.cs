using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class FicheroIAP_Usuarios
    {

        /// <summary>
        /// Summary description for FicheroIAP_Usuarios
        /// </summary>
		#region Private Variables
		private Int32 _t001_IDFICEPI;
		private Int32 _t314_idusuario;
		private String _Profesional;
		private Int32 _t303_ultcierreIAP;
		private Int32 _t303_idnodo;
		private Boolean _t314_jornadareducida;
		private Single _t314_horasjor_red;
		private DateTime _t314_fdesde_red;
		private DateTime _t314_fhasta_red;
		private Boolean _t314_controlhuecos;
		private DateTime _fUltImputacion;
		private Int32 _t066_idcal;
		private String _t066_descal;
		private Int32 _t066_semlabL;
		private Int32 _t066_semlabM;
		private Int32 _t066_semlabX;
		private Int32 _t066_semlabJ;
		private Int32 _t066_semlabV;
		private Int32 _t066_semlabS;
		private Int32 _t066_semlabD;
		private String _t001_codred;
		private DateTime _t001_fecalta;
		private DateTime _t001_fecbaja;

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

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public Int32 t303_ultcierreIAP
		{
			get{return _t303_ultcierreIAP;}
			set{_t303_ultcierreIAP = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
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

		public Int32 t066_idcal
		{
			get{return _t066_idcal;}
			set{_t066_idcal = value;}
		}

		public String t066_descal
		{
			get{return _t066_descal;}
			set{_t066_descal = value;}
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

		public DateTime t001_fecalta
		{
			get{return _t001_fecalta;}
			set{_t001_fecalta = value;}
		}

		public DateTime t001_fecbaja
		{
			get{return _t001_fecbaja;}
			set{_t001_fecbaja = value;}
		}


        #endregion

	}
}
