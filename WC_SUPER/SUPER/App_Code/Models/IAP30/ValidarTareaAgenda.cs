using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ValidarTareaAgenda
    {

        /// <summary>
        /// Summary description for ValidarTareaAgenda
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private String _t332_destarea;

		#endregion

		#region Public Properties
		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}


        #endregion

	}
}
