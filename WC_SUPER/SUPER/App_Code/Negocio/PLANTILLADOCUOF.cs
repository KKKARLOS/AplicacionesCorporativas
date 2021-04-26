using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PLANTILLADOCUOF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T631_PLANTILLADOCUOF
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/11/2010 10:31:35	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PLANTILLADOCUOF
	{
		#region Propiedades y Atributos

		private int _t631_iddocuplanof;
		public int t631_iddocuplanof
		{
			get {return _t631_iddocuplanof;}
			set { _t631_iddocuplanof = value ;}
		}

		private int? _t629_idplantillaof;
		public int? t629_idplantillaof
		{
			get {return _t629_idplantillaof;}
			set { _t629_idplantillaof = value ;}
		}

		private string _t631_descripcion;
		public string t631_descripcion
		{
			get {return _t631_descripcion;}
			set { _t631_descripcion = value ;}
		}

		private string _t631_nombrearchivo;
		public string t631_nombrearchivo
		{
			get {return _t631_nombrearchivo;}
			set { _t631_nombrearchivo = value ;}
		}

		private byte[] _t631_archivo;
		public byte[] t631_archivo
		{
			get {return _t631_archivo;}
			set { _t631_archivo = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t631_fecha;
		public DateTime t631_fecha
		{
			get {return _t631_fecha;}
			set { _t631_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t631_fechamodif;
		public DateTime t631_fechamodif
		{
			get {return _t631_fechamodif;}
			set { _t631_fechamodif = value ;}
		}

		private string _t631_usuticks;
		public string t631_usuticks
		{
			get {return _t631_usuticks;}
			set { _t631_usuticks = value ;}
		}

        private string _DesAutor;
        public string DesAutor
        {
            get { return _DesAutor; }
            set { _DesAutor = value; }
        }

        private string _DesAutorModif;
        public string DesAutorModif
        {
            get { return _DesAutorModif; }
            set { _DesAutorModif = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }
        #endregion

		#region Constructor

		public PLANTILLADOCUOF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T631_PLANTILLADOCUOF.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, Nullable<int> t629_idplantillaof , string t631_descripcion , string t631_nombrearchivo ,
                                 Nullable<long> idContentServer, int t314_idusuario_autor, string t631_usuticks)
		{
            //if (t631_archivo.Length == 0) t631_archivo = null; 
            SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t631_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t631_descripcion;
			aParam[2] = new SqlParameter("@t631_nombrearchivo", SqlDbType.Text, 250);
			aParam[2].Value = t631_nombrearchivo;
            //aParam[3] = new SqlParameter("@t631_archivo", SqlDbType.Binary, 2147483647);
            //aParam[3].Value = t631_archivo;
            aParam[3] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[3].Value = idContentServer;
            aParam[4] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_autor;
			aParam[5] = new SqlParameter("@t631_usuticks", SqlDbType.Text, 50);
			aParam[5].Value = t631_usuticks;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLADOCUOF_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLADOCUOF_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T631_PLANTILLADOCUOF.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t631_iddocuplanof, Nullable<int> t629_idplantillaof, string t631_descripcion,
                                 string t631_nombrearchivo, Nullable<long> idContentServer, int t314_idusuario_modif)
		{
            //if (t631_archivo.Length == 0) t631_archivo = null;
            SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t631_iddocuplanof", SqlDbType.Int, 4);
			aParam[0].Value = t631_iddocuplanof;
			aParam[1] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[1].Value = t629_idplantillaof;
			aParam[2] = new SqlParameter("@t631_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t631_descripcion;
			aParam[3] = new SqlParameter("@t631_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t631_nombrearchivo;
            //aParam[4] = new SqlParameter("@t631_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t631_archivo;
            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLADOCUOF_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLADOCUOF_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T631_PLANTILLADOCUOF a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t631_iddocuplanof)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t631_iddocuplanof", SqlDbType.Int, 4);
			aParam[0].Value = t631_iddocuplanof;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLADOCUOF_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLADOCUOF_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T631_PLANTILLADOCUOF,
		/// y devuelve una instancia u objeto del tipo PLANTILLADOCUOF
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
        public static PLANTILLADOCUOF Select(SqlTransaction tr, int t631_iddocuplanof)//, bool bTraerArchivo) 
		{
			PLANTILLADOCUOF o = new PLANTILLADOCUOF();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t631_iddocuplanof", SqlDbType.Int, 4);
			aParam[0].Value = t631_iddocuplanof;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLADOCUOF_O", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLADOCUOF_O", aParam);

			if (dr.Read())
			{
				if (dr["t631_iddocuplanof"] != DBNull.Value)
					o.t631_iddocuplanof = int.Parse(dr["t631_iddocuplanof"].ToString());
				if (dr["t629_idplantillaof"] != DBNull.Value)
					o.t629_idplantillaof = int.Parse(dr["t629_idplantillaof"].ToString());
				if (dr["t631_descripcion"] != DBNull.Value)
					o.t631_descripcion = (string)dr["t631_descripcion"];
				if (dr["t631_nombrearchivo"] != DBNull.Value)
					o.t631_nombrearchivo = (string)dr["t631_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    //if (dr["t2_iddocumento"].ToString() != "")
                //    //{
                //    //    SUPER.BLL.ContentServer oCS = new SUPER.BLL.ContentServer(long.Parse(dr["t2_iddocumento"].ToString()));
                //    //    o.sError = oCS.Error;
                //    //    o.t631_archivo = oCS.Archivo;
                //    //}
                //    //else
                //    if (dr["t2_iddocumento"].ToString() == "")
                //    {
                //        //if (dr["t631_archivo"] == System.DBNull.Value)
                //        //    o.sError = "El archivo no tiene contenido";
                //        //else
                //            o.t631_archivo = (byte[])dr["t631_archivo"];
                //    }
                //}
                if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = int.Parse(dr["t314_idusuario_autor"].ToString());
				if (dr["t631_fecha"] != DBNull.Value)
					o.t631_fecha = (DateTime)dr["t631_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = int.Parse(dr["t314_idusuario_modif"].ToString());
				if (dr["t631_fechamodif"] != DBNull.Value)
					o.t631_fechamodif = (DateTime)dr["t631_fechamodif"];
				if (dr["t631_usuticks"] != DBNull.Value)
					o.t631_usuticks = (string)dr["t631_usuticks"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de PLANTILLADOCUOF"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        public static SqlDataReader Catalogo(int t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
            aParam[0].Value = t629_idplantillaof;

            return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLADOCUOF_CAT", aParam);
        }
        public static SqlDataReader CatalogoByUsuTicks(string t631_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t631_usuticks", SqlDbType.VarChar, 50);
            aParam[0].Value = t631_usuticks;

            return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLADOCUOF_ByUsuTicks_CAT", aParam);
        }

        public static List<PLANTILLADOCUOF> Lista(SqlTransaction tr, int t629_idplantillaof)
        {
            List<PLANTILLADOCUOF> oLista = new List<PLANTILLADOCUOF>();
            PLANTILLADOCUOF oElem;

            SqlDataReader dr = SUPER.DAL.PLANTILLADOCUOF.Catalogo(tr, t629_idplantillaof);
            while (dr.Read())
            {
                oElem = new PLANTILLADOCUOF();
                oElem.t629_idplantillaof = t629_idplantillaof;
                oElem.t631_descripcion = dr["t631_descripcion"].ToString();
                oElem.t631_nombrearchivo = dr["t631_nombrearchivo"].ToString();
                if (dr["t2_iddocumento"].ToString() != "")
                {//Recojo el contenido del archivo de Atenea
                    oElem.t631_archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)dr["t2_iddocumento"]).content;
                }
                //else
                //{//El archivo no está en Atenea
                //    oElem.t631_archivo = (byte[])dr["t631_archivo"];
                //}
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        #endregion
	}
}
