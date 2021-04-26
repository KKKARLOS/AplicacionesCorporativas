using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{

    public class SolicCatSuper
    {
        public SolicCatSuper()
        {
        }

        public int ta206_idsolicitudpreventa { get; set; }
        public string ta206_denominacion { get; set; }
        public string ta206_estado { get; set; }
        public string ta206_itemorigen { get; set; }
        public int ta206_iditemorigen { get; set; }
        public DateTime ta206_fechacreacion { get; set; }
        public int numeroacciones { get; set; }
        public int accionesabiertas { get; set; }

        public string ta200_denominacion { get; set; }
        public string ta201_denominacion { get; set; }
        public string promotor { get; set; }
        public string den_cuenta { get; set; }
        public string ta206_motivoanulacion { get; set; }

    }
}