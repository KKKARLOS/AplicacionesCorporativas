using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : FIGURAS_PROFESI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T707_FIGURAS_PROFESI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/04/2011 16:27:27	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAS_PROFESI
	{
		#region Propiedades y Atributos

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private string _t707_figura;
		public string t707_figura
		{
			get {return _t707_figura;}
			set { _t707_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAS_PROFESI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T707_FIGURAS_PROFESI.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	07/04/2011 16:27:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t001_idficepi, string t707_figura, string t001_facturacion)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t707_figura", SqlDbType.Text, 2);
            aParam[1].Value = t707_figura;
            aParam[2] = new SqlParameter("@t001_facturacion", SqlDbType.Text, 1);
            aParam[2].Value = t001_facturacion;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GEM_FIGURAS_PROFESI_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_FIGURAS_PROFESI_I", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T707_FIGURAS_PROFESI a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	07/04/2011 16:27:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t001_idficepi, string t707_figura)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t707_figura", SqlDbType.Char, 2);
            aParam[1].Value = t707_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_FIGURAS_PROFESI_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_FIGURAS_PROFESI_D", aParam);
        }
        public static int DeleteALL(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_FIGURAS_PROFESI_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_FIGURAS_PROFESI_D2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T707_FIGURAS_PROFESI.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	07/04/2011 16:27:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GEM_FIGURAS_PROFESI_CF", aParam);
        }
        #endregion
    }
}
