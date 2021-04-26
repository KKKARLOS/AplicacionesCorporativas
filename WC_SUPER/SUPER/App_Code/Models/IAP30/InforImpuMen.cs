using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class InforImpuMen
    {

        /// <summary>
        /// Summary description for InforImpuMen
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private String _Profesional;
		private Int32 _AnnoMes;
		private String _AnnoMesText;
		private DateTime _Fecha;
		private String _DiaSemana;
		private Single _Horas;

		#endregion

		#region Public Properties
		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public String Profesional
		{
			get{return _Profesional;}
			set{_Profesional = value;}
		}

		public Int32 AnnoMes
		{
			get{return _AnnoMes;}
			set{_AnnoMes = value;}
		}

		public String AnnoMesText
		{
			get{return _AnnoMesText;}
			set{_AnnoMesText = value;}
		}

		public DateTime Fecha
		{
			get{return _Fecha;}
			set{_Fecha = value;}
		}

		public String DiaSemana
		{
			get{return _DiaSemana;}
			set{_DiaSemana = value;}
		}

		public Single Horas
		{
			get{return _Horas;}
			set{_Horas = value;}
		}


        #endregion

	}
}
