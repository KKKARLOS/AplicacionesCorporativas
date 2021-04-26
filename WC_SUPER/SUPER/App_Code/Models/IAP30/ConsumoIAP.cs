using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAP
    {

        /// <summary>
        /// Summary description for ConsumoIAP
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private Int32 _t314_idusuario;
		private DateTime _t337_fecha;
		private Single _t337_esfuerzo;
		private Double _t337_esfuerzoenjor;
		private String _t337_comentario;
		private DateTime _t337_fecmodif;
		private Int32 _t314_idusuario_modif;

       //propiedades para la imputación diaria

        private string _tipo;
        private string _accion;
        private Int32 _idpt;
        private Nullable<DateTime> _ffe;
        private Nullable<DateTime> _ffeOrig;
        private Nullable<Double> _ete;
        private Nullable<Double> _eteOrig;
        private Boolean _fin;
        private Boolean _finOrig;

		#endregion

		#region Public Properties
		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public DateTime t337_fecha
		{
			get{return _t337_fecha;}
			set{_t337_fecha = value;}
		}

		public Single t337_esfuerzo
		{
			get{return _t337_esfuerzo;}
			set{_t337_esfuerzo = value;}
		}

		public Double t337_esfuerzoenjor
		{
			get{return _t337_esfuerzoenjor;}
			set{_t337_esfuerzoenjor = value;}
		}

		public String t337_comentario
		{
			get{return _t337_comentario;}
			set{_t337_comentario = value;}
		}

		public DateTime t337_fecmodif
		{
			get{return _t337_fecmodif;}
			set{_t337_fecmodif = value;}
		}

		public Int32 t314_idusuario_modif
		{
			get{return _t314_idusuario_modif;}
			set{_t314_idusuario_modif = value;}
		}

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public String accion
        {
            get { return _accion; }
            set { _accion = value; }
        }

        public Int32 idpt
        {
            get { return _idpt; }
            set { _idpt = value; }
        }

        public Nullable<DateTime> ffe
        {
            get { return _ffe; }
            set { _ffe = value; }
        }

        public Nullable<DateTime> ffeOrig
        {
            get { return _ffeOrig; }
            set { _ffeOrig = value; }
        }

        public Nullable<Double> ete
        {
            get { return _ete; }
            set { _ete = value; }
        }

        public Nullable<Double> eteOrig
        {
            get { return _eteOrig; }
            set { _eteOrig = value; }
        }

        public Boolean fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public Boolean finOrig
        {
            get { return _finOrig; }
            set { _finOrig = value; }
        }

        #endregion

	}
}
