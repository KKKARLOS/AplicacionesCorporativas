using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.APP.Models
{
    public class PRIOALERTAS
    {

        /// <summary>
        /// Summary description for PRIOALERTAS
        /// </summary>
        #region Private Variables
        private Byte _t820_idalerta_1;
        private Byte _t820_idalerta_2;
        private Byte _t820_idalerta_g;

        #endregion

        #region Public Properties
        public Byte t820_idalerta_1
        {
            get { return _t820_idalerta_1; }
            set { _t820_idalerta_1 = value; }
        }

        public Byte t820_idalerta_2
        {
            get { return _t820_idalerta_2; }
            set { _t820_idalerta_2 = value; }
        }

        public Byte t820_idalerta_g
        {
            get { return _t820_idalerta_g; }
            set { _t820_idalerta_g = value; }
        }

        public string denAlert1 { get; set; }
        public string denAlert2 { get; set; }
        public string denAlertG { get; set; }

        public byte grupo1 { get; set; }
        public byte grupo2 { get; set; }
        public byte grupoG { get; set; }

        public string denGrupo1 { get; set; }
        #endregion

    }
}
