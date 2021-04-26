using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class UsuarioActivo
    {

        /// <summary>
        /// Summary description for UsuarioActivo
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _Profesional;
		private Int32 _t303_idnodo;
		private String _t001_sexo;
		private Int32 _t001_idficepi;
		private String _EMPRESA;
		private String _t303_denominacion;
		private String _tipo;

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

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public String t001_sexo
		{
			get{return _t001_sexo;}
			set{_t001_sexo = value;}
		}

		public Int32 t001_idficepi
		{
			get{return _t001_idficepi;}
			set{_t001_idficepi = value;}
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


        #endregion

	}
}
