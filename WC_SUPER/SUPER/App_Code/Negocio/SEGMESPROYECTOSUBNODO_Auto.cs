using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SEGMESPROYECTOSUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T325_SEGMESPROYECTOSUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	20/04/2009 16:03:19	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SEGMESPROYECTOSUBNODO
	{
		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t325_anomes;
		public int t325_anomes
		{
			get {return _t325_anomes;}
			set { _t325_anomes = value ;}
		}

		private string _t325_estado;
		public string t325_estado
		{
			get {return _t325_estado;}
			set { _t325_estado = value ;}
		}

        private decimal _t325_avanceprod;
        public decimal t325_avanceprod
		{
			get {return _t325_avanceprod;}
			set { _t325_avanceprod = value ;}
		}

        private decimal _t325_gastosfinancieros;
        public decimal t325_gastosfinancieros
		{
			get {return _t325_gastosfinancieros;}
			set { _t325_gastosfinancieros = value ;}
		}

		private bool _t325_traspasoIAP;
		public bool t325_traspasoIAP
		{
			get {return _t325_traspasoIAP;}
			set { _t325_traspasoIAP = value ;}
		}

        private decimal _t325_prodperiod;
        public decimal t325_prodperiod
		{
			get {return _t325_prodperiod;}
			set { _t325_prodperiod = value ;}
		}

        private decimal _t325_consperiod;
        public decimal t325_consperiod
		{
			get {return _t325_consperiod;}
			set { _t325_consperiod = value ;}
		}
		#endregion

		#region Constructor

		public SEGMESPROYECTOSUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        ///// -----------------------------------------------------------------------------
        ///// <summary>
        ///// Obtiene un registro de la tabla T325_SEGMESPROYECTOSUBNODO,
        ///// y devuelve una instancia u objeto del tipo SEGMESPROYECTOSUBNODO
        ///// </summary>
        ///// <returns></returns>
        ///// <history>
        ///// 	Creado por [sqladmin]	20/04/2009 16:03:19
        ///// </history>
        ///// -----------------------------------------------------------------------------
        //public static SEGMESPROYECTOSUBNODO Select(SqlTransaction tr, int t325_idsegmesproy, string t422_idmoneda) 
        //{
        //    SEGMESPROYECTOSUBNODO o = new SEGMESPROYECTOSUBNODO();

        //    SqlParameter[] aParam = new SqlParameter[2];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
        //    aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGMESPROYECTOSUBNODO_SEL", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_SEL", aParam);

        //    if (dr.Read())
        //    {
        //        if (dr["t325_idsegmesproy"] != DBNull.Value)
        //            o.t325_idsegmesproy = int.Parse(dr["t325_idsegmesproy"].ToString());
        //        if (dr["t305_idproyectosubnodo"] != DBNull.Value)
        //            o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
        //        if (dr["t325_anomes"] != DBNull.Value)
        //            o.t325_anomes = int.Parse(dr["t325_anomes"].ToString());
        //        if (dr["t325_estado"] != DBNull.Value)
        //            o.t325_estado = (string)dr["t325_estado"];
        //        if (dr["t325_avanceprod"] != DBNull.Value)
        //            o.t325_avanceprod = decimal.Parse(dr["t325_avanceprod"].ToString());
        //        if (dr["t325_gastosfinancieros"] != DBNull.Value)
        //            o.t325_gastosfinancieros = decimal.Parse(dr["t325_gastosfinancieros"].ToString());
        //        if (dr["t325_traspasoIAP"] != DBNull.Value)
        //            o.t325_traspasoIAP = (bool)dr["t325_traspasoIAP"];
        //        if (dr["t325_prodperiod"] != DBNull.Value)
        //            o.t325_prodperiod = decimal.Parse(dr["t325_prodperiod"].ToString());
        //        if (dr["t325_consperiod"] != DBNull.Value)
        //            o.t325_consperiod = decimal.Parse(dr["t325_consperiod"].ToString());


        //        if (dr["t301_modelocoste"] != DBNull.Value)
        //            o.t301_modelocoste = (string)dr["t301_modelocoste"];
        //        if (dr["t301_modelotarif"] != DBNull.Value)
        //            o.t301_modelotarif = (string)dr["t301_modelotarif"];

        //    }
        //    else
        //    {
        //        throw (new NullReferenceException("No se ha obtenido ningun dato de SEGMESPROYECTOSUBNODO"));
        //    }

        //    dr.Close();
        //    dr.Dispose();

        //    return o;
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T325_SEGMESPROYECTOSUBNODO en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	20/04/2009 16:03:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT305_idproyectosubnodo(SqlTransaction tr, int t305_idproyectosubnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;


			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SEGMESPROYECTOSUBNODO_SByPSN", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_SByPSN", aParam);
		}

		#endregion
	}
}
