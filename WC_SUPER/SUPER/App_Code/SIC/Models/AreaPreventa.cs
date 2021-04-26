using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
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
        private string _responsable;
        private Int16 _ta199_idunidadpreventa;
        private bool _accesoadetalle;
        private string _ta199_denominacion;
        private string _ta202_figura;
        private Boolean _esbaja;
        private int _t001_idficepi;
        private string _profesional;
        private Int32 _ta201_idsubareapreventa;
        private String _ta201_denominacion;
        private int _t331_idpt;

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

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public String responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }

        public Int32 ta201_idsubareapreventa
        {
            get { return _ta201_idsubareapreventa; }
            set { _ta201_idsubareapreventa = value; }
        }

        public String ta201_denominacion
        {
            get { return _ta201_denominacion; }
            set { _ta201_denominacion = value; }
        }

        public Int16 ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }


        public bool accesoadetalle {
            get { return _accesoadetalle; }
            set { _accesoadetalle = value; }
        }

        public string ta199_denominacion {
            get { return _ta199_denominacion; }
            set { _ta199_denominacion = value; }
        }

        public string ta202_figura
        {
            get { return _ta202_figura; }
            set { _ta202_figura = value; }
        }

        public bool esbaja
        {
            get { return _esbaja; }
            set { _esbaja = value; }
        }

        public String profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public Int32 t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        #endregion

    }
}
