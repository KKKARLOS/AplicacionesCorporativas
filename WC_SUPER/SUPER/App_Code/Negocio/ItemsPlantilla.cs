using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ITEMSPLANTILLA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t339_ITEMSPLANTILLA
	/// </summary>
	/// <history>
	/// 	Creado por [doarhumi]	19/11/2007 15:51:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ITEMSPLANTILLA
	{
		#region Propiedades y Atributos

		private int _t339_iditems;
		public int t339_iditems
		{
			get {return _t339_iditems;}
			set { _t339_iditems = value ;}
		}

		private string _t339_tipoitem;
		public string t339_tipoitem
		{
			get {return _t339_tipoitem;}
			set { _t339_tipoitem = value ;}
		}

		private string _t339_desitem;
		public string t339_desitem
		{
			get {return _t339_desitem;}
			set { _t339_desitem = value ;}
		}

		private byte _t339_margen;
		public byte t339_margen
		{
			get {return _t339_margen;}
			set { _t339_margen = value ;}
		}

		private short _t339_orden;
		public short t339_orden
		{
			get {return _t339_orden;}
			set { _t339_orden = value ;}
		}

		private int _t338_idplantilla;
		public int t338_idplantilla
		{
			get {return _t338_idplantilla;}
			set { _t338_idplantilla = value ;}
		}

        private string _tipoPlant;
        public string tipoPlant
        {
            get { return _tipoPlant; }
            set { _tipoPlant = value; }
        }

		private bool _t339_facturable;
		public bool t339_facturable
		{
			get {return _t339_facturable;}
			set { _t339_facturable = value ;}
		}

		private bool _t339_avanceauto;
		public bool t339_avanceauto
		{
			get {return _t339_avanceauto;}
			set { _t339_avanceauto = value ;}
		}

		private bool _t339_obligaest;
		public bool t339_obligaest
		{
			get {return _t339_obligaest;}
			set { _t339_obligaest = value ;}
		}
		#endregion

		#region Constructores

		public ITEMSPLANTILLA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t339_ITEMSPLANTILLA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [doarhumi]	19/11/2007 15:51:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t339_tipoitem , string t339_desitem , byte t339_margen , short t339_orden , int t338_idplantilla , bool t339_facturable , bool t339_avanceauto , bool t339_obligaest)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t339_tipoitem", SqlDbType.Text, 1);
			aParam[0].Value = t339_tipoitem;
			aParam[1] = new SqlParameter("@t339_desitem", SqlDbType.Text, 100);
			aParam[1].Value = t339_desitem;
			aParam[2] = new SqlParameter("@t339_margen", SqlDbType.TinyInt, 1);
			aParam[2].Value = t339_margen;
			aParam[3] = new SqlParameter("@t339_orden", SqlDbType.SmallInt, 2);
			aParam[3].Value = t339_orden;
			aParam[4] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[4].Value = t338_idplantilla;
			aParam[5] = new SqlParameter("@t339_facturable", SqlDbType.Bit, 1);
			aParam[5].Value = t339_facturable;
			aParam[6] = new SqlParameter("@t339_avanceauto", SqlDbType.Bit, 1);
			aParam[6].Value = t339_avanceauto;
			aParam[7] = new SqlParameter("@t339_obligaest", SqlDbType.Bit, 1);
			aParam[7].Value = t339_obligaest;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ITEMSPLANTILLA_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ITEMSPLANTILLA_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t339_ITEMSPLANTILLA.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	19/11/2007 15:51:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t339_iditems, string t339_tipoitem, string t339_desitem, byte t339_margen, short t339_orden, int t338_idplantilla, bool t339_facturable, bool t339_avanceauto, bool t339_obligaest)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;
			aParam[1] = new SqlParameter("@t339_tipoitem", SqlDbType.Text, 1);
			aParam[1].Value = t339_tipoitem;
			aParam[2] = new SqlParameter("@t339_desitem", SqlDbType.Text, 100);
			aParam[2].Value = t339_desitem;
			aParam[3] = new SqlParameter("@t339_margen", SqlDbType.TinyInt, 1);
			aParam[3].Value = t339_margen;
			aParam[4] = new SqlParameter("@t339_orden", SqlDbType.SmallInt, 2);
			aParam[4].Value = t339_orden;
			aParam[5] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[5].Value = t338_idplantilla;
			aParam[6] = new SqlParameter("@t339_facturable", SqlDbType.Bit, 1);
			aParam[6].Value = t339_facturable;
			aParam[7] = new SqlParameter("@t339_avanceauto", SqlDbType.Bit, 1);
			aParam[7].Value = t339_avanceauto;
			aParam[8] = new SqlParameter("@t339_obligaest", SqlDbType.Bit, 1);
			aParam[8].Value = t339_obligaest;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ITEMSPLANTILLA_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ITEMSPLANTILLA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t339_ITEMSPLANTILLA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	19/11/2007 15:51:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t339_iditems)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ITEMSPLANTILLA_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ITEMSPLANTILLA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t339_ITEMSPLANTILLA,
		/// y devuelve una instancia u objeto del tipo ITEMSPLANTILLA
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [doarhumi]	19/11/2007 15:51:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ITEMSPLANTILLA Select(SqlTransaction tr, int t339_iditems) 
		{
			ITEMSPLANTILLA o = new ITEMSPLANTILLA();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ITEMSPLANTILLA_S1", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ITEMSPLANTILLA_S1", aParam);

			if (dr.Read())
			{
				if (dr["t339_iditems"] != DBNull.Value)
					o.t339_iditems = (int)dr["t339_iditems"];
				if (dr["t339_tipoitem"] != DBNull.Value)
					o.t339_tipoitem = (string)dr["t339_tipoitem"];
				if (dr["t339_desitem"] != DBNull.Value)
					o.t339_desitem = (string)dr["t339_desitem"];
				if (dr["t339_margen"] != DBNull.Value)
					o.t339_margen = byte.Parse(dr["t339_margen"].ToString());
				if (dr["t339_orden"] != DBNull.Value)
                    o.t339_orden = short.Parse(dr["t339_orden"].ToString());
				if (dr["t338_idplantilla"] != DBNull.Value)
					o.t338_idplantilla = (int)dr["t338_idplantilla"];
                if (dr["t338_ambito"] != DBNull.Value)
                    o.tipoPlant = (string)dr["t338_ambito"];//Empresarial, Departamental, Privada
                if (dr["t339_facturable"] != DBNull.Value)
					o.t339_facturable = (bool)dr["t339_facturable"];
				if (dr["t339_avanceauto"] != DBNull.Value)
					o.t339_avanceauto = (bool)dr["t339_avanceauto"];
				if (dr["t339_obligaest"] != DBNull.Value)
					o.t339_obligaest = (bool)dr["t339_obligaest"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de ITEMSPLANTILLA"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// COMPRUEBA SI EL ELEMENTO DE PLANTILLA TIENE ESTIMACION OBLIGATORIA
        /// </summary>
        /// <history>
        /// 	Creado por [doarhumi]	19/11/2007 15:51:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static bool bObligaEst(SqlTransaction tr, int nIdItem)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdItem", SqlDbType.Int, 4);
            aParam[0].Value = nIdItem;

            if (tr == null)
                return (bool)SqlHelper.ExecuteScalar("SUP_ITEM_PLANT_OBLIGA", aParam);
            else
                return (bool)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ITEM_PLANT_OBLIGA", aParam);
        }
        /// <summary>
        /// 
        /// Comprueba si un item de plantilla (tarea) es de avance automatico,
        /// </summary>
        public static bool bAvanceAutomatico(SqlTransaction tr, int nIdItem)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdItem", SqlDbType.Int, 4);
            aParam[0].Value = nIdItem;

            if (tr == null)
                return (bool)SqlHelper.ExecuteScalar("SUP_ITEM_PLANT_AVANCE", aParam);
            else
                return (bool)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ITEM_PLANT_AVANCE", aParam);
        }
        /// <summary>
        /// 
        /// Inserta en una plantilla los items de otra plantilla
        /// </summary>
        public static void Duplicar(SqlTransaction tr, int nIdOrigen, int nIdDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdOrigen", SqlDbType.Int, 4);
            aParam[0].Value = nIdOrigen;
            aParam[1] = new SqlParameter("@nIdDestino", SqlDbType.Int, 4);
            aParam[1].Value = nIdDestino;

            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_ITEM_PLANT_DUPLICAR", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ITEM_PLANT_DUPLICAR", aParam);
        }

		#endregion
	}
}
