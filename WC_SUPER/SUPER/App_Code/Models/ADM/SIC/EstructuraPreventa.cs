using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.ADM.SIC.Models
{


    /// <summary>
    /// Descripción breve de EstructuraPreventa
    /// </summary>
    public class EstructuraPreventa
    {
        public Boolean estado { get; set; }
        public Int16 indentacion { get; set; }
        public Int16 unidad { get; set; }
        public Int32 area { get; set; }
        public Int32 subarea { get; set; }
        public String denominacion { get; set; }
    }
}