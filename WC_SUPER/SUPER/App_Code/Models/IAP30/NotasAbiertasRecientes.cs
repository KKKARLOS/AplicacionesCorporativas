using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class NotasAbiertasRecientes
    {

        /// <summary>
        /// Summary description for NotasAbiertasRecientes
        /// </summary>
		#region Private Variables
		private Int32 _t420_idreferencia;
		private Int32 _nota_aparcada;
		private String _TipoNota;
		private String _t431_idestado;
		private String _t431_denominacion;
		private DateTime _t420_fTramitada;
		private Int32 _t314_idusuario_interesado;
		private String _t001_sexo_interesado;
		private String _Interesado;
		private String _Solicitante;
		private String _AprobadaPor;
		private String _NoAprobadaPor;
		private String _AceptadaPor;
		private String _NoAceptadaPor;
		private DateTime _t420_fAprobada;
		private DateTime _t420_fNoAprobada;
		private DateTime _t420_fAceptada;
		private DateTime _t420_fNoaceptada;
		private DateTime _t420_fAnulada;
		private String _AnuladaPor;
		private DateTime _t420_fContabilizada;
		private DateTime _t420_fPagada;
		private String _t420_concepto;
		private String _t423_denominacion;
		private Int32 _t301_idproyecto;
		private String _t301_denominacion;
		private Decimal _TOTALVIAJE;
		private Decimal _TOTALEUROS;
		private Decimal _ACOBRAR_SINRETENCION;
		private Decimal _ACOBRAR_SINRETENCION_EUROS;
		private Decimal _ACOBRAR_NOMINA;
		private String _t422_idmoneda;
		private String _t422_denominacion;

		#endregion

		#region Public Properties
		public Int32 t420_idreferencia
		{
			get{return _t420_idreferencia;}
			set{_t420_idreferencia = value;}
		}

		public Int32 nota_aparcada
		{
			get{return _nota_aparcada;}
			set{_nota_aparcada = value;}
		}

		public String TipoNota
		{
			get{return _TipoNota;}
			set{_TipoNota = value;}
		}

		public String t431_idestado
		{
			get{return _t431_idestado;}
			set{_t431_idestado = value;}
		}

		public String t431_denominacion
		{
			get{return _t431_denominacion;}
			set{_t431_denominacion = value;}
		}

		public DateTime t420_fTramitada
		{
			get{return _t420_fTramitada;}
			set{_t420_fTramitada = value;}
		}

		public Int32 t314_idusuario_interesado
		{
			get{return _t314_idusuario_interesado;}
			set{_t314_idusuario_interesado = value;}
		}

		public String t001_sexo_interesado
		{
			get{return _t001_sexo_interesado;}
			set{_t001_sexo_interesado = value;}
		}

		public String Interesado
		{
			get{return _Interesado;}
			set{_Interesado = value;}
		}

		public String Solicitante
		{
			get{return _Solicitante;}
			set{_Solicitante = value;}
		}

		public String AprobadaPor
		{
			get{return _AprobadaPor;}
			set{_AprobadaPor = value;}
		}

		public String NoAprobadaPor
		{
			get{return _NoAprobadaPor;}
			set{_NoAprobadaPor = value;}
		}

		public String AceptadaPor
		{
			get{return _AceptadaPor;}
			set{_AceptadaPor = value;}
		}

		public String NoAceptadaPor
		{
			get{return _NoAceptadaPor;}
			set{_NoAceptadaPor = value;}
		}

		public DateTime t420_fAprobada
		{
			get{return _t420_fAprobada;}
			set{_t420_fAprobada = value;}
		}

		public DateTime t420_fNoAprobada
		{
			get{return _t420_fNoAprobada;}
			set{_t420_fNoAprobada = value;}
		}

		public DateTime t420_fAceptada
		{
			get{return _t420_fAceptada;}
			set{_t420_fAceptada = value;}
		}

		public DateTime t420_fNoaceptada
		{
			get{return _t420_fNoaceptada;}
			set{_t420_fNoaceptada = value;}
		}

		public DateTime t420_fAnulada
		{
			get{return _t420_fAnulada;}
			set{_t420_fAnulada = value;}
		}

		public String AnuladaPor
		{
			get{return _AnuladaPor;}
			set{_AnuladaPor = value;}
		}

		public DateTime t420_fContabilizada
		{
			get{return _t420_fContabilizada;}
			set{_t420_fContabilizada = value;}
		}

		public DateTime t420_fPagada
		{
			get{return _t420_fPagada;}
			set{_t420_fPagada = value;}
		}

		public String t420_concepto
		{
			get{return _t420_concepto;}
			set{_t420_concepto = value;}
		}

		public String t423_denominacion
		{
			get{return _t423_denominacion;}
			set{_t423_denominacion = value;}
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

		public Decimal TOTALVIAJE
		{
			get{return _TOTALVIAJE;}
			set{_TOTALVIAJE = value;}
		}

		public Decimal TOTALEUROS
		{
			get{return _TOTALEUROS;}
			set{_TOTALEUROS = value;}
		}

		public Decimal ACOBRAR_SINRETENCION
		{
			get{return _ACOBRAR_SINRETENCION;}
			set{_ACOBRAR_SINRETENCION = value;}
		}

		public Decimal ACOBRAR_SINRETENCION_EUROS
		{
			get{return _ACOBRAR_SINRETENCION_EUROS;}
			set{_ACOBRAR_SINRETENCION_EUROS = value;}
		}

		public Decimal ACOBRAR_NOMINA
		{
			get{return _ACOBRAR_NOMINA;}
			set{_ACOBRAR_NOMINA = value;}
		}

		public String t422_idmoneda
		{
			get{return _t422_idmoneda;}
			set{_t422_idmoneda = value;}
		}

		public String t422_denominacion
		{
			get{return _t422_denominacion;}
			set{_t422_denominacion = value;}
		}


        #endregion

	}
}
