using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class AccionLider
    {
        public AccionLider()
        {
        }

        public string ta205_denominacion { get; set; }
        public int t001_idficepi_lider { get; set; }
        public string profesional { get; set; }
        public bool posibleLider { get; set; }
        public string areaPreventa { get; set; }
        public string subareaPreventa { get; set; }
    }
}