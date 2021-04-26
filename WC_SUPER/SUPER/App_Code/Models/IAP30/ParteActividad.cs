using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ParteActividad
    {

        /// <summary>
        /// Summary description for ParteActividad
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private Int32 _t314_idusuario;
		private DateTime _t337_fecha;
		private Single _t337_esfuerzo;
		private Int32 _t301_idproyecto;
		private String _t332_destarea;
		private String _t335_desactividad;
		private String _t334_desfase;
		private String _t331_despt;
		private String _t305_seudonimo;
		private Boolean _t332_facturable;
		private Int32 _t324_idmodofact;
		private String _t324_denominacion;
		private String _Profesional;
		private String _ProfesionalSinAlias;
		private String _t302_denominacion;

        private DateTime _t337_fecha_desde;
        private DateTime _t337_fecha_hasta;

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

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public Boolean t332_facturable
		{
			get{return _t332_facturable;}
			set{_t332_facturable = value;}
		}

		public Int32 t324_idmodofact
		{
			get{return _t324_idmodofact;}
			set{_t324_idmodofact = value;}
		}

		public String t324_denominacion
		{
			get{return _t324_denominacion;}
			set{_t324_denominacion = value;}
		}

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public String ProfesionalSinAlias
		{
			get{return _ProfesionalSinAlias;}
			set{_ProfesionalSinAlias = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}

        public DateTime t337_fecha_desde
        {
            get { return _t337_fecha_desde; }
            set { _t337_fecha_desde = value; }
        }

        public DateTime t337_fecha_hasta
        {
            get { return _t337_fecha_hasta; }
            set { _t337_fecha_hasta = value; }
        }

        #endregion

	}
}
