using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoEstado
    {

        /// <summary>
        /// Summary description for AsuntoEstado
        /// </summary>
		#region Private Variables
		private Int32 _T382_idasunto;
		private Byte _T385_codestado;
		private String _Estado;
		private DateTime _T385_fecha;
		private Int32 _T385_idautor;
		private String _nomRecurso;

		#endregion

		#region Public Properties
		public Int32 T382_idasunto
		{
			get{return _T382_idasunto;}
			set{_T382_idasunto = value;}
		}

		public Byte T385_codestado
		{
			get{return _T385_codestado;}
			set{_T385_codestado = value;}
		}

		public String Estado
		{
			get{return _Estado;}
			set{_Estado = value;}
		}

		public DateTime T385_fecha
		{
			get{return _T385_fecha;}
			set{_T385_fecha = value;}
		}

		public Int32 T385_idautor
		{
			get{return _T385_idautor;}
			set{_T385_idautor = value;}
		}

		public String nomRecurso
		{
			get{return _nomRecurso;}
			set{_nomRecurso = value;}
		}


        #endregion

	}
}
