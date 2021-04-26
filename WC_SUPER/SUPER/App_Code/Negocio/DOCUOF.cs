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
	/// Class	 : DOCUOF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T624_DOCUOF
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/10/2010 10:14:49	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUOF
	{
		#region Propiedades y Atributos

		private int _t624_iddocuof;
		public int t624_iddocuof
		{
			get {return _t624_iddocuof;}
			set { _t624_iddocuof = value ;}
		}

		private int _t610_idordenfac;
		public int t610_idordenfac
		{
			get {return _t610_idordenfac;}
			set { _t610_idordenfac = value ;}
		}

		private string _t624_descripcion;
		public string t624_descripcion
		{
			get {return _t624_descripcion;}
			set { _t624_descripcion = value ;}
		}

		private string _t624_nombrearchivo;
		public string t624_nombrearchivo
		{
			get {return _t624_nombrearchivo;}
			set { _t624_nombrearchivo = value ;}
		}

		private byte[] _t624_archivo;
		public byte[] t624_archivo
		{
			get {return _t624_archivo;}
			set { _t624_archivo = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t624_fecha;
		public DateTime t624_fecha
		{
			get {return _t624_fecha;}
			set { _t624_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t624_fechamodif;
		public DateTime t624_fechamodif
		{
			get {return _t624_fechamodif;}
			set { _t624_fechamodif = value ;}
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

		public DOCUOF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T624_DOCUOF.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/10/2010 10:14:49
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<int> t610_idordenfac, string t624_descripcion, string t624_nombrearchivo,
                                 Nullable<long> idContentServer, byte[] t624_archivo, int t314_idusuario_autor, string t624_usuticks)
		{
            if (t624_archivo.Length == 0) t624_archivo = null; 
            
            SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
			aParam[0].Value = t610_idordenfac;
			aParam[1] = new SqlParameter("@t624_descripcion", SqlDbType.VarChar, 50);
			aParam[1].Value = t624_descripcion;
			aParam[2] = new SqlParameter("@t624_nombrearchivo", SqlDbType.VarChar, 250);
			aParam[2].Value = t624_nombrearchivo;

            aParam[3] = new SqlParameter("@t624_archivo", SqlDbType.Binary, 2147483647);
            aParam[3].Value = t624_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_autor;
            aParam[6] = new SqlParameter("@t624_usuticks", SqlDbType.VarChar, 50);
            aParam[6].Value = t624_usuticks;
           

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUOF_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUOF_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T624_DOCUOF.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/10/2010 10:14:49
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t624_iddocuof, int t610_idordenfac, string t624_descripcion, string t624_nombrearchivo,
                                 Nullable<long> idContentServer, byte[] t624_archivo, int t314_idusuario_modif)
		{
            if (t624_archivo.Length == 0) t624_archivo = null;
            
            SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t624_iddocuof", SqlDbType.Int, 4);
			aParam[0].Value = t624_iddocuof;
			aParam[1] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            if (t610_idordenfac==0)
                aParam[1].Value = null;
            else
			    aParam[1].Value = t610_idordenfac;
            aParam[2] = new SqlParameter("@t624_descripcion", SqlDbType.VarChar, 50);
			aParam[2].Value = t624_descripcion;
            aParam[3] = new SqlParameter("@t624_nombrearchivo", SqlDbType.VarChar, 250);
			aParam[3].Value = t624_nombrearchivo;


            aParam[4] = new SqlParameter("@t624_archivo", SqlDbType.Binary, 2147483647);
            aParam[4].Value = t624_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
			aParam[6].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_DOCUOF_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUOF_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T624_DOCUOF a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/10/2010 10:14:49
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t624_iddocuof)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t624_iddocuof", SqlDbType.Int, 4);
			aParam[0].Value = t624_iddocuof;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_DOCUOF_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUOF_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T624_DOCUOF,
		/// y devuelve una instancia u objeto del tipo DOCUOF
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/10/2010 10:14:49
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUOF Select(SqlTransaction tr, int t624_iddocuof, bool bTraerArchivo) 
		{
			DOCUOF o = new DOCUOF();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t624_iddocuof", SqlDbType.Int, 4);
			aParam[0].Value = t624_iddocuof;
            aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUOF_O", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUOF_O", aParam);

			if (dr.Read())
			{
				if (dr["t624_iddocuof"] != DBNull.Value)
					o.t624_iddocuof = int.Parse(dr["t624_iddocuof"].ToString());
				if (dr["t610_idordenfac"] != DBNull.Value)
					o.t610_idordenfac = int.Parse(dr["t610_idordenfac"].ToString());
				if (dr["t624_descripcion"] != DBNull.Value)
					o.t624_descripcion = (string)dr["t624_descripcion"];
				if (dr["t624_nombrearchivo"] != DBNull.Value)
					o.t624_nombrearchivo = (string)dr["t624_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                if (bTraerArchivo)
                {
                    if (dr["t2_iddocumento"].ToString() == "")
                        o.t624_archivo = (byte[])dr["t624_archivo"];
                }
                if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = int.Parse(dr["t314_idusuario_autor"].ToString());
				if (dr["t624_fecha"] != DBNull.Value)
					o.t624_fecha = (DateTime)dr["t624_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = int.Parse(dr["t314_idusuario_modif"].ToString());
				if (dr["t624_fechamodif"] != DBNull.Value)
					o.t624_fechamodif = (DateTime)dr["t624_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUOF"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        public static SqlDataReader Catalogo(int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUOF_CAT", aParam);
        }
        public static SqlDataReader CatalogoByUsuTicks(string t624_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t624_usuticks", SqlDbType.VarChar, 50);
            aParam[0].Value = t624_usuticks;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUOF_ByUsuTicks_CAT", aParam);
        }

        public static List<DOCUOF> Lista(SqlTransaction tr, int t610_idordenfac)
        {
            List<DOCUOF> oLista = new List<DOCUOF>();
            DOCUOF oElem;

            SqlDataReader dr = SUPER.DAL.DOCUOF.Catalogo(tr, t610_idordenfac);
            while (dr.Read())
            {
                oElem = new DOCUOF();
                oElem.t610_idordenfac = t610_idordenfac;
                oElem.t624_descripcion = dr["t624_descripcion"].ToString();
                oElem.t624_nombrearchivo = dr["t624_nombrearchivo"].ToString();
                if (dr["t2_iddocumento"].ToString() != "")
                {//Recojo el contenido del archivo de Atenea
                    oElem.t624_archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)dr["t2_iddocumento"]).content;
                }
                //else
                //{//El archivo no está en Atenea
                //    oElem.t624_archivo = (byte[])dr["t624_archivo"];
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
