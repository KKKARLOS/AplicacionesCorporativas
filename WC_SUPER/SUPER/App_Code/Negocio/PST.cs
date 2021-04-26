using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : PST
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T346_PST
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	31/10/2007 16:43:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PST
	{
		#region Propiedades y Atributos

		private int _t346_idpst;
		public int t346_idpst
		{
			get {return _t346_idpst;}
			set { _t346_idpst = value ;}
		}

        private int _t303_idnodo;
        public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private string _t346_codpst;
		public string t346_codpst
		{
			get {return _t346_codpst;}
			set { _t346_codpst = value ;}
		}

		private string _t346_despst;
		public string t346_despst
		{
			get {return _t346_despst;}
			set { _t346_despst = value ;}
		}

		private bool _t346_estado;
		public bool t346_estado
		{
			get {return _t346_estado;}
			set { _t346_estado = value ;}
		}
        private int? _cod_cliente;
        public int? cod_cliente
        {
            get { return _cod_cliente; }
            set { _cod_cliente = value; }
        }
        private string _desCliente;
        public string desCliente
        {
            get { return _desCliente; }
            set { _desCliente = value; }
        }

        private int? _idOTExterno;
        public int? idOTExterno
        {
            get { return _idOTExterno; }
            set { _idOTExterno = value; }
        }

        private string _idOrigenExterno;
        public string idOrigenExterno
        {
            get { return _idOrigenExterno; }
            set { _idOrigenExterno = value; }
        }

        private DateTime? _t346_fecharef;
        public DateTime? t346_fecharef
        {
            get { return _t346_fecharef; }
            set { _t346_fecharef = value; }
        }

        private bool _clienteNulo;
        public bool clienteNulo
        {
            get { return _clienteNulo; }
            set { _clienteNulo = value; }
        }

        #endregion

        #region Constructores

        public PST() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static int Insert(SqlTransaction tr, int t303_idnodo, string t346_codpst, string t346_despst, bool t346_estado,
                            Nullable<int> cod_cliente, Nullable<int> idOTExterno, string idOrigenExterno, Nullable<DateTime> t346_fecharef, decimal fHoras, decimal fPresupuesto, string t422_moneda)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t346_codpst", SqlDbType.Text, 25);
            aParam[1].Value = t346_codpst;
            aParam[2] = new SqlParameter("@t346_despst", SqlDbType.Text, 50);
            aParam[2].Value = t346_despst;
            aParam[3] = new SqlParameter("@t346_estado", SqlDbType.Bit, 1);
            aParam[3].Value = t346_estado;
            aParam[4] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[4].Value = cod_cliente;
            aParam[5] = new SqlParameter("@idOTExterno", SqlDbType.Int, 4);
            aParam[5].Value = idOTExterno;
            aParam[6] = new SqlParameter("@idOrigenExterno", SqlDbType.Text, 10);
            if (idOrigenExterno == "")
                aParam[6].Value = null;
            else
                aParam[6].Value = idOrigenExterno;
            aParam[7] = new SqlParameter("@t346_fecharef", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = t346_fecharef;
            aParam[8] = new SqlParameter("@t346_idCuentaHERMES", SqlDbType.VarChar, 25);
            aParam[8].Value = null;
            aParam[9] = new SqlParameter("@t346_horas", SqlDbType.Real, 4);
            aParam[9].Value = fHoras;
            aParam[10] = new SqlParameter("@t346_presupuesto", SqlDbType.Money, 8);
            aParam[10].Value = fPresupuesto;
            aParam[11] = new SqlParameter("@t422_moneda", SqlDbType.VarChar, 5);
            aParam[11].Value = t422_moneda;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PST_I_PANT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PST_I_PANT", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Insertao updatea un registro desde un fichero en la tabla t346_PST.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	31/10/2007 16:43:48
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t346_idpst, int t303_idnodo, string t346_codpst, string t346_despst,
                                bool t346_estado, Nullable<int> cod_cliente, Nullable<int> idOTExterno, string idOrigenExterno, 
                                Nullable<DateTime> t346_fecharef, decimal fHoras, decimal fPresupuesto, string t422_moneda)
		{
			SqlParameter[] aParam = new SqlParameter[12];
			aParam[0] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[0].Value = t346_idpst;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t346_codpst", SqlDbType.Text, 25);
			aParam[2].Value = t346_codpst;
			aParam[3] = new SqlParameter("@t346_despst", SqlDbType.Text, 50);
			aParam[3].Value = t346_despst;
			aParam[4] = new SqlParameter("@t346_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t346_estado;
            aParam[5] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[5].Value = cod_cliente;
            aParam[6] = new SqlParameter("@idOTExterno", SqlDbType.Int, 4);
            aParam[6].Value = idOTExterno;
            aParam[7] = new SqlParameter("@idOrigenExterno", SqlDbType.Text, 10);
            if (idOrigenExterno == "")
                aParam[7].Value = null;
            else
                aParam[7].Value = idOrigenExterno;
            aParam[8] = new SqlParameter("@t346_fecharef", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = t346_fecharef;
            aParam[9] = new SqlParameter("@t346_horas", SqlDbType.Real, 4);
            aParam[9].Value = fHoras;
            aParam[10] = new SqlParameter("@t346_presupuesto", SqlDbType.Money, 8);
            aParam[10].Value = fPresupuesto;
            aParam[11] = new SqlParameter("@t422_moneda", SqlDbType.VarChar, 5);
            aParam[11].Value = t422_moneda;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PST_U_PANT", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PST_U_PANT", aParam);
		}
		public static int Delete(SqlTransaction tr, int t346_idpst)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[0].Value = t346_idpst;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PST_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PST_D", aParam);
		}
		public static PST Select(int t346_idpst) 
		{
			PST o = new PST();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[0].Value = t346_idpst;

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_PST_S", aParam);

			if (dr.Read())
			{
				if (dr["t346_idpst"] != DBNull.Value)
					o.t346_idpst = (int)dr["t346_idpst"];
				if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t346_codpst"] != DBNull.Value)
					o.t346_codpst = (string)dr["t346_codpst"];
				if (dr["t346_despst"] != DBNull.Value)
					o.t346_despst = (string)dr["t346_despst"];
				if (dr["t346_estado"] != DBNull.Value)
					o.t346_estado = (bool)dr["t346_estado"];
                if (dr["cod_cliente"] != DBNull.Value)
                    o.cod_cliente = int.Parse(dr["cod_cliente"].ToString());
                if (dr["nom_cliente"] != DBNull.Value)
                    o.desCliente = (string)dr["nom_cliente"];
                if (dr["idOTExterno"] != DBNull.Value)
                    o.idOTExterno = int.Parse(dr["idOTExterno"].ToString());
                if (dr["idOrigenExterno"] != DBNull.Value)
                    o.idOrigenExterno = (string)dr["idOrigenExterno"];
                else
                    o.idOrigenExterno = "";
                if (dr["t346_fecharef"] != DBNull.Value)
                    o.t346_fecharef = (DateTime)dr["t346_fecharef"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de OTC"));
			}

            dr.Close();
            dr.Dispose();

            return o;
		}
        public static SqlDataReader Catalogo(Nullable<int> t346_idpst, Nullable<int> t303_idnodo, string t346_codpst, string t346_despst, 
                                             Nullable<bool> t346_estado, Nullable<int> cod_cliente, Nullable<int> idOTExterno, 
                                             string idOrigenExterno, Nullable<DateTime> t346_fecharef, string cboArea, Nullable<bool> clienteNulo)
		{
			SqlParameter[] aParam = new SqlParameter[11];
			aParam[0] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[0].Value = t346_idpst;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t346_codpst", SqlDbType.Text, 25);
			aParam[2].Value = t346_codpst;
			aParam[3] = new SqlParameter("@t346_despst", SqlDbType.Text, 50);
			aParam[3].Value = t346_despst;
			aParam[4] = new SqlParameter("@t346_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t346_estado;

            aParam[5] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[5].Value = cod_cliente;
            aParam[6] = new SqlParameter("@idOTExterno", SqlDbType.Int, 4);
            aParam[6].Value = idOTExterno;
            aParam[7] = new SqlParameter("@idOrigenExterno", SqlDbType.Text, 10);
            if (idOrigenExterno == "")
                aParam[7].Value = null;
            else
                aParam[7].Value = idOrigenExterno;
            aParam[8] = new SqlParameter("@t346_fecharef", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = t346_fecharef;

            aParam[9] = new SqlParameter("@Area", SqlDbType.VarChar, 1);
            aParam[9].Value = cboArea;
            aParam[10] = new SqlParameter("@clienteNulo", SqlDbType.Bit, 1);
            aParam[10].Value = clienteNulo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_PST_C", aParam);
		}

		#endregion
	}
}
