using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class EstimacionIAP
    {

        /// <summary>
        /// Summary description for EstimacionIAP
        /// </summary>
		#region Private Variables
		private Int32 _t314_idusuario;
		private Int32 _t332_idtarea;
		private Double? _t336_ete;
		private DateTime? _t336_ffe;
		private String _t336_comentario;
		private Boolean _t336_completado;

		#endregion

		#region Public Properties
		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public Double? t336_ete
		{
			get{return _t336_ete;}
			set{_t336_ete = value;}
		}

		public DateTime? t336_ffe
		{
			get{return _t336_ffe;}
			set{_t336_ffe = value;}
		}

		public String t336_comentario
		{
			get{return _t336_comentario;}
			set{_t336_comentario = value;}
		}

		public Boolean t336_completado
		{
			get{return _t336_completado;}
			set{_t336_completado = value;}
		}


        #endregion

	}
}
