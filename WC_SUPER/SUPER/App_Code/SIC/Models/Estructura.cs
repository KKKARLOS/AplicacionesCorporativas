using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class Estructura
    {
        public Estructura()
        {
        }

        public int ta199_idunidadpreventa { get; set; }
        public string ta199_denominacion { get; set; }
        public int ta200_idareapreventa { get; set; }
        public string ta200_denominacion { get; set; }
        public int ta201_idsubareapreventa { get; set; }
        public string ta201_denominacion { get; set; }
        public Boolean ta201_obligalider { get; set; }
    }
}