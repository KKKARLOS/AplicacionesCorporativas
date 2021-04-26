using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AnnoGasto
    {

        /// <summary>
        /// Summary description for AnnoGasto
        /// </summary>
		#region Private Variables
		private Int16 _t419_anno;
		private DateTime _t419_fdesde;
		private DateTime _t419_fhasta;
		private Int32 _t001_idficepi;
		private DateTime _t419_fmodif;

		#endregion

		#region Public Properties
		public Int16 t419_anno
		{
			get{return _t419_anno;}
			set{_t419_anno = value;}
		}

		public DateTime t419_fdesde
		{
			get{return _t419_fdesde;}
			set{_t419_fdesde = value;}
		}

		public DateTime t419_fhasta
		{
			get{return _t419_fhasta;}
			set{_t419_fhasta = value;}
		}

		public Int32 t001_idficepi
		{
			get{return _t001_idficepi;}
			set{_t001_idficepi = value;}
		}

		public DateTime t419_fmodif
		{
			get{return _t419_fmodif;}
			set{_t419_fmodif = value;}
		}


        #endregion

	}
}
