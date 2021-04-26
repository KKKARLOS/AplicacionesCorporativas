using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.IAP30.Models
{
   public class TareaBitacora
    {
        /// <summary>
        /// Summary description for TareaBitacora
        /// </summary>
		#region Private Variables
		private Int32 _cod_tarea;
		private String _nom_tarea;
		private Int32 _cod_pt;
		private String _nom_pt;
		private Int32 _cod_pe;
		private String _nom_pe;
		private String _t301_estado;
		private Int32 _t305_idproyectosubnodo;
		private Int32 _cod_une;
		private Int32 _t332_orden;
		private String _t332_acceso_iap;

		#endregion

		#region Public Properties
		public Int32 cod_tarea
		{
			get{return _cod_tarea;}
			set{_cod_tarea = value;}
		}

		public String nom_tarea
		{
			get{return _nom_tarea;}
			set{_nom_tarea = value;}
		}

		public Int32 cod_pt
		{
			get{return _cod_pt;}
			set{_cod_pt = value;}
		}

		public String nom_pt
		{
			get{return _nom_pt;}
			set{_nom_pt = value;}
		}

		public Int32 cod_pe
		{
			get{return _cod_pe;}
			set{_cod_pe = value;}
		}

		public String nom_pe
		{
			get{return _nom_pe;}
			set{_nom_pe = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 cod_une
		{
			get{return _cod_une;}
			set{_cod_une = value;}
		}

		public Int32 t332_orden
		{
			get{return _t332_orden;}
			set{_t332_orden = value;}
		}

		public String t332_acceso_iap
		{
			get{return _t332_acceso_iap;}
			set{_t332_acceso_iap = value;}
		}

        public String nom_fase { get; set; }
        public String nom_actividad { get; set; }
        public String t305_accesobitacora_pst { get; set; }
        public String t331_acceso_iap { get; set; }
        public string sAccesoBitacora { get; set; }
        #endregion

	}
}
