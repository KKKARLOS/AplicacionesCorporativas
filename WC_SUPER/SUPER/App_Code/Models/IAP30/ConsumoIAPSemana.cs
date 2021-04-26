using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{

    public class ConsumoIAPSemana
    {

        /// <summary>
        /// Summary description for ConsumoIAPSemana
        /// </summary>
        #region Private Variables
        private Int32 _nivel;
        private String _tipo;
        private Int32 _t301_idproyecto;
        private String _t301_estado;
        private String _t301_denominacion;
        private Int32 _t305_idproyectosubnodo;
        private String _t305_seudonimo;
        private String _t303_denominacion;
        private String _t302_denominacion;
        private Int32 _t303_idnodo;
        private Int32 _t331_idpt;
        private Nullable<Int32> _t334_idfase;
        private Nullable<Int32> _t335_idactividad;
        private Nullable<Int32> _t332_idtarea;
        private Nullable<Int32> _t332_estado;
        private String _denominacion;
        private Double _esf_Lunes;
        private Double _esfJorn_Lunes;
        private String _com_Lunes;
        private Nullable<Int32> _lab_Lunes;
        private Nullable<Int32> _out_Lunes;
        private Nullable<Int32> _vig_Lunes;
        private Nullable<Int32> _vac_Lunes;
        private Double _esf_Martes;
        private Double _esfJorn_Martes;
        private String _com_Martes;
        private Nullable<Int32> _lab_Martes;
        private Nullable<Int32> _out_Martes;
        private Nullable<Int32> _vig_Martes;
        private Nullable<Int32> _vac_Martes;
        private Double _esf_Miercoles;
        private Double _esfJorn_Miercoles;
        private String _com_Miercoles;
        private Nullable<Int32> _lab_Miercoles;
        private Nullable<Int32> _out_Miercoles;
        private Nullable<Int32> _vig_Miercoles;
        private Nullable< Int32> _vac_Miercoles;
        private Double _esf_Jueves;
        private Double _esfJorn_Jueves;
        private String _com_Jueves;
        private Nullable<Int32> _lab_Jueves;
        private Nullable<Int32> _out_Jueves;
        private Nullable<Int32> _vig_Jueves;
        private Nullable<Int32> _vac_Jueves;
        private Double _esf_Viernes;
        private Double _esfJorn_Viernes;
        private String _com_Viernes;
        private Nullable<Int32> _lab_Viernes;
        private Nullable<Int32> _out_Viernes;
        private Nullable<Int32> _vig_Viernes;
        private Nullable<Int32> _vac_Viernes;
        private Double _esf_Sabado;
        private Double _esfJorn_Sabado;
        private String _com_Sabado;
        private Nullable<Int32> _lab_Sabado;
        private Nullable<Int32> _out_Sabado;
        private Nullable<Int32> _vig_Sabado;
        private Nullable<Int32> _vac_Sabado;
        private Double _esf_Domingo;
        private Double _esfJorn_Domingo;
        private String _com_Domingo;
        private Nullable<Int32> _lab_Domingo;
        private Nullable<Int32> _out_Domingo;
        private Nullable<Int32> _vig_Domingo;
        private Nullable<Int32> _vac_Domingo;
        private Nullable<Double> _TotalEstimado;
        private Nullable<DateTime> _FinEstimado;
        private Double _EsfuerzoTotalAcumulado;
        private Nullable<Double> _Pendiente;
        private DateTime _t330_falta;
        private Nullable<DateTime> _t330_fbaja;
        private Nullable<Int32> _t323_regjornocompleta;
        private Nullable<Int32> _t323_regfes;
        private Nullable<Int32> _t331_obligaest;
        private Nullable<Int32> _HayIndicaciones;
        private Nullable<Int32> _t336_completado;
        private Nullable<Int32> _t332_impiap;
        private DateTime _t332_fiv;
        private DateTime _t332_ffv;
        private String _t346_codpst;
        private String _t346_despst;
        private String _t332_otl;
        private String _orden;
        private String _AccesoBitacora;
        private Boolean _t305_imputablegasvi;
        private Int32 _t323_idnaturaleza;
        private String _Responsable;
        private DateTime? _fultiimp;

        #endregion

        #region Public Properties
        public Int32 nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Int32 t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        public String t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }

        public String t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }

        public Int32 t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        public String t305_seudonimo
        {
            get { return _t305_seudonimo; }
            set { _t305_seudonimo = value; }
        }

        public String t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        public String t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }

        public Int32 t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        public Int32 t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        public Nullable<Int32> t334_idfase
        {
            get { return _t334_idfase; }
            set { _t334_idfase = value; }
        }

        public Nullable<Int32> t335_idactividad
        {
            get { return _t335_idactividad; }
            set { _t335_idactividad = value; }
        }

        public Nullable<Int32> t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }

        public Nullable<Int32> t332_estado
        {
            get { return _t332_estado; }
            set { _t332_estado = value; }
        }

        public String denominacion
        {
            get { return _denominacion; }
            set { _denominacion = value; }
        }

        public Double esf_Lunes
        {
            get { return _esf_Lunes; }
            set { _esf_Lunes = value; }
        }
        public Double esfJorn_Lunes
        {
            get { return _esfJorn_Lunes; }
            set { _esfJorn_Lunes = value; }
        }

        public String com_Lunes
        {
            get { return _com_Lunes; }
            set { _com_Lunes = value; }
        }

        public Nullable<Int32> lab_Lunes
        {
            get { return _lab_Lunes; }
            set { _lab_Lunes = value; }
        }

        public Nullable<Int32> out_Lunes
        {
            get { return _out_Lunes; }
            set { _out_Lunes = value; }
        }

        public Nullable<Int32> vig_Lunes
        {
            get { return _vig_Lunes; }
            set { _vig_Lunes = value; }
        }

        public Nullable<Int32> vac_Lunes
        {
            get { return _vac_Lunes; }
            set { _vac_Lunes = value; }
        }

        public Double esf_Martes
        {
            get { return _esf_Martes; }
            set { _esf_Martes = value; }
        }
        public Double esfJorn_Martes
        {
            get { return _esfJorn_Martes; }
            set { _esfJorn_Martes = value; }
        }

        public String com_Martes
        {
            get { return _com_Martes; }
            set { _com_Martes = value; }
        }

        public Nullable<Int32> lab_Martes
        {
            get { return _lab_Martes; }
            set { _lab_Martes = value; }
        }

        public Nullable<Int32> out_Martes
        {
            get { return _out_Martes; }
            set { _out_Martes = value; }
        }

        public Nullable<Int32> vig_Martes
        {
            get { return _vig_Martes; }
            set { _vig_Martes = value; }
        }

        public Nullable<Int32> vac_Martes
        {
            get { return _vac_Martes; }
            set { _vac_Martes = value; }
        }

        public Double esf_Miercoles
        {
            get { return _esf_Miercoles; }
            set { _esf_Miercoles = value; }
        }
        public Double esfJorn_Miercoles
        {
            get { return _esfJorn_Miercoles; }
            set { _esfJorn_Miercoles = value; }
        }

        public String com_Miercoles
        {
            get { return _com_Miercoles; }
            set { _com_Miercoles = value; }
        }

        public Nullable<Int32> lab_Miercoles
        {
            get { return _lab_Miercoles; }
            set { _lab_Miercoles = value; }
        }

        public Nullable<Int32> out_Miercoles
        {
            get { return _out_Miercoles; }
            set { _out_Miercoles = value; }
        }

        public Nullable<Int32> vig_Miercoles
        {
            get { return _vig_Miercoles; }
            set { _vig_Miercoles = value; }
        }

        public Nullable<Int32> vac_Miercoles
        {
            get { return _vac_Miercoles; }
            set { _vac_Miercoles = value; }
        }

        public Double esf_Jueves
        {
            get { return _esf_Jueves; }
            set { _esf_Jueves = value; }
        }
        public Double esfJorn_Jueves
        {
            get { return _esfJorn_Jueves; }
            set { _esfJorn_Jueves = value; }
        }

        public String com_Jueves
        {
            get { return _com_Jueves; }
            set { _com_Jueves = value; }
        }

        public Nullable<Int32> lab_Jueves
        {
            get { return _lab_Jueves; }
            set { _lab_Jueves = value; }
        }

        public Nullable<Int32> out_Jueves
        {
            get { return _out_Jueves; }
            set { _out_Jueves = value; }
        }

        public Nullable<Int32> vig_Jueves
        {
            get { return _vig_Jueves; }
            set { _vig_Jueves = value; }
        }

        public Nullable<Int32> vac_Jueves
        {
            get { return _vac_Jueves; }
            set { _vac_Jueves = value; }
        }

        public Double esf_Viernes
        {
            get { return _esf_Viernes; }
            set { _esf_Viernes = value; }
        }
        public Double esfJorn_Viernes
        {
            get { return _esfJorn_Viernes; }
            set { _esfJorn_Viernes = value; }
        }

        public String com_Viernes
        {
            get { return _com_Viernes; }
            set { _com_Viernes = value; }
        }

        public Nullable<Int32> lab_Viernes
        {
            get { return _lab_Viernes; }
            set { _lab_Viernes = value; }
        }

        public Nullable<Int32> out_Viernes
        {
            get { return _out_Viernes; }
            set { _out_Viernes = value; }
        }

        public Nullable<Int32> vig_Viernes
        {
            get { return _vig_Viernes; }
            set { _vig_Viernes = value; }
        }

        public Nullable<Int32> vac_Viernes
        {
            get { return _vac_Viernes; }
            set { _vac_Viernes = value; }
        }

        public Double esf_Sabado
        {
            get { return _esf_Sabado; }
            set { _esf_Sabado = value; }
        }
        public Double esfJorn_Sabado
        {
            get { return _esfJorn_Sabado; }
            set { _esfJorn_Sabado = value; }
        }

        public String com_Sabado
        {
            get { return _com_Sabado; }
            set { _com_Sabado = value; }
        }

        public Nullable<Int32> lab_Sabado
        {
            get { return _lab_Sabado; }
            set { _lab_Sabado = value; }
        }

        public Nullable<Int32> out_Sabado
        {
            get { return _out_Sabado; }
            set { _out_Sabado = value; }
        }

        public Nullable<Int32> vig_Sabado
        {
            get { return _vig_Sabado; }
            set { _vig_Sabado = value; }
        }

        public Nullable<Int32> vac_Sabado
        {
            get { return _vac_Sabado; }
            set { _vac_Sabado = value; }
        }

        public Double esf_Domingo
        {
            get { return _esf_Domingo; }
            set { _esf_Domingo = value; }
        }
        public Double esfJorn_Domingo
        {
            get { return _esfJorn_Domingo; }
            set { _esfJorn_Domingo = value; }
        }

        public String com_Domingo
        {
            get { return _com_Domingo; }
            set { _com_Domingo = value; }
        }

        public Nullable<Int32> lab_Domingo
        {
            get { return _lab_Domingo; }
            set { _lab_Domingo = value; }
        }

        public Nullable<Int32> out_Domingo
        {
            get { return _out_Domingo; }
            set { _out_Domingo = value; }
        }

        public Nullable<Int32> vig_Domingo
        {
            get { return _vig_Domingo; }
            set { _vig_Domingo = value; }
        }

        public Nullable<Int32> vac_Domingo
        {
            get { return _vac_Domingo; }
            set { _vac_Domingo = value; }
        }

        public Nullable<Double> TotalEstimado
        {
            get { return _TotalEstimado; }
            set { _TotalEstimado = value; }
        }

        public Nullable<DateTime> FinEstimado
        {
            get { return _FinEstimado; }
            set { _FinEstimado = value; }
        }

        public Double EsfuerzoTotalAcumulado
        {
            get { return _EsfuerzoTotalAcumulado; }
            set { _EsfuerzoTotalAcumulado = value; }
        }

        public Nullable<Double> Pendiente
        {
            get { return _Pendiente; }
            set { _Pendiente = value; }
        }

        public DateTime t330_falta
        {
            get { return _t330_falta; }
            set { _t330_falta = value; }
        }

        public Nullable<DateTime> t330_fbaja
        {
            get { return _t330_fbaja; }
            set { _t330_fbaja = value; }
        }

        public Nullable<Int32> t323_regjornocompleta
        {
            get { return _t323_regjornocompleta; }
            set { _t323_regjornocompleta = value; }
        }

        public Nullable<Int32> t323_regfes
        {
            get { return _t323_regfes; }
            set { _t323_regfes = value; }
        }

        public Nullable<Int32> t331_obligaest
        {
            get { return _t331_obligaest; }
            set { _t331_obligaest = value; }
        }

        public Nullable<Int32> HayIndicaciones
        {
            get { return _HayIndicaciones; }
            set { _HayIndicaciones = value; }
        }

        public Nullable<Int32> t336_completado
        {
            get { return _t336_completado; }
            set { _t336_completado = value; }
        }

        public Nullable<Int32> t332_impiap
        {
            get { return _t332_impiap; }
            set { _t332_impiap = value; }
        }

        public DateTime t332_fiv
        {
            get { return _t332_fiv; }
            set { _t332_fiv = value; }
        }

        public DateTime t332_ffv
        {
            get { return _t332_ffv; }
            set { _t332_ffv = value; }
        }

        public String t346_codpst
        {
            get { return _t346_codpst; }
            set { _t346_codpst = value; }
        }

        public String t346_despst
        {
            get { return _t346_despst; }
            set { _t346_despst = value; }
        }

        public String t332_otl
        {
            get { return _t332_otl; }
            set { _t332_otl = value; }
        }

        public String orden
        {
            get { return _orden; }
            set { _orden = value; }
        }

        public String AccesoBitacora
        {
            get { return _AccesoBitacora; }
            set { _AccesoBitacora = value; }
        }

        public Boolean t305_imputablegasvi
        {
            get { return _t305_imputablegasvi; }
            set { _t305_imputablegasvi = value; }
        }

        public Int32 t323_idnaturaleza
        {
            get { return _t323_idnaturaleza; }
            set { _t323_idnaturaleza = value; }
        }

        public String Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        public DateTime? fultiimp
        {
            get { return _fultiimp; }
            set { _fultiimp = value; }
        }

        #endregion

    }
}
