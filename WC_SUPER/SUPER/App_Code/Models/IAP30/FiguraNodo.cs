using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class FiguraNodo
    {

        /// <summary>
        /// Summary description for FiguraNodo
        /// </summary>
		#region Private Variables
		private Int32? _t303_idnodo;
		private Int32? _t314_idusuario;
		private String _t308_figura;

		#endregion

		#region Public Properties
		public Int32? t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public Int32? t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String t308_figura
		{
			get{return _t308_figura;}
			set{_t308_figura = value;}
		}


        #endregion

	}
}
