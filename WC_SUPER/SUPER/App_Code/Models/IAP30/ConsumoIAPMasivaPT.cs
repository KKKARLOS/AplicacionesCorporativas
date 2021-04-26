using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ConsumoIAPMasivaPT
    {

        /// <summary>
        /// Summary description for ConsumoIAPMasivaPT
        /// </summary>
		#region Private Variables
		private Int32 _nivel;
		private Int32 _t331_idpt;
		private String _t331_despt;
		private Int32 _t331_orden;

		#endregion

		#region Public Properties
		public Int32 nivel
		{
			get{return _nivel;}
			set{_nivel = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public Int32 t331_orden
		{
			get{return _t331_orden;}
			set{_t331_orden = value;}
		}


        #endregion

	}
}
