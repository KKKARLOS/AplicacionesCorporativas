using System;

namespace GEMO.BLL
{
	public class ElementoLista 
	{
		protected int _Cod;
		protected string _Des;

        public ElementoLista(string strCodigo, string strDescripcion)
		{
			nCodigo = int.Parse(strCodigo);
			sDescripcion = strDescripcion;
		}
        public int nCodigo
		{
            get { return _Cod; }
            set { _Cod = value; }
		}
        public string sDescripcion
		{
			get { return _Des; }
			set { _Des = value; }
		}

	}
}
