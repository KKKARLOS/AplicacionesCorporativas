using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaRecursos
    {

        /// <summary>
        /// Summary description for TareaRecursos
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private Int32 _t314_idusuario;
		private Double _t336_ete;
		private DateTime _t336_ffe;
		private Double _t336_etp;
		private DateTime _t336_fip;
		private DateTime _t336_ffp;
		private Int32 _t333_idperfilproy;
		private Int32 _t336_estado;
		private String _t336_comentario;
		private String _t336_indicaciones;
		private String _nombreCompleto;
		private Double _Pendiente;
        private Boolean _t336_completado;
		private Boolean _t336_notif_exceso;
		private Decimal _Acumulado;
        private Nullable<DateTime> _PrimerConsumo;
        private Nullable<DateTime> _UltimoConsumo;

		#endregion

		#region Public Properties
		public Int32 t332_idtarea
		{
			get{return _t332_idtarea;}
			set{_t332_idtarea = value;}
		}

		public Int32 t314_idusuario
		{
			get{return _t314_idusuario;}
			set{_t314_idusuario = value;}
		}

		public Double t336_ete
		{
			get{return _t336_ete;}
			set{_t336_ete = value;}
		}

		public DateTime t336_ffe
		{
			get{return _t336_ffe;}
			set{_t336_ffe = value;}
		}

		public Double t336_etp
		{
			get{return _t336_etp;}
			set{_t336_etp = value;}
		}

		public DateTime t336_fip
		{
			get{return _t336_fip;}
			set{_t336_fip = value;}
		}

		public DateTime t336_ffp
		{
			get{return _t336_ffp;}
			set{_t336_ffp = value;}
		}

		public Int32 t333_idperfilproy
		{
			get{return _t333_idperfilproy;}
			set{_t333_idperfilproy = value;}
		}

		public Int32 t336_estado
		{
			get{return _t336_estado;}
			set{_t336_estado = value;}
		}

		public String t336_comentario
		{
			get{return _t336_comentario;}
			set{_t336_comentario = value;}
		}

		public String t336_indicaciones
		{
			get{return _t336_indicaciones;}
			set{_t336_indicaciones = value;}
		}

		public String nombreCompleto
		{
			get{return _nombreCompleto;}
			set{_nombreCompleto = value;}
		}

		public Double Pendiente
		{
			get{return _Pendiente;}
			set{_Pendiente = value;}
		}

        public Boolean t336_completado
		{
			get{return _t336_completado;}
			set{_t336_completado = value;}
		}

		public Boolean t336_notif_exceso
		{
			get{return _t336_notif_exceso;}
			set{_t336_notif_exceso = value;}
		}

		public Decimal Acumulado
		{
			get{return _Acumulado;}
			set{_Acumulado = value;}
		}

        public Nullable<DateTime> PrimerConsumo
		{
			get{return _PrimerConsumo;}
			set{_PrimerConsumo = value;}
		}

        public Nullable<DateTime> UltimoConsumo
		{
			get{return _UltimoConsumo;}
			set{_UltimoConsumo = value;}
		}


        #endregion

	}
}
