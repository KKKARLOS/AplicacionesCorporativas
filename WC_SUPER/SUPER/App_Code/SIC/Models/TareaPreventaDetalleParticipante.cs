using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
    public class TareaPreventaDetalleParticipante
    {
        public int ta207_idtareapreventa { get; set; }
        public int ta201_idsubareapreventa { get; set; }
        public int ta204_idaccionpreventa { get; set; }
        public String ta207_denominacion { get; set; }
        public String ta207_descripcion { get; set; }
        public String ta207_observaciones { get; set; }
        public DateTime ta207_fechafinprevista { get; set; }
        public string ta205_denominacion { get; set; }
        public int t001_idficepi_lider { get; set; }
        public string lider { get; set; }        
        public string ta207_comentarios  { get; set; }
        public Nullable<DateTime> ta207_fechafinreal { get; set; }
        public DateTime ta207_fechacreacion { get; set; }
        public string ta207_estado { get; set; }
        public string ta214_estado{ get; set; }
        public String ta207_motivoanulacion { get; set; }
        public int? ta219_idtipotareapreventa { get; set; }
        public string ta219_denominacion { get; set; }
    }
}