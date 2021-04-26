using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
    public class TareaPreventaCatalogoParticipante
    {
        public int ta207_idtareapreventa { get; set; }
        public int ta207_idtareapreventa_participante { get; set; }
        public int ta204_idaccionpreventa { get; set; }
        public String ta207_denominacion { get; set; }
        public DateTime ta207_fechafinprevista { get; set; }
        public DateTime ta207_fechafinreal { get; set; }
        public DateTime ta207_fechacreacion { get; set; }
        public string ta205_denominacion { get; set; }
        public string ta201_denominacion { get; set; }
        public string ta200_denominacion { get; set; }
        public string ta199_denominacion { get; set; }
        public string lider { get; set; }
        public int t001_idficepi_lider { get; set; }
        public string  ta207_estado { get; set; }
        public string participantes { get; set; }
        public string den_cuenta { get; set; }
        public string ta206_itemorigen { get; set; }
        public int ta206_iditemorigen { get; set; }
        public string ta206_denominacion { get; set; }
        public string den_item { get; set; }
        public Boolean ta208_negrita { get; set; }
        public Boolean accesoadetalle { get; set; }
        public string solicitante { get; set; }

    }
}