using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FUNCIONES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T356_FUNCIONES
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FUNCIONES
	{
		#region Propiedades y Atributos

		private int _t356_idfuncion;
		public int t356_idfuncion
		{
			get {return _t356_idfuncion;}
			set { _t356_idfuncion = value ;}
		}

		private string _t356_desfuncion;
		public string t356_desfuncion
		{
			get {return _t356_desfuncion;}
			set { _t356_desfuncion = value ;}
		}

		private short _t303_idnodo;
		public short t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}
		#endregion

		#region Constructores

		public FUNCIONES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T356_FUNCIONES.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t356_desfuncion , short t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t356_desfuncion", SqlDbType.Text, 40);
			aParam[0].Value = t356_desfuncion;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
			aParam[1].Value = t303_idnodo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FUNCIONES_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FUNCIONES_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T356_FUNCIONES.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t356_idfuncion, string t356_desfuncion, short t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
			aParam[0].Value = t356_idfuncion;
			aParam[1] = new SqlParameter("@t356_desfuncion", SqlDbType.Text, 40);
			aParam[1].Value = t356_desfuncion;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
			aParam[2].Value = t303_idnodo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FUNCIONES_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FUNCIONES_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T356_FUNCIONES a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t356_idfuncion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
			aParam[0].Value = t356_idfuncion;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FUNCIONES_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FUNCIONES_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T356_FUNCIONES,
		/// y devuelve una instancia u objeto del tipo FUNCIONES
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static FUNCIONES Select(SqlTransaction tr, int t356_idfuncion) 
        //{
        //    FUNCIONES o = new FUNCIONES();

        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
        //    aParam[0].Value = t356_idfuncion;

        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_FUNCIONES_S", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FUNCIONES_S", aParam);

        //    if (dr.Read())
        //    {
        //        if (dr["t356_idfuncion"] != DBNull.Value)
        //            o.t356_idfuncion = (int)dr["t356_idfuncion"];
        //        if (dr["t356_desfuncion"] != DBNull.Value)
        //            o.t356_desfuncion = (string)dr["t356_desfuncion"];
        //        if (dr["t303_idnodo"] != DBNull.Value)
        //            o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());

        //    }
        //    else
        //    {
        //        throw (new NullReferenceException("No se ha obtenido ningun dato de FUNCIONES"));
        //    }

        //    dr.Close();
        //    dr.Dispose();

        //    return o;
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T356_FUNCIONES en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static SqlDataReader SelectByt303_idnodo(short t303_idnodo) 
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
        //    aParam[0].Value = t303_idnodo;

        //    SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("PSP_FUNCIONES_SByt303_idnodo", aParam);

        //    return dr;
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T356_FUNCIONES.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t356_idfuncion, string t356_desfuncion, Nullable<short> t303_idnodo, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
			aParam[0].Value = t356_idfuncion;
			aParam[1] = new SqlParameter("@t356_desfuncion", SqlDbType.Text, 40);
			aParam[1].Value = t356_desfuncion;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
			aParam[2].Value = t303_idnodo;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FUNCIONES_C", aParam);
		}

		#endregion
	}
}
