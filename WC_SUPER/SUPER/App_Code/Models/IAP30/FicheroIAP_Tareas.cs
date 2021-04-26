using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class FicheroIAP_Tareas
    {

        /// <summary>
        /// Summary description for FicheroIAP_Tareas
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private Int32 _t331_idpt;
		private Byte _t331_estado;
		private Byte _t332_estado;
		private Single _t332_cle;
		private String _t332_tipocle;
		private Boolean _t332_impiap;
		private Int32 _t305_idproyectosubnodo;
		private DateTime _t332_fiv;
		private DateTime _t332_ffv;
		private String _t323_denominacion;
		private Boolean _t323_regjornocompleta;
		private Boolean _t323_regfes;
		private Boolean _t331_obligaest;
		private String _t301_estado;

		#endregion

		#region Public Properties
		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public String t332_destarea
		{
			get{return _t332_destarea;}
			set{_t332_destarea = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public Byte t331_estado
		{
			get{return _t331_estado;}
			set{_t331_estado = value;}
		}

		public Byte t332_estado
		{
			get{return _t332_estado;}
			set{_t332_estado = value;}
		}

		public Single t332_cle
		{
			get{return _t332_cle;}
			set{_t332_cle = value;}
		}

		public String t332_tipocle
		{
			get{return _t332_tipocle;}
			set{_t332_tipocle = value;}
		}

		public Boolean t332_impiap
		{
			get{return _t332_impiap;}
			set{_t332_impiap = value;}
		}

		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public DateTime t332_fiv
		{
			get{return _t332_fiv;}
			set{_t332_fiv = value;}
		}

		public DateTime t332_ffv
		{
			get{return _t332_ffv;}
			set{_t332_ffv = value;}
		}

		public String t323_denominacion
		{
			get{return _t323_denominacion;}
			set{_t323_denominacion = value;}
		}

		public Boolean t323_regjornocompleta
		{
			get{return _t323_regjornocompleta;}
			set{_t323_regjornocompleta = value;}
		}

		public Boolean t323_regfes
		{
			get{return _t323_regfes;}
			set{_t323_regfes = value;}
		}

		public Boolean t331_obligaest
		{
			get{return _t331_obligaest;}
			set{_t331_obligaest = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}


        #endregion

	}
}
