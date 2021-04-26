using System;

namespace SUPER.Capa_Negocio
{
	/// <summary>
	/// Elemento que formará parte de un ArrayList que será la fuente de datos
	/// de los combos, de forma que la conexión se pueda cerrar y guardar la 
	/// información en la caché durante un tiempo determinado.
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
