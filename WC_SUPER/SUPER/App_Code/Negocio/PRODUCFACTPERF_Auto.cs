using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PRODUCFACTPERF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T444_PRODUCFACTPERF
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	06/05/2009 16:24:55	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PRODUCFACTPERF
	{
		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t333_idperfilproy;
		public int t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}

        private decimal _t444_imptarifamc;
        public decimal t444_imptarifamc
		{
			get {return _t444_imptarifamc;}
			set { _t444_imptarifamc = value ;}
		}

		private double _t444_unidades;
		public double t444_unidades
		{
			get {return _t444_unidades;}
			set { _t444_unidades = value ;}
		}
		#endregion

		#region Constructor

		public PRODUCFACTPERF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		#endregion
	}
}
