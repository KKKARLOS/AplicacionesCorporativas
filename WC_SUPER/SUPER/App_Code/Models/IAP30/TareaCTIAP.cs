using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaCTIAP
    {

        /// <summary>
        /// Summary description for TareaCTIAP
        /// </summary>
		#region Private Variables
		private String _t001_codred;
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private String _t331_despt;
		private String _t335_desactividad;
		private String _t334_desfase;
		private Int32 _t301_idproyecto;
		private String _t301_denominacion;
		private String _MAIL;

		#endregion

		#region Public Properties
		public String t001_codred
		{
			get{return _t001_codred;}
			set{_t001_codred = value;}
		}

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

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public String MAIL
		{
			get{return _MAIL;}
			set{_MAIL = value;}
		}


        #endregion

	}
}
