using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONSPERMES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T378_CONSPERMES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/05/2008 11:34:38	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONSPERMES
	{

		#region Propiedades y Atributos

		private int _t325_idsegmesproy;
		public int t325_idsegmesproy
		{
			get {return _t325_idsegmesproy;}
			set { _t325_idsegmesproy = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private double _t378_unidades;
		public double t378_unidades
		{
			get {return _t378_unidades;}
			set { _t378_unidades = value ;}
		}

        private decimal _t378_costeunitariocon;
        public decimal t378_costeunitariocon
		{
			get {return _t378_costeunitariocon;}
			set { _t378_costeunitariocon = value ;}
		}

        private decimal _t378_costeunitariorep;
        public decimal t378_costeunitariorep
		{
			get {return _t378_costeunitariorep;}
			set { _t378_costeunitariorep = value ;}
		}

		private int _t303_idnodo_usuariomes;
		public int t303_idnodo_usuariomes
		{
			get {return _t303_idnodo_usuariomes;}
			set { _t303_idnodo_usuariomes = value ;}
		}

		private int _t313_idempresa_nodomes;
		public int t313_idempresa_nodomes
		{
			get {return _t313_idempresa_nodomes;}
			set { _t313_idempresa_nodomes = value ;}
		}
		#endregion

		#region Constructores

		public CONSPERMES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T378_CONSPERMES,
        /// y devuelve una instancia u objeto del tipo CONSPERMES
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	13/03/2009 11:05:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static CONSPERMES Select(SqlTransaction tr, int t325_idsegmesproy, int t314_idusuario)
        {
            CONSPERMES o = new CONSPERMES();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSPERMES_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSPERMES_S", aParam);

            if (dr.Read())
            {
                if (dr["t325_idsegmesproy"] != DBNull.Value)
                    o.t325_idsegmesproy = int.Parse(dr["t325_idsegmesproy"].ToString());
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t378_unidades"] != DBNull.Value)
                    o.t378_unidades = double.Parse(dr["t378_unidades"].ToString());
                if (dr["t378_costeunitariocon"] != DBNull.Value)
                    o.t378_costeunitariocon = decimal.Parse(dr["t378_costeunitariocon"].ToString());
                if (dr["t378_costeunitariorep"] != DBNull.Value)
                    o.t378_costeunitariorep = decimal.Parse(dr["t378_costeunitariorep"].ToString());
                if (dr["t303_idnodo_usuariomes"] != DBNull.Value)
                    o.t303_idnodo_usuariomes = int.Parse(dr["t303_idnodo_usuariomes"].ToString());
                if (dr["t313_idempresa_nodomes"] != DBNull.Value)
                    o.t313_idempresa_nodomes = int.Parse(dr["t313_idempresa_nodomes"].ToString());

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CONSPERMES"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T378_CONSPERMES.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	30/10/2009 10:10:24
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t325_idsegmesproy, Nullable<int> t314_idusuario, Nullable<double> t378_unidades, Nullable<decimal> t378_costeunitariocon, Nullable<decimal> t378_costeunitariorep, Nullable<int> t303_idnodo_usuariomes, Nullable<int> t313_idempresa_nodomes, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t378_unidades", SqlDbType.Float, 8);
            aParam[2].Value = t378_unidades;
            aParam[3] = new SqlParameter("@t378_costeunitariocon", SqlDbType.Money, 8);
            aParam[3].Value = t378_costeunitariocon;
            aParam[4] = new SqlParameter("@t378_costeunitariorep", SqlDbType.Money, 8);
            aParam[4].Value = t378_costeunitariorep;
            aParam[5] = new SqlParameter("@t303_idnodo_usuariomes", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo_usuariomes;
            aParam[6] = new SqlParameter("@t313_idempresa_nodomes", SqlDbType.Int, 4);
            aParam[6].Value = t313_idempresa_nodomes;

            aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[7].Value = nOrden;
            aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[8].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSPERMES_C", aParam);
        }

		#endregion
	}
}
