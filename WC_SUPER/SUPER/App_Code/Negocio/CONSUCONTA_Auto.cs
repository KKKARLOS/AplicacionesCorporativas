using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONSUCONTA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T478_CONSUCONTA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/01/2010 16:49:27	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONSUCONTA
	{
		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t315_idproveedor;
		public int t315_idproveedor
		{
			get {return _t315_idproveedor;}
			set { _t315_idproveedor = value ;}
		}

		private int _t478_nconsumo;
		public int t478_nconsumo
		{
			get {return _t478_nconsumo;}
			set { _t478_nconsumo = value ;}
		}

		private decimal _t478_importe;
		public decimal t478_importe
		{
			get {return _t478_importe;}
			set { _t478_importe = value ;}
		}

		private int _t329_idclaseeco;
		public int t329_idclaseeco
		{
			get {return _t329_idclaseeco;}
			set { _t329_idclaseeco = value ;}
		}

		private int _t313_idempresa;
		public int t313_idempresa
		{
			get {return _t313_idempresa;}
			set { _t313_idempresa = value ;}
		}

		private int _t478_ndocumento;
		public int t478_ndocumento
		{
			get {return _t478_ndocumento;}
			set { _t478_ndocumento = value ;}
		}

		private string _t478_descripcion;
		public string t478_descripcion
		{
			get {return _t478_descripcion;}
			set { _t478_descripcion = value ;}
		}
		#endregion

		#region Constructor

		public CONSUCONTA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T478_CONSUCONTA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/01/2010 16:49:27
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t325_idsegmesproy , int t315_idproveedor , int t478_nconsumo , decimal t478_importe , int t329_idclaseeco , int t313_idempresa , int t478_ndocumento , string t478_descripcion)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
			aParam[0].Value = t325_idsegmesproy;
			aParam[1] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
			aParam[1].Value = t315_idproveedor;
			aParam[2] = new SqlParameter("@t478_nconsumo", SqlDbType.Int, 4);
			aParam[2].Value = t478_nconsumo;
			aParam[3] = new SqlParameter("@t478_importe", SqlDbType.Money, 8);
			aParam[3].Value = t478_importe;
			aParam[4] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
			aParam[4].Value = t329_idclaseeco;
			aParam[5] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
			aParam[5].Value = t313_idempresa;
			aParam[6] = new SqlParameter("@t478_ndocumento", SqlDbType.Int, 4);
			aParam[6].Value = t478_ndocumento;
			aParam[7] = new SqlParameter("@t478_descripcion", SqlDbType.Text, 50);
			aParam[7].Value = t478_descripcion;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CONSUCONTA_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUCONTA_I", aParam);
		}

		#endregion
	}
}
