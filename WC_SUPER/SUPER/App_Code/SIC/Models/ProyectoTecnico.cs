using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.SIC.Models
{

    /// <summary>
    /// Descripción breve de ProyectoTecnico
    /// </summary>
    public class ProyectoTecnico
    {
        #region Public Properties

        public int t331_idpt { get; set; }
        public int t305_idproyectosubnodo { get; set; }
        public int t301_idproyecto { get; set; }
        public string t331_despt { get; set; }
        public string t301_denominacion { get; set; }

        #endregion
    }
}