using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAsunto
    {

        /// <summary>
        /// Summary description for DocumentoAsunto
        /// </summary>
		#region Private Variables
		private Int32 _T382_idasunto;
		private Int32 _t314_idusuario_autor;
		private String _autor;
		private Int32 _T386_autormodif;
		private String _T386_descripcion;
		private DateTime _T386_fecha;
		private DateTime _T386_fechamodif;
		private Int32 _T386_iddocasu;
		private Boolean _T386_modolectura;
		private String _T386_nombrearchivo;
		private Boolean _T386_privado;
		private Boolean _T386_tipogestion;
		private String _T386_weblink;

		#endregion

		#region Public Properties
		public Int32 T382_idasunto
		{
			get{return _T382_idasunto;}
			set{_T382_idasunto = value;}
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

		public Int32 T386_autormodif
		{
			get{return _T386_autormodif;}
			set{_T386_autormodif = value;}
		}

		public String T386_descripcion
		{
			get{return _T386_descripcion;}
			set{_T386_descripcion = value;}
		}

		public DateTime T386_fecha
		{
			get{return _T386_fecha;}
			set{_T386_fecha = value;}
		}

		public DateTime T386_fechamodif
		{
			get{return _T386_fechamodif;}
			set{_T386_fechamodif = value;}
		}

		public Int32 T386_iddocasu
		{
			get{return _T386_iddocasu;}
			set{_T386_iddocasu = value;}
		}

		public Boolean T386_modolectura
		{
			get{return _T386_modolectura;}
			set{_T386_modolectura = value;}
		}

		public String T386_nombrearchivo
		{
			get{return _T386_nombrearchivo;}
			set{_T386_nombrearchivo = value;}
		}

		public Boolean T386_privado
		{
			get{return _T386_privado;}
			set{_T386_privado = value;}
		}

		public Boolean T386_tipogestion
		{
			get{return _T386_tipogestion;}
			set{_T386_tipogestion = value;}
		}

		public String T386_weblink
		{
			get{return _T386_weblink;}
			set{_T386_weblink = value;}
		}


        #endregion

	}
}
