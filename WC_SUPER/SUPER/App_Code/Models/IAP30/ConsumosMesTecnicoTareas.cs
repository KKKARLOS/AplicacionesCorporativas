using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumosMesTecnicoTareas
    {

        /// <summary>
        /// Summary description for ConsumosMesTecnicoTareas
        /// </summary>
		#region Private Variables
		private Int32  _t301_idproyecto;
		private Int32  _t305_idproyectosubnodo;
		private Int32  _t331_idpt;
		private Int32  _t334_idfase;
		private Int32  _t335_idactividad;
		private Int32  _t332_idtarea;
		private String _t301_denominacion;
		private String _Cualidad;
		private String _t302_denominacion;
		private Int32  _t303_idnodo;
		private String _t303_denominacion;
		private String _T331_despt;
		private String _t334_desfase;
		private String _t335_desactividad;
		private String _t332_destarea;
		private Double _TotalHorasReportadas;
		private Double _TotalJornadasReportadas;

		#endregion

		#region Public Properties
		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
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

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public String Cualidad
		{
			get{return _Cualidad;}
			set{_Cualidad = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
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

		public String T331_despt
		{
			get{return _T331_despt;}
			set{_T331_despt = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}

		public Double TotalHorasReportadas
		{
			get{return _TotalHorasReportadas;}
			set{_TotalHorasReportadas = value;}
		}

		public Double TotalJornadasReportadas
		{
			get{return _TotalJornadasReportadas;}
			set{_TotalJornadasReportadas = value;}
		}


        #endregion

	}
}
