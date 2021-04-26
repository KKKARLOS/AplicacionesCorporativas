using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para usar StringBuilder
using System.Text;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POOLRPT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T361_POOLRPT
	/// </summary>
	/// <history>
	/// 	Creado por [doarhumi]	20/11/2007 16:25:12	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POOLRPT
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructores

		public POOLRPT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T361_POOLRPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [doarhumi]	20/11/2007 16:25:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_POOLRPT_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOLRPT_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T361_POOLRPT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	20/11/2007 16:25:12
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_POOLRPT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOLRPT_D", aParam);
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T361_POOLRPT en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [doarhumi]	20/11/2007 16:25:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByProyecto(int t305_idproyectosubnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_POOLRPT_SByProyecto", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla T361_POOLRPT en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [doarhumi]	20/11/2007 16:25:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByProyecto(SqlTransaction tr, int t305_idproyectosubnodo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_POOLRPT_DByProyecto", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOLRPT_DByProyecto", aParam);
		}

        public static bool HayPool(int nIdPE)
        {// Devuelve true si hay integrantes del pool de RTPTs
            bool bRes;
            try
            {
                SqlDataReader rdr = POOLRPT.SelectByProyecto(nIdPE);
                if (rdr.Read())
                {
                    bRes = true;
                }
                else
                {
                    bRes = false;
                }
                rdr.Close();
                rdr.Dispose();
                return bRes;
            }
            catch (Exception)
            {
                //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
                return false;
            }
        }

		#endregion
	}
}
