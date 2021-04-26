using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class ParametrizacionDestinoPT
    {
        /// <summary>
        /// Summary description for PARAMETRIZACIONDESTINOPT
        /// </summary>
        #region Private Variables
        private Int32 _ta212_idorganizacioncomercial;
        private Int32 _t001_idficepi_comercial;
        private Int32? _t331_idpt;
        

        #endregion

        #region Public Properties
        public Int32 ta212_idorganizacioncomercial
        {
            get { return _ta212_idorganizacioncomercial; }
            set { _ta212_idorganizacioncomercial = value; }
        }
        public Int32 t001_idficepi_comercial
        {
            get { return _t001_idficepi_comercial; }
            set { _t001_idficepi_comercial = value; }
        }
        public Int32? t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        public String denOC { get; set; }
        public String denProfesional { get; set; }
        public String denPT { get; set; }
        public String bd { get; set; }
        public bool baja { get; set; }
        public Boolean ta213_nocambioautomatico { get; set; }

        #endregion

    }
}
