using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAccionPT
    {

        /// <summary>
        /// Summary description for DocumentoAccionPT
        /// </summary>
		#region Private Variables
		private Int32 _T410_idaccion;
		private Int32 _T314_idusuario_autor;
		private String _autor;
		private Int32 _T412_autormodif;
		private String _T412_descripcion;
		private DateTime _T412_fecha;
		private DateTime _T412_fechamodif;
		private Int32 _T412_iddocacc;
		private Boolean _T412_modolectura;
		private String _T412_nombrearchivo;
		private Boolean _T412_privado;
		private Boolean _T412_tipogestion;
		private String _T412_weblink;

		#endregion

		#region Public Properties
		public Int32 T410_idaccion
		{
			get{return _T410_idaccion;}
			set{_T410_idaccion = value;}
		}

		public Int32 T314_idusuario_autor
		{
			get{return _T314_idusuario_autor;}
			set{_T314_idusuario_autor = value;}
		}

		public String autor
		{
			get{return _autor;}
			set{_autor = value;}
		}

		public Int32 T412_autormodif
		{
			get{return _T412_autormodif;}
			set{_T412_autormodif = value;}
		}

		public String T412_descripcion
		{
			get{return _T412_descripcion;}
			set{_T412_descripcion = value;}
		}

		public DateTime T412_fecha
		{
			get{return _T412_fecha;}
			set{_T412_fecha = value;}
		}

		public DateTime T412_fechamodif
		{
			get{return _T412_fechamodif;}
			set{_T412_fechamodif = value;}
		}

		public Int32 T412_iddocacc
		{
			get{return _T412_iddocacc;}
			set{_T412_iddocacc = value;}
		}

		public Boolean T412_modolectura
		{
			get{return _T412_modolectura;}
			set{_T412_modolectura = value;}
		}

		public String T412_nombrearchivo
		{
			get{return _T412_nombrearchivo;}
			set{_T412_nombrearchivo = value;}
		}

		public Boolean T412_privado
		{
			get{return _T412_privado;}
			set{_T412_privado = value;}
		}

		public Boolean T412_tipogestion
		{
			get{return _T412_tipogestion;}
			set{_T412_tipogestion = value;}
		}

		public String T412_weblink
		{
			get{return _T412_weblink;}
			set{_T412_weblink = value;}
		}


        #endregion

	}
}
