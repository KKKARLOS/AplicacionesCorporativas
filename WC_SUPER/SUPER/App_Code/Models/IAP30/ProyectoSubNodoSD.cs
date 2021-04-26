using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProyectoSubNodoSD
    {

        /// <summary>
        /// Summary description for ProyectoSubNodoSD
        /// </summary>
		#region Private Variables
		private Int32 _t305_idproyectosubnodo;
		private Int32 _t301_idproyecto;
		private Int32 _t304_idsubnodo;
		private Boolean _t305_finalizado;
		private String _t305_cualidad;
		private Boolean _t305_heredanodo;
		private Int32 _t303_idnodo;
		private String _t303_denominacion;
		private String _t304_denominacion;
		private Int32 _t303_ultcierreeco;
		private Int32 _t314_idusuario_responsable;
		private String _Responsable;
		private String _t305_seudonimo;
		private String _t305_accesobitacora_iap;
		private String _t305_accesobitacora_pst;
		private Boolean _t305_imputablegasvi;
		private Boolean _t305_admiterecursospst;
		private Boolean _t305_avisoresponsablepst;
		private Boolean _t305_avisorecursopst;
		private Boolean _t305_avisofigura;
		private String _t305_modificaciones;
		private String _t305_observaciones;
		private Boolean _t320_facturable;
		private Int32 _mesesCerrados;
		private Int32 _t001_ficepi_visador;
		private String _Visador;
		private Boolean _t305_supervisor_visador;
		private Int32 _t476_idcnp;
		private Int32 _t485_idcsn1p;
		private Int32 _t487_idcsn2p;
		private Int32 _t489_idcsn3p;
		private Int32 _t491_idcsn4p;
		private String _t305_observacionesadm;
		private Byte _t305_importaciongasvi;
		private String _t391_denominacion;
		private String _t392_denominacion;
		private String _t393_denominacion;
		private String _t394_denominacion;
		private String _t301_categoria;
		private String _t422_idmoneda;
		private String _t422_denominacion;
		private String _t422_denominacionimportes;
		private Boolean _t305_opd;
		private Int32 _t001_idficepi_visadorcv;
		private String _VisadorCV;
		private Int32 _t001_idficepi_interlocutor;
		private String _Interlocutor;
		private String _PROFESIONAL_DICENO_CVT;
		private DateTime _t301_fechano_cvt;
		private String _t301_motivono_cvt;

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

		public Int32 t304_idsubnodo
		{
			get{return _t304_idsubnodo;}
			set{_t304_idsubnodo = value;}
		}

		public Boolean t305_finalizado
		{
			get{return _t305_finalizado;}
			set{_t305_finalizado = value;}
		}

		public String t305_cualidad
		{
			get{return _t305_cualidad;}
			set{_t305_cualidad = value;}
		}

		public Boolean t305_heredanodo
		{
			get{return _t305_heredanodo;}
			set{_t305_heredanodo = value;}
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

		public String t304_denominacion
		{
			get{return _t304_denominacion;}
			set{_t304_denominacion = value;}
		}

		public Int32 t303_ultcierreeco
		{
			get{return _t303_ultcierreeco;}
			set{_t303_ultcierreeco = value;}
		}

		public Int32 t314_idusuario_responsable
		{
			get{return _t314_idusuario_responsable;}
			set{_t314_idusuario_responsable = value;}
		}

		public String Responsable
		{
			get{return _Responsable;}
			set{_Responsable = value;}
		}

		public String t305_seudonimo
		{
			get{return _t305_seudonimo;}
			set{_t305_seudonimo = value;}
		}

		public String t305_accesobitacora_iap
		{
			get{return _t305_accesobitacora_iap;}
			set{_t305_accesobitacora_iap = value;}
		}

		public String t305_accesobitacora_pst
		{
			get{return _t305_accesobitacora_pst;}
			set{_t305_accesobitacora_pst = value;}
		}

		public Boolean t305_imputablegasvi
		{
			get{return _t305_imputablegasvi;}
			set{_t305_imputablegasvi = value;}
		}

		public Boolean t305_admiterecursospst
		{
			get{return _t305_admiterecursospst;}
			set{_t305_admiterecursospst = value;}
		}

		public Boolean t305_avisoresponsablepst
		{
			get{return _t305_avisoresponsablepst;}
			set{_t305_avisoresponsablepst = value;}
		}

		public Boolean t305_avisorecursopst
		{
			get{return _t305_avisorecursopst;}
			set{_t305_avisorecursopst = value;}
		}

		public Boolean t305_avisofigura
		{
			get{return _t305_avisofigura;}
			set{_t305_avisofigura = value;}
		}

		public String t305_modificaciones
		{
			get{return _t305_modificaciones;}
			set{_t305_modificaciones = value;}
		}

		public String t305_observaciones
		{
			get{return _t305_observaciones;}
			set{_t305_observaciones = value;}
		}

		public Boolean t320_facturable
		{
			get{return _t320_facturable;}
			set{_t320_facturable = value;}
		}

		public Int32 mesesCerrados
		{
			get{return _mesesCerrados;}
			set{_mesesCerrados = value;}
		}

		public Int32 t001_ficepi_visador
		{
			get{return _t001_ficepi_visador;}
			set{_t001_ficepi_visador = value;}
		}

		public String Visador
		{
			get{return _Visador;}
			set{_Visador = value;}
		}

		public Boolean t305_supervisor_visador
		{
			get{return _t305_supervisor_visador;}
			set{_t305_supervisor_visador = value;}
		}

		public Int32 t476_idcnp
		{
			get{return _t476_idcnp;}
			set{_t476_idcnp = value;}
		}

		public Int32 t485_idcsn1p
		{
			get{return _t485_idcsn1p;}
			set{_t485_idcsn1p = value;}
		}

		public Int32 t487_idcsn2p
		{
			get{return _t487_idcsn2p;}
			set{_t487_idcsn2p = value;}
		}

		public Int32 t489_idcsn3p
		{
			get{return _t489_idcsn3p;}
			set{_t489_idcsn3p = value;}
		}

		public Int32 t491_idcsn4p
		{
			get{return _t491_idcsn4p;}
			set{_t491_idcsn4p = value;}
		}

		public String t305_observacionesadm
		{
			get{return _t305_observacionesadm;}
			set{_t305_observacionesadm = value;}
		}

		public Byte t305_importaciongasvi
		{
			get{return _t305_importaciongasvi;}
			set{_t305_importaciongasvi = value;}
		}

		public String t391_denominacion
		{
			get{return _t391_denominacion;}
			set{_t391_denominacion = value;}
		}

		public String t392_denominacion
		{
			get{return _t392_denominacion;}
			set{_t392_denominacion = value;}
		}

		public String t393_denominacion
		{
			get{return _t393_denominacion;}
			set{_t393_denominacion = value;}
		}

		public String t394_denominacion
		{
			get{return _t394_denominacion;}
			set{_t394_denominacion = value;}
		}

		public String t301_categoria
		{
			get{return _t301_categoria;}
			set{_t301_categoria = value;}
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

		public String t422_denominacionimportes
		{
			get{return _t422_denominacionimportes;}
			set{_t422_denominacionimportes = value;}
		}

		public Boolean t305_opd
		{
			get{return _t305_opd;}
			set{_t305_opd = value;}
		}

		public Int32 t001_idficepi_visadorcv
		{
			get{return _t001_idficepi_visadorcv;}
			set{_t001_idficepi_visadorcv = value;}
		}

		public String VisadorCV
		{
			get{return _VisadorCV;}
			set{_VisadorCV = value;}
		}

		public Int32 t001_idficepi_interlocutor
		{
			get{return _t001_idficepi_interlocutor;}
			set{_t001_idficepi_interlocutor = value;}
		}

		public String Interlocutor
		{
			get{return _Interlocutor;}
			set{_Interlocutor = value;}
		}

		public String PROFESIONAL_DICENO_CVT
		{
			get{return _PROFESIONAL_DICENO_CVT;}
			set{_PROFESIONAL_DICENO_CVT = value;}
		}

		public DateTime t301_fechano_cvt
		{
			get{return _t301_fechano_cvt;}
			set{_t301_fechano_cvt = value;}
		}

		public String t301_motivono_cvt
		{
			get{return _t301_motivono_cvt;}
			set{_t301_motivono_cvt = value;}
		}


        #endregion

	}
}
