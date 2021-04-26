using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoAgendaDia
    {

        /// <summary>
        /// Summary description for ConsumoAgendaDia
        /// </summary>
		#region Private Variables
		private Decimal _tot_Dia;

		#endregion

		#region Public Properties
		public Decimal tot_Dia
		{
			get{return _tot_Dia;}
			set{_tot_Dia = value;}
		}


        #endregion

	}
}
