using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POSICIONFACORIGEN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T620_POSICIONFACORIGEN
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	17/09/2010 12:35:39	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POSICIONFACORIGEN
	{
		#region Propiedades y Atributos

		private int _T619_idordenfac;
		public int T619_idordenfac
		{
			get {return _T619_idordenfac;}
			set { _T619_idordenfac = value ;}
		}

		private int _T620_posicion;
		public int T620_posicion
		{
			get {return _T620_posicion;}
			set { _T620_posicion = value ;}
		}

		private string _T620_descripcion;
		public string T620_descripcion
		{
			get {return _T620_descripcion;}
			set { _T620_descripcion = value ;}
		}

		private float _T620_unidades;
		public float T620_unidades
		{
			get {return _T620_unidades;}
			set { _T620_unidades = value ;}
		}

		private decimal _T620_preciounitario;
		public decimal T620_preciounitario
		{
			get {return _T620_preciounitario;}
			set { _T620_preciounitario = value ;}
		}
		#endregion

		#region Constructor

		public POSICIONFACORIGEN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T620_POSICIONFACORIGEN,
		/// y devuelve una instancia u objeto del tipo POSICIONFACORIGEN
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	17/09/2010 12:35:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static POSICIONFACORIGEN Select(SqlTransaction tr, int T619_idordenfac, int T620_posicion) 
		{
			POSICIONFACORIGEN o = new POSICIONFACORIGEN();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@T619_idordenfac", SqlDbType.Int, 4);
			aParam[0].Value = T619_idordenfac;
			aParam[1] = new SqlParameter("@T620_posicion", SqlDbType.Int, 4);
			aParam[1].Value = T620_posicion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_POSICIONFACORIGEN_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_POSICIONFACORIGEN_S", aParam);

			if (dr.Read())
			{
				if (dr["T619_idordenfac"] != DBNull.Value)
					o.T619_idordenfac = int.Parse(dr["T619_idordenfac"].ToString());
				if (dr["T620_posicion"] != DBNull.Value)
					o.T620_posicion = int.Parse(dr["T620_posicion"].ToString());
				if (dr["T620_descripcion"] != DBNull.Value)
					o.T620_descripcion = (string)dr["T620_descripcion"];
				if (dr["T620_unidades"] != DBNull.Value)
					o.T620_unidades = float.Parse(dr["T620_unidades"].ToString());
				if (dr["T620_preciounitario"] != DBNull.Value)
					o.T620_preciounitario = decimal.Parse(dr["T620_preciounitario"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de POSICIONFACORIGEN"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
