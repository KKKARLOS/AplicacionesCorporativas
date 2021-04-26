using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class FechasUsuarioPSN
    {

        /// <summary>
        /// Summary description for FechasUsuarioPSN
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private DateTime _t330_falta;
		private DateTime _t330_fbaja;

		#endregion

		#region Public Properties
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public DateTime t330_falta
		{
			get{return _t330_falta;}
			set{_t330_falta = value;}
		}

		public DateTime t330_fbaja
		{
			get{return _t330_fbaja;}
			set{_t330_fbaja = value;}
		}


        #endregion

	}
}
