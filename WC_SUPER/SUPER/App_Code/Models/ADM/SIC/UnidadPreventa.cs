using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class UnidadPreventa
    {

        /// <summary>
        /// Summary description for UNIDADPREVENTA
        /// </summary>
        #region Private Variables
        private Int16 _ta199_idunidadpreventa;
        private String _ta199_denominacion;
        private Boolean _ta199_estadoactiva;

        #endregion

        #region Public Properties
        public Int16 ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }

        public String ta199_denominacion
        {
            get { return _ta199_denominacion; }
            set { _ta199_denominacion = value; }
        }

        public Boolean ta199_estadoactiva
        {
            get { return _ta199_estadoactiva; }
            set { _ta199_estadoactiva = value; }
        }


        #endregion

    }
}
