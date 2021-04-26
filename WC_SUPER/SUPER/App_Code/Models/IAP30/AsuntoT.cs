using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoT
    {

        /// <summary>
        /// Summary description for AsuntoT
        /// </summary>
        #region Public Properties
        public Int32? t303_idnodo { get; set; }
        public Int32? t301_idproyecto { get; set; }
        public Int32 t305_idproyectosubnodo { get; set; }
        public String T600_alerta { get; set; }
        public String T600_desasunto { get; set; }
        public String T600_desasuntolong { get; set; }
        public String T600_dpto { get; set; }
        public String T600_estado { get; set; }
        public Double T600_etp { get; set; }
        public Double T600_etr { get; set; }
        public DateTime T600_fcreacion { get; set; }
        public DateTime? T600_ffin { get; set; }
        public DateTime? T600_flimite { get; set; }
        public DateTime T600_fnotificacion { get; set; }
        public Int32 T600_idasunto { get; set; }
        public String T600_notificador { get; set; }
        public String T600_obs { get; set; }
        public String T600_prioridad { get; set; }
        public String T600_refexterna { get; set; }
        public Int32 T600_registrador { get; set; }
        public Int32 T600_responsable { get; set; }
        public String T600_severidad { get; set; }
        public String T600_sistema { get; set; }
        public Int32 t384_idtipo { get; set; }
        public String t384_destipo { get; set; }
        public String Registrador { get; set; }
        public String Responsable { get; set; }
        public String T600_estado_anterior { get; set; }
        public String DesPE { get; set; }
        public String DesEstado { get; set; }
        public String DesSeveridad { get; set; }
        public String DesPrioridad { get; set; }
        public Int32 t331_idpt { get; set; }
        public String t331_despt { get; set; }
        public String t334_desfase { get; set; }
        public String t335_desactividad { get; set; }
        public Int32 t332_idtarea { get; set; }
        public String t332_destarea { get; set; }

		#endregion



	}
}
