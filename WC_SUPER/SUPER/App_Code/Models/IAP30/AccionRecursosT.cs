using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AccionRecursosT
    {

        /// <summary>
        /// Summary description for AccionRecursosT
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _MAIL;
		private String _nomRecurso;
		private Int32 _t601_idaccion;
		private Boolean _t605_notificar;
		private String _t001_sexo;
		private Int32 _t303_idnodo;
		private Int32 _baja;
		private String _tipo;

		#endregion

		#region Public Properties
		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String MAIL
		{
			get{return _MAIL;}
			set{_MAIL = value;}
		}

		public String nomRecurso
		{
			get{return _nomRecurso;}
			set{_nomRecurso = value;}
		}

		public Int32 t601_idaccion
		{
			get{return _t601_idaccion;}
			set{_t601_idaccion = value;}
		}

		public Boolean t605_notificar
		{
			get{return _t605_notificar;}
			set{_t605_notificar = value;}
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
        public String accionBD { get; set; }

        #endregion

	}
}
