using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProyectoNota
    {

        /// <summary>
        /// Summary description for ProyectoNota
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t301_idproyecto;
		private String _t301_denominacion;
		private String _t305_seudonimo;
		private String _t305_cualidad;
		private String _t301_estado;
		private String _Responsable_Proyecto;
		private String _Aprobador;
		private String _Sexo_Aprobador;
		private String _t303_denominacion;
		private String _t302_denominacion;

		#endregion

		#region Public Properties
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public Int32 t301_idproyecto
		{
			get{return _t301_idproyecto;}
			set{_t301_idproyecto = value;}
		}

		public String t301_denominacion
		{
			get{return _t301_denominacion;}
			set{_t301_denominacion = value;}
		}

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public String t305_cualidad
		{
			get{return _t305_cualidad;}
			set{_t305_cualidad = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

		public String Responsable_Proyecto
		{
			get{return _Responsable_Proyecto;}
			set{_Responsable_Proyecto = value;}
		}

		public String Aprobador
		{
			get{return _Aprobador;}
			set{_Aprobador = value;}
		}

		public String Sexo_Aprobador
		{
			get{return _Sexo_Aprobador;}
			set{_Sexo_Aprobador = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}


        #endregion

	}
}
