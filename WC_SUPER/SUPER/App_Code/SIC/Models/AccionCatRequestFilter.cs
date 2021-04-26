using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class AccionCatRequestFilter
    {

        public int? ta206_idsolicitudpreventa { get; set; }
        public int? ta199_idunidadpreventa { get; set; }
        public int? ta200_idareapreventa { get; set; }
        public int? ta201_idsubareapreventa { get; set; }
        public int? ta204_idaccionpreventa { get; set; }
        public short? ta205_idtipoaccionpreventa { get; set; }
        public int? t001_idficepilider { get; set; }
        public int? t001_idficepipromotor { get; set; }
        public String ta204_estado { get; set; }
        public int? ta206_iditemorigen { get; set; }
        public String ta206_itemorigen { get; set; }
        public DateTime? ta204_fechafinestipuladaini { get; set; }
        public DateTime? ta204_fechafinestipuladafin { get; set; }

        public AccionCatRequestFilter()
        {
            ta206_idsolicitudpreventa = null;
            ta199_idunidadpreventa = null;
            ta200_idareapreventa = null;
            ta201_idsubareapreventa = null;
            ta204_idaccionpreventa = null;
            ta205_idtipoaccionpreventa = null;
            t001_idficepilider = null;
            t001_idficepipromotor = null;
            ta204_estado = null;
            ta206_iditemorigen = null;
            ta206_itemorigen = null;
            ta204_fechafinestipuladaini = null;
            ta204_fechafinestipuladafin = null;

        }
    }


    public class AccionCatFigAreaSubareaFilter
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

        public AccionCatFigAreaSubareaFilter()
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

    public class AccionCatAmbitoCRMFilter
    {

        public string estado { get; set; }
        public string itemorigen { get; set; }
        public string iditemorigen { get; set; }
        public string importeDesde { get; set; }
        public string importeHasta { get; set; }
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string promotor { get; set; }
        public string[] lideres { get; set; }
        public string[] clientes { get; set; }
        public string[] acciones { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public AccionCatAmbitoCRMFilter()
        {
            estado = null;
            itemorigen = null;
            iditemorigen = null;
            importeDesde = null;
            importeHasta = null;
            ffinDesde = null;
            ffinHasta = null;
            promotor = null;
            lideres = null;
            clientes = null;
            acciones = null;
            unidades = null;
            areas = null;
            subareas = null;
        }

    }


}