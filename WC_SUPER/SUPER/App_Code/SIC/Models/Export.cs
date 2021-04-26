using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class ExportAccionesFilter
    {

        public string estado { get; set; }
        public string itemorigen { get; set; }
        public string iditemorigen { get; set; }
        public string importeDesde { get; set; }
        public string importeHasta { get; set; }
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string promotor { get; set; }
        public string comercial { get; set; }
        public string[] lideres { get; set; }
        public string[] clientes { get; set; }
        public string[] acciones { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public ExportAccionesFilter()
        {
            estado = null;
            itemorigen = null;
            iditemorigen = null;
            importeDesde = null;
            importeHasta = null;
            ffinDesde = null;
            ffinHasta = null;
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

    public class ExportTareasFilter
    {

        public string estado { get; set; }
        public string estado_tarea { get; set; }
        public string itemorigen { get; set; }
        public string iditemorigen { get; set; }
        public string importeDesde { get; set; }
        public string importeHasta { get; set; }
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string ffinDesde_tarea { get; set; }
        public string ffinHasta_tarea { get; set; }
        public string promotor { get; set; }
        public string comercial { get; set; }
        public string[] lideres { get; set; }
        public string[] clientes { get; set; }
        public string[] acciones { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public ExportTareasFilter()
        {
            estado = null;
            estado_tarea = null;
            itemorigen = null;
            iditemorigen = null;
            importeDesde = null;
            importeHasta = null;
            ffinDesde = null;
            ffinHasta = null;
            ffinDesde_tarea = null;
            ffinHasta_tarea = null;
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

    public class ExportCargaTrabajoFilter
    {

        public string estado { get; set; }
        public string estado_tarea { get; set; }
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string ffinDesde_tarea { get; set; }
        public string ffinHasta_tarea { get; set; }
        public string[] lideres { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public ExportCargaTrabajoFilter()
        {
            estado = null;
            estado_tarea = null;
            ffinDesde = null;
            ffinHasta = null;
            ffinDesde_tarea = null;
            ffinHasta_tarea = null;
            lideres = null;
            unidades = null;
            areas = null;
            subareas = null;
        }

    }
}