using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProyectoSubNodo
    {

        /// <summary>
        /// Summary description for ProyectoSubNodo
        /// </summary>
		#region Private Variables
		private String _t301_estado;
		private String _t301_denominacion;
		private Int32 _t301_idproyecto;
		private String _t301_categoria;
		private String _t305_accesobitacora_pst;
		private String _t305_cualidad;
		private Int32 _t314_idusuario_SAT;
		private Int32 _t314_idusuario_SAA;
		private Boolean _t301_externalizable;
		private String _t422_idmoneda;
		private String _denMoneda;

		#endregion

		#region Public Properties
        public Int32 t305_idproyectosubnodo { get; set; }
		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t301_categoria
		{
			get{return _t301_categoria;}
			set{_t301_categoria = value;}
		}

		public String t305_accesobitacora_pst
		{
			get{return _t305_accesobitacora_pst;}
			set{_t305_accesobitacora_pst = value;}
		}

		public String t305_cualidad
		{
			get{return _t305_cualidad;}
			set{_t305_cualidad = value;}
		}

		public Int32 t314_idusuario_SAT
		{
			get{return _t314_idusuario_SAT;}
			set{_t314_idusuario_SAT = value;}
		}

		public Int32 t314_idusuario_SAA
		{
			get{return _t314_idusuario_SAA;}
			set{_t314_idusuario_SAA = value;}
		}

		public Boolean t301_externalizable
		{
			get{return _t301_externalizable;}
			set{_t301_externalizable = value;}
		}

		public String t422_idmoneda
		{
			get{return _t422_idmoneda;}
			set{_t422_idmoneda = value;}
		}

		public String denMoneda
		{
			get{return _denMoneda;}
			set{_denMoneda = value;}
		}

        public String t305_accesobitacora_iap { get; set; }

        #endregion

	}
}
