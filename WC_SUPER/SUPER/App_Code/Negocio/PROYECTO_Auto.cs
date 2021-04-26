using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PROYECTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T301_PROYECTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/06/2008 9:02:50	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PROYECTO
	{
		#region Propiedades y Atributos

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private string _t301_estado;
		public string t301_estado
		{
			get {return _t301_estado;}
			set { _t301_estado = value ;}
		}

		private string _t301_denominacion;
		public string t301_denominacion
		{
			get {return _t301_denominacion;}
			set { _t301_denominacion = value ;}
		}

		private string _t301_descripcion;
		public string t301_descripcion
		{
			get {return _t301_descripcion;}
			set { _t301_descripcion = value ;}
		}

		private int _t302_idcliente_proyecto;
		public int t302_idcliente_proyecto
		{
			get {return _t302_idcliente_proyecto;}
			set { _t302_idcliente_proyecto = value ;}
		}

		private DateTime _t301_fcreacion;
		public DateTime t301_fcreacion
		{
			get {return _t301_fcreacion;}
			set { _t301_fcreacion = value ;}
		}

		private int? _t306_idcontrato;
		public int? t306_idcontrato
		{
			get {return _t306_idcontrato;}
			set { _t306_idcontrato = value ;}
		}

		private int? _t307_idhorizontal;
		public int? t307_idhorizontal
		{
			get {return _t307_idhorizontal;}
			set { _t307_idhorizontal = value ;}
		}

		private int _t323_idnaturaleza;
		public int t323_idnaturaleza
		{
			get {return _t323_idnaturaleza;}
			set { _t323_idnaturaleza = value ;}
		}

		private byte? _t316_idmodalidad;
		public byte? t316_idmodalidad
		{
			get {return _t316_idmodalidad;}
			set { _t316_idmodalidad = value ;}
		}

		private DateTime _t301_fiprev;
		public DateTime t301_fiprev
		{
			get {return _t301_fiprev;}
			set { _t301_fiprev = value ;}
		}

		private DateTime _t301_ffprev;
		public DateTime t301_ffprev
		{
			get {return _t301_ffprev;}
			set { _t301_ffprev = value ;}
		}

		private string _t301_categoria;
		public string t301_categoria
		{
			get {return _t301_categoria;}
			set { _t301_categoria = value ;}
		}

		private string _t301_modelocoste;
		public string t301_modelocoste
		{
			get {return _t301_modelocoste;}
			set { _t301_modelocoste = value ;}
		}

		private string _t301_modelotarif;
		public string t301_modelotarif
		{
			get {return _t301_modelotarif;}
			set { _t301_modelotarif = value ;}
		}

        private short? _t301_annoPIG;
        public short? t301_annoPIG
        {
            get { return _t301_annoPIG; }
            set { _t301_annoPIG = value; }
        }

        private byte _t320_idtipologiaproy;
        public byte t320_idtipologiaproy
        {
            get { return _t320_idtipologiaproy; }
            set { _t320_idtipologiaproy = value; }
        }

        private bool _t301_pap;
        public bool t301_pap
        {
            get { return _t301_pap; }
            set { _t301_pap = value; }
        }

        private bool _t301_pgrcg;
        public bool t301_pgrcg
        {
            get { return _t301_pgrcg; }
            set { _t301_pgrcg = value; }
        }

        private bool _t301_esreplicable;
        public bool t301_esreplicable
        {
            get { return _t301_esreplicable; }
            set { _t301_esreplicable = value; }
        }
        
        private bool _t301_activagar;
        public bool t301_activagar
        {
            get { return _t301_activagar; }
            set { _t301_activagar = value; }
        }

        private short? _t301_mesesprevgar;
        public short? t301_mesesprevgar
        {
            get { return _t301_mesesprevgar; }
            set { _t301_mesesprevgar = value; }
        }

        private DateTime? _t301_iniciogar;
        public DateTime? t301_iniciogar
        {
            get { return _t301_iniciogar; }
            set { _t301_iniciogar = value; }
        }

        private DateTime? _t301_fingar;
        public DateTime? t301_fingar
        {
            get { return _t301_fingar; }
            set { _t301_fingar = value; }
        }

        private int? _t195_idlineaoferta;
        public int? t195_idlineaoferta
        {
            get { return _t195_idlineaoferta; }
            set { _t195_idlineaoferta = value; }
        }

        private string _t195_denominacion;
        public string t195_denominacion
        {
            get { return _t195_denominacion; }
            set { _t195_denominacion = value; }
        }

        public bool t323_coste { get; set; }
        #endregion

        #region Constructores y Destructores

        public PROYECTO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T301_PROYECTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/06/2008 9:02:50
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t301_estado, string t301_denominacion, string t301_descripcion, int t302_idcliente_proyecto, Nullable<int> t306_idcontrato, Nullable<int> t307_idhorizontal, int t323_idnaturaleza, Nullable<byte> t316_idmodalidad, DateTime t301_fiprev, DateTime t301_ffprev, string t301_categoria, string t301_modelocoste, string t301_modelotarif, 
            Nullable<short> t301_annoPIG, bool t301_pap, bool t301_pgrcg, bool t301_esreplicable, Nullable<short> t301_mesesprevgar, bool t301_activagar, 
            Nullable<DateTime> t301_iniciogar, Nullable<DateTime> t301_fingar, Nullable<int> t195_idlineaoferta)
		{
			SqlParameter[] aParam = new SqlParameter[22];
			aParam[0] = new SqlParameter("@t301_estado", SqlDbType.Text, 1);
			aParam[0].Value = t301_estado;
			aParam[1] = new SqlParameter("@t301_denominacion", SqlDbType.Text, 70);
			aParam[1].Value = t301_denominacion;
			aParam[2] = new SqlParameter("@t301_descripcion", SqlDbType.Text, 2147483647);
			aParam[2].Value = t301_descripcion;
			aParam[3] = new SqlParameter("@t302_idcliente_proyecto", SqlDbType.Int, 4);
			aParam[3].Value = t302_idcliente_proyecto;
			aParam[4] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[4].Value = t306_idcontrato;
			aParam[5] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[5].Value = t307_idhorizontal;
			aParam[6] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[6].Value = t323_idnaturaleza;
			aParam[7] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[7].Value = t316_idmodalidad;
			aParam[8] = new SqlParameter("@t301_fiprev", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t301_fiprev;
			aParam[9] = new SqlParameter("@t301_ffprev", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t301_ffprev;
			aParam[10] = new SqlParameter("@t301_categoria", SqlDbType.Text, 1);
			aParam[10].Value = t301_categoria;
			aParam[11] = new SqlParameter("@t301_modelocoste", SqlDbType.Text, 1);
			aParam[11].Value = t301_modelocoste;
			aParam[12] = new SqlParameter("@t301_modelotarif", SqlDbType.Text, 1);
			aParam[12].Value = t301_modelotarif;
            aParam[13] = new SqlParameter("@t301_annoPIG", SqlDbType.SmallInt, 2);
            aParam[13].Value = t301_annoPIG;
            aParam[14] = new SqlParameter("@t301_pap", SqlDbType.Bit, 1);
            aParam[14].Value = t301_pap;
            aParam[15] = new SqlParameter("@t301_pgrcg", SqlDbType.Bit, 1);
            aParam[15].Value = t301_pgrcg;
            aParam[16] = new SqlParameter("@t301_esreplicable", SqlDbType.Bit, 1);
            aParam[16].Value = t301_esreplicable;

            aParam[17] = new SqlParameter("@t301_mesesprevgar", SqlDbType.SmallInt, 2);
            if (t301_activagar) t301_mesesprevgar = null;
            aParam[17].Value = t301_mesesprevgar;

            aParam[18] = new SqlParameter("@t301_activagar", SqlDbType.Bit, 1);
            aParam[18].Value = t301_activagar;

			aParam[19] = new SqlParameter("@t301_iniciogar", SqlDbType.SmallDateTime, 4);
			aParam[19].Value = t301_iniciogar;

			aParam[20] = new SqlParameter("@t301_fingar", SqlDbType.SmallDateTime, 4);
			aParam[20].Value = t301_fingar;

            //Si tiene contrato, no puede tener línea de oferta
            aParam[21] = new SqlParameter("@t195_idlineaoferta", SqlDbType.Int, 4);
            if (t306_idcontrato == null)
                aParam[21].Value = t195_idlineaoferta;
            else
                aParam[21].Value = null;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROYECTO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T301_PROYECTO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/06/2008 9:02:50
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t301_idproyecto, string t301_estado, string t301_denominacion, string t301_descripcion, int t302_idcliente_proyecto, Nullable<int> t306_idcontrato, Nullable<int> t307_idhorizontal, int t323_idnaturaleza, Nullable<byte> t316_idmodalidad, DateTime t301_fiprev, DateTime t301_ffprev, string t301_categoria, string t301_modelocoste, string t301_modelotarif, Nullable<short> t301_annoPIG, bool t301_pap, bool t301_pgrcg, bool t301_esreplicable,
            Nullable<short> t301_mesesprevgar, bool t301_activagar, Nullable<DateTime> t301_iniciogar, Nullable<DateTime> t301_fingar, Nullable<int> t195_idlineaoferta)
		{
            SqlParameter[] aParam = new SqlParameter[23];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t301_estado", SqlDbType.Text, 1);
			aParam[1].Value = t301_estado;
			aParam[2] = new SqlParameter("@t301_denominacion", SqlDbType.Text, 70);
			aParam[2].Value = t301_denominacion;
			aParam[3] = new SqlParameter("@t301_descripcion", SqlDbType.Text, 2147483647);
			aParam[3].Value = t301_descripcion;
			aParam[4] = new SqlParameter("@t302_idcliente_proyecto", SqlDbType.Int, 4);
			aParam[4].Value = t302_idcliente_proyecto;
			aParam[5] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[5].Value = t306_idcontrato;
			aParam[6] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[6].Value = t307_idhorizontal;
			aParam[7] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[7].Value = t323_idnaturaleza;
			aParam[8] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[8].Value = t316_idmodalidad;
			aParam[9] = new SqlParameter("@t301_fiprev", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t301_fiprev;
			aParam[10] = new SqlParameter("@t301_ffprev", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t301_ffprev;
			aParam[11] = new SqlParameter("@t301_categoria", SqlDbType.Text, 1);
			aParam[11].Value = t301_categoria;
			aParam[12] = new SqlParameter("@t301_modelocoste", SqlDbType.Text, 1);
			aParam[12].Value = t301_modelocoste;
			aParam[13] = new SqlParameter("@t301_modelotarif", SqlDbType.Text, 1);
			aParam[13].Value = t301_modelotarif;
            aParam[14] = new SqlParameter("@t301_annoPIG", SqlDbType.SmallInt, 2);
            aParam[14].Value = t301_annoPIG;
            aParam[15] = new SqlParameter("@t301_pap", SqlDbType.Bit, 1);
            aParam[15].Value = t301_pap;
            aParam[16] = new SqlParameter("@t301_pgrcg", SqlDbType.Bit, 1);
            aParam[16].Value = t301_pgrcg;
            aParam[17] = new SqlParameter("@t301_esreplicable", SqlDbType.Bit, 1);
            aParam[17].Value = t301_esreplicable;

            aParam[18] = new SqlParameter("@t301_mesesprevgar", SqlDbType.SmallInt, 2);
            if (t301_activagar) t301_mesesprevgar = null;
            aParam[18].Value = t301_mesesprevgar;

            aParam[19] = new SqlParameter("@t301_activagar", SqlDbType.Bit, 1);
            aParam[19].Value = t301_activagar;

            aParam[20] = new SqlParameter("@t301_iniciogar", SqlDbType.SmallDateTime, 4);
            aParam[20].Value = t301_iniciogar;

            aParam[21] = new SqlParameter("@t301_fingar", SqlDbType.SmallDateTime, 4);
            aParam[21].Value = t301_fingar;
            
            //Si tiene contrato, no puede tener línea de oferta
            aParam[22] = new SqlParameter("@t195_idlineaoferta", SqlDbType.Int, 4);
            if (t306_idcontrato == null)
                aParam[22].Value = t195_idlineaoferta;
            else
                aParam[22].Value = null;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_U", aParam);
		}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T301_PROYECTO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	22/09/2008 16:08:10
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_D", aParam);
        }

		#endregion
	}
}
