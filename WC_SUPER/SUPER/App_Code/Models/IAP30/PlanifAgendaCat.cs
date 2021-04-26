using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class PlanifAgendaCat
    {

        /// <summary>
        /// Summary description for PlanifAgendaCat
        /// </summary>
		#region Private Variables
        private Int32 _Idficepi;
        private Int32 _IdficepiMod;
		private Int32 _ID;
		private String _Motivo;
        private String _MotivoEliminacion;
        private String _Asunto;
        private String _Privado;
        private String _Observaciones;
        private Nullable <Int32> _IdTarea;
        private String _DesTarea;
        private String _Profesional;
        private String _Promotor;
        private String _CodRedProfesional;
        private String _CodRedPromotor; 		
		private DateTime _StartTime;
		private DateTime _EndTime;
        private DateTime _FechaMod;
        private bool[] _DiasSemana;
		#endregion

		#region Public Properties
        public Int32 Idficepi
        {
            get { return _Idficepi; }
            set { _Idficepi = value; }
        }

        public Int32 IdficepiMod
        {
            get { return _IdficepiMod; }
            set { _IdficepiMod = value; }
        }

		public Int32 ID
		{
			get{return _ID;}
			set{_ID = value;}
		}

		public String Motivo
		{
			get{return _Motivo;}
			set{_Motivo = value;}
		}

        public String MotivoEliminacion
        {
            get { return _MotivoEliminacion; }
            set { _MotivoEliminacion = value; }
        }

        public String Asunto
        {
            get { return _Asunto; }
            set { _Asunto = value; }
        }

        public String Privado
        {
            get { return _Privado; }
            set { _Privado = value; }
        }

        public String Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        public Nullable <Int32> IdTarea
        {
            get { return _IdTarea; }
            set { _IdTarea = value; }
        }

        public String DesTarea
        {
            get { return _DesTarea; }
            set { _DesTarea = value; }
        }

        public String Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }

        public String Promotor
        {
            get { return _Promotor; }
            set { _Promotor = value; }
        }

        public String CodRedProfesional
        {
            get { return _CodRedProfesional; }
            set { _CodRedProfesional = value; }
        }
        public String CodRedPromotor
        {
            get { return _CodRedPromotor; }
            set { _CodRedPromotor = value; }
        }


		public DateTime StartTime
		{
			get{return _StartTime;}
			set{_StartTime = value;}
		}

		public DateTime EndTime
		{
			get{return _EndTime;}
			set{_EndTime = value;}
		}

        public DateTime FechaMod
        {
            get { return _FechaMod; }
            set { _FechaMod = value; }
        }

        public bool[] DiasSemana
        {
            get { return _DiasSemana; }
            set { _DiasSemana = value; }
        }
        #endregion

	}
}
