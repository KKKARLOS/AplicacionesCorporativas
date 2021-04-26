using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{

    public class TipoAsuntoCat
    {
        /// <summary>
        /// Summary description for TipoAsuntoCat
        /// </summary>
        #region Private Variables
        private String _t384_destipo;
        private Int32? _t384_idtipo;
        private Byte? _t384_orden;
        private Byte _nOrden;
        private Byte _nAscDesc;

        #endregion

        #region Public Properties
        public String t384_destipo
        {
            get { return _t384_destipo; }
            set { _t384_destipo = value; }
        }

        public Int32? t384_idtipo
        {
            get { return _t384_idtipo; }
            set { _t384_idtipo = value; }
        }

        public Byte? t384_orden
        {
            get { return _t384_orden; }
            set { _t384_orden = value; }
        }

        public Byte nOrden
        {
            get { return _nOrden; }
            set { _nOrden = value; }
        }
        public Byte nAscDesc
        {
            get { return _nAscDesc; }
            set { _nAscDesc = value; }
        }


        #endregion

    }
}
