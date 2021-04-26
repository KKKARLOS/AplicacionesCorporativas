using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProfesionalIap
    {

        /// <summary>
        /// Summary description for ProfesionalIap
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _PROFESIONAL;
		private String _EMPRESA;
		private String _t303_denominacion;
		private Int32 _t001_idficepi;
		private String _t001_codred;
		private String _t001_sexo;
		private String _tipo;
        private Int32 _es_responsable;

		#endregion

		#region Public Properties
		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String PROFESIONAL
		{
			get{return _PROFESIONAL;}
			set{_PROFESIONAL = value;}
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

		public Int32 t001_idficepi
		{
			get{return _t001_idficepi;}
			set{_t001_idficepi = value;}
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

		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}

        public Int32 es_responsable
        {
            get { return _es_responsable; }
            set { _es_responsable = value; }
        }
        #endregion

	}
}
