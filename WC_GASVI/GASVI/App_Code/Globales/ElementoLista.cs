using System;
using System.Configuration;

namespace GASVI.BLL
{
	public class ElementoLista 
	{
		protected string _Val;
        protected string _Des;
        protected string _DatoAux1;
        protected string _DatoAux2;
        protected string _DatoAux3;
        protected string _DatoAux4;
        protected string _DatoAux5;
        protected string _DatoAux6;

        public ElementoLista(string strCodigo, string strDescripcion)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1, string sDatoAuxiliar2)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
            sDatoAux2 = sDatoAuxiliar2;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1, string sDatoAuxiliar2, string sDatoAuxiliar3)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
            sDatoAux2 = sDatoAuxiliar2;
            sDatoAux3 = sDatoAuxiliar3;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1, string sDatoAuxiliar2, string sDatoAuxiliar3, string sDatoAuxiliar4)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
            sDatoAux2 = sDatoAuxiliar2;
            sDatoAux3 = sDatoAuxiliar3;
            sDatoAux4 = sDatoAuxiliar4;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1, string sDatoAuxiliar2, string sDatoAuxiliar3, string sDatoAuxiliar4, string sDatoAuxiliar5)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
            sDatoAux2 = sDatoAuxiliar2;
            sDatoAux3 = sDatoAuxiliar3;
            sDatoAux4 = sDatoAuxiliar4;
            sDatoAux5 = sDatoAuxiliar5;
        }
        public ElementoLista(string strCodigo, string strDescripcion, string sDatoAuxiliar1, string sDatoAuxiliar2, string sDatoAuxiliar3, string sDatoAuxiliar4, string sDatoAuxiliar5, string sDatoAuxiliar6)
        {
            sValor = strCodigo;
            sDenominacion = strDescripcion;
            sDatoAux1 = sDatoAuxiliar1;
            sDatoAux2 = sDatoAuxiliar2;
            sDatoAux3 = sDatoAuxiliar3;
            sDatoAux4 = sDatoAuxiliar4;
            sDatoAux5 = sDatoAuxiliar5;
            sDatoAux6 = sDatoAuxiliar6;
        }
        
        
        
        public string sValor
		{
			get { return _Val; }
			set { _Val = value; }
		}
		public string sDenominacion
		{
			get { return _Des; }
			set { _Des = value; }
		}
        public string sDatoAux1
        {
            get { return _DatoAux1; }
            set { _DatoAux1 = value; }
        }
        public string sDatoAux2
        {
            get { return _DatoAux2; }
            set { _DatoAux2 = value; }
        }
        public string sDatoAux3
        {
            get { return _DatoAux3; }
            set { _DatoAux3 = value; }
        }
        public string sDatoAux4
        {
            get { return _DatoAux4; }
            set { _DatoAux4 = value; }
        }
        public string sDatoAux5
        {
            get { return _DatoAux5; }
            set { _DatoAux5 = value; }
        }
        public string sDatoAux6
        {
            get { return _DatoAux6; }
            set { _DatoAux6 = value; }
        }
    }
}
