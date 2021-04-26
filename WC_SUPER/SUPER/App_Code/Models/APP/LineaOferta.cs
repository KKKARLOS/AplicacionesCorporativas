using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.APP.Models
{
    /// <summary>
    /// Descripción breve de LineaOferta
    /// </summary>
    public class LineaOferta
    {
        #region Private Variables
        private short _indentacion;
        private Int32 _ta212_idorganizacioncomercial;
        private String _ta212_denominacion;
        private Int32 _t195_idlineaoferta;
        private String _t195_denominacion;
        private bool _activo;
        #endregion

        #region Public Properties
        public short indentacion
        {
            get { return _indentacion; }
            set { _indentacion = value; }
        }
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
        public Int32 t195_idlineaoferta
        {
            get { return _t195_idlineaoferta; }
            set { _t195_idlineaoferta = value; }
        }
        public String t195_denominacion
        {
            get { return _t195_denominacion; }
            set { _t195_denominacion = value; }
        }
        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        #endregion
    }
}