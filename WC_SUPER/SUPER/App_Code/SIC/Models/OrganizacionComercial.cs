using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class OrganizacionComercial
    {

        /// <summary>
        /// Summary description for OrganizacionComercial
        /// </summary>
        #region Private Variables
        private Int32 _ta212_idorganizacioncomercial;
        private String _ta212_denominacion;
        private String _ta212_codigoexterno;
        private Boolean _ta212_activa;

        #endregion

        #region Public Properties
        public Int32 ta212_idorganizacioncomercial
        {
            get { return _ta212_idorganizacioncomercial; }
            set { _ta212_idorganizacioncomercial = value; }
        }

        public String ta212_denominacion
        {
            get { return _ta212_denominacion; }
            set { _ta212_denominacion = value; }
        }

        public String ta212_codigoexterno
        {
            get { return _ta212_codigoexterno; }
            set { _ta212_codigoexterno = value; }
        }

        public Boolean ta212_activa
        {
            get { return _ta212_activa; }
            set { _ta212_activa = value; }
        }


        #endregion

    }
}
