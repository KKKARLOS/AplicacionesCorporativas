using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.IAP30.Models
{
    /// <summary>
    /// Descripción breve de UsuarioGV
    /// </summary>
    public class UsuarioGV
    {
        #region Private Variables
        private int _t314_idusuario;
        private string _Nombre;
        private string _t422_idmoneda;
        private int _t313_idempresa;
        private string _t313_denominacion;
        private int _t303_idnodo;
        private string _t303_denominacion;
        private int? _t010_idoficina_base;
        private bool _bAutorresponsable;
        #region Territorio
        private byte _T007_IDTERRFIS;
        private string _T007_NOMTERRFIS;
        private decimal _T007_ITERDC;
        private decimal _T007_ITERMD;
        private decimal _T007_ITERDA;
        private decimal _T007_ITERDE;
        private decimal _T007_ITERK;
        private string _T007_CODSAP;
        private decimal _T007_ITEATDC;
        private decimal _T007_ITEATMD;
        private decimal _T007_ITEATDA;
        private decimal _T007_ITEATDE;
        private decimal _T007_ITEATKM;
        #endregion
        #region Oficina
        private short? _t010_idoficina;
        private string _t010_desoficina;
        #endregion
        #region Dieta
        private byte? _t069_iddietakm;
        private string _t069_descripcion;
        private decimal _t069_icdc;
        private decimal _t069_icmd;
        private decimal _t069_icda;
        private decimal _t069_icde;
        private decimal _t069_ick;
        #endregion
        #endregion

        #region Public Properties
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }
        public int t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }
        public int? t010_idoficina_base
        {
            get { return _t010_idoficina_base; }
            set { _t010_idoficina_base = value; }
        }
        public bool bAutorresponsable
        {
            get { return _bAutorresponsable; }
            set { _bAutorresponsable = value; }
        }
        #region Territorio
        public byte T007_IDTERRFIS
        {
            get { return _T007_IDTERRFIS; }
            set { _T007_IDTERRFIS = value; }
        }
        public string T007_NOMTERRFIS
        {
            get { return _T007_NOMTERRFIS; }
            set { _T007_NOMTERRFIS = value; }
        }
        public decimal T007_ITERDC
        {
            get { return _T007_ITERDC; }
            set { _T007_ITERDC = value; }
        }
        public decimal T007_ITERMD
        {
            get { return _T007_ITERMD; }
            set { _T007_ITERMD = value; }
        }
        public decimal T007_ITERDA
        {
            get { return _T007_ITERDA; }
            set { _T007_ITERDA = value; }
        }
        public decimal T007_ITERDE
        {
            get { return _T007_ITERDE; }
            set { _T007_ITERDE = value; }
        }
        public decimal T007_ITERK
        {
            get { return _T007_ITERK; }
            set { _T007_ITERK = value; }
        }
        public string T007_CODSAP
        {
            get { return _T007_CODSAP; }
            set { _T007_CODSAP = value; }
        }
        public decimal T007_ITEATDC
        {
            get { return _T007_ITEATDC; }
            set { _T007_ITEATDC = value; }
        }
        public decimal T007_ITEATMD
        {
            get { return _T007_ITEATMD; }
            set { _T007_ITEATMD = value; }
        }
        public decimal T007_ITEATDA
        {
            get { return _T007_ITEATDA; }
            set { _T007_ITEATDA = value; }
        }
        public decimal T007_ITEATDE
        {
            get { return _T007_ITEATDE; }
            set { _T007_ITEATDE = value; }
        }
        public decimal T007_ITEATKM
        {
            get { return _T007_ITEATKM; }
            set { _T007_ITEATKM = value; }
        }
        #endregion
        #region Oficina
        public short? t010_idoficina
        {
            get { return _t010_idoficina; }
            set { _t010_idoficina = value; }
        }
        public string t010_desoficina
        {
            get { return _t010_desoficina; }
            set { _t010_desoficina = value; }
        }
        #endregion
        #region Dieta
        public byte? t069_iddietakm
        {
            get { return _t069_iddietakm; }
            set { _t069_iddietakm = value; }
        }
        public string t069_descripcion
        {
            get { return _t069_descripcion; }
            set { _t069_descripcion = value; }
        }
        public decimal t069_icdc
        {
            get { return _t069_icdc; }
            set { _t069_icdc = value; }
        }
        public decimal t069_icmd
        {
            get { return _t069_icmd; }
            set { _t069_icmd = value; }
        }
        public decimal t069_icda
        {
            get { return _t069_icda; }
            set { _t069_icda = value; }
        }
        public decimal t069_icde
        {
            get { return _t069_icde; }
            set { _t069_icde = value; }
        }
        public decimal t069_ick
        {
            get { return _t069_ick; }
            set { _t069_ick = value; }
        }
        #endregion
        #endregion
    }
}