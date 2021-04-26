using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAPTotalSemana
    {

        /// <summary>
        /// Summary description for ConsumoIAPTotalSemana
        /// </summary>
		#region Private Variables
		private Double _tot_Lunes;
		private Double _tot_Martes;
		private Double _tot_Miercoles;
		private Double _tot_Jueves;
		private Double _tot_Viernes;
		private Double _tot_Sabado;
		private Double _tot_Domingo;

		#endregion

		#region Public Properties
		public Double tot_Lunes
		{
			get{return _tot_Lunes;}
			set{_tot_Lunes = value;}
		}

		public Double tot_Martes
		{
			get{return _tot_Martes;}
			set{_tot_Martes = value;}
		}

		public Double tot_Miercoles
		{
			get{return _tot_Miercoles;}
			set{_tot_Miercoles = value;}
		}

		public Double tot_Jueves
		{
			get{return _tot_Jueves;}
			set{_tot_Jueves = value;}
		}

		public Double tot_Viernes
		{
			get{return _tot_Viernes;}
			set{_tot_Viernes = value;}
		}

		public Double tot_Sabado
		{
			get{return _tot_Sabado;}
			set{_tot_Sabado = value;}
		}

		public Double tot_Domingo
		{
			get{return _tot_Domingo;}
			set{_tot_Domingo = value;}
		}


        #endregion

	}
}
