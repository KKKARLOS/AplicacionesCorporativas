using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{
    /// <summary>
    /// Descripción breve de OportunidadNegocio
    /// </summary>
    public class OportunidadNegocio
    {
        #region Public Properties
        public Int32 t303_idnodo { get; set; }
        //public String nomune { get; set; }
        public Int32 t306_icontrato { get; set; }
        public Int32 t377_idextension { get; set; }
        public String t377_denominacion { get; set; }
        public Int32 ta212_idorganizacioncomercial { get; set; }
        public String ta212_denominacion { get; set; }//Denominación de la organización comercial
        public Int32 t302_idcliente_contrato { get; set; }
        public String cliente { get; set; }//Nombre comercial del cliente
        public Int32 t314_idusuario_comercialhermes { get; set; }
        public String comercial { get; set; }
        public Int32 t314_idusuario_gestorprod { get; set; }
        public String codred_gestor_produccion { get; set; }
        public String gestor { get; set; }
        public Int32 t314_idusuario_responsable { get; set; }
        public String responsable { get; set; }
        public DateTime t377_fechacontratacion { get; set; }
        public decimal t377_importeser { get; set; }
        public decimal t377_marpreser { get; set; }
        public decimal t377_importepro { get; set; }
        public decimal t377_marprepro { get; set; }
        public String t422_idmoneda { get; set; }
        public String tipocontrato { get; set; }
        public decimal duracion { get; set; }

        public int t195_idlineaoferta { get; set; }
        public String t195_denominacion { get; set; }

        #endregion
    }
}