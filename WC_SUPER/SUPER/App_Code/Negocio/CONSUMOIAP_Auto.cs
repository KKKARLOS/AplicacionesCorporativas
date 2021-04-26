using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONSUMOIAP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T337_CONSUMOIAP
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	06/10/2008 12:24:04	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONSUMOIAP
	{
		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private DateTime _t337_fecha;
		public DateTime t337_fecha
		{
			get {return _t337_fecha;}
			set { _t337_fecha = value ;}
		}

		private float _t337_esfuerzo;
		public float t337_esfuerzo
		{
			get {return _t337_esfuerzo;}
			set { _t337_esfuerzo = value ;}
		}

		private double _t337_esfuerzoenjor;
		public double t337_esfuerzoenjor
		{
			get {return _t337_esfuerzoenjor;}
			set { _t337_esfuerzoenjor = value ;}
		}

		private string _t337_comentario;
		public string t337_comentario
		{
			get {return _t337_comentario;}
			set { _t337_comentario = value ;}
		}

		private DateTime _t337_fecmodif;
		public DateTime t337_fecmodif
		{
			get {return _t337_fecmodif;}
			set { _t337_fecmodif = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}
		#endregion

		#region Constructor

		public CONSUMOIAP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T337_CONSUMOIAP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	06/10/2008 12:24:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t332_idtarea , int t314_idusuario , DateTime t337_fecha , float t337_esfuerzo , double t337_esfuerzoenjor , string t337_comentario , DateTime t337_fecmodif , int t314_idusuario_modif)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t337_fecha", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t337_fecha;
			aParam[3] = new SqlParameter("@t337_esfuerzo", SqlDbType.Real, 4);
			aParam[3].Value = t337_esfuerzo;
			aParam[4] = new SqlParameter("@t337_esfuerzoenjor", SqlDbType.Float, 8);
			aParam[4].Value = t337_esfuerzoenjor;
            aParam[5] = new SqlParameter("@t337_comentario", SqlDbType.VarChar, 7500);
			aParam[5].Value = t337_comentario;
			aParam[6] = new SqlParameter("@t337_fecmodif", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t337_fecmodif;
			aParam[7] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
			aParam[7].Value = t314_idusuario_modif;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CONSUMOIAP_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUMOIAP_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T337_CONSUMOIAP.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	06/10/2008 12:24:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t332_idtarea, int t314_idusuario, DateTime t337_fecha, float t337_esfuerzo, double t337_esfuerzoenjor, string t337_comentario, DateTime t337_fecmodif, int t314_idusuario_modif)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t337_fecha", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t337_fecha;
			aParam[3] = new SqlParameter("@t337_esfuerzo", SqlDbType.Real, 4);
			aParam[3].Value = t337_esfuerzo;
			aParam[4] = new SqlParameter("@t337_esfuerzoenjor", SqlDbType.Float, 8);
			aParam[4].Value = t337_esfuerzoenjor;
            aParam[5] = new SqlParameter("@t337_comentario", SqlDbType.VarChar, 7500);
			aParam[5].Value = t337_comentario;
			aParam[6] = new SqlParameter("@t337_fecmodif", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t337_fecmodif;
			aParam[7] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
			aParam[7].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONSUMOIAP_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUMOIAP_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T337_CONSUMOIAP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	06/10/2008 12:24:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t332_idtarea, int t314_idusuario, DateTime t337_fecha)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t337_fecha", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t337_fecha;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONSUMOIAP_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUMOIAP_D", aParam);
		}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T337_CONSUMOIAP,
        /// y devuelve una instancia u objeto del tipo CONSUMOIAP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/02/2011 8:33:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static CONSUMOIAP Select(SqlTransaction tr, int t332_idtarea, int t314_idusuario, DateTime t337_fecha)
        {
            CONSUMOIAP o = new CONSUMOIAP();

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t337_fecha", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t337_fecha;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAP_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSUMOIAP_S", aParam);

            if (dr.Read())
            {
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = int.Parse(dr["t332_idtarea"].ToString());
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t337_fecha"] != DBNull.Value)
                    o.t337_fecha = (DateTime)dr["t337_fecha"];
                if (dr["t337_esfuerzo"] != DBNull.Value)
                    o.t337_esfuerzo = float.Parse(dr["t337_esfuerzo"].ToString());
                if (dr["t337_esfuerzoenjor"] != DBNull.Value)
                    o.t337_esfuerzoenjor = double.Parse(dr["t337_esfuerzoenjor"].ToString());
                if (dr["t337_comentario"] != DBNull.Value)
                    o.t337_comentario = (string)dr["t337_comentario"];
                if (dr["t337_fecmodif"] != DBNull.Value)
                    o.t337_fecmodif = (DateTime)dr["t337_fecmodif"];
                if (dr["t314_idusuario_modif"] != DBNull.Value)
                    o.t314_idusuario_modif = int.Parse(dr["t314_idusuario_modif"].ToString());

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CONSUMOIAP"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

		#endregion
	}
}
