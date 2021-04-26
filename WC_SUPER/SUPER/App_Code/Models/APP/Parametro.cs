using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{
    public class Parametro
    {
        #region Public Properties
        public Int32 codTabla { get; set; }
        public Int32 codParametro { get; set; }
        public short orden { get; set; }
        public String valor { get; set; }
        public String denominacion { get; set; }

        #endregion
    }
}