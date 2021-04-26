using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.Progress.Models
{
    public class Colectivo
    {
        #region Private Variables
        private short _t941_idcolectivo;
        private string _t941_denominacion;
        private int _t934_idmodeloformulario;
        private int _t001_idficepi;
        private string _profesional;

       

        #endregion

        #region Public Properties
        public short t941_idcolectivo
        {
            get { return _t941_idcolectivo; }
            set { _t941_idcolectivo = value; }
        }

        public string t941_denominacion
        {
            get { return _t941_denominacion; }
            set { _t941_denominacion = value; }
        }

        public int t934_idmodeloformulario
        {
            get { return _t934_idmodeloformulario; }
            set { _t934_idmodeloformulario = value; }
        }

        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public string profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }
        #endregion
    }
}
