using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class TipoAccionPreventa
    {

        /// <summary>
        /// Summary description for TipoAccionPreventa
        /// </summary>
        #region Private Variables
        private Int16 _ta205_idtipoaccionpreventa;
        private String _ta205_denominacion;
        private Boolean _ta205_origen_on;
        private Boolean _ta205_origen_partida;
        private Boolean _ta205_origen_super;
        private Boolean _ta205_estadoactiva;
        private Boolean _ta205_unicaxaccion;
        private Int32 _ta205_orden;
        private byte? _ta205_plazominreq;

        #endregion

        #region Public Properties
        public Int16 ta205_idtipoaccionpreventa
        {
            get { return _ta205_idtipoaccionpreventa; }
            set { _ta205_idtipoaccionpreventa = value; }
        }

        public String ta205_denominacion
        {
            get { return _ta205_denominacion; }
            set { _ta205_denominacion = value; }
        }

        public Boolean ta205_origen_on
        {
            get { return _ta205_origen_on; }
            set { _ta205_origen_on = value; }
        }

        public Boolean ta205_origen_partida
        {
            get { return _ta205_origen_partida; }
            set { _ta205_origen_partida = value; }
        }

        public Boolean ta205_origen_super
        {
            get { return _ta205_origen_super; }
            set { _ta205_origen_super = value; }
        }

        public Boolean ta205_estadoactiva
        {
            get { return _ta205_estadoactiva; }
            set { _ta205_estadoactiva = value; }
        }

        public Boolean ta205_unicaxaccion
        {
            get { return _ta205_unicaxaccion; }
            set { _ta205_unicaxaccion = value; }
        }

        public Int32 ta205_orden
        {
            get { return _ta205_orden; }
            set { _ta205_orden = value; }
        }

        public byte? ta205_plazominreq
        {
            get { return _ta205_plazominreq; }
            set { _ta205_plazominreq = value; }
        }

        #endregion

    }
}
