using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class AsuntoEstadoT
    {

        /// <summary>
        /// Summary description for AsuntoEstadoT
        /// </summary>
		#region Private Variables
		private Int32 _t600_idasunto;
		private Byte _t606_estado;
		private String _Estado;
		private DateTime _t606_fecha;
		private Int32 _t314_idusuario;
		private String _nomRecurso;

		#endregion

		#region Public Properties
		public Int32 t600_idasunto
		{
			get{return _t600_idasunto;}
			set{_t600_idasunto = value;}
		}

		public Byte t606_estado
		{
			get{return _t606_estado;}
			set{_t606_estado = value;}
		}

		public String Estado
		{
			get{return _Estado;}
			set{_Estado = value;}
		}

		public DateTime t606_fecha
		{
			get{return _t606_fecha;}
			set{_t606_fecha = value;}
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
