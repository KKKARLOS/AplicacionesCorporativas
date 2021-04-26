using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : CECRESTRICCION
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T319_CECRESTRICCION
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	21/08/2009 13:16:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CECRESTRICCION
	{
		#region Propiedades y Atributos

		private int _t345_idcec;
		public int t345_idcec
		{
			get {return _t345_idcec;}
			set { _t345_idcec = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t435_idvcec;
		public int t435_idvcec
		{
			get {return _t435_idvcec;}
			set { _t435_idvcec = value ;}
		}
		#endregion

		#region Constructor

		public CECRESTRICCION() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T319_CECRESTRICCION.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	21/08/2009 13:16:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t345_idcec, int t303_idnodo , int t435_idvcec)
		{
			SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
			aParam[2].Value = t435_idvcec;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CECRESTRICCION_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECRESTRICCION_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T319_CECRESTRICCION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	21/08/2009 13:16:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t345_idcec, int t303_idnodo, int t435_idvcec)
		{
			SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
			aParam[2].Value = t435_idvcec;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CECRESTRICCION_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECRESTRICCION_D", aParam);
		}

		#endregion
	}
}
