using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : AEITEMSPLANTILLA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t371_AEITEMSPLANTILLA
	/// </summary>
	/// <history>
	/// 	Creado por [doarhumi]	27/11/2007 14:56:06	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AEITEMSPLANTILLA
	{

		#region Propiedades y Atributos

		private int _t339_iditems;
		public int t339_iditems
		{
			get {return _t339_iditems;}
			set { _t339_iditems = value ;}
		}

		private int _t341_idae;
		public int t341_idae
		{
			get {return _t341_idae;}
			set { _t341_idae = value ;}
		}

		private int _t340_idvae;
		public int t340_idvae
		{
			get {return _t340_idvae;}
			set { _t340_idvae = value ;}
		}
		#endregion

		#region Constructores

		public AEITEMSPLANTILLA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t371_AEITEMSPLANTILLA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [doarhumi]	27/11/2007 14:56:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t339_iditems, int t340_idvae)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
            aParam[0].Value = t339_iditems;
            aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[1].Value = t340_idvae;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("SUP_AEITEMSPLANTILLA_I", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEITEMSPLANTILLA_I", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta en la tabla t371_AEITEMSPLANTILLA sacando los valores de T049 o T055 en función del tipo.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [doarhumi]	24/04/2007 14:56:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarAE(SqlTransaction tr, string sTipoItem, int idElemento, int t339_iditems)
        {
            string sProc;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
            aParam[0].Value = t339_iditems;
            aParam[1] = new SqlParameter("@idElemento", SqlDbType.Int, 4);
            aParam[1].Value = idElemento;

            switch (sTipoItem)
            {
                case "P":
                    sProc = "SUP_AEITEMSPLANTILLA_IP";
                    break;
                case "T":
                    sProc = "SUP_AEITEMSPLANTILLA_IT";
                    break;
                default:
                    return;
            }
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery(sProc, aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, sProc, aParam);
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t371_AEITEMSPLANTILLA.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	27/11/2007 14:56:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t339_iditems, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AEITEMSPLANTILLA_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEITEMSPLANTILLA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t371_AEITEMSPLANTILLA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [doarhumi]	27/11/2007 14:56:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t339_iditems, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AEITEMSPLANTILLA_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEITEMSPLANTILLA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t371_AEITEMSPLANTILLA en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [doarhumi]	27/11/2007 14:56:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt339_iditems(SqlTransaction tr, int t339_iditems) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[0].Value = t339_iditems;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_AEITEMSPLANTILLA_SByt339_iditems", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_AEITEMSPLANTILLA_SByt339_iditems", aParam);
		}

		#endregion
	}
}
