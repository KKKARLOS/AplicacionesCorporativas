using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class TipoTareaPreventa
    {

        /// <summary>
        /// Summary description for TipoAccionPreventa
        /// </summary>
        #region Private Variables
        private Int16 _ta219_idtipotareapreventa;
        private String _ta219_denominacion;
        private Boolean _ta219_estadoactiva;
        private Int32 _ta219_orden;


        #endregion

        #region Public Properties
        public Int16 ta219_idtipotareapreventa
        {
            get { return _ta219_idtipotareapreventa; }
            set { _ta219_idtipotareapreventa = value; }
        }

        public String ta219_denominacion
        {
            get { return _ta219_denominacion; }
            set { _ta219_denominacion = value; }
        }
              
        public Boolean ta219_estadoactiva
        {
            get { return _ta219_estadoactiva; }
            set { _ta219_estadoactiva = value; }
        }        

        public Int32 ta219_orden
        {
            get { return _ta219_orden; }
            set { _ta219_orden = value; }
        }
        #endregion

    }
}
