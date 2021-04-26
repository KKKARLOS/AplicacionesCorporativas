using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SUPER.Capa_Negocio
{
    public class ParametrosWS
    {
        #region Propiedades y Atributos complementarios

        private string _NombreParam;
        public string NombreParam
        {
            get { return _NombreParam; }
            set { _NombreParam = value; }
        }

        private object _Valor;
        public object Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        #endregion

        public ParametrosWS()
        {
        }

        public ParametrosWS(string sNombreParam, object oValor)
        {
            _NombreParam = sNombreParam;
            _Valor = oValor;
        }


    }
}