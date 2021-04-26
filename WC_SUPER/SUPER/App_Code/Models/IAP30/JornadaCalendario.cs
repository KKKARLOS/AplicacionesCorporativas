using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class JornadaCalendario
    {

        /// <summary>
        /// Summary description for JornadaCalendario
        /// </summary>
		#region Private Variables
        private Int32 _dia;
		private Int32 _estilo_festivo;
		private Double _esfuerzo;
		private Double _horas_estandar;
		private DateTime _dia_entero;
		private Double? _horas_planificadas;
		private Int32 _dia_festivo;
		private Int32 _dia_vacaciones;

		#endregion

		#region Public Properties
                
		public Int32 dia
		{
			get{return _dia;}
			set{_dia = value;}
		}

		public Int32 estilo_festivo
		{
			get{return _estilo_festivo;}
			set{_estilo_festivo = value;}
		}

		public Double esfuerzo
		{
			get{return _esfuerzo;}
			set{_esfuerzo = value;}
		}

		public Double horas_estandar
		{
			get{return _horas_estandar;}
			set{_horas_estandar = value;}
		}

		public DateTime dia_entero
		{
			get{return _dia_entero;}
			set{_dia_entero = value;}
		}

		public Double? horas_planificadas
		{
			get{return _horas_planificadas;}
			set{_horas_planificadas = value;}
		}

		public Int32 dia_festivo
		{
			get{return _dia_festivo;}
			set{_dia_festivo = value;}
		}

		public Int32 dia_vacaciones
		{
			get{return _dia_vacaciones;}
			set{_dia_vacaciones = value;}
		}


        #endregion

	}
}
