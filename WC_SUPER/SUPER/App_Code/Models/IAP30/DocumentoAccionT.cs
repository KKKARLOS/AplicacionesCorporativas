using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAccionT
    {

        /// <summary>
        /// Summary description for DocumentoAccionT
        /// </summary>
		#region Private Variables
		private Int32 _T601_idaccion;
		private Int32 _T314_idusuario_autor;
		private String _autor;
		private Int32 _T603_autormodif;
		private String _T603_descripcion;
		private DateTime _T603_fecha;
		private DateTime _T603_fechamodif;
		private Int32 _T603_iddocacc;
		private Boolean _T603_modolectura;
		private String _T603_nombrearchivo;
		private Boolean _T603_privado;
		private Boolean _T603_tipogestion;
		private String _T603_weblink;

		#endregion

		#region Public Properties
		public Int32 T601_idaccion
		{
			get{return _T601_idaccion;}
			set{_T601_idaccion = value;}
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

		public Int32 T603_autormodif
		{
			get{return _T603_autormodif;}
			set{_T603_autormodif = value;}
		}

		public String T603_descripcion
		{
			get{return _T603_descripcion;}
			set{_T603_descripcion = value;}
		}

		public DateTime T603_fecha
		{
			get{return _T603_fecha;}
			set{_T603_fecha = value;}
		}

		public DateTime T603_fechamodif
		{
			get{return _T603_fechamodif;}
			set{_T603_fechamodif = value;}
		}

		public Int32 T603_iddocacc
		{
			get{return _T603_iddocacc;}
			set{_T603_iddocacc = value;}
		}

		public Boolean T603_modolectura
		{
			get{return _T603_modolectura;}
			set{_T603_modolectura = value;}
		}

		public String T603_nombrearchivo
		{
			get{return _T603_nombrearchivo;}
			set{_T603_nombrearchivo = value;}
		}

		public Boolean T603_privado
		{
			get{return _T603_privado;}
			set{_T603_privado = value;}
		}

		public Boolean T603_tipogestion
		{
			get{return _T603_tipogestion;}
			set{_T603_tipogestion = value;}
		}

		public String T603_weblink
		{
			get{return _T603_weblink;}
			set{_T603_weblink = value;}
		}


        #endregion

	}
}
