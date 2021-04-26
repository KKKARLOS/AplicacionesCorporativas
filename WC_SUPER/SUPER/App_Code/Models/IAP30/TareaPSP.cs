using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class TareaPSP
    {

        /// <summary>
        /// Summary description for TareaPSP
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private String _t332_destarealong;
		private Int32 _t331_idpt;
		private Int32 _t334_idfase;
		private Int32 _t335_idactividad;
		private Int32 _t314_idusuario_promotor;
		private Int32 _t314_idusuario_ultmodif;
		private DateTime _t332_falta;
		private DateTime _t332_fultmodif;
		private DateTime _t332_fiv;
		private DateTime? _t332_ffv;
		private Byte _t332_estado;
		private DateTime? _t332_fipl;
		private DateTime? _t332_ffpl;
		private Double _t332_etpl;
		private DateTime? _t332_ffpr;
		private Double _t332_etpr;
		private Int32 _t346_idpst;
		private Single _t332_cle;
		private String _t332_tipocle;
		private Int32 _t332_orden;
		private Boolean _t332_facturable;
		private Decimal _t332_presupuesto;
		private Int32 _t353_idorigen;
		private String _t332_otl;
		private String _t332_incidencia;
		private String _t332_observaciones;
		private Boolean _t332_notificable;
		private String _t332_notas1;
		private String _t332_notas2;
		private String _t332_notas3;
		private String _t332_notas4;
		private Double _t332_avance;
		private Boolean _t332_avanceauto;
		private Int32 _t314_idusuario_fin;
		private DateTime? _t332_ffin;
		private Int32 _t314_idusuario_cierre;
		private DateTime? _t332_fcierre;
		private Boolean _t332_impiap;
		private Boolean _t332_notasiap;
		private Boolean _t332_heredanodo;
		private Boolean _t332_heredaproyeco;
		private String _t332_mensaje;
		private String _t332_acceso_iap;
		private Int32 _t324_idmodofact;
		private String _t324_denominacion;

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

		public String t332_destarealong
		{
			get{return _t332_destarealong;}
			set{_t332_destarealong = value;}
		}

		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public Int32 t334_idfase
		{
			get{return _t334_idfase;}
			set{_t334_idfase = value;}
		}

		public Int32 t335_idactividad
		{
			get{return _t335_idactividad;}
			set{_t335_idactividad = value;}
		}

		public Int32 t314_idusuario_promotor
		{
			get{return _t314_idusuario_promotor;}
			set{_t314_idusuario_promotor = value;}
		}

		public Int32 t314_idusuario_ultmodif
		{
			get{return _t314_idusuario_ultmodif;}
			set{_t314_idusuario_ultmodif = value;}
		}

		public DateTime t332_falta
		{
			get{return _t332_falta;}
			set{_t332_falta = value;}
		}

		public DateTime t332_fultmodif
		{
			get{return _t332_fultmodif;}
			set{_t332_fultmodif = value;}
		}

		public DateTime t332_fiv
		{
			get{return _t332_fiv;}
			set{_t332_fiv = value;}
		}

		public DateTime? t332_ffv
		{
			get{return _t332_ffv;}
			set{_t332_ffv = value;}
		}

		public Byte t332_estado
		{
			get{return _t332_estado;}
			set{_t332_estado = value;}
		}

		public DateTime? t332_fipl
		{
			get{return _t332_fipl;}
			set{_t332_fipl = value;}
		}

		public DateTime? t332_ffpl
		{
			get{return _t332_ffpl;}
			set{_t332_ffpl = value;}
		}

		public Double t332_etpl
		{
			get{return _t332_etpl;}
			set{_t332_etpl = value;}
		}

		public DateTime? t332_ffpr
		{
			get{return _t332_ffpr;}
			set{_t332_ffpr = value;}
		}

		public Double t332_etpr
		{
			get{return _t332_etpr;}
			set{_t332_etpr = value;}
		}

		public Int32 t346_idpst
		{
			get{return _t346_idpst;}
			set{_t346_idpst = value;}
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

		public Int32 t332_orden
		{
			get{return _t332_orden;}
			set{_t332_orden = value;}
		}

		public Boolean t332_facturable
		{
			get{return _t332_facturable;}
			set{_t332_facturable = value;}
		}

		public Decimal t332_presupuesto
		{
			get{return _t332_presupuesto;}
			set{_t332_presupuesto = value;}
		}

		public Int32 t353_idorigen
		{
			get{return _t353_idorigen;}
			set{_t353_idorigen = value;}
		}

		public String t332_otl
		{
			get{return _t332_otl;}
			set{_t332_otl = value;}
		}

		public String t332_incidencia
		{
			get{return _t332_incidencia;}
			set{_t332_incidencia = value;}
		}

		public String t332_observaciones
		{
			get{return _t332_observaciones;}
			set{_t332_observaciones = value;}
		}

		public Boolean t332_notificable
		{
			get{return _t332_notificable;}
			set{_t332_notificable = value;}
		}

		public String t332_notas1
		{
			get{return _t332_notas1;}
			set{_t332_notas1 = value;}
		}

		public String t332_notas2
		{
			get{return _t332_notas2;}
			set{_t332_notas2 = value;}
		}

		public String t332_notas3
		{
			get{return _t332_notas3;}
			set{_t332_notas3 = value;}
		}

		public String t332_notas4
		{
			get{return _t332_notas4;}
			set{_t332_notas4 = value;}
		}

		public Double t332_avance
		{
			get{return _t332_avance;}
			set{_t332_avance = value;}
		}

		public Boolean t332_avanceauto
		{
			get{return _t332_avanceauto;}
			set{_t332_avanceauto = value;}
		}

		public Int32 t314_idusuario_fin
		{
			get{return _t314_idusuario_fin;}
			set{_t314_idusuario_fin = value;}
		}

		public DateTime? t332_ffin
		{
			get{return _t332_ffin;}
			set{_t332_ffin = value;}
		}

		public Int32 t314_idusuario_cierre
		{
			get{return _t314_idusuario_cierre;}
			set{_t314_idusuario_cierre = value;}
		}

		public DateTime? t332_fcierre
		{
			get{return _t332_fcierre;}
			set{_t332_fcierre = value;}
		}

		public Boolean t332_impiap
		{
			get{return _t332_impiap;}
			set{_t332_impiap = value;}
		}

		public Boolean t332_notasiap
		{
			get{return _t332_notasiap;}
			set{_t332_notasiap = value;}
		}

		public Boolean t332_heredanodo
		{
			get{return _t332_heredanodo;}
			set{_t332_heredanodo = value;}
		}

		public Boolean t332_heredaproyeco
		{
			get{return _t332_heredaproyeco;}
			set{_t332_heredaproyeco = value;}
		}

		public String t332_mensaje
		{
			get{return _t332_mensaje;}
			set{_t332_mensaje = value;}
		}

		public String t332_acceso_iap
		{
			get{return _t332_acceso_iap;}
			set{_t332_acceso_iap = value;}
		}

		public Int32 t324_idmodofact
		{
			get{return _t324_idmodofact;}
			set{_t324_idmodofact = value;}
		}

		public String t324_denominacion
		{
			get{return _t324_denominacion;}
			set{_t324_denominacion = value;}
		}


        #endregion

	}
}
