using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : RTPT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t354_RTPT
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class RTPT
	{
		#region Propiedades y Atributos

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructores

		public RTPT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos Básicos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t354_RTPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t331_idpt , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_RTPT_I_SNE", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RTPT_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t354_RTPT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t331_idpt, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_RTPT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RTPT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t354_RTPT,
		/// y devuelve una instancia u objeto del tipo RTPT
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static RTPT Select(SqlTransaction tr, int t331_idpt, int t314_idusuario) 
		{
			RTPT o = new RTPT();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_RTPT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_RTPT_S", aParam);

			if (dr.Read())
			{
				if (dr["t331_idpt"] != DBNull.Value)
					o.t331_idpt = (int)dr["t331_idpt"];
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = (int)dr["t314_idusuario"];

            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de RTPT"));
			}

            dr.Close();
            dr.Dispose();
            
            return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t354_RTPT en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt331_idpt(int t331_idpt) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;

            return SqlHelper.ExecuteSqlDataReader("SUP_RTPT_SByt331_idpt", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla t354_RTPT en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByt331_idpt(SqlTransaction tr, int t331_idpt)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;

		    if (tr == null)
			    SqlHelper.ExecuteNonQuery("SUP_RTPT_DByt331_idpt", aParam);
		    else
			    SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RTPT_DByt331_idpt", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla t354_RTPT.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2007 17:05:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t331_idpt, Nullable<int> t314_idusuario, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_RTPT_C", aParam);
        }
        #endregion

        #region Metodos

        public static SqlDataReader SelectMailByt331_idpt(SqlTransaction tr, int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_RTPTMAIL_SByt331_idpt", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_RTPTMAIL_SByt331_idpt", aParam);
        }
        public static bool ExisteRTPT(SqlTransaction tr, int t331_idpt, int t314_idusuario)
        {
            bool bRes;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_RTPT_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_RTPT_S", aParam);

            if (dr.Read())
            {
                bRes = true;

            }
            else
            {
                bRes = false;
            }

            dr.Close();
            dr.Dispose();

            return bRes;
        }

        #endregion
	}
}
