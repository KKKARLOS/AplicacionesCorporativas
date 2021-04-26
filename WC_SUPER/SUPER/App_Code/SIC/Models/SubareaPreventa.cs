using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class SubareaPreventa
    {

        /// <summary>
        /// Summary description for SubareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta201_idsubareapreventa;
        private String _ta201_denominacion;
        private Nullable<Boolean> _ta201_estadoactiva;
        private bool _ta201_permitirautoasignacionlider;
        private Int32 _ta200_idareapreventa;
        private String _ta200_denominacion;
        private Int32 _t001_idficepi_responsable;
        private String _responsable;
        private bool _accesoAdetalle;
        private bool _mantenimientoDeFiguras;
        private int _t001_idficepi;
        private string _profesional;
        private string _ta203_figura;
        private bool _checkDelegado;
        private bool _checkColaborador;
        private bool _checkLider;
        private string _profesionales;

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

        public Nullable<Boolean> ta201_estadoactiva
        {
            get { return _ta201_estadoactiva; }
            set { _ta201_estadoactiva = value; }
        }

        public bool ta201_permitirautoasignacionlider
        {
            get { return _ta201_permitirautoasignacionlider; }
            set { _ta201_permitirautoasignacionlider = value; }
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

        public Int32 t001_idficepi_responsable
        {
            get { return _t001_idficepi_responsable; }
            set { _t001_idficepi_responsable = value; }
        }

        public String responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }

        public Boolean accesoAdetalle
        {
            get { return _accesoAdetalle; }
            set { _accesoAdetalle = value; }
        }

        public Boolean mantenimientoDeFiguras
        {
            get { return _mantenimientoDeFiguras; }
            set { _mantenimientoDeFiguras = value; }
        }

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public String profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public string ta203_figura
        {
            get { return _ta203_figura; }
            set { _ta203_figura = value; }
        }

        public Boolean checkDelegado
        {
            get { return _checkDelegado; }
            set { _checkDelegado = value; }
        }

        public Boolean checkColaborador
        {
            get { return _checkColaborador; }
            set { _checkColaborador = value; }
        }

        public Boolean checkLider
        {
            get { return _checkLider; }
            set { _checkLider = value; }
        }

        public String profesionales
        {
            get { return _profesionales; }
            set { _profesionales = value; }
        }

        #endregion

    }
}
