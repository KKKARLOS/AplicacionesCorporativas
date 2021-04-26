using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareasAgendaPSN
    {

        /// <summary>
        /// Summary description for TareasAgendaPSN
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t301_idproyecto;
		private String _t305_seudonimo;
		private String _t301_denominacion;
		private String _t303_denominacion;
		private String _t302_denominacion;
		private String _responsable;

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

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}

		public String responsable
		{
			get{return _responsable;}
			set{_responsable = value;}
		}


        #endregion

	}
}
