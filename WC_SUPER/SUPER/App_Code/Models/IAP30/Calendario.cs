using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Calendario
    {

        /// <summary>
        /// Summary description for Calendario
        /// </summary>
		#region Private Variables
		private Int32   _t066_idcal;
		private String  _t066_descal;
		private Int32   _t066_estado;
		private Int32   _t303_idnodo;
		private String  _t066_tipocal;
		private Int32   _t066_semlabL;
		private Int32   _t066_semlabM;
		private Int32   _t066_semlabX;
		private Int32   _t066_semlabJ;
		private Int32   _t066_semlabV;
		private Int32   _t066_semlabS;
		private Int32   _t066_semlabD;
		private String  _t066_obs;

		#endregion

		#region Public Properties
		public Int32 t066_idcal
		{
			get{return _t066_idcal;}
			set{_t066_idcal = value;}
		}

		public String t066_descal
		{
			get{return _t066_descal;}
			set{_t066_descal = value;}
		}

		public Int32 t066_estado
		{
			get{return _t066_estado;}
			set{_t066_estado = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public String t066_tipocal
		{
			get{return _t066_tipocal;}
			set{_t066_tipocal = value;}
		}

		public Int32 t066_semlabL
		{
			get{return _t066_semlabL;}
			set{_t066_semlabL = value;}
		}

		public Int32 t066_semlabM
		{
			get{return _t066_semlabM;}
			set{_t066_semlabM = value;}
		}

		public Int32 t066_semlabX
		{
			get{return _t066_semlabX;}
			set{_t066_semlabX = value;}
		}

		public Int32 t066_semlabJ
		{
			get{return _t066_semlabJ;}
			set{_t066_semlabJ = value;}
		}

		public Int32 t066_semlabV
		{
			get{return _t066_semlabV;}
			set{_t066_semlabV = value;}
		}

		public Int32 t066_semlabS
		{
			get{return _t066_semlabS;}
			set{_t066_semlabS = value;}
		}

		public Int32 t066_semlabD
		{
			get{return _t066_semlabD;}
			set{_t066_semlabD = value;}
		}

		public String t066_obs
		{
			get{return _t066_obs;}
			set{_t066_obs = value;}
		}


        #endregion

	}
}
