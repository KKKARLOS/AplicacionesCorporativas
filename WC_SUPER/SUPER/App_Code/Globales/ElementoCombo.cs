using System;

namespace SUPER.Capa_Negocio
{
	/// <summary>
	/// Elemento que formar� parte de un ArrayList que ser� la fuente de datos
	/// de los combos, de forma que la conexi�n se pueda cerrar y guardar la 
	/// informaci�n en la cach� durante un tiempo determinado.
	/// </summary>
	public class ElementoCombo 
	{
		protected string _Val;
		protected string _Des;

		public ElementoCombo(string strCodigo, string strDescripcion)
		{
			strVal = strCodigo;
			strDes = strDescripcion;
		}
		public string strVal
		{
			get { return _Val; }
			set { _Val = value; }
		}
		public string strDes
		{
			get { return _Des; }
			set { _Des = value; }
		}

	}
}
