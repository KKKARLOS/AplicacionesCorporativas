using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TABLASAUDITSUPER
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T300_TABLASAUDITSUPER
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/01/2010 8:54:49	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TABLASAUDITSUPER
	{
		#region Propiedades y Atributos

		private string _t300_tabla;
		public string t300_tabla
		{
			get {return _t300_tabla;}
			set { _t300_tabla = value ;}
		}

		private bool _t300_auditar;
		public bool t300_auditar
		{
			get {return _t300_auditar;}
			set { _t300_auditar = value ;}
		}

		private int? _t001_idficepi;
		public int? t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}
		#endregion

		#region Constructor

        public TABLASAUDITSUPER()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }
        public TABLASAUDITSUPER(string t300_tabla, bool t300_auditar, int? t001_idficepi)
        {
            this.t300_tabla = t300_tabla;
            this.t300_auditar = t300_auditar;
            this.t001_idficepi = t001_idficepi;
        }

		#endregion

        #region Metodos

        public static SqlDataReader ObtenerTablas(int nOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_CAMPOS", aParam);
        }

        public static SqlDataReader CatalogoTablas()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TABLASAUDITSUPER_C", aParam);
        }

        public static int Update(SqlTransaction tr, string t300_tabla, bool t300_auditar)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t300_tabla", SqlDbType.VarChar, 50);
            aParam[0].Value = t300_tabla;

            aParam[1] = new SqlParameter("@t300_auditar", SqlDbType.Bit, 1);
            aParam[1].Value = t300_auditar;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TABLASAUDITSUPER_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TABLASAUDITSUPER_U", aParam);
        }
        #endregion
    }
}
