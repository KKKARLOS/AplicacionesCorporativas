using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class CabeceraGV
    {

        /// <summary>
        /// Summary description for CabeceraGV
        /// </summary>
		#region Private Variables
		private Int32 _t420_idreferencia;
		private String _t431_idestado;
		private String _t420_concepto;
		private Int32 _t001_idficepi_solicitante;
		private Int32 _t314_idusuario_interesado;
		private Byte _t423_idmotivo;
		private Boolean _t420_justificantes;
		private Int32 _t305_idproyectosubnodo;
		private String _t422_idmoneda;
		private String _t420_comentarionota;
		private String _t420_anotaciones;
		private Decimal _t420_importeanticipo;
		private DateTime? _t420_fanticipo;
		private String _t420_lugaranticipo;
		private Decimal _t420_importedevolucion;
		private DateTime? _t420_fdevolucion;
		private String _t420_lugardevolucion;
		private String _t420_aclaracionesanticipo;
		private Decimal _t420_pagadotransporte;
		private Decimal _t420_pagadohotel;
		private Decimal _t420_pagadootros;
		private String _t420_aclaracionepagado;
		private Int32 _t313_idempresa;
		private Byte _t007_idterrfis;
		private Decimal _t420_impdico;
		private Decimal _t420_impmdco;
		private Decimal _t420_impalco;
		private Decimal _t420_impkmco;
		private Decimal _t420_impdeco;
		private Decimal _t420_impdiex;
		private Decimal _t420_impmdex;
		private Decimal _t420_impalex;
		private Decimal _t420_impkmex;
		private Decimal _t420_impdeex;
		private Int16 _t010_idoficina;
		private Int32? _t420_idreferencia_lote;
		private String _t175_idcc;
        private Boolean _autoResponsable;

        #endregion

        #region Public Properties
        public Int32 t420_idreferencia
		{
			get{return _t420_idreferencia;}
			set{_t420_idreferencia = value;}
		}
		public String t431_idestado
		{
			get{return _t431_idestado;}
			set{_t431_idestado = value;}
		}
		public String t420_concepto
		{
			get{return _t420_concepto;}
			set{_t420_concepto = value;}
		}
		public Int32 t001_idficepi_solicitante
		{
			get{return _t001_idficepi_solicitante;}
			set{_t001_idficepi_solicitante = value;}
		}
		public Int32 t314_idusuario_interesado
		{
			get{return _t314_idusuario_interesado;}
			set{_t314_idusuario_interesado = value;}
		}
		public Byte t423_idmotivo
		{
			get{return _t423_idmotivo;}
			set{_t423_idmotivo = value;}
		}
		public Boolean t420_justificantes
		{
			get{return _t420_justificantes;}
			set{_t420_justificantes = value;}
		}
		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}
		public String t422_idmoneda
		{
			get{return _t422_idmoneda;}
			set{_t422_idmoneda = value;}
		}
		public String t420_comentarionota
		{
			get{return _t420_comentarionota;}
			set{_t420_comentarionota = value;}
		}
		public String t420_anotaciones
		{
			get{return _t420_anotaciones;}
			set{_t420_anotaciones = value;}
		}
		public Decimal t420_importeanticipo
		{
			get{return _t420_importeanticipo;}
			set{_t420_importeanticipo = value;}
		}
		public DateTime? t420_fanticipo
		{
			get{return _t420_fanticipo;}
			set{_t420_fanticipo = value;}
		}
		public String t420_lugaranticipo
		{
			get{return _t420_lugaranticipo;}
			set{_t420_lugaranticipo = value;}
		}
		public Decimal t420_importedevolucion
		{
			get{return _t420_importedevolucion;}
			set{_t420_importedevolucion = value;}
		}
		public DateTime? t420_fdevolucion
		{
			get{return _t420_fdevolucion;}
			set{_t420_fdevolucion = value;}
		}
		public String t420_lugardevolucion
		{
			get{return _t420_lugardevolucion;}
			set{_t420_lugardevolucion = value;}
		}
		public String t420_aclaracionesanticipo
		{
			get{return _t420_aclaracionesanticipo;}
			set{_t420_aclaracionesanticipo = value;}
		}
		public Decimal t420_pagadotransporte
		{
			get{return _t420_pagadotransporte;}
			set{_t420_pagadotransporte = value;}
		}
		public Decimal t420_pagadohotel
		{
			get{return _t420_pagadohotel;}
			set{_t420_pagadohotel = value;}
		}
		public Decimal t420_pagadootros
		{
			get{return _t420_pagadootros;}
			set{_t420_pagadootros = value;}
		}
		public String t420_aclaracionepagado
		{
			get{return _t420_aclaracionepagado;}
			set{_t420_aclaracionepagado = value;}
		}
		public Int32 t313_idempresa
		{
			get{return _t313_idempresa;}
			set{_t313_idempresa = value;}
		}
		public Byte t007_idterrfis
		{
			get{return _t007_idterrfis;}
			set{_t007_idterrfis = value;}
		}
		public Decimal t420_impdico
		{
			get{return _t420_impdico;}
			set{_t420_impdico = value;}
		}
		public Decimal t420_impmdco
		{
			get{return _t420_impmdco;}
			set{_t420_impmdco = value;}
		}
		public Decimal t420_impalco
		{
			get{return _t420_impalco;}
			set{_t420_impalco = value;}
		}
		public Decimal t420_impkmco
		{
			get{return _t420_impkmco;}
			set{_t420_impkmco = value;}
		}
		public Decimal t420_impdeco
		{
			get{return _t420_impdeco;}
			set{_t420_impdeco = value;}
		}
		public Decimal t420_impdiex
		{
			get{return _t420_impdiex;}
			set{_t420_impdiex = value;}
		}
		public Decimal t420_impmdex
		{
			get{return _t420_impmdex;}
			set{_t420_impmdex = value;}
		}
		public Decimal t420_impalex
		{
			get{return _t420_impalex;}
			set{_t420_impalex = value;}
		}
		public Decimal t420_impkmex
		{
			get{return _t420_impkmex;}
			set{_t420_impkmex = value;}
		}
		public Decimal t420_impdeex
		{
			get{return _t420_impdeex;}
			set{_t420_impdeex = value;}
		}
		public Int16 t010_idoficina
		{
			get{return _t010_idoficina;}
			set{_t010_idoficina = value;}
		}
		public Int32? t420_idreferencia_lote
		{
			get{return _t420_idreferencia_lote;}
			set{_t420_idreferencia_lote = value;}
		}
		public String t175_idcc
		{
			get{return _t175_idcc;}
			set{_t175_idcc = value;}
		}
        public Boolean autoResponsable
        {
            get { return _autoResponsable; }
            set { _autoResponsable = value; }
        }


        #endregion

    }
}
