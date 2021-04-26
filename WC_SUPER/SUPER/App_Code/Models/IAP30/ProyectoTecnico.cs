using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class ProyectoTecnico
    {

        /// <summary>
        /// Summary description for ProyectoTecnico
        /// </summary>
		#region Private Variables
		private Int32 _t331_idpt;
		private String _t331_despt;
		private Int32 _cod_une;
		private String _t303_denominacion;
		private Int32 _t305_idproyectosubnodo;
		private String _t305_cualidad;
		private Int32 _num_proyecto;
		private String _t301_estado;
		private String _nom_proyecto;
		private Byte _t331_estado;
		private Boolean _t331_obligaest;
		private Int32 _t331_orden;
		private Int32 _t346_idpst;
		private String _t346_codpst;
		private String _t346_despst;
		private Int32 _cod_cliente;
		private String _nom_cliente;
		private String _t331_desptlong;
		private Boolean _t331_heredanodo;
		private Boolean _t331_heredaproyeco;
		private String _t331_acceso_iap;
		private Boolean _t305_admiterecursospst;
		private Boolean _t305_avisorecursopst;
		private String _t305_accesobitacora_pst;
		private Boolean _t301_esreplicable;

		#endregion

		#region Public Properties
		public Int32 t331_idpt
		{
			get{return _t331_idpt;}
			set{_t331_idpt = value;}
		}

		public String t331_despt
		{
			get{return _t331_despt;}
			set{_t331_despt = value;}
		}

		public Int32 cod_une
		{
			get{return _cod_une;}
			set{_cod_une = value;}
		}

		public String t303_denominacion
		{
			get{return _t303_denominacion;}
			set{_t303_denominacion = value;}
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

		public Int32 num_proyecto
		{
			get{return _num_proyecto;}
			set{_num_proyecto = value;}
		}

		public String t301_estado
		{
			get{return _t301_estado;}
			set{_t301_estado = value;}
		}

		public String nom_proyecto
		{
			get{return _nom_proyecto;}
			set{_nom_proyecto = value;}
		}

		public Byte t331_estado
		{
			get{return _t331_estado;}
			set{_t331_estado = value;}
		}

		public Boolean t331_obligaest
		{
			get{return _t331_obligaest;}
			set{_t331_obligaest = value;}
		}

		public Int32 t331_orden
		{
			get{return _t331_orden;}
			set{_t331_orden = value;}
		}

		public Int32 t346_idpst
		{
			get{return _t346_idpst;}
			set{_t346_idpst = value;}
		}

		public String t346_codpst
		{
			get{return _t346_codpst;}
			set{_t346_codpst = value;}
		}

		public String t346_despst
		{
			get{return _t346_despst;}
			set{_t346_despst = value;}
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

		public String t331_desptlong
		{
			get{return _t331_desptlong;}
			set{_t331_desptlong = value;}
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

		public String t331_acceso_iap
		{
			get{return _t331_acceso_iap;}
			set{_t331_acceso_iap = value;}
		}

		public Boolean t305_admiterecursospst
		{
			get{return _t305_admiterecursospst;}
			set{_t305_admiterecursospst = value;}
		}

		public Boolean t305_avisorecursopst
		{
			get{return _t305_avisorecursopst;}
			set{_t305_avisorecursopst = value;}
		}

		public String t305_accesobitacora_pst
		{
			get{return _t305_accesobitacora_pst;}
			set{_t305_accesobitacora_pst = value;}
		}

		public Boolean t301_esreplicable
		{
			get{return _t301_esreplicable;}
			set{_t301_esreplicable = value;}
		}


        #endregion

	}
}
