using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PARAMETROCONSULTAPERSONAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T474_PARAMETROCONSULTAPERSONAL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	13/01/2010 15:18:13	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PARAMETROCONSULTAPERSONAL
	{
		#region Propiedades y Atributos

		private int _t472_idconsulta;
		public int t472_idconsulta
		{
			get {return _t472_idconsulta;}
			set { _t472_idconsulta = value ;}
		}

		private string _t474_textoparametro;
		public string t474_textoparametro
		{
			get {return _t474_textoparametro;}
			set { _t474_textoparametro = value ;}
		}

		private string _t474_nombreparametro;
		public string t474_nombreparametro
		{
			get {return _t474_nombreparametro;}
			set { _t474_nombreparametro = value ;}
		}

		private string _t474_tipoparametro;
		public string t474_tipoparametro
		{
			get {return _t474_tipoparametro;}
			set { _t474_tipoparametro = value ;}
		}

		private string _t474_comentarioparametro;
		public string t474_comentarioparametro
		{
			get {return _t474_comentarioparametro;}
			set { _t474_comentarioparametro = value ;}
		}

		private string _t474_valordefecto;
		public string t474_valordefecto
		{
			get {return _t474_valordefecto;}
			set { _t474_valordefecto = value ;}
		}

		private string _t474_visible;
		public string t474_visible
		{
			get {return _t474_visible;}
			set { _t474_visible = value ;}
		}

		private byte _t474_orden;
		public byte t474_orden
		{
			get {return _t474_orden;}
			set { _t474_orden = value ;}
		}

        private bool _t474_opcional;
		public bool t474_opcional
		{
			get {return _t474_opcional;}
			set { _t474_opcional = value ;}
		}

		#endregion

		#region Constructor

		public PARAMETROCONSULTAPERSONAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T474_PARAMETROCONSULTAPERSONAL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 15:18:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t472_idconsulta, string t474_textoparametro, string t474_nombreparametro, string t474_tipoparametro, string t474_comentarioparametro, string t474_valordefecto, string t474_visible, byte t474_orden, bool t474_opcional)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;
			aParam[1] = new SqlParameter("@t474_textoparametro", SqlDbType.Text, 25);
			aParam[1].Value = t474_textoparametro;
			aParam[2] = new SqlParameter("@t474_nombreparametro", SqlDbType.Text, 25);
			aParam[2].Value = t474_nombreparametro;
			aParam[3] = new SqlParameter("@t474_tipoparametro", SqlDbType.Text, 1);
			aParam[3].Value = t474_tipoparametro;
			aParam[4] = new SqlParameter("@t474_comentarioparametro", SqlDbType.Text, 100);
			aParam[4].Value = t474_comentarioparametro;
			aParam[5] = new SqlParameter("@t474_valordefecto", SqlDbType.Text, 7500);
			aParam[5].Value = t474_valordefecto;
			aParam[6] = new SqlParameter("@t474_visible", SqlDbType.Text, 1);
			aParam[6].Value = t474_visible;
			aParam[7] = new SqlParameter("@t474_orden", SqlDbType.TinyInt, 1);
			aParam[7].Value = t474_orden;
            aParam[8] = new SqlParameter("@t474_opcional", SqlDbType.Bit, 1);
            aParam[8].Value = t474_opcional;
            
			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_PARAMETROCONSULTAPERSONAL_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T474_PARAMETROCONSULTAPERSONAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 15:18:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t472_idconsulta, string t474_textoparametro, string t474_nombreparametro, string t474_tipoparametro, string t474_comentarioparametro, string t474_valordefecto, string t474_visible, byte t474_orden, bool t474_opcional)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;
			aParam[1] = new SqlParameter("@t474_textoparametro", SqlDbType.Text, 25);
			aParam[1].Value = t474_textoparametro;
			aParam[2] = new SqlParameter("@t474_nombreparametro", SqlDbType.Text, 25);
			aParam[2].Value = t474_nombreparametro;
			aParam[3] = new SqlParameter("@t474_tipoparametro", SqlDbType.Text, 1);
			aParam[3].Value = t474_tipoparametro;
			aParam[4] = new SqlParameter("@t474_comentarioparametro", SqlDbType.Text, 100);
			aParam[4].Value = t474_comentarioparametro;
			aParam[5] = new SqlParameter("@t474_valordefecto", SqlDbType.Text, 8000);
			aParam[5].Value = t474_valordefecto;
			aParam[6] = new SqlParameter("@t474_visible", SqlDbType.Text, 1);
			aParam[6].Value = t474_visible;
			aParam[7] = new SqlParameter("@t474_orden", SqlDbType.TinyInt, 1);
			aParam[7].Value = t474_orden;
            aParam[8] = new SqlParameter("@t474_opcional", SqlDbType.Bit, 1);
            aParam[8].Value = t474_opcional;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PARAMETROCONSULTAPERSONAL_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T474_PARAMETROCONSULTAPERSONAL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 15:18:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t472_idconsulta, string t474_textoparametro)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;
			aParam[1] = new SqlParameter("@t474_textoparametro", SqlDbType.Text, 25);
			aParam[1].Value = t474_textoparametro;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PARAMETROCONSULTAPERSONAL_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla T474_PARAMETROCONSULTAPERSONAL en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [sqladmin]	13/01/2010 15:18:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByT472_idconsulta(SqlTransaction tr, int t472_idconsulta)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;


			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_PARAMETROCONSULTAPERSONAL_DByT472_idconsulta", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETROCONSULTAPERSONAL_DByT472_idconsulta", aParam);
		}

		#endregion
	}
}
