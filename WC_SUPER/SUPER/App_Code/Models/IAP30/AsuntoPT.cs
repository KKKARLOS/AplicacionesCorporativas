using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoPT
    {

        /// <summary>
        /// Summary description for AsuntoPT
        /// </summary>
		#region Private Variables
        private Int32? _t303_idnodo;
        private Int32? _t301_idproyecto;
        private Int32 _t305_idproyectosubnodo;
		private Int32 _t331_idpt;
        private String _t331_despt;
		private String _T409_alerta;
		private String _T409_desasunto;
		private String _T409_desasuntolong;
		private String _T409_dpto;
		private String _T409_estado;
		private Double _T409_etp;
		private Double _T409_etr;
		private DateTime _T409_fcreacion;
        private DateTime? _T409_ffin;
        private DateTime? _T409_flimite;
		private DateTime _T409_fnotificacion;
		private Int32 _T409_idasunto;
		private String _T409_notificador;
		private String _T409_obs;
		private String _T409_prioridad;
		private String _T409_refexterna;
		private Int32 _T409_registrador;
		private Int32 _T409_responsable;
		private String _T409_severidad;
		private String _T409_sistema;
		private Int32 _t384_idtipo;
		private String _t384_destipo;
        private String _Registrador;
        private String _Responsable;
        private String _T409_estado_anterior;
        private String _DesPE;
        private String _DesEstado;
        private String _DesSeveridad;
        private String _DesPrioridad;
		#endregion

		#region Public Properties
        public Int32? t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        public Int32? t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        public Int32 t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }
		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}
        public String t331_despt
        {
            get { return _t331_despt; }
            set { _t331_despt = value; }
        }
		public String T409_alerta
		{
			get{return _T409_alerta;}
			set{_T409_alerta = value;}
		}

		public String T409_desasunto
		{
			get{return _T409_desasunto;}
			set{_T409_desasunto = value;}
		}

		public String T409_desasuntolong
		{
			get{return _T409_desasuntolong;}
			set{_T409_desasuntolong = value;}
		}

		public String T409_dpto
		{
			get{return _T409_dpto;}
			set{_T409_dpto = value;}
		}

		public String T409_estado
		{
			get{return _T409_estado;}
			set{_T409_estado = value;}
		}

		public Double T409_etp
		{
			get{return _T409_etp;}
			set{_T409_etp = value;}
		}

		public Double T409_etr
		{
			get{return _T409_etr;}
			set{_T409_etr = value;}
		}

		public DateTime T409_fcreacion
		{
			get{return _T409_fcreacion;}
			set{_T409_fcreacion = value;}
		}

        public DateTime? T409_ffin
		{
			get{return _T409_ffin;}
			set{_T409_ffin = value;}
		}

        public DateTime? T409_flimite
		{
			get{return _T409_flimite;}
			set{_T409_flimite = value;}
		}

		public DateTime T409_fnotificacion
		{
			get{return _T409_fnotificacion;}
			set{_T409_fnotificacion = value;}
		}

		public Int32 T409_idasunto
		{
			get{return _T409_idasunto;}
			set{_T409_idasunto = value;}
		}

		public String T409_notificador
		{
			get{return _T409_notificador;}
			set{_T409_notificador = value;}
		}

		public String T409_obs
		{
			get{return _T409_obs;}
			set{_T409_obs = value;}
		}

		public String T409_prioridad
		{
			get{return _T409_prioridad;}
			set{_T409_prioridad = value;}
		}

		public String T409_refexterna
		{
			get{return _T409_refexterna;}
			set{_T409_refexterna = value;}
		}

		public Int32 T409_registrador
		{
			get{return _T409_registrador;}
			set{_T409_registrador = value;}
		}

		public Int32 T409_responsable
		{
			get{return _T409_responsable;}
			set{_T409_responsable = value;}
		}

		public String T409_severidad
		{
			get{return _T409_severidad;}
			set{_T409_severidad = value;}
		}

		public String T409_sistema
		{
			get{return _T409_sistema;}
			set{_T409_sistema = value;}
		}

		public Int32 t384_idtipo
		{
			get{return _t384_idtipo;}
			set{_t384_idtipo = value;}
		}

		public String t384_destipo
		{
			get{return _t384_destipo;}
			set{_t384_destipo = value;}
		}
        public String Registrador
        {
            get { return _Registrador; }
            set { _Registrador = value; }
        }
        public String Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public String T409_estado_anterior
        {
            get { return _T409_estado_anterior; }
            set { _T409_estado_anterior = value; }
        }
        public String DesPE
        {
            get { return _DesPE; }
            set { _DesPE = value; }
        }

        public String DesEstado
        {
            get { return _DesEstado; }
            set { _DesEstado = value; }
        }

        public String DesSeveridad
        {
            get { return _DesSeveridad; }
            set { _DesSeveridad = value; }
        }

        public String DesPrioridad
        {
            get { return _DesPrioridad; }
            set { _DesPrioridad = value; }
        }
  
        #endregion

	}
}
