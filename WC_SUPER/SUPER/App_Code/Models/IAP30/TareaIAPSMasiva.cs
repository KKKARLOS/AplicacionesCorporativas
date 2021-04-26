using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción completa de TareaIAPSMasiva
/// </summary>
namespace IB.SUPER.IAP30.Models
{
    public class TareaIAPSMasiva
    {

        /// <summary>
        /// Summary description for TareaIAPMasiva
        /// </summary>
        #region Private Variables
        private Int32 _t305_idproyectosubnodo;
        private Int32 _t331_idpt;
        private String _denominacion;
        private Int32 _t323_regfes;
        private Int32 _t323_regjornocompleta;
        private Int32 _t331_obligaest;
        private Int32 _t334_idfase;
        private Int32 _t335_idactividad;
        private Int32 _t332_idtarea;
        private Byte _t332_estado;
        private Boolean _t332_impiap;
        private String _t301_estado;

        private String _t332_destarea;
        private Int32 _t301_idproyecto;
        private String _t305_seudonimo;
        private String _t301_denominacion;
        private String _t331_despt;
        private String _t332_destarealong;
        private String _t332_notas1;
        private String _t332_notas2;
        private String _t332_notas3;
        private String _t332_notas4;
        private String _t332_mensaje;
        private Double _t336_etp;
        private DateTime? _t336_ffp;
        private Double _t336_ete;
        private DateTime? _t336_ffe;
        private Byte _t336_completado;
        private String _t334_desfase;
        private String _t335_desactividad;
        private String _t336_indicaciones;
        private String _t336_comentario;
        private DateTime? _dPrimerConsumo;
        private DateTime? _dUltimoConsumo;
        private Double _esfuerzo;
        private Double _esfuerzoenjor;
        private Double _nPendienteEstimado;
        private Double _nAvanceTeorico;
        private Boolean _t332_notasiap;
        private Int32 _t324_idmodofact;
        private String _t324_denominacion;
        private Boolean _t336_estado;
        private DateTime? _fechaInicioImpPermitida;
        private DateTime? _fechaFinImpPermitida;

        #endregion

        #region Public Properties
        public Int32 t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }       

        public String denominacion
        {
            get { return _denominacion; }
            set { _denominacion = value; }
        }

        public Int32 t323_regfes
        {
            get { return _t323_regfes; }
            set { _t323_regfes = value; }
        }

        public Int32 t323_regjornocompleta
        {
            get { return _t323_regjornocompleta; }
            set { _t323_regjornocompleta = value; }
        }

        public Int32 t331_obligaest
        {
            get { return _t331_obligaest; }
            set { _t331_obligaest = value; }
        }

        public Int32 t334_idfase
        {
            get { return _t334_idfase; }
            set { _t334_idfase = value; }
        }
        
        public Int32 t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }

        public Byte t332_estado
        {
            get { return _t332_estado; }
            set { _t332_estado = value; }
        }

        public String t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }

        public DateTime? dPrimerConsumo
        {
            get { return _dPrimerConsumo; }
            set { _dPrimerConsumo = value; }
        }

        public DateTime? dUltimoConsumo
        {
            get { return _dUltimoConsumo; }
            set { _dUltimoConsumo = value; }
        }        

        public Double nPendienteEstimado
        {
            get { return _nPendienteEstimado; }
            set { _nPendienteEstimado = value; }
        }

        public String t332_destarea
        {
            get { return _t332_destarea; }
            set { _t332_destarea = value; }
        }

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

        public String t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }

        public String t331_despt
        {
            get { return _t331_despt; }
            set { _t331_despt = value; }
        }

        public String t332_destarealong
        {
            get { return _t332_destarealong; }
            set { _t332_destarealong = value; }
        }

        public String t332_notas1
        {
            get { return _t332_notas1; }
            set { _t332_notas1 = value; }
        }

        public String t332_notas2
        {
            get { return _t332_notas2; }
            set { _t332_notas2 = value; }
        }

        public String t332_notas3
        {
            get { return _t332_notas3; }
            set { _t332_notas3 = value; }
        }

        public String t332_notas4
        {
            get { return _t332_notas4; }
            set { _t332_notas4 = value; }
        }

        public String t332_mensaje
        {
            get { return _t332_mensaje; }
            set { _t332_mensaje = value; }
        }

        public Double t336_etp
        {
            get { return _t336_etp; }
            set { _t336_etp = value; }
        }

        public DateTime? t336_ffp
        {
            get { return _t336_ffp; }
            set { _t336_ffp = value; }
        }

        public Double t336_ete
        {
            get { return _t336_ete; }
            set { _t336_ete = value; }
        }

        public DateTime? t336_ffe
        {
            get { return _t336_ffe; }
            set { _t336_ffe = value; }
        }

        public Byte t336_completado
        {
            get { return _t336_completado; }
            set { _t336_completado = value; }
        }

        public String t334_desfase
        {
            get { return _t334_desfase; }
            set { _t334_desfase = value; }
        }

        public String t335_desactividad
        {
            get { return _t335_desactividad; }
            set { _t335_desactividad = value; }
        }

        public String t336_indicaciones
        {
            get { return _t336_indicaciones; }
            set { _t336_indicaciones = value; }
        }

        public String t336_comentario
        {
            get { return _t336_comentario; }
            set { _t336_comentario = value; }
        }    

        public Double esfuerzo
        {
            get { return _esfuerzo; }
            set { _esfuerzo = value; }
        }

        public Double esfuerzoenjor
        {
            get { return _esfuerzoenjor; }
            set { _esfuerzoenjor = value; }
        }

        public Double nAvanceTeorico
        {
            get { return _nAvanceTeorico; }
            set { _nAvanceTeorico = value; }
        }      

        public Boolean t332_notasiap
        {
            get { return _t332_notasiap; }
            set { _t332_notasiap = value; }
        }

        public Int32 t324_idmodofact
        {
            get { return _t324_idmodofact; }
            set { _t324_idmodofact = value; }
        }

        public String t324_denominacion
        {
            get { return _t324_denominacion; }
            set { _t324_denominacion = value; }
        }

        public Boolean t336_estado
        {
            get { return _t336_estado; }
            set { _t336_estado = value; }
        }

        public DateTime? fechaInicioImpPermitida
        {
            get { return _fechaInicioImpPermitida; }
            set { _fechaInicioImpPermitida = value; }
        }
        public DateTime? fechaFinImpPermitida
        {
            get { return _fechaFinImpPermitida; }
            set { _fechaFinImpPermitida = value; }
        }


        #endregion

    }
}