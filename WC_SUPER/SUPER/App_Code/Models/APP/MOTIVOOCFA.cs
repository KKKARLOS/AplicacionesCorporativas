using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{
    public class MOTIVOOCFA
    {
        /// <summary>
        /// Summary description for MOTIVOOCFA
        /// </summary>
        #region Private Variables
        private Int32 _t840_idmotivo;
        private String _t820_tipo;
        private String _t840_descripcion;

        #endregion

        #region Public Properties
        public Int32 t840_idmotivo
        {
            get { return _t840_idmotivo; }
            set { _t840_idmotivo = value; }
        }
        public String t820_tipo
        {
            get { return _t820_tipo; }
            set { _t820_tipo = value; }
        }
        public String t840_descripcion
        {
            get { return _t840_descripcion; }
            set { _t840_descripcion = value; }
        }
        public string desTipo { get; set; }

        #endregion

    }
}
