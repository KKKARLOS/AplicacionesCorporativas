using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.IAP30.Models
{

    /// <summary>
    /// Descripción breve de ConsultaFacturabilidad
    /// </summary>
    public class ConsultaFacturabilidad
    {
        #region Private Variables
        private String _Proyecto;
        private String _Tarea;
        private bool _Facturable;

        private Double _t332_etpl;
        private Double _t336_etp;
        private Double _horas_planificadas_periodo;
        private Double _horas_tecnico_periodo;
        private Double _horas_otros_periodo;
        private Double _horas_total_periodo;
        private Double _horas_planificadas_finperiodo;
        private Double _horas_tecnico_finperiodo;
        private Double _horas_otros_finperiodo;
        private Double _horas_total_finperiodo;

        /*
        private Int32 _t301_idproyecto;
        private String _t305_seudonimo;
        private String _t331_despt;
        private Int32 _t332_idtarea;
        private String _t332_destarea;
        private bool _t332_facturable;
        private Int32 _t332_orden;
        private String _t335_desactividad;
        private String _t334_desfase;
        */
        private byte _t320_idtipologiaproy;
        private String _t320_denominacion;
        private Int32 _t323_idnaturaleza;
        private String _t323_denominacion;
        #endregion

        #region Public Properties
        public String Proyecto
        {
            get { return _Proyecto; }
            set { _Proyecto = value; }
        }
        public String Tarea
        {
            get { return _Tarea; }
            set { _Tarea = value; }
        }
        public bool Facturable
        {
            get { return _Facturable; }
            set { _Facturable = value; }
        }

        public Double t332_etpl
        {
            get { return _t332_etpl; }
            set { _t332_etpl = value; }
        }
        public Double t336_etp
        {
            get { return _t336_etp; }
            set { _t336_etp = value; }
        }
        public Double horas_planificadas_periodo
        {
            get { return _horas_planificadas_periodo; }
            set { _horas_planificadas_periodo = value; }
        }
        public Double horas_tecnico_periodo
        {
            get { return _horas_tecnico_periodo; }
            set { _horas_tecnico_periodo = value; }
        }
        public Double horas_otros_periodo
        {
            get { return _horas_otros_periodo; }
            set { _horas_otros_periodo = value; }
        }
        public Double horas_total_periodo
        {
            get { return _horas_total_periodo; }
            set { _horas_total_periodo = value; }
        }
        public Double horas_planificadas_finperiodo
        {
            get { return _horas_planificadas_finperiodo; }
            set { _horas_planificadas_finperiodo = value; }
        }
        public Double horas_tecnico_finperiodo
        {
            get { return _horas_tecnico_finperiodo; }
            set { _horas_tecnico_finperiodo = value; }
        }
        public Double horas_otros_finperiodo
        {
            get { return _horas_otros_finperiodo; }
            set { _horas_otros_finperiodo = value; }
        }
        public Double horas_total_finperiodo
        {
            get { return _horas_total_finperiodo; }
            set { _horas_total_finperiodo = value; }
        }

        public byte t320_idtipologiaproy
        {
            get { return _t320_idtipologiaproy; }
            set { _t320_idtipologiaproy = value; }
        }
        public String t320_denominacion
        {
            get { return _t320_denominacion; }
            set { _t320_denominacion = value; }
        }
        public Int32 t323_idnaturaleza
        {
            get { return _t323_idnaturaleza; }
            set { _t323_idnaturaleza = value; }
        }
        public String t323_denominacion
        {
            get { return _t323_denominacion; }
            set { _t323_denominacion = value; }
        }
        /*
        public Int32 t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }
        public String t305_seudonimo
        {
            get { return _t305_seudonimo; }
            set { _t305_seudonimo = value; }
        }
        public String t331_despt
        {
            get { return _t331_despt; }
            set { _t331_despt = value; }
        }
        public Int32 t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }
        public String t332_destarea
        {
            get { return _t332_destarea; }
            set { _t332_destarea = value; }
        }
        public bool t332_facturable
        {
            get { return _t332_facturable; }
            set { _t332_facturable = value; }
        }
        public Int32 t332_orden
        {
            get { return _t332_orden; }
            set { _t332_orden = value; }
        }
        public String t335_desactividad
        {
            get { return _t335_desactividad; }
            set { _t335_desactividad = value; }
        }
        public String t334_desfase
        {
            get { return _t334_desfase; }
            set { _t334_desfase = value; }
        }

        */
        #endregion
    }
}