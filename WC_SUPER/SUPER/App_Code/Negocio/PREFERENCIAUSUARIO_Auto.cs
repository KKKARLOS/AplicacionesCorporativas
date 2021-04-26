using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PREFERENCIAUSUARIO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T462_PREFERENCIAUSUARIO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/02/2009 16:23:39	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PREFERENCIAUSUARIO
	{
		#region Propiedades y Atributos

		private int _t462_idPrefUsuario;
		public int t462_idPrefUsuario
		{
			get {return _t462_idPrefUsuario;}
			set { _t462_idPrefUsuario = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private short _t463_idpantalla;
		public short t463_idpantalla
		{
			get {return _t463_idpantalla;}
			set { _t463_idpantalla = value ;}
		}

		private string _t462_denominacion;
		public string t462_denominacion
		{
			get {return _t462_denominacion;}
			set { _t462_denominacion = value ;}
		}

		private bool _t462_defecto;
		public bool t462_defecto
		{
			get {return _t462_defecto;}
			set { _t462_defecto = value ;}
		}

		private string _t462_p1;
		public string t462_p1
		{
			get {return _t462_p1;}
			set { _t462_p1 = value ;}
		}

		private string _t462_p2;
		public string t462_p2
		{
			get {return _t462_p2;}
			set { _t462_p2 = value ;}
		}

		private string _t462_p3;
		public string t462_p3
		{
			get {return _t462_p3;}
			set { _t462_p3 = value ;}
		}

		private string _t462_p4;
		public string t462_p4
		{
			get {return _t462_p4;}
			set { _t462_p4 = value ;}
		}

		private string _t462_p5;
		public string t462_p5
		{
			get {return _t462_p5;}
			set { _t462_p5 = value ;}
		}

		private string _t462_p6;
		public string t462_p6
		{
			get {return _t462_p6;}
			set { _t462_p6 = value ;}
		}

		private string _t462_p7;
		public string t462_p7
		{
			get {return _t462_p7;}
			set { _t462_p7 = value ;}
		}

		private string _t462_p8;
		public string t462_p8
		{
			get {return _t462_p8;}
			set { _t462_p8 = value ;}
		}

		private string _t462_p9;
		public string t462_p9
		{
			get {return _t462_p9;}
			set { _t462_p9 = value ;}
		}

		private string _t462_p10;
		public string t462_p10
		{
			get {return _t462_p10;}
			set { _t462_p10 = value ;}
		}

		private string _t462_p11;
		public string t462_p11
		{
			get {return _t462_p11;}
			set { _t462_p11 = value ;}
		}

		private string _t462_p12;
		public string t462_p12
		{
			get {return _t462_p12;}
			set { _t462_p12 = value ;}
		}

		private string _t462_p13;
		public string t462_p13
		{
			get {return _t462_p13;}
			set { _t462_p13 = value ;}
		}

		private string _t462_p14;
		public string t462_p14
		{
			get {return _t462_p14;}
			set { _t462_p14 = value ;}
		}

		private string _t462_p15;
		public string t462_p15
		{
			get {return _t462_p15;}
			set { _t462_p15 = value ;}
		}
		#endregion

		#region Constructor

		public PREFERENCIAUSUARIO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T462_PREFERENCIAUSUARIO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/02/2009 16:23:39
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_D", aParam);
        }
        public static int DeleteCVT(SqlTransaction tr, int t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIOCVT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIOCVT_D", aParam);
        }

		#endregion
	}
}
