using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PRODUCFACTPROF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T433_PRODUCFACTPROF
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/06/2008 15:51:42	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PRODUCFACTPROF
	{

		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private int _t314_idrecurso;
		public int t314_idrecurso
		{
			get {return _t314_idrecurso;}
			set { _t314_idrecurso = value ;}
		}

		private int _t333_idperfilproy;
		public int t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}

		private double _t433_unidades;
		public double t433_unidades
		{
			get {return _t433_unidades;}
			set { _t433_unidades = value ;}
		}
		#endregion


		#region Constructores

		public PRODUCFACTPROF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion


		#region Metodos



        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T433_PRODUCFACTPROF a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/03/2010 11:16:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t325_idsegmesproy, int t332_idtarea, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PRODUCFACTPROF_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPROF_D", aParam);
        }

        #endregion
	}
}
