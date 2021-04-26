using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONSNIVELMES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T379_CONSNIVELMES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/05/2008 9:20:20	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONSNIVELMES
	{
		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t380_idNivelConsumo;
		public int t380_idNivelConsumo
		{
			get {return _t380_idNivelConsumo;}
			set { _t380_idNivelConsumo = value ;}
		}

		private decimal _t379_costeunitario;
        public decimal t379_costeunitario
		{
			get {return _t379_costeunitario;}
			set { _t379_costeunitario = value ;}
		}

		private double _t379_unidades;
		public double t379_unidades
		{
			get {return _t379_unidades;}
			set { _t379_unidades = value ;}
		}
		#endregion

		#region Constructores

		public CONSNIVELMES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static void Insert(SqlTransaction tr, int t325_idsegmesproy, int t442_idnivel, decimal t379_costeunitario, double t379_unidades)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
			aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t442_idnivel", SqlDbType.Int, 4);
            aParam[1].Value = t442_idnivel;
			aParam[2] = new SqlParameter("@t379_costeunitario", SqlDbType.Money, 8);
			aParam[2].Value = t379_costeunitario;
			aParam[3] = new SqlParameter("@t379_unidades", SqlDbType.Float, 8);
			aParam[3].Value = t379_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSNIVELMES_I", aParam);
		}
		public static void DeleteByT325_idsegmesproy(SqlTransaction tr, int t325_idsegmesproy)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
			aParam[0].Value = t325_idsegmesproy;

			SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSNIVELMES_DByT325_idsegmesproy", aParam);
		}
        
        public static SqlDataReader Catalogo(int t325_idsegmesproy, string t325_estado, string t422_idmoneda)
		{
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader((t325_estado == "A") ? "SUP_CONSNIVELMES_MESA" : "SUP_CONSNIVELMES_MESC", aParam);
        }

        public static int Delete(SqlTransaction tr, int t325_idsegmesproy, int nIdNivel)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t442_idnivel", SqlDbType.Int, 4);
            aParam[1].Value = nIdNivel;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSNIVELMES_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si existe consumo de un usuario en un mes
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static Nullable<double> GetUnidades(SqlTransaction tr, int nIdSegMesProy, int nIdNivel)
        {
            double? dUnidades = null;
            SqlDataReader dr;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdSegMesProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdNivel", SqlDbType.Int, 4);

            aParam[0].Value = nIdSegMesProy;
            aParam[1].Value = nIdNivel;

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSNIVELMES_UNIDADES_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSNIVELMES_UNIDADES_S", aParam);

            if (dr.Read())
            {
                dUnidades = (double)dr[0];
            }
            dr.Close();
            dr.Dispose();
            return dUnidades;
        }
        public static void UpdateUnidades(SqlTransaction tr, int t325_idsegmesproy, int nIdNivel, double t379_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t442_idnivel", SqlDbType.Int, 4);
            aParam[1].Value = nIdNivel;
            aParam[2] = new SqlParameter("@t379_unidades", SqlDbType.Float, 8);
            aParam[2].Value = t379_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSNIVELMES_UNIDADES_U", aParam);
        }
        #endregion
	}
}
