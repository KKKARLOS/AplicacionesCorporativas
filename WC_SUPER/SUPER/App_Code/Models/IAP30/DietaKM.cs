using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.IAP30.Models
{
    /// <summary>
    /// Descripción breve de DietaKM
    /// </summary>
    public class DietaKM
    {
        #region Propiedades privadas

        private byte _t069_iddietakm;
        private string _t069_descripcion;
        private decimal _t069_icdc;
        private decimal _t069_icmd;
        private decimal _t069_icda;
        private decimal _t069_icde;
        private decimal _t069_ick;
        #endregion

        #region Propiedades públicas

        public byte t069_iddietakm
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

    }
}