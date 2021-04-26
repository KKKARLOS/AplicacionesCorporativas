using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Documento
    {

        /// <summary>
        /// Summary description for Documento
        /// </summary>
		#region Private Variables
		private Int32 _idDocumento; // Nro correlativo para cada elemento
		private Int32 _idElemento;  // Identificador del elemento: Tarea, Proyecto Económico, Asunto de PE, Accion de PE, Asunto de PT, .....
		private String _descripcion;
		private String _weblink;
		private String _nombrearchivo;
		private Int64 _t2_iddocumento;
		private Boolean _privado;
		private Boolean _modolectura;
		private Boolean _tipogestion;
		private Int32 _idusuario_autor;
		private DateTime _fecha;
		private String _autor;
		private Int32 _idusuario_modif;
		private DateTime _fechamodif;
		private String _autormodif;

		#endregion

		#region Public Properties
        public Int32 idDocumento
        {
            get { return _idDocumento; }
            set { _idDocumento = value; }
        }

        public Int32 idElemento
		{
            get { return _idElemento; }
            set { _idElemento = value; }
		}

		public String descripcion
		{
			get{return _descripcion;}
			set{_descripcion = value;}
		}

		public String weblink
		{
			get{return _weblink;}
			set{_weblink = value;}
		}

		public String nombrearchivo
		{
			get{return _nombrearchivo;}
			set{_nombrearchivo = value;}
		}

		public Int64 t2_iddocumento
		{
			get{return _t2_iddocumento;}
			set{_t2_iddocumento = value;}
		}

		public Boolean privado
		{
			get{return _privado;}
			set{_privado = value;}
		}

		public Boolean modolectura
		{
			get{return _modolectura;}
			set{_modolectura = value;}
		}

		public Boolean tipogestion
		{
			get{return _tipogestion;}
			set{_tipogestion = value;}
		}

		public Int32 idusuario_autor
		{
			get{return _idusuario_autor;}
			set{_idusuario_autor = value;}
		}

		public DateTime fecha
		{
			get{return _fecha;}
			set{_fecha = value;}
		}

		public String autor
		{
			get{return _autor;}
			set{_autor = value;}
		}

		public Int32 idusuario_modif
		{
			get{return _idusuario_modif;}
			set{_idusuario_modif = value;}
		}

		public DateTime fechamodif
		{
			get{return _fechamodif;}
			set{_fechamodif = value;}
		}

		public String autormodif
		{
			get{return _autormodif;}
			set{_autormodif = value;}
		}


        #endregion

	}
}
