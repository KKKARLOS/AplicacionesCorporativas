using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AUDITSUPER
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T499_AUDITSUPER
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	12/02/2010 13:51:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AUDITSUPER
	{
		#region Propiedades y Atributos

		private int _t499_idaudit;
		public int t499_idaudit
		{
			get {return _t499_idaudit;}
			set { _t499_idaudit = value ;}
		}

		private string _t300_tabla;
		public string t300_tabla
		{
			get {return _t300_tabla;}
			set { _t300_tabla = value ;}
		}

		private string _t498_atributodentecnica;
		public string t498_atributodentecnica
		{
			get {return _t498_atributodentecnica;}
			set { _t498_atributodentecnica = value ;}
		}

		private string _t499_accion;
		public string t499_accion
		{
			get {return _t499_accion;}
			set { _t499_accion = value ;}
		}

		private string _t499_que;
		public string t499_que
		{
			get {return _t499_que;}
			set { _t499_que = value ;}
		}

		private int? _t001_idficepi_quien;
		public int? t001_idficepi_quien
		{
			get {return _t001_idficepi_quien;}
			set { _t001_idficepi_quien = value ;}
		}

		private DateTime _t499_cuando;
		public DateTime t499_cuando
		{
			get {return _t499_cuando;}
			set { _t499_cuando = value ;}
		}

		private string _t499_valorantiguo;
		public string t499_valorantiguo
		{
			get {return _t499_valorantiguo;}
			set { _t499_valorantiguo = value ;}
		}

		private string _t499_valornuevo;
		public string t499_valornuevo
		{
			get {return _t499_valornuevo;}
			set { _t499_valornuevo = value ;}
		}

		private string _t499_usuario_sistema;
		public string t499_usuario_sistema
		{
			get {return _t499_usuario_sistema;}
			set { _t499_usuario_sistema = value ;}
		}

		private int? _t301_idproyecto;
		public int? t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private int? _t305_idproyectosubnodo;
		public int? t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private string _t499_hostname;
		public string t499_hostname
		{
			get {return _t499_hostname;}
			set { _t499_hostname = value ;}
		}

		private string _t499_id1;
		public string t499_id1
		{
			get {return _t499_id1;}
			set { _t499_id1 = value ;}
		}

		private string _t499_id2;
		public string t499_id2
		{
			get {return _t499_id2;}
			set { _t499_id2 = value ;}
		}

		private string _t499_id3;
		public string t499_id3
		{
			get {return _t499_id3;}
			set { _t499_id3 = value ;}
		}

		private string _t499_aplicacion;
		public string t499_aplicacion
		{
			get {return _t499_aplicacion;}
			set { _t499_aplicacion = value ;}
		}
		#endregion

		#region Constructor

		public AUDITSUPER() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T499_AUDITSUPER,
		/// y devuelve una instancia u objeto del tipo AUDITSUPER
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	12/02/2010 13:51:54
		/// </history>
		/// -----------------------------------------------------------------------------
		/*
        public static AUDITSUPER Select(SqlTransaction tr, int t499_idaudit) 
		{
			AUDITSUPER o = new AUDITSUPER();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t499_idaudit", SqlDbType.Int, 4);
			aParam[0].Value = t499_idaudit;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_AUDITSUPER_S", aParam);

			if (dr.Read())
			{
				if (dr["t499_idaudit"] != DBNull.Value)
					o.t499_idaudit = int.Parse(dr["t499_idaudit"].ToString());
				if (dr["t300_tabla"] != DBNull.Value)
					o.t300_tabla = (string)dr["t300_tabla"];
				if (dr["t498_atributodentecnica"] != DBNull.Value)
					o.t498_atributodentecnica = (string)dr["t498_atributodentecnica"];
				if (dr["t499_accion"] != DBNull.Value)
					o.t499_accion = (string)dr["t499_accion"];
				if (dr["t499_que"] != DBNull.Value)
					o.t499_que = (string)dr["t499_que"];
				if (dr["t001_idficepi_quien"] != DBNull.Value)
					o.t001_idficepi_quien = int.Parse(dr["t001_idficepi_quien"].ToString());
				if (dr["t499_cuando"] != DBNull.Value)
					o.t499_cuando = (DateTime)dr["t499_cuando"];
				if (dr["t499_valorantiguo"] != DBNull.Value)
					o.t499_valorantiguo = (string)dr["t499_valorantiguo"];
				if (dr["t499_valornuevo"] != DBNull.Value)
					o.t499_valornuevo = (string)dr["t499_valornuevo"];
				if (dr["t499_usuario_sistema"] != DBNull.Value)
					o.t499_usuario_sistema = (string)dr["t499_usuario_sistema"];
				if (dr["t301_idproyecto"] != DBNull.Value)
					o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
				if (dr["t305_idproyectosubnodo"] != DBNull.Value)
					o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
				if (dr["t499_hostname"] != DBNull.Value)
					o.t499_hostname = (string)dr["t499_hostname"];
				if (dr["t499_id1"] != DBNull.Value)
					o.t499_id1 = (string)dr["t499_id1"];
				if (dr["t499_id2"] != DBNull.Value)
					o.t499_id2 = (string)dr["t499_id2"];
				if (dr["t499_id3"] != DBNull.Value)
					o.t499_id3 = (string)dr["t499_id3"];
				if (dr["t499_aplicacion"] != DBNull.Value)
					o.t499_aplicacion = (string)dr["t499_aplicacion"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de AUDITSUPER"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}
        */
		#endregion
	}
}
