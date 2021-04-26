using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{
    public class ProyectoEconomico
    {
        public string nom_proyecto { get; set; }
        public int cod_cliente { get; set; }
        public int cod_contrato { get; set; }
        public int cod_extension { get; set; }
        public int cod_naturaleza { get; set; }
        public byte modalidad { get; set; }
        public DateTime fini_prevista { get; set; }
        public DateTime ffin_prevista { get; set; }
        public string categoria { get; set; }
        public string modelo_coste { get; set; }
        public string modelo_tarifa { get; set; }
        public bool automatico { get; set; }
        public DateTime fecha_sap { get; set; }

        public int cod_proyecto { get; set; }
        public int cod_subnodo { get; set; }
        public string cualidad { get; set; }
        public int cod_usuario_responsable { get; set; }
        public string seudonimo { get; set; }

        public int t305_idproyectosubnodo { get; set; }
        public string t301_estado { get; set; }
        public string t302_denominacion { get; set; }
        public string t301_categoria { get; set; }
        public string t305_cualidad { get; set; }
        public string proy_responsable { get; set; }
        public string codred_gestor_produccion { get; set; }
        public int umc_iap_nodo { get; set; }

    }
}