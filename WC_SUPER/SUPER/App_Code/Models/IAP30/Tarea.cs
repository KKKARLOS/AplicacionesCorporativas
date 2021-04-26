using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Tarea
    {

        /// <summary>
        /// Summary description for Tarea
        /// </summary>
		#region Private Variables
		private Int32 _t332_idtarea;
		private String _t332_destarea;
		private String _t332_destarealong;
		private Int32 _t331_idpt;
		private Int32 _t334_idfase;
		private Int32 _t335_idactividad;
		private Boolean _t332_notificable;
		private DateTime _t332_fiv;
		private DateTime _t332_ffv;
		private Byte _t332_estado;
		private DateTime _t332_fipl;
		private DateTime _t332_ffpl;
		private Double _t332_etpl;
		private DateTime _t332_ffpr;
		private Double _t332_etpr;
		private String _t332_observaciones;
		private Single _t332_cle;
		private String _t332_tipocle;
		private Int32 _t332_orden;
		private Boolean _t332_facturable;
		private Int32 _t305_idproyectosubnodo;
		private String _t305_cualidad;
		private Int32 _t303_idnodo;
		private String _t303_denominacion;
		private Int32 _num_proyecto;
		private String _nom_proyecto;
		private String _t331_despt;
		private String _t334_desfase;
		private String _t335_desactividad;
		private Int32 _cod_cliente;
		private String _nom_cliente;
		private Decimal _t332_presupuesto;
		private Int32 _t353_idorigen;
		private String _t332_incidencia;
		private Double _t332_avance;
		private Boolean _t332_avanceauto;
		private Boolean _t332_impiap;
		private Boolean _t305_admiterecursospst;
		private Boolean _t331_heredanodo;
		private Boolean _t331_heredaproyeco;
		private Boolean _t334_heredanodo;
		private Boolean _t334_heredaproyeco;
		private Boolean _t335_heredanodo;
		private Boolean _t335_heredaproyeco;
		private Boolean _t332_heredanodo;
		private Boolean _t332_heredaproyeco;
		private String _t332_mensaje;
		private Boolean _t332_notif_prof;
		private Boolean _t305_avisorecursopst;
		private String _t301_estado;
		private String _t332_acceso_iap;
		private Int32 _t324_idmodofact;
		private String _t324_denominacion;
		private Boolean _t301_esreplicable;
		private Boolean _t305_opd;
        private Double _consumo;
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

		public Boolean t332_notificable
		{
			get{return _t332_notificable;}
			set{_t332_notificable = value;}
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

		public Byte t332_estado
		{
			get{return _t332_estado;}
			set{_t332_estado = value;}
		}

		public DateTime t332_fipl
		{
			get{return _t332_fipl;}
			set{_t332_fipl = value;}
		}

		public DateTime t332_ffpl
		{
			get{return _t332_ffpl;}
			set{_t332_ffpl = value;}
		}

		public Double t332_etpl
		{
			get{return _t332_etpl;}
			set{_t332_etpl = value;}
		}

		public DateTime t332_ffpr
		{
			get{return _t332_ffpr;}
			set{_t332_ffpr = value;}
		}

		public Double t332_etpr
		{
			get{return _t332_etpr;}
			set{_t332_etpr = value;}
		}

		public String t332_observaciones
		{
			get{return _t332_observaciones;}
			set{_t332_observaciones = value;}
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

		public Int32 t305_idproyectosubnodo
		{
			get{return _t305_idproyectosubnodo;}
			set{_t305_idproyectosubnodo = value;}
		}

		public String t305_cualidad
		{
			get{return _t305_cualidad;}
			set{_t305_cualidad = value;}
		}

		public Int32 t303_idnodo
		{
			get{return _t303_idnodo;}
			set{_t303_idnodo = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
		}

		public Int32 num_proyecto
		{
			get{return _num_proyecto;}
			set{_num_proyecto = value;}
		}

		public String nom_proyecto
		{
			get{return _nom_proyecto;}
			set{_nom_proyecto = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public String t334_desfase
		{
			get{return _t334_desfase;}
			set{_t334_desfase = value;}
		}

		public String t335_desactividad
		{
			get{return _t335_desactividad;}
			set{_t335_desactividad = value;}
		}

		public Int32 cod_cliente
		{
			get{return _cod_cliente;}
			set{_cod_cliente = value;}
		}

		public String nom_cliente
		{
			get{return _nom_cliente;}
			set{_nom_cliente = value;}
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

		public String t332_incidencia
		{
			get{return _t332_incidencia;}
			set{_t332_incidencia = value;}
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

		public Boolean t332_impiap
		{
			get{return _t332_impiap;}
			set{_t332_impiap = value;}
		}

		public Boolean t305_admiterecursospst
		{
			get{return _t305_admiterecursospst;}
			set{_t305_admiterecursospst = value;}
		}

		public Boolean t331_heredanodo
		{
			get{return _t331_heredanodo;}
			set{_t331_heredanodo = value;}
		}

		public Boolean t331_heredaproyeco
		{
			get{return _t331_heredaproyeco;}
			set{_t331_heredaproyeco = value;}
		}

		public Boolean t334_heredanodo
		{
			get{return _t334_heredanodo;}
			set{_t334_heredanodo = value;}
		}

		public Boolean t334_heredaproyeco
		{
			get{return _t334_heredaproyeco;}
			set{_t334_heredaproyeco = value;}
		}

		public Boolean t335_heredanodo
		{
			get{return _t335_heredanodo;}
			set{_t335_heredanodo = value;}
		}

		public Boolean t335_heredaproyeco
		{
			get{return _t335_heredaproyeco;}
			set{_t335_heredaproyeco = value;}
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

		public Boolean t332_notif_prof
		{
			get{return _t332_notif_prof;}
			set{_t332_notif_prof = value;}
		}

		public Boolean t305_avisorecursopst
		{
			get{return _t305_avisorecursopst;}
			set{_t305_avisorecursopst = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
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

		public Boolean t301_esreplicable
		{
			get{return _t301_esreplicable;}
			set{_t301_esreplicable = value;}
		}

		public Boolean t305_opd
		{
			get{return _t305_opd;}
			set{_t305_opd = value;}
		}

        public Double consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
        }
        #endregion

	}
}
