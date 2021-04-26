using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
 
    public class AccionPreventaCAT
    {
        public AccionPreventaCAT()
        {
        }

	    public int ta204_idaccionpreventa {get; set; }
		public DateTime ta204_fechafinestipulada {get; set; }
		public DateTime ta204_fechacreacion {get; set; }
        public DateTime ta204_fechafinreal { get; set; }
        public string estadoAccion {get; set; }
		public string ta204_descripcion {get; set; }
		public string ta204_observaciones {get; set; }
		public int ta201_idsubareapreventa {get; set; }
		public int t001_idficepi_lider {get; set; }
		public int t001_idficepi_promotor {get; set; }
		public int ta205_idtipoaccionpreventa {get; set; }
		public int ta206_idsolicitudpreventa {get; set; }
		public string tipoAccion {get; set; }
		public string unidadPreventa {get; set; }
		public string areaPreventa {get; set; }
		public string subareaPreventa {get; set; }
        public bool ta201_permitirautoasignacionlider { get; set; }
		public string lider {get; set; }
        public string promotor { get; set; }
        public string ta206_itemorigen { get; set; }
        public int ta206_iditemorigen { get; set; }
        public double importe {get; set;}
        public string den_item { get; set; }
        public string ta206_denominacion { get; set; }
        public string den_cuenta { get; set; }
        public string den_unidadcomercial { get; set; }
        public string moneda { get; set; }
        public Boolean ta208_negrita { get; set; }
        public Int16 ta205_plazominreq { get; set; }

    }

}