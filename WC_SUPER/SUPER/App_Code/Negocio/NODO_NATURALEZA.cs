using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : NODO_NATURALEZA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T471_NODO_NATURALEZA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	06/04/2009 9:11:52	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class NODO_NATURALEZA
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T471_NODO_NATURALEZA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	06/04/2009 9:11:52
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t303_idnodo, int t323_idnaturaleza, bool t471_replicaPIG,
                                 bool t471_heredanodo, bool t471_imputableGASVI,
                                 int t314_idusuario_responsable, Nullable<int> t001_idficepi_visador)
		{
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@t323_idnaturaleza", SqlDbType.Int, 4, t323_idnaturaleza),
                ParametroSql.add("@t471_replicaPIG", SqlDbType.Bit, 1, t471_replicaPIG),
                ParametroSql.add("@t471_heredanodo", SqlDbType.Bit, 1, t471_heredanodo),
                ParametroSql.add("@t471_imputableGASVI", SqlDbType.Bit, 1, t471_imputableGASVI),
                ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable),
                ParametroSql.add("@t001_idficepi_visador", SqlDbType.Int, 4, t001_idficepi_visador)
            };

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_NODO_NATURALEZA_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_NATURALEZA_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T471_NODO_NATURALEZA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	06/04/2009 9:11:52
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int DeleteAll(SqlTransaction tr)
		{
			SqlParameter[] aParam = new SqlParameter[0];
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NODO_NATURALEZA_DALL", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_NATURALEZA_DALL", aParam);
		}

		#endregion
	}
}
