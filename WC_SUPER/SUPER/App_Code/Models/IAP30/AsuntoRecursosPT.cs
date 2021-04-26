using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoRecursosPT
    {

        /// <summary>
        /// Summary description for AsuntoRecursosPT
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _nomRecurso;
		private Int32 _T409_idasunto;
		private Boolean _t413_notificar;
		private String _MAIL;
		private String _t001_sexo;
		private Int32 _t303_idnodo;
		private Int32 _baja;
		private String _tipo;
        private String _accionBD;

		#endregion

		#region Public Properties
		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String nomRecurso
		{
			get{return _nomRecurso;}
			set{_nomRecurso = value;}
		}

		public Int32 T409_idasunto
		{
			get{return _T409_idasunto;}
			set{_T409_idasunto = value;}
		}

		public Boolean t413_notificar
		{
			get{return _t413_notificar;}
			set{_t413_notificar = value;}
		}

		public String MAIL
		{
			get{return _MAIL;}
			set{_MAIL = value;}
		}

		public String t001_sexo
		{
			get{return _t001_sexo;}
			set{_t001_sexo = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public Int32 baja
		{
			get{return _baja;}
			set{_baja = value;}
		}

		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}
        public String accionBD
        {
            get { return _accionBD; }
            set { _accionBD = value; }
        } 

        #endregion

	}
}
