using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class SolicCatSuperRF
    {
           

        public string estado { get; set; }
        public string  estadoSolicitud { get; set; }
        public string itemorigen { get; set; }
        public string iditemorigen { get; set; }
        public string importeDesde { get; set; }
        public string importeHasta { get; set; }
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string solicitud { get; set; }
        public string promotor { get; set; }
        public string comercial { get; set; }
        public string[] lideres { get; set; }
        public string[] clientes { get; set; }
        public string[] acciones { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public SolicCatSuperRF()
        {
            estado = null;
            itemorigen = null;
            iditemorigen = null;
            importeDesde = null;
            importeHasta = null;
            ffinDesde = null;
            ffinHasta = null;
            solicitud = null;
            promotor = null;
            comercial = null;
            lideres = null;
            clientes = null;
            acciones = null;
            unidades = null;
            areas = null;
            subareas = null;
        }

    
    }
}