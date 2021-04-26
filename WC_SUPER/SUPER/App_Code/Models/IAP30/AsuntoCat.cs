using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AsuntoCat
/// </summary>
/// using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{

    public class AsuntoCat
    {
        #region Public Properties
        public Int32 idAsunto { get; set; }
        public String desAsunto { get; set; }
        public String desTipo { get; set; }
        public String severidad { get; set; }
        public String prioridad { get; set; }
        public DateTime? fLimite { get; set; }
        public DateTime? fNotificacion { get; set; }
        public String estado { get; set; }
        public Int32 idUserResponsable { get; set; }
        #endregion
    }
}