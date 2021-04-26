using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAsuntoT
    {

        /// <summary>
        /// Summary description for DocumentoAsuntoT
        /// </summary>
		#region Private Variables
		private Int32 _T600_idasunto;
		private Int32 _t314_idusuario_autor;
		private String _autor;
		private Int32 _T602_autormodif;
		private String _T602_descripcion;
		private DateTime _T602_fecha;
		private DateTime _T602_fechamodif;
		private Int32 _T602_iddocasu;
		private Boolean _T602_modolectura;
		private String _T602_nombrearchivo;
		private Boolean _T602_privado;
		private Boolean _T602_tipogestion;
		private String _T602_weblink;

		#endregion

		#region Public Properties
		public Int32 T600_idasunto
		{
			get{return _T600_idasunto;}
			set{_T600_idasunto = value;}
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

		public Int32 T602_autormodif
		{
			get{return _T602_autormodif;}
			set{_T602_autormodif = value;}
		}

		public String T602_descripcion
		{
			get{return _T602_descripcion;}
			set{_T602_descripcion = value;}
		}

		public DateTime T602_fecha
		{
			get{return _T602_fecha;}
			set{_T602_fecha = value;}
		}

		public DateTime T602_fechamodif
		{
			get{return _T602_fechamodif;}
			set{_T602_fechamodif = value;}
		}

		public Int32 T602_iddocasu
		{
			get{return _T602_iddocasu;}
			set{_T602_iddocasu = value;}
		}

		public Boolean T602_modolectura
		{
			get{return _T602_modolectura;}
			set{_T602_modolectura = value;}
		}

		public String T602_nombrearchivo
		{
			get{return _T602_nombrearchivo;}
			set{_T602_nombrearchivo = value;}
		}

		public Boolean T602_privado
		{
			get{return _T602_privado;}
			set{_T602_privado = value;}
		}

		public Boolean T602_tipogestion
		{
			get{return _T602_tipogestion;}
			set{_T602_tipogestion = value;}
		}

		public String T602_weblink
		{
			get{return _T602_weblink;}
			set{_T602_weblink = value;}
		}


        #endregion

	}
}
