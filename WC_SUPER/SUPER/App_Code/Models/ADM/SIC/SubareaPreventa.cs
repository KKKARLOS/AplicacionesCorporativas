using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.ADM.SIC.Models
{
    public class SubareaPreventa
    {
        /// <summary>
        /// Summary description for SubareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta201_idsubareapreventa;
        private String _ta201_denominacion;
        private Boolean _ta201_estadoactiva;
        //private String _ta201_asignacionlider;
        private Boolean _ta201_permitirautoasignacionlider;
        private Int32 _t001_idficepi_responsable;
        private String _Responsable;
        private Int16 _ta199_idunidadpreventa;
        private String _ta199_denominacion;
        private Int32 _ta200_idareapreventa;
        private String _ta200_denominacion;

        #endregion

        #region Public Properties
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
        public Boolean ta201_estadoactiva
        {
            get { return _ta201_estadoactiva; }
            set { _ta201_estadoactiva = value; }
        }
        //public String ta201_asignacionlider
        //{
        //    get { return _ta201_asignacionlider; }
        //    set { _ta201_asignacionlider = value; }
        //}
        public Boolean ta201_permitirautoasignacionlider
        {
            get { return _ta201_permitirautoasignacionlider; }
            set { _ta201_permitirautoasignacionlider = value; }
        }
        public Int32 t001_idficepi_responsable
        {
            get { return _t001_idficepi_responsable; }
            set { _t001_idficepi_responsable = value; }
        }
        public String Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        public Int16 ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }
        public String ta199_denominacion
        {
            get { return _ta199_denominacion; }
            set { _ta199_denominacion = value; }
        }

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

        #endregion

    }
}
