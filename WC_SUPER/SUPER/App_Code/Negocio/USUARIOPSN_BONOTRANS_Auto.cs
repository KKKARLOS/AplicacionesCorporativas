using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIOPSN_BONOTRANS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T665_USUARIOPSN_BONOTRANS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	05/05/2011 17:40:12	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUARIOPSN_BONOTRANS
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t655_idbono;
		public int t655_idbono
		{
			get {return _t655_idbono;}
			set { _t655_idbono = value ;}
		}

		private int? _t665_anomesdesde;
		public int? t665_anomesdesde
		{
			get {return _t665_anomesdesde;}
			set { _t665_anomesdesde = value ;}
		}

		private int? _t665_anomeshasta;
		public int? t665_anomeshasta
		{
			get {return _t665_anomeshasta;}
			set { _t665_anomeshasta = value ;}
		}

		private string _t665_comentario;
		public string t665_comentario
		{
			get {return _t665_comentario;}
			set { _t665_comentario = value ;}
		}
		#endregion

		#region Constructor

		public USUARIOPSN_BONOTRANS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T665_USUARIOPSN_BONOTRANS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	05/05/2011 17:40:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo , int t314_idusuario , int t655_idbono , Nullable<int> t665_anomesdesde , Nullable<int> t665_anomeshasta)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t655_idbono", SqlDbType.Int, 4);
			aParam[2].Value = t655_idbono;
			aParam[3] = new SqlParameter("@t665_anomesdesde", SqlDbType.Int, 4);
			aParam[3].Value = t665_anomesdesde;
			aParam[4] = new SqlParameter("@t665_anomeshasta", SqlDbType.Int, 4);
			aParam[4].Value = t665_anomeshasta;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_USUARIOPSN_BONOTRANS_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPSN_BONOTRANS_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T665_USUARIOPSN_BONOTRANS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/05/2011 17:40:12
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, int t655_idbono, int t655_idbono_new, Nullable<int> t665_anomesdesde, Nullable<int> t665_anomeshasta)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t655_idbono", SqlDbType.Int, 4);
            aParam[2].Value = t655_idbono;
            aParam[3] = new SqlParameter("@t655_idbono_new", SqlDbType.Int, 4);
            aParam[3].Value = t655_idbono_new;
            aParam[4] = new SqlParameter("@t665_anomesdesde", SqlDbType.Int, 4);
			aParam[4].Value = t665_anomesdesde;
			aParam[5] = new SqlParameter("@t665_anomeshasta", SqlDbType.Int, 4);
			aParam[5].Value = t665_anomeshasta;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIOPSN_BONOTRANS_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPSN_BONOTRANS_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T665_USUARIOPSN_BONOTRANS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/05/2011 17:40:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, int t655_idbono)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t655_idbono", SqlDbType.Int, 4);
			aParam[2].Value = t655_idbono;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIOPSN_BONOTRANS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPSN_BONOTRANS_D", aParam);
		}


		#endregion
	}
}
