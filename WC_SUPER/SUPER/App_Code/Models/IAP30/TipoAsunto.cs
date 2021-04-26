using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TipoAsunto
    {

        /// <summary>
        /// Summary description for TipoAsunto
        /// </summary>
		#region Private Variables
		private String _T384_destipo;
		private Int32 _T384_idtipo;
		private Byte _T384_orden;

		#endregion

		#region Public Properties
		public String T384_destipo
		{
			get{return _T384_destipo;}
			set{_T384_destipo = value;}
		}

		public Int32 T384_idtipo
		{
			get{return _T384_idtipo;}
			set{_T384_idtipo = value;}
		}

		public Byte T384_orden
		{
			get{return _T384_orden;}
			set{_T384_orden = value;}
		}


        #endregion

	}
}
