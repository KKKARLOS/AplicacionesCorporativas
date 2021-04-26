using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{

    public class TareaCLE
    {
		/// <summary>
        /// Summary description for Tarea
        /// </summary>
		#region Private Variables
        private Int32 _idTarea;
        private Double _consumo;
        private Double _limite;
        private DateTime _fecha;
        private ArrayList _destinatariosMail;

		#endregion

		#region Public Properties

        public Int32 idtarea
        {
            get { return _idTarea; }
            set { _idTarea = value; }
        }

        public Double consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
        }

        public Double limite
        {
            get { return _limite; }
            set { _limite = value; }
        }

        public DateTime fecha
		{
			get{return _fecha;}
			set{_fecha = value;}
		}

        public ArrayList destinatariosMail
        {
            get { return _destinatariosMail; }
            set { _destinatariosMail = value; }
        }
        #endregion
	}
}