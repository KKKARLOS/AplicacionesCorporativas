using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class SALPROV
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t315_idproveedor;
		public int t315_idproveedor
		{
			get {return _t315_idproveedor;}
			set { _t315_idproveedor = value ;}
		}

		private int _t329_idclaseeco;
		public int t329_idclaseeco
		{
			get {return _t329_idclaseeco;}
			set { _t329_idclaseeco = value ;}
		}

		private decimal _t479_importe;
		public decimal t479_importe
		{
			get {return _t479_importe;}
			set { _t479_importe = value ;}
		}
		#endregion

		#region Constructor

		public SALPROV() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T479_SALPROV.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	27/01/2010 11:20:15
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo , int t315_idproveedor , int t329_idclaseeco , decimal t479_importe)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
			aParam[1].Value = t315_idproveedor;
			aParam[2] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
			aParam[2].Value = t329_idclaseeco;
			aParam[3] = new SqlParameter("@t479_importe", SqlDbType.Money, 8);
			aParam[3].Value = t479_importe;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_SALPROV_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SALPROV_I", aParam);
		}
		#endregion
	}
}
