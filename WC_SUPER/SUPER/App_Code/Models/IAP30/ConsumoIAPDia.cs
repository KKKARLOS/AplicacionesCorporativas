using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAPDia
    {

        /// <summary>
        /// Summary description for ConsumoIAPDia
        /// Global -> consumos en el resto de tareas
        /// Tarea -> consumos en la tarea especificada
        /// </summary>
        #region Public Variables

        public double nHorasDiaGlobal { get; set; }
        public double nHorasDiaTarea { get; set; }

		#endregion


	}
}
