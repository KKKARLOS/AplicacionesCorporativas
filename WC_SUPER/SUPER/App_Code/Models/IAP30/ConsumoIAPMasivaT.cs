using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAPMasivaT
    {

        /// <summary>
        /// Summary description for ConsumoIAPMasivaT
        /// </summary>
		#region Private Variables
		private Int32 _nivel;
		private String _tipo;
		private Int32 _t334_idfase;
		private Int32 _t335_idactividad;
		private Int32 _t332_idtarea;
		private Int32 _t332_estado;
		private String _denominacion;
		private Int32 _t332_impiap;
		private Int32 _t323_regjornocompleta;
		private Int32 _t323_regfes;
		private Int32 _t331_obligaest;
		private String _orden;
		private String _t301_estado;

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

		public Int32 t332_estado
		{
			get{return _t332_estado;}
			set{_t332_estado = value;}
		}

		public String denominacion
		{
			get{return _denominacion;}
			set{_denominacion = value;}
		}

		public Int32 t332_impiap
		{
			get{return _t332_impiap;}
			set{_t332_impiap = value;}
		}

		public Int32 t323_regjornocompleta
		{
			get{return _t323_regjornocompleta;}
			set{_t323_regjornocompleta = value;}
		}

		public Int32 t323_regfes
		{
			get{return _t323_regfes;}
			set{_t323_regfes = value;}
		}

		public Int32 t331_obligaest
		{
			get{return _t331_obligaest;}
			set{_t331_obligaest = value;}
		}

		public String orden
		{
			get{return _orden;}
			set{_orden = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}


        #endregion

	}
}
