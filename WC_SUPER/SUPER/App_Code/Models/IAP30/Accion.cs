using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{   
   public class Accion
    {

        /// <summary>
        /// Summary description for Accion
        /// </summary>

		#region Public Properties
            public int t301_idproyecto { get; set; }
            public String DesPE { get; set; }
            public int t382_idasunto { get; set; }
            public String t382_desasunto { get; set; }
            public int t383_idaccion { get; set; }
            public String t383_desaccion { get; set; }
            public String t383_desaccionlong { get; set; }
            public DateTime t383_fcreacion { get; set; }
            public DateTime? t383_flimite { get; set; }
            public DateTime? t383_ffin { get; set; }
            public byte t383_avance { get; set; }
            public String t383_obs { get; set; }
            public String t383_dpto { get; set; }
            public String t383_alerta { get; set; }
            public Int32 t382_responsable { get; set; }
            //public String Responsable { get; set; }
        #endregion

	}
}
