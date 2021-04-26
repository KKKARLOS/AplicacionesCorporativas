using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class DocumentoAccion
    {

        /// <summary>
        /// Summary description for DocumentoAccion
        /// </summary>
		#region Private Variables
		private Int32 _T383_idaccion;
		private Int32 _T314_idusuario_autor;
		private String _autor;
		private Int32 _T387_autormodif;
		private String _T387_descripcion;
		private DateTime _T387_fecha;
		private DateTime _T387_fechamodif;
		private Int32 _T387_iddocacc;
		private Boolean _T387_modolectura;
		private String _T387_nombrearchivo;
		private Boolean _T387_privado;
		private Boolean _T387_tipogestion;
		private String _T387_weblink;

		#endregion

		#region Public Properties
		public Int32 T383_idaccion
		{
			get{return _T383_idaccion;}
			set{_T383_idaccion = value;}
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

		public Int32 T387_autormodif
		{
			get{return _T387_autormodif;}
			set{_T387_autormodif = value;}
		}

		public String T387_descripcion
		{
			get{return _T387_descripcion;}
			set{_T387_descripcion = value;}
		}

		public DateTime T387_fecha
		{
			get{return _T387_fecha;}
			set{_T387_fecha = value;}
		}

		public DateTime T387_fechamodif
		{
			get{return _T387_fechamodif;}
			set{_T387_fechamodif = value;}
		}

		public Int32 T387_iddocacc
		{
			get{return _T387_iddocacc;}
			set{_T387_iddocacc = value;}
		}

		public Boolean T387_modolectura
		{
			get{return _T387_modolectura;}
			set{_T387_modolectura = value;}
		}

		public String T387_nombrearchivo
		{
			get{return _T387_nombrearchivo;}
			set{_T387_nombrearchivo = value;}
		}

		public Boolean T387_privado
		{
			get{return _T387_privado;}
			set{_T387_privado = value;}
		}

		public Boolean T387_tipogestion
		{
			get{return _T387_tipogestion;}
			set{_T387_tipogestion = value;}
		}

		public String T387_weblink
		{
			get{return _T387_weblink;}
			set{_T387_weblink = value;}
		}


        #endregion

	}
}
