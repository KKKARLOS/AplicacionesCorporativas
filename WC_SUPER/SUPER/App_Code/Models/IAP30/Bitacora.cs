using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.IAP30.Models
{

    /// <summary>
    /// Descripción breve de Bitacora
    /// </summary>
    public class Bitacora
    {
        #region Public Properties
        public Int32 codigo { get; set; }
        public String ASoACC { get; set; }//ASU o ACC (ASUNTO O ACCIÓN)
        public String denominacion { get; set; }
        public String desTipo { get; set; }
        public String severidad { get; set; }
        public String prioridad { get; set; }
        public DateTime? fNotificacion { get; set; }
        public DateTime? fLimite { get; set; }
        public DateTime? fFin { get; set; }
        public String estado { get; set; }
        public String descripcion { get; set; }
        public byte? avance { get; set; }

        public Int32 idPE { get; set; }
        public Int32 idPSN { get; set; }
        public Int32? idPT { get; set; }
        public Int32? idTarea { get; set; }
        public String denPE { get; set; }
        public String denPT { get; set; }
        public String denTarea { get; set; }

        #endregion
    }
}