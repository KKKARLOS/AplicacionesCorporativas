using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAsuntoPT
    {

        /// <summary>
        /// Summary description for DocumentoAsuntoPT
        /// </summary>
		#region Private Variables
		private Int32 _T409_idasunto;
		private Int32 _t314_idusuario_autor;
		private String _autor;
		private Int32 _T411_autormodif;
		private String _T411_descripcion;
		private DateTime _T411_fecha;
		private DateTime _T411_fechamodif;
		private Int32 _T411_iddocasu;
		private Boolean _T411_modolectura;
		private String _T411_nombrearchivo;
		private Boolean _T411_privado;
		private Boolean _T411_tipogestion;
		private String _T411_weblink;

		#endregion

		#region Public Properties
		public Int32 T409_idasunto
		{
			get{return _T409_idasunto;}
			set{_T409_idasunto = value;}
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

		public Int32 T411_autormodif
		{
			get{return _T411_autormodif;}
			set{_T411_autormodif = value;}
		}

		public String T411_descripcion
		{
			get{return _T411_descripcion;}
			set{_T411_descripcion = value;}
		}

		public DateTime T411_fecha
		{
			get{return _T411_fecha;}
			set{_T411_fecha = value;}
		}

		public DateTime T411_fechamodif
		{
			get{return _T411_fechamodif;}
			set{_T411_fechamodif = value;}
		}

		public Int32 T411_iddocasu
		{
			get{return _T411_iddocasu;}
			set{_T411_iddocasu = value;}
		}

		public Boolean T411_modolectura
		{
			get{return _T411_modolectura;}
			set{_T411_modolectura = value;}
		}

		public String T411_nombrearchivo
		{
			get{return _T411_nombrearchivo;}
			set{_T411_nombrearchivo = value;}
		}

		public Boolean T411_privado
		{
			get{return _T411_privado;}
			set{_T411_privado = value;}
		}

		public Boolean T411_tipogestion
		{
			get{return _T411_tipogestion;}
			set{_T411_tipogestion = value;}
		}

		public String T411_weblink
		{
			get{return _T411_weblink;}
			set{_T411_weblink = value;}
		}


        #endregion

	}
}
