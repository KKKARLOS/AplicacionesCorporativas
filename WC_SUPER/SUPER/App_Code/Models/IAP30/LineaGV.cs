using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.IAP30.Models
{
    public class LineaGV
    {
        #region Private Variables
        private Int32? _idCabecera;
        private Int32? _idLinea;
        private DateTime _desde;
        private DateTime _hasta;
        private String _destino;
        private String _anotacion;
        private Byte _dietaCompleta;
        private Byte _mediaDieta;
        private Byte _dietaEspecial;
        private Byte _dietaAlojamiento;
        private Int16 _numKm;
        private Int32? _idECO;
        private decimal _peaje;
        private decimal _comida;
        private decimal _transporte;
        private decimal _hotel;
        #endregion

        #region Public Properties
        public Int32? idCabecera
        {
            get { return _idCabecera; }
            set { _idCabecera = value; }
        }
        public Int32? idLinea
        {
            get { return _idLinea; }
            set { _idLinea = value; }
        }
        public DateTime desde
        {
            get { return _desde; }
            set { _desde = value; }
        }
        public DateTime hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }
        public String destino
        {
            get { return _destino; }
            set { _destino = value; }
        }
        public String anotacion
        {
            get { return _anotacion; }
            set { _anotacion = value; }
        }
        public Byte dietaCompleta
        {
            get { return _dietaCompleta; }
            set { _dietaCompleta = value; }
        }
        public Byte mediaDieta
        {
            get { return _mediaDieta; }
            set { _mediaDieta = value; }
        }
        public Byte dietaEspecial
        {
            get { return _dietaEspecial; }
            set { _dietaEspecial = value; }
        }
        public Byte dietaAlojamiento
        {
            get { return _dietaAlojamiento; }
            set { _dietaAlojamiento = value; }
        }
        public Int16 numKm
        {
            get { return _numKm; }
            set { _numKm = value; }
        }
        public Int32? idECO
        {
            get { return _idECO; }
            set { _idECO = value; }
        }
        public decimal peaje
        {
            get { return _peaje; }
            set { _peaje = value; }
        }
        public decimal comida
        {
            get { return _comida; }
            set { _comida = value; }
        }
        public decimal transporte
        {
            get { return _transporte; }
            set { _transporte = value; }
        }
        public decimal hotel
        {
            get { return _hotel; }
            set { _hotel = value; }
        }
        #endregion
    }
}