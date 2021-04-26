using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Asunto
    {

        /// <summary>
        /// Summary description for Asunto
        /// </summary>
		#region Private Variables
        private Int32? _t303_idnodo;       
        private Int32? _t301_idproyecto;
		private Int32 _t305_idproyectosubnodo;
		private String _T382_alerta;
		private String _T382_desasunto;
		private String _T382_desasuntolong;
		private String _T382_dpto;
		private String _T382_estado;
		private Double _T382_etp;
		private Double _T382_etr;
		private DateTime _T382_fcreacion;
		private DateTime? _T382_ffin;
		private DateTime? _T382_flimite;
		private DateTime _T382_fnotificacion;
		private Int32 _T382_idasunto;
		private String _T382_notificador;
		private String _T382_obs;
		private String _T382_prioridad;
		private String _T382_refexterna;
		private Int32 _T382_registrador;
		private Int32 _T382_responsable;
		private String _T382_severidad;
		private String _T382_sistema;
		private Int32 _T384_idtipo;
		private String _T384_destipo;
        private String _Registrador;
        private String _Responsable;
        private String _T382_estado_anterior;
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
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public String T382_alerta
		{
			get{return _T382_alerta;}
			set{_T382_alerta = value;}
		}

		public String T382_desasunto
		{
			get{return _T382_desasunto;}
			set{_T382_desasunto = value;}
		}

		public String T382_desasuntolong
		{
			get{return _T382_desasuntolong;}
			set{_T382_desasuntolong = value;}
		}

		public String T382_dpto
		{
			get{return _T382_dpto;}
			set{_T382_dpto = value;}
		}

		public String T382_estado
		{
			get{return _T382_estado;}
			set{_T382_estado = value;}
		}

		public Double T382_etp
		{
			get{return _T382_etp;}
			set{_T382_etp = value;}
		}

		public Double T382_etr
		{
			get{return _T382_etr;}
			set{_T382_etr = value;}
		}

		public DateTime T382_fcreacion
		{
			get{return _T382_fcreacion;}
			set{_T382_fcreacion = value;}
		}

		public DateTime? T382_ffin
		{
			get{return _T382_ffin;}
			set{_T382_ffin = value;}
		}

		public DateTime? T382_flimite
		{
			get{return _T382_flimite;}
			set{_T382_flimite = value;}
		}

		public DateTime T382_fnotificacion
		{
			get{return _T382_fnotificacion;}
			set{_T382_fnotificacion = value;}
		}

		public Int32 T382_idasunto
		{
			get{return _T382_idasunto;}
			set{_T382_idasunto = value;}
		}

		public String T382_notificador
		{
			get{return _T382_notificador;}
			set{_T382_notificador = value;}
		}

		public String T382_obs
		{
			get{return _T382_obs;}
			set{_T382_obs = value;}
		}

		public String T382_prioridad
		{
			get{return _T382_prioridad;}
			set{_T382_prioridad = value;}
		}

		public String T382_refexterna
		{
			get{return _T382_refexterna;}
			set{_T382_refexterna = value;}
		}

		public Int32 T382_registrador
		{
			get{return _T382_registrador;}
			set{_T382_registrador = value;}
		}

		public Int32 T382_responsable
		{
			get{return _T382_responsable;}
			set{_T382_responsable = value;}
		}

		public String T382_severidad
		{
			get{return _T382_severidad;}
			set{_T382_severidad = value;}
		}

		public String T382_sistema
		{
			get{return _T382_sistema;}
			set{_T382_sistema = value;}
		}

		public Int32 T384_idtipo
		{
			get{return _T384_idtipo;}
			set{_T384_idtipo = value;}
		}

		public String T384_destipo
		{
			get{return _T384_destipo;}
			set{_T384_destipo = value;}
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
        public String T382_estado_anterior
        {
            get { return _T382_estado_anterior; }
            set { _T382_estado_anterior = value; }
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
