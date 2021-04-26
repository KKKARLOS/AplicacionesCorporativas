using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class UsuarioProyectoSubNodo
    {

        /// <summary>
        /// Summary description for UsuarioProyectoSubNodo
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t314_idusuario;
		private Decimal _t330_costecon;
		private Decimal _t330_costerep;
		private Boolean _t330_deriva;
		private DateTime _t330_falta;
		private DateTime? _t330_fbaja;
		private Int32? _t333_idperfilproy;

		#endregion

		#region Public Properties
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public Decimal t330_costecon
		{
			get{return _t330_costecon;}
			set{_t330_costecon = value;}
		}

		public Decimal t330_costerep
		{
			get{return _t330_costerep;}
			set{_t330_costerep = value;}
		}

		public Boolean t330_deriva
		{
			get{return _t330_deriva;}
			set{_t330_deriva = value;}
		}

		public DateTime t330_falta
		{
			get{return _t330_falta;}
			set{_t330_falta = value;}
		}

		public DateTime? t330_fbaja
		{
			get{return _t330_fbaja;}
			set{_t330_fbaja = value;}
		}

		public Int32? t333_idperfilproy
		{
			get{return _t333_idperfilproy;}
			set{_t333_idperfilproy = value;}
		}


        #endregion

	}
}
