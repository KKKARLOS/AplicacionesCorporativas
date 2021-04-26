using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Tecnicos
    {

        /// <summary>
        /// Summary description for Tecnicos
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _Profesional;
		private Int32 _IdTarifa;
		private Int32? _t303_idnodo;
		private String _t001_sexo;
		private String _t001_codred;
		private Int32 _baja;
		private String _EMPRESA;
		private String _t303_denominacion;
		private String _tipo;
		private String _t001_email;
		private String _MAIL;
        private String _Apellido1;
        private String _Apellido2;
        private String _Nombre;
		private Int32? _nPSN;
        private String _Cualidad;
		private Int32? _idTarea;
        private bool _Foraneos;
        private bool _SoloActivos;

		#endregion

		#region Public Properties
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

		public Int32 IdTarifa
		{
			get{return _IdTarifa;}
			set{_IdTarifa = value;}
		}

		public Int32? t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public String t001_sexo
		{
			get{return _t001_sexo;}
			set{_t001_sexo = value;}
		}

		public String t001_codred
		{
			get{return _t001_codred;}
			set{_t001_codred = value;}
		}

		public Int32 baja
		{
			get{return _baja;}
			set{_baja = value;}
		}

		public String EMPRESA
		{
			get{return _EMPRESA;}
			set{_EMPRESA = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}

		public String t001_email
		{
			get{return _t001_email;}
			set{_t001_email = value;}
		}

		public String MAIL
		{
			get{return _MAIL;}
			set{_MAIL = value;}
		}

        //parámetros de entrada (SUP_TECNICOS_TARIFA_NOMBRE)

		public String Apellido1
		{
			get{return _Apellido1;}
			set{_Apellido1 = value;}
		}

       	public String Apellido2
		{
			get{return _Apellido2;}
			set{_Apellido2 = value;}
		}
       	public String Nombre
		{
			get{return _Nombre;}
			set{_Nombre = value;}
		}
       	public int? nPSN
		{
			get{return _nPSN;}
			set{_nPSN = value;}
		}
		public String Cualidad
		{
			get{return _Cualidad;}
			set{_Cualidad = value;}
		}     
       	public int? idTarea
		{
			get{return _idTarea;}
			set{_idTarea = value;}
		}

        public bool Foraneos
		{
			get{return _Foraneos;}
			set{_Foraneos = value;}
		}  
    
        public bool SoloActivos
		{
			get{return _SoloActivos;}
			set{_SoloActivos = value;}
		} 


        #endregion

	}
}
