using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AccionPT
    {

        /// <summary>
        /// Summary description for AccionPT
        /// </summary>
        #region Public Properties

        public Int32 T409_idasunto { get; set; }
        public String T409_desasunto { get; set; }
        public DateTime T410_fcreacion { get; set; }
        public String T410_alerta { get; set; }
        public Byte T410_avance { get; set; }
        public String T410_desaccion { get; set; }
        public String T410_desaccionlong { get; set; }
        public String T410_dpto { get; set; }
        public DateTime? T410_ffin { get; set; }
        public DateTime? T410_flimite { get; set; }
        public Int32 T410_idaccion { get; set; }
        public String T410_obs { get; set; }
        public Int32 t314_idusuario_responsable { get; set; }
        public Int32 t301_idproyecto { get; set; }
        public String t301_denominacion { get; set; }
        public Int32 t331_idpt { get; set; }
        public String t331_despt { get; set; }
        #endregion

	}
}
