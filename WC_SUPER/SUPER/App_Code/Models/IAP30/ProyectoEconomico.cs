using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProyectoEconomico
    {

        /// <summary>
        /// Summary description for ProyectoEconomico
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t301_idproyecto;
		private String _t301_denominacion;
		private String _t301_estado;
		private Boolean _t320_facturable;
		private Int32 _t302_idcliente_proyecto;
		private String _t302_denominacion;
		private String _t301_categoria;
		private String _t305_cualidad;
		private String _responsable;
		private Int32 _t303_idnodo;
		private String _t303_denominacion;
        private Int32 _t303_ultcierreeco;
		private String _t305_accesobitacora_pst;
		private String _t304_denominacion;
		private String _t391_denominacion;
		private String _t392_denominacion;
		private String _t393_denominacion;
		private String _t394_denominacion;
		private String _t001_exttel;
		private Boolean _t305_admiterecursospst;
		private String _t301_modelotarif;
        private Int32 _modo_lectura;
        private Int32 _rtpt;
        private String _desmotivo;
		private String _t422_idmoneda_proyecto;

		#endregion

		#region Public Properties
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

		public Boolean t320_facturable
		{
			get{return _t320_facturable;}
			set{_t320_facturable = value;}
		}

		public Int32 t302_idcliente_proyecto
		{
			get{return _t302_idcliente_proyecto;}
			set{_t302_idcliente_proyecto = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}

		public String t301_categoria
		{
			get{return _t301_categoria;}
			set{_t301_categoria = value;}
		}

		public String t305_cualidad
		{
			get{return _t305_cualidad;}
			set{_t305_cualidad = value;}
		}

		public String responsable
		{
			get{return _responsable;}
			set{_responsable = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

        public Int32 t303_ultcierreeco
        {
            get { return _t303_ultcierreeco; }
            set { _t303_ultcierreeco = value; }
        }

		public String t305_accesobitacora_pst
		{
			get{return _t305_accesobitacora_pst;}
			set{_t305_accesobitacora_pst = value;}
		}

		public String t304_denominacion
		{
			get{return _t304_denominacion;}
			set{_t304_denominacion = value;}
		}

		public String t391_denominacion
		{
			get{return _t391_denominacion;}
			set{_t391_denominacion = value;}
		}

		public String t392_denominacion
		{
			get{return _t392_denominacion;}
			set{_t392_denominacion = value;}
		}

		public String t393_denominacion
		{
			get{return _t393_denominacion;}
			set{_t393_denominacion = value;}
		}

		public String t394_denominacion
		{
			get{return _t394_denominacion;}
			set{_t394_denominacion = value;}
		}

		public String t001_exttel
		{
			get{return _t001_exttel;}
			set{_t001_exttel = value;}
		}

		public Boolean t305_admiterecursospst
		{
			get{return _t305_admiterecursospst;}
			set{_t305_admiterecursospst = value;}
		}

		public String t301_modelotarif
		{
			get{return _t301_modelotarif;}
			set{_t301_modelotarif = value;}
		}

        public Int32 modo_lectura
        {
            get { return _modo_lectura; }
            set { _modo_lectura = value; }
        }

        public Int32 rtpt
        {
            get { return _rtpt; }
            set { _rtpt = value; }
        }

        public String desmotivo
        {
            get { return _desmotivo; }
            set { _desmotivo = value; }
        }

		public String t422_idmoneda_proyecto
		{
			get{return _t422_idmoneda_proyecto;}
			set{_t422_idmoneda_proyecto = value;}
		}


        #endregion

	}
}
