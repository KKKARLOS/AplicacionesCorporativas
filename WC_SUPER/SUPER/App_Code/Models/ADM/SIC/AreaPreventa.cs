using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class AreaPreventa
    {

        /// <summary>
        /// Summary description for AreaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta200_idareapreventa;
        private String _ta200_denominacion;
        private Boolean _ta200_estadoactiva;
        private Int32 _t001_idficepi_responsable;
        private Int16 _ta199_idunidadpreventa;
        private Int32? _t331_idpt;
        private String _Responsable;

        #endregion

        #region Public Properties
        public Int32 ta200_idareapreventa
        {
            get { return _ta200_idareapreventa; }
            set { _ta200_idareapreventa = value; }
        }

        public String ta200_denominacion
        {
            get { return _ta200_denominacion; }
            set { _ta200_denominacion = value; }
        }

        public Boolean ta200_estadoactiva
        {
            get { return _ta200_estadoactiva; }
            set { _ta200_estadoactiva = value; }
        }

        public Int32 t001_idficepi_responsable
        {
            get { return _t001_idficepi_responsable; }
            set { _t001_idficepi_responsable = value; }
        }

        public Int16 ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }

        public Int32? t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        public String Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        #endregion

    }
}
