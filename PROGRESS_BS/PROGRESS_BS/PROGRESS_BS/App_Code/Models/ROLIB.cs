using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
   
   public class ROLIB
    {

        /// <summary>
        /// Summary description for ROLIB
        /// </summary>
		#region Private Variables
		private short _t004_idrol;
		private string _t004_desrol;
		private bool _t004_aprobador;
        private string _profesional;
        private string _rolNuevo;
        private DateTime _fechaCambio;
        private int _numeroprofesionales;
        private int _t001_idficepi;
        
		#endregion

		#region Public Properties
		public short t004_idrol
		{
			get{return _t004_idrol;}
			set{_t004_idrol = value;}
		}

		public string t004_desrol
		{
			get{return _t004_desrol;}
			set{_t004_desrol = value;}
		}
       
		public bool t004_aprobador
		{
			get{return _t004_aprobador;}
			set{_t004_aprobador = value;}
		}

        public string Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public string RolNuevo
        {
            get { return _rolNuevo; }
            set { _rolNuevo = value; }
        }

        public DateTime FechaCambio
        {
            get { return _fechaCambio; }
            set { _fechaCambio = value; }
        }

        public int Numeroprofesionales
        {
            get { return _numeroprofesionales; }
            set { _numeroprofesionales = value; }
        }

        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

       
      

        #endregion

	}
}
