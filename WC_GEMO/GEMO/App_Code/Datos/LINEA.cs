using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GEMO.DAL
{

	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : LINEA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T708_LINEA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
    public partial class LINEA
	{
        #region Propiedades y Atributos

        private int _t708_idlinea;
        public int t708_idlinea
        {
            get { return _t708_idlinea; }
            set { _t708_idlinea = value; }
        }
        private short? _t708_prefintern;
        public short? t708_prefintern
        {
            get { return _t708_prefintern; }
            set { _t708_prefintern = value; }
        }

        private long _t708_numlinea;
        public long t708_numlinea
        {
            get { return _t708_numlinea; }
            set { _t708_numlinea = value; }
        }

        private int _t708_numext;
        public int t708_numext
        {
            get { return _t708_numext; }
            set { _t708_numext = value; }
        }

        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _empresa;
        public string empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        private byte? _t712_idtarjeta;
        public byte? t712_idtarjeta
        {
            get { return _t712_idtarjeta; }
            set { _t712_idtarjeta = value; }
        }

        private byte? _t063_idproveedor;
        public byte? t063_idproveedor
        {
            get { return _t063_idproveedor; }
            set { _t063_idproveedor = value; }
        }

        private short? _t134_idmedio;
        public short? t134_idmedio
        {
            get { return _t134_idmedio; }
            set { _t134_idmedio = value; }
        }

        private string _t708_modelo;
        public string t708_modelo
        {
            get { return _t708_modelo; }
            set { _t708_modelo = value; }
        }

        private string _t708_IMEI;
        public string t708_IMEI
        {
            get { return _t708_IMEI; }
            set { _t708_IMEI = value; }
        }

        private string _t708_ICC;
        public string t708_ICC
        {
            get { return _t708_ICC; }
            set { _t708_ICC = value; }
        }

        private string _t708_observa;
        public string t708_observa
        {
            get { return _t708_observa; }
            set { _t708_observa = value; }
        }

        private string _t710_idestado;
        public string t710_idestado
        {
            get { return _t710_idestado; }
            set { _t710_idestado = value; }
        }

        private short? _t711_idtarifa;
        public short? t711_idtarifa
        {
            get { return _t711_idtarifa; }
            set { _t711_idtarifa = value; }
        }

        private int? _t001_responsable;
        public int? t001_responsable
        {
            get { return _t001_responsable; }
            set { _t001_responsable = value; }
        }

        private string _responsable;
        public string responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }
        private string _t708_tipouso;
        public string t708_tipouso
        {
            get { return _t708_tipouso; }
            set { _t708_tipouso = value; }
        }

        private int? _t001_beneficiario;
        public int? t001_beneficiario
        {
            get { return _t001_beneficiario; }
            set { _t001_beneficiario = value; }
        }

        private string _beneficiario;
        public string beneficiario
        {
            get { return _beneficiario; }
            set { _beneficiario = value; }
        }

        private string _t708_departamento;
        public string t708_departamento
        {
            get { return _t708_departamento; }
            set { _t708_departamento = value; }
        }

        private bool _t708_QEQ;
        public bool t708_QEQ
        {
            get { return _t708_QEQ; }
            set { _t708_QEQ = value; }
        }
        #endregion
		#region Metodos
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T708_LINEA,
        /// y devuelve una instancia u objeto del tipo LINEA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	11/04/2011 17:54:57
        /// </history>
        /// -----------------------------------------------------------------------------
        public static LINEA Select(SqlTransaction tr, int t708_idlinea)
        {
            LINEA o = new LINEA();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
            aParam[0].Value = t708_idlinea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GEM_LINEA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GEM_LINEA_S", aParam);

            if (dr.Read())
            {
                if (dr["t708_idlinea"] != DBNull.Value)
                    o.t708_idlinea = int.Parse(dr["t708_idlinea"].ToString());
                if (dr["t708_prefintern"] != DBNull.Value)
                    o.t708_prefintern = short.Parse(dr["t708_prefintern"].ToString());
                if (dr["t708_numlinea"] != DBNull.Value)
                    o.t708_numlinea = long.Parse(dr["t708_numlinea"].ToString());
                if (dr["t708_numext"] != DBNull.Value)
                    o.t708_numext = int.Parse(dr["t708_numext"].ToString());
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["empresa"] != DBNull.Value)
                    o.empresa = (string)dr["empresa"];
                if (dr["t712_idtarjeta"] != DBNull.Value)
                    o.t712_idtarjeta = byte.Parse(dr["t712_idtarjeta"].ToString());
                if (dr["t063_idproveedor"] != DBNull.Value)
                    o.t063_idproveedor = byte.Parse(dr["t063_idproveedor"].ToString());
                if (dr["t134_idmedio"] != DBNull.Value)
                    o.t134_idmedio = (short)dr["t134_idmedio"];
                if (dr["t708_modelo"] != DBNull.Value)
                    o.t708_modelo = (string)dr["t708_modelo"];
                if (dr["t708_IMEI"] != DBNull.Value)
                    o.t708_IMEI = (string)dr["t708_IMEI"];
                if (dr["t708_ICC"] != DBNull.Value)
                    o.t708_ICC = (string)dr["t708_ICC"];
                if (dr["t708_observa"] != DBNull.Value)
                    o.t708_observa = (string)dr["t708_observa"];
                if (dr["t710_idestado"] != DBNull.Value)
                    o.t710_idestado = (string)dr["t710_idestado"];
                if (dr["t711_idtarifa"] != DBNull.Value)
                    o.t711_idtarifa = (short)dr["t711_idtarifa"];
                if (dr["t001_responsable"] != DBNull.Value)
                    o.t001_responsable = int.Parse(dr["t001_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.responsable = (string)dr["responsable"];
                if (dr["t708_tipouso"] != DBNull.Value)
                    o.t708_tipouso = (string)dr["t708_tipouso"];
                if (dr["t001_beneficiario"] != DBNull.Value)
                    o.t001_beneficiario = int.Parse(dr["t001_beneficiario"].ToString());
                if (dr["beneficiario"] != DBNull.Value)
                    o.beneficiario = (string)dr["beneficiario"];
                if (dr["t708_departamento"] != DBNull.Value)
                    o.t708_departamento = (string)dr["t708_departamento"];
                if (dr["t708_QEQ"] != DBNull.Value)
                    o.t708_QEQ = (bool)dr["t708_QEQ"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de LINEA"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T708_LINEA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<short>t708_prefintern, long t708_numlinea, int t708_numext, Nullable<int> t313_idempresa, Nullable<byte> t712_idtarjeta, Nullable<byte> t063_idproveedor, Nullable<short> t134_idmedio, string t708_modelo, string t708_IMEI, string t708_ICC, string t708_observa, string t710_idestado, Nullable<short> t711_idtarifa, Nullable<int> t001_responsable, string t708_tipouso, Nullable<int> t001_beneficiario, string t708_departamento, bool t708_QEQ)
		{
			SqlParameter[] aParam = new SqlParameter[18];
            aParam[0] = new SqlParameter("@t708_prefintern", SqlDbType.SmallInt, 2);
            aParam[0].Value = t708_prefintern;
            aParam[1] = new SqlParameter("@t708_numlinea", SqlDbType.BigInt, 8);
			aParam[1].Value = t708_numlinea;
			aParam[2] = new SqlParameter("@t708_numext", SqlDbType.Int, 4);
			aParam[2].Value = t708_numext;
			aParam[3] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
			aParam[3].Value = t313_idempresa;
            aParam[4] = new SqlParameter("@t712_idtarjeta", SqlDbType.TinyInt, 1);
            aParam[4].Value = t712_idtarjeta;
			aParam[5] = new SqlParameter("@t063_idproveedor", SqlDbType.TinyInt, 1);
			aParam[5].Value = t063_idproveedor;
			aParam[6] = new SqlParameter("@t134_idmedio", SqlDbType.SmallInt, 2);
			aParam[6].Value = t134_idmedio;
			aParam[7] = new SqlParameter("@t708_modelo", SqlDbType.Text, 40);
			aParam[7].Value = t708_modelo;
			aParam[8] = new SqlParameter("@t708_IMEI", SqlDbType.Text, 20);
			aParam[8].Value = t708_IMEI;
			aParam[9] = new SqlParameter("@t708_ICC", SqlDbType.Text, 20);
			aParam[9].Value = t708_ICC;
			aParam[10] = new SqlParameter("@t708_observa", SqlDbType.Text, 2147483647);
			aParam[10].Value = t708_observa;
			aParam[11] = new SqlParameter("@t710_idestado", SqlDbType.Char, 1);
			aParam[11].Value = t710_idestado;
			aParam[12] = new SqlParameter("@t711_idtarifa", SqlDbType.SmallInt, 2);
			aParam[12].Value = t711_idtarifa;
			aParam[13] = new SqlParameter("@t001_responsable", SqlDbType.Int, 4);
			aParam[13].Value = t001_responsable;
			aParam[14] = new SqlParameter("@t708_tipouso", SqlDbType.Text, 1);
			aParam[14].Value = t708_tipouso;
			aParam[15] = new SqlParameter("@t001_beneficiario", SqlDbType.Int, 4);
			aParam[15].Value = t001_beneficiario;
			aParam[16] = new SqlParameter("@t708_departamento", SqlDbType.Text, 50);
			aParam[16].Value = t708_departamento;
			aParam[17] = new SqlParameter("@t708_QEQ", SqlDbType.Bit, 1);
			aParam[17].Value = t708_QEQ;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("GEM_LINEA_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_LINEA_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T708_LINEA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t708_idlinea, Nullable<short> t708_prefintern, long t708_numlinea, int t708_numext, Nullable<int> t313_idempresa, Nullable<byte> t712_idtarjeta, Nullable<byte> t063_idproveedor, Nullable<short> t134_idmedio, string t708_modelo, string t708_IMEI, string t708_ICC, string t708_observa, string t710_idestado, Nullable<short> t711_idtarifa, Nullable<int> t001_responsable, string t708_tipouso, Nullable<int> t001_beneficiario, string t708_departamento, bool t708_QEQ)
		{
			SqlParameter[] aParam = new SqlParameter[19];
			aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
			aParam[0].Value = t708_idlinea;
            aParam[1] = new SqlParameter("@t708_prefintern", SqlDbType.SmallInt, 2);
            aParam[1].Value = t708_prefintern;
            aParam[2] = new SqlParameter("@t708_numlinea", SqlDbType.BigInt, 8);
			aParam[2].Value = t708_numlinea;
			aParam[3] = new SqlParameter("@t708_numext", SqlDbType.Int, 4);
			aParam[3].Value = t708_numext;
			aParam[4] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
			aParam[4].Value = t313_idempresa;
            aParam[5] = new SqlParameter("@t712_idtarjeta", SqlDbType.TinyInt, 1);
            aParam[5].Value = t712_idtarjeta;
			aParam[6] = new SqlParameter("@t063_idproveedor", SqlDbType.TinyInt, 1);
			aParam[6].Value = t063_idproveedor;
			aParam[7] = new SqlParameter("@t134_idmedio", SqlDbType.SmallInt, 2);
			aParam[7].Value = t134_idmedio;
			aParam[8] = new SqlParameter("@t708_modelo", SqlDbType.Text, 40);
			aParam[8].Value = t708_modelo;
			aParam[9] = new SqlParameter("@t708_IMEI", SqlDbType.Text, 20);
			aParam[9].Value = t708_IMEI;
			aParam[10] = new SqlParameter("@t708_ICC", SqlDbType.Text, 20);
			aParam[10].Value = t708_ICC;
			aParam[11] = new SqlParameter("@t708_observa", SqlDbType.Text, 2147483647);
			aParam[11].Value = t708_observa;
			aParam[12] = new SqlParameter("@t710_idestado", SqlDbType.Char, 1);
			aParam[12].Value = t710_idestado;
			aParam[13] = new SqlParameter("@t711_idtarifa", SqlDbType.SmallInt, 2);
			aParam[13].Value = t711_idtarifa;
			aParam[14] = new SqlParameter("@t001_responsable", SqlDbType.Int, 4);
			aParam[14].Value = t001_responsable;
			aParam[15] = new SqlParameter("@t708_tipouso", SqlDbType.Text, 1);
			aParam[15].Value = t708_tipouso;
			aParam[16] = new SqlParameter("@t001_beneficiario", SqlDbType.Int, 4);
			aParam[16].Value = t001_beneficiario;
			aParam[17] = new SqlParameter("@t708_departamento", SqlDbType.Text, 50);
			aParam[17].Value = t708_departamento;
			aParam[18] = new SqlParameter("@t708_QEQ", SqlDbType.Bit, 1);
			aParam[18].Value = t708_QEQ;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_LINEA_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_LINEA_U", aParam);
		}
        public static int UpdateReasigRes(SqlTransaction tr, int t708_idlinea, Nullable<int> t001_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
            aParam[0].Value = t708_idlinea;
            aParam[1] = new SqlParameter("@t001_responsable", SqlDbType.Int, 4);
            aParam[1].Value = t001_responsable;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_LINEA_U_RESP", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_LINEA_U_RESP", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Registra en la tabla T708_LINEA las líneas facturadas que no están inventaridas con estado pre-activo
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	11/04/2011 16:23:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int ActualizarFacturadasNoInventariadas(SqlTransaction tr, string sLineas)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sLineas", SqlDbType.VarChar, 8000);
            aParam[0].Value = sLineas;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_ACTUALIZAR_CASO1", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_ACTUALIZAR_CASO1", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza la tabla T708_LINEA de aquellas líneas activas no facturadas y las pasa a estado pre-inactivo
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	11/04/2011 16:23:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int ActualizarActivasSinFactura(SqlTransaction tr, string sLineas)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sLineas", SqlDbType.VarChar, 8000);
            aParam[0].Value = sLineas;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_ACTUALIZAR_CASO2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_ACTUALIZAR_CASO2", aParam);
        }
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T708_LINEA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t708_idlinea)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
			aParam[0].Value = t708_idlinea;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_LINEA_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_LINEA_D", aParam);
		}



		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T708_LINEA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Busqueda(
                    Nullable<long> t708_numlinea,
                    Nullable<int> t708_numext,
                    string t708_IMEI,
                    string t708_ICC,
                    Nullable<short> t708_prefintern,
                    string sEmpresas,
                    string sResponsables,
                    string sBeneficiarios,
                    string sDepartamentos,
                    string sEstados,
                    string sMedios,
                    int t001_idficepi
               )
        {
            SqlParameter[] aParam = new SqlParameter[12];

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t708_prefintern", SqlDbType.SmallInt, 2);
            aParam[1].Value = t708_prefintern;
            aParam[2] = new SqlParameter("@t708_numlinea", SqlDbType.BigInt, 8);
            aParam[2].Value = t708_numlinea;
            aParam[3] = new SqlParameter("@t708_numext", SqlDbType.Int, 4);
            aParam[3].Value = t708_numext;
            aParam[4] = new SqlParameter("@t708_IMEI", SqlDbType.Text, 20);
            aParam[4].Value = t708_IMEI;
            aParam[5] = new SqlParameter("@t708_ICC", SqlDbType.Text, 20);
            aParam[5].Value = t708_ICC;            
            aParam[6] = new SqlParameter("@sEmpresas", SqlDbType.VarChar, 8000);
            aParam[6].Value = sEmpresas;
            aParam[7] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[7].Value = sResponsables;
            aParam[8] = new SqlParameter("@sBeneficiarios", SqlDbType.VarChar, 8000);
            aParam[8].Value = sBeneficiarios;
            aParam[9] = new SqlParameter("@sDepartamentos", SqlDbType.VarChar, 8000);
            aParam[9].Value = sDepartamentos;
            aParam[10] = new SqlParameter("@sEstados", SqlDbType.VarChar, 8000);
            aParam[10].Value = sEstados;
            aParam[11] = new SqlParameter("@sMedios", SqlDbType.VarChar, 8000);
            aParam[11].Value = sMedios;



            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
                return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_ADM", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }

        public static SqlDataReader Usuario(
                    int t001_idficepi,
                    string sEstados, 
                    string sResponsable, 
                    string sBeneficiario
               )
        {
            SqlParameter[] aParam = new SqlParameter[4];

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            aParam[1] = new SqlParameter("@sEstados", SqlDbType.Char, 100);
            aParam[1].Value = sEstados;

            aParam[2] = new SqlParameter("@sResponsable", SqlDbType.Char, 1);
            aParam[2].Value = sResponsable;

            aParam[3] = new SqlParameter("@sBeneficiario", SqlDbType.Char, 1);
            aParam[3].Value = sBeneficiario;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
                return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader Responsables()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader DestinatariosFactura()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_DESTI_FACT2", aParam);
        }
        public static SqlDataReader Responsables(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_AL", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader Responsables(string t001_apellido1, string t001_apellido2, string t001_nombre, bool sMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = sMostrarBajas;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_AL_FAC", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }       
        public static SqlDataReader ResponsablesLineas(
                    int t001_idficepi
                    )
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_LIN", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader ResponsablesLineasFactura(
                    int t001_idficepi
                    )
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_LIN_FAC", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader ResponsablesLineasFactura(
                    int t001_idficepi,
                    DateTime dFecha
                    )
        {
            SqlParameter[] aParam = new SqlParameter[2];

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dFecha;

            //if (HttpContext.Current.Session["ADMIN"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_LIN_FAC2", aParam);
            //return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_RESP_LIN_FAC", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("GEM_LINEA_USU", aParam);
        }
        public static SqlDataReader Controladores()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_CONTROLADORES", aParam);
        }

        public static SqlDataReader FacturadasNoInventariadas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_CUADRE_CASO1", aParam);
        }
        public static SqlDataReader ActivasSinFactura()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_CUADRE_CASO2", aParam);
        }
        public static SqlDataReader InactivasYBloqueadasConFactura()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_CUADRE_CASO3", aParam);
        }
        public static SqlDataReader DescuadresVarios(int iAnnomes , int iProveedor )
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@iAnnomes", SqlDbType.Int, 4);
            aParam[0].Value = iAnnomes;            
            aParam[1] = new SqlParameter("@iProveedor", SqlDbType.Int, 4);
            aParam[1].Value = iProveedor;

            return SqlHelper.ExecuteSqlDataReader("GEM_CUADRE_CASO4", aParam);
        }
		#endregion
	}
}
