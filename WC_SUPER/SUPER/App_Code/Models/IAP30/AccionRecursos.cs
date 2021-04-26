using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{   
   public class AccionRecursos
   {
        /// <summary>
        /// Summary description for AccionRecursos
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _nomRecurso;
		private Int32 _T383_idaccion;
		private Boolean _T389_notificar;
		private String _mail;
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

		public Int32 T383_idaccion
		{
			get{return _T383_idaccion;}
			set{_T383_idaccion = value;}
		}

		public Boolean T389_notificar
		{
			get{return _T389_notificar;}
			set{_T389_notificar = value;}
		}

		public String mail
		{
            get { return _mail; }
            set { _mail = value; }
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
