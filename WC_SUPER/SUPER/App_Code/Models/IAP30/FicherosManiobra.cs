using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class FicherosManiobra
    {

        /// <summary>
        /// Summary description for FicherosManiobra
        /// </summary>
		#region Private Variables
		private Byte _t722_idtipo;
		private Int32 _t001_idficepi;
		private Byte[] _t722_fichero;

		#endregion

		#region Public Properties
		public Byte t722_idtipo
		{
			get{return _t722_idtipo;}
			set{_t722_idtipo = value;}
		}

		public Int32 t001_idficepi
		{
			get{return _t001_idficepi;}
			set{_t001_idficepi = value;}
		}

		public Byte[] t722_fichero
		{
			get{return _t722_fichero;}
			set{_t722_fichero = value;}
		}


        #endregion

	}
}
