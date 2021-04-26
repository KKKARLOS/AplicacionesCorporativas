using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class EmpresasProfesional
    {

        /// <summary>
        /// Summary description for EmpresasProfesional
        /// </summary>
		#region Private Variables
		private Int32 _t313_idempresa;
		private String _t313_denominacion;
		private Int16 _t007_idterrfis;
		private String _t007_nomterrfis;
		private Decimal _T007_ITERDC;
		private Decimal _T007_ITERMD;
		private Decimal _T007_ITERDA;
		private Decimal _T007_ITERDE;
		private Decimal _T007_ITERK;

		#endregion

		#region Public Properties
		public Int32 t313_idempresa
		{
			get{return _t313_idempresa;}
			set{_t313_idempresa = value;}
		}

		public String t313_denominacion
		{
			get{return _t313_denominacion;}
			set{_t313_denominacion = value;}
		}

		public Int16 t007_idterrfis
		{
			get{return _t007_idterrfis;}
			set{_t007_idterrfis = value;}
		}

		public String t007_nomterrfis
		{
			get{return _t007_nomterrfis;}
			set{_t007_nomterrfis = value;}
		}

		public Decimal T007_ITERDC
		{
			get{return _T007_ITERDC;}
			set{_T007_ITERDC = value;}
		}

		public Decimal T007_ITERMD
		{
			get{return _T007_ITERMD;}
			set{_T007_ITERMD = value;}
		}

		public Decimal T007_ITERDA
		{
			get{return _T007_ITERDA;}
			set{_T007_ITERDA = value;}
		}

		public Decimal T007_ITERDE
		{
			get{return _T007_ITERDE;}
			set{_T007_ITERDE = value;}
		}

		public Decimal T007_ITERK
		{
			get{return _T007_ITERK;}
			set{_T007_ITERK = value;}
		}


        #endregion

	}
}
