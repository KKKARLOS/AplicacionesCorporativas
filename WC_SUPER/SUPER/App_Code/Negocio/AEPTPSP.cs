using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AEPTPSP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t355_AEPTPSP
	/// </summary>
	/// <history>
	/// 	Creado por [doarhumi]	08/11/2006 16:55:50	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AEPTPSP
	{

		#region Propiedades y Atributos

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private int _t341_idae;
		public int t341_idae
		{
			get {return _t341_idae;}
			set { _t341_idae = value ;}
		}

		private int _t340_idvae;
		public int t340_idvae
		{
			get {return _t340_idvae;}
			set { _t340_idvae = value ;}
		}
		#endregion

		#region Constructores

		public AEPTPSP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion


		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t355_AEPTPSP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [doarhumi]	08/11/2006 16:55:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t331_idpt , int t341_idae, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = t341_idae;
            aParam[2] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[2].Value = t340_idvae;

            if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_AEPTSUP_I_SNE", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPTSUP_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t355_AEPTPSP.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	08/11/2006 16:55:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t331_idpt, int t341_idae, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = t341_idae;
            aParam[2] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[2].Value = t340_idvae;
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AEPTSUP_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPTSUP_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t355_AEPTPSP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	08/11/2006 16:55:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t331_idpt, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AEPTSUP_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPTSUP_D", aParam);
		}


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t355_AEPTPSP en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [doarhumi]	08/11/2006 16:55:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt340_idvae(int t340_idvae) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[0].Value = t340_idvae;

            return SqlHelper.ExecuteSqlDataReader("SUP_AEPTSUP_SByt340_idvae", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla t355_AEPTPSP,
        /// pasando por parámetro el código de Proyecto Tecnico.
        /// </summary>
        /// <history>
        /// 	Creado por [DOATHUMI]	08/11/2006 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoByPT(int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_AEPTSUP_CByt331_idpt", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_AEPTPSP_CByIdPt", aParam);
        }
        #endregion
	}
}
