using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FOTOSEGT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T375_FOTOSEGT
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	14/01/2009 9:20:21	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FOTOSEGT
    {
        #region Propiedades y Atributos

        private int _t375_idfotot;
        public int t375_idfotot
        {
            get { return _t375_idfotot; }
            set { _t375_idfotot = value; }
        }

        private int _t374_idfotopt;
        public int t374_idfotopt
        {
            get { return _t374_idfotopt; }
            set { _t374_idfotopt = value; }
        }

        private int? _t461_idfotosega;
        public int? t461_idfotosega
        {
            get { return _t461_idfotosega; }
            set { _t461_idfotosega = value; }
        }

        private string _t375_denominacion;
        public string t375_denominacion
        {
            get { return _t375_denominacion; }
            set { _t375_denominacion = value; }
        }

        private double? _t375_totpl;
        public double? t375_totpl
        {
            get { return _t375_totpl; }
            set { _t375_totpl = value; }
        }

        private DateTime? _t375_inipl;
        public DateTime? t375_inipl
        {
            get { return _t375_inipl; }
            set { _t375_inipl = value; }
        }

        private DateTime? _t375_finpl;
        public DateTime? t375_finpl
        {
            get { return _t375_finpl; }
            set { _t375_finpl = value; }
        }

        private decimal? _t375_presupuesto;
        public decimal? t375_presupuesto
        {
            get { return _t375_presupuesto; }
            set { _t375_presupuesto = value; }
        }

        private double? _t375_mes;
        public double? t375_mes
        {
            get { return _t375_mes; }
            set { _t375_mes = value; }
        }

        private double? _t375_acumulado;
        public double? t375_acumulado
        {
            get { return _t375_acumulado; }
            set { _t375_acumulado = value; }
        }

        private double? _t375_pendest;
        public double? t375_pendest
        {
            get { return _t375_pendest; }
            set { _t375_pendest = value; }
        }

        private double? _t375_totalest;
        public double? t375_totalest
        {
            get { return _t375_totalest; }
            set { _t375_totalest = value; }
        }

        private DateTime? _t375_finest;
        public DateTime? t375_finest
        {
            get { return _t375_finest; }
            set { _t375_finest = value; }
        }

        private double? _t375_totalpr;
        public double? t375_totalpr
        {
            get { return _t375_totalpr; }
            set { _t375_totalpr = value; }
        }

        private double? _t375_pendpr;
        public double? t375_pendpr
        {
            get { return _t375_pendpr; }
            set { _t375_pendpr = value; }
        }

        private DateTime? _t375_finpr;
        public DateTime? t375_finpr
        {
            get { return _t375_finpr; }
            set { _t375_finpr = value; }
        }

        private double? _t375_porcpr;
        public double? t375_porcpr
        {
            get { return _t375_porcpr; }
            set { _t375_porcpr = value; }
        }

        private double? _t375_porcav;
        public double? t375_porcav
        {
            get { return _t375_porcav; }
            set { _t375_porcav = value; }
        }

        private double? _t375_producido;
        public double? t375_producido
        {
            get { return _t375_producido; }
            set { _t375_producido = value; }
        }

        private double? _t375_porccon;
        public double? t375_porccon
        {
            get { return _t375_porccon; }
            set { _t375_porccon = value; }
        }

        private double? _t375_porcdes;
        public double? t375_porcdes
        {
            get { return _t375_porcdes; }
            set { _t375_porcdes = value; }
        }

        private double? _t375_PorcDesPlazo;
        public double? t375_PorcDesPlazo
        {
            get { return _t375_PorcDesPlazo; }
            set { _t375_PorcDesPlazo = value; }
        }

        private int _t375_orden;
        public int t375_orden
        {
            get { return _t375_orden; }
            set { _t375_orden = value; }
        }
        #endregion

        #region Constructor

        public FOTOSEGT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T375_FOTOSEGT.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	14/01/2009 9:20:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t374_idfotopt, Nullable<int> t461_idfotosega, string t375_denominacion, Nullable<double> t375_totpl, Nullable<DateTime> t375_inipl, Nullable<DateTime> t375_finpl, Nullable<decimal> t375_presupuesto, Nullable<double> t375_mes, Nullable<double> t375_acumulado, Nullable<double> t375_pendest, Nullable<double> t375_totalest, Nullable<DateTime> t375_finest, Nullable<double> t375_totalpr, Nullable<double> t375_pendpr, Nullable<DateTime> t375_finpr, Nullable<double> t375_porcpr, Nullable<double> t375_porcav, Nullable<decimal> t375_producido, Nullable<double> t375_porccon, Nullable<double> t375_porcdes, Nullable<double> t375_PorcDesPlazo, int t375_orden)
        {
            SqlParameter[] aParam = new SqlParameter[22];
            aParam[0] = new SqlParameter("@t374_idfotopt", SqlDbType.Int, 4);
            aParam[0].Value = t374_idfotopt;
            aParam[1] = new SqlParameter("@t461_idfotosega", SqlDbType.Int, 4);
            aParam[1].Value = t461_idfotosega;
            aParam[2] = new SqlParameter("@t375_denominacion", SqlDbType.Text, 60);
            aParam[2].Value = t375_denominacion;
            aParam[3] = new SqlParameter("@t375_totpl", SqlDbType.Float, 8);
            aParam[3].Value = t375_totpl;
            aParam[4] = new SqlParameter("@t375_inipl", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t375_inipl;
            aParam[5] = new SqlParameter("@t375_finpl", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t375_finpl;
            aParam[6] = new SqlParameter("@t375_presupuesto", SqlDbType.Money, 8);
            aParam[6].Value = t375_presupuesto;
            aParam[7] = new SqlParameter("@t375_mes", SqlDbType.Float, 8);
            aParam[7].Value = t375_mes;
            aParam[8] = new SqlParameter("@t375_acumulado", SqlDbType.Float, 8);
            aParam[8].Value = t375_acumulado;
            aParam[9] = new SqlParameter("@t375_pendest", SqlDbType.Float, 8);
            aParam[9].Value = t375_pendest;
            aParam[10] = new SqlParameter("@t375_totalest", SqlDbType.Float, 8);
            aParam[10].Value = t375_totalest;
            aParam[11] = new SqlParameter("@t375_finest", SqlDbType.SmallDateTime, 4);
            aParam[11].Value = t375_finest;
            aParam[12] = new SqlParameter("@t375_totalpr", SqlDbType.Float, 8);
            aParam[12].Value = t375_totalpr;
            aParam[13] = new SqlParameter("@t375_pendpr", SqlDbType.Float, 8);
            aParam[13].Value = t375_pendpr;
            aParam[14] = new SqlParameter("@t375_finpr", SqlDbType.SmallDateTime, 4);
            aParam[14].Value = t375_finpr;
            aParam[15] = new SqlParameter("@t375_porcpr", SqlDbType.Float, 8);
            aParam[15].Value = t375_porcpr;
            aParam[16] = new SqlParameter("@t375_porcav", SqlDbType.Float, 8);
            aParam[16].Value = t375_porcav;
            aParam[17] = new SqlParameter("@t375_producido", SqlDbType.Money, 8);
            aParam[17].Value = t375_producido;
            aParam[18] = new SqlParameter("@t375_porccon", SqlDbType.Float, 8);
            aParam[18].Value = t375_porccon;
            aParam[19] = new SqlParameter("@t375_porcdes", SqlDbType.Float, 8);
            aParam[19].Value = t375_porcdes;
            aParam[20] = new SqlParameter("@t375_PorcDesPlazo", SqlDbType.Float, 8);
            aParam[20].Value = t375_PorcDesPlazo;
            aParam[21] = new SqlParameter("@t375_orden", SqlDbType.Int, 4);
            aParam[21].Value = t375_orden;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FOTOSEGT_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FOTOSEGT_I", aParam));
        }

        #endregion
    }
}
