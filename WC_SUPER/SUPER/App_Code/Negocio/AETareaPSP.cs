using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : AETAREAPSP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t351_AETAREAPSP
	/// </summary>
	/// <history>
	/// 	Creado por [DOTOFEAN]	27/09/2006 17:04:24	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AETAREAPSP
	{

		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
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

		public AETAREAPSP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t351_AETAREAPSP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOTOFEAN]	27/09/2006 17:04:24
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t332_idtarea , int t341_idae, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = t341_idae;
            aParam[2] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[2].Value = t340_idvae;

            if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_AETAREAPSP_I_SNE", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AETAREAPSP_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t351_AETAREAPSP.
		/// </summary>
		/// <history>
		/// 	Creado por [DOTOFEAN]	27/09/2006 17:04:24
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t332_idtarea, int t341_idae, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = t341_idae;
            aParam[2] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[2].Value = t340_idvae;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AETAREAPSP_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AETAREAPSP_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t351_AETAREAPSP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOTOFEAN]	27/09/2006 17:04:24
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Delete(SqlTransaction tr, int t332_idtarea, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_AETAREAPSP_D", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AETAREAPSP_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t351_AETAREAPSP en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOTOFEAN]	27/09/2006 17:04:24
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader  SelectByt332_idtarea(int t332_idtarea) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;

            return SqlHelper.ExecuteSqlDataReader("SUP_AETAREAPSP_SByt332_idtarea", aParam);
		}


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina AE de tarea que ya figuren asociados al PT
        /// Elimina los registros de la tabla t351_AETAREAPSP que ta estén en la T055_AEPTPSP.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	22/05/2007
        /// Se comentariza pus se sustituye por triggers
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static void DeleteDuplicados(SqlTransaction tr, int t332_idtarea, int t331_idpt)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
        //    aParam[0].Value = t331_idpt;
        //    aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
        //    aParam[1].Value = t332_idtarea;

        //    int returnValue;
        //    if (tr == null)
        //        returnValue = SqlHelper.ExecuteNonQuery("SUP_AE_DUPLI_D", aParam);
        //    else
        //        returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_DUPLI_D", aParam);
        //}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla t351_AETAREAPSP,
        /// pasando por parámetro el código de tarea.
        /// </summary>
        /// <history>
        /// 	Creado por [DOTOFEAN]	02/10/2006 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoByTarea(int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AETAREAPSP_CByt332_idtarea", aParam);
        }

        /// <summary>
        /// 
        /// Duplica los datos básicos de la relación Tarea/atributo estadístico de una tarea,
        /// correspondientes a la tabla t351_AETAREAPSP,
        /// Tambien duplica los grupos funcionales del pool
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int DuplicarAEs(SqlTransaction tr, int nIdTareaAnt, int nIdTareaAct)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTareaAnt", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdTareaAct", SqlDbType.Int, 4);

            aParam[0].Value = nIdTareaAnt;
            aParam[1].Value = nIdTareaAct;

            int nResul = 0;
            if (tr == null)
                nResul = SqlHelper.ExecuteNonQuery("SUP_TAREA_AE_DUP", aParam);
            else
                nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREA_AE_DUP", aParam);

            return nResul;
        }

        public static SqlDataReader CatalogoByPT(int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AETAREAPSP_CByt331_idpt", aParam);
        }

        #endregion
    }
}
