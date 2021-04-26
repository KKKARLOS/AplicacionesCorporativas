using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AccionT
    {

        /// <summary>
        /// Summary description for AccionT
        /// </summary>
		#region Public Properties
        public DateTime T601_fcreacion { get; set; }
        public Int32 T600_idasunto { get; set; }
        public Byte T601_avance { get; set; }
        public String T601_desaccion { get; set; }
        public DateTime? T601_ffin { get; set; }
        public DateTime? T601_flimite { get; set; }
        public Int32 T601_idaccion { get; set; }

        public Int32 t314_idusuario_responsable { get; set; }
        public String T601_alerta { get; set; }
        public String T601_dpto { get; set; }
        public String T601_desaccionlong { get; set; }
        public String T601_obs { get; set; }

        public Int32 t301_idproyecto { get; set; }
        public String t301_denominacion { get; set; }
        public Int32 t331_idpt { get; set; }
        public String t331_despt { get; set; }
        public String t334_desfase { get; set; }
        public String t335_desactividad { get; set; }
        public Int32 t332_idtarea { get; set; }
        public String t332_destarea { get; set; }
        public String t600_desasunto { get; set; }

        #endregion

	}
}
