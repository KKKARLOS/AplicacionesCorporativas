using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareasAgendaT
    {

        /// <summary>
        /// Summary description for TareasAgendaT
        /// </summary>
		#region Private Variables
		private Int32 _nivel;
		private String _tipo;
		private Int32 _t334_idfase;
		private Int32 _t335_idactividad;
		private Int32 _t332_idtarea;
		private Int32 _estado;
		private String _denominacion;
		private String _orden;

		#endregion

		#region Public Properties
		public Int32 nivel
		{
			get{return _nivel;}
			set{_nivel = value;}
		}

		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}

		public Int32 t334_idfase
		{
			get{return _t334_idfase;}
			set{_t334_idfase = value;}
		}

		public Int32 t335_idactividad
		{
			get{return _t335_idactividad;}
			set{_t335_idactividad = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public Int32 estado
		{
			get{return _estado;}
			set{_estado = value;}
		}

		public String denominacion
		{
			get{return _denominacion;}
			set{_denominacion = value;}
		}

		public String orden
		{
			get{return _orden;}
			set{_orden = value;}
		}


        #endregion

	}
}
