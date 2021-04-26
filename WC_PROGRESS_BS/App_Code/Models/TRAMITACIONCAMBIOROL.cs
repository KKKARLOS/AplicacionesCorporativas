using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
   
   public class TramitacionCambioRol
    {

        /// <summary>
        /// Summary description for TRAMITACIONCAMBIOROL
        /// </summary>
		#region Private Variables
        private int _t940_idtramitacambiorol;
        private string _profesional;
        private string _nombrecorto;
		private Int32 _t001_idficepi_promotor;
		private Int32 _t001_idficepi_interesado;
		private Int16 _t004_idrol_actual;
		private Int16 _t004_idrol_propuesto;
		private String _t940_motivopropuesto;
		private DateTime _t940_fechaproposicion;
        private string _t940_desrolActual;
        private string _t940_desrolPropuesto;
        private string _t940_motivorechazo;
        private string _nombre_promotor;
        private string _correoresporigen;
        private string _correointeresado;       
        private string _nombreapellidos_interesado;
        private string _nombre_interesado;
        private string _nombreapellidos_promotor;
        private DateTime _t940_fecharesolucion;
        private string _aprobador;
        public string CorreoPromotor { get; set; }
        public string CorreoAprobador { get; set; }        
        public string nomCortoPromotor { get; set; }
        public string nomCortoAprobador { get; set; }
        public int t001_idficepi_aprobador { get; set; }
        public string nomCortoInteresado { get; set; }
        public int n_solicitudes_APA { get; set; }
        public int n_solicitudes_DPA { get; set; }
        public int n_solicitudes_ASBY { get; set; }
        public int n_solicitudes_DSBY { get; set; }
        public char t940_resolucion { get; set; }

        #endregion 

		#region Public Properties
        public int t940_idtramitacambiorol
		{
            get { return _t940_idtramitacambiorol; }
            set { _t940_idtramitacambiorol = value; }
		}

        public String Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public String Nombrecorto
        {
            get { return _nombrecorto; }
            set { _nombrecorto = value; }
        }

		public Int32 t001_idficepi_promotor
		{
			get{return _t001_idficepi_promotor;}
			set{_t001_idficepi_promotor = value;}
		}

        public string nombre_promotor
        {
            get { return _nombre_promotor; }
            set { _nombre_promotor = value; }
        }

        public string correoresporigen
        {
            get { return _correoresporigen; }
            set { _correoresporigen = value; }
        }

        public string correointeresado
        {
            get { return _correointeresado; }
            set { _correointeresado = value; }
        }

        public string nombreapellidos_interesado
        {
            get { return _nombreapellidos_interesado; }
            set { _nombreapellidos_interesado = value; }
        }

        public string nombre_interesado
        {
            get { return _nombre_interesado; }
            set { _nombre_interesado = value; }
        }

        public string nombreapellidos_promotor
        {
            get { return _nombreapellidos_promotor; }
            set { _nombreapellidos_promotor = value; }
        }
       
		public Int32 t001_idficepi_interesado
		{
			get{return _t001_idficepi_interesado;}
			set{_t001_idficepi_interesado = value;}
		}

		public Int16 t004_idrol_actual
		{
			get{return _t004_idrol_actual;}
			set{_t004_idrol_actual = value;}
		}

		public Int16 t004_idrol_propuesto
		{
			get{return _t004_idrol_propuesto;}
			set{_t004_idrol_propuesto = value;}
		}

		public String t940_motivopropuesto
		{
			get{return _t940_motivopropuesto;}
			set{_t940_motivopropuesto = value;}
		}

		public DateTime t940_fechaproposicion
		{
			get{return _t940_fechaproposicion;}
			set{_t940_fechaproposicion = value;}
		}

        public DateTime t940_fecharesolucion
        {
            get { return _t940_fecharesolucion; }
            set { _t940_fecharesolucion = value; }
        }

        public String t940_desrolActual
        {
            get { return _t940_desrolActual; }
            set { _t940_desrolActual = value; }
        }

        public String t940_desrolPropuesto
        {
            get { return _t940_desrolPropuesto; }
            set { _t940_desrolPropuesto = value; }
        }

        public String t940_motivorechazo
        {
            get { return _t940_motivorechazo; }
            set { _t940_motivorechazo = value; }
        }

        public String aprobador
        {
            get { return _aprobador; }
            set { _aprobador = value; }
        }

        #endregion

	}

   public class TRAMITACIONCAMBIOROL_INS
   {

       /// <summary>
       /// Summary description for TRAMITACIONCAMBIOROL
       /// </summary>
       #region Private Variables
       private Int32 _t001_idficepi_promotor;
       private Int32 _t001_idficepi_interesado;
       private Int16 _t004_idrol_propuesto;
       private String _t940_motivopropuesto;

       #endregion

       #region Public Properties

       public Int32 t001_idficepi_promotor
       {
           get { return _t001_idficepi_promotor; }
           set { _t001_idficepi_promotor = value; }
       }

       public Int32 t001_idficepi_interesado
       {
           get { return _t001_idficepi_interesado; }
           set { _t001_idficepi_interesado = value; }
       }
       
       public Int16 t004_idrol_propuesto
       {
           get { return _t004_idrol_propuesto; }
           set { _t004_idrol_propuesto = value; }
       }

       public String t940_motivopropuesto
       {
           get { return _t940_motivopropuesto; }
           set { _t940_motivopropuesto = value; }
       }

       #endregion
   }
}
