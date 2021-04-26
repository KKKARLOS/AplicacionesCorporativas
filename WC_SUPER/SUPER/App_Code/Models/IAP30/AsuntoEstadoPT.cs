using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoEstadoPT
    {

        /// <summary>
        /// Summary description for AsuntoEstadoPT
        /// </summary>
		#region Private Variables
		private Int32 _t409_idasunto;
		private Byte _t416_estado;
		private String _Estado;
		private DateTime _t416_fecha;
		private Int32 _t314_idusuario;
		private String _nomRecurso;

		#endregion

		#region Public Properties
		public Int32 t409_idasunto
		{
			get{return _t409_idasunto;}
			set{_t409_idasunto = value;}
		}

		public Byte t416_estado
		{
			get{return _t416_estado;}
			set{_t416_estado = value;}
		}

		public String Estado
		{
			get{return _Estado;}
			set{_Estado = value;}
		}

		public DateTime t416_fecha
		{
			get{return _t416_fecha;}
			set{_t416_fecha = value;}
		}

		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String nomRecurso
		{
			get{return _nomRecurso;}
			set{_nomRecurso = value;}
		}


        #endregion

	}
}
