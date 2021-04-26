using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class CONSUMOIAP_PROYECTOS
    {

        /// <summary>
        /// Summary description for CONSUMOIAP_PROYECTOS
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private String _t305_seudonimo;
		private Int32 _t301_idproyecto;

		#endregion

		#region Public Properties
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

        #endregion

	}
}
