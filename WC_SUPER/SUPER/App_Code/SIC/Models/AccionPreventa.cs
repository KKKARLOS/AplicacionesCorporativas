using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class AccionPreventa
    {

        /// <summary>
        /// Summary description for AccionPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta204_idaccionpreventa;
        private string _ta204_idaccionpreventa_encrypt;
        private DateTime _ta204_fechacreacion;
        private DateTime _ta204_fechafinestipulada;
        private DateTime? _ta204_fechafinreal;
        private String _ta204_estado;
        private String _ta204_descripcion;
        private String _ta204_observaciones;
        private String _ta204_motivoanulacion;
        private Int32 _ta201_idsubareapreventa;
        private Int32? _t001_idficepi_lider;
        private Int32 _t001_idficepi_promotor;
        private Int16 _ta205_idtipoaccionpreventa;
        private String _ta205_denominacion;
        private Int32 _ta206_idsolicitudpreventa;
        private string _ta206_denominacion;
        private Int32? _t331_idpt;
        private String _ta206_idsolicitudpreventa_encrypt;
        private string _tipoAccion;
        private Int32 _ta199_idunidadpreventa;
        private string _unidadPreventa;
        private Int32 _ta200_idareapreventa;
        private string _areaPreventa;
        private string _subareaPreventa;
        private string _lider;
        private string _promotor;
        private int _numaccionesbylider;
        private int _tareasCount;
        private int _ta206_iditemorigen;        
        private string _ta206_itemorigen;
        
        public Boolean btnAddTarea { get; set; }

        public double importe { get; set; }
        public string den_unidadcomercial { get; set; }
        public string moneda { get; set; }
        public string ta206_estado { get; set; }
        public bool linkaccesoaccion { get; set; }
        public short ta205_plazominreq { get; set; }

















        #endregion

        #region Public Properties
        public Int32 ta204_idaccionpreventa
        {
            get { return _ta204_idaccionpreventa; }
            set
            {
                _ta204_idaccionpreventa = value;
                ta204_idaccionpreventa_encrypt = Shared.Crypt.Encrypt(value.ToString()); 
            }
        }
        public string ta204_idaccionpreventa_encrypt
        {
            get { return _ta204_idaccionpreventa_encrypt; }
            set { _ta204_idaccionpreventa_encrypt = value; }
        }

        public DateTime ta204_fechacreacion
        {
            get { return _ta204_fechacreacion; }
            set { _ta204_fechacreacion = value; }
        }
        public DateTime ta204_fechafinestipulada
        {
            get { return _ta204_fechafinestipulada; }
            set { _ta204_fechafinestipulada = value; }
        }

        public DateTime? ta204_fechafinreal
        {
            get { return _ta204_fechafinreal; }
            set { _ta204_fechafinreal = value; }
        }

        public String ta204_estado
        {
            get { return _ta204_estado; }
            set { _ta204_estado = value; }
        }

        public String ta204_descripcion
        {
            get { return _ta204_descripcion; }
            set { _ta204_descripcion = value; }
        }

        public String ta204_observaciones
        {
            get { return _ta204_observaciones; }
            set { _ta204_observaciones = value; }
        }

        public String ta204_motivoanulacion
        {
            get { return _ta204_motivoanulacion; }
            set { _ta204_motivoanulacion = value; }
        }

        public Int32 ta201_idsubareapreventa
        {
            get { return _ta201_idsubareapreventa; }
            set { _ta201_idsubareapreventa = value; }
        }

        public Int32? t001_idficepi_lider
        {
            get { return _t001_idficepi_lider; }
            set { _t001_idficepi_lider = value; }
        }

        public Int32 t001_idficepi_promotor
        {
            get { return _t001_idficepi_promotor; }
            set { _t001_idficepi_promotor = value; }
        }

        public Int16 ta205_idtipoaccionpreventa
        {
            get { return _ta205_idtipoaccionpreventa; }
            set { _ta205_idtipoaccionpreventa = value; }
        }

        public string ta205_denominacion
        {
            get { return _ta205_denominacion; }
            set { _ta205_denominacion = value; }
        }

        public Int32 ta206_idsolicitudpreventa
        {
            get { return _ta206_idsolicitudpreventa; }
            set { 
                _ta206_idsolicitudpreventa = value;
                _ta206_idsolicitudpreventa_encrypt = Shared.Crypt.Encrypt(value.ToString());
            }
        }


        public string ta206_denominacion {
            get { return _ta206_denominacion; }
            set { _ta206_denominacion = value; }
        }

        public Int32? t331_idpt
        {
            get { return _t331_idpt; }
            set { _t331_idpt = value; }
        }

        public string ta206_idsolicitudpreventa_encrypt
        {
            get { return _ta206_idsolicitudpreventa_encrypt;  }
            set { _ta206_idsolicitudpreventa_encrypt = value;  }
        }


        public string tipoAccion
        {
            get { return _tipoAccion; }
            set { _tipoAccion= value;}
        }
        public Int32 ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }
        public string unidadPreventa
        {
            get { return _unidadPreventa; }
            set { _unidadPreventa = value;}
        }

        public Int32 ta200_idareapreventa
        {
            get { return _ta200_idareapreventa; }
            set { _ta200_idareapreventa = value; }
        }

        public string areaPreventa
        {
            get { return _areaPreventa; }
            set { _areaPreventa = value;}
        }
        public string subareaPreventa
        {
            get { return _subareaPreventa; }
            set { _subareaPreventa = value;}
        }
        public string lider
        {
            get { return _lider; }
            set { _lider = value; }
        }

        public string promotor
        {
            get { return _promotor; }
            set { _promotor = value; }
        }


        public Int32 numaccionesbylider
        {
            get { return _numaccionesbylider; }
            set { _numaccionesbylider = value; }            
        }

        public Int32 tareasCount
        {
            get { return _tareasCount; }
            set { _tareasCount = value; }            
        }

        public int ta206_iditemorigen
        {
            get { return _ta206_iditemorigen; }
            set { _ta206_iditemorigen = value; }
        }

        public string ta206_itemorigen
        {
            get { return _ta206_itemorigen; }
            set { _ta206_itemorigen = value; }
        }

        //Properties modal imputaciones de la pantalla catálogo CRM
        public decimal jornadas { get; set; }
        public decimal euros { get; set; }

        #endregion

    }
}
