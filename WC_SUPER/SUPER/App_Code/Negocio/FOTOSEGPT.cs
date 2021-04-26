using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : FOTOSEGPT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T374_FOTOSEGPT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/05/2008 12:40:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FOTOSEGPT
	{
		#region Propiedades y Atributos

		private int _T373_idFotoPE;
		public int T373_idFotoPE
		{
			get {return _T373_idFotoPE;}
			set { _T373_idFotoPE = value ;}
		}

		private string _T374_denominacion;
		public string T374_denominacion
		{
			get {return _T374_denominacion;}
			set { _T374_denominacion = value ;}
		}

		private int _T374_idFotoPT;
		public int T374_idFotoPT
		{
			get {return _T374_idFotoPT;}
			set { _T374_idFotoPT = value ;}
		}
		#endregion

		#region Constructores

		public FOTOSEGPT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T374_FOTOSEGPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/05/2008 12:40:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int T373_idFotoPE , string T374_denominacion, Nullable<decimal> t374_presupuesto, Nullable<double> t374_porcav, Nullable<decimal> t374_producido)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@T373_idFotoPE", SqlDbType.Int, 4);
			aParam[0].Value = T373_idFotoPE;
			aParam[1] = new SqlParameter("@T374_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = T374_denominacion;
            aParam[2] = new SqlParameter("@T374_presupuesto", SqlDbType.Money, 8);
            aParam[2].Value = t374_presupuesto;
            aParam[3] = new SqlParameter("@T374_porcav", SqlDbType.Float, 8);
            aParam[3].Value = t374_porcav;
            aParam[4] = new SqlParameter("@T374_producido", SqlDbType.Money, 8);
            aParam[4].Value = t374_producido;            

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FOTOSEGPT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FOTOSEGPT_I", aParam));
		}      

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T374_FOTOSEGPT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	29/05/2008 12:40:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int T373_idFotoPE, string T374_denominacion, Nullable<int> T374_idFotoPT, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@T373_idFotoPE", SqlDbType.Int, 4);
			aParam[0].Value = T373_idFotoPE;
			aParam[1] = new SqlParameter("@T374_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = T374_denominacion;
			aParam[2] = new SqlParameter("@T374_idFotoPT", SqlDbType.Int, 4);
			aParam[2].Value = T374_idFotoPT;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOSEGPT_C", aParam);
		}
         
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los proyectos técnicos de un proyecto económico de una foto de seguimiento de proyecto
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoPT(int T373_idFotoPE, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@T373_idFotoPE", SqlDbType.Int, 4, T373_idFotoPE);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOSEGPT_CATA", aParam);
        }
        /// <summary>
        /// 
        /// Obtiene los datos de la instantánea, a nivel de fases + actividades + tareas.
        /// </summary> 
        public static SqlDataReader DatosFaseActivTareas(int nPT, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nPT", SqlDbType.Int, 4, nPT);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOSEGPT_CATA2", aParam);
        }

        #endregion
	}
}
