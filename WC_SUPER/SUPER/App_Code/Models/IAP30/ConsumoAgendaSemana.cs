using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoAgendaSemana
    {

        /// <summary>
        /// Summary description for ConsumoAgendaSemana
        /// </summary>
		#region Private Variables
		private Decimal _tot_Lunes;
		private Decimal _tot_Martes;
		private Decimal _tot_Miercoles;
		private Decimal _tot_Jueves;
		private Decimal _tot_Viernes;
		private Decimal _tot_Sabado;
		private Decimal _tot_Domingo;

		#endregion

		#region Public Properties
		public Decimal tot_Lunes
		{
			get{return _tot_Lunes;}
			set{_tot_Lunes = value;}
		}

		public Decimal tot_Martes
		{
			get{return _tot_Martes;}
			set{_tot_Martes = value;}
		}

		public Decimal tot_Miercoles
		{
			get{return _tot_Miercoles;}
			set{_tot_Miercoles = value;}
		}

		public Decimal tot_Jueves
		{
			get{return _tot_Jueves;}
			set{_tot_Jueves = value;}
		}

		public Decimal tot_Viernes
		{
			get{return _tot_Viernes;}
			set{_tot_Viernes = value;}
		}

		public Decimal tot_Sabado
		{
			get{return _tot_Sabado;}
			set{_tot_Sabado = value;}
		}

		public Decimal tot_Domingo
		{
			get{return _tot_Domingo;}
			set{_tot_Domingo = value;}
		}


        #endregion

	}
}
