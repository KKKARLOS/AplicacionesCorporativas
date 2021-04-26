using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class NodoPSN
    {

        /// <summary>
        /// Summary description for NodoPSN
        /// </summary>
		#region Private Variables
		private Int32 _t303_idnodo;
        private String _t301_estado;

		#endregion

		#region Public Properties
		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

        public String t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }

        #endregion

	}
}
