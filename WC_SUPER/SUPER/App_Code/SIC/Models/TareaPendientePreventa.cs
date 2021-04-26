using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{
   
   public class TareaPendientePreventa
    {

        /// <summary>
        /// Summary description for TareaPendientePreventa
        /// </summary>
		#region Private Variables
		private Int32 _ta208_idtareapendientepreventa;
		private Byte _ta209_idconceptotareapendiente;
		private DateTime _ta208_fechaplazo;
		private Int32 _t001_idficepi_interesado;
		private Int32 _ta204_idaccionpreventa;

		#endregion

		#region Public Properties
		public Int32 ta208_idtareapendientepreventa
		{
			get{return _ta208_idtareapendientepreventa;}
			set{_ta208_idtareapendientepreventa = value;}
		}

		public Byte ta209_idconceptotareapendiente
		{
			get{return _ta209_idconceptotareapendiente;}
			set{_ta209_idconceptotareapendiente = value;}
		}

		public DateTime ta208_fechaplazo
		{
			get{return _ta208_fechaplazo;}
			set{_ta208_fechaplazo = value;}
		}

		public Int32 t001_idficepi_interesado
		{
			get{return _t001_idficepi_interesado;}
			set{_t001_idficepi_interesado = value;}
		}

		public Int32 ta204_idaccionpreventa
		{
			get{return _ta204_idaccionpreventa;}
			set{_ta204_idaccionpreventa = value;}
		}


        #endregion

	}
}
