using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class SolicitudPreventa
    {

        #region Private Variables
        private Int32 _ta206_idsolicitudpreventa;
        private String _ta206_denominacion;
        private String _ta206_estado;
        private DateTime _ta206_fechacreacion;
        private Int32 _t001_idficepi_promotor;
        private Int32? _ta206_iditemorigen;
        private String _ta206_itemorigen;
        private Int32 _t332_idtarea;
        private Int32? _ta200_idareapreventa;

        #endregion

        #region Public Properties
        public Int32 ta206_idsolicitudpreventa
        {
            get { return _ta206_idsolicitudpreventa; }
            set { _ta206_idsolicitudpreventa = value; }
        }

        public String ta206_denominacion
        {
            get { return _ta206_denominacion; }
            set { _ta206_denominacion = value; }
        }

        public String ta206_estado
        {
            get { return _ta206_estado; }
            set { _ta206_estado = value; }
        }

        public DateTime ta206_fechacreacion
        {
            get { return _ta206_fechacreacion; }
            set { _ta206_fechacreacion = value; }
        }

        public Int32 t001_idficepi_promotor
        {
            get { return _t001_idficepi_promotor; }
            set { _t001_idficepi_promotor = value; }
        }

        public Int32? ta206_iditemorigen
        {
            get { return _ta206_iditemorigen; }
            set { _ta206_iditemorigen = value; }
        }

        public String ta206_itemorigen
        {
            get { return _ta206_itemorigen; }
            set { _ta206_itemorigen = value; }
        }
        public Int32 t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }
        public Int32? ta200_idareapreventa
        {
            get { return _ta200_idareapreventa; }
            set { _ta200_idareapreventa = value; }
        }
        public bool botonactivo { get; set; }

        #endregion

    }
}
