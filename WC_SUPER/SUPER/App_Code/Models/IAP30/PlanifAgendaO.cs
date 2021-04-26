using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class PlanifAgendaO
    {

        /// <summary>
        /// Summary description for PlanifAgendaO
        /// </summary>
		#region Private Variables
		private Int32 _t458_idPlanif;
		private Int32 _t001_idficepi;
		private Int32 _t001_idficepi_mod;
		private DateTime _t458_fechamod;
		private String _t458_asunto;
		private String _t458_motivo;
		private DateTime _t458_fechoraini;
		private DateTime _t458_fechorafin;
		private Int32 _t332_idtarea;
		private String _t458_privado;
		private String _t458_observaciones;
		private String _t332_destarea;
		private String _Profesional;
		private String _Promotor;
		private String _codred_profesional;
		private String _codred_promotor;

		#endregion

		#region Public Properties
		public Int32 t458_idPlanif
		{
			get{return _t458_idPlanif;}
			set{_t458_idPlanif = value;}
		}

		public Int32 t001_idficepi
		{
			get{return _t001_idficepi;}
			set{_t001_idficepi = value;}
		}

		public Int32 t001_idficepi_mod
		{
			get{return _t001_idficepi_mod;}
			set{_t001_idficepi_mod = value;}
		}

		public DateTime t458_fechamod
		{
			get{return _t458_fechamod;}
			set{_t458_fechamod = value;}
		}

		public String t458_asunto
		{
			get{return _t458_asunto;}
			set{_t458_asunto = value;}
		}

		public String t458_motivo
		{
			get{return _t458_motivo;}
			set{_t458_motivo = value;}
		}

		public DateTime t458_fechoraini
		{
			get{return _t458_fechoraini;}
			set{_t458_fechoraini = value;}
		}

		public DateTime t458_fechorafin
		{
			get{return _t458_fechorafin;}
			set{_t458_fechorafin = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public String t458_privado
		{
			get{return _t458_privado;}
			set{_t458_privado = value;}
		}

		public String t458_observaciones
		{
			get{return _t458_observaciones;}
			set{_t458_observaciones = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public String Promotor
		{
			get{return _Promotor;}
			set{_Promotor = value;}
		}

		public String codred_profesional
		{
			get{return _codred_profesional;}
			set{_codred_profesional = value;}
		}

		public String codred_promotor
		{
			get{return _codred_promotor;}
			set{_codred_promotor = value;}
		}


        #endregion

	}
}
