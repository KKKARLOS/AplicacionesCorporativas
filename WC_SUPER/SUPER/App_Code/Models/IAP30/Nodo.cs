using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{

    public class Nodo
    {

        /// <summary>
        /// Summary description for Nodo
        /// </summary>
        #region Private Variables
        private Int32 _t303_idnodo;
        private String _t303_denominacion;
        private Boolean _t303_cierreIAPestandar;
        private Int32 _t303_ultcierreIAP;
        private Boolean _t303_cierreECOestandar;
        private Int32 _t303_ultcierreECO;
        private String _t303_denominacion_CNP;
        private Int32 _t303_obligatorio_CNP;
        private String _t391_denominacion_CSN1P;
        private Int32 _t391_obligatorio_CSN1P;
        private String _t392_denominacion_CSN2P;
        private Int32 _t392_obligatorio_CSN2P;
        private String _t393_denominacion_CSN3P;
        private Int32 _t393_obligatorio_CSN3P;
        private String _t394_denominacion_CSN4P;
        private Int32 _t394_obligatorio_CSN4P;
        private Single _t303_margencesionprof;
        private String _t422_idmoneda;
        private String _t422_denominacion;

        #endregion

        #region Public Properties
        public Int32 t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        public String t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        public Boolean t303_cierreIAPestandar
        {
            get { return _t303_cierreIAPestandar; }
            set { _t303_cierreIAPestandar = value; }
        }

        public Int32 t303_ultcierreIAP
        {
            get { return _t303_ultcierreIAP; }
            set { _t303_ultcierreIAP = value; }
        }

        public Boolean t303_cierreECOestandar
        {
            get { return _t303_cierreECOestandar; }
            set { _t303_cierreECOestandar = value; }
        }

        public Int32 t303_ultcierreECO
        {
            get { return _t303_ultcierreECO; }
            set { _t303_ultcierreECO = value; }
        }

        public String t303_denominacion_CNP
        {
            get { return _t303_denominacion_CNP; }
            set { _t303_denominacion_CNP = value; }
        }

        public Int32 t303_obligatorio_CNP
        {
            get { return _t303_obligatorio_CNP; }
            set { _t303_obligatorio_CNP = value; }
        }

        public String t391_denominacion_CSN1P
        {
            get { return _t391_denominacion_CSN1P; }
            set { _t391_denominacion_CSN1P = value; }
        }

        public Int32 t391_obligatorio_CSN1P
        {
            get { return _t391_obligatorio_CSN1P; }
            set { _t391_obligatorio_CSN1P = value; }
        }

        public String t392_denominacion_CSN2P
        {
            get { return _t392_denominacion_CSN2P; }
            set { _t392_denominacion_CSN2P = value; }
        }

        public Int32 t392_obligatorio_CSN2P
        {
            get { return _t392_obligatorio_CSN2P; }
            set { _t392_obligatorio_CSN2P = value; }
        }

        public String t393_denominacion_CSN3P
        {
            get { return _t393_denominacion_CSN3P; }
            set { _t393_denominacion_CSN3P = value; }
        }

        public Int32 t393_obligatorio_CSN3P
        {
            get { return _t393_obligatorio_CSN3P; }
            set { _t393_obligatorio_CSN3P = value; }
        }

        public String t394_denominacion_CSN4P
        {
            get { return _t394_denominacion_CSN4P; }
            set { _t394_denominacion_CSN4P = value; }
        }

        public Int32 t394_obligatorio_CSN4P
        {
            get { return _t394_obligatorio_CSN4P; }
            set { _t394_obligatorio_CSN4P = value; }
        }

        public Single t303_margencesionprof
        {
            get { return _t303_margencesionprof; }
            set { _t303_margencesionprof = value; }
        }

        public String t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        public String t422_denominacion
        {
            get { return _t422_denominacion; }
            set { _t422_denominacion = value; }
        }

        public string t303_modelocostes { get; set; }
        public string t303_modelotarifas { get; set; }
        public bool t303_desglose { get; set; }
        public int t314_idusuario_responsable { get; set; }
        #endregion

    }
}
