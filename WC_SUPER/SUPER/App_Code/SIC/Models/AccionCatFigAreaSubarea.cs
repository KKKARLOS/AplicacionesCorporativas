using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
    public class AccionCatFigAreaSubarea
    {
        public AccionCatFigAreaSubarea()
        {
        }

        public Int32 ta204_idaccionpreventa { get; set; }
        public DateTime ta204_fechafinestipulada { get; set; }
        public DateTime ta204_fechacreacion { get; set; }
        public DateTime ta204_fechafinreal { get; set; }
        public String ta204_estado { get; set; }
        public String ta204_descripcion { get; set; }
        public String ta204_observaciones { get; set; }
        public Int32 ta201_idsubareapreventa { get; set; }
        public Int32 t001_idficepi_lider { get; set; }
        public Int32 t001_idficepi_promotor { get; set; }
        public Int32 ta205_idtipoaccionpreventa { get; set; }
        public Int32 ta206_idsolicitudpreventa { get; set; }
        //t331_idpt
        public String tipoAccion { get; set; }
        public String unidadPreventa { get; set; }
        public String areaPreventa { get; set; }
        public String subareaPreventa { get; set; }
        public String lider { get; set; }
        public String promotor { get; set; }
        public String comercial { get; set; }
        public Int32 ta206_iditemorigen { get; set; }
        public String ta206_itemorigen { get; set; }
        public Double importe { get; set; }
        public String den_item { get; set; }
        public String ta206_denominacion { get; set; }
        public String den_cuenta { get; set; }
        public String den_unidadcomercial { get; set; }
        public String moneda { get; set; }
        public Boolean ta208_negrita { get; set; }
        public Int16 ta205_plazominreq { get; set; }
    }
}