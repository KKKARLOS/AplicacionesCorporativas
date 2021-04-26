using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.IAP30.Models
{
    /// <summary>
    /// Descripción breve de OficinaGV
    /// </summary>
    public class OficinaGV
    {
        #region Private Variables
        private int _t010_idoficina;
        private string _t010_desoficina;
        #endregion
        #region Public Properties
        public int t010_idoficina
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
    }
}