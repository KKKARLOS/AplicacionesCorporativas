using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocutC3
    {

        /// <summary>
        /// Summary description for DocutC3
        /// </summary>
		#region Private Variables
		private Int32 _t363_iddocut;
		private Int32 _t332_idtarea;
		private String _t363_descripcion;
		private String _t363_weblink;
		private String _t363_nombrearchivo;
		private Boolean _t363_privado;
		private Boolean _t363_modolectura;
		private Boolean _t363_tipogestion;
		private Int32 _t314_idusuario_autor;
		private String _autor;

		#endregion

		#region Public Properties
		public Int32 t363_iddocut
		{
			get{return _t363_iddocut;}
			set{_t363_iddocut = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public String t363_descripcion
		{
			get{return _t363_descripcion;}
			set{_t363_descripcion = value;}
		}

		public String t363_weblink
		{
			get{return _t363_weblink;}
			set{_t363_weblink = value;}
		}

		public String t363_nombrearchivo
		{
			get{return _t363_nombrearchivo;}
			set{_t363_nombrearchivo = value;}
		}

		public Boolean t363_privado
		{
			get{return _t363_privado;}
			set{_t363_privado = value;}
		}

		public Boolean t363_modolectura
		{
			get{return _t363_modolectura;}
			set{_t363_modolectura = value;}
		}

		public Boolean t363_tipogestion
		{
			get{return _t363_tipogestion;}
			set{_t363_tipogestion = value;}
		}

		public Int32 t314_idusuario_autor
		{
			get{return _t314_idusuario_autor;}
			set{_t314_idusuario_autor = value;}
		}

		public String autor
		{
			get{return _autor;}
			set{_autor = value;}
		}


        #endregion

	}
}
