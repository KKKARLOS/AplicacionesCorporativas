using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
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

        public short ta205_plazominreq { get; set; }

        #endregion

        public TipoAccionPreventa()
        {
        }

        public TipoAccionPreventa(Int16 ta205_idtipoaccionpreventa)
        {
            _ta205_idtipoaccionpreventa = ta205_idtipoaccionpreventa;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            TipoAccionPreventa objAsTAP = obj as TipoAccionPreventa;
            if (objAsTAP == null) return false;
            else return Equals(objAsTAP);
        }

        public override int GetHashCode()
        {
            return ta205_idtipoaccionpreventa;
        }

        public bool Equals(TipoAccionPreventa other)
        {
            if (other == null) return false;
            return (this.ta205_idtipoaccionpreventa.Equals(other.ta205_idtipoaccionpreventa));
        }
    }
}
